using System;

namespace Synth
{
    // TODO: add tempo calulcations for BPM
    public struct Time
    {
        public const double MinValue = 0;
        public const double MaxValue = double.MaxValue;

        public readonly double Value;

        public Time(double t) => Value = Assert(t);

        private static double Assert(double t) 
            => t < 0 
            ? throw new ArgumentOutOfRangeException(nameof(t), "value must be positive") 
            : t;

        public static implicit operator double(Time t) => t.Value;

        public static implicit operator Time(double t) => new Time(t);

        public static bool HasElapsed(Time t0, Time t, double duration)
            => duration <= 0 || Elapsed(t0, t, duration) >= 1;

        public static double Elapsed(Time t0, Time t, double duration)
            => (t - t0) / duration;
    }
}
