using IthsBoardGamers.DataAccess.ChatDB.Models;
using IthsBoardGamers.DataAccess.GamesDB.Models;
using IthsBoardGamers.DataAccess.Services;
using IthsBoardGamers.Shared.DTOs;
using Microsoft.Extensions.Options;
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
    public async Task AddAsync(BoardGameDTO item)
    {
        throw new NotImplementedException();
    }

    public async Task AddManyAsync(IEnumerable<BoardGameDTO> items)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<BoardGameDTO>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<BoardGameDTO> GetAsync(object id)
    {
        throw new NotImplementedException();
    }

    public async Task<BoardGameDTO> UpdateAsync(BoardGameDTO item, object id)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(object id)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteManyAsync(IEnumerable<BoardGameDTO> items)
    {
        throw new NotImplementedException();
    }
}