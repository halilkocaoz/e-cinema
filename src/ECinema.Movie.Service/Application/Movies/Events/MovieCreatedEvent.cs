using System.Text.Json;
using MediatR;

namespace ECinema.Movie.Service.Application.Movies.Events;

public class MovieCreatedEvent(Data.Movie movie) : INotification
{
    public Data.Movie Movie { get; set; } = movie;
}

public class MovieCreatedEventHandler(ILogger<MovieCreatedEventHandler> logger) : INotificationHandler<MovieCreatedEvent>
{
    public Task Handle(MovieCreatedEvent notification, CancellationToken cancellationToken)
    {
        // todo send rabbitmq message here
        logger.LogInformation($"Movie created: {JsonSerializer.Serialize(notification.Movie)}");
        return Task.CompletedTask;
    }
}