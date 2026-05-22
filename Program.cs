using AdvTest;
using AdvTest.Helpers;
using AdvTest.Models;

string? input;

try
{
    Console.SetWindowSize(80, 25);
    Console.SetBufferSize(80, 1000);
}
catch (PlatformNotSupportedException)
{
    // Non-Windows platform, ignore
}

Console.ForegroundColor = ConsoleColor.Cyan;

// Title screen fixed width, no word wrap by design. :-)
Console.WriteLine("********************************************************************************");
Console.WriteLine("*                                                                              *");
Console.WriteLine("*         YOU HAVE BEEN LOCKED IN BY THE SCHOOL'S CHEMISTRY TEACHER.           *");
Console.WriteLine("*          HE HAS GONE MAD AND IS CONDUCTING DANGEROUS EXPERIMENTS.            *");
Console.WriteLine("*                   THE SCHOOL COULD EXPLODE AT ANY MOMENT.                    *");
Console.WriteLine("*                   YOU MUST ESCAPE BEFORE IT IS TOO LATE!                     *");
Console.WriteLine("*                                                                              *");
Console.WriteLine("********************************************************************************");
Console.WriteLine();

GameState gameState = InitGame.Create();
Console.WriteLine(RunCommand.TextCommand("look", gameState));

do
{
    if (!gameState.MsxMode)
        Console.ForegroundColor = ConsoleColor.White;
    Console.Write("> ");

    if (!gameState.MsxMode)
        Console.ForegroundColor = ConsoleColor.Green;
    input = Console.ReadLine() ?? string.Empty;

    if (!gameState.MsxMode)
        Console.ForegroundColor = ConsoleColor.Cyan;

    if (input != "q")
    {
        Console.WriteLine(RunCommand.TextCommand(input, gameState));
        gameState.Player.MoveCount++;
    }

    if (gameState.Items.Any(i => i.Id == 2 && i.RoomId == 0))
    {
        gameState.Player.KnifeCount++;
        if (gameState.Player.KnifeCount >= 4)
        {
            Console.WriteLine(ConsoleHelper.WordWrap(
                "Oops. You tripped and fell on the knife. As you tried to break " +
                "your fall you drove it through your stomach. You cry out for help, " +
                "but the only one who comes is the chemistry teacher, who kicks you " +
                "in the face with a sneer."));
            Console.WriteLine("\nThere is no way around it. You are dead. It was nice knowing you.");
            break;
        }
    }

    if (gameState.Player.IsDead)
    {
        Console.WriteLine("\nThere is no way around it. You are dead. It was nice knowing you.");
        break;
    }

    if (gameState.Player.HasEscaped)
    {
        var victoryText = $"CONGRATULATIONS!! You sprint for your life and make it out " +
            $"of the school alive after {gameState.Player.MoveCount} moves!";
        Console.WriteLine(ConsoleHelper.WordWrap(victoryText));
        break;
    }

    if (gameState.Player.MoveCount >= 40)
    {
        Console.WriteLine(ConsoleHelper.WordWrap(
            "\nSuddenly the school collapses around you. A beam strikes your " +
            "head and you are hurled to the ground. The last thing you see before " +
            "losing consciousness is a red pool spreading before you."));
        Console.WriteLine("\nThere is no way around it. You are dead. It was nice knowing you.");
        break;
    }

} while (input != "q");