using MediatR;

namespace ECinema.Movie.WebAPI.Application.Movies.Commands.Update;

public sealed record UpdateMovieCommand(string Id, string Name, string Base64Poster, List<string> Cast) : IRequest<bool>;