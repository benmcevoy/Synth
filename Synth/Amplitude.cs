using System;

namespace Synth
{
    public class Amplitude
    {
        public static double Normalize(byte value) => (double)value / Byte.MaxValue;
        public static double Normalize(double value) => value / Byte.MaxValue;
        public static double ToDecibel(double value) => 20D * Math.Log10(value);
        public static double FromDecibel(double value) => Math.Pow(10, value / 20d);
        public static byte Constrain(byte value) => value > Byte.MaxValue ? Byte.MaxValue : value;
        public static double Constrain(double value) => value > Byte.MaxValue ? Byte.MaxValue : value < Byte.MinValue ? byte.MinValue : value;
    }
}
