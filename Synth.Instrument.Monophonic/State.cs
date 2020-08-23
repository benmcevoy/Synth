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
        public Amplitude Volume1 = Amplitude.MaxValue;
        public string WF2 = "Square";
        public double Harmonic2 = 0;
        public Amplitude Volume2 = Amplitude.MaxValue;

        public double PulseWidth = 0;
        public double Attack = 0.1;
        public double Decay = 0.1;
        public Amplitude SustainLevel = Amplitude.MaxValue;
        public double Release = 0.1;

        public double Delay = 0;
        public double DelayFeedback =0;

        public double FilterFrequency = 1000;
        public double FilterResonance = 0.7;

        public double LFO;
        public bool IsLfoRoutedToPulseWidth;
        public bool IsLfoRoutedToHarmonic;
        public bool IsLfoRingModulate;

        public double Modulate(bool enabled, Time t, double f)
            => enabled
                ? Synth.WaveForm.WaveForms.Sawtooth()(t, f + LFO, 0)
                : f;

        public Func<Time, double, double, Amplitude> WaveForm(Time t)
            => IsLfoRingModulate
                ? Synth.WaveForm.WaveForms.Detune(
                    WaveFormVolume(FromName(WF1, Harmonic1), Volume1),
                    WaveFormVolume(FromName(WF2, Modulate(IsLfoRoutedToHarmonic, t, Harmonic2)), Volume2), LFO)
                : Synth.WaveForm.WaveForms.Add(
                    WaveFormVolume(FromName(WF1, Harmonic1), Volume1),
                    WaveFormVolume(FromName(WF2, Modulate(IsLfoRoutedToHarmonic, t, Harmonic2)), Volume2));

        private static Func<Time, double, double, Amplitude> FromName(string name, double harmonic)
            => name switch
            {
               // "Sine" => Synth.WaveForms.SineWave(),
                "Square" => Synth.WaveForm.WaveForms.SquareWave(),
                "Triangle" => Synth.WaveForm.WaveForms.Triangle(),
                "Sawtooth" => Synth.WaveForm.WaveForms.Sawtooth(),
                "Noise" => Synth.WaveForm.WaveForms.Noise(new Noise.WhiteNoiseGenerator()),
                _ => Synth.WaveForm.WaveForms.Triangle()
            };

        private static Func<Time, double, double, Amplitude> WaveFormVolume(Func<Time, double, double, Amplitude> wave, Amplitude volume)
            => (t, f, w)
            => (Amplitude)(volume * wave(t, f, w));
    }
}

