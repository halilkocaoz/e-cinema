namespace ECinema.Movie.Contracts.Messaging.Movie;

public class MovieCreatedMessage(string name, string base64Poster, List<string> cast, List<string> genres)
{
    public static string MessageName => nameof(MovieCreatedMessage);

    public string Name { get; } = name;
    public string Base64Poster { get; } = base64Poster;
    public List<string> Cast { get; } = cast;
    public List<string> Genres { get; } = genres;
}