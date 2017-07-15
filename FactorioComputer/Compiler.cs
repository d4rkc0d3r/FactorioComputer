using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioComputer
{
    class Compiler
    {
        public void LoadInstructionMap(IEnumerator<string> lines)
        {
            string instruction;
            while(!(instruction = lines.Current.Trim()).Equals(""))
            {
                if (!lines.MoveNext())
                    return; // should probably throw an exception
                string signalString = lines.Current.Trim();
            }
        }
    }
}
