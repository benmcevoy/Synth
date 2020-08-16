using System.IO;
using NAudio.Wave;

namespace Synth.Instrument.Monophonic
{
    public class WaveOutDevice 
    {
        private readonly WaveOutEvent _device = new WaveOutEvent();

        public WaveOutDevice(Stream source, int sampleRate)
            => _device.Init(new RawSourceWaveStream(source, new WaveFormat(sampleRate, 8, 1)));

        public void Play()
        {
            Stop();

            for (var i = 0f; i < 1f; i += 0.1f)
            {
                _device.Volume = i;
            }

            _device.Volume = 1;

            _device.Play();
        }

        public void Stop()
        {
            for (var i = 1f; i > 0f; i -= 0.1f)
            {
                _device.Volume = i;
            }

            _device.Volume = 0;
        }
    }
}
