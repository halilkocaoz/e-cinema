using System.Linq.Expressions;
using ECinema.Common.Infrastructure.Models;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ECinema.Common.Infrastructure;

public abstract class MongoRepository<T> : IRepository<T> where T : MongoEntity
{
    private readonly IMongoCollection<T> _collection;
    private readonly IPublisher _mediatrPublisher;
    protected MongoRepository(IOptions<MongoDbSettings> options, IPublisher mediatrPublisher)
    {
        _mediatrPublisher = mediatrPublisher;
        var mongoDbSettings = options.Value;

        var client = new MongoClient(mongoDbSettings.ConnectionString);
        var db = client.GetDatabase(mongoDbSettings.Database);

        _collection = db.GetCollection<T>(typeof(T).Name.ToLowerInvariant());
    }

    public virtual IQueryable<T> Get(Expression<Func<T, bool>> predicate = null)
    {
        return predicate == null
            ? _collection.AsQueryable()
            : _collection.AsQueryable().Where(predicate);
    }

    public virtual Task<T> GetAsync(Expression<Func<T, bool>> predicate)
    {
        return _collection.Find(predicate).FirstOrDefaultAsync();
    }

    public virtual Task<T> GetByIdAsync(string id)
    {
        return _collection.Find(x => x._id.ToString() == id).FirstOrDefaultAsync();
    }

    private async Task PublishEvents(T entity)
    {
        if (entity.DomainEvents != null)
        {
            foreach (var domainEvent in entity.DomainEvents)
                await _mediatrPublisher.Publish(domainEvent);
            entity.ClearDomainEvents();
        }
    }

    public virtual async Task<T> AddAsync(T entity)
    {
        await PublishEvents(entity);

        var options = new InsertOneOptions { BypassDocumentValidation = false };
        await _collection.InsertOneAsync(entity, options);
        return entity;
    }

    public virtual async Task<T> UpdateAsync(T entity)
    {
        await PublishEvents(entity);

        return await _collection.FindOneAndReplaceAsync(x => x._id == entity._id, entity);
    }

    public virtual async Task<T> DeleteAsync(T entity)
    {
        await PublishEvents(entity);

        return await _collection.FindOneAndDeleteAsync(x => x._id == entity._id);
    }
}

public class MongoDbSettings
{
    public string ConnectionString { get; set; }
    public string Database { get; set; }

    public const string ConnectionStringValueName = nameof(ConnectionString);
    public const string DatabaseValueName = nameof(Database);
}