﻿using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Synth.Instrument.Monophonic
{
    public partial class MainWindow : Window
    {
        private readonly EightBitPcmStream _pcm;
        private readonly Voice _voice = new Voice();
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

            _pcm = new EightBitPcmStream(sampleRate, _voice);
            _timer.Interval = TimeSpan.FromMilliseconds(20);

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
                _state.Harmonic1 = Harmonic1.Value / 12d;
                _state.Harmonic2 = Harmonic2.Value / 12d;
                _state.PulseWidth = PW.Value;
            };

            _keyboard.KeyDown += MainWindow_KeyDown;
            _keyboard.KeyUp += MainWindow_KeyUp;

            var device = new Devices.WaveOutDevice(_pcm, sampleRate, 1);

            // "pull" from state
            _voice.Attack = () => _state.Attack;
            _voice.Decay = () => _state.Decay;
            _voice.SustainLevel = () => _state.SustainLevel;
            _voice.Release = () => _state.Release;
            _voice.WaveForm = (t, f, w) => _state.WaveForm()(t, f, w);
            _voice.PulseWidth = (t, f) => _state.PulseWidth;

            device.Play();

            _timer.Start();
        }

        private void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            // NotePriority is the strategy for handling multiple simultaneous key presses in a monophonic instrument
            // NotePriority should be in a class.  We have scatted the logic for "LastNotePressed" in these key press events :(
            // other NotePriority strategies include HighestNote and LowestNote 
            // pop the current key off
            _state.NotePriority.TryPop(out var key1);

            // see if we have another key held down
            if (_state.NotePriority.TryPeek(out var key2))
            {
                HandleKeyDown(key2);
                return;
            }

            TriggerVoice(_voice, _voice.Frequency, true);
        }

        private bool TriggerVoice(Voice voice, Func<double, double> f, bool release = false)
        {
            if (release)
            {
                voice.TriggerRelease(_pcm.Time);

                return true;
            }

            voice.Frequency = f;
            voice.TriggerAttack(_pcm.Time);

            return true;
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (_state.NotePriority.TryPeek(out var test))
            {
                // if it's a new key then push onto the stack
                if (test != e.Key)
                    _state.NotePriority.Push(e.Key);
            }

            HandleKeyDown(e.Key);
        }

        private bool HandleKeyDown(Key key) => key switch
        {
            Key.A => TriggerVoice(_voice, PitchTable.C3),
            Key.W => TriggerVoice(_voice, PitchTable.Db3),
            Key.S => TriggerVoice(_voice, PitchTable.D3),
            Key.E => TriggerVoice(_voice, PitchTable.Eb3),
            Key.D => TriggerVoice(_voice, PitchTable.E3),
            Key.F => TriggerVoice(_voice, PitchTable.F3),
            Key.T => TriggerVoice(_voice, PitchTable.Gb3),
            Key.G => TriggerVoice(_voice, PitchTable.G3),
            Key.Y => TriggerVoice(_voice, PitchTable.Ab3),
            Key.H => TriggerVoice(_voice, PitchTable.A3),
            Key.U => TriggerVoice(_voice, PitchTable.Bb3),
            Key.J => TriggerVoice(_voice, PitchTable.B3),
            Key.K => TriggerVoice(_voice, PitchTable.C4),
            _ => false
        };
    }
}