using IthsBoardGamers.DataAccess.ChatDB.Models;
using IthsBoardGamers.DataAccess.GamesDB.Models;
using IthsBoardGamers.DataAccess.Services;
using IthsBoardGamers.Shared;
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

    public async Task<ServiceResponse<ChatMessageDto?>> AddAsync(ChatMessageDto? item)
    {
        if (item is null)
        {
            return new()
            {
                Data = null,
                Success = false,
                Message = "No message to add"
            };
        }

        await _chatCollection.InsertOneAsync(new ChatMessageModel()
        {
            Message = item.Message,
            SenderEmail = item.Sender.Email,
            TimeSent = item.TimeSent
        });
        return new()
        {
            Data = item,
            Success = true,
            Message = "Chat message added"
        };
    }

    public async Task<ServiceResponse<ChatMessageDto?[]?>> AddManyAsync(ChatMessageDto?[]? items)
    {
        if (items is null)
        {
            return new()
            {
                Data = null,
                Success = false,
                Message = "No messages to add"
            };
        }

        await _chatCollection.InsertManyAsync(items
            .Select(ConvertToChatMessageModel));

        return new()
        {
            Data = items.ToArray(),
            Success = true,
            Message = "MessagesAdded"
        };
    }

    public async Task<ServiceResponse<ChatMessageDto?[]?>> GetAllAsync()
    {
        var all = await _chatCollection.FindAsync(_ => true);
        var results = all.ToList()
                    .Select(ConvertToChatMessageDto).ToArray();
        return new()
        {
            Data = results,
            Success = true,
            Message = ""
        };
    }

    public async Task<ServiceResponse<ChatMessageDto?>> GetAsync(object id)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResponse<ChatMessageDto?>> UpdateAsync(ChatMessageDto? item, object id)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResponse<ChatMessageDto?>> DeleteAsync(object id)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResponse<ChatMessageDto?[]?>> DeleteManyAsync(ChatMessageDto?[]? items)
    {
        throw new NotImplementedException();
    }
    private ChatMessageDto? ConvertToChatMessageDto(ChatMessageModel? dataModel)
    {
        if (dataModel is null)
        {
            return null;
        }

        return new()
        {
            Sender = new() { Email = dataModel.SenderEmail },
            Message = dataModel.Message,
            TimeSent = dataModel.TimeSent
        };
    }
    private ChatMessageModel? ConvertToChatMessageModel(ChatMessageDto? dto)
    {
        if (dto is null)
        {
            return null;
        }

        return new()
        {
            SenderEmail = dto.Sender.Email,
            Message = dto.Message,
            TimeSent = dto.TimeSent
        };
    }
}