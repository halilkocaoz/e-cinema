using System.Text.Json;
using ECinema.Movie.Contracts.Messaging.Movie;
using MassTransit;

namespace ECinema.MovieHouse.Application.Messaging.Consumers.Movie;

public class MovieUpdatedConsumer : IConsumer<MovieUpdatedMessage>
{
    public Task Consume(ConsumeContext<MovieUpdatedMessage> context)
    {
        Console.WriteLine($"Movie updated consumer: {JsonSerializer.Serialize(context.Message)}");
        return Task.CompletedTask;
    }
}