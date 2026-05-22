using System.Text;

namespace AdvTest.Helpers;

/// <summary>
/// Helper methods for console output formatting.
/// </summary>
public static class ConsoleHelper
{
    /// <summary>
    /// Wraps text at word boundaries to fit within the specified width.
    /// </summary>
    public static string WordWrap(string text, int width = 78)
    {
        var words = text.Split(' ');
        var line = new StringBuilder();
        var result = new StringBuilder();

        foreach (var word in words)
        {
            if (line.Length + word.Length + 1 > width)
            {
                result.AppendLine(line.ToString().TrimEnd());
                line.Clear();
            }
            line.Append(word + " ");
        }

        if (line.Length > 0)
            result.Append(line.ToString().TrimEnd());

        return result.ToString();
    }
}