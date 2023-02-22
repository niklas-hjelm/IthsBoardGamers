using IthsBoardGamers.Shared.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System.Net.Http.Json;
using IthsBoardGamers.Shared;

namespace IthsBoardGamers.Client.Pages;

public partial class Chat : ComponentBase
{
    HubConnection _chatHub;
    List<ChatMessageDto> AllMessages { get; } = new();
    ChatMessageDto CurrentChatMessage { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        _chatHub = new HubConnectionBuilder().WithUrl(_navigationManager.BaseUri + "hubs/chatHub").Build();
        _chatHub.On<ChatMessageDto>("BroadcastMessage", (message) =>
        {
            AllMessages.Add(message);
            StateHasChanged();
        });
        await _chatHub.StartAsync();

        var response = await _client.GetFromJsonAsync<ServiceResponse<ChatMessageDto[]>>(_client.BaseAddress + "allMessages");
        if (response?.Data != null)
        {
            AllMessages.AddRange(response.Data);
        }

        await base.OnInitializedAsync();
    }

    private async Task SendMessage()
    {
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;
        if (user.Identity is null || !user.Identity.IsAuthenticated)
        {
            _navigationManager.NavigateTo("");
            return;
        }
        if (CurrentChatMessage.Sender is null)
        {
            var emailClaim = user.FindFirst(p => p.Type == "preferred_username");
            if (emailClaim is null)
            {
                _navigationManager.NavigateTo("");
                return;
            }

            CurrentChatMessage.Sender = new UserDto() { Email = emailClaim.Value, Name = user.Identity.Name };
        }

        CurrentChatMessage.TimeSent = DateTime.UtcNow;
        await _chatHub.SendAsync("BroadcastMessage", CurrentChatMessage);
        CurrentChatMessage.Message = string.Empty;
    }

}