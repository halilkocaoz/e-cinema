using ECinema.Common;
using ECinema.Movie.Data;
using MediatR;

namespace ECinema.Movie.Application.Movies.Commands.Update;

internal sealed class UpdateMovieCommandHandler(IMovieRepository movieRepository) : IRequestHandler<UpdateMovieCommand, bool>
{
    public async Task<bool> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = await movieRepository.GetByIdAsync(request.Id);
        if (movie is null)
            throw new ApiException("Movie not found", 404);
        
        movie.Cast = request.Cast;
        movie.Name = request.Name;
        movie.Base64Poster = request.Base64Poster;
        movie.Genres = request.Genres;
        movie.AddUpdatedMessage();
        
        await movieRepository.UpdateAsync(movie);
        return true;
    }
}