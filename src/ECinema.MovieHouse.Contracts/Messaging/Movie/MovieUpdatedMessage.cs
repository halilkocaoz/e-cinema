namespace ECinema.MovieHouse.Contracts.Messaging.Movie;

public class MovieUpdatedMessage(string name, string base64Poster, List<string> cast)
{
    public static string MessageName => nameof(MovieUpdatedMessage);

    public string Name { get; } = name;
    public string Base64Poster { get; } = base64Poster;
    public List<string> Cast { get; } = cast;
}