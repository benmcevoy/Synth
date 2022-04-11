using System.IO;
using Synth.WaveForm;
using W = System.Func<Synth.Time, Synth.Frequency, double, Synth.Phase, Synth.WaveForm.Phasor>;

namespace Synth.Console
{
    public class ExampleSamplePlayerWaveForm
    {
        private readonly Stream _stream;
        private readonly double _sampleRate;
        private readonly double _scaleToFrequency;
        private readonly byte[] _buffer = new byte[2];
        private double _position;

        public ExampleSamplePlayerWaveForm(Stream pcmStream, double voiceSampleRate, double pcmStreamSampleRate,
            double scaleToFrequency = 440)
        {
            _stream = pcmStream;
            _sampleRate = pcmStreamSampleRate / voiceSampleRate;
            _stream.Position = 44;
            _scaleToFrequency = scaleToFrequency;
        }

        public W Sample(double harmonic = 1)
            => (t, f, w, p) =>
            {
                // read two bytes to make a short
                _stream.Read(_buffer, 0, 2);
                _position += _sampleRate * (f / _scaleToFrequency) * harmonic;
                _stream.Position = (int)_position;

                if (_stream.Position > _stream.Length) _position = 44;

                // second byte [1] is shifted to be the high byte - little endian 
                return new Phasor((short)(_buffer[0] | _buffer[1] << 8), 0d);
            };
    }
}