using FxCalc.CustomAttribute;
using FxCalc.Enums.Units;
using System.Collections.Generic;

namespace FxCalc
{
    public class UnitaryCalculation<T> : SimpleCalculation<T> where T : struct
    {
        public UnitaryCalculation()
        {
            SetVarible(new UnitaryVarible { Name = "[EUR]", Value = "1" });
            SetVarible(new UnitaryVarible { Name = "[USD]", Value = "1" });
        }
        #region Units
        public AreaUnit AreaUnit { get; set; }
        public CurrencyUnit CurrencyUnit { get; set; }
        public LengthUnit LengthUnit { get; set; }
        public PieceUnit PieceUnit { get; set; }
        public VolumeUnit VolumeUnit { get; set; }
        public WeightUnit WeightUnit { get; set; }
        #endregion
        #region Private Property
        private string usd = "1";
        private string eur = "1";
        #endregion
        public List<UnitaryVarible> EditedVaribles { get; set; }
        public override T Compute(string? newFormule = null)
        {
            if (newFormule != null)
            {
                Formule = newFormule;
            }

            EditedVaribles = new List<UnitaryVarible>();
            
            foreach (var v in Varibles)
            {
                var varib = v as UnitaryVarible;
                var units = varib.Units;
                foreach (var u in units)
                {
                    varib = UnitConvert(varib, u);
                }
                EditedVaribles.Add(varib);
            }
            return Compute(EditedVaribles);
        }
        public T Compute(List<UnitaryVarible> setVaribles)
        {
            List<ISimpleVarible> list = new List<ISimpleVarible>();
            list.AddRange(setVaribles);
            return base.Compute(list);
        }
        public UnitaryVarible UnitConvert(UnitaryVarible v, Enum unit)
        {
            if (unit.GetType() == typeof(CurrencyUnit))
            {
                return CurrencyUnitConvert(v, unit);
            }
            UnitData varibUnit = UnitAttribute.GetUnitAttribute(unit).AsUnitData();
            UnitData ud = UnitAttribute.GetUnitAttribute(PieceUnit).AsUnitData();
            if (unit.GetType() == typeof(AreaUnit))
            {
                ud = UnitAttribute.GetUnitAttribute(AreaUnit).AsUnitData();
            }
            else if (unit.GetType() == typeof(LengthUnit))
            {
                ud = UnitAttribute.GetUnitAttribute(LengthUnit).AsUnitData();
            }
            else if (unit.GetType() == typeof(VolumeUnit))
            {
                ud = UnitAttribute.GetUnitAttribute(VolumeUnit).AsUnitData();
            }
            else if (unit.GetType() == typeof(WeightUnit))
            {
                ud = UnitAttribute.GetUnitAttribute(WeightUnit).AsUnitData();
            }

            if (this.InputType == Enums.InputType.Double)
            {
                double c = Math.Abs(ud.DefaultValue.ToDouble());
                double cv = Math.Abs(ud.DefaultValue.ToDouble());
                double v1 = 0;
                double v2 = 0;

                if (varibUnit.DefaultValue.ToDouble() > 0)
                {
                    v1 = v.Value.ToDouble() * cv;
                }
                else
                {
                    v1 = v.Value.ToDouble() / cv;
                }

                if (ud.DefaultValue.ToDouble() > 0)
                {
                    v2 = v1 / c;
                }
                else
                {
                    v2 = v1 * c;
                }



                return new UnitaryVarible
                {
                    Name = v.Name,
                    Units = v.Units,
                    Value = v2.ToString()
                };
            }
            else
            {
                decimal c = Math.Abs(ud.DefaultValue.ToDecimal());
                decimal cv = Math.Abs(varibUnit.DefaultValue.ToDecimal());
                decimal v1 = 0;
                decimal v2 = 0;
                if (varibUnit.DefaultValue.ToDecimal() > 0)
                {
                    v1 = v.Value.ToDecimal() * cv;
                }
                else
                {
                    v1 = v.Value.ToDecimal() / cv;
                }

                if (ud.DefaultValue.ToDecimal() > 0)
                {
                    v2 = v1 / c;
                }
                else
                {
                    v2 = v1 * c;
                }


                return new UnitaryVarible
                {
                    Name = v.Name,
                    Units = v.Units,
                    Value = v2.ToString()
                };
            }
        }
        public UnitaryVarible CurrencyUnitConvert(UnitaryVarible v, Enum unit)
        {
            decimal decv1 = 0;
            decimal decv2 = 0;
            double dobv1 = 0;
            double dobv2 = 0;
            UnitData ud = UnitAttribute.GetUnitAttribute(unit).AsUnitData();
            if (ud.Symbol == "$")
            {
                ud.DefaultValue = usd;
            }
            if (ud.Symbol == "€")
            {
                ud.DefaultValue = eur;
            }

            if (InputType == Enums.InputType.Double)
            {
                dobv1 = v.Value.ToDouble() * ud.DefaultValue.ToDouble();

                if (CurrencyUnit == CurrencyUnit.USD)
                {
                    dobv2 = dobv1 / usd.ToDouble();
                }
                else if (CurrencyUnit == CurrencyUnit.EUR)
                {
                    dobv2 = dobv1 / eur.ToDouble();
                }
                else
                {
                    dobv2 = dobv1;
                }
                return new UnitaryVarible
                {
                    Name = v.Name,
                    Units = v.Units,
                    Value = dobv2.ToString()
                };
            }else
            {
                {
                decv1 = v.Value.ToDecimal() * ud.DefaultValue.ToDecimal();

                if (CurrencyUnit == CurrencyUnit.USD)
                {
                    decv2 = decv1 / usd.ToDecimal();
                }
                else if (CurrencyUnit == CurrencyUnit.EUR)
                {
                    decv2 = decv1 / eur.ToDecimal();
                }
                else
                {
                    decv2 = decv1;
                }
                return new UnitaryVarible
                {
                    Name = v.Name,
                    Units = v.Units,
                    Value = decv2.ToString()
                };
            }
            }
        }
        public void SetCurrency(decimal USD,decimal EUR)
        {
            SetVarible(new UnitaryVarible { Name = "[EUR]",Value = EUR.ToString() });
            SetVarible(new UnitaryVarible { Name = "[USD]", Value = USD.ToString() });
        }
        public void SetCurrency(double USD, double EUR)
        {
            SetVarible(new UnitaryVarible { Name = "[EUR]", Value = EUR.ToString() });
            SetVarible(new UnitaryVarible { Name = "[USD]", Value = USD.ToString() });
        }
        public override void SetVarible(ISimpleVarible varible)
        {
            if (varible.Name == "[EUR]")
            {
                eur = varible.Value.ToString();
            }
            if (varible.Name == "[USD]")
            {
                usd = varible.Value.ToString();
            }
            base.SetVarible(varible);
        }
        public override void SetVarible(List<ISimpleVarible> varibles)
        {
            foreach (var v in varibles)
            {
                if (v.Name == "[EUR]")
                {
                    eur = v.Value.ToString();
                }
                if (v.Name == "[USD]")
                {
                    usd = v.Value.ToString();
                }
            }
            base.SetVarible(varibles);
        }

        public override void SetVarible(string name, string value)
        {
            if (name == "[EUR]")
            {
                eur = value.ToString();
            }
            if (name == "[USD]")
            {
                usd = value.ToString();
            }
            base.SetVarible(name, value);
        }
    }
}
