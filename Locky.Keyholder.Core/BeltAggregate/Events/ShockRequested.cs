using Locky.Keyholder.Core.ValueObjects;

namespace Locky.Keyholder.Core.BeltAggregate.Events;

public record ShockRequested(ShockIntensity Intensity);