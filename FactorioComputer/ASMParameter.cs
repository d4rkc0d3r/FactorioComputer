using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioComputer
{
    class ASMParameter
    {
        public int Value;
        public int Offset;
        public bool IsConstant;
        public bool IsRegister;
        public bool IsPointer;
        public bool AddWrittenValue;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (IsRegister)
                sb.Append('@');
            else if(IsPointer)
                sb.Append('*');
            sb.Append(Value);
            if (Offset != 0)
            {
                if (Offset > 0)
                    sb.Append('+');
                sb.Append(Offset);
            }
            return sb.ToString();
        }

        public static ASMParameter Parse(string str)
        {
            ASMParameter result = new ASMParameter();
            switch(str[0])
            {
                case '*':
                    result.IsPointer = true;
                    str = str.Substring(1);
                    int offsetIndex = str.IndexOf('-');
                    if (offsetIndex == -1)
                        offsetIndex = str.IndexOf('+');
                    if (offsetIndex == -1)
                        result.Value = int.Parse(str);
                    else
                    {
                        result.Value = int.Parse(str.Substring(0, offsetIndex));
                        result.Offset = int.Parse(str.Substring(offsetIndex));
                    }
                    break;
                case '@':
                    result.IsRegister = true;
                    result.Value = int.Parse(str.Substring(1));
                    break;
                default:
                    result.IsConstant = true;
                    result.Value = int.Parse(str);
                    break;
            }
            return result;
        }
    }
}
