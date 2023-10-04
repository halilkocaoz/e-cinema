using MediatR;

namespace ECinema.Movie.Service.Application.Movies.Commands.Create;

public sealed record CreateMovieCommand(string Name, string Base64Poster, List<string> Cast) : IRequest<bool>;