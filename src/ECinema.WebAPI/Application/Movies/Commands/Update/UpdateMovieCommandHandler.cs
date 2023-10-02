using MediatR;

namespace ECinema.WebAPI.Application.Movies.Commands.Update;

internal sealed class UpdateMovieCommandHandler : IRequestHandler<UpdateMovieCommand, bool>
{
    public Task<bool> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}