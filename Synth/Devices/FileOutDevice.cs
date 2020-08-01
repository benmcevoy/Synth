using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Synth.Devices
{
    public class FileOutDevice : IDevice
    {
        private readonly Stream _source;
        private readonly int _sampleRate;
        private readonly int _durationInSeconds;
        private readonly string _fileName;

        public FileOutDevice(Stream source, int sampleRate, int durationInSeconds, string fileName)
        {
            _source = source;
            _sampleRate = sampleRate;
            _durationInSeconds = durationInSeconds;
            _fileName = fileName;
        }

        public void Play()
        {
            var reader = new BinaryReader(_source);

            var file = File.OpenWrite(_fileName);
            var count = _sampleRate * _durationInSeconds;

            file.Write(reader.ReadBytes(count), 0, count);
        }

        public void Stop()
        {
           // throw new NotImplementedException();
        }
    }
}
