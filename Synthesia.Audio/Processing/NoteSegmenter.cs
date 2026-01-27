using System;
using System.Collections.Generic;
using System.Text;
using Synthesia.Core.Models;

namespace Synthesia.Audio.Processing
{
    public class NoteSegmenter
    {
        private const double MinNoteDuration = 0.08; // seconds

        public List<NoteEvent> Segment(
            List<(double Time, double Frequency)> frames)
        {
            var notes = new List<NoteEvent>();

            PianoNote? currentNote = null;
            double noteStart = 0;

            foreach (var frame in frames)
            {
                var detectedNote = PianoNote.FromFrequency(frame.Frequency);

                if (detectedNote == null)
                    continue;

                if (currentNote == null)
                {
                    currentNote = detectedNote;
                    noteStart = frame.Time;
                    continue;
                }

                if (currentNote != null)
                {
                    var endTime = frames[^1].Time;
                    var duration = endTime - noteStart;

                    if (duration >= MinNoteDuration)
                    {
                        notes.Add(new NoteEvent(
                            currentNote,
                            noteStart,
                            endTime
                        ));
                    }
                }

            }

            return notes;
        }
    }
}
