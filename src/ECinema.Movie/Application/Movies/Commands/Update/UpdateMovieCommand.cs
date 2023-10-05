using MediatR;

namespace ECinema.Movie.Application.Movies.Commands.Update;

public sealed record UpdateMovieCommand(string Id, string Name, string Base64Poster, List<string> Cast, List<string> Genres) : IRequest<bool>;