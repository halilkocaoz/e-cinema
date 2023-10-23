using ECinema.Common.Infrastructure.Models;
using ECinema.Movie.Application.Movies.Events;

namespace ECinema.Movie.Data;

public class Movie : MongoEntity
{
    public Movie(string name, string base64Poster, List<string> cast, List<string> genres)
    {
        Name = name;
        Base64Poster = base64Poster;
        Cast = cast;
        Genres = genres;
        AddDomainEvent(new MovieCreatedEvent(this));
    }
    
    public void Update(string name, string base64Poster, List<string> cast, List<string> genres)
    {
        Name = name;
        Base64Poster = base64Poster;
        Cast = cast;
        Genres = genres;
        AddDomainEvent(new MovieUpdatedEvent(this));
    }
    public string Name { get; private set; }
    public string Base64Poster { get; private set; }
    public List<string> Cast { get; private set; }
    public List<string> Genres { get; private set; }
}