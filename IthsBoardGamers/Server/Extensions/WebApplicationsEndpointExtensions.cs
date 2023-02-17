using IthsBoardGamers.DataAccess.Services;
using IthsBoardGamers.Shared.DTOs;

namespace IthsBoardGamers.Server.Extensions;

public static class WebApplicationsEndpointExtensions
{
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        app.MapGet("allMessages", GetAllMessagesHandler);

        return app;
    }

    private static async Task<IResult> GetAllMessagesHandler(IRepository<ChatMessageDto> chatRepository)
    {
        return Results.Ok(await chatRepository.GetAllAsync());
    }
}