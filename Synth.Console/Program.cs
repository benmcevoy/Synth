using System;

namespace Synth.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            const int sampleRate = 44100;


            var voice = new Voice
            {
                WaveForm = WaveForm.Sawtooth(),
                Volume = () => 255,
                Attack = () => 0.1,
                Decay = () => 0.1,
                SustainLevel = () => 240,
                SustainDuration = () => 1,
                Release = () => 0.2,
            };

            var pcm = new EightBitPcmStream(sampleRate, voice);
            var device = new Devices.WaveOutDevice(pcm, sampleRate, 1);
            var isPlaying = true;

            device.Play();

            while (isPlaying)
            {
                // we have reached the limit of console app without hooking WM_ messages
                // WPF version has key up/down to control ADSR... although I am still writing that...
                if (!System.Console.KeyAvailable) continue;

                var key = System.Console.ReadKey().Key;

                if (key == ConsoleKey.Escape) break;

                voice.Frequency = ProcessKeyPress(key);
                voice.TriggerADSR(pcm.Time);
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


