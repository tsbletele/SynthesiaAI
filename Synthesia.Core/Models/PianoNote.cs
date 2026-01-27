using System;
using System.Collections.Generic;
using System.Text;

namespace Synthesia.Core.Models;

public class PianoNote
{
    public string Name { get; }
    public double Frequency { get; }

    public PianoNote(string name, double frequency)
    {
        Name = name;
        Frequency = frequency;
    }

    public override string ToString() => Name;
}