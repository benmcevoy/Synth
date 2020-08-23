using System;
using Synth.Envelope;
using Synth.Pitch;
using Synth.WaveForm;

namespace Synth
{
    public class Voice
    {
        // TODO: what a mess
        public Voice(int sampleRate)
        {
            SampleRate = sampleRate;
            WaveForms.SampleRate = sampleRate;
        }

        // Tempo - in BPM for metronome, and any speed dependant things

        /// <summary>
        /// The overall voice volume. 
        /// </summary>
        public Func<Amplitude> Volume = () => Amplitude.MaxValue;

        /// <summary>
        /// Produce a frequency value for the current time.
        /// </summary>
        public Func<Frequency> Frequency = () => PitchTable.A4;

        /// <summary>
        /// Produce a waveform from the current time, frequency and pulsewidth.
        /// </summary>
        public Func<Time, Frequency, double, Phase, Phasor> WaveForm = WaveForms.SineWave();

        /// <summary>
        /// Produce the next envelope value for the current time and current envelope value.
        /// </summary>
        public Func<Time, double> Envelope = EnvelopeGenerator.Mute();

        /// <summary>
        /// Produce a pulsewidth value from the current time and frequency. Pulse width can be used to modulate waveforms.
        /// </summary>
        public Func<Time, Frequency, double> PulseWidth = (t, f) => 0;

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
        public Func<Amplitude> SustainLevel = () => Amplitude.MaxValue;

        /// <summary>
        /// The sustain duration. 1.0 is equal to 1 second.  Sustain duration is only used when TriggerADSR is called.
        /// </summary>
        /// <remarks>Sustain duration is useful when gating is unavailable to trigger the release phase, such as in a sequencer or "programmed" music.</remarks>
        public Func<double> SustainDuration = () => 0.5;

        /// <summary>
        /// The release duration. 1.0 is equal to 1 second.
        /// </summary>
        public Func<double> Release = () => 0.1;

        /// <summary>
        /// Trigger the attack, decay, sustain phases of the envelope modulation.  
        /// </summary>
        /// <param name="t0">Start time</param>
        /// <returns></returns>
        public Func<Time, double> TriggerAttack()
            => Envelope = EnvelopeGenerator.TriggerAttack(VoiceOutput.Time, VoiceOutput.Envelope, Attack(), Decay(), SustainLevel());

        /// <summary>
        /// Trigger the release phase of the envelope modulation.
        /// </summary>
        /// <param name="t0">Start time</param>
        /// <returns></returns>
        public Func<Time, double> TriggerRelease()
            => Envelope = EnvelopeGenerator.TriggerRelease(VoiceOutput.Time, VoiceOutput.Envelope, SustainLevel(), Release());

        /// <summary>
        /// Trigger an automatic ADSR cycle of the envelope modulation.
        /// </summary>
        /// <param name="t0">Start time</param>
        /// <returns></returns>
        public Func<Time, double> TriggerADSR()
            => Envelope = EnvelopeGenerator.TriggerADSR(VoiceOutput.Time, VoiceOutput.Envelope, Attack(), Decay(), SustainLevel(), SustainDuration(), Release());

        public Func<Time, double> TriggerOn() => Envelope = EnvelopeGenerator.Sustain(SustainLevel());

        public Func<Time, double> TriggerOff() => Envelope = EnvelopeGenerator.Mute();

        /// <summary>
        /// Final output of this voice.
        /// </summary>
        /// <returns></returns>
        public virtual VoiceOutput Output(Time t)
            => VoiceOutput = new VoiceOutput(
                  (Amplitude)(Volume() * Envelope(t) * WaveForm(t, Frequency(), PulseWidth(t, Frequency()), VoiceOutput.Phasor).Amplitude / Amplitude.MaxValue),
                  Envelope(t),
                  t,
                  WaveForm(t, Frequency(), PulseWidth(t, Frequency()), VoiceOutput.Phasor));

        public VoiceOutput VoiceOutput = new VoiceOutput();

        public int SampleRate { get; }
    }
}