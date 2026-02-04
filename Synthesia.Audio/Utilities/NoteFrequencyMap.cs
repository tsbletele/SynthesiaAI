using Synthesia.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

//Computers don't understand musical notes, they understand frequencies.

namespace Synthesia.Audio.Utilities;

public static class NoteFrequencyMap
{
    public static readonly IReadOnlyList<PianoNote> Notes =
        BuildPianoRange();

    private static List<PianoNote> BuildPianoRange()
    {
        var notes = new List<PianoNote>();

        for (int midi = 21; midi <= 108; midi++)
        {
            notes.Add(PianoNote.FromMidi(midi));
        }

        return notes;
    }

    public static PianoNote FindClosestNote(double frequency)
    {
        return Notes
            .OrderBy(n => Math.Abs(n.Frequency - frequency))
            .First();
    }
}

