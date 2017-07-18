using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioComputer
{
    class ASMInstructionTemplate
    {
        private ASMParameterTemplate[] parameter;
        public string Token { get; private set; }
        public int Delay { get; private set; }
        public bool IsGeneric { get; private set; }
        private List<Dictionary<string, string>> nativeInstructions = new List<Dictionary<string,string>>();

        private ASMInstructionTemplate()
        {

        }

        public void AddNativeInstructionTemplate(string str)
        {
            nativeInstructions.Add(str.Split(',').Select(s => s.Split('=')).ToDictionary(s => s[0], s => s[1]));
        }

        public bool Accepts(ASMInstruction instr)
        {
            if (parameter.Length != instr.Parameter.Length || !Token.Equals(instr.Token))
                return false;
            for(int i = 0; i < parameter.Length; i++)
            {
                if (!parameter[i].Accepts(instr.Parameter[i]))
                    return false;
            }
            return true;
        }

        public Dictionary<string, int>[] Compile(ASMInstruction instruction, int lineNumber)
        {
            Dictionary<string, int>[] result = new Dictionary<string, int>[nativeInstructions.Count];
            for (int i = 0; i < result.Length; ++i)
            {
                result[i] = new Dictionary<string, int>();
                foreach (var entry in nativeInstructions[i])
                {
                    string value = entry.Value.Replace("__LINE__", lineNumber.ToString());
                    for (int j = 0; j < parameter.Length; ++j)
                    {
                        var p = instruction.Parameter[j];
                        if (p.IsRegister)
                        {
                            value = value.Replace(parameter[j].Token + ".a", p.Value.ToString());
                            value = value.Replace(parameter[j].Token + ".c", p.Offset.ToString());
                            value = value.Replace(parameter[j].Token + ".p", "0");
                        }
                        else if (p.IsConstant)
                        {
                            value = value.Replace(parameter[j].Token + ".a", "0");
                            value = value.Replace(parameter[j].Token + ".c", p.Value.ToString());
                            value = value.Replace(parameter[j].Token + ".p", "0");
                        }
                        else // if (p.IsPointer)
                        {
                            value = value.Replace(parameter[j].Token + ".p", p.Value.ToString());
                            value = value.Replace(parameter[j].Token + ".a", p.Offset.ToString());
                            value = value.Replace(parameter[j].Token + ".c", "0");
                        }
                    }
                    for (int j = 0; j < parameter.Length; ++j)
                    {
                        value = value.Replace(parameter[j].Token, instruction.Parameter[j].Value.ToString());
                    }
                    result[i].Add(entry.Key, Program.ParseAdditionAndSubstraction(value));
                }
            }
            return result;
        }

        public static ASMInstructionTemplate Parse(string str)
        {
            var ret = new ASMInstructionTemplate();
            string[] split = str.Split(' ');
            ret.Token = split[0];
            int i = 0;
            var paramList = new List<ASMParameterTemplate>();
            while (split[++i][0] != '(')
            {
                paramList.Add(ASMParameterTemplate.Parse(split[i]));
            }
            ret.Delay = split[i][1] - '0';
            ret.IsGeneric = split[split.Length - 1].Equals("generic");
            ret.parameter = paramList.ToArray();
            return ret;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Token).Append(' ');
            foreach (var param in parameter)
            {
                sb.Append(param).Append(' ');
            }
            sb.Append('(').Append(Delay).Append(')');
            if (IsGeneric) sb.Append(" generic");
            sb.AppendLine();
            foreach (var map in nativeInstructions)
            {
                sb.Append("  ").AppendLine(map.Select(e => e.Key + '=' + e.Value).Aggregate((a, b) => a + ',' + b));
            }
            return sb.ToString();
        }
    }
}
