using System.Text.Json;
using MediatR;

namespace ECinema.Movie.WebAPI.Application.Movies.Events;

public class MovieUpdatedEvent(Data.Movie movie) : INotification
{
    public Data.Movie Movie { get; set; } = movie;
}

public class MovieUpdatedEventHandler(ILogger<MovieUpdatedEventHandler> logger) : INotificationHandler<MovieUpdatedEvent>
{
    public Task Handle(MovieUpdatedEvent notification, CancellationToken cancellationToken)
    {
        // todo send rabbitmq message here
        logger.LogInformation($"Movie updated: {JsonSerializer.Serialize(notification.Movie)}");
        return Task.CompletedTask;
    }
}