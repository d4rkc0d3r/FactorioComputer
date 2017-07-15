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

        private ASMInstructionTemplate()
        {

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
    }
}
