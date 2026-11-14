using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomaticHunting;

public static class CrossWorldData
{
    public static readonly Dictionary<string, string> ServerWorldTravel = GlobalGameData.BuildDcs()
        .SelectMany(dc => dc.Servers).Distinct(StringComparer.OrdinalIgnoreCase)
        .ToDictionary(name => name, name => GlobalCommandPolicy.WorldPrefix + name, StringComparer.OrdinalIgnoreCase);

    public static readonly Dictionary<string, string> InstanceSwitch = Enumerable.Range(1, 9)
        .ToDictionary(i => i + "X", i => GlobalCommandPolicy.InstancePrefix + i, StringComparer.OrdinalIgnoreCase);
}
