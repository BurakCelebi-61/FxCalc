using FxCalc.Enums.Units;
using System.Diagnostics;

namespace FxCalc.CustomAttribute
{
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public class UnitData
    {
        public string Description { get; set; }
        public string DefaultValue { get; set; }
        public string Symbol { get; set; }
        public string Type { get; set; }
        public UnitAttribute SetUnitAttribute()
        {
            return new UnitAttribute()
            {
                DefaultValue = DefaultValue.ToString(),
                Symbol = Symbol,
                Description = Description
            };
        }

        public override string ToString()
        {
            return $"{Description} {Symbol}";
        }

        private string GetDebuggerDisplay()
        {
            return $"{Description} : {DefaultValue} {Symbol}";
        }
        public Enum GetEnum()
        {
            switch (Type)
            {
                case "AreaUnit":
                    return Utils.NameToEnum<AreaUnit>(Description);
                case "CurrencyUnit":
                    return Utils.NameToEnum<CurrencyUnit>(Description);
                case "LengthUnit":
                    return Utils.NameToEnum<LengthUnit>(Description);
                case "PieceUnit":
                    return PieceUnit.Piece;
                case "VolumeUnit":
                    return Utils.NameToEnum<VolumeUnit>(Description);
                case "WeightUnit":
                    return Utils.NameToEnum<WeightUnit>(Description);
                default:
                    return PieceUnit.Piece;
                    break;
            }

        }
    }
}
