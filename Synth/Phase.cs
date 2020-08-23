using System;

namespace Synth
{
    public struct Phase
    {
        public const double MinValue = 0;
        public const double MaxValue = Math.PI * 2;

        public readonly double Value;

        public Phase(double p) => Value = Assert(p);

        private static double Assert(double p)
            => p < 0
            ? throw new ArgumentOutOfRangeException(nameof(p), "value must be positive")
            : p > MaxValue 
            ? throw new ArgumentOutOfRangeException(nameof(p), "value cannot exceed 2*PI") 
            : p;

        public static implicit operator double(Phase p) => p.Value;

        public static implicit operator Phase(double p) => new Phase(p);
    }
}