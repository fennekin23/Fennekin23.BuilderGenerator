using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.MinVer;
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
    EnableGitHubToken = true,
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

    [MinVer]
    readonly MinVer MinVer;
    
    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution(GenerateProjects = true, SuppressBuildProjectCheck = true)]
    readonly Solution Solution;

    readonly AbsolutePath SourceDirectory = RootDirectory / "src";
    readonly AbsolutePath ArtifactsDirectory = RootDirectory / "artifacts";

    GitHubActions GitHubActions => GitHubActions.Instance;
    
    bool IsTag => GitHubActions?.Ref?.StartsWith("refs/tags/") ?? false;

    [Parameter] [Secret] readonly string NuGetApiKey;
    [Parameter] readonly string NuGetUrl;

    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
            SourceDirectory.GlobDirectories("**/bin", "**/obj").DeleteDirectories();
            ArtifactsDirectory.CreateOrCleanDirectory();
        });

    Target Restore => _ => _
        .Executes(() =>
        {
            DotNetRestore(s => s.SetProjectFile(Solution));
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            ReportSummary(s =>
                s.AddPairWhenValueNotNull("Version", MinVer.MinVerVersion));
            
            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .SetContinuousIntegrationBuild(IsServerBuild)
                .SetAssemblyVersion(MinVer.AssemblyVersion)
                .SetFileVersion(MinVer.FileVersion)
                .SetInformationalVersion(MinVer.MinVerVersion)
                .SetVersion(MinVer.MinVerVersion)
                .EnableNoLogo()
                .EnableNoRestore());
        });

    Target Test => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            DotNetTest(s => s
                .SetProjectFile(Solution.Fennekin23_BuilderGenerator_Tests)
                .SetConfiguration(Configuration)
                .SetLoggers(["GitHubActions"])
                .EnableNoLogo()
                .EnableNoRestore());
        });
    
    Target Pack => _ => _
        .DependsOn(Compile)
        .After(Test)
        .Produces(ArtifactsDirectory)
        .Executes(() =>
        {
            ReportSummary(s =>
                s.AddPairWhenValueNotNull("Version", MinVer.PackageVersion));
            
            DotNetPack(s => s
                .SetProject(Solution.Fennekin23_BuilderGenerator)
                .SetConfiguration(Configuration)
                .SetContinuousIntegrationBuild(IsServerBuild)
                .SetOutputDirectory(ArtifactsDirectory)
                .SetVersion(MinVer.PackageVersion)
                .EnableNoBuild()
                .EnableNoLogo()
                .EnableNoRestore());
        });

    Target PushToNuGet => _ => _
        .DependsOn(Pack)
        .Requires(() => NuGetApiKey)
        .Requires(() => NuGetUrl)
        .OnlyWhenStatic(() => IsTag && IsServerBuild)
        .Executes(() =>
        {
            var packages = ArtifactsDirectory.GlobFiles("*.nupkg");
            
            DotNetNuGetPush(s => s
                .SetApiKey(NuGetApiKey)
                .SetSource(NuGetUrl)
                .EnableSkipDuplicate()
                .CombineWith(packages, (x, package) => x
                    .SetTargetPath(package)));
        });
}
