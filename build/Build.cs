using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitVersion;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

[ShutdownDotNetAfterServerBuild]
[GitHubActions("BuildAndPackage",
    GitHubActionsImage.UbuntuLatest,
    // GitHubActionsImage.WindowsLatest,
    // GitHubActionsImage.MacOsLatest,
    OnPushTags = ["*"],
    OnPushBranches = ["main"],
    OnPullRequestBranches = ["*"],
    AutoGenerate = false,
    InvokedTargets = [nameof(Clean), nameof(Compile)]
)]
class Build : NukeBuild
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode

    public static int Main () => Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution(GenerateProjects = true, SuppressBuildProjectCheck = true)]
    readonly Solution Solution;

    AbsolutePath SourceDirectory => RootDirectory / "src";

    [GitVersion]
    readonly GitVersion GitVersion;
    
    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
            SourceDirectory.GlobDirectories("**/bin", "**/obj").DeleteDirectories();
        });

    Target Restore => _ => _
        .Executes(() =>
        {
            DotNetRestore(s => 
                s.SetProjectFile(Solution));
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            ReportSummary(s =>
                s.AddPairWhenValueNotNull("Version", GitVersion.SemVer));
            
            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .SetContinuousIntegrationBuild(IsServerBuild)
                .SetAssemblyVersion(GitVersion.AssemblySemVer)
                .SetFileVersion(GitVersion.AssemblySemFileVer)
                .SetInformationalVersion(GitVersion.InformationalVersion)
                .EnableNoLogo()
                .EnableNoRestore());
        });
}
