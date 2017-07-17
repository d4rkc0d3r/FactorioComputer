﻿using System;
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

        public Dictionary<string, int>[] Compile(ASMInstruction instruction)
        {
            Dictionary<string, int>[] result = new Dictionary<string, int>[nativeInstructions.Count];
            // TODO: implement actual compilation
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
