using IthsBoardGamers.DataAccess.ChatDB.Models;
using IthsBoardGamers.DataAccess.Services;
using IthsBoardGamers.Shared.DTOs;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace IthsBoardGamers.DataAccess.ChatDB;

public class ChatRepository : IRepository<ChatMessageDto>
{
    private readonly IMongoCollection<ChatMessageModel> _chatCollection;

    public ChatRepository(IOptions<DatabaseOptions> opt)
    {
        var databaseOptions = opt.Value;
        var connectionString = $"mongodb://{databaseOptions.Host}:{databaseOptions.Port}";
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseOptions.Database);
        _chatCollection = database.GetCollection<ChatMessageModel>("AllChat", new() { AssignIdOnInsert = true });
    }

    public async Task AddAsync(ChatMessageDto item)
    {
        await _chatCollection.InsertOneAsync(new ChatMessageModel()
        {
            Message = item.Message,
            SenderEmail = item.Sender.Email,
            TimeSent = item.TimeSent
        });
    }

    public async Task AddManyAsync(IEnumerable<ChatMessageDto> items)
    {
        await _chatCollection.InsertManyAsync(items
            .Select(m => new ChatMessageModel
            {
                Message = m.Message,
                SenderEmail = m.Sender.Email,
                TimeSent = m.TimeSent
            })
        );
    }

    public async Task<IEnumerable<ChatMessageDto>> GetAllAsync()
    {
        var all = await _chatCollection.FindAsync(_ => true);
        return all.ToList()
            .Select(m => new ChatMessageDto()
            {
                Message = m.Message, 
                Sender = new UserDto() {Email = m.SenderEmail}, 
                TimeSent = m.TimeSent
            });
    }

    public async Task<ChatMessageDto> GetAsync(object id)
    {
        throw new NotImplementedException();
    }

    public async Task<ChatMessageDto> UpdateAsync(ChatMessageDto item, object id)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(object id)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteManyAsync(IEnumerable<ChatMessageDto> items)
    {
        throw new NotImplementedException();
    }
}