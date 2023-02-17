using System.ComponentModel.DataAnnotations;

namespace IthsBoardGamers.Shared.DTOs;

public class ChatMessageDto
{
    public string Sender { get; set; }

    public string Message { get; set; }

    public DateTime TimeSent { get; set; }
}