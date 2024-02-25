using Locky.Keyholder.Core.BeltAggregate.Events;
using Marten.Events.Aggregation;

namespace Locky.Keyholder.UseCases.BeltAggregate.Poll;

public class PollProjection: SingleStreamProjection<PollResponse>
{
    public PollProjection()
    {
        DeleteEvent<Deactivated>();
    
        ProjectEvent<UnlockAccepted>(response =>
        {
            response.CanUnlock = true;
        });
        ProjectEvent<UnlockDenied>(response =>
        {
            response.CanUnlock = false;
        });
        ProjectEvent<Locked>(response =>
        {
            response.CanUnlock = false;
        });
        ProjectEvent<Unlocked>(response =>
        {
            response.CanUnlock = false;
        });
        ProjectEvent<ShockRequested>((response, @event) =>
        {
            response.ShouldShock = true;
            response.ShockLevel = @event.Intensity.Intensity;
            response.ShockDuration = @event.Intensity.Duration.TotalMilliseconds;
        });
        ProjectEvent<Shocked>(response =>
        {
            response.ShouldShock = false;
        });
        
    }
}