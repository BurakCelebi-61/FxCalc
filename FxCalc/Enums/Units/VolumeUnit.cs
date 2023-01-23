using FxCalc.CustomAttribute;

namespace FxCalc.Enums.Units
{
    [Unit(Description = "Metre küp", Symbol = "m³", DefaultValue = "1", Type = "VolumeUnit")]
    public enum VolumeUnit
    {
        [Unit(Description = "Kilometre küp", Symbol = "Km³", DefaultValue = "1000000000", Type = "VolumeUnit")]
        cubicKilometers = 3,
        [Unit(Description = "Hektometre küp", Symbol = "Hm³", DefaultValue = "1000000", Type = "VolumeUnit")]
        CubicHectometer = 2,
        [Unit(Description = "Dekametre küp", Symbol = "dam³", DefaultValue = "1000", Type = "VolumeUnit")]
        CubicDecameter = 1,
        [Unit(Description = "Metre küp", Symbol = "m³", DefaultValue = "1")]
        CubicMeter = 0,
        [Unit(Description = "Desimetre küp", Symbol = "dm³", DefaultValue = "-1000", Type = "VolumeUnit")]
        CubicDecimeters = -1,
        [Unit(Description = "Santimetre küp", Symbol = "cm³", DefaultValue = "-1000000", Type = "VolumeUnit")]
        cubicCentimeters = -2,
        [Unit(Description = "Milimetre küp", Symbol = "mm³", DefaultValue = "-1000000000", Type = "VolumeUnit")]
        cubicMillimeter = -3,
    }
}
