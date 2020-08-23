using System;
using W = System.Func<Synth.Time, Synth.Frequency, double, Synth.Phase, Synth.WaveForm.Phasor>;

namespace Synth.Noise
{
    public class WhiteNoiseGenerator : INoiseGenerator
    {
        private static readonly Random _random = new Random();

        public W Next() => (t, f, w, p) => (Amplitude)(Amplitude.MaxValue * _random.NextDouble());
    }
}
