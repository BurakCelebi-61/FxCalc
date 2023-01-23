using FxCalc.CustomAttribute;

namespace FxCalc.Enums.Units
{
    [Unit(Description = "Kilogram", Symbol = "kg", DefaultValue = "1", Type = "WeightUnit")]
    public enum WeightUnit
    {
        [Unit(Description = "Ton", Symbol = "t", DefaultValue = "1000", Type = "WeightUnit")]
        Ton,
        [Unit(Description = "Kental", Symbol = "q", DefaultValue = "100", Type = "WeightUnit")]
        Quintal,
        [Unit(Description = "Kilogram", Symbol = "kg", DefaultValue = "1", Type = "WeightUnit")]
        Kilogram,
        [Unit(Description = "Hektogram", Symbol = "hg", DefaultValue = "-10", Type = "WeightUnit")]
        Hectogram,
        [Unit(Description = "Dekagram", Symbol = "dak", DefaultValue = "-100", Type = "WeightUnit")]
        Decagram,
        [Unit(Description = "Gram", Symbol = "g", DefaultValue = "-1000", Type = "WeightUnit")]
        Gram,
        [Unit(Description = "Desigram", Symbol = "dg", DefaultValue = "-10000", Type = "WeightUnit")]
        Decigram,
        [Unit(Description = "Santigram", Symbol = "cg", DefaultValue = "-100000", Type = "WeightUnit")]
        Centigram,
        [Unit(Description = "Miligram", Symbol = "mg", DefaultValue = "-1000000", Type = "WeightUnit")]
        Milligram,
        [Unit(Description = "Mikrogram", Symbol = "μg", DefaultValue = "-10000000", Type = "WeightUnit")]
        microgram
    }
}
