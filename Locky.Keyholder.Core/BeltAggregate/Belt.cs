using Locky.Keyholder.Core.BeltAggregate.Events;
using Locky.Keyholder.Core.ValueObjects;

namespace Locky.Keyholder.Core.BeltAggregate;

public class Belt: AggregateBase
{
    private DateTimeOffset _lastCheckIn = DateTimeOffset.MinValue;
    private PullInterval _interval = PullInterval.Default;
    private ShockIntensity _intensity = ShockIntensity.Default;
    private DateTimeOffset _nextAllowedUnlockingTime = DateTimeOffset.MinValue;
    
    private bool _shockRequested = false;
    
    private bool _isRegistered = false;
    private bool _isLocked = false;
    private bool _isDeactivated = false;
    
    
    public void Register()
    {
        if (_isRegistered)
        {
            throw new InvalidOperationException("Belt is already registered");
        }
        
        var @event = new Registered();
        AddUncommittedEvent(@event);
        Apply(@event);
    }

    public void SetInterval(PullInterval interval)
    {
        if (!_isRegistered || _isDeactivated)
        {
            throw new InvalidOperationException("Belt is not registered or is deactivated");
        }

        var @event = new IntervalSet(interval);

    }
    
    public void RequestSock(ShockIntensity intensity)
    {
        if (!_isRegistered || _isDeactivated)
        {
            throw new InvalidOperationException("Belt is not registered or is deactivated");
        }
        
        // if shock already requested, throw exception
        if (_shockRequested)
        {
            throw new InvalidOperationException("Shock already requested");
        }
        
        var @event = new ShockRequested(intensity);
        AddUncommittedEvent(@event);
        Apply(@event);
    }
    
    public void Lock()
    {
        if (!_isRegistered || _isDeactivated)
        {
            throw new InvalidOperationException("Belt is not registered or is deactivated");
        }
        
        if (_isLocked)
        {
            throw new InvalidOperationException("Belt is already locked");
        }
        
        var @event = new Locked();
        AddUncommittedEvent(@event);
        Apply(@event);
    }
    
    public void Unlock(DateTimeOffset currentTime)
    {
        if (!_isRegistered || _isDeactivated)
        {
            throw new InvalidOperationException("Belt is not registered or is deactivated");
        }
        
        if (!_isLocked)
        {
            throw new InvalidOperationException("Belt is not locked");
        }
        
        if (_nextAllowedUnlockingTime > currentTime)
        {
            throw new InvalidOperationException("Belt cannot be unlocked yet");
        }
        
        
        var @event = new Unlocked();
        AddUncommittedEvent(@event);
        Apply(@event);
    }
    
    public void Deactivate()
    {
        if (!_isRegistered || _isDeactivated)
        {
            throw new InvalidOperationException("Belt is not registered or is deactivated");
        }
        
        var @event = new Deactivated();
        AddUncommittedEvent(@event);
        Apply(@event);
    }
    
    public void Shock()
    {
        if (!_isRegistered || _isDeactivated)
        {
            throw new InvalidOperationException("Belt is not registered or is deactivated");
        }
        
        if (!_shockRequested)
        {
            throw new InvalidOperationException("Shock not requested");
        }
        
        var @event = new Shocked();
        AddUncommittedEvent(@event);
        Apply(@event);
    }

    public void Apply(Registered @event)
    {
        _isRegistered = true;
    }
    
    public void Apply(IntervalSet @event)
    {
        _interval = @event.Interval;
    }
    
    public void Apply(ShockRequested @event)
    {
        _intensity = @event.Intensity;
        _shockRequested = true;
    }
    
    public void Apply(Locked @event)
    {
        _isLocked = true;
    }
    
    public void Apply(Unlocked @event)
    {
        _isLocked = false;
    }
    
    public void Apply(Deactivated @event)
    {
        _isDeactivated = true;
    }
    
    public void Apply(Shocked @event)
    {
        _shockRequested = false;
    }
    
}