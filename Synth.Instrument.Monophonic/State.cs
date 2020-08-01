﻿using System;
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

        public Func<double, double, double, byte> WaveForm()
            => Synth.WaveForm.Add(
                WaveFormVolume(FromName(WF1, Harmonic1), Volume1),
                WaveFormVolume(FromName(WF2, Harmonic2), Volume2));

        private static Func<double, double, double, byte> FromName(string name, double harmonic) 
            => name switch
                {
                    "Sine" => Synth.WaveForm.SineWave(harmonic),
                    "Square" => Synth.WaveForm.SquareWave(harmonic),
                    "Triangle" => Synth.WaveForm.Triangle(harmonic),
                    "Pulse" => Synth.WaveForm.PulseWave(harmonic),
                    "Sawtooth" => Synth.WaveForm.Sawtooth(harmonic),
                    _ => Synth.WaveForm.SineWave(harmonic)
                };

        private static Func<double, double, double, byte> WaveFormVolume(Func<double, double, double, byte> wave, byte volume)
            => (t, f, w)
            => Envelope.Sustain(wave(t,f,w))(t, volume);
    }
}