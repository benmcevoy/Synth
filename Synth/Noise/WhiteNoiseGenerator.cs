using System;
using W = System.Func<double, double, double, byte>;

namespace Synth.Noise
{
    public class WhiteNoiseGenerator : INoiseGenerator
    {
        private static readonly Random _random = new Random();

        public W Next() => (t, f, w) => Convert.ToByte(_random.Next(byte.MinValue, byte.MaxValue + 1));

        public static double Random() => _random.NextDouble();
    }
}
