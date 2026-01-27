using System;
using System.Collections.Generic;
using System.Text;
using Synthesia.Core.Models;
using Synthesia.Audio.Interfaces;
using Synthesia.Audio.Utilities;
using NAudio.Wave;

namespace Synthesia.Audio.Processing;

public class NaivePianoExtractor : IAudioNoteExtractor
{
    public async Task<AudioExtractionResult> ExtractAsync(
        string audioFilePath,
        CancellationToken cancellationToken = default)
    {
        return await Task.Run(() =>
        {
            return ProcessAudio(audioFilePath);
        }, cancellationToken);
    }

    private AudioExtractionResult ProcessAudio(string path)
    {
        using var reader = new AudioFileReader(path);

        int sampleRate = reader.WaveFormat.SampleRate;
        int windowSize = 2048;
        float[] buffer = new float[windowSize];

        var notes = new List<NoteEvent>();

        double currentTime = 0;
        PianoNote? currentNote = null;
        double noteStart = 0;

        while (reader.Read(buffer, 0, windowSize) > 0)
        {
            double frequency = DetectFrequency(buffer, sampleRate);

            if (frequency < 50)
                continue;

            PianoNote note = NoteFrequencyMap.FindClosestNote(frequency);

            if (note != currentNote)
            {
                if (currentNote != null)
                {
                    notes.Add(new NoteEvent(
                        currentNote!,
                        noteStart,
                        currentTime));
                }

                currentNote = note;
                noteStart = currentTime;
            }

            currentTime += (double)windowSize / sampleRate;
        }

        return new AudioExtractionResult(notes);
    }

    private double DetectFrequency(float[] buffer, int sampleRate)
    {
        int zeroCrossings = 0;

        for (int i = 1; i < buffer.Length; i++)
        {
            if (buffer[i - 1] <= 0 && buffer[i] > 0)
                zeroCrossings++;
        }

        return (zeroCrossings * sampleRate) / (double)buffer.Length;
    }
}
