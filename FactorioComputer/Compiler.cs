using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioComputer
{
    class Compiler
    {
        private List<ASMInstructionTemplate> instructionMap = new List<ASMInstructionTemplate>();

        public void LoadInstructionMap(IEnumerator<string> lines)
        {
            if (!lines.MoveNext())
                return; // empty file?
            string instructionString;
            while (!string.IsNullOrEmpty(instructionString = lines.Current))
            {
                if (!lines.MoveNext())
                    return; // should probably throw an exception
                var instruction = ASMInstructionTemplate.Parse(instructionString);
                instructionMap.Add(instruction);
                while (lines.Current.StartsWith("  "))
                {
                    instruction.AddNativeInstructionTemplate(lines.Current.Substring(2));
                    if (!lines.MoveNext())
                        return; // no newline on EOF :(
                }
            }
        }
    }
}
