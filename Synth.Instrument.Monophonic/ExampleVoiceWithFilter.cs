using System;

namespace Synth.Instrument.Monophonic
{
    public class ExampleVoiceWithFilter : Voice
    {
        private readonly Filter _filter;

        public ExampleVoiceWithFilter(int sampleRate) : base(sampleRate) => _filter = new Filter(sampleRate);
        public Func<double, double> FilterFrequency = (t) => 1000;
        public Func<double, double> FilterResonance = (t) => 0.707;

        public override VoiceOutput Output(Time t)
        {
            var @out = base.Output(t);

            var output = _filter.LowPass(FilterFrequency(t), FilterResonance(t), @out.Out);

            return @out.With(output);
        }
    }
}
