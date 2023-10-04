namespace ECinema.MovieHouse.Service.Models.MovieHouses;

public class CreateMovieHouseModel(string name, List<string> movieGenres)
{
    public string Name { get; set; } = name;
    public List<string> MovieGenres { get; set; } = movieGenres;
}