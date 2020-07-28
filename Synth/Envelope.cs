using System;
using static System.Byte;
using E = System.Func<double, byte, byte>;

namespace Synth
{
    public static class Envelope
    {
        public static E Attack(double t0, double duration)
            => (t, v)
                => !Elapsed(t0, t, duration)
                    ? Convert.ToByte(v * (v * (t - t0)) / MaxValue)
                    : MaxValue;

        public static E Decay(double t0, double duration, byte sustain)
            => (t, v)
                => !Elapsed(t0, t, duration)
                    ? Convert.ToByte(v * (MaxValue - (sustain * (t - t0))) / MaxValue)
                    : MinValue;

        public static E Sustain(byte sustain)
            => (t, v)
                => v = Convert.ToByte(v * sustain / MaxValue);

        public static E Release(double t0, double duration, byte sustain)
            => (t, v)
                => !Elapsed(t0, t, duration)
                    ? Convert.ToByte(v * (sustain - (sustain * (t - t0))) / MaxValue)
                    : MinValue;

        public static E Mute()
            => (t, v)
                => MinValue;

        internal static E TriggerAttack(double t0, double attack, double decay, byte sustain)
            => (t, v)
                => AttackState(t0, t, attack, decay) switch
                    {
                        1 => Attack(t0, attack)(t, v),
                        2 => Decay(t0 + attack, decay, sustain)(t, v),
                        _ => Sustain(sustain)(t, v)
                    };

        internal static E TriggerRelease(double t0, byte sustain, double release)
            => (t, v)
                => ReleaseState(t0, t, release) switch
                    {
                        1 => Release(t0, release, sustain)(t, v),
                        _ => Mute()(t, v)
                    };

        private static bool Elapsed(double t0, double t, double duration)
            => t - t0 > duration;

        private static int AttackState(double t0, double t, double attack, double decay)
            => !Elapsed(t0, t, attack) ? 1
                : !Elapsed(t0 + attack, t, decay) ? 2
                : int.MaxValue;

        private static int ReleaseState(double t0, double t, double release)
            => !Elapsed(t0, t, release) ? 1
                : int.MaxValue;
    }
}