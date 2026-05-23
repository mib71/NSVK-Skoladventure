namespace SkolAdventure.Models;

/// <summary>
/// Represents the current state of the game, including the player and all rooms.
/// </summary>
public class GameState
{
    /// <summary>
    /// The player in the game.
    /// </summary>
    public Player Player { get; set; } = new();

    /// <summary>
    /// All rooms in the game world.
    /// </summary>
    public List<Room> Rooms { get; set; } = [];

    /// <summary>
    /// All items in the game world.
    /// </summary>
    public List<Item> Items { get; set; } = [];

    public bool MsxMode { get; set; }
}