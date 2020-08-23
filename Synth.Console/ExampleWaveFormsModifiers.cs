using System;
using static System.Math;
using W = System.Func<Synth.Time, Synth.Frequency, double, Synth.Amplitude>;

namespace Synth.Console
{
    public class ExampleWaveFormsModifiers
    {
        // slide up and down
        public static W Vibrato(W wave, int speed = 120)
            => (t, f, w)
                => wave(t, Sin(speed * t) + f, w);
    }

    public static class Arpeggiator
    {
        // TODO: should not be a waveform, it's just changing frequency
        public static W Arpeggio(W wave, Time t0, int[] scale, double speed = 10, ArpeggioDirection direction = ArpeggioDirection.Up)
        => (t, f, w)
            => direction switch
            {
                ArpeggioDirection.Up => wave(t, Frequency.FromReference(f, scale[Up(t0, t, 1 / speed, scale.Length)]), w),
                ArpeggioDirection.Down => wave(t, Frequency.FromReference(f, scale[Down(t0, t, 1 / speed, scale.Length - 1)]), w),
                ArpeggioDirection.PingPong => wave(t, Frequency.FromReference(f, scale[PingPong(t0, t, 1 / speed, scale.Length - 1)]), w),
                ArpeggioDirection.Random => wave(t, Frequency.FromReference(f, scale[Random(t0, t, 1 / speed, scale.Length - 1)]), w),
                _ => wave(t, f, w)
            };

        private static int Up(Time t0, Time t, double speed, int length)
            => (int)Floor((t - t0) / speed % length);

        // TODO:
        private static int Down(Time t0, Time t, double speed, int length)
            => (int)Floor((t - t0) / speed % length);

        // TODO:
        private static int PingPong(Time t0, Time t, double speed, int length)
            => (int)Floor((t - t0) / speed % length);

        // TODO: so naive - needs to quantize , respect speed, remember the random value for the current time slice
        // etc.
        private static int Random(Time t0, Time t, double speed, int length)
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

        public static int[] OnTheRun1 = new int[] { 0, 1, 2, 3, 5, 6, 7, 8 };
        public static int[] OnTheRun2 = new int[] { 0, 3, 5, 3, 10, 8, 10, 12 };
        public static int[] OnTheRun3 = new int[] { 0, 3, 10, 3, 10, 8, 3, 5 };
        public static int[] OnTheRun4 = new int[] { 12, 9, 12, 9, 12, 11, 10, 9, 12, 11, 10, 9, 7, 5, 2, 0 };
    }
}
