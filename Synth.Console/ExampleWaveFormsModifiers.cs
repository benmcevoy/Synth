using System;
using Synth.Frequency;
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
        // want to get rid of the t0
        public static W Arpeggio(W wave, double t0, int[] scale, double speed = 0.1)
        => (t, f, w)
            => wave(t, Pitch.FromReference(f, scale[Limit(Quantize(t0, t, speed), scale.Length - 1)])(t), w);

        private static int Quantize(double t0, double t, double speed)
            => (int)Floor(speed + ((t - t0) / speed));

        private static int Limit(int value, int limit)
            => value >= limit ? limit : value;
    }

    public class Arpeggio
    {
        public static int[] Diminshed7th = new int[] { 0, 3, 7, 11, 14, 17, 20, 24, 20, 17, 14, 11, 7, 3, 0 };
        public static int[] TonicTriad = new int[] { 0, 1, 3, 5, 3, 1, 0 };
        public static int[] Nice = new int[] { 1, 12, 3, 15, 17, 5, 19, 7, 19, 5, 17, 15, 3, 12 };
    }
}
