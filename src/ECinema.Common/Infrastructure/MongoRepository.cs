using System.Linq.Expressions;
using ECinema.Common.Infrastructure.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ECinema.Common.Infrastructure;

public abstract class MongoRepository<T> : IRepository<T> where T : Entity
{
    private readonly IMongoCollection<T> _collection;

    [Obsolete("Obsolete")]
    private static MongoClientSettings GetMongoClientSettings(MongoDbSettings mongoDbSettings)
    {
        var internalIdentity = new MongoInternalIdentity(mongoDbSettings.Database, mongoDbSettings.UserName);
        var passwordEvidence = new PasswordEvidence(mongoDbSettings.Password);
        var mongoCredential = new MongoCredential(mongoDbSettings.AuthMechanism, internalIdentity, passwordEvidence);
        var mongoClientSettings = new MongoClientSettings
        {
            Credential = mongoCredential,
            ReadPreference = ReadPreference.SecondaryPreferred,
            WriteConcern = WriteConcern.Acknowledged,
            GuidRepresentation = GuidRepresentation.Standard
        };
        return mongoClientSettings;
    }
    
    [Obsolete("Obsolete")]
    private MongoRepository(IOptions<MongoDbSettings> options)
    {
        var mongoDbSettings = options.Value ?? throw new ArgumentNullException(nameof(options));
        var mongoClientSettings = GetMongoClientSettings(mongoDbSettings);

        var client = new MongoClient(mongoClientSettings);
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

    public virtual async Task<T> AddAsync(T entity)
    {
        var options = new InsertOneOptions {BypassDocumentValidation = false};
        await _collection.InsertOneAsync(entity, options);
        return entity;
    }

    public virtual async Task<T> UpdateAsync(T entity)
    {
        return await _collection.FindOneAndReplaceAsync(x => x.Id == entity.Id, entity);
    }

    public virtual async Task<T> DeleteAsync(T entity)
    {
        return await _collection.FindOneAndDeleteAsync(x => x.Id == entity.Id);
    }
}

public class MongoDbSettings
{
    public string? ConnectionString { get; set; }
    public string? Database { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public string? AuthMechanism { get; set; }

    public const string ConnectionStringValueName = nameof(ConnectionString);
    public const string DatabaseValueName = nameof(Database);
    public const string UserNameValueName = nameof(UserName);
    public const string PasswordValueName = nameof(Password);
    public const string AuthMechanismValueName = nameof(AuthMechanism);
}