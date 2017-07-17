using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioComputer
{
    class Program
    {
        static IEnumerable<string> ReadFrom(string file)
        {
            string line;
            using (var reader = File.OpenText(file))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }

        static void Main(string[] args)
        {
            var compiler = new Compiler();
            compiler.LoadInstructionMap(ReadFrom("asm_to_signals.txt").GetEnumerator());
        }
    }
}
