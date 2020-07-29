using System;
using static System.Math;
using W = System.Func<double, double, double, byte>;

namespace Synth.Console
{
    public class ExampleWaveFormsModifiers
    {
        // slide up and down
        public static W Vibrato(W wave, int speed = 120)
            => (t, f, w)
                => wave(t, Sin(speed * t) + f, w);

        // would be nice to have it go up/down
        public static W Arpeggio(W wave, double t0, int[] scale, double speed = 0.1)
        => (t, f, w)
            => wave(t, f + Twelfth(f) * scale[Limit(Quantize(t0, t, speed), scale.Length - 1)], w);


        // needs to be circular or stop, or up/down - a "bend"
        // wants to glide from scale[n] to scale[n+1]
        [Obsolete("not working yet")]
        public static W Glissando(W wave, double[] scale, int speed = 16)
            => (t, f, w)
                => wave(t, scale[(int)Floor(speed * t % scale.Length)], w);

        private static double Twelfth(double value) => value / 12d;

        private static int Quantize(double t0, double t, double speed)
            => (int)Floor(speed + ((t - t0) / speed));

        private static int Limit(int value, int limit)
            => value >= limit ? limit : value;
    }

    public class Arpeggio
    {
        public static int[] Diminshed7th = new int[] { 0, 3, 7, 11, 14, 17, 20, 24, 20, 17, 14, 11, 7, 3, 0 };
        public static int[] TonicTriad = new int[] { 0, 1, 3, 5, 3, 1, 0 };
    }
}
