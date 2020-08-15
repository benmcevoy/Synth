namespace Synth
{
    public struct VoiceOutput
    {
        public VoiceOutput(byte output, double envelope, double time)
        {
            Out = output;
            Envelope = envelope;
            Time = time;
        }

        public byte Out;
        public double Envelope;
        public double Time;

        public VoiceOutput With(byte output) => new VoiceOutput(output, Envelope, Time);
    }
}

