using IthsBoardGamers.DataAccess.ChatDB;
using IthsBoardGamers.DataAccess.Services;
using IthsBoardGamers.Server.Hubs;
using IthsBoardGamers.Shared.DTOs;

namespace IthsBoardGamers.Server.Extensions;

public static class WebApplicationsEndpointExtensions
{
    #region Games
    public static WebApplication MapGamesEndpoints(this WebApplication app)
    {
        app.MapGet("allGames", GetAllGamesHandler);
        app.MapPost("saveGame", SaveGameHandler);

        return app;
    }

    private static async Task<IResult> SaveGameHandler(IRepository<BoardGameDTO> gameRepository, BoardGameDTO game)
    {
        await gameRepository.AddAsync(game);
        return Results.Ok("");
    }

    private static async Task<IResult> GetAllGamesHandler(IRepository<BoardGameDTO> gameRepository)
    {
        return Results.Ok(await gameRepository.GetAllAsync());

    }

    #endregion

    #region Chat

    public static WebApplication MapChatEndpoints(this WebApplication app)
    {
        app.MapHub<ChatHub>("/hubs/chatHub");

        app.MapGet("allMessages", GetAllMessagesHandler);

        return app;
    }
    private static async Task<IResult> GetAllMessagesHandler(IRepository<ChatMessageDto> chatRepository)
    {
        return Results.Ok(await chatRepository.GetAllAsync());
    }

    #endregion

}