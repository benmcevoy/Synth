using System;
using static System.Math;
using static System.Byte;
using Envelope = System.Func<double, byte, byte>;

namespace Synth
{
    public static class ADSR
    {
        // the rate must be converted to a duration equivalent
        // attack - 0:255 => 2:8000 milliSeconds
        // decay - 0:255 => 6:24000
        // sustain - use first byte of t0 0:255 => well, on SID it's gated. 
        // release - same as decay (identical)

        // gated - that's an idea. links to key press.

        // some implementations have the ADS as first phase, then a trigger kicks off R

        // ADSR is in a seperate class then.


        private static byte Rate(byte value, int min, int max)
        {
            return 16;
        }

        public static Envelope TriggerADS(byte a, byte d, byte s) 
            => (t, v) 
                => MaxValue;

        public static Envelope TriggerR(byte r) 
            => (t, v) 
                => Envelopes.Release(t, Rate(r, 6, 24000))(t, v);
    }
}