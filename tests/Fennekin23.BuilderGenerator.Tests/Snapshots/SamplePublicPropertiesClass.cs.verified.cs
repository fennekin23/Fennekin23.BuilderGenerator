﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the Fennekin23.BuilderGenerator source generator
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#nullable enable

namespace Fennekin23.BuilderGenerator.Tests
{
    /// <summary>
    /// Builder class for <see cref="Fennekin23.BuilderGenerator.Tests.SamplePublicPropertiesClass" />
    /// <summary>
    public sealed partial class SamplePublicPropertiesClassBuilder
    {
        private String _stringproperty = default!;
        public SamplePublicPropertiesClassBuilder WithStringProperty(String value)
        {
            _stringproperty = value;
            return this;
        }
        private Int32 _intproperty = default!;
        public SamplePublicPropertiesClassBuilder WithIntProperty(Int32 value)
        {
            _intproperty = value;
            return this;
        }
        public Fennekin23.BuilderGenerator.Tests.SamplePublicPropertiesClass Build()
        {
            return new Fennekin23.BuilderGenerator.Tests.SamplePublicPropertiesClass()
            {
                StringProperty = _stringproperty,
                IntProperty = _intproperty
            };
        }
    }
}