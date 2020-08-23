using W = System.Func<Synth.Time, double, double, Synth.Amplitude>;

namespace Synth.Noise
{
    public interface INoiseGenerator { W Next(); }
}
