using System.ComponentModel.DataAnnotations;

namespace ECinema.Movie.Models.Movies;

public class UpsertMovieModel
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Base64Poster { get; set; }
    [Required]
    public List<string> Cast { get; set; }
    [Required]
    public List<string> Genres { get; set; }
}