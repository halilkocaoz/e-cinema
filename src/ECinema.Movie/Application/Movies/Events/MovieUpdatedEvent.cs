using System.Text.Json;
using ECinema.Movie.Contracts.Messaging.Movie;
using MassTransit;
using MediatR;

namespace ECinema.Movie.Application.Movies.Events;

public class MovieUpdatedEvent(Data.Movie movie) : INotification
{
    public Data.Movie Movie { get; } = movie;
}

public class MovieUpdatedEventHandler(ILogger<MovieUpdatedEventHandler> logger, IPublishEndpoint publishEndpoint) : INotificationHandler<MovieUpdatedEvent>
{
    public async Task Handle(MovieUpdatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation($"{JsonSerializer.Serialize(notification.Movie)}");
        await publishEndpoint.Publish(new MovieUpdatedMessage(notification.Movie.Name,
            notification.Movie.Base64Poster,
            notification.Movie.Cast,
            notification.Movie.Genres), cancellationToken);
    }
}