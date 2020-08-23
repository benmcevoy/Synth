using static System.Math;

namespace Synth.WaveForm
{
    public class SineWaveForm : WaveForm
    {
        public SineWaveForm(int sampleRate) : base(sampleRate) { }

        public override WaveFormOut Out(Time t, double f, double w, WaveFormOut waveFormOut)
        {
            var phase = Phase(f, waveFormOut.Phase);

            return new WaveFormOut(Scale(Sin(phase)), phase);
        }
    }
}