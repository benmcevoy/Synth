using W = System.Func<double, double, double, short>;

namespace Synth.Noise
{
    public interface INoiseGenerator { W Next(); }
}
