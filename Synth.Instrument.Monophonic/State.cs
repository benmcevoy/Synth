using System;
using System.Collections.Generic;
using System.Windows.Input;
using Synth.WaveForm;

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
        public double DelayFeedback = 0;

        public Frequency FilterFrequency = 1000;
        public double FilterResonance = 0.7;

        public double LFO;
        public bool IsLfoRoutedToPulseWidth;
        public bool IsLfoRoutedToHarmonic;
        public bool IsLfoRingModulate;

        public Frequency Modulate(bool enabled, Time t, Frequency f)
            => enabled
                ? new Frequency(f + Math.Abs(Math.Sin(2 * Math.PI * LFO * t)))
                : f;

        public Func<Time, Frequency, double, Phase, Phasor> WaveForm(Time t)
            => IsLfoRingModulate
                ? WaveForms.Detune(
                    WaveFormVolume(FromName(WF1, Harmonic1), Volume1),
                    WaveFormVolume(FromName(WF2, Modulate(IsLfoRoutedToHarmonic, t, Harmonic2)), Volume2), LFO)
                : WaveForms.Add(
                    WaveFormVolume(FromName(WF1, Harmonic1), Volume1),
                    WaveFormVolume(FromName(WF2, Modulate(IsLfoRoutedToHarmonic, t, Harmonic2)), Volume2));

        private static Func<Time, Frequency, double, Phase, Phasor> FromName(string name, double harmonic)
            => name switch
            {
                "Sine" => WaveForms.SineWave(),
                "Square" => WaveForms.SquareWave(),
                "Triangle" => WaveForms.Triangle(),
                "Sawtooth" => WaveForms.Sawtooth(),
                "Noise" => WaveForms.Noise(new Noise.WhiteNoiseGenerator()),
                _ => WaveForms.SineWave()
            };

        private static Func<Time, Frequency, double, Phase, Phasor> WaveFormVolume(Func<Time, Frequency, double, Phase, Phasor> wave, Amplitude volume)
            => (t, f, w, p)
            => (Amplitude)(volume * wave(t, f, w, p).Amplitude);
    }
}

