namespace Locky.Keyholder.UseCases.BeltAggregate.Poll;

public class PollResponse
{
    public bool ShouldShock { get; set; }
    public bool CanUnlock { get; set; }
    public int ShockLevel { get; set; }
    public double ShockDuration { get; set; }
};