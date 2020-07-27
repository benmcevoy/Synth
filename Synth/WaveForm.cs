using System;
using static System.Math;
using static System.Byte;
using W = System.Func<double, double, double, byte>;

namespace Synth
{
    public static class WaveForm
    {
        public const double TwoPI = 2 * PI;
        public const double HalfPI = PI / 2;
        public static Random Random = new Random();
        public static double Angle(double t, double f) => TwoPI * f * t;

        /// <summary>
        /// Heaviside step function, 0 if negative, 1 if positive
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int H(double value) => Sign(value) == -1 ? 0 : 1;

        /// <summary>
        /// Offset and scale signed double to fit into the range of a byte (0-255)
        /// </summary>
        /// <remarks>
        /// 8 bit PCM is 0-255, hence the 128 + (127 * f()) to offset signed doubles
        /// </remarks>
        /// <param name="value">a value between 0 and 1</param>
        /// <returns></returns>
        private static byte Offset(double value) => Convert.ToByte(128 + (127 * value));

        public static W SineWave() => (t, f, w) => Offset(Sin(Angle(f, t)));
        public static W SquareWave() => (t, f, w) => Offset(Sign(Sin(Angle(f, t))));
        public static W Noise() => (t, f, w) => (byte)Random.Next(MinValue, MaxValue + 1);
        public static W Triangle() => (t, f, w) => Offset(Asin(Sin(Angle(f, t))) / HalfPI);
        public static W Sawtooth() => (t, f, w) => Convert.ToByte(MaxValue * (f * t % 0.9));
        public static W PulseWave() => (t, f, w) => Convert.ToByte(MaxValue * H(Cos(Angle(f, t)) - Cos(PI * w * t)));
    }
}
