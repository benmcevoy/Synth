using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

using Synth.Frequency;

namespace Synth.Instrument.Monophonic
{
    public partial class MainWindow : Window
    {
        private readonly EightBitPcmStream _pcm;
        private readonly ExampleVoiceWithDelay _voice;
        private readonly DispatcherTimer _timer = new DispatcherTimer();

        // System.Windows whatever this is is tooooo slow.
        private readonly KeyboardHookEx _keyboard = new KeyboardHookEx();

        // the dispatcher is god damn slow.  Use the state as mediator. then we can update the state 
        // on a slow cycle and the samplerate can query the state as fast as it can, 
        // instead of having to go via the dispatcher, changing threads, etc.
        private readonly State _state = new State();

        public MainWindow()
        {
            InitializeComponent();

            const int sampleRate = 44100;

            _voice = new ExampleVoiceWithDelay(sampleRate);

            _pcm = new EightBitPcmStream(sampleRate, _voice);
            _timer.Interval = TimeSpan.FromMilliseconds(10);

            // this "stuff" should be in some class, perhaps the State itself.
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
                _state.Harmonic1 = Harmonic1.Value / 12d + Harmonic1Fine.Value;
                _state.Harmonic2 = Harmonic2.Value / 12d + Harmonic2Fine.Value;
                _state.PulseWidth = PW.Value;
                _state.LFO = LFO.Value;
                _state.IsLfoRoutedToHarmonic = ModHarmonic.IsChecked ?? false;
                _state.IsLfoRoutedToPulseWidth = ModPW.IsChecked ?? false;
                _state.IsLfoRingModulate = RingMod.IsChecked ?? false;
                _state.Delay = Delay.Value;
                _state.DelayFeedback = DelayFeedback.Value;
            };

            _keyboard.KeyDown += MainWindow_KeyDown;
            _keyboard.KeyUp += MainWindow_KeyUp;

            var device = new Devices.WaveOutDevice(_pcm, sampleRate, 1);

            // "pull" from state
            _voice.Attack = () => _state.Attack;
            _voice.Decay = () => _state.Decay;
            _voice.SustainLevel = () => _state.SustainLevel;
            _voice.Release = () => _state.Release;
            _voice.WaveForm = (t, f, w) => _state.WaveForm(t)(t, f, w);
            _voice.PulseWidth = (t, f) => _state.Modulate(_state.IsLfoRoutedToPulseWidth, t, _state.PulseWidth);

            _voice.Delay = () => _state.Delay;
            _voice.DelayFeedback = () => _state.DelayFeedback;

            device.Play();

            _timer.Start();
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            _state.NotePriority.Push(e.Key);
            _voice.Frequency = HandleKeyDown(e.Key);
            _voice.TriggerAttack();
        }

        private void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            // NotePriority is the strategy for handling multiple simultaneous key presses in a monophonic instrument
            // NotePriority should be in a class.  We have scattered the logic for "LastNotePressed" in these key press events :(
            // other NotePriority strategies include HighestNote and LowestNote 
            // pop the current key off

            // this implementation is whacky :)
            _state.NotePriority.TryPop(out var currentKey);

            if (currentKey != e.Key)
            {
                _voice.TriggerRelease();
                return;
            }

            // see if we had another key held down
            if (_state.NotePriority.TryPeek(out var key))
            {
                _voice.Frequency = HandleKeyDown(key);
                _voice.Envelope = Envelope.EnvelopeGenerator.Sustain(_voice.SustainLevel());
                return;
            }

            _voice.TriggerRelease();
        }

        private Func<double, double> HandleKeyDown(Key key) => key switch
        {
            Key.A => PitchTable.C3,
            Key.W => PitchTable.Db3,
            Key.S => PitchTable.D3,
            Key.E => PitchTable.Eb3,
            Key.D => PitchTable.E3,
            Key.F => PitchTable.F3,
            Key.T => PitchTable.Gb3,
            Key.G => PitchTable.G3,
            Key.Y => PitchTable.Ab3,
            Key.H => PitchTable.A3,
            Key.U => PitchTable.Bb3,
            Key.J => PitchTable.B3,
            Key.K => PitchTable.C4,
            _ => _voice.Frequency
        };
    }
}
