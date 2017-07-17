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

        public void LoadInstructionMap(IEnumerable<string> s)
        {
            IEnumerator<string> lines = s.GetEnumerator();
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

        public IEnumerable<string> ResolveLabels(IEnumerable<string> data)
        {
            int lineNumber = 1;
            List<string> dataList = new List<string>();
            Dictionary<string, string> labels = new Dictionary<string, string>();
            foreach (string line in data)
            {
                string[] split = line.Split(':');
                if (split.Length == 1)
                {
                    dataList.Add(split[0]);
                }
                else
                {
                    dataList.Add(split[1]);
                    labels.Add(split[0], lineNumber.ToString());
                }
                lineNumber++;
            }
            foreach (string line in dataList)
            {
                string output = line;
                foreach (var entry in labels)
                {
                    output = output.Replace(entry.Key, entry.Value);
                }
                yield return output;
            }
        }
    }
}
