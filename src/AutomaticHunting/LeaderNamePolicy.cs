using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace AutomaticHunting;

public static class LeaderNamePolicy
{
    // A Global character's first and last names are one identity.
    public static string Normalize(string? value)
    {
        if (string.IsNullOrWhiteSpace(value)) return string.Empty;
        int world = value.IndexOfAny(new[] { '@', '＠' });
        if (world >= 0) value = value.Substring(0, world);
        var result = new StringBuilder(value.Length);
        bool pendingSpace = false;
        foreach (char character in value)
        {
            if (char.IsWhiteSpace(character))
            {
                pendingSpace = result.Length > 0;
                continue;
            }
            var category = char.GetUnicodeCategory(character);
            if (char.IsControl(character) || category == UnicodeCategory.Format || category == UnicodeCategory.PrivateUse)
                continue;
            if (pendingSpace) result.Append(' ');
            result.Append(character);
            pendingSpace = false;
        }
        return result.ToString().TrimEnd(':', '：', ' ');
    }

    public static bool Matches(string? sender, string? leader)
    {
        string name = Normalize(sender);
        return name.Length > 0 && string.Equals(name, Normalize(leader), StringComparison.OrdinalIgnoreCase);
    }

    public static IEnumerable<string> ParseLegacy(string? value)
    {
        if (string.IsNullOrWhiteSpace(value)) yield break;
        // Spaces belong to character names; only explicit separators divide leaders.
        foreach (string entry in value.Split(new[] { '|', ';', '；', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
        {
            string name = Normalize(entry);
            if (name.Length > 0) yield return name;
        }
    }
}
