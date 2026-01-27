using Synthesia.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

//Computers don't understand musical notes, they understand frequencies.

namespace Synthesia.Audio.Utilities;

public static class NoteFrequencyMap
{
    public static readonly List<PianoNote> Notes =
    [
        new PianoNote("C3", 130.81),
        new PianoNote("D3", 146.83),
        new PianoNote("E3", 164.81),
        new PianoNote("F3", 174.61),
        new PianoNote("G3", 196.00),
        new PianoNote("A3", 220.00),
        new PianoNote("B3", 246.94),
        new PianoNote("C4", 261.63)
    ];

    public static PianoNote FindClosestNote(double frequency)
    {
        return Notes
            .OrderBy(n => Math.Abs(n.Frequency - frequency))
            .First();
    }
}

