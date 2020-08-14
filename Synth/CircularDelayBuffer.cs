namespace Synth
{
    public class CircularDelayBuffer : CircularBuffer
    {
        private readonly int _sampleRate;

        public CircularDelayBuffer(int sampleRate) : base(6 * sampleRate) => _sampleRate = sampleRate;

        public override byte Read() => Buffer[(WritePointer - (int)(Delay * _sampleRate) + Size) % Size];

        /// <summary>
        /// Delay is a value in seconds or parts of.
        /// </summary>
        public double Delay = 0.5;
    }
}