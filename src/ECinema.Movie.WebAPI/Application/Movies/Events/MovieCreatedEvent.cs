using MediatR;

namespace ECinema.Movie.WebAPI.Application.Movies.Events;

public class MovieCreatedEvent(Data.Movie movie) : INotification
{
    public Data.Movie Movie { get; set; } = movie;
}