namespace Synth
{
    public class CircularBuffer
    {
        protected readonly byte[] Buffer;
        protected readonly int Size;
        protected int WritePointer;

        public CircularBuffer(int size)
        {
            Size = size;

            Buffer = new byte[size];
        }

        public virtual void Write(byte value)
        {
            WritePointer++;

            if (WritePointer >= Size) WritePointer = 0;

            Buffer[WritePointer] = value;
        }

        public virtual byte Read() => Buffer[WritePointer];
    }
}