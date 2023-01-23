using FxCalc.CustomAttribute;

namespace FxCalc.Enums.Units
{
    [Unit(Description = "Meter", Symbol = "m", DefaultValue = "1", Type = "LengthUnit")]
    public enum LengthUnit
    {
        [Unit(Description = "Megametre", Symbol = "Mm", DefaultValue = "1000000", Type = "LengthUnit")]
        Megameter = 4,
        [Unit(Description = "Kilometre", Symbol = "km", DefaultValue = "1000", Type = "LengthUnit")]
        Kilometer = 3,
        [Unit(Description = "Hektometre", Symbol = "hm", DefaultValue = "100", Type = "LengthUnit")]
        hectometer = 2,
        [Unit(Description = "Dekametre", Symbol = "dam", DefaultValue = "10", Type = "LengthUnit")]
        Decameter = 1,
        [Unit(Description = "Metre", Symbol = "m", DefaultValue = "1")]
        Meter = 0,
        [Unit(Description = "Desimetre", Symbol = "dm", DefaultValue = "-10", Type = "LengthUnit")]
        Decimeter = -1,
        [Unit(Description = "Santimetre", Symbol = "cm", DefaultValue = "-100", Type = "LengthUnit")]
        Centimeter = -2,
        [Unit(Description = "Milimetre", Symbol = "mm", DefaultValue = "-1000", Type = "LengthUnit")]
        Millimeter = -3,
        [Unit(Description = "Mikrometre", Symbol = "μm", DefaultValue = "-1000000", Type = "LengthUnit")]
        Micrometer = -4,
        [Unit(Description = "Nanometre", Symbol = "nm", DefaultValue = "-1000000000", Type = "LengthUnit")]
        Nanometer = -5,
        [Unit(Description = "Angstrom", Symbol = "A o", DefaultValue = "-10000000", Type = "LengthUnit")]
        Angstrom = -6,
        [Unit(Description = "Mikromikron", Symbol = "μμ", DefaultValue = "-100000000", Type = "LengthUnit")]
        Micromicron = -7,
    }
}
