using ECinema.Common.Infrastructure.Models;
using ECinema.Movie.Application.Movies.Events;

namespace ECinema.Movie.Data;

public class Movie : MongoEntity
{
    public Movie(string name, string base64Poster, List<string> cast)
    {
        Name = name;
        Base64Poster = base64Poster;
        Cast = cast;
        AddDomainEvent(new MovieCreatedEvent(this));
    }

    public string Name { get; set; }
    public string Base64Poster { get; set; }
    public List<string> Cast { get; set; }

    public void AddUpdatedMessage() 
        => AddDomainEvent(new MovieUpdatedEvent(this));
}