using System;
using System.Collections.Generic;
using System.Text;

namespace Synthesia.Core.Models
{
    public class PianoNote
    {
        public string Name { get; }
        public int MidiNumber { get; }
        public double Frequency { get; }

        private PianoNote(string name, int midiNumber, double frequency)
        {
            Name = name;
            MidiNumber = midiNumber;
            Frequency = frequency;
        }

        // 🎵 Stable frequency → note conversion
        public static PianoNote? FromFrequency(double frequency)
        {
            if (frequency <= 0)
                return null;

            // 🔑 FLOOR + bias = stable pitch detection
            double midiExact =
                69 + 12 * Math.Log2(frequency / 440.0);

            int midi = (int)Math.Floor(midiExact + 0.5);

            // Piano range guard (A0 = 21, C8 = 108)
            if (midi < 21 || midi > 108)
                return null;

            string noteName = MidiToNoteName(midi);
            double exactFrequency = MidiToFrequency(midi);

            return new PianoNote(noteName, midi, exactFrequency);
        }

        public static PianoNote FromMidi(int extractedMidi)
        {
            // Piano keyboard range: A0 (21) → C8 (108)
            if (extractedMidi < 21 || extractedMidi > 108)
                throw new ArgumentOutOfRangeException(
                    nameof(extractedMidi),
                    "MIDI note must be between 21 (A0) and 108 (C8)"
                );

            string noteName = MidiToNoteName(extractedMidi);
            double exactFrequency = MidiToFrequency(extractedMidi);

            return new PianoNote(noteName, extractedMidi, exactFrequency);
        }

        private static string MidiToNoteName(int midi)
        {
            string[] notes =
            {
                "C", "C#", "D", "D#", "E",
                "F", "F#", "G", "G#", "A", "A#", "B"
            };

            int octave = (midi / 12) - 1;
            string note = notes[midi % 12];

            return $"{note}{octave}";
        }

        private static double MidiToFrequency(int midi)
        {
            return 440.0 * Math.Pow(2, (midi - 69) / 12.0);
        }
    }
}
