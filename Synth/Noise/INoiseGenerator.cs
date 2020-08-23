using W = System.Func<Synth.Time, double, double, short>;

namespace Synth.Noise
{
    public interface INoiseGenerator { W Next(); }
}
