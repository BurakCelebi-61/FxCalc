using FxCalc;
using FxCalc.Enums.Units;

namespace FxCalcTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void EngineComputeTest()
        {
            FxCalc.Engine<decimal> eng = new Engine<decimal>();
            //5*((-56*4)/-4)*25+(25-7*(21))
            eng.Formule = "5,84*((-56*0,25)/-4)*25+(25-7*(21,54))";
            var spros = eng.Compute(eng.Formule);
        }
        [Test]
        public void SimpleCalculationComputeTest()
        {
            //a*((b*c)/a)*d+(c-d*(c,a))
            SimpleCalculation<decimal> calculation = new SimpleCalculation<decimal>(FxCalc.Enums.InputType.Double);
            calculation.Formule = "a*((b*c)/a)*d+(c-d*(c,a))";
            calculation.SetVarible("a", "43");
            calculation.SetVarible("b", "200");
            calculation.SetVarible("c", "-15");
            calculation.SetVarible("d", "0,054");
            calculation.Compute();

        }

        [Test]
        public void UnitaryCalculationComputeTest()
        {
            UnitaryCalculation<decimal> calculation = new UnitaryCalculation<decimal>();
            calculation.AreaUnit = FxCalc.Enums.Units.AreaUnit.SquareDecameter;
            calculation.CurrencyUnit = FxCalc.Enums.Units.CurrencyUnit.USD;
            calculation.LengthUnit = FxCalc.Enums.Units.LengthUnit.Meter;
            calculation.PieceUnit = FxCalc.Enums.Units.PieceUnit.Piece;
            calculation.VolumeUnit = FxCalc.Enums.Units.VolumeUnit.CubicMeter;
            calculation.WeightUnit = FxCalc.Enums.Units.WeightUnit.Kilogram;
            calculation.SetCurrency(18.41m, 20.4m);
            calculation.Formule = "[Pres]+[PVCUnitPrice]+[Color]";
            calculation.SetVarible(
                new UnitaryVarible
                {
                    Name = "[Pres]",
                    Value = "5,23",
                    Units = new List<Enum> { { CurrencyUnit.USD }, { WeightUnit.Kilogram } }
                });
            calculation.SetVarible(
                new UnitaryVarible
                {
                    Name = "[PVCUnitPrice]",
                    Value = "0,06",
                    Units = new List<Enum> { { CurrencyUnit.EUR }, { LengthUnit.Meter } }
                });
            calculation.SetVarible(
                new UnitaryVarible
                {
                    Name = "[Color]",
                    Value = "18",
                    Units = new List<Enum> { { WeightUnit.Kilogram }, { CurrencyUnit.TRY } }
                });
            calculation.Compute();

        }

        [Test]
        public void ConditionalCalculationTest()
        {
            ConditionalCalculation<decimal> calculation = new ConditionalCalculation<decimal>();
            calculation.AreaUnit = FxCalc.Enums.Units.AreaUnit.SquareDecameter;
            calculation.CurrencyUnit = FxCalc.Enums.Units.CurrencyUnit.USD;
            calculation.LengthUnit = FxCalc.Enums.Units.LengthUnit.Meter;
            calculation.PieceUnit = FxCalc.Enums.Units.PieceUnit.Piece;
            calculation.VolumeUnit = FxCalc.Enums.Units.VolumeUnit.CubicMeter;
            calculation.WeightUnit = FxCalc.Enums.Units.WeightUnit.Kilogram;
            calculation.SetCurrency(18.41m, 20.4m);
            calculation.Formules.Add(new ConditionalFormula
            {
                Conditional = "[ProfileBarrierType]==PVC || [ProfileBarrierType]==POLYAMIDE",
                Formula = "[Pres]+[PVCUnitPrice]+[Color]"
            });
            calculation.Formules.Add(new ConditionalFormula
            {
                Conditional = "[ProfileBarrierType]==POLYAMIDE",
                Formula = "[Pres]+[PVCUnitPrice]+[Color]"
            });
            calculation.SetVarible(
                new UnitaryVarible
                {
                    Name = "[Pres]",
                    Value = "5,23",
                    Units = new List<Enum> { { CurrencyUnit.USD }, { WeightUnit.Kilogram } }
                });
            calculation.SetVarible(
                new UnitaryVarible
                {
                    Name = "[PVCUnitPrice]",
                    Value = "0,06",
                    Units = new List<Enum> { { CurrencyUnit.EUR }, { LengthUnit.Meter } }
                });
            calculation.SetVarible(
                new UnitaryVarible
                {
                    Name = "[Color]",
                    Value = "18",
                    Units = new List<Enum> { { WeightUnit.Kilogram }, { CurrencyUnit.TRY } }
                });
            calculation.SetVarible(
                new UnitaryVarible
                {
                    Name = "[ProfileBarrierType]",
                    Value = "PVC"
                });
            calculation.Compute();

        }
    }
}