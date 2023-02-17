namespace IthsBoardGamers.Shared.DTOs;

public class BoardGameDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int MinimumPlayers { get; set; }
    public int MaximumPlayers { get; set; }
    public int Playtime { get; set; }
    public UserDto Owner { get; set; }
}