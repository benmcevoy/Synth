using System.IO;
using NAudio.Wave;

namespace Synth.Devices
{
    public class WaveOutDevice : IDevice
    {
        private readonly WaveOutEvent _device;

        public WaveOutDevice(Stream source, int sampleRate, int numberOfChannels)
        {
            _device = new WaveOutEvent();
            _device.Init(new RawSourceWaveStream(source, new WaveFormat(sampleRate, 8, numberOfChannels)));
        }

        public void Play()
        {
            _device.Play();
        }
    }
}
