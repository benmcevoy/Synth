using System;
using static System.Math;
using static System.Byte;
using WaveForm = System.Func<double, double, byte>;

namespace Synth
{
    public static class WaveForms
    {
        // 8 bit PCM is 0-255, hence the 128 + (127 * f()) to offset signed doubles

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
        /// Offset and scale signed double to fit into the range of an byte (0-255)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte Offset(double value) => Convert.ToByte(128 + (127 * value));

        public static WaveForm SineWave(double pulseWidth = 0) => (t, f) => Offset(Sin(Angle(f, t)));
        public static WaveForm SquareWave(double pulseWidth = 0) => (t, f) => Offset(Sign(Sin(Angle(f, t))));
        public static WaveForm Noise(double pulseWidth = 0) => (t, f) => (byte)Random.Next(MinValue, MaxValue + 1);
        public static WaveForm Triangle(double pulseWidth = 0) => (t, f) => Offset(Asin(Sin(Angle(f, t))) / HalfPI);
        public static WaveForm Sawtooth(double pulseWidth = 0) => (t, f) => Convert.ToByte(MaxValue * (f * t % 0.9));

        public static WaveForm PulseWave(double pulseWidth = 220)
            => (t, f)
            => Convert.ToByte(MaxValue * H(Cos(Angle(f, t)) - Cos(PI * pulseWidth * t)));
    }
}
