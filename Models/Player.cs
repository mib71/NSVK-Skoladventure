namespace SkolAdventure.Models;

/// <summary>
/// Represents the player in the adventure game.
/// </summary>
public class Player
{
    /// <summary>
    /// Gets or sets the ID of the room the player is currently in.
    /// </summary>
    public int LocationId { get; set; }

    /// <summary>
    /// Gets or sets the number of moves the player has made.
    /// </summary>
    public int MoveCount { get; set; }

    public int KnifeCount { get; set; } 
    /// <summary>
    /// Whether the player has drilled open the lock in the north workshop.
    /// </summary>
    public bool HasDrilledLock { get; set; }

    /// <summary>
    /// Whether the player has unlocked the staff room.
    /// </summary>
    public bool HasUnlockedStaffRoom { get; set; }

    /// <summary>
    /// Whether the player has escaped the school.
    /// </summary>
    public bool HasEscaped { get; set; }

    /// <summary>
    /// Whether the player has connected the drill to a power outlet.
    /// </summary>
    public bool HasConnectedDrill { get; set; }

    /// <summary>
    /// Whether the player is dead.
    /// </summary>
    public bool IsDead { get; set; }
}