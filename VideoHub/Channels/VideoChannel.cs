using System.Threading.Channels;
using VideoHub.Model;

namespace VideoHub.Channels;

public class VideoChannel
{
    private readonly Channel<ProcessVideo> _channel = Channel.CreateUnbounded<ProcessVideo>();

    public ValueTask PublishAsync(ProcessVideo request) => _channel.Writer.WriteAsync(request);

    public IAsyncEnumerable<ProcessVideo> SubscribeAsync(CancellationToken cancellationToken)
        => _channel.Reader.ReadAllAsync(cancellationToken);
}

public class FullHdVideoChannel : VideoChannel
{
    public VideoChannel Channel { get; } = new();
}

public class UltraHdVideoChannel : VideoChannel
{
    public VideoChannel Channel { get; } = new();
}