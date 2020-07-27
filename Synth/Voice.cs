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
        public Func<double, byte, byte> Envelope = Synth.Envelope.Sustain();
        public Func<double, double, double> PulseWidth = (t, f) => PitchTable.A4 / 2;

        // ADSR needs producer of values Func<double> for rate and duration.
        // all the variables things should be here as "registers"

        //public void Trigger(double t0)
        //{
        //    Synth.Envelope.TriggerADS(t0)
        //}

        public byte Output(double t)
            => Convert.ToByte(Volume(t) * (double)Envelope(t, WaveForm(t, Frequency(t), PulseWidth(t, Frequency(t)))) / byte.MaxValue);
    }
}
