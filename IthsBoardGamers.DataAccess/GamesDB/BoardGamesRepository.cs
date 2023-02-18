using IthsBoardGamers.DataAccess.ChatDB.Models;
using IthsBoardGamers.DataAccess.GamesDB.Models;
using IthsBoardGamers.DataAccess.Services;
using IthsBoardGamers.Shared;
using IthsBoardGamers.Shared.DTOs;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace IthsBoardGamers.DataAccess.GamesDB;

public class BoardGamesRepository : IRepository<BoardGameDTO>
{
    private readonly IMongoCollection<BoardGameModel> _gamesCollection;

    public BoardGamesRepository(IOptions<DatabaseOptions> opt)
    {
        var databaseOptions = opt.Value;
        var connectionString = $"mongodb://{databaseOptions.Host}:{databaseOptions.Port}";
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseOptions.Database);
        _gamesCollection = database.GetCollection<BoardGameModel>("AllGames", new() { AssignIdOnInsert = true });
    }
    public async Task<ServiceResponse<BoardGameDTO?>> AddAsync(BoardGameDTO? item)
    {
        if (item is null)
        {
            return new()
            {
                Data = null,
                Success = false,
                Message = "No game to add"
            };
        }
        await _gamesCollection.InsertOneAsync(ConvertToBoardGameModel(item));
        return new()
        {
            Data = item,
            Success = true,
            Message = "Game added!"
        };
    }

    public async Task<ServiceResponse<BoardGameDTO?[]?>> AddManyAsync(BoardGameDTO?[]? items)
    {
        if (items is null)
        {
            return new()
            {
                Data = null,
                Success = false,
                Message = "No games to add"
            };
        }

        if (items.Any(g => g is null))
        {
            return new()
            {
                Data = null,
                Success = false,
                Message = "one or more of the games are null"
            };
        }

        await _gamesCollection.InsertManyAsync(items.Select(ConvertToBoardGameModel));
        return new()
        {
            Data = items.ToArray(),
            Success = true,
            Message = "Games added"
        };
    }

    public async Task<ServiceResponse<BoardGameDTO?[]?>> GetAllAsync()
    {
        var all = await _gamesCollection.FindAsync(_ => true);
        var results = all.ToList().Select(ConvertToBoardGameDto).ToArray();
        return new()
        {
            Data = results,
            Success = true,
            Message = ""
        };
    }

    public async Task<ServiceResponse<BoardGameDTO?>> GetAsync(object id)
    {
        var filter = Builders<BoardGameModel>.Filter.Eq("Id", (ObjectId)id);
        var found = await _gamesCollection.FindAsync(filter);
        var result = ConvertToBoardGameDto(found.FirstOrDefault());
        return result is not null
            ? new()
            {
                Data = result,
                Success = true,
                Message = ""
            }
            : new()
            {
                Data = null,
                Success = false,
                Message = $"No game found with id: {(ObjectId)id}"
            };
    }

    public async Task<ServiceResponse<BoardGameDTO?>> UpdateAsync(BoardGameDTO? item, object id)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResponse<BoardGameDTO?>> DeleteAsync(object id)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResponse<BoardGameDTO?[]?>> DeleteManyAsync(BoardGameDTO?[]? items)
    {
        throw new NotImplementedException();
    }

    private BoardGameDTO? ConvertToBoardGameDto(BoardGameModel? dataModel)
    {
        if (dataModel is null)
        {
            return null;
        }

        return new BoardGameDTO
        {
            Name = dataModel.Name,
            Description = dataModel.Description,
            MaximumPlayers = dataModel.MaximumPlayers,
            MinimumPlayers = dataModel.MinimumPlayers,
            Owner = new UserDto() { Email = dataModel.OwnerEmail },
            Playtime = dataModel.Playtime
        };
    }
    private BoardGameModel? ConvertToBoardGameModel(BoardGameDTO? dto)
    {
        if (dto is null)
        {
            return null;
        }

        return new BoardGameModel
        {
            Name = dto.Name,
            Description = dto.Description,
            MaximumPlayers = dto.MaximumPlayers,
            MinimumPlayers = dto.MinimumPlayers,
            OwnerEmail = dto.Owner.Email,
            Playtime = dto.Playtime
        };
    }
}