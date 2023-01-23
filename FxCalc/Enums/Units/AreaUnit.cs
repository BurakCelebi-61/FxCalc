using FxCalc.CustomAttribute;

namespace FxCalc.Enums.Units
{
    [Unit(Description = "Metre kare", Symbol = "m²", DefaultValue = "1", Type = "AreaUnit")]
    public enum AreaUnit
    {
        [Unit(Description = "Kilometre kare", Symbol = "km²", DefaultValue = "1000000", Type = "AreaUnit")]
        SquareKilometer = 3,
        [Unit(Description = "Hektometre kare", Symbol = "hm²", DefaultValue = "10000", Type = "AreaUnit")]
        SquareHectometer = 2,
        [Unit(Description = "Dekametre kare", Symbol = "dam²", DefaultValue = "100", Type = "AreaUnit")]
        SquareDecameter = 1,
        [Unit(Description = "Metre kare", Symbol = "m²", DefaultValue = "1", Type = "AreaUnit")]
        SquareMeter = 0,
        [Unit(Description = "Desimetre kare", Symbol = "dm²", DefaultValue = "-100", Type = "AreaUnit")]
        SquareDecimeter = -1,
        [Unit(Description = "Santimetre kare", Symbol = "cm2²", DefaultValue = "-10000", Type = "AreaUnit")]
        SquareCentimeter = -2,
        [Unit(Description = "Milimetre kare", Symbol = "mm²", DefaultValue = "-1000000", Type = "AreaUnit")]
        SquareMillimeter = -3
    }

}
