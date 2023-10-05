using ECinema.Common.Infrastructure;
using MediatR;
using Microsoft.Extensions.Options;

namespace ECinema.MovieHouse.Data;

public interface IMovieHouseRepository : IRepository<MovieHouse>;

public class MovieHouseRepository(IOptions<MongoDbSettings> opts, IPublisher publisher) : MongoRepository<MovieHouse>(opts, publisher), IMovieHouseRepository;