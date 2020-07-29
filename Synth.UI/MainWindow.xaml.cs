using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Synth.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly EightBitPcmStream _pcm;
        private readonly Voice _voice = new Voice();
        private readonly DispatcherTimer _timer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();

            const int sampleRate = 44100;

            _pcm = new EightBitPcmStream(sampleRate, _voice);

            Mixer.Volume = 255;

            _voice.Volume = 255;
            _voice.Frequency = PitchTable.C3;
            _voice.WaveForm = WaveForm.Sawtooth();
            _voice.Attack = 1;
            _voice.Decay = 1;
            _voice.Sustain = 128;
            _voice.Release = 1;

            _timer.Tick += new EventHandler(Draw);
            _timer.Interval = TimeSpan.FromMilliseconds(20);
            _timer.Start();

            KeyDown += MainWindow_KeyDown;
            KeyUp += MainWindow_KeyUp;

            var device = new Devices.WaveOutDevice(_pcm, sampleRate, 1);

            device.Play();
        }

        private int _drawX = 0;
        private void Draw(object sender, EventArgs e)
        {
            if (_drawX > 600)
            {
                _drawX = 0;
                Output.Children.Clear();
            }

            var l = new Line
            {
                Stroke = Brushes.Blue,
                StrokeThickness = 0.8,
                X1 = _drawX,
                X2 = _drawX,
                Y2 = 300 - _voice.Output(_pcm.Time),
                Y1 = 300
            };

            Output.Children.Add(l);

            _drawX++;
        }

        private void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            TriggerVoice(_voice, _voice.Frequency, false);
        }

        private void TriggerVoice(Voice voice, Func<double, double> f, bool attack)
        {
            if (attack)
            {
                voice.Frequency = f;
                voice.TriggerAttack(_pcm.Time);
            }
            else voice.Envelope = voice.TriggerRelease(_pcm.Time);
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.IsRepeat) return;

            switch (e.Key)
            {
                case Key.A: TriggerVoice(_voice, PitchTable.C3, true); return;
                case Key.W: TriggerVoice(_voice, PitchTable.Db3, true); return;
                case Key.S: TriggerVoice(_voice, PitchTable.D3, true); return;
                case Key.E: TriggerVoice(_voice, PitchTable.Eb3, true); return;
                case Key.D: TriggerVoice(_voice, PitchTable.E3, true); return;
                case Key.F: TriggerVoice(_voice, PitchTable.F3, true); return;
                case Key.T: TriggerVoice(_voice, PitchTable.Gb3, true); return;
                case Key.G: TriggerVoice(_voice, PitchTable.G3, true); return;
                case Key.Y: TriggerVoice(_voice, PitchTable.Ab3, true); return;
                case Key.H: TriggerVoice(_voice, PitchTable.A3, true); return;
                case Key.U: TriggerVoice(_voice, PitchTable.Bb3, true); return;
                case Key.J: TriggerVoice(_voice, PitchTable.B3, true); return;
                case Key.K: TriggerVoice(_voice, PitchTable.C4, true); return;

                default: return;
            }
        }
    }
}

