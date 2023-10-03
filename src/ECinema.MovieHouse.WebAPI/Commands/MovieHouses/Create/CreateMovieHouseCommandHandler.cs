using MediatR;

namespace ECinema.MovieHouse.WebAPI.Commands.MovieHouses.Create;

public class CreateMovieHouseCommandHandler : IRequestHandler<CreateMovieHouseCommand, bool>
{
    public Task<bool> Handle(CreateMovieHouseCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}