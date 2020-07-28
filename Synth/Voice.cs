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
            => Convert.ToByte(voices[0].Output(t) / (voices.Length));

    }

    public class Voice
    {
        public Func<double, double> Frequency = (t) => PitchTable.A4;
        public Func<double, byte> Volume = (t) => 255;
        public Func<double, double, double, byte> WaveForm = Synth.WaveForm.SineWave();
        public Func<double, byte, byte> Envelope = Synth.Envelope.Mute();
        public Func<double, double, double> PulseWidth = (t, f) => PitchTable.A4 / 2;

        public double Attack = 0.5;
        public double Decay = 1;
        public byte Sustain = 128;
        public double Release = 1;

        public Func<double, byte, byte> TriggerAttack(double t0)
            => Envelope = Synth.Envelope.TriggerAttack(t0, Attack, Decay, Sustain);

        public Func<double, byte, byte> TriggerRelease(double t0)
            => Envelope = Synth.Envelope.TriggerRelease(t0, Sustain, Release);

        public byte Output(double t)
            => Convert.ToByte(Volume(t) * (double)Envelope(t, WaveForm(t, Frequency(t), PulseWidth(t, Frequency(t)))) / byte.MaxValue);
    }
}
