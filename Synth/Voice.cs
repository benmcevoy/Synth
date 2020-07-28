using System;

namespace Synth
{
    public static class Mixer
    {
        public static byte Volume = 128;

        // TODO: support more voices...
        public static byte Mix(double t, params Voice[] voices)
            => Convert.ToByte(Volume * Average(t, voices) / byte.MaxValue);

        private static byte Average(double t, params Voice[] voices) 
            => Convert.ToByte(voices[0].Output(t) / (voices.Length ));

    }

    public class Voice
    {
        public Func<double, double> Frequency = (t) => PitchTable.A4;
        public Func<double, byte> Volume = (t) => 255;
        public Func<double, double, double, byte> WaveForm = Synth.WaveForm.SineWave();
        public Func<double, byte, byte> Envelope = Synth.Envelope.Mute();
        public Func<double, double, double> PulseWidth = (t, f) => PitchTable.A4 / 2;

        public byte AttackRate = 16;
        public double AttackDuration = 1;
        public byte DecayRate = 16;
        public double DecayDuration = 1;
        public byte SustainLevel = 32;
        public byte ReleaseRate = 16;
        public double ReleaseDuration = 1;

        public Func<double, byte, byte> TriggerAttack(double t0)
            => Envelope = Synth.Envelope.TriggerAttack(t0, AttackRate, AttackDuration, DecayRate, DecayDuration, SustainLevel);

        public Func<double, byte, byte> TriggerRelease(double t0)
            => Envelope = Synth.Envelope.TriggerRelease(t0, ReleaseRate, ReleaseDuration);

        public byte Output(double t)
            => Convert.ToByte(Volume(t) * (double)Envelope(t, WaveForm(t, Frequency(t), PulseWidth(t, Frequency(t)))) / byte.MaxValue);
    }
}
