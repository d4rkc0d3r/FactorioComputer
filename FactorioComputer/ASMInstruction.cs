using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioComputer
{
    class ASMInstruction
    {
        public ASMParameter[] Parameter;
        public string Token;
        public bool DoesAdd;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (DoesAdd)
                sb.Append('+');
            sb.Append(Token);
            foreach (ASMParameter p in Parameter)
                sb.Append(' ').Append(p);
            return sb.ToString();
        }

        public static ASMInstruction Parse(string str)
        {
            if (string.IsNullOrEmpty(str))
                return null;
            ASMInstruction instruction = new ASMInstruction();
            string[] split = str.Split(' ');
            if (split[0][0] == '+')
            {
                instruction.DoesAdd = true;
                instruction.Token = split[0].Substring(1);
            }
            else
                instruction.Token = split[0];
            instruction.Parameter = new ASMParameter[split.Length - 1];
            for (int i = 0; i < instruction.Parameter.Length; ++i)
                instruction.Parameter[i] = ASMParameter.Parse(split[i + 1]);
            return instruction;
        }
    }
}
