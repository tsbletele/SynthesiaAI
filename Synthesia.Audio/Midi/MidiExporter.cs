using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using coreNoteEvent = Synthesia.Core.Models.NoteEvent;

namespace Synthesia.Audio.Midi
{
    public class MidiExporter
    {
        public MidiFile Export(
            List<coreNoteEvent> notes,
            int tempoBpm = 120)
        {
            var trackChunk = new TrackChunk();
            var tempoMap = TempoMap.Default;

            using (var notesManager = trackChunk.ManageNotes())
            {
                foreach (var noteEvent in notes)
                {
                    // 🔒 HARD STABILIZATION STEP
                    int midi = noteEvent.Note.MidiNumber;

                    // Guard AGAIN (export-level safety)
                    if (midi < 21 || midi > 108)
                        continue;

                    var length = TimeConverter.ConvertFrom(
                        MusicalTimeSpan.FromDouble(noteEvent.DurationSeconds),
                        tempoMap);

                    var startTime = TimeConverter.ConvertFrom(
                        MusicalTimeSpan.FromDouble(noteEvent.StartTimeSeconds),
                        tempoMap);

                    var midiNote = new Melanchall.DryWetMidi.Interaction.Note(
                        (SevenBitNumber)midi,
                        length)
                    {
                        Time = startTime,
                        Velocity = (SevenBitNumber)80 // fixed velocity for now
                    };

                    notesManager.Objects.Add(midiNote);
                }
            }

            return new MidiFile(trackChunk);
        }

        public void SaveToFile(MidiFile midiFile, string path)
        {
            midiFile.Write(path);
        }
    }
}
