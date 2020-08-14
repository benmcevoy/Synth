using System;

namespace Synth.Console
{
    class ExampleVoiceWithDelay : Voice
    {
        private readonly CircularBuffer _buffer;

        public double Delay = 0.5;

        public ExampleVoiceWithDelay(int sampleRate)
        {
            _buffer = new CircularBuffer(4 * sampleRate, sampleRate);
        }

        public override VoiceOutput Output(double t)
        {
            var @out = base.Output(t);

            _buffer.Delay = Delay;
            _buffer.Write(@out.Out);

            return new VoiceOutput(Convert.ToByte((_buffer.Read() + @out.Out) / 2), @out.Envelope, @out.Time);
        }
    }
}
