namespace AdvTest.Models;

/// <summary>
/// Represents a cardinal direction for navigation between rooms.
/// </summary>
public enum Direction { N, S, E, W }

/// <summary>
/// Represents a room in the adventure game.
/// </summary>
public class Room
{
    /// <summary>
    /// Gets or sets the unique identifier for the room.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the short display name of the room.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the detailed description shown when the player enters or looks around.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the available exits from this room, mapping each direction to the ID of the connected room.
    /// </summary>
    public Dictionary<Direction, int> Exits { get; set; } = new();
}