using ECinema.Movie.Contracts.Messaging.Movie;
using ECinema.MovieHouse.Data;
using MassTransit;

namespace ECinema.MovieHouse.Application.Messaging.Consumers.Movie;

public class MovieCreatedConsumer(ILogger<MovieCreatedConsumer> logger, IMovieHouseRepository movieHouseRepository) : IConsumer<MovieCreatedMessage>
{
    public Task Consume(ConsumeContext<MovieCreatedMessage> context)
    {
        var interestedMovieHouses = movieHouseRepository.Get(x => context.Message.Genres.Intersect(x.InterestedGenres).Any());
        foreach (var interestedMovieHouse in interestedMovieHouses)
        {
            logger.LogInformation($"Movie house {interestedMovieHouse.Name} is informing about created movie {context.Message.Name}");
        }
        return Task.CompletedTask;
    }
}