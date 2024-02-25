namespace Locky.Keyholder.Core.ValueObjects;

public record ShockIntensity(int Intensity, TimeSpan Duration)
{
    
    private static int MinIntensity => 1;
    private static int MaxIntensity => 99;
    private static TimeSpan MinDuration => TimeSpan.FromMilliseconds(250);
    private static TimeSpan MaxDuration => TimeSpan.FromSeconds(5);
    
    public static ShockIntensity Default => None;
    public static ShockIntensity None => new(MinIntensity, MinDuration);
    public static ShockIntensity Max => new(MaxIntensity, MaxDuration);
    
    
    
    
    public int Intensity { get; init; } = ValidateIntensity(Intensity);
    public TimeSpan Duration { get; init; } = ValidateDuration(Duration);

    private static TimeSpan ValidateDuration(TimeSpan duration)
    {
        if (duration < MinDuration)
        {
            throw new ArgumentException($"Duration cannot be less than {MinDuration:g}");
        }
        
        if (duration > MaxDuration)
        {
            throw new ArgumentException($"Duration cannot be greater than {MaxDuration:g}");
        }
        return duration;
        
    }

    private static int ValidateIntensity(int intensity)
    {
        if (intensity < MinIntensity)
        {
            throw new ArgumentException($"Intensity cannot be less than {MinIntensity}");
        }
        
        if (intensity > MaxIntensity)
        {
            throw new ArgumentException($"Intensity cannot be greater than {MaxIntensity}");
        }
        return intensity;
    }
}
