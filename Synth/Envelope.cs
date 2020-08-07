using static System.Byte;
using E = System.Func<double, byte, byte>;
using static Synth.Time;
using static System.Convert;
using static Synth.Amplitude;
using static Synth.Easings;

namespace Synth
{
    public static class Envelope
    {
        public static E Attack(double t0, double duration)
            => (t, v)
                => !HasElapsed(t0, t, duration)
                    ? ToByte(v * Linear(0, 1)(t0, t, duration))
                    : MaxValue;
        public static E Decay(double t0, double duration, byte sustain)
            => (t, v)
                => !HasElapsed(t0, t, duration)
                    ? ToByte(v * Linear(1, Normalize(sustain))(t0, t, duration))
                    : sustain;

        public static E Sustain(byte sustain)
            => (t, v)
                => v = ToByte(v * Normalize(sustain));

        public static E Release(double t0, double duration, byte sustain)
             => (t, v)
                => !HasElapsed(t0, t, duration)
                    ? ToByte(v * Linear(Normalize(sustain), 0)(t0, t, duration))
                    : MinValue;

        public static E Mute() => (t, v) => MinValue;

        public static E TriggerADSR(double t0, double t, double attack, double decay, byte sustain, double sustainDuration, double release)
            => !HasElapsed(t0, t, attack) ? Attack(t0, attack)
                : !HasElapsed(t0 + attack, t, decay) ? Decay(t0 + attack, decay, sustain)
                : !HasElapsed(t0 + attack + decay, t, sustainDuration) ? Sustain(sustain)
                : !HasElapsed(t0 + attack + decay + sustainDuration, t, release) ? Release(t0 + attack + decay + sustainDuration, release, sustain)
                : Mute();

        public static E TriggerAttack(double t0, double attack, double decay, byte sustain)
            => (t, v)
                => !HasElapsed(t0, t, attack) ? Attack(t0, attack)(t, v)
                : !HasElapsed(t0 + attack, t, decay) ? Decay(t0 + attack, decay, sustain)(t, v)
                : Sustain(sustain)(t, v);

        public static E TriggerRelease(double t0, byte sustain, double release)
            => (t, v)
                => !HasElapsed(t0, t, release)
                    ? Release(t0, release, sustain)(t, v)
                    : Mute()(t, v);
    }
}