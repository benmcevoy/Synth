using System;

namespace Synth
{
    public static class Filter
    {

        // Calculate the filter coefficients based on the given parameters
        // Borrows code from the Bela Biquad library, itself based on code by
        // Nigel Redmon
        public static Tuple<double[], double[]> CalculateCoefficients(double t, double f, double q)
        {
            var k = Math.Tan(Math.PI * f * t);
            var norm = 1.0 / (1 + k / q + k * k);
            var input = new double[2] { 2 * (k * k - 1) * norm, (1 - k / q + k * k) * norm };
            var output = new double[3] { k * k * norm, 2 * k * k * norm, k * k * norm };

            return new Tuple<double[], double[]>(input, output);
        }

        // 1st order IIR
        //public static byte LowPass(double r)
        // r * y[n-1] + (1-r)x[n]
        // y is output
        // x is input
        // need a circular buffer or two
    }
}
