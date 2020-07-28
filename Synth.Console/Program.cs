using System;

namespace Synth.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            const int sampleRate = 44100;

            Mixer.Volume = 255;

            var voice = new Voice
            {
                WaveForm = WaveForm.SineWave(),
                Attack = 1,
                Decay = 1,
                Sustain = 128,
                Release = 1,
                Envelope = Envelope.Sustain(128)
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
                voice.TriggerRelease(pcm.Time);
            }

            device.Stop();
        }

        public static double Harmonic(double t, double f) => f + (f * 2);

        public static double Pulsator(double t, double f) => f + Math.Sin(WaveForm.Angle(t, f));

        private static Func<double, double> ProcessKeyPress(ConsoleKey key) =>
            key switch
            {
                ConsoleKey.A => (t) => PitchTable.C3,
                ConsoleKey.W => (t) => PitchTable.Db3,
                ConsoleKey.S => (t) => PitchTable.D3,
                ConsoleKey.E => (t) => PitchTable.Eb3,
                ConsoleKey.D => (t) => PitchTable.E3,
                ConsoleKey.F => (t) => PitchTable.F3,
                ConsoleKey.T => (t) => PitchTable.Gb3,
                ConsoleKey.G => (t) => PitchTable.G3,
                ConsoleKey.Y => (t) => PitchTable.Ab3,
                ConsoleKey.H => (t) => PitchTable.A3,
                ConsoleKey.U => (t) => PitchTable.Bb3,
                ConsoleKey.J => (t) => PitchTable.B3,
                ConsoleKey.K => (t) => PitchTable.C4,
                _ => (t) => PitchTable.A4
            };
    }
}


