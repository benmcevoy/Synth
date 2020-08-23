using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Synth
{
    public class MonoWaveStream : Stream
    {
        private readonly double _sampleRate;
        private readonly Voice _voice;
        private readonly byte[] _header;

        public MonoWaveStream(int sampleRate, Voice voice)
        {
            _sampleRate = sampleRate;
            _voice = voice;
            _header = Header(sampleRate * 4, sizeof(short) * 8, sampleRate, 1);
        }

        public double Time => Position / _sampleRate;

        public override int Read(byte[] buffer, int offset, int count)
        {
            var counter = 0;

            while (Position < _header.Length && Position < count)
            {
                buffer[offset + Position] = _header[Position];
                Position++;
                counter++;
            }

            while (counter < count)
            {
                Append(ref buffer, _voice.Output(Time).Out.Value, counter);
                counter += sizeof(short);
                Position++;
            }

            return counter;
        }

        public override long Seek(long offset, SeekOrigin origin)
            => Position = (origin == SeekOrigin.Begin)
                ? Position = 0
                : Position += offset;

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

        private static int Append<T>(ref byte[] target, T source, int offset)
            => Append(ref target, new T[] { source }, offset);

        private static int Append(ref byte[] target, string value, int offset)
            => Append(ref target, Encoding.ASCII.GetBytes(value), offset);

        private static int SizeOf<T>(T[] value)
            => value.Length * Marshal.SizeOf(value[0]);

        private static int Append<T>(ref byte[] target, T[] source, int offset)
        {
            var count = SizeOf(source);

            Buffer.BlockCopy(source, 0, target, offset, count);

            return count;
        }

        public override long Position { get; set; }
        public override void Flush() { }
        public override void SetLength(long value) { }
        public override void Write(byte[] buffer, int offset, int count) { }
        public override bool CanRead { get; } = true;
        public override bool CanSeek { get; } = true;
        public override bool CanWrite { get; } = false;
        public override long Length { get; }
    }
}
