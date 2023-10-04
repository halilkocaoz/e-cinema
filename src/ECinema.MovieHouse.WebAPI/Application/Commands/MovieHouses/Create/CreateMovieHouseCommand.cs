using MediatR;

namespace ECinema.MovieHouse.WebAPI.Application.Commands.MovieHouses.Create;

public sealed record CreateMovieHouseCommand(string Name, List<string> MovieGenres) : IRequest<bool>;
