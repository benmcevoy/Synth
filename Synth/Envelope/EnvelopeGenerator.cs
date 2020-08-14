using static Synth.Time;
using static Synth.Envelope.Easings;
using static Synth.Amplitude;

using E = System.Func<double, double>;

namespace Synth.Envelope
{
    public static class EnvelopeGenerator
    {
        public static E Attack(double t0, double e0, double duration)
            => (t)
                => !HasElapsed(t0, t, duration)
                    ? Linear(e0, 1)(t0, t, duration)
                    : 1;
        public static E Decay(double t0, double e0, double duration, byte sustainLevel)
            => (t)
                => !HasElapsed(t0, t, duration)
                    ? Linear(1, Normalize(sustainLevel))(t0, t, duration)
                    : Normalize(sustainLevel);

        public static E Sustain(byte sustainLevel)
            => (t)
                => Normalize(sustainLevel);

        public static E Release(double t0, double e0, double duration)
             => (t)
                => !HasElapsed(t0, t, duration)
                    ? Linear(e0, 0)(t0, t, duration)
                    : 0;

        public static E Mute() => (t) => 0;

        public static E TriggerADSR(double t0, double e0, double attack, double decay, byte sustainLevel, double sustainDuration, double release)
            => (t)
                => !HasElapsed(t0, t, attack) ? Attack(t0, e0, attack)(t)
                    : !HasElapsed(t0 + attack, t, decay) ? Decay(t0 + attack, e0, decay, sustainLevel)(t)
                    : !HasElapsed(t0 + attack + decay, t, sustainDuration) ? Sustain(sustainLevel)(t)
                    : !HasElapsed(t0 + attack + decay + sustainDuration, t, release) ? Release(t0 + attack + decay + sustainDuration, Normalize(sustainLevel), release)(t)
                    : Mute()(t);

        public static E TriggerAttack(double t0, double e0, double attack, double decay, byte sustainLevel)
            => (t)
                => !HasElapsed(t0, t, attack) ? Attack(t0, e0, attack)(t)
                : !HasElapsed(t0 + attack, t, decay) ? Decay(t0 + attack, e0, decay, sustainLevel)(t)
                : Sustain(sustainLevel)(t);

        public static E TriggerRelease(double t0, double e0, byte sustainLevel, double release)
            => (t)
                => !HasElapsed(t0, t, release)
                    ? Release(t0, e0 > Normalize(sustainLevel) ? e0 : Normalize(sustainLevel), release)(t)
                    : Mute()(t);
    }
}