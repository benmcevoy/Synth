using System;

namespace Synth
{
    public class Amplitude
    {
        public static double Normalize(byte value) => (double)value / Byte.MaxValue;

        public static double Normalize(double value) => value / Byte.MaxValue;
    }
}
