namespace Synth
{
    public class CircularBuffer
    {
        private readonly byte[] _buffer;
        private readonly int _size;
        private readonly int _sampleRate;
        private int _writePointer;

        public CircularBuffer(int size, int sampleRate)
        {
            _size = size;
            _sampleRate = sampleRate;
            _buffer = new byte[size];
        }

        public virtual void Write(byte value)
        {
            _buffer[_writePointer] = value;
            
            _writePointer++;

            if (_writePointer >= _size) _writePointer = 0;
        }

        public virtual byte Read() => _buffer[(_writePointer - (int)(Delay * _sampleRate) + _size) % _size];
        
        public double Delay = 0;
    }
}