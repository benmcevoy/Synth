using W = System.Func<Synth.Time, Synth.Frequency, double, Synth.Phase, Synth.WaveForm.Phasor>;

namespace Synth.Noise
{
    public interface INoiseGenerator { W Next(); }
}
