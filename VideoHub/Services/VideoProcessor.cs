using System.Diagnostics;
using VideoHub.Model;

namespace VideoHub.Services;

public class VideoProcessor
{
    private static readonly Random Random = new();
    private readonly ILogger<VideoProcessor> _logger;

    public VideoProcessor(ILogger<VideoProcessor> logger)
    {
        _logger = logger;
    }

    public async Task ProcessAsync(ProcessVideo request)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        _logger.LogInformation($"Started processing a {request.Quality} video with path {request.Path}.");
        await Task.Delay(TimeSpan.FromSeconds(Random.Next(3, 7)));
        stopwatch.Stop();
        _logger.LogInformation("Processing completed.");
    }
}