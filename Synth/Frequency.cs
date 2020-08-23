using System;

namespace Synth
{
    public struct Frequency
    {
        public const double MinValue = 0;
        public const double MaxValue = double.MaxValue;

        public readonly double Value;

        public Frequency(double f) => Value = Assert(f);

        private static double Assert(double f)
            => f < 0
            ? throw new ArgumentOutOfRangeException(nameof(f), "value must be positive")
            : f;

        public static implicit operator double(Frequency f) => f.Value;

        public static implicit operator Frequency(double f) => new Frequency(f);

        public static Frequency FromMidiNote(int n)
           => 440 * Math.Pow(2D, (n - 69D) / 12D);

        public static Frequency FromReference(Frequency f, int pitch)
            => f * Math.Pow(2D, pitch / 12D);
    }
}
