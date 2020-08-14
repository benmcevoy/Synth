using System;
using W = System.Func<double, double, double, byte>;

namespace Synth.Noise
{
    [Obsolete("based on c64 23 bit LSFR/XOR but not working")]
    // i think the real one was doing this LSFR as fast as you like (1MHz) then sampling it at f, e.g. 440Hz
    // when doing that on a thread it does make noise, but it's not tonal or pitched
    // in c64 if you do a frequency sweep it makes a crazy rushing sound
    // instead pink noise (3db drop around a f) would do it, like a band pass filter you can move up/down the frequency
    // but I have yet to write anything in the freq domain yet (still thinking/learning FIR and digital filters)
    public class PitchedNoiseGenerator : INoiseGenerator
    {
        // 23 bit binary seed
        private int Seed = 0b11111111_11111111_1111110;

        public W Next() => (t, f, w) =>
        {
            var count = f;

            while (count > 0)
            {
                // shift left
                Seed <<= 1;

                // xor bits 22 and 17 and stick it in bit 0
                Seed += Xor();

                count--;
            }

            // The 8 bits of the noise waveform are assembled from bits 20, 18, 14, 11, 9, 5, 2 and 0 of the oscillator.
            // but here we use the lower 8 bits
            return (byte)Seed;
        };

        private int Xor()
        {
            var bit1 = (Seed & (1 << 22 - 1)) != 0;
            var bit2 = (Seed & (1 << 17 - 1)) != 0;

            return (bit1 || bit2) && !(bit1 && bit2) ? 1 : 0;
        }
    }
}
