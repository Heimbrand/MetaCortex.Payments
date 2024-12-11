using MetaCortex.Payments.DataAccess.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MetaCortex.Payments.DataAccess.Entities;

public class BaseDocument : IEntity<string>
{

    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = default!;
}