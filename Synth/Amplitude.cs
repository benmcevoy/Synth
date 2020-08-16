using System;

namespace Synth
{
    public class Amplitude
    {
        //private static double Clip(double value) => value > 1 ? 1 : value < -1 ? -1 : value;
        //private static double Scale(double value) => (value - 128) / 128;

        private static double Scale1(double value, int ordinal) => Math.Log(value, 2) / (8 + Math.Log(ordinal, 2));

        // TODO: when adding amplitudes we have to convert to signed values, scale to -1:1, then add, constrain to -1:1
        // then offset back to unsigned 0:255
        // better to give up on 8 bit and switch to 16 bit signed
        // the extreme is 255+255, what if if we do some kind of Pow funciton to logarthimic scale
        // the 512 back to 255 (or 1?)?? Just thinking here.  Constrain is blunt, it clips.
        // especially for the filter which adds 5 or 6 values

        public static double Add(double v1, double v2) => Scale1(v1 + v2, 2);
        public static double Add(double v1, double v2, double v3) => Scale1(v1 + v2 + v3, 3);
        public static double Add(double v1, double v2, double v3, double v4) => Scale1(v1 + v2 + v3 + v4, 4);
        public static double Add(double v1, double v2, double v3, double v4, double v5) => Scale1(v1 + v2 + v3 + v4 + v5, 5);
        public static double Add(double v1, double v2, double v3, double v4, double v5, double v6) => Scale1(v1 + v2 + v3 + v4 + v5 + v6, 6);

        public static double Normalize(byte value) => (double)value / Byte.MaxValue;
        public static double Normalize(double value) => value / Byte.MaxValue;
        public static double ToDecibel(double value) => 20D * Math.Log10(value);
        public static double FromDecibel(double value) => Math.Pow(10, value / 20d);
        public static byte Constrain(byte value) => value > Byte.MaxValue ? Byte.MaxValue : value;
        public static double Constrain(double value) => value > Byte.MaxValue ? Byte.MaxValue : value < Byte.MinValue ? byte.MinValue : value;
    }
}
