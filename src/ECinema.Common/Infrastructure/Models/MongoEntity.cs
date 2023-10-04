using System.Text.Json.Serialization;
using MediatR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ECinema.Common.Infrastructure.Models;

[BsonIgnoreExtraElements(Inherited = true)]
public abstract class MongoEntity
{
    [BsonId]
    public ObjectId _id { get; set; }
    
    [BsonElement("CreatedAt")]
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    private List<INotification> _domainEvents;

    [JsonIgnore]
    public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

    protected void AddDomainEvent(INotification eventItem)
    {
        _domainEvents ??= new List<INotification>();
        _domainEvents.Add(eventItem);
    }

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }
}