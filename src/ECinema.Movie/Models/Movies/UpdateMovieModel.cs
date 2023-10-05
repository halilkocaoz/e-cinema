namespace ECinema.Movie.Models.Movies;

public class UpdateMovieModel(string name, string base64Poster, List<string> cast, List<string> genres)
{
    public string Name { get; } = name;
    public string Base64Poster { get; } = base64Poster;
    public List<string> Cast { get; } = cast;
    public List<string> Genres { get; } = genres;
}