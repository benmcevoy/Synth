using System;
using Synth.Frequency;

namespace Synth.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO: review all the todo's
            // DONE - convert to 16 bit.  jumping between signal scales of -1:1, 0:1 and 0:255 is very confusing. 
            // you can always decimate back to 8bit at the end if you want

            // Phase issue when frequency changes - we get a click

            // tempo, duration should be able to be expressed as semi or quarter times
            // metronome
            // sync metronome to tempo, delay to tempo, etc, lfo to tempo

            // amplitude adding
            // parameter inputs
            // delay, filter, arp, envelope, etc all have parameters

            // delay, filter, etc - convert to a pipeline, add an on/off switch  OUTPUT PHASES
            // filter - add high pass so we can have band and notch somehow?

            // arpeggiator! also pipelined, on/off...  INPUT PHASE

            // LFO just waveform at low f, on/off INPUT PHASE

            // wavetable support for line in and samples, and our waveforms
            // waveforms/wavetable - get the c64 to spit them out as 0-255 values? won't work for pulsewidth...?


            // cleanup, stabilize and build the control surface
            // sequencer
            // midi support
            // etc etc

            const int sampleRate = 44100;

            var voice = new Voice();
            var pcm = new MonoWaveStream(sampleRate, voice);
            var device = new Devices.WaveOutDevice(pcm, sampleRate);
            var isPlaying = true;

            device.Play(); voice.TriggerOn();

            while (isPlaying)
            {
                if (!System.Console.KeyAvailable) continue;

                var key = System.Console.ReadKey().Key;

                if (key == ConsoleKey.Escape) break;

                voice.Frequency = ProcessKeyPress(key);
                voice.TriggerADSR();
            }

            device.Stop();
        }

        public static double Harmonic(double t, double f) => f + (t * 300);

        public static double Pulsator(double t, double f, double r) => f + r * Math.Sin(t);

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