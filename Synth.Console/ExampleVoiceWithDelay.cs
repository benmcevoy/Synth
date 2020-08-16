using System;

namespace Synth.Console
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

            _buffer.Write((short)(inputSample + outputSample * DelayFeedback()));
            _buffer.Delay = Delay();

            return @out.With((short)(inputSample + outputSample));
        }
    }

    class ExampleVoiceWithDelayAndFilter : Voice
    {
        private readonly Filter _filter;
        private readonly CircularDelayBuffer _buffer;

        public Func<double> Delay = () => 0.5;
        public Func<double> DelayFeedback = () => 0.5;
        public Func<double, double> FilterFrequency = (t) => 1000;
        public Func<double, double> FilterResonance = (t) => 0.707;

        public ExampleVoiceWithDelayAndFilter(int sampleRate)
        {
            _buffer = new CircularDelayBuffer(sampleRate) { Delay = Delay() };
            _filter = new Filter(sampleRate);
        }

        public override VoiceOutput Output(double t)
        {
            var @out = base.Output(t);

            var filter = FilterImpl(t, @out.Out);

            var delay = DelayImpl(t, filter);

            return @out.With(delay);
        }

        private short DelayImpl(double t, short v)
        {
            var inputSample = v;
            var outputSample = _buffer.Read();

            _buffer.Write((short)(inputSample + outputSample * DelayFeedback()));
            _buffer.Delay = Delay();

            return (short)(inputSample + outputSample);
        }

        private short FilterImpl(double t, short v)
        {
            var @out = base.Output(t);
            var output = _filter.LowPass(FilterFrequency(t), FilterResonance(t), @out.Out);

            return output;
        }
    }
}
