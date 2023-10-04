using System.Linq.Expressions;
using ECinema.Common.Infrastructure.Models;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ECinema.Common.Infrastructure;

public abstract class MongoRepository<T> : IRepository<T> where T : Entity
{
    private readonly IMongoCollection<T> _collection;
    private readonly IPublisher _publisher;
    [Obsolete("Obsolete")]
    protected MongoRepository(IOptions<MongoDbSettings> options, IPublisher publisher)
    {
        _publisher = publisher;
        var mongoDbSettings = options.Value ?? throw new ArgumentNullException(nameof(options));

        var client = new MongoClient(mongoDbSettings.ConnectionString);
        var db = client.GetDatabase(mongoDbSettings.Database);

        _collection = db.GetCollection<T>(typeof(T).Name.ToLowerInvariant());
    }

    public virtual IQueryable<T> Get(Expression<Func<T, bool>>? predicate = null)
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
        return _collection.Find(x => x.Id == id).SingleOrDefaultAsync();
    }
    
    private async Task PublishEvents(T entity)
    {
        if (entity.DomainEvents != null)
        {
            foreach (var domainEvent in entity.DomainEvents)
                await _publisher.Publish(domainEvent);
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
        
        return await _collection.FindOneAndReplaceAsync(x => x.Id == entity.Id, entity);
    }

    public virtual async Task<T> DeleteAsync(T entity)
    {
        await PublishEvents(entity);
        
        return await _collection.FindOneAndDeleteAsync(x => x.Id == entity.Id);
    }
}

public class MongoDbSettings
{
    public string? ConnectionString { get; set; }
    public string? Database { get; set; }

    public const string ConnectionStringValueName = nameof(ConnectionString);
    public const string DatabaseValueName = nameof(Database);
}