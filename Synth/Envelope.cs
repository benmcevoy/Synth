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

        public static E ADSR(double t0, double attack, double decay, byte sustain, double sustainDuration, double release)
            => (t, v)
                => !Elapsed(t0, t, attack) ? Attack(t0, attack)(t, v)
                : !Elapsed(t0 + attack, t, decay) ? Decay(t0 + attack, decay, sustain)(t, v)
                : !Elapsed(t0 + attack + decay, t, sustainDuration) ? Sustain(sustain)(t, v)
                : !Elapsed(t0 + attack + decay + release, t, release) ? Release(t0, release, sustain)(t, v)
                : Mute()(t, v);

        internal static E TriggerAttack(double t0, double attack, double decay, byte sustain)
            => (t, v)
                => !Elapsed(t0, t, attack) ? Attack(t0, attack)(t, v)
                : !Elapsed(t0 + attack, t, decay) ? Decay(t0 + attack, decay, sustain)(t, v)
                : Sustain(sustain)(t, v);

        internal static E TriggerRelease(double t0, byte sustain, double release)
            => (t, v)
                => !Elapsed(t0, t, release) ? Release(t0, release, sustain)(t, v)
                    : Mute()(t, v);

        private static bool Elapsed(double t0, double t, double duration)
            => t - t0 > duration;
    }
}