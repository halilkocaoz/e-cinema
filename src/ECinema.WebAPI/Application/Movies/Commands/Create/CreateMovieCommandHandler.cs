using MediatR;

namespace ECinema.WebAPI.Application.Movies.Commands.Create;

internal sealed class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, bool>
{
    public Task<bool> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}