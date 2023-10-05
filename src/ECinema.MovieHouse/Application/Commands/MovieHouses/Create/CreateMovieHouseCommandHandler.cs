using ECinema.MovieHouse.Data;
using MediatR;

namespace ECinema.MovieHouse.Application.Commands.MovieHouses.Create;

public class CreateMovieHouseCommandHandler(IMovieHouseRepository movieHouseRepository) : IRequestHandler<CreateMovieHouseCommand, bool>
{
    public async Task<bool> Handle(CreateMovieHouseCommand request, CancellationToken cancellationToken)
    {
        var movieHouse = new Data.MovieHouse(request.Name, request.InterestedGenres, request.WillBeInformedGenres);
        await movieHouseRepository.AddAsync(movieHouse);

        return true;
    }
}