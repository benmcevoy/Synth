using System;

namespace Synth
{
    // TODO: add tempo calulcations for BPM
    public struct Time
    {
        public readonly double Value;

        public Time(double t) => Value = Assert(t);

        private static double Assert(double t) 
            => t < 0 
            ? throw new ArgumentException("Value needs to be positive") 
            : t;

        public static implicit operator double(Time t) => t.Value;

        public static implicit operator Time(double t) => new Time(t);

        public static bool HasElapsed(double t0, double t, double duration)
            => duration <= 0 || Elapsed(t0, t, duration) >= 1;

        public static double Elapsed(double t0, double t, double duration)
            => (t - t0) / duration;
    }
}
