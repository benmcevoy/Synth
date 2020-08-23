namespace Synth
{
    public class CircularBuffer
    {
        protected readonly Amplitude[] Buffer;
        protected readonly int Size;
        protected int WritePointer;

        public CircularBuffer(int size)
        {
            Size = size;

            Buffer = new Amplitude[size];
        }

        public virtual void Write(Amplitude value)
        {
            WritePointer++;

            if (WritePointer >= Size) WritePointer = 0;

            Buffer[WritePointer] = value;
        }

        public virtual Amplitude Read() => Buffer[WritePointer];
    }
}