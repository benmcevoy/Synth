using static System.Math;
using Synth.Noise;

using W = System.Func<Synth.Time, double, double, short>;
using W1 = System.Func<Synth.Time, double, double, Synth.WaveForm.WaveFormOut, Synth.WaveForm.WaveFormOut>;

namespace Synth.WaveForm
{
    // TODO: this class is probaly a real thing and should not be static
    public static class WaveForms
    {
        public const double TwoPI = 2 * PI;
        public const double HalfPI = PI / 2;

        private static short Scale(double value) => (short)(short.MaxValue * value);

        public static int SampleRate = 44100;

        /// <summary>
        /// Angular frequency in radians.
        /// </summary>
        /// <param name="t"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public static double Angle(double t, double f) => TwoPI * f * t;

        // TODO: this must go
        private static IWaveForm _sine = new SineWaveForm(SampleRate);
        /// <summary>
        /// A sinusoidal wave form.
        /// </summary>
        /// <returns></returns>
        public static W1 SineWave() => _sine.Out;

        /// <summary>
        /// A square wave form that can be modulated by the PulseWidth function. Very similar to the pulse waveform.
        /// </summary>
        public static W SquareWave() => (t, f, w) => Scale(Sign(Sin(Angle(f, t)) - Sin(PI * w * t)));

        /// <summary>
        /// A random noise wave form generator.
        /// </summary>
        /// <returns></returns>
        public static W Noise(INoiseGenerator noiseGenerator = null) => (noiseGenerator ?? new WhiteNoiseGenerator()).Next();

        /// <summary>
        /// A triangle wave form.
        /// </summary>
        /// <returns></returns>
        public static W Triangle() => (t, f, w) => Scale(Asin(Sin(Angle(f, t))) / HalfPI);

        /// <summary>
        /// A sawtooth wave form.
        /// </summary>
        /// <returns></returns>
        public static W Sawtooth() => (t, f, w) => Scale(f * t % 0.9F);

        public static W RingModulate(W w1, W w2)
           => (t, f, w) => (short)(w1(t, f, w) * w2(t, f, w));

        public static W Detune(W w1, W w2, double r)
           => (t, f, w) => (short)(w1(t, (1 + r) * f, w) + w2(t, (1 - r) * f, w));

        /// <summary>
        /// Combine multiple waveforms by addition to produce a new wave form.
        /// </summary>
        public static W Add(W w1) => w1;

        /// <summary>
        /// Combine multiple waveforms by addition to produce a new wave form.
        /// </summary>
        public static W Add(W w1, W w2)
                => (t, f, w) => Amplitude.Add(w1(t, f, w), w2(t, f, w));

        /// <summary>
        /// Combine multiple waveforms by addition to produce a new wave form.
        /// </summary>
        public static W Add(W w1, W w2, W w3)
            => (t, f, w) => Amplitude.Add(w1(t, f, w), w2(t, f, w), w3(t, f, w));

        /// <summary>
        /// Combine multiple waveforms by addition to produce a new wave form.
        /// </summary>
        public static W Add(W w1, W w2, W w3, W w4)
            => (t, f, w) => Amplitude.Add(w1(t, f, w), w2(t, f, w), w3(t, f, w), w4(t, f, w));
    }
}