namespace ECinema.MovieHouse.Models.MovieHouses;

public class CreateMovieHouseModel(string name, List<string> interestedGenres, List<string> willBeInformedEmails)
{
    public string Name { get; } = name;
    public List<string> InterestedGenres { get; } = interestedGenres;
    public List<string> WillBeInformedEmails { get; } = willBeInformedEmails;
}