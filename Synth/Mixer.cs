using System;
using static System.Byte;

namespace Synth
{
    public static class Mixer
    {
        public static byte Volume = 128;

        // TODO: support more voices...
        public static byte Mix(double t, Voice voice)
            => Convert.ToByte(Volume * voice.Output(t) / MaxValue);
    }
}
