using System;

namespace Synth.Instrument.Monophonic
{
    class ExampleVoiceWithDelay : Voice
    {
        private readonly CircularDelayBuffer _buffer;

        public Func<double> Delay = () => 0.5;
        public Func<double> DelayFeedback = () => 0.5;

        public ExampleVoiceWithDelay(int sampleRate)
            => _buffer = new CircularDelayBuffer(sampleRate) { Delay = Delay() };

        public override VoiceOutput Output(double t)
        {
            var @out = base.Output(t);

            var inputSample = @out.Out;
            var outputSample = _buffer.Read();

            _buffer.Write(Convert.ToByte(Amplitude.Constrain(inputSample + outputSample * DelayFeedback())));
            _buffer.Delay = Delay();

            return @out.With(Convert.ToByte(Amplitude.Constrain(inputSample + outputSample)));
        }
    }
}
