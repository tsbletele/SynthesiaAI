using Synthesia.Audio.Processing;

var frames = new List<(double, double)>
{
    (0.00, 261.63),
    (0.05, 262.00),
    (0.10, 261.80),
    (0.40, 293.66),
    (0.80, 329.63),
};

var segmenter = new NoteSegmenter();
var notes = segmenter.Segment(frames);

foreach (var n in notes)
{
    Console.WriteLine($"{n.Note.Name} {n.StartTimeSeconds:F2}s → {n.EndTimeSeconds:F2}s");
}
