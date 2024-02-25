namespace Locky.Keyholder.Core.ValueObjects;

public record PullInterval(TimeSpan Interval)
{
    public static PullInterval Default => new PullInterval(TimeSpan.FromSeconds(15));
    public static PullInterval Fast => new PullInterval(TimeSpan.FromSeconds(1));
    public static PullInterval Slow => new PullInterval(TimeSpan.FromSeconds(60));
    
    private static TimeSpan MinInterval => TimeSpan.FromMilliseconds(500);
    private static TimeSpan MaxInterval => TimeSpan.FromMinutes(5);
    
    private static TimeSpan ValidateInterval(TimeSpan interval)
    {
        if (interval < MinInterval)
        {
            return MinInterval;
        }

        if (interval > MaxInterval)
        {
            return MaxInterval;
        }

        return interval;
    }
}