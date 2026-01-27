using System;
using System.Collections.Generic;
using System.Text;

namespace Synthesia.Core.Models
{
    public sealed record NoteEvent
        (
            PianoNote Note,
            double StartTimeSeconds,
            double EndTimeSeconds
        )
    {
       public double DurationSeconds => EndTimeSeconds - StartTimeSeconds;
    }
}
