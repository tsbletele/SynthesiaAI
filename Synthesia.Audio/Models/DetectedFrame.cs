namespace Synthesia.Audio.Models
{
    public sealed class DetectedFrame
    {
        public double TimeSeconds { get; }
        public double Frequency { get; }

        public DetectedFrame(double timeSeconds, double frequency)
        {
            TimeSeconds = timeSeconds;
            Frequency = frequency;
        }
    }
}
