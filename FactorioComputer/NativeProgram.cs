using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioComputer
{
    class NativeProgram
    {
        private List<NativeInstruction> lines = new List<NativeInstruction>();

        public int LineCount()
        {
            return lines.Count;
        }

        public NativeInstruction this[int key]
        {
            get
            {
                while (key >= lines.Count)
                {
                    lines.Add(new NativeInstruction(lines.Count + 1));
                }
                return lines[key];
            }
            private set
            {
                lines[key] = value;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var instr in lines)
            {
                sb.Append(instr).AppendLine();
            }
            return sb.ToString();
        }
    }
}
