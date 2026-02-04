using Synthesia.Audio.Midi;
using Synthesia.Audio.Processing;
using Synthesia.Audio.Utilities;
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

var frames = new List<(double, double)>
{
    (0.00, 261.63),
    (0.05, 262.00),
    (0.10, 261.80),
    (0.40, 293.66),
    (0.80, 329.63),
};

var segmenter = new NoteSegmenter();
var not3s = segmenter.Segment(frames);

foreach (var n in not3s)
{
    Console.WriteLine($"{n.Note.Name} {n.StartTimeSeconds:F2}s → {n.EndTimeSeconds:F2}s");
}

var testFrequencies = new[]
{
    130.81, // C3
    261.63, // C4
    440.00, // A4
    493.88  // B4
};

foreach (var f in testFrequencies)
{
    var note = NoteFrequencyMap.FindClosestNote(f);
    Console.WriteLine($"{f} Hz → {note.Name} (MIDI {note.MidiNumber})");
}



