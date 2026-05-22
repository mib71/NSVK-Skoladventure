namespace AdvTest.Models;

/// <summary>
/// Represents an item in the adventure game world.
/// </summary>
public class Item
{
    /// <summary>
    /// The unique identifier for the item.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The display name of the item.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Keyword the player uses to interact with the item.
    /// </summary>
    public string Keyword { get; set; } = string.Empty;

    /// <summary>
    /// The ID of the room where the item is located.
    /// A value of 0 indicates the item is carried by the player.
    /// </summary>
    public int RoomId { get; set; }

    /// <summary>
    /// Whether the item is hidden and requires searching to find.
    /// </summary>
    public bool Hidden { get; set; }
}