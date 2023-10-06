using ECinema.Movie.Contracts.Messaging.Movie;
using ECinema.MovieHouse.Data;
using MassTransit;

namespace ECinema.MovieHouse.Application.Messaging.Consumers.Movie;

public class MovieUpdatedConsumer(ILogger<MovieUpdatedConsumer> logger, IMovieHouseRepository movieHouseRepository) : IConsumer<MovieUpdatedMessage>
{
    public Task Consume(ConsumeContext<MovieUpdatedMessage> context)
    {
        var interestedMovieHouses = movieHouseRepository.Get(x => context.Message.Genres.Intersect(x.InterestedGenres).Any());
        foreach (var interestedMovieHouse in interestedMovieHouses)
        {
            logger.LogInformation($"Movie house {interestedMovieHouse.Name} is informing about updated movie {context.Message.Name}");
        }
        return Task.CompletedTask;
    }
}