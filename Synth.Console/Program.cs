using System;
using Synth.Frequency;

namespace Synth.Console
{
    class Program
    {
        static void Main(string[] args)
        {

            // TODO: review all the todo's
            // convert to 16 bit.  jumping between signal scales of -1:1, 0:1 and 0:255 is very confusing. 
            // you can always decimate back to 8bit at the end if you want
            // tempo
            // amplitude adding
            // parameter inputs
            // delay, filter, etc - convert to a pipeline, add an on/off switch  OUTPUT PHASES
            // arpeggiator! also pipelined, on/off...  INPUT PHASE
            // LFO just waveform at low f, on/off INPUT PHASE
            // wavetable support for line in and samples, and our waveforms
            // waveforms/wavetable - get the c64 to spit them out as 0-255 values? won't work for pulsewidth...?
            // metronome
            // filter - add high pass so we can have band and notch somehow?

            // *******************************************
            // where is the line between core and external?
            // *******************************************

            // sync metronome to tempo, delay to tempo, etc, lfo to tempo

            // delay, filter, arp, envelope, etc all have parameters

            // Voice return raw pcm, stream does the header, and multiplexing 
            // device is removed from core, get rid of the naudio dependancy 

            // cleanup, stabilize and build the control surface
            // sequencer
            // midi support
            // etc etc

            const int sampleRate = 44100;

            var voice =  new ExampleVoiceWithDelayAndFilter(sampleRate)
            {
                Volume = () => 128,
                Attack = () => 0.2,
                Decay = () => 0,
                Release = () => 0.2,
                SustainLevel = () => 240,
                SustainDuration = () => 3,
                FilterFrequency = (t) => Pulsator(t, 1800, 500),
                FilterResonance = (t) => Pulsator(t, 5, 10),
                DelayFeedback = () => 0.5,
                Delay = () => 0.5
            };

            // lol
            //var sample = System.IO.File.Open("guitar-loop.wav", System.IO.FileMode.Open);
            //voice.WaveForm = new ExampleSamplePlayerWaveForm(sample, sampleRate, sampleRate, PitchTable.C3(0))
            //    .Sample();

            //ExampleWaveFormsModifiers.Arpeggio(WaveForm.Triangle(), pcm.Time, Arpeggio.Foo);

            //voice.WaveForm = WaveForm.Add(WaveForm.SineWave(), WaveForm.SineWave(1.5));// ;, WaveForm.SineWave(2), WaveForm.SineWave(2.5));

            var pcm = new EightBitPcmStream(sampleRate, voice);
            var device = new Devices.WaveOutDevice(pcm, sampleRate, 1);
            var isPlaying = true;

            device.Play();

            voice.TriggerOn();

            while (isPlaying)
            {
                if (!System.Console.KeyAvailable) continue;

                var key = System.Console.ReadKey().Key;

                if (key == ConsoleKey.Escape) break;

                voice.Frequency = ProcessKeyPress(key);
                voice.WaveForm = ExampleWaveFormsModifiers.Arpeggio(WaveForm.SineWave(0.25), pcm.Time, Arpeggio.Nice, 10, ArpeggioDirection.Up);
                //voice.TriggerOn();
                
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