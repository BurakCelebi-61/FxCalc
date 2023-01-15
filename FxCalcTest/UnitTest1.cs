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
        public void Test1()
        {
            FxCalc.Engine eng = new Engine();
            var pros = new ProcessFile();
            eng.Formule = "5*((56*4)/4)*25+(25-7*(21))";
            pros = eng.ProcessFiling(eng.Formule,FxCalc.Enums.ProcessInputType.RootFormul, 0);
        }
    }
}