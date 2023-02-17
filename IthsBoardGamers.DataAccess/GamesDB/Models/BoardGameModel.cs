using IthsBoardGamers.Shared.DTOs;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace IthsBoardGamers.DataAccess.GamesDB.Models;

public class BoardGameModel
{
    [BsonId]
    public ObjectId Id { get; set; }
    [BsonElement]
    public string Name { get; set; }
    [BsonElement]
    public string Description { get; set; }
    [BsonElement]
    public int MinimumPlayers { get; set; }
    [BsonElement]
    public int MaximumPlayers { get; set; }
    [BsonElement]
    public int Playtime { get; set; }
    [BsonElement]
    public string OwnerEmail { get; set; }
}