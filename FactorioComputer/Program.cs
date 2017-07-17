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
        public static int ParseAdditionAndSubstraction(string str)
        {
            int value = 0;
            int startIndex = 0;
            int nextIndex = 0;
            while (true)
            {
                nextIndex = str.IndexOf('-', startIndex + 1);
                if (nextIndex == -1)
                {
                    nextIndex = str.IndexOf('+', startIndex + 1);
                }
                if (nextIndex == -1)
                {
                    value += int.Parse(str.Substring(startIndex));
                    break;
                }
                else
                {
                    value += int.Parse(str.Substring(startIndex, nextIndex - startIndex));
                    startIndex = nextIndex;
                }
            }
            return value;
        }

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
            string source = "../../../prime_hand_optimized.asm";
            var compiler = new Compiler();
            compiler.LoadInstructionMap(ReadFrom("asm_to_signals.txt"));
            foreach (string line in compiler.ResolveLabels(ReadFrom(source)))
            {
                Console.WriteLine(line);
            }
            Console.ReadKey();
        }
    }
}
