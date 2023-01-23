using FxCalc.CustomAttribute;

namespace FxCalc.Enums.Units
{
    [Unit(Description = "Türk Lirası", Symbol = "₺", DefaultValue = "1", Type = "CurrencyUnit")]
    public enum CurrencyUnit
    {
        [Unit(Description = "Türk Lirası", Symbol = "₺", DefaultValue = "1", Type = "CurrencyUnit")]
        TRY = 0,
        [Unit(Description = "Amerikan Doları", Symbol = "$", DefaultValue = "1", Type = "CurrencyUnit")]
        USD = 1,
        [Unit(Description = "Euro", Symbol = "€", DefaultValue = "1", Type = "CurrencyUnit")]
        EUR = 2,

    }
}
