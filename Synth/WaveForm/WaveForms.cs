using static System.Math;
using Synth.Noise;

using W = System.Func<Synth.Time, Synth.Frequency, double, Synth.Phase, Synth.WaveForm.Phasor>;

namespace Synth.WaveForm
{
    // TODO: this class is probaly a real thing and should not be static
    public static class WaveForms
    {
        public const double TwoPI = 2 * PI;
        public const double HalfPI = PI / 2;
        public static int SampleRate = 44100;

        /// <summary>
        /// Angular frequency in radians.
        /// </summary>
        public static double Angle(double t, double f) => TwoPI * f * t;

        /// <summary>
        /// A sinusoidal wave form.
        /// </summary>
        public static W SineWave() => (t, f, w, p) => new Phasor(Amplitude.Scale(Sin(Phase(f, p))), Phase(f, p));

        /// <summary>
        /// A square wave form that can be modulated by the PulseWidth function. Very similar to the pulse waveform.
        /// </summary>
        public static W SquareWave() => (t, f, w, p) => Amplitude.Scale(Sign(Sin(Angle(f, t)) - Sin(PI * w * t)));

        /// <summary>
        /// A random noise wave form generator.
        /// </summary>
        public static W Noise(INoiseGenerator noiseGenerator = null) => (noiseGenerator ?? new WhiteNoiseGenerator()).Next();

        /// <summary>
        /// A triangle wave form.
        /// </summary>
        public static W Triangle() => (t, f, w, p) => Amplitude.Scale(Asin(Sin(Angle(f, t))) / HalfPI);

        /// <summary>
        /// A sawtooth wave form.
        /// </summary>
        public static W Sawtooth() => (t, f, w, p) => Amplitude.Scale(f * t % 0.9F);

        public static W RingModulate(W w1, W w2)
           => (t, f, w, p) => new Phasor((short)(w1(t, f, w, p).Amplitude * w2(t, f, w, p).Amplitude), p);

        public static W Detune(W w1, W w2, double r)
           => (t, f, w, p) => Amplitude.Add(w1(t, (1 + r) * f, w, p), w2(t, (1 - r) * f, w, p));

        /// <summary>
        /// Combine multiple waveforms by addition to produce a new wave form.
        /// </summary>
        public static W Add(W w1) => w1;

        /// <summary>
        /// Combine multiple waveforms by addition to produce a new wave form.
        /// </summary>
        public static W Add(W w1, W w2)
                => (t, f, w, p) => Amplitude.Add(w1(t, f, w, p), w2(t, f, w, p));

        /// <summary>
        /// Combine multiple waveforms by addition to produce a new wave form.
        /// </summary>
        public static W Add(W w1, W w2, W w3)
            => (t, f, w, p) => Amplitude.Add(w1(t, f, w, p), w2(t, f, w, p), w3(t, f, w, p));

        /// <summary>
        /// Combine multiple waveforms by addition to produce a new wave form.
        /// </summary>
        public static W Add(W w1, W w2, W w3, W w4)
            => (t, f, w, p) => Amplitude.Add(w1(t, f, w, p), w2(t, f, w, p), w3(t, f, w, p), w4(t, f, w, p));

        private static Phase Phase(Frequency f, Phase p0) => Limit(Next(f, p0));
        private static double Next(Frequency f, double p) => p += TwoPI * f / SampleRate;
        private static Phase Limit(double p) => p >= TwoPI ? p - TwoPI : p;
    }
}