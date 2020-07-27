using System;

namespace Synth
{
    // TODO: should become an object so i can have more than one voice
/*
 * constraints and limits
identify and name the knobs
    volume
    width, speed, should all be limited to byte resolution
    just copy SID

 */


public static class Voice
{
    // TODO: voice volume
    public static byte Volume = 255;

    public static double Frequency = 440;

    public static Func<double, double, byte> WaveForm = WaveForms.SineWave();

    public static Func<double, byte, byte> Envelope = Envelopes.Sustain();


    // TODO: should be moved out of here
    public static byte MasterVolume = 128;

    // TODO: should be moved out of here
    public static byte Master(double t, double f) =>
        MasterVolume > 0 ? Convert.ToByte(MasterVolume * (double)Envelope(t, WaveForm(t, f)) / byte.MaxValue) : byte.MinValue;
}
}
