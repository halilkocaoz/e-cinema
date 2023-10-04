using ECinema.Common.Infrastructure;
using MediatR;
using Microsoft.Extensions.Options;

namespace ECinema.Movie.Data;

public interface IMovieRepository : IRepository<Movie>;

public class MovieRepository(IOptions<MongoDbSettings> opts, IPublisher publisher) : MongoRepository<Movie>(opts, publisher), IMovieRepository;