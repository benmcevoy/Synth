namespace Synth.WaveForm
{
    public struct Phasor
    {
        public Phasor(Amplitude amplitude, Phase phase)
        {
            Amplitude = amplitude;
            Phase = phase;
        }

        public Phase Phase;
        public Amplitude Amplitude;

        public static implicit operator Phase(Phasor p) => p.Phase;

        public static implicit operator Phasor(Phase p) => new Phasor(0, p);

        public static implicit operator Amplitude(Phasor a) => a.Amplitude;

        public static implicit operator Phasor(Amplitude a) => new Phasor(a, 0);
    }
}