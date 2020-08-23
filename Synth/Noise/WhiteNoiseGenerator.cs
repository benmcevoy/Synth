using System;
using W = System.Func<Synth.Time, double, double, Synth.Amplitude>;

namespace Synth.Noise
{
    public class WhiteNoiseGenerator : INoiseGenerator
    {
        private static readonly Random _random = new Random();

        public W Next() => (t, f, w) => (Amplitude)(Amplitude.MaxValue * _random.NextDouble());
    }
}
