using System.IO;
using NAudio.Wave;

namespace Synth.Devices
{
    public class WaveOutDevice : IDevice
    {
        private readonly WaveOutEvent _device = new WaveOutEvent();

        public WaveOutDevice(Stream source, int sampleRate, int numberOfChannels)
            => _device.Init(new RawSourceWaveStream(source, new WaveFormat(sampleRate, 8, numberOfChannels)));

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
