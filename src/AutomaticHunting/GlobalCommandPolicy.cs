using System;
using System.Globalization;

namespace AutomaticHunting;

internal static class GlobalCommandPolicy
{
    internal const string InstancePrefix = "/ahglobal instance ";
    internal const string WorldPrefix = "/ahglobal world ";

    internal static bool TryParseInstance(string command, out int instance)
    {
        instance = 0;
        return command.StartsWith(InstancePrefix, StringComparison.OrdinalIgnoreCase)
            && int.TryParse(command.AsSpan(InstancePrefix.Length), NumberStyles.None, CultureInfo.InvariantCulture, out instance)
            && instance is >= 1 and <= 9;
    }

    internal static bool IsMark(string candidate, string mark) =>
        !string.IsNullOrWhiteSpace(mark) && candidate.Contains(mark, StringComparison.OrdinalIgnoreCase);
}
