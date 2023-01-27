using FxCalc.Enums.Units;
using System;
using System.Linq;
using System.Reflection;

namespace FxCalc.CustomAttribute
{
    public class UnitAttribute : Attribute
    {
        public string Description { get; set; }
        public string DefaultValue { get; set; }
        public string Symbol { get; set; }
        public string Type { get; set; }
        public static UnitAttribute GetUnitAttribute(Enum enumValue)
        {
            // Get instance of the attribute.
            UnitAttribute r = enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<UnitAttribute>();
            if (enumValue.GetType() == typeof(CurrencyUnit))
            {
                if ((CurrencyUnit)enumValue == CurrencyUnit.USD)
                {
                    r.DefaultValue = Utils.USD.ToString();
                }
                if ((CurrencyUnit)enumValue == CurrencyUnit.EUR)
                {
                    r.DefaultValue = Utils.EUR.ToString();
                }
            }
            if (r == null)
            {
                return null;
            }
            else
            {
                return r;
            }

        }


        public UnitData AsUnitData()
        {
            return new UnitData
            {
                Description = this.Description,
                Symbol = this.Symbol,
                DefaultValue = this.DefaultValue,
                Type = this.Type
            };
        }
    }
}
