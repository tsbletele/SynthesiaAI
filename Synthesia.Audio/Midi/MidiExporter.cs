using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using coreNoteEvent = Synthesia.Core.Models.NoteEvent;

namespace Synthesia.Audio.Export;

public sealed class MidiExporter
{
    private const int TicksPerQuarterNote = 480;
    private const int TempoBpm = 120;

    public void Export(List<coreNoteEvent> notes, string filePath)
    {
        var tempoMap = TempoMap.Create(
            Tempo.FromBeatsPerMinute(TempoBpm));

        var trackChunk = new TrackChunk();
        var midiFile = new MidiFile(trackChunk)
        {
            TimeDivision = new TicksPerQuarterNoteTimeDivision(
                TicksPerQuarterNote)
        };

        var manager = trackChunk.ManageNotes();

        foreach (var note in notes)
        {
            long startTicks = SecondsToTicks(note.StartTimeSeconds);
            long lengthTicks = SecondsToTicks(
                note.EndTimeSeconds - note.StartTimeSeconds);

            var midiNote = new Melanchall.DryWetMidi.Interaction.Note(
                (SevenBitNumber)note.Note.MidiNumber,
                lengthTicks)
            {
                Time = startTicks
            };

            manager.Objects.Add(midiNote);
        }

        midiFile.Write(filePath);
    }

    private static long SecondsToTicks(double seconds)
    {
        return (long)(
            seconds * TempoBpm * TicksPerQuarterNote / 60.0
        );
    }
}
