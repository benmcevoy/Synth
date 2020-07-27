using System;
using static System.Math;
using static System.Byte;
using E = System.Func<double, byte, byte>;

namespace Synth
{
    public static class Envelope
    {
        private static bool Elapsed(double t0, double t1, double duration)
            => t1 - t0 > duration;

        private static int State(double t0, double t, double attackDuration, double decayDuration)
            => !Elapsed(t0, t, attackDuration) ? 1
                : !Elapsed(t0, t, decayDuration) ? 2
                : int.MaxValue;

        public static E TriggerADS(double t0, byte a, double aT, byte d, double dT, byte s)
            => (t, v)
                => State(t0, t, aT, dT) switch
                {
                    1 => v = Attack(t0, a)(t, v),
                    2 => v = Decay(t0 + aT, d)(t, v),
                    _ => v = Sustain(t0 + aT + dT, s)(t, v)
                };

        public static E TriggerR(double t0, byte r)
            => (t, v)
                => Release(t0, r)(t, v);

        // convert the rate to a duration
        private static double T(byte rate) => 85 / rate * Pow(rate, 2) / 8;

        public static E Pulse(double t0 = 0, byte rate = 16)
            => (t, v)
                => Convert.ToByte(128 + Sin(rate * t) / 2 * v);

        public static E Attack(double t0 = 0, byte rate = 32)
            => (t, v)
                => T(rate) * (t - t0) < MaxValue
                    ? Convert.ToByte(v * (T(rate) * (t - t0)) / MaxValue)
                    : v;

        public static E Decay(double t0 = 0, byte rate = 16)
            => (t, v)
                => T(rate) * (t - t0) < MaxValue
                    ? Convert.ToByte(v * ((MaxValue - T(rate) * (t - t0)) / MaxValue))
                    : MinValue;

        public static E Sustain(double t0 = 0, byte rate = MaxValue)
            => (t, v)
                => Convert.ToByte(v * rate / MaxValue);

        public static E Release(double t0 = 0, byte rate = 16)
            => Decay(t0, rate);
    }
}