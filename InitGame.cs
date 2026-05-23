using SkolAdventure.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SkolAdventure;

public static class InitGame
{
    
    public static GameState Create()
    {
        var roomsJson = File.ReadAllText("rooms.json");
        var itemsJson = File.ReadAllText("items.json");

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        };

        var rooms = JsonSerializer.Deserialize<List<Room>>(roomsJson, options) ?? new();
        var items = JsonSerializer.Deserialize<List<Item>>(itemsJson, options) ?? new();

        var player = new Player
        {
            LocationId = 1
        };

        return new GameState
        {
            Player = player,
            Rooms = rooms,
            Items = items,
            MsxMode = false
        };
    }
}
