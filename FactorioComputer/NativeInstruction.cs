using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioComputer
{
    class NativeInstruction
    {
        private int lineNumber;
        private Dictionary<string, int> signals = new Dictionary<string, int>();
        private Dictionary<string, string> wardenSignals = new Dictionary<string, string>
        {
            { "X", "yellow" },
            { "Y", "pink" },
            { "Z", "cyan" },
            { "cannon-shell", "explosive-cannon-shell" },
            { "uranium-cannon-shell", "explosive-uranium-cannon-shell" },
            { "rocket", "explosive-rocket" },
            { "firearm-magazine", "piercing-rounds-magazine" }
        };

        public NativeInstruction(int lineNumber)
        {
            this.lineNumber = lineNumber;
        }

        public void Add(string signal, int value)
        {
            if (value == 0) // a 0 signal in Factorio is equal to no signal
                return;
            signals.Add(signal, value);
            string warden;
            if (wardenSignals.TryGetValue(signal, out warden))
                signals.Add(warden, -1);
        }

        public bool Remove(string signal)
        {
            return signals.Remove(signal);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach(var entry in signals)
                sb.Append(entry.Key).Append('=').Append(entry.Value).Append(',');
            if(signals.Count > 0)
                sb.Remove(sb.Length - 1, 1); // remove last ','
            return sb.ToString();
        }
    }
}
