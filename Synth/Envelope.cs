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

        private static int AttackState(double t0, double t, double attackDuration, double decayDuration)
            => !Elapsed(t0, t, attackDuration) ? 1
                : !Elapsed(t0, t + attackDuration, decayDuration) ? 2
                : int.MaxValue;

        private static int ReleaseState(double t0, double t, double releaseDuration)
            => !Elapsed(t0, t, releaseDuration) ? 1 : int.MaxValue;

        internal static E TriggerAttack(double t0, byte aR, double aT, byte dR, double dT, byte s)
            => (t, v)
                => AttackState(t0, t, aT, dT) switch
                {
                    1 => v = Attack(t0, aR)(t, v),
                    2 => v = Decay(t0 + aT, dR)(t, v),
                    _ => v = Sustain(s)(t, v)
                };

        internal static E TriggerRelease(double t0, byte rR, double rT)
            => (t, v)
                => ReleaseState(t0, t, rT) switch
                {
                    1 => v = Release(t0, rR)(t, v),
                    _ => v = Mute()(t, v)
                };

        // convert the rate to a duration
        private static double T(byte rate) => 85 / rate * Pow(rate, 2) / 8;

        public static E Pulse(double t0, byte rate)
            => (t, v)
                => Convert.ToByte(128 + Sin(rate * t) / 2 * v);

        public static E Attack(double t0, byte rate)
            => (t, v)
                => T(rate) * (t - t0) < MaxValue
                    ? Convert.ToByte(v * (T(rate) * (t - t0)) / MaxValue)
                    : v;

        public static E Decay(double t0, byte rate)
            => (t, v)
                => T(rate) * (t - t0) < MaxValue
                    ? Convert.ToByte(v * ((MaxValue - T(rate) * (t - t0)) / MaxValue))
                    : MinValue;

        public static E Sustain(byte level) => (t, v) => level;

        public static E Release(double t0, byte rate)
            => Decay(t0, rate);

        public static E Mute() => (t0, v) => 0;
    }
}