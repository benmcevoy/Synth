using System;

namespace Synth
{
    public struct Time
    {
        private readonly double _value;

        public Time(double t)
        {
            if (t < 0) throw new ArgumentException("Value needs to be positive");

            _value = t;
        }

        public static implicit operator double(Time t) => t._value;

        public static implicit operator Time(double t)
        {

            if (t < 0)
                throw new ArgumentOutOfRangeException("Only positive values allowed");
            return new Time(t);
        }

        // TODO: add tempo calulcations for BPM

        public static bool HasElapsed(double t0, double t, double duration)
            => duration <= 0 || Elapsed(t0, t, duration) >= 1;

        public static double Elapsed(double t0, double t, double duration)
            => (t - t0) / duration;
    }
}
