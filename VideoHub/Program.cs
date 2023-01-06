using VideoHub.Channels;
using VideoHub.Model;
using VideoHub.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddSingleton<VideoProcessor>()
    .AddSingleton<FullHdVideoChannel>()
    .AddSingleton<UltraHdVideoChannel>()
    .AddHostedService<FullHdBackgroundVideoProcessor>()
    .AddHostedService<UltraHdBackgroundVideoProcessor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "VideoHub");

app.MapPost("/videos", async (ProcessVideo request, FullHdVideoChannel fullHdChannel,
    UltraHdVideoChannel ultraHdChannel) =>
{
    await fullHdChannel.Channel.PublishAsync(request);
    await ultraHdChannel.Channel.PublishAsync(request);
    return Results.Accepted();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();