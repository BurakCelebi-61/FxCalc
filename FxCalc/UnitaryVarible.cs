using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FxCalc
{
    public class UnitaryVarible : SimpleVarible
    {
        public List<Enum> Units { get; set; } = new List<Enum>();

        private string GetDebuggerDisplay()
        {
            return $"{this.Name} : {this.Value}";
        }
    }
}
