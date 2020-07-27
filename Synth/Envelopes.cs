using System;
using static System.Math;
using static System.Byte;
using Envelope = System.Func<double, byte, byte>;

namespace Synth
{
    public static class Envelopes
    {
        // convert the rate to a duration
        private static double T(byte rate) => 85 / rate * Pow(rate, 2) / 8;

        public static Envelope Pulse(double t0 = 0, byte rate = 16)
            => (t, v)
                => Convert.ToByte(128 + Sin(rate * t) / 2 * v);

        public static Envelope Attack(double t0 = 0, byte rate = 32)
            => (t, v)
                => T(rate) * (t - t0) < MaxValue
                    ? Convert.ToByte(v * (T(rate) * (t - t0)) / MaxValue)
                    : v;

        public static Envelope Decay(double t0 = 0, byte rate = 16)
            => (t, v)
                => T(rate) * (t - t0) < MaxValue
                    ? Convert.ToByte(v * ((MaxValue - T(rate) * (t - t0)) / MaxValue))
                    : MinValue;

        public static Envelope Sustain(double t0 = 0, byte rate = MaxValue)
            => (t, v)
                => Convert.ToByte(v * rate / MaxValue);

        public static Envelope Release(double t0 = 0, byte rate = 16)
            => Decay(t0, rate);
    }
}