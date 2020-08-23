namespace Synth.WaveForm
{
    public interface IWaveForm
    {
        WaveFormOut Out(Time t, double f, double w, WaveFormOut waveFormOut);
    }
}