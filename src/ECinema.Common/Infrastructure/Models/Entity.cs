using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ECinema.Common.Infrastructure.Models;

[BsonIgnoreExtraElements(Inherited = true)]
public abstract class Entity
{

    [BsonRepresentation(BsonType.ObjectId)]
    [BsonId]
    [BsonElement(Order = 0)]
    [JsonIgnore]
    public string Id { get; } = ObjectId.GenerateNewId().ToString()!;

    public virtual DateTime CreatedAt { get; } = DateTime.UtcNow;
}