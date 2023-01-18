using FxCalc;
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
            SimpleCalculation<decimal> calculation = new SimpleCalculation<decimal>();
            calculation.Formule = "a*((b*c)/a)*d+(c-d*(c,a))";
            calculation.SetVarible("a", "43");
            calculation.SetVarible("b", "200");
            calculation.SetVarible("c", "-15");
            calculation.SetVarible("d", "0,054");
            calculation.Compute();
            
        }
    }
}