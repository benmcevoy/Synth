using Synth.WaveForm;

namespace Synth
{
    public struct VoiceOutput
    {
        // TOOD: reorder these to output, time, envelope, waveform, ???, maybe dry/wet instead of out?
        public VoiceOutput(Amplitude output, double envelope, Time time, Phasor p)
        {
            Out = output;
            Envelope = envelope;
            Time = time;
            Phasor = p;
        }

        public Amplitude Out;
        public double Envelope;
        public Time Time;
        public Phasor Phasor;

        public VoiceOutput With(Amplitude output) => new VoiceOutput(output, Envelope, Time, Phasor);
    }
}

