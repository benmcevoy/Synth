﻿namespace Synth.WaveForm
{
    // TODO: bad name. it's a complex number essentially
    public struct WaveFormOut
    {
        public WaveFormOut(Amplitude amplitude, double phase)
        {
            Amplitude = amplitude;
            Phase = phase;
        }

        public double Phase;
        public Amplitude Amplitude;
    }
}