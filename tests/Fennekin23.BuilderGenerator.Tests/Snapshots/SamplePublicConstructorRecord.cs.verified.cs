﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the Fennekin23.BuilderGenerator source generator.
//     Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
#nullable enable
namespace Fennekin23.BuilderGenerator.Tests.Samples
{
    /// <summary>
    ///    Builder class for <see cref="Fennekin23.BuilderGenerator.Tests.Samples.SamplePublicConstructorRecord" />.
    /// </summary>
    public sealed class SamplePublicConstructorRecordBuilder
    {
        private String _stringproperty = default!;
        public SamplePublicConstructorRecordBuilder WithstringProperty(String value)
        {
            _stringproperty = value;
            return this;
        }
        private Int32? _intproperty = default!;
        public SamplePublicConstructorRecordBuilder WithintProperty(Int32? value)
        {
            _intproperty = value;
            return this;
        }
        public Fennekin23.BuilderGenerator.Tests.Samples.SamplePublicConstructorRecord Build()
        {
            return
                new Fennekin23.BuilderGenerator.Tests.Samples.SamplePublicConstructorRecord
                (
                    stringProperty: _stringproperty,
                    intProperty: _intproperty
                )
                {
                };
        }
    }
}
