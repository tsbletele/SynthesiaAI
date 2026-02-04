using Synthesia.Audio.Models;
using Synthesia.Core.Models;

namespace Synthesia.Audio.Processing
{
    public sealed class NoteSegmenter
    {
        private const double MinNoteDuration = 0.08;

        public List<NoteEvent> Segment(List<DetectedFrame> frames)
        {
            var notes = new List<NoteEvent>();

            PianoNote? currentNote = null;
            double startTime = 0;

            foreach (var frame in frames)
            {
                var note = PianoNote.FromFrequency(frame.Frequency);
                if (note == null)
                    continue;

                if (currentNote == null)
                {
                    currentNote = note;
                    startTime = frame.TimeSeconds;
                    continue;
                }

                // ⚠ Compare MIDI, not string names
                if (note.MidiNumber != currentNote.MidiNumber)
                {
                    double duration = frame.TimeSeconds - startTime;

                    if (duration >= MinNoteDuration)
                    {
                        notes.Add(new NoteEvent(
                            currentNote,
                            startTime,
                            frame.TimeSeconds
                        ));
                    }

                    currentNote = note;
                    startTime = frame.TimeSeconds;
                }
            }

            return notes;
        }
    }
}
