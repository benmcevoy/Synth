using static System.Math;

namespace Synth.WaveForm
{
    // TODO: instead of WaveFormOut replace with a struct WaveForm here instead, or combine as per the other primitives
    public abstract class WaveForm : IWaveForm
    {
        protected int SampleRate;
        protected const double TwoPI = 2 * PI;
        public abstract WaveFormOut Out(Time t, double f, double w, WaveFormOut waveFormOut);
        protected WaveForm(int sampleRate) => SampleRate = sampleRate;
        protected double Phase(double f, double p0) => Limit(Next(f, p0));
        private double Next(double f, double p0) => p0 += TwoPI * f / SampleRate;
        private double Limit(double phase) => phase >= TwoPI ? phase - TwoPI : phase;
    }
}