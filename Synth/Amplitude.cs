using System;

namespace Synth
{
    public struct Amplitude
    {
        public const short MinValue = short.MinValue;
        public const short MaxValue = short.MaxValue;
        public readonly short Value;

        public Amplitude(short a) => Value = a;
        public static implicit operator short(Amplitude a) => a.Value;
        public static implicit operator Amplitude(short a) => new Amplitude(a);
        public static Amplitude Scale(double value) => (Amplitude)(MaxValue * value);
        private static Amplitude Scale(double value, int ordinal) => (Amplitude)(MaxValue * (Math.Log(value, 2) / (8 + Math.Log(ordinal, 2))));
        public static Amplitude Add(Amplitude v1, Amplitude v2) => Scale(v1 + v2, 2);
        public static Amplitude Add(Amplitude v1, Amplitude v2, Amplitude v3) => Scale(v1 + v2 + v3, 3);
        public static Amplitude Add(Amplitude v1, Amplitude v2, Amplitude v3, Amplitude v4) => Scale(v1 + v2 + v3 + v4, 4);
        public static Amplitude Add(Amplitude v1, Amplitude v2, Amplitude v3, Amplitude v4, Amplitude v5) => Scale(v1 + v2 + v3 + v4 + v5, 5);
        public static Amplitude Add(Amplitude v1, Amplitude v2, Amplitude v3, Amplitude v4, Amplitude v5, Amplitude v6) => Scale(v1 + v2 + v3 + v4 + v5 + v6, 6);
        public static Amplitude ToDecibel(double value) => (Amplitude)(20 * Math.Log10(value));
        public static Amplitude FromDecibel(double value) => (Amplitude)Math.Pow(10, value / 20);

        /// <summary>
        /// Normalize a short to the range -1:1
        /// </summary>
        public static double Normalize(Amplitude value) => value / MaxValue;
    }
}
