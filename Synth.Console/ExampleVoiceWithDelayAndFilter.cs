using System;

namespace Synth.Console
{
    class ExampleVoiceWithDelayAndFilter : Voice
    {
        private readonly Filter _filter;
        private readonly CircularDelayBuffer _buffer;

        public bool IsDelayEnabled = false;
        public bool IsFilterEnabled = false;

        public Func<double> Delay = () => 0.5;
        public Func<double> DelayFeedback = () => 0.5;
        public Func<double, double> FilterFrequency = (t) => 1000;
        public Func<double, double> FilterResonance = (t) => 0.707; // 0.707 ~= 1/sqrt(2)

        public ExampleVoiceWithDelayAndFilter(int sampleRate) : base(sampleRate)
        {
            _buffer = new CircularDelayBuffer(SampleRate) { Delay = Delay() };
            _filter = new Filter(SampleRate);
        }

        public override VoiceOutput Output(Time t)
        {
            var @out = base.Output(t);
            var sample = @out.Out;

            if (IsFilterEnabled) sample = FilterImpl(t, sample);
            if (IsDelayEnabled) sample = DelayImpl(t, sample);

            return @out.With(sample);
        }

        private short DelayImpl(Time t, Amplitude v)
        {
            var inputSample = v * 0.5;
            var outputSample = _buffer.Read();

            _buffer.Write((short)(inputSample + outputSample * DelayFeedback()));
            _buffer.Delay = Delay();

            return (short)(inputSample + outputSample);
        }

        private short FilterImpl(Time t, Amplitude v)
        {
            var @out = base.Output(t);
            var output = _filter.LowPass(FilterFrequency(t), FilterResonance(t), @out.Out);

            return output;
        }
    }
}
