using System;

namespace Synth.Frequency
{
    public static class Pitch
    {
        public static Func<double, double> FromMidiNote(int n)
            => (t) => 440 * Math.Pow(2D, (n - 69D) / 12D);

        public static Func<double, double> FromReference(double f, int pitch)
            => (t) => f * Math.Pow(2D, pitch / 12D);
    }
}
