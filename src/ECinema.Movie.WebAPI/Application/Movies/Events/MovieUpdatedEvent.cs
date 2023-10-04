using MediatR;

namespace ECinema.Movie.WebAPI.Application.Movies.Events;

public class MovieUpdatedEvent(Data.Movie movie) : INotification
{
    public Data.Movie Movie { get; set; } = movie;
}