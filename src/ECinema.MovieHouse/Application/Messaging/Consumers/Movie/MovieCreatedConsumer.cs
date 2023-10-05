using System.Text.Json;
using ECinema.MovieHouse.Contracts.Messaging.Movie;
using MassTransit;

namespace ECinema.MovieHouse.Application.Messaging.Consumers.Movie;

public class MovieCreatedConsumer : IConsumer<MovieCreatedMessage>
{
    public Task Consume(ConsumeContext<MovieCreatedMessage> context)
    {
        Console.WriteLine($"Movie created consumer: {JsonSerializer.Serialize(context.Message)}");
        return Task.CompletedTask;
    }
}