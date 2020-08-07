using System;
using static System.Math;
using static Synth.Time;

namespace Synth
{
    public class Easings
    {
        // envelope ranges from 0 to 1
        // defintion is  Func<t0, time, duration, scale> so we are calulating a "scaling" or volume factor for a point of time
        //. after a bit of thinking I decided to look at animation libraries for easing funcions

        // linear
        // ease (of which there are many variations)
        // https://easings.net/ thanks you

        // oh good, mx+c a line

        /// <summary>
        /// Linear ease. By setting start > end will ease out, e.g. decay.
        /// </summary>
        /// <param name="start">value between 0:1</param>
        /// <param name="end">value between 0:1</param>
        public static Func<double, double, double, double> Linear(double start = 0, double end = 1)
            => (t0, t, d)
                => HasElapsed(t0, t, d)
                    ? end
                    : start + Elapsed(t0, t, d) * (end - start);

        /// <summary>
        /// Sinisoidal ease in or out. By setting start > end will ease out, e.g. decay.
        /// </summary>
        /// <param name="start">value between 0:1</param>
        /// <param name="end">value between 0:1</param>
        public static Func<double, double, double, double> Sine(double start = 0, double end = 1)
            => (t0, t, d)
                => HasElapsed(t0, t, d)
                    ? end
                    : start + (1 - Cos(Elapsed(t0, t, d) * PI / 2)) * (end - start);

        /// <summary>
        /// Sinisoidal ease in and out. By setting start > end will ease out, e.g. decay.
        /// </summary>
        /// <param name="start">value between 0:1</param>
        /// <param name="end">value between 0:1</param>
        public static Func<double, double, double, double> SineInOut(double start = 0, double end = 1)
            => (t0, t, d)
                => HasElapsed(t0, t, d)
                    ? end
                    : start - (Cos(Elapsed(t0, t, d) * PI) - 1) / 2 * (end - start);

        /// <summary>
        /// Quint ease in and out. By setting start > end will ease out, e.g. decay.
        /// </summary>
        /// <param name="start">value between 0:1</param>
        /// <param name="end">value between 0:1</param>
        public static Func<double, double, double, double> Quint(double start = 0, double end = 1)
            => (t0, t, d)
                => HasElapsed(t0, t, d)
                    ? end
                    : start + Pow(Elapsed(t0, t, d), 4) * (end - start);
    }
}
