namespace ECinema.WebAPI.Models.Movies;

public class UpdateMovieModel(string name, string base64Poster, List<string> cast)
{
    public string Name { get; } = name;
    public string Base64Poster { get; } = base64Poster;
    public List<string> Cast { get; } = cast;
}