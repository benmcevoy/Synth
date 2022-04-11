using System;
using System.IO;
using System.Runtime.InteropServices;
using Synth.Console.Devices.SDL2;

namespace Synth.Console.Devices
{
    public class SdlAudioDevice : IDevice
    {
        const int SAMPLES = 4096;
        private readonly Stream _source;
        private readonly int _sampleRate;
        // buffer is 2* as samples are 16 bit = 2 bytes each
        private readonly byte[] _buffer = new byte [SAMPLES*2];
        private readonly IntPtr _handle;
        private SDL.SDL_AudioSpec _want;

        public SdlAudioDevice(Stream source, int sampleRate)
        {
            _source = source;
            _sampleRate = sampleRate;

            if (SDL2.SDL.SDL_InitSubSystem(SDL2.SDL.SDL_INIT_AUDIO) < 0)
                throw new Exception(SDL2.SDL.SDL_GetError());
            
            _handle = GCHandle.Alloc(_buffer, GCHandleType.Pinned).AddrOfPinnedObject();
        }

        public void Play()
        {
            _want = new SDL.SDL_AudioSpec
            {
                channels = 1,
                freq = _sampleRate,
                samples = SAMPLES,
                format = SDL.AUDIO_U16LSB,
                callback = Next
            };

            SDL.SDL_AudioSpec have;

            var device = SDL2.SDL.SDL_OpenAudioDevice(IntPtr.Zero,
                0,
                ref _want,
                out have,
                (int)SDL2.SDL.SDL_AUDIO_ALLOW_FORMAT_CHANGE);
            
            // start playing!
            SDL2.SDL.SDL_PauseAudioDevice(device, 0); 
        }

        public void Stop() { }
        
        private void Next(IntPtr userData, IntPtr stream, int length)
        {
            _source.Read(_buffer, (int)_source.Position, length);
            SDL.SDL_memcpy(stream, _handle, new IntPtr(length));
        }
    }
}