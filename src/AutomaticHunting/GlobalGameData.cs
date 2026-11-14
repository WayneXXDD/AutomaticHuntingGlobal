using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Lumina.Excel.Sheets;

namespace AutomaticHunting;

/// <summary>Reads regional and localized data from the running Global client.</summary>
internal static class GlobalGameData
{
    private static readonly PropertyInfo WorldDcProperty = typeof(World).GetProperty("LogicalDataCenter")
        ?? typeof(World).GetProperty("DataCenter")
        ?? throw new InvalidOperationException("國際服世界資料缺少資料中心欄位。");

    private static uint GetDcId(World world)
    {
        var reference = WorldDcProperty.GetValue(world);
        if (reference == null) return 0;
        return Convert.ToUInt32(reference.GetType().GetProperty("RowId")?.GetValue(reference) ?? reference);
    }

    internal static (string Name, string[] Servers)[] BuildDcs()
    {
        var worlds = AutomaticHunting.DataManager.GetExcelSheet<World>();
        var dcs = AutomaticHunting.DataManager.GetExcelSheet<WorldDCGroupType>();
        return worlds.Where(w => w.IsPublic && GetDcId(w) is > 0 and < 99)
            .GroupBy(GetDcId)
            .Select(group => (Name: dcs.GetRow(group.Key).Name.ExtractText(),
                Servers: group.Select(w => w.Name.ExtractText()).Where(n => n.Length > 0).Order(StringComparer.OrdinalIgnoreCase).ToArray()))
            .Where(dc => dc.Name.Length > 0 && dc.Servers.Length > 0)
            .OrderBy(dc => dc.Name, StringComparer.OrdinalIgnoreCase).ToArray();
    }

    private static IEnumerable<TerritoryType> HuntTerritories() =>
        AutomaticHunting.DataManager.GetExcelSheet<TerritoryType>()
            .Where(t => t.ExVersion.RowId is >= 3 and <= 5 && t.NotoriousMonsterTerritory.RowId != 0);

    internal static Dictionary<int, string[]> BuildMapNames() => HuntTerritories()
        .GroupBy(t => (int)t.ExVersion.RowId)
        .ToDictionary(g => g.Key, g => g.OrderBy(t => t.RowId)
            .Select(t => t.PlaceName.Value.Name.ExtractText()).Where(n => n.Length > 0).Distinct().ToArray());

    internal static Dictionary<string, string[]> BuildAetheryteNames()
    {
        var aetherytes = AutomaticHunting.DataManager.GetExcelSheet<Aetheryte>();
        return HuntTerritories().GroupBy(t => t.PlaceName.Value.Name.ExtractText()).ToDictionary(g => g.Key,
            g => aetherytes.Where(a => a.IsAetheryte && g.Any(t => a.Territory.RowId == t.RowId))
                .OrderBy(a => a.RowId).Select(a => a.PlaceName.Value.Name.ExtractText()).Where(n => n.Length > 0).Distinct().ToArray());
    }

    internal static Dictionary<string, string[]> BuildMarks(int rank) => HuntTerritories()
        .GroupBy(t => t.PlaceName.Value.Name.ExtractText()).ToDictionary(g => g.Key,
            g => g.SelectMany(t => t.NotoriousMonsterTerritory.Value.NotoriousMonsters)
                .Where(m => m.RowId != 0).Select(m => m.Value)
                .Where(m => m.Rank == rank && m.BNpcName.RowId != 0)
                .Select(m => m.BNpcName.Value.Singular.ExtractText())
                .Where(n => n.Length > 0).Distinct(StringComparer.OrdinalIgnoreCase).ToArray());

    internal static string DefaultDashCommand => "/ac \"" +
        AutomaticHunting.DataManager.GetExcelSheet<Lumina.Excel.Sheets.Action>().GetRow(16461).Name.ExtractText() + "\"";

    internal static string DefaultMountCommand => "/gaction \"" +
        AutomaticHunting.DataManager.GetExcelSheet<GeneralAction>().GetRow(24).Name.ExtractText() + "\"";

    internal static void Migrate(PluginConfiguration config)
    {
        if (config.Version >= 3) return;
        config.ARealmMarks = BuildMarks(2);
        config.SRealmMarks = BuildMarks(3);
        if (config.SDashCommand == "/ac \u8c03\u505c") config.SDashCommand = DefaultDashCommand;
        config.MountCommand = DefaultMountCommand;
        config.CurrentInstanceFingerprint = "";
        config.CurrentInstanceNumber = Math.Clamp(config.CurrentInstanceNumber, 1, 9);
        foreach (var map in config.MultiLineMaps) map.InstanceCount = Math.Clamp(map.InstanceCount, 1, 9);
        config.Version = 3;
    }
}
