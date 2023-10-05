using System.Text.Json;
using ECinema.Movie.Contracts.Messaging.Movie;
using MassTransit;
using MediatR;

namespace ECinema.Movie.Application.Movies.Events;

public class MovieCreatedEvent(Data.Movie movie) : INotification
{
    public Data.Movie Movie { get; } = movie;
}

public class MovieCreatedEventHandler(ILogger<MovieCreatedEventHandler> logger, IPublishEndpoint publishEndpoint)
    : INotificationHandler<MovieCreatedEvent>
{
    public async Task Handle(MovieCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation($"{JsonSerializer.Serialize(notification.Movie)}");
        await publishEndpoint.Publish(new MovieCreatedMessage(notification.Movie.Name,
            notification.Movie.Base64Poster,
            notification.Movie.Cast,
            notification.Movie.Genres), cancellationToken);
    }
}