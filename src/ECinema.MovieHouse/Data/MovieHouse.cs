using ECinema.Common.Infrastructure.Models;

namespace ECinema.MovieHouse.Data;

public class MovieHouse(string name, List<string> interestedGenres, List<string> willBeInformedEmails)
    : MongoEntity
{
    public string Name { get; private set; } = name;
    public List<string> InterestedGenres { get; private set; } = interestedGenres;
    public List<string> WillBeInformedEmails { get; private set; } = willBeInformedEmails;
}