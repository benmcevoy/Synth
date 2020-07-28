namespace Synth.Console
{
    public class ExampleVoices
    {
        public static Voice Piano() => new Voice 
        {
            WaveForm = WaveForm.SineWave(),
            Attack = 0.5,
            Decay = 0.5,
            Sustain = 64,
            Release = 1
        };
    }
}
