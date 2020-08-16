using System;
using W = System.Func<double, double, double, short>;

namespace Synth.Noise
{
    public class WhiteNoiseGenerator : INoiseGenerator
    {
        private static readonly Random _random = new Random();

        public W Next() => (t, f, w) => (short)(short.MaxValue * _random.NextDouble());
    }
}
