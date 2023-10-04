using ECinema.Common.Infrastructure.Models;
using ECinema.Movie.WebAPI.Application.Movies.Events;

namespace ECinema.Movie.WebAPI.Data;

public class Movie(string name, string base64Poster, List<string> cast) : MongoEntity
{
    public string Name { get; set; } = name;
    public string Base64Poster { get; set; } = base64Poster;
    public List<string> Cast { get; set; } = cast;

    public void AddCreatedMessage() 
        => AddDomainEvent(new MovieCreatedEvent(this));
    
    public void AddUpdatedMessage() 
        => AddDomainEvent(new MovieUpdatedEvent(this));
}