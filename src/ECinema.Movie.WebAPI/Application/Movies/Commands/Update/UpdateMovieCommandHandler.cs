using ECinema.Movie.WebAPI.Data;
using MediatR;

namespace ECinema.Movie.WebAPI.Application.Movies.Commands.Update;

internal sealed class UpdateMovieCommandHandler(IMovieRepository movieRepository) : IRequestHandler<UpdateMovieCommand, bool>
{
    public async Task<bool> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = await movieRepository.GetByIdAsync(request.Id);
        if (movie is null)
            return false;
        
        movie.Cast = request.Cast;
        movie.Name = request.Name;
        movie.Base64Poster = request.Base64Poster;
        movie.AddUpdatedMessage();
        
        await movieRepository.UpdateAsync(movie);
        return true;
    }
}