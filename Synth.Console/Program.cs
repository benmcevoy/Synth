﻿using System;
using Synth.Frequency;

namespace Synth.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            const int sampleRate = 44100;

            var voice = new ExampleVoiceWithFilter(sampleRate)
            {
                Volume = () => 128,
                Attack = () => 0.2,
                Decay = () => 0,
                Release = () => 0.2,
                SustainLevel = () => 240,
                SustainDuration = () => 1,
                FilterFrequency = (t) => Harmonic(t,1000),
                FilterResonance = (t) => 1
            };

            // lol
            //var sample = System.IO.File.Open("guitar-loop.wav", System.IO.FileMode.Open);
            //voice.WaveForm = new ExampleSamplePlayerWaveForm(sample, sampleRate, sampleRate, PitchTable.C3(0))
            //    .Sample();

            //ExampleWaveFormsModifiers.Arpeggio(WaveForm.Triangle(), pcm.Time, Arpeggio.Foo);



            var pcm = new EightBitPcmStream(sampleRate, voice);
            var device = new Devices.WaveOutDevice(pcm, sampleRate, 1);
            var isPlaying = true;

            device.Play();

            while (isPlaying)
            {
                if (!System.Console.KeyAvailable) continue;

                var key = System.Console.ReadKey().Key;

                if (key == ConsoleKey.Escape) break;

                voice.Frequency = ProcessKeyPress(key);
                voice.WaveForm = ExampleWaveFormsModifiers.Arpeggio(WaveForm.Triangle(0.5), pcm.Time, Arpeggio.Nice, 0.2);
                voice.TriggerADSR();
            }

            device.Stop();
        }

        public static double Harmonic(double t, double f) => f + (t * 120);

        public static double Pulsator(double t, double f) => f + Math.Sin(WaveForm.Angle(t, f));

        private static Func<double, double> ProcessKeyPress(ConsoleKey key) =>
            key switch
            {
                ConsoleKey.A => PitchTable.C3,
                ConsoleKey.W => PitchTable.Db3,
                ConsoleKey.S => PitchTable.D3,
                ConsoleKey.E => PitchTable.Eb3,
                ConsoleKey.D => PitchTable.E3,
                ConsoleKey.F => PitchTable.F3,
                ConsoleKey.T => PitchTable.Gb3,
                ConsoleKey.G => PitchTable.G3,
                ConsoleKey.Y => PitchTable.Ab3,
                ConsoleKey.H => PitchTable.A3,
                ConsoleKey.U => PitchTable.Bb3,
                ConsoleKey.J => PitchTable.B3,
                ConsoleKey.K => PitchTable.C4,
                _ => PitchTable.A4
            };
    }
}