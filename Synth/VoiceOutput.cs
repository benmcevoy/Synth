using Synth.WaveForm;

namespace Synth
{
    public struct VoiceOutput
    {
        // TOOD: reorder these to output, time, envelope, waveform, ???, maybe dry/wet instead of out?
        public VoiceOutput(Amplitude output, double envelope, Time time, WaveFormOut waveFormOut)
        {
            Out = output;
            Envelope = envelope;
            Time = time;
            WaveFormOut = waveFormOut;
        }

        public Amplitude Out;
        public double Envelope;
        public Time Time;
        public WaveFormOut WaveFormOut;

        public VoiceOutput With(Amplitude output) => new VoiceOutput(output, Envelope, Time, WaveFormOut);
    }
}

