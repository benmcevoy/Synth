using static System.Math;

namespace Synth.WaveForm
{
    public abstract class WaveForm : IWaveForm
    {

        protected WaveForm(int sampleRate) => SampleRate = sampleRate;
        protected int SampleRate;
        protected const double TwoPI = 2 * PI;
        protected static Amplitude Scale(double value) => (Amplitude)(Amplitude.MaxValue * value);

        protected double Phase(double f, double p0)
        {
            p0 += TwoPI * f / SampleRate;

            return p0 >= TwoPI ? p0 - TwoPI : p0;
        }

        public abstract WaveFormOut Out(Time t, double f, double w, WaveFormOut waveFormOut);
    }
}