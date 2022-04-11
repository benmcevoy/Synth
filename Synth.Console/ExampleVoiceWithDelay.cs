using System;

namespace Synth.Console
{
    /// <summary>
    /// This voice has delay parameters that can be manipulated.
    /// </summary>
    /// <example>
    ///     new ExampleVoiceWithDelay(){
    ///         Delay = () => 0.25,
    //          DelayFeedback = () => 0.1,
    ///     };
    /// </example>
    class ExampleVoiceWithDelay : Voice
    {
        private readonly CircularDelayBuffer _buffer;

        public Func<double> Delay = () => 0.5;
        public Func<double> DelayFeedback = () => 0.5;

        public ExampleVoiceWithDelay(int sampleRate) : base(sampleRate)
            => _buffer = new CircularDelayBuffer(SampleRate) { Delay = Delay() };

        public override VoiceOutput Output(Time t)
        {
            var @out = base.Output(t);

            var inputSample = @out.Out;
            var outputSample = _buffer.Read();

            _buffer.Write((short)(inputSample + outputSample * DelayFeedback()));
            _buffer.Delay = Delay();

            return @out.With((short)(inputSample + outputSample));
        }
    }
}
