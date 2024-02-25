using Locky.Keyholder.Core.Services;

namespace Locky.Keyholder.UseCases.BeltAggregate.Poll;

public static class PollHandler
{
    public static async Task<PollResponse> Handle(PollRequest request,IReadmodelLoader<PollResponse> readmodelLoader, CancellationToken cancellationToken)
    {
        var model = await readmodelLoader.LoadAsync(Guid.Empty, cancellationToken);
        return model;
    }
}