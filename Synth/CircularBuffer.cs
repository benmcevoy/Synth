namespace Synth
{
    public class CircularBuffer
    {
        protected readonly short[] Buffer;
        protected readonly int Size;
        protected int WritePointer;

        public CircularBuffer(int size)
        {
            Size = size;

            Buffer = new short[size];
        }

        public virtual void Write(short value)
        {
            WritePointer++;

            if (WritePointer >= Size) WritePointer = 0;

            Buffer[WritePointer] = value;
        }

        public virtual short Read() => Buffer[WritePointer];
    }
}