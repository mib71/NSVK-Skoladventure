using SkolAdventure.Models;

namespace SkolAdventure.Helpers;

public static class RunCommand
{
    private static readonly HashSet<string> ValidCommands = new()
    {
        "go", "take", "drop", "look", "search", "msx", "drill", "connect",
        "examine", "drink", "use", "cut", "unlock", "inventory", "read"
    };

    public static string TextCommand(string command, GameState state)
    {
        command = command.Trim().ToLower();

        if (command == "")
            return "You didn't enter a command!";

        var words = (command.Split(' ', '.', StringSplitOptions.RemoveEmptyEntries).ToList());
        return RunCommandFromList(words, state);
    }

    private static string RunCommandFromList(List<string> words, GameState state)
    {
        var verb = words.FirstOrDefault() ?? string.Empty;
        var noun = words.ElementAtOrDefault(1) ?? string.Empty;

        if (!ValidCommands.Contains(verb))
            return $"You can't {verb}.";

        return verb switch
        {
            "go" => Move(noun, state),
            "look" => Look(state),
            "take" => Take(noun, state),
            "drop" => Drop(noun, state),
            "search" => Search(state),
            "drill" => Drill(noun, state),
            "connect" => Connect(noun, state),
            "examine" => Examine(noun, state),
            "drink" => Drink(noun, state),
            "msx" => ToggleMsx(state),
            "use" => Use(noun, state),
            "unlock" => Unlock(noun, state),
            "inventory" => Inventory(state),
            "read" => Read(noun, state),
            _ => $"You can't {verb}."
        };
    }

    private static string Move(string directionInput, GameState state)
    {
        
        if (!Enum.TryParse<Direction>(directionInput, ignoreCase: true, out var direction))
            return $"You can't go {directionInput}.";

        var currentRoom = state.Rooms.First(r => r.Id == state.Player.LocationId);
        
        if (!currentRoom.Exits.TryGetValue(direction, out var nextRoomId))
            return $"You can't go {direction}.";

        var nextRoom = state.Rooms.First(r => r.Id == nextRoomId);
        state.Player.LocationId = nextRoomId;
        return DescribeRoom(nextRoom, state);
    }

    private static string Look(GameState state)
    {
        var currentRoom = state.Rooms.First(r => r.Id == state.Player.LocationId);
        return DescribeRoom(currentRoom, state);
    }

    private static string DescribeRoom(Room room, GameState state)
    {
        var exits = string.Join(", ", room.Exits.Keys);
        var items = state.Items.Where(i => i.RoomId == room.Id && !i.Hidden).ToList();

        var itemText = items.Count > 0
            ? "\nYou can see: " + string.Join(", ", items.Select(i => i.Name))
            : string.Empty;

        return $"{room.Name}\n{ConsoleHelper.WordWrap(room.Description)}{itemText}\n\nYou can move: {exits}";
    }

    private static string Take(string keyword, GameState state)
    {
        if (keyword == string.Empty)
            return "Take what?";

        var item = state.Items.FirstOrDefault(i =>
            i.Keyword == keyword && i.RoomId == state.Player.LocationId);

        if (item is null)
            return $"There is no {keyword} here.";

        item.RoomId = 0;
        return $"You pick up {item.Name}.";
    }

    private static string Drop(string keyword, GameState state)
    {
        if (keyword == string.Empty)
            return "Drop what?";

        var item = state.Items.FirstOrDefault(i =>
            i.Keyword == keyword && i.RoomId == 0);

        if (item is null)
            return $"You are not carrying {keyword}.";

        item.RoomId = state.Player.LocationId;
        return $"You drop {item.Name}.";
    }

    private static string ToggleMsx(GameState state)
    {
        state.MsxMode = !state.MsxMode;

        if (state.MsxMode)
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            return "MSX MODE ACTIVATED. GREETINGS FROM NSVK!";
        }

        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Clear();
        return "Back to terminal.";
    }

    private static string Search(GameState state)
    {
        var hiddenItems = state.Items
            .Where(i => i.RoomId == state.Player.LocationId && i.Hidden)
            .ToList();

        if (!hiddenItems.Any())
            return "You search the room but find nothing of interest.";

        foreach (var item in hiddenItems)
            item.Hidden = false;

        var found = string.Join(", ", hiddenItems.Select(i => i.Name));
        return $"You search carefully and find: {found}.";
    }

    private static string Inventory(GameState state)
    {
        var carried = state.Items.Where(i => i.RoomId == 0).ToList();

        if (!carried.Any())
            return "You are not carrying anything.";

        var items = string.Join(", ", carried.Select(i => i.Name));
        return $"You are carrying: {items}.";
    }

    private static string Examine(string keyword, GameState state)
    {
        if (keyword == string.Empty)
            return "Examine what?";

        var item = state.Items.FirstOrDefault(i =>
            i.Keyword == keyword && i.RoomId == 0);

        if (item is null)
            return $"You are not carrying {keyword}.";

        return $"It is {item.Name}.";
    }

    private static string Connect(string keyword, GameState state)
    {
        if (keyword == string.Empty)
            return "Connect what?";

        var drill = state.Items.FirstOrDefault(i => i.RoomId == 0 && i.Id == 5);

        if (drill is null)
            return "You are not carrying the drill.";

        if (state.Player.LocationId != 1)
            return "There is no power outlet here.";

        if (state.Player.HasConnectedDrill)
            return "The drill is already connected.";

        state.Player.HasConnectedDrill = true;
        return "You plug the drill into the power outlet. It hums to life.";
    }

    private static string Drill(string keyword, GameState state)
    {
        if (keyword == string.Empty)
            return "Drill what?";

        var drill = state.Items.FirstOrDefault(i => i.RoomId == 0 && i.Id == 5);

        if (drill is null)
            return "You are not carrying the drill.";

        if (!state.Player.HasConnectedDrill)
            return "You press the button but nothing happens.";

        if (state.Player.LocationId != 1)
            return "There is nothing to drill here.";

        if (keyword != "lock" && keyword != "door")
            return $"You can't drill {keyword}.";

        if (state.Player.HasDrilledLock)
            return "You have already drilled through the lock.";

        var room = state.Rooms.First(r => r.Id == state.Player.LocationId);
        room.Exits[Direction.E] = 2;
        state.Player.HasDrilledLock = true;
        return "You drill wildly around the lock. The door swings open to the east!";
    }

    private static string Unlock(string keyword, GameState state)
    {
        if (keyword == string.Empty)
            return "Unlock what?";

        if (keyword != "door" && keyword != "lock")
            return $"You can't unlock {keyword}.";

        // Room 7: unlock staff room door with small key (id 3)
        if (state.Player.LocationId == 7)
        {
            var smallKey = state.Items.FirstOrDefault(i => i.RoomId == 0 && i.Id == 3);

            if (smallKey is null)
                return "You need a key to unlock this door.";

            if (state.Player.HasUnlockedStaffRoom)
                return "The door is already unlocked.";

            var room = state.Rooms.First(r => r.Id == 7);
            room.Exits[Direction.W] = 6;
            state.Player.HasUnlockedStaffRoom = true;
            return "You unlock the door with the small key. It swings open to the west!";
        }

        // Room 5: escape with large key (id 4)
        if (state.Player.LocationId == 5)
        {
            var largeKey = state.Items.FirstOrDefault(i => i.RoomId == 0 && i.Id == 4);

            if (largeKey is null)
                return "You need a key to unlock this door.";

            state.Player.HasEscaped = true;
            return "You unlock the door and burst out into the fresh air. YOU ESCAPED!";
        }

        return "There is nothing to unlock here.";
    }

    private static string Drink(string keyword, GameState state)
    {
        if (keyword == string.Empty)
            return "Drink what?";

        var item = state.Items.FirstOrDefault(i =>
            i.Keyword == keyword && i.RoomId == 0);

        if (item is null)
            return "You have nothing to drink!";

        if (item.Id != 6)
            return $"You can't drink {keyword}.";

        state.Player.IsDead = true;
        return "Yeach!!! This cheap wine tastes of arsenic!! " +
               "The room suddenly starts spinning, you stumble and fall dead on the floor...";
    }

    private static string Use(string keyword, GameState state)
    {
        if (keyword == string.Empty)
            return "Use what?";

        var item = state.Items.FirstOrDefault(i =>
            i.Keyword == keyword && i.RoomId == 0);

        if (item is null)
            return $"You are not carrying {keyword}.";

        return item.Id switch
        {
            1 => UseCrowbar(state),
            2 => UseKnife(state),
            _ => $"You can't figure out how to use {item.Name}."
        };
    }

    private static string UseCrowbar(GameState state)
    {
        return "You swing the crowbar around but nothing useful happens here.";
    }

    private static string UseKnife(GameState state)
    {
        return "You wave the knife around but nothing useful happens here.";
    }

    private static string Read(string keyword, GameState state)
    {
        if (keyword == string.Empty)
            return "Read what?";

        var item = state.Items.FirstOrDefault(i =>
            i.Keyword == keyword && i.RoomId == 0);

        if (item is null)
            return $"You are not carrying {keyword}.";

        if (item.Id != 7)
            return $"There is nothing to read on {item.Name}.";

        return ConsoleHelper.WordWrap(
            "The crumpled note reads: 'Welcome to school! " +
            "Today's lesson: how to escape a mad chemistry teacher. " +
            "Good luck! - Nordiska SpectraVideo Klubben. " +
            "P.S. If you find a SpectraVideo SV-328, please return it to us.'");
    }
}
