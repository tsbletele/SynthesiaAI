using Synthesia.Audio.Midi;
using Synthesia.Core.Models;

Console.WriteLine("Testing MIDI export...");

var notes = new List<NoteEvent>
{
    new NoteEvent(
        PianoNote.FromFrequency(261.63), // C4
        0.0,
        0.5
    ),
    new NoteEvent(
        PianoNote.FromFrequency(293.66), // D4
        0.5,
        1.0
    ),
    new NoteEvent(
        PianoNote.FromFrequency(329.63), // E4
        1.0,
        1.5
    )
};

var exporter = new MidiExporter();
var midi = exporter.Export(notes);

exporter.SaveToFile(midi, "test_output.mid");

Console.WriteLine("DONE → test_output.mid created");

var test = PianoNote.FromFrequency(261.63);
Console.WriteLine($"{test.Name} ({test.MidiNumber})");

