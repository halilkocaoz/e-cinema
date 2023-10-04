using MediatR;

namespace ECinema.MovieHouse.Service.Application.Commands.MovieHouses.Create;

public sealed record CreateMovieHouseCommand(string Name, List<string> MovieGenres) : IRequest<bool>;
