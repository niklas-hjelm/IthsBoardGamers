using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace IthsBoardGamers.DataAccess.ChatDB.Models;

public class ChatMessageModel
{
    [BsonId]
    public ObjectId Id { get; set; }
    [BsonElement]
    public string Sender { get; set; }
    [BsonElement]
    public string Message { get; set; }
    [BsonElement]
    public DateTime TimeSent { get; set; }
}