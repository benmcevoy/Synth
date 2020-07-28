using System;
using static System.Byte;

namespace Synth
{
    public static class Mixer
    {
        public static byte Volume = 128;

        // TODO: support more voices...
        public static byte Mix(double t, Voice voice)
            => Convert.ToByte(Volume * voice.Output(t) / MaxValue);

    }

    public class Voice
    {
        public Func<double, double> Frequency = (t) => PitchTable.A4;
        public Func<double, byte> Volume = (t) => 255;
        public Func<double, double, double, byte> WaveForm = Synth.WaveForm.SineWave();
        public Func<double, byte, byte> Envelope = Synth.Envelope.Mute();
        public Func<double, double, double> PulseWidth = (t, f) => PitchTable.A4 / 2;

        public double Attack = 0.5;
        public double Decay = 0.5;
        public byte Sustain = 128;
        public double Release = 1;

        public Func<double, byte, byte> TriggerAttack(double t0)
            => Envelope = Synth.Envelope.TriggerAttack(t0, Attack, Decay, Sustain);

        public Func<double, byte, byte> TriggerRelease(double t0)
            => Envelope = Synth.Envelope.TriggerRelease(t0, Sustain, Release);

        public byte Output(double t)
            => Convert.ToByte(Volume(t) * (double)Envelope(t, WaveForm(t, Frequency(t), PulseWidth(t, Frequency(t)))) / MaxValue);
    }
}
