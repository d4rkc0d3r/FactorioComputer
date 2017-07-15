using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioComputer
{
    class NativeInstruction
    {
        private int lineNumber;
        private Dictionary<string, int> signals = new Dictionary<string, int>();

        public NativeInstruction(int lineNumber)
        {
            this.lineNumber = lineNumber;
        }

        public string ToString(bool printLineNumber = false)
        {
            StringBuilder sb = new StringBuilder();
            if (printLineNumber)
                sb.Append(lineNumber).Append(':');
            foreach(var entry in signals)
                sb.Append(entry.Key).Append('=').Append(entry.Value).Append(',');
            sb.Remove(sb.Length - 1, 1); // remove last ','
            return sb.ToString();
        }
    }
}
