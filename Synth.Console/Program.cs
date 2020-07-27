using System;

namespace Synth.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            const int sampleRate = 11025;
            const int durationInSeconds = 32;

            var pcm = new EightBitPcmStream(sampleRate, durationInSeconds);
            var device = new Devices.WaveOutDevice(pcm, sampleRate, 1);
            //var device = new Devices.FileOutDevice(pcm, sampleRate, durationInSeconds, "wave.wav");

            Voice.MasterVolume = 120;
            Voice.Frequency = PitchTable.C3;
            Voice.Envelope = Envelopes.Attack(0, rate:TimingTable.S1);
            Voice.WaveForm = WaveForms.Triangle();
            
            device.Play();

            while (true)
            {
                if (!System.Console.KeyAvailable) continue;

                Voice.Frequency = ProcessKeyPress(System.Console.ReadKey().Key);

                // need a retrigger here instead
                Voice.Envelope = Envelopes.Attack(rate: 64);
            }
        }

        private static double ProcessKeyPress(ConsoleKey key) =>
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
                _ => Voice.Frequency
            };
    }
}


