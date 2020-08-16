using System;
using Synth.Frequency;
using static System.Math;
using W = System.Func<double, double, double, short>;

namespace Synth.Console
{
    public class ExampleWaveFormsModifiers
    {
        // slide up and down
        public static W Vibrato(W wave, int speed = 120)
            => (t, f, w)
                => wave(t, Sin(speed * t) + f, w);

        public static W Arpeggio(W wave, double t0, int[] scale, double speed = 0.1, ArpeggioDirection direction = ArpeggioDirection.Up)
        => (t, f, w)
            => direction switch
            {
                ArpeggioDirection.Up => wave(t, Pitch.FromReference(f, scale[Up(t0, t, 1 / speed, scale.Length)])(t), w),
                ArpeggioDirection.Down => wave(t, Pitch.FromReference(f, scale[Down(t0, t, 1 / speed, scale.Length - 1)])(t), w),
                ArpeggioDirection.PingPong => wave(t, Pitch.FromReference(f, scale[PingPong(t0, t, 1 / speed, scale.Length - 1)])(t), w),
                ArpeggioDirection.Random => wave(t, Pitch.FromReference(f, scale[Random(t0, t, 1 / speed, scale.Length - 1)])(t), w),
                _ => wave(t, f, w)
            };

        private static int Up(double t0, double t, double speed, int length)
            => (int)Floor((t - t0) / speed % length);

        private static int Down(double t0, double t, double speed, int length)
            => (int)Floor((t - t0) / speed % length);

        private static int PingPong(double t0, double t, double speed, int length)
            => (int)Floor((t - t0) / speed % length);

        // TODO: so naive - needs to quantize , respect speed, remember the random value for the current time slice
        // etc.
        private static int Random(double t0, double t, double speed, int length)
            => _random.Next(0, length + 1);

        private static readonly Random _random = new Random();
    }

    public enum ArpeggioDirection
    {
        Up,
        Down,
        PingPong,
        Random
    }

    public class Arpeggio
    {
        public static int[] Diminshed7th = new int[] { 0, 3, 7, 11, 14, 17, 20, 24, 20, 17, 14, 11, 7, 3 };
        public static int[] TonicTriad = new int[] { 0, 1, 3, 5, 3, 1 };
        public static int[] Nice = new int[] { 0, 11, 2, 14, 16, 4, 18, 6 };
    }
}
