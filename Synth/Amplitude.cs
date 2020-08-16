using System;

namespace Synth
{
    public class Amplitude
    {
        private static short Scale(double value, int ordinal) => (short)(short.MaxValue * (Math.Log(value, 2) / (8 + Math.Log(ordinal, 2))));

        public static short Add(double v1, double v2) => Scale(v1 + v2, 2);
        public static short Add(double v1, double v2, double v3) => Scale(v1 + v2 + v3, 3);
        public static short Add(double v1, double v2, double v3, double v4) => Scale(v1 + v2 + v3 + v4, 4);
        public static short Add(double v1, double v2, double v3, double v4, double v5) => Scale(v1 + v2 + v3 + v4 + v5, 5);
        public static short Add(double v1, double v2, double v3, double v4, double v5, double v6) => Scale(v1 + v2 + v3 + v4 + v5 + v6, 6);
        public static short ToDecibel(double value) => (short)(20 * Math.Log10(value));
        public static short FromDecibel(double value) => (short)Math.Pow(10, value / 20);

        /// <summary>
        /// Normalize a short to the range -1:1
        /// </summary>
        public static double Normalize(short value) => value / short.MaxValue;
    }
}
