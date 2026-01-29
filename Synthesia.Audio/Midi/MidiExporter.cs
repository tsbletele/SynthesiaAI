using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using CoreNoteEvent = Synthesia.Core.Models.NoteEvent;

namespace Synthesia.Audio.Midi
{
    public class MidiExporter
    {
        private const int TicksPerQuarterNote = 480;
        private const int TempoBpm = 120;

        public MidiFile Export(List<CoreNoteEvent> notes)
        {
            var trackChunk = new TrackChunk();

            // Set tempo
            var tempoEvent = new SetTempoEvent(
                (int)(60000000 / TempoBpm));

            trackChunk.Events.Add(tempoEvent);

            foreach (var note in notes)
            {
                int startTicks = SecondsToTicks(note.StartTimeSeconds);
                int durationTicks = SecondsToTicks(note.DurationSeconds);

                var noteOn = new NoteOnEvent(
                    (SevenBitNumber)note.Note.MidiNumber,
                    (SevenBitNumber)100)
                {
                    DeltaTime = startTicks
                };

                var noteOff = new NoteOffEvent(
                    (SevenBitNumber)note.Note.MidiNumber,
                    (SevenBitNumber)0)
                {
                    DeltaTime = durationTicks
                };

                trackChunk.Events.Add(noteOn);
                trackChunk.Events.Add(noteOff);
            }

            return new MidiFile(trackChunk)
            {
                TimeDivision = new TicksPerQuarterNoteTimeDivision(
                    TicksPerQuarterNote)
            };
        }

        private int SecondsToTicks(double seconds)
        {
            double beatsPerSecond = TempoBpm / 60.0;
            double ticksPerSecond = beatsPerSecond * TicksPerQuarterNote;
            return (int)(seconds * ticksPerSecond);
        }

        public void SaveToFile(MidiFile midiFile, string path)
        {
            midiFile.Write(path);
        }
    }
}
