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
    }
}

