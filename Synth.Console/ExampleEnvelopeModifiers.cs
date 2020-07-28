using System;
using E = System.Func<double, byte, byte>;

namespace Synth.Console
{
    public class ExampleEnvelopeModifiers
    {
        public static E Pulse(E envelope, byte rate)
            => (t, v)
                => envelope(t, Convert.ToByte(128 + Math.Sin(rate * t) / 2 * v));
    }
}
