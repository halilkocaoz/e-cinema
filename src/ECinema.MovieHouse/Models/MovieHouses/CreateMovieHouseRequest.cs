namespace ECinema.MovieHouse.Models.MovieHouses;

public class CreateMovieHouseModel(string name, List<string> movieGenres)
{
    public string Name { get; } = name;
    public List<string> MovieGenres { get; } = movieGenres;
}