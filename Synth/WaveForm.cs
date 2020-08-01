using System;
using static System.Convert;
using static System.Math;
using static System.Byte;
using W = System.Func<double, double, double, byte>;

namespace Synth
{
    public static class WaveForm
    {
        public const double TwoPI = 2 * PI;
        public const double HalfPI = PI / 2;

        /// <summary>
        /// Angular frequency in radians.
        /// </summary>
        /// <param name="t"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public static double Angle(double t, double f) => TwoPI * f * t;

        private static readonly Random _random = new Random();
        /// <summary>
        /// Random byte in the range 0:255
        /// </summary>
        /// <returns></returns>
        public static byte Random() => ToByte(_random.Next(MinValue, MaxValue + 1));

        /// <summary>
        /// Heaviside step function, 0 if negative, 1 if positive.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int H(double value) => Sign(value) == -1 ? 0 : 1;

        /// <summary>
        /// A sinusoidal wave form.
        /// </summary>
        /// <returns></returns>
        public static W SineWave(double harmonic = 1) => (t, f, w) => Offset(Sin(Angle(f * harmonic, t)));

        /// <summary>
        /// A square wave form that can be modulated by the PulseWidth function. Very similar to the pulse waveform.
        /// </summary>
        public static W SquareWave(double harmonic = 1) => (t, f, w) => Offset(Sign(Sin(Angle(f * harmonic, t)) - Sin(PI * w * t)));

        /// <summary>
        /// A random noise wave form generator.
        /// </summary>
        /// <returns></returns>
        public static W Noise() => (t, f, w) => Random();

        /// <summary>
        /// A triangle wave form.
        /// </summary>
        /// <returns></returns>
        public static W Triangle(double harmonic = 1) => (t, f, w) => Offset(Asin(Sin(Angle(f * harmonic, t))) / HalfPI);

        /// <summary>
        /// A sawtooth wave form.
        /// </summary>
        /// <returns></returns>
        public static W Sawtooth(double harmonic = 1) => (t, f, w) => ToByte(MaxValue * (f * harmonic * t % 0.9));

        /// <summary>
        /// A pulse wave form that can be modulated by the PulseWidth function.  Very similar to the square waveform.
        /// </summary>
        public static W PulseWave(double harmonic = 1) => (t, f, w) => ToByte(MaxValue * H(Cos(Angle(f * harmonic, t)) - Cos(PI * w * t)));

        /// <summary>
        /// Combine multiple waveforms by addition to produce a new wave form.
        /// </summary>
        public static W Add(W w1) => w1;

        /// <summary>
        /// Combine multiple waveforms by addition to produce a new wave form.
        /// </summary>
        public static W Add(W w1, W w2)
            => (t, f, w) => ToByte((w1(t, f, w) + w2(t, f, w)) / 2);

        /// <summary>
        /// Combine multiple waveforms by addition to produce a new wave form.
        /// </summary>
        public static W Add(W w1, W w2, W w3)
            => (t, f, w) => ToByte((w1(t, f, w) + w2(t, f, w) + w3(t, f, w)) / 3);

        /// <summary>
        /// Combine multiple waveforms by addition to produce a new wave form.
        /// </summary>
        public static W Add(W w1, W w2, W w3, W w4)
            => (t, f, w) => ToByte((w1(t, f, w) + w2(t, f, w) + w3(t, f, w) + w4(t, f, w)) / 4);

        /// <summary>
        /// Scale doubles in the range -1:1 to 8 bit unsigned bytes in the range 0:255.
        /// </summary>
        private static byte Offset(double value) => ToByte(128 + value * 127);
    }
}
