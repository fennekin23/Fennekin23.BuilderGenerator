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
    /// Builder class for <see cref="Fennekin23.BuilderGenerator.Tests.SamplePublicPositionalClass" />
    /// <summary>
    public sealed partial class SamplePublicPositionalClassBuilder
    {
        private String _stringproperty = default!;
        public SamplePublicPositionalClassBuilder WithStringProperty(String value)
        {
            _stringproperty = value;
            return this;
        }
        private Int32 _intproperty = default!;
        public SamplePublicPositionalClassBuilder WithIntProperty(Int32 value)
        {
            _intproperty = value;
            return this;
        }
        private Boolean _booleanproperty = default!;
        public SamplePublicPositionalClassBuilder WithBooleanProperty(Boolean value)
        {
            _booleanproperty = value;
            return this;
        }
        private Int64 _longproperty = default!;
        public SamplePublicPositionalClassBuilder WithLongProperty(Int64 value)
        {
            _longproperty = value;
            return this;
        }
        public Fennekin23.BuilderGenerator.Tests.SamplePublicPositionalClass Build()
        {
            return new Fennekin23.BuilderGenerator.Tests.SamplePublicPositionalClass(StringProperty: _stringproperty, IntProperty: _intproperty)
            {
                BooleanProperty = _booleanproperty,
                LongProperty = _longproperty
            };
        }
    }
}