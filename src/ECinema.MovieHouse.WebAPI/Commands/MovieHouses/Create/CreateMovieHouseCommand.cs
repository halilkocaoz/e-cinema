using MediatR;

namespace ECinema.MovieHouse.WebAPI.Commands.MovieHouses.Create;

public record CreateMovieHouseCommand(string Name, List<string> MovieGenres) : IRequest<bool>;