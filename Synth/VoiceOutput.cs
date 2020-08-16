namespace Synth
{
    public struct VoiceOutput
    {
        public VoiceOutput(short output, double envelope, double time)
        {
            Out = output;
            Envelope = envelope;
            Time = time;
        }

        public short Out;
        public double Envelope;
        public double Time;

        public VoiceOutput With(short output) => new VoiceOutput(output, Envelope, Time);
    }
}

