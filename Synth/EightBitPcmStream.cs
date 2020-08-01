using System;
using System.IO;
using System.Text;

namespace Synth
{
    public class EightBitPcmStream : Stream
    {
        private readonly double _sampleRate;
        private readonly Voice _voice;
        private readonly byte[] _header;
        private long _position;

        public EightBitPcmStream(int sampleRate, Voice voice)
        {
            _sampleRate = sampleRate;
            _voice = voice;
            _header = Header(sampleRate * 4, 8, sampleRate, 1);
        }

        public double Time => _position / _sampleRate;

        public override int Read(byte[] buffer, int offset, int count)
        {
            var counter = 0;

            while (_position < _header.Length && _position < count)
            {
                buffer[offset + _position] = _header[_position];
                _position++;
                counter++;
            }

            while (counter < count)
            {
                buffer[offset + counter] = _voice.Output(Time);
                counter++;
                _position++;
            }

            return counter;
        }

        public override void Flush() { }
        public override long Seek(long offset, SeekOrigin origin)
        {
            if (origin == SeekOrigin.Begin)
            {
                _position = 0;
                return _position;
            }

            _position += offset;

            return _position;
        }

        public override void SetLength(long value) { }
        public override void Write(byte[] buffer, int offset, int count) { }
        public override bool CanRead { get; } = true;
        public override bool CanSeek { get; } = true;
        public override bool CanWrite { get; } = false;
        public override long Length { get; }

        public override long Position
        {
            get => _position;
            set => _position = value;
        }

        private static byte[] Header(int dataLength, short bitDepth, int sampleRate, short numberOfChannels)
        {
            const short pcm = 1;
            var audioFormatNumberOfChannels = new[] { pcm, numberOfChannels };
            var output = new byte[44];
            var offset = 0;

            offset += Append(ref output, "RIFF", offset);
            offset += Append(ref output, 36 + dataLength, offset);
            offset += Append(ref output, "WAVEfmt ", offset);
            offset += Append(ref output, 16, offset);
            offset += Append(ref output, audioFormatNumberOfChannels, offset);
            offset += Append(ref output, sampleRate, offset);
            offset += Append(ref output, sampleRate * numberOfChannels * (bitDepth / 8), offset);
            offset += Append(ref output, (short)(numberOfChannels * (bitDepth / 8)), offset);
            offset += Append(ref output, bitDepth, offset);
            offset += Append(ref output, "data", offset);
            offset += Append(ref output, dataLength * numberOfChannels * (bitDepth / 8), offset);

            return output;
        }

        private static int Append(ref byte[] target, string value, int offset)
        {
            return Append(ref target, Encoding.ASCII.GetBytes(value), offset);
        }

        private static int Append(ref byte[] target, byte[] value, int offset)
        {
            var count = value.Length;

            Buffer.BlockCopy(value, 0, target, offset, count);

            return count;
        }

        private static int Append(ref byte[] target, short[] value, int offset)
        {
            var count = value.Length * sizeof(short);

            Buffer.BlockCopy(value, 0, target, offset, count);

            return count;
        }

        private static int Append(ref byte[] target, int[] value, int offset)
        {
            var count = value.Length * sizeof(int);

            Buffer.BlockCopy(value, 0, target, offset, count);

            return count;
        }

        private static int Append(ref byte[] target, long[] value, int offset)
        {
            var count = value.Length * sizeof(long);

            Buffer.BlockCopy(value, 0, target, offset, count);

            return count;
        }

        private static int Append(ref byte[] target, byte value, int offset)
        {
            const int count = sizeof(byte);

            Buffer.BlockCopy(BitConverter.GetBytes(value), 0, target, offset, sizeof(byte));

            return count;
        }

        private static int Append(ref byte[] target, short value, int offset)
        {
            const int count = sizeof(short);

            Buffer.BlockCopy(BitConverter.GetBytes(value), 0, target, offset, count);

            return count;
        }

        private static int Append(ref byte[] target, long value, int offset)
        {
            const int count = sizeof(long);

            Buffer.BlockCopy(BitConverter.GetBytes(value), 0, target, offset, count);

            return count;
        }

        private static int Append(ref byte[] target, int value, int offset)
        {
            const int count = sizeof(int);

            Buffer.BlockCopy(BitConverter.GetBytes(value), 0, target, offset, count);

            return count;
        }
    }
}
