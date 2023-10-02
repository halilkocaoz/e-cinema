using MediatR;

namespace ECinema.WebAPI.Application.Movies.Commands.Update;

public sealed record UpdateMovieCommand(Guid Id, string Name, string Base64Poster, List<string> Cast) : IRequest<bool>;