using ECinema.Movie.Service.Data;
using MediatR;

namespace ECinema.Movie.Service.Application.Movies.Commands.Create;

internal sealed class CreateMovieCommandHandler(IMovieRepository movieRepository) : IRequestHandler<CreateMovieCommand, bool>
{
    public async Task<bool> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = new Data.Movie(request.Name, request.Base64Poster, request.Cast);
        await movieRepository.AddAsync(movie);
        
        return true;
    }
}