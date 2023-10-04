using ECinema.Common.Infrastructure;
using MediatR;
using Microsoft.Extensions.Options;

namespace ECinema.Movie.Service.Data;

public interface IMovieRepository : IRepository<Movie>;

[Obsolete("Obsolete")]
public class MovieRepository(IOptions<MongoDbSettings> opts, IPublisher publisher) : MongoRepository<Movie>(opts, publisher), IMovieRepository;