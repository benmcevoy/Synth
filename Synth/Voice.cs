using System;

namespace Synth
{
    public static class Master
    {
        public static byte Volume = 128;
        public static byte Mix(double t, Voice voice) => Convert.ToByte(Volume * voice.Output(t) / byte.MaxValue);
    }

    public class Voice
    {
        public Func<double, double> Frequency = (t) => PitchTable.A4;
        public Func<double, byte> Volume = (t) => 255;
        public Func<double, double, double, byte> WaveForm = Synth.WaveForm.SineWave();
        public Func<double, byte, byte> Envelope = Synth.Envelope.Mute();
        public Func<double, double, double> PulseWidth = (t, f) => PitchTable.A4 / 2;

        public byte AttackRate = 64;
        public double AttackDuration = 128;
        public byte DecayRate = 128;
        public double DecayDuration = 64;
        public byte SustainLevel = 32;
        public byte ReleaseRate = 16;
        public double ReleaseDuration = 1024;

        public Func<double, byte, byte> TriggerAttack(double t0) 
            => Envelope = Synth.Envelope.TriggerAttack(t0, AttackRate, AttackDuration, DecayRate, DecayDuration, SustainLevel);

        public Func<double, byte, byte> TriggerRelease(double t0)
            => Envelope = Synth.Envelope.TriggerRelease(t0, ReleaseRate, ReleaseDuration);


        public byte Output(double t)
            => Convert.ToByte(Volume(t) * (double)Envelope(t, WaveForm(t, Frequency(t), PulseWidth(t, Frequency(t)))) / byte.MaxValue);
    }
}
