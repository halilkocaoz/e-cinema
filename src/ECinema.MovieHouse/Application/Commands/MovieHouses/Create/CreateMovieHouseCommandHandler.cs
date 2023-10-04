using MediatR;

namespace ECinema.MovieHouse.Application.Commands.MovieHouses.Create;

public class CreateMovieHouseCommandHandler : IRequestHandler<CreateMovieHouseCommand, bool>
{
    public Task<bool> Handle(CreateMovieHouseCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}