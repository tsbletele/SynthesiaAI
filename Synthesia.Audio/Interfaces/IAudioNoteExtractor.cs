using System;
using System.Collections.Generic;
using System.Text;
using Synthesia.Core.Models;

namespace Synthesia.Audio.Interfaces;

public interface IAudioNoteExtractor
{
    Task<AudioExtractionResult> ExtractAsync(
        string audioFilePath,
        CancellationToken cancellationToken = default
    );
}
