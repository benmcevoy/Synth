using System;

namespace Synth.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            const int sampleRate = 11025;
            const int durationInSeconds = 32;

            Master.Volume = 64;

            var voice = new Voice
            {
                WaveForm = WaveForm.PulseWave(),
                PulseWidth = Harmonic,
                Envelope = Envelope.Decay()
            };

            var pcm = new EightBitPcmStream(sampleRate, durationInSeconds, voice);
            var device = new Devices.WaveOutDevice(pcm, sampleRate, 1);
            //var device = new Devices.FileOutDevice(pcm, sampleRate, durationInSeconds, "wave.wav");

            device.Play();

            while (true)
            {
                // we have reached the limit of console app without hooking WM_ messages
                // WPF version has key up/down to control ADSR... although I am still writing that...
                if (!System.Console.KeyAvailable) continue;

                voice.Frequency = ProcessKeyPress(System.Console.ReadKey().Key);
                
                // need a retrigger here instead
                voice.Envelope = Envelope.Decay(pcm.Time);
            }
        }

        public static double Harmonic(double t, double f) => f + (f * 2) ;

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


