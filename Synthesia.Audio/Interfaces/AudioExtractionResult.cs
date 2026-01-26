using System;
using System.Collections.Generic;
using System.Text;
using Synthesia.Core.Models;

namespace Synthesia.Audio.Interfaces;

public sealed record AudioExtractionResult(
    IReadOnlyList<NoteEvent> Notes,
    double DurationSeconds,
    string SourceFile
);