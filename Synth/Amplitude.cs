using System;

namespace Synth
{
    public class Amplitude
    {
        public static double Normalize(byte value) => (double)value / Byte.MaxValue;
    }
}
