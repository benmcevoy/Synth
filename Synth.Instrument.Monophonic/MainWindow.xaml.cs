using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Synth.Instrument.Monophonic
{


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly EightBitPcmStream _pcm;
        private readonly Voice _voice = new Voice();
        private readonly DispatcherTimer _timer = new DispatcherTimer();

        // System.Windows whatever this is is tooooo slow.
        private readonly KeyboardHookEx _keyboard = new KeyboardHookEx();

        // the dispatcher is god damn slow.  Use the state as mediator. then we can update the state 
        // on a slow cycle (50Hz) and the samplerate can query the state as fast as it can, 
        // instead of having to go via the dispatcher.
        private readonly State _state = new State();

        public MainWindow()
        {
            InitializeComponent();

            const int sampleRate = 44100;

            _pcm = new EightBitPcmStream(sampleRate, _voice);
            _timer.Interval = TimeSpan.FromTicks(500);

            _timer.Tick += (s, e) =>
            {
                // "push" into state
                _state.Attack = A1.Value / 10d;
                _state.Decay = D1.Value / 10d;
                _state.SustainLevel = (byte)S1.Value;
                _state.Release = R1.Value / 10d;
                _state.WF1 = WF1.Text;
                _state.Volume1 = (byte)Volume1.Value;
                _state.WF2 = WF2.Text;
                _state.Volume2 = (byte)Volume2.Value;
                _state.Harmonic1 = Harmonic1.Value;
                _state.Harmonic2 = Harmonic2.Value;
                _state.PulseWidth = PW.Value;
            };

            _keyboard.KeyDown += MainWindow_KeyDown;
            _keyboard.KeyUp += MainWindow_KeyUp;

            var device = new Devices.WaveOutDevice(_pcm, sampleRate, 1);

            device.Play();

            Loaded += MainWindow_Loaded;

            // "pull" from state
            _voice.Attack = () => _state.Attack;
            _voice.Decay = () => _state.Decay;
            _voice.SustainLevel = () => _state.SustainLevel;
            _voice.Release = () => _state.Release;
            _voice.WaveForm = (t, f, w) => _state.WaveForm()(t, f, w);
            _voice.PulseWidth = (t, f) => _state.PulseWidth;
        }

        private void MainWindow_Loaded(object sender, EventArgs e)
        {
            Loaded -= MainWindow_Loaded;

            _timer.Start();
        }

        private void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            _state.NotePriority.TryPop(out var key1);

            if (_state.NotePriority.TryPeek(out var key2))
            {
                HandleKeyDown(key2);
                return;
            }

            TriggerVoice(_voice, _voice.Frequency, true);
        }

        private void TriggerVoice(Voice voice, Func<double, double> f, bool release = false)
        {
            if (!release)
            {
                voice.Frequency = f;
                voice.TriggerAttack(_pcm.Time);
            }
            else voice.TriggerRelease(_pcm.Time);
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (_state.NotePriority.TryPeek(out var test))
            {
                if (test == e.Key) return;
            }

            _state.NotePriority.Push(e.Key);

            HandleKeyDown(e.Key);
        }

        private void HandleKeyDown(Key key)
        {
            switch (key)
            {
                case Key.A: TriggerVoice(_voice, PitchTable.C3); return;
                case Key.W: TriggerVoice(_voice, PitchTable.Db3); return;
                case Key.S: TriggerVoice(_voice, PitchTable.D3); return;
                case Key.E: TriggerVoice(_voice, PitchTable.Eb3); return;
                case Key.D: TriggerVoice(_voice, PitchTable.E3); return;
                case Key.F: TriggerVoice(_voice, PitchTable.F3); return;
                case Key.T: TriggerVoice(_voice, PitchTable.Gb3); return;
                case Key.G: TriggerVoice(_voice, PitchTable.G3); return;
                case Key.Y: TriggerVoice(_voice, PitchTable.Ab3); return;
                case Key.H: TriggerVoice(_voice, PitchTable.A3); return;
                case Key.U: TriggerVoice(_voice, PitchTable.Bb3); return;
                case Key.J: TriggerVoice(_voice, PitchTable.B3); return;
                case Key.K: TriggerVoice(_voice, PitchTable.C4); return;

                default: return;
            }
        }
    }
}
