using System;
using Synth.Pitch;

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
            // when you have a clock that can tick you then have a step sequencer

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

            var voice = new Voice(sampleRate)
            {
                //SustainDuration = () => 0.2,
                //Release = () => 0.01,
                //Attack = () => 0,
                //Decay = () => 0,

                // WaveForm = WaveForm.WaveForms.Add(
                //             WaveForm.WaveForms.Detune(WaveForm.WaveForms.SineWave(), WaveForm.WaveForms.SineWave(), 0.5),
                //             WaveForm.WaveForms.Detune(WaveForm.WaveForms.SineWave(), WaveForm.WaveForms.SineWave(), 0.6)),

                WaveForm =  WaveForm.WaveForms.SineWave(),
                Frequency = () => 440,
                Volume = () => Amplitude.MaxValue,
                //Delay = () => 0.5,
                //DelayFeedback = () => 0.5,

                //IsFilterEnabled = true,
                //IsDelayEnabled = true,
                //FilterFrequency = (t) => Pulsator(t, 1000, 500),
                //FilterResonance = (t) => Pulsator(t, 12, 6),
                //PulseWidth = (t, f) => Pulsator(t, 4 / 12, 1 / 12),
            };

            var pcm = new MonoWaveStream(sampleRate, voice);
            var device = new Devices.SdlAudioDevice (pcm, sampleRate);
            var isPlaying = true;

            device.Play();

            voice.TriggerOn();

            while (isPlaying)
            {
                if (!System.Console.KeyAvailable) continue;

                var key = System.Console.ReadKey().Key;

                if (key == ConsoleKey.Escape) break;


                voice.Frequency = ProcessKeyPress(key);
                //voice.WaveForm = Arpeggiator.Arpeggio(WaveForm.WaveForms.SineWave(), pcm.Time, Arpeggio.Nice);

            }

            device.Stop();
        }

        public static double Harmonic(double t, double f) => f + (t * 300);

        public static double Pulsator(double t, double f, double r) => f + r * Math.Sin(t * 2);

        private static Func<Frequency> ProcessKeyPress(ConsoleKey key) =>
            key switch
            {
                ConsoleKey.A => () => PitchTable.C2,
                ConsoleKey.W => () => PitchTable.Db2,
                ConsoleKey.S => () => PitchTable.D2,
                ConsoleKey.E => () => PitchTable.Eb2,
                ConsoleKey.D => () => PitchTable.E2,
                ConsoleKey.F => () => PitchTable.F2,
                ConsoleKey.T => () => PitchTable.Gb2,
                ConsoleKey.G => () => PitchTable.G2,
                ConsoleKey.Y => () => PitchTable.Ab2,
                ConsoleKey.H => () => PitchTable.A2,
                ConsoleKey.U => () => PitchTable.Bb2,
                ConsoleKey.J => () => PitchTable.B2,
                ConsoleKey.K => () => PitchTable.C3,
                _ => () => PitchTable.A4
            };
    }
}