using System;
using System.Collections.Generic;
using System.Text;
using Synthesia.Core.Models;

namespace Synthesia.Audio.Interfaces;

public class AudioExtractionResult
{
    public IReadOnlyList<NoteEvent> Notes { get; }

    public AudioExtractionResult(List<NoteEvent> notes)
    {
        Notes = notes;
    }
}