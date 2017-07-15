using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioComputer
{
    class ASMParameterTemplate
    {
        public bool AllowConstant { get; private set; }
        public bool AllowRegister { get; private set; }
        public bool AllowPointer { get; private set; }
        public string Token { get; private set; }

        private ASMParameterTemplate()
        {

        }

        public bool Accepts(ASMParameter param)
        {
            return (AllowConstant && param.IsConstant) || (AllowRegister && param.IsRegister) || (AllowPointer && param.IsPointer);
        }

        public static ASMParameterTemplate Parse(string str)
        {
            var ret = new ASMParameterTemplate();
            for(int i = 0; i < str.Length; i++)
            {
                switch(str[i])
                {
                    case '*':
                        ret.AllowPointer = true;
                        break;
                    case '@':
                        ret.AllowRegister = true;
                        break;
                    case '$':
                        ret.AllowConstant = true;
                        break;
                    default:
                        ret.Token = str.Substring(i);
                        i = str.Length;
                        break;
                }
            }
            return ret;
        }
    }
}
