namespace Synth
{
    public class Time
    {
        public static bool HasElapsed(double t0, double t, double duration)
            => duration <= 0 || Elapsed(t0, t, duration) >= 1;

        public static double Elapsed(double t0, double t, double duration)
            => (t - t0) / duration;
    }
}
