using System;
using static System.Byte;

namespace Synth
{
    public class Voice
    {
        /// <summary>
        /// The overall voice volume. 
        /// </summary>
        public Func<byte> Volume = () => 255;

        /// <summary>
        /// Produce a frequency value for the current time.
        /// </summary>
        public Func<double, double> Frequency = PitchTable.A4;

        /// <summary>
        /// Produce a waveform from the current time, frequency and pulsewidth.
        /// </summary>
        public Func<double, double, double, byte> WaveForm = Synth.WaveForm.SineWave();

        /// <summary>
        /// Produce the next volume value for the current time and current volume.
        /// </summary>
        public Func<double, byte, byte> Envelope = Synth.Envelope.Mute();

        /// <summary>
        /// Produce a pulsewidth value from the current time and frequency. Pulse width can be used to modulate waveforms.
        /// </summary>
        public Func<double, double, double> PulseWidth = (t, f) => 0;

        /// <summary>
        /// The attack duration. 1.0 is equal to 1 second.
        /// </summary>
        public Func<double> Attack = () => 0.1;

        /// <summary>
        /// The decay duration. 1.0 is equal to 1 second.
        /// </summary>
        public Func<double> Decay = () => 0.1;

        /// <summary>
        /// The sustain volume level.
        /// </summary>
        public Func<byte> SustainLevel = () => 255;

        /// <summary>
        /// The sustain duration. 1.0 is equal to 1 second.  Sustain duration is only used when TriggerADSR is called.
        /// </summary>
        /// <remarks>Sustain duration is useful when gating is unavailable to trigger the release phase, such as in a sequencer or "programmed" music.</remarks>
        public Func<double> SustainDuration = () => 1;

        /// <summary>
        /// The release duration. 1.0 is equal to 1 second.
        /// </summary>
        public Func<double> Release = () => 0.1;

        /// <summary>
        /// Trigger the attack, decay, sustain phases of the envelope modulation.  
        /// </summary>
        /// <param name="t0">Start time</param>
        /// <returns></returns>
        public Func<double, byte, byte> TriggerAttack(double t0)
            => Envelope = (t, v) => Synth.Envelope.TriggerAttack(t0, Attack(), Decay(), SustainLevel())(t, v);

        /// <summary>
        /// Trigger the release phase of the envelope modulation.
        /// </summary>
        /// <param name="t0">Start time</param>
        /// <returns></returns>
        public Func<double, byte, byte> TriggerRelease(double t0)
            => Envelope = (t, v) => Synth.Envelope.TriggerRelease(t0, SustainLevel(), Release())(t, v);

        /// <summary>
        /// Trigger an automatic ADSR cycle of the envelope modulation.
        /// </summary>
        /// <param name="t0">Start time</param>
        /// <returns></returns>
        public Func<double, byte, byte> TriggerADSR(double t0)
            => Envelope = (t, v) => Synth.Envelope.TriggerADSR(t0, t, Attack(), Decay(), SustainLevel(), SustainDuration(), Release())(t, v);

        /// <summary>
        /// Final output of this voice.
        /// </summary>
        /// <returns></returns>
        public byte Output(double t)
            => Convert.ToByte(Volume() * Envelope(t, WaveForm(t, Frequency(t), PulseWidth(t, Frequency(t)))) / MaxValue);
    }
}

