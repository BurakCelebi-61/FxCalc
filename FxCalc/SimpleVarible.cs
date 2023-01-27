using System.Diagnostics;

namespace FxCalc
{
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public class SimpleVarible : ISimpleVarible
    {
        public string Name { get; set; }
        public string Value { get; set; }

        private string GetDebuggerDisplay()
        {
            return $"{Name} : {Value}";
        }
    }
}
