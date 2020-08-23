using System;

namespace Synth
{
    public class Filter
    {
        private double n1, n2, o1, o2, b0, b1, b2, a1, a2 = 0;
        private readonly int _sampleRate;

        public Filter(int sampleRate) => _sampleRate = sampleRate;

        public Amplitude LowPass(Frequency f, double q, Amplitude v)
        {
            Coefficients(f, q);

            var n = 0.5 * v;
            var o =       b0 * n
                        + b1 * n1
                        + b2 * n2
                        - a1 * o1
                        - a2 * o2;

            n2 = n1; n1 = n; o2 = o1; o1 = o;

            return (short)o;
        }

        // Calculate the filter coefficients based on the given parameters
        // Borrows code from the Bela Biquad library, itself based on code by
        // Nigel Redmon
        private void Coefficients(Frequency f, double q)
        {
            var k = Math.Tan(Math.PI * f / _sampleRate);
            var norm = 1D / (1D + k / q + k * k);

            b0 = k * k * norm;
            b1 = 2D * b0;
            b2 = b0;
            a1 = 2D * (k * k - 1D) * norm;
            a2 = (1D - k / q + k * k) * norm;
        }
    }
}
