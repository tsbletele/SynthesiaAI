using System;
using System.Collections.Generic;
using Synthesia.Core.Models;

namespace Synthesia.Audio.Processing
{
    public class NoteSegmenter
    {
        private const double MinNoteDuration = 0.10; // seconds
        private const int SameNoteTolerance = 1;     // MIDI steps

        public List<NoteEvent> Segment(
            List<(double TimeSeconds, double Frequency)> frames)
        {
            var results = new List<NoteEvent>();

            PianoNote? currentNote = null;
            double noteStartTime = 0;

            foreach (var frame in frames)
            {
                var detected = PianoNote.FromFrequency(frame.Frequency);

                if (detected == null)
                    continue;

                // First valid note
                if (currentNote == null)
                {
                    currentNote = detected;
                    noteStartTime = frame.TimeSeconds;
                    continue;
                }

                // Same note (with tolerance)
                if (Math.Abs(detected.MidiNumber - currentNote.MidiNumber) <= SameNoteTolerance)
                {
                    continue;
                }

                // Note change → close previous note
                double duration = frame.TimeSeconds - noteStartTime;

                if (duration >= MinNoteDuration)
                {
                    results.Add(new NoteEvent(
                        currentNote,
                        noteStartTime,
                        frame.TimeSeconds
                    ));
                }

                // Start new note
                currentNote = detected;
                noteStartTime = frame.TimeSeconds;
            }

            return results;
        }
    }
}
