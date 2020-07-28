using System;
using static System.Math;
using static System.Byte;
using E = System.Func<double, byte, byte>;

namespace Synth
{
    public static class Envelope
    {
        // convert the duration to a rate
        private static double T(double duration) => 85 / duration * Pow(duration, 2) / 8;

        public static E Attack(double t0, double duration)
            => (t, v)
                => T(duration) * (t - t0) < MaxValue
                    ? v = Convert.ToByte(v * (T(duration) * (t - t0)) / MaxValue)
                    : v;

        public static E Decay(double t0, double duration)
            => (t, v)
                => T(duration) * (t - t0) < MaxValue
                    ? v = Convert.ToByte(v * ((MaxValue - T(duration) * (t - t0)) / MaxValue))
                    : v = MinValue;

        public static E Sustain(byte s) => (t, v) => v = Convert.ToByte(v * s / MaxValue);

        public static E Release(double t0, double rate)
            => Decay(t0, rate);

        public static E Mute() => (t0, v) => 0;

        internal static E TriggerAttack(double t0, double attack, double decay, byte sustain)
            => (t, v)
                => AttackState(t0, t, attack, decay) switch
                {
                    1 => Attack(t0, T(attack))(t, v),
                    2 => Decay(t0 + attack, T(decay))(t, v),
                    _ => Sustain(sustain)(t, v)
                };

        internal static E TriggerRelease(double t0, byte sustain, double release)
            => (t, v)
                => ReleaseState(t0, t, release) switch
                {
                    1 => Release(t0, T(release))(t, Sustain(sustain)(t, v)),
                    _ => Mute()(t, v)
                };

        private static bool Elapsed(double t0, double t1, double duration)
            => t1 - t0 > duration;

        private static int AttackState(double t0, double t, double attackDuration, double decayDuration)
            => !Elapsed(t0, t, attackDuration) ? 1
                : !Elapsed(t0, t + attackDuration, decayDuration) ? 2
                : int.MaxValue;

        private static int ReleaseState(double t0, double t, double releaseDuration)
            => !Elapsed(t0, t, releaseDuration) ? 1
                : int.MaxValue;
    }
}