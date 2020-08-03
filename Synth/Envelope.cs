using System;
using static System.Byte;
using E = System.Func<double, byte, byte>;

namespace Synth
{
    public static class Envelope
    {
        // todo: envelopes need to respect the starting volume.
        // ah! i need a v0? otherwise these ramps start at zero and it's all clicky
        // also need to be able to abandon the current 
        public static E Attack(double t0, double duration)
            => (t, v)
                => !HasElapsed(t0, t, duration)
                    ? Convert.ToByte(v * Elapsed(t0, t, duration))
                    : MaxValue;

        public static E Decay(double t0, double duration, byte sustain)
            => (t, v)
                => !HasElapsed(t0, t, duration)
                    ? Convert.ToByte(v - v * Elapsed(t0, t, duration) * (MaxValue - sustain) / MaxValue)
                    : sustain;

        public static E Sustain(byte sustain)
            => (t, v)
                => v = Convert.ToByte(v * sustain / MaxValue);

        public static E Release(double t0, double duration, byte sustain)
             => (t, v)
                => !HasElapsed(t0, t, duration)
                    ? Convert.ToByte(v * ((-sustain * Elapsed(t0, t, duration) + sustain) / MaxValue))
                    : MinValue;

        public static E Mute()
            => (t, v)
                => MinValue;

        public static E TriggerADSR(double t0, double t, double attack, double decay, byte sustain, double sustainDuration, double release)
            => !HasElapsed(t0, t, attack) ? Attack(t0, attack)
                : !HasElapsed(t0 + attack, t, decay) ? Decay(t0 + attack, decay, sustain)
                : !HasElapsed(t0 + attack + decay, t, sustainDuration) ? Sustain(sustain)
                : !HasElapsed(t0 + attack + decay + sustainDuration, t, release) ? Release(t0 + attack + decay + sustainDuration, release, sustain)
                : Mute();

        internal static E TriggerAttack(double t0, double attack, double decay, byte sustain)
            => (t, v)
                => !HasElapsed(t0, t, attack) ? Attack(t0, attack)(t, v)
                : !HasElapsed(t0 + attack, t, decay) ? Decay(t0 + attack, decay, sustain)(t, v)
                : Sustain(sustain)(t, v);

        internal static E TriggerRelease(double t0, byte sustain, double release)
            => (t, v)
                => !HasElapsed(t0, t, release) 
                    ? Release(t0, release, sustain)(t, v)
                    : Mute()(t, v);

        private static bool HasElapsed(double t0, double t, double duration)
            => duration <= 0 || Elapsed(t0, t, duration) >= 1;

        private static double Elapsed(double t0, double t, double duration)
            => (t - t0) / duration;
    }
}