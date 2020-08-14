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
        public byte Volume1 = 255;
        public string WF2 = "Square";
        public double Harmonic2 = 0;
        public byte Volume2 = 255;

        public double PulseWidth = 0;
        public double Attack = 0.1;
        public double Decay = 0.1;
        public byte SustainLevel = 240;
        public double Release = 0.1;

        public double Delay = 0;
        public double DelayFeedback = 0;

        public double LFO;
        public bool IsLfoRoutedToPulseWidth;
        public bool IsLfoRoutedToHarmonic;
        public bool IsLfoRingModulate;

        public double Modulate(bool enabled, double t, double f)
            => enabled
                ? Synth.WaveForm.SineWave()(t, f + LFO, 0)
                : f;

        public Func<double, double, double, byte> WaveForm(double t)
            => IsLfoRingModulate
                ? Synth.WaveForm.Detune(
                    WaveFormVolume(FromName(WF1, Harmonic1), Volume1),
                    WaveFormVolume(FromName(WF2, Modulate(IsLfoRoutedToHarmonic, t, Harmonic2)), Volume2), LFO)
                : Synth.WaveForm.Add(
                    WaveFormVolume(FromName(WF1, Harmonic1), Volume1),
                    WaveFormVolume(FromName(WF2, Modulate(IsLfoRoutedToHarmonic, t, Harmonic2)), Volume2));

        private static Func<double, double, double, byte> FromName(string name, double harmonic)
            => name switch
            {
                "Sine" => Synth.WaveForm.SineWave(harmonic),
                "Square" => Synth.WaveForm.SquareWave(harmonic),
                "Triangle" => Synth.WaveForm.Triangle(harmonic),
                "Sawtooth" => Synth.WaveForm.Sawtooth(harmonic),
                "Noise" => Synth.WaveForm.Noise(new Noise.PitchedNoiseGenerator()),
                _ => Synth.WaveForm.SineWave(harmonic)
            };

        private static Func<double, double, double, byte> WaveFormVolume(Func<double, double, double, byte> wave, byte volume)
            => (t, f, w)
            => (byte)(Amplitude.Normalize(volume) * wave(t, f, w));
    }
}

