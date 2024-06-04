﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the Fennekin23.BuilderGenerator source generator
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#nullable enable

namespace Fennekin23.BuilderGenerator.Tests.Samples
{
    /// <summary>
    /// Builder class for <see cref="Fennekin23.BuilderGenerator.Tests.Samples.SampleInternalPositionalRecord" />
    /// <summary>
    internal sealed partial class SampleInternalPositionalRecordBuilder
    {
        private String _stringproperty = default!;
        public SampleInternalPositionalRecordBuilder WithStringProperty(String value)
        {
            _stringproperty = value;
            return this;
        }
        private Int32 _intproperty = default!;
        public SampleInternalPositionalRecordBuilder WithIntProperty(Int32 value)
        {
            _intproperty = value;
            return this;
        }
        private Boolean _booleanproperty = default!;
        public SampleInternalPositionalRecordBuilder WithBooleanProperty(Boolean value)
        {
            _booleanproperty = value;
            return this;
        }
        private Int64 _longproperty = default!;
        public SampleInternalPositionalRecordBuilder WithLongProperty(Int64 value)
        {
            _longproperty = value;
            return this;
        }
        public Fennekin23.BuilderGenerator.Tests.Samples.SampleInternalPositionalRecord Build()
        {
            return new Fennekin23.BuilderGenerator.Tests.Samples.SampleInternalPositionalRecord(StringProperty: _stringproperty, IntProperty: _intproperty)
            {
                BooleanProperty = _booleanproperty,
                LongProperty = _longproperty
            };
        }
    }
}