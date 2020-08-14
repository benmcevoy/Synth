using W = System.Func<double, double, double, byte>;

namespace Synth.Noise
{
    public interface INoiseGenerator
    {
        W Next();
    }
}
