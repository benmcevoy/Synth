using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Synth.Instrument.Monophonic
{
    class State
    {
        public Stack<Key> NotePriority = new Stack<Key>();

        public string WF1 = "Sine";
        public double Harmonic1 = 1;
        public short Volume1 = short.MaxValue;
        public string WF2 = "Square";
        public double Harmonic2 = 0;
        public short Volume2 = short.MaxValue;

        public double PulseWidth = 0;
        public double Attack = 0.1;
        public double Decay = 0.1;
        public short SustainLevel = short.MaxValue;
        public double Release = 0.1;

        public double Delay = 0;
        public double DelayFeedback =0;

        public double FilterFrequency = 1000;
        public double FilterResonance = 0.7;

        public double LFO;
        public bool IsLfoRoutedToPulseWidth;
        public bool IsLfoRoutedToHarmonic;
        public bool IsLfoRingModulate;

        public double Modulate(bool enabled, double t, double f)
            => enabled
                ? Synth.WaveForm.SineWave()(t, f + LFO, 0)
                : f;

        public Func<double, double, double, short> WaveForm(double t)
            => IsLfoRingModulate
                ? Synth.WaveForm.Detune(
                    WaveFormVolume(FromName(WF1, Harmonic1), Volume1),
                    WaveFormVolume(FromName(WF2, Modulate(IsLfoRoutedToHarmonic, t, Harmonic2)), Volume2), LFO)
                : Synth.WaveForm.Add(
                    WaveFormVolume(FromName(WF1, Harmonic1), Volume1),
                    WaveFormVolume(FromName(WF2, Modulate(IsLfoRoutedToHarmonic, t, Harmonic2)), Volume2));

        private static Func<double, double, double, short> FromName(string name, double harmonic)
            => name switch
            {
                "Sine" => Synth.WaveForm.SineWave(harmonic),
                "Square" => Synth.WaveForm.SquareWave(harmonic),
                "Triangle" => Synth.WaveForm.Triangle(harmonic),
                "Sawtooth" => Synth.WaveForm.Sawtooth(harmonic),
                "Noise" => Synth.WaveForm.Noise(new Noise.WhiteNoiseGenerator()),
                _ => Synth.WaveForm.SineWave(harmonic)
            };

        private static Func<double, double, double, short> WaveFormVolume(Func<double, double, double, short> wave, short volume)
            => (t, f, w)
            => (short)(volume * wave(t, f, w));
    }
}

