using System.IO;
using W = System.Func<double, double, double, byte>;

namespace Synth.Console
{
    public class ExampleSamplePlayerWaveForm
    {
        private readonly Stream _stream;
        private readonly double _sampleRate;
        private readonly double _scaleToFrequency;
        private readonly byte[] _buffer = new byte[1];
        private double _position;

        public ExampleSamplePlayerWaveForm(Stream pcmStream, double voiceSampleRate, double pcmStreamSampleRate, double scaleToFrequency = 440)
        {
            _stream = pcmStream;
            _sampleRate = pcmStreamSampleRate / voiceSampleRate;
            _stream.Position = 44;
            _scaleToFrequency = scaleToFrequency;
        }

        public W Sample(double harmonic = 1)
        => (t, f, w) =>
            {
                _stream.Read(_buffer, 0, 1);
                _position += _sampleRate * (f / _scaleToFrequency) * harmonic;
                _stream.Position = (int)_position;

                if (_stream.Position > _stream.Length) _position = 44;

                return _buffer[0];
            };
    }
}

