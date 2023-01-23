using System.Diagnostics;

namespace FxCalc
{
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public class UnitaryVarible : SimpleVarible
    {
        public List<Enum> Units { get; set; } = new List<Enum>();

        private string GetDebuggerDisplay()
        {
            return $"{this.Name} : {this.Value}";
        }
    }
}
