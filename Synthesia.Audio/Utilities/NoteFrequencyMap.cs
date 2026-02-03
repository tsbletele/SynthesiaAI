using Synthesia.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

//Computers don't understand musical notes, they understand frequencies.

namespace Synthesia.Audio.Utilities;

public static class NoteFrequencyMap
{
    public static readonly List<PianoNote> Notes =
    [
        new PianoNote("C3", 48, 130.81),
        new PianoNote("D3", 50, 146.83),
        new PianoNote("E3", 52, 164.81),
        new PianoNote("F3", 53, 174.61),
        new PianoNote("G3", 55, 196.00),
        new PianoNote("A3", 57, 220.00),
        new PianoNote("B3", 59, 246.94),
        new PianoNote("C4", 60, 261.63)
    ];

    public static PianoNote FindClosestNote(double frequency)
    {
        return Notes
            .OrderBy(n => Math.Abs(n.Frequency - frequency))
            .First();
    }
}

