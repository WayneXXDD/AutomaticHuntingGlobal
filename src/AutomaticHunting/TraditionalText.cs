using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.Json;
using Dalamud.Bindings.ImGui;
using Lumina.Excel.Sheets;

namespace AutomaticHunting;

internal static class TraditionalText
{
    private static TraditionalNameFormatter formatter = new(new Dictionary<string, string>(), new HashSet<string>());

    internal static void Initialize()
    {
        using var resource = typeof(TraditionalText).Assembly.GetManifestResourceStream("AutomaticHunting.TraditionalNames.zh-TW.json")
            ?? throw new InvalidOperationException("找不到繁體中文名稱資源，請重新安裝插件。");
        var source = JsonSerializer.Deserialize<Dictionary<string, Dictionary<uint, string>>>(resource)
            ?? throw new InvalidOperationException("無法讀取繁體中文名稱資源。");
        var names = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        var places = AutomaticHunting.DataManager.GetExcelSheet<PlaceName>();
        var monsters = AutomaticHunting.DataManager.GetExcelSheet<BNpcName>();
        var mounts = AutomaticHunting.DataManager.GetExcelSheet<Mount>();
        foreach (var (id, display) in source["PlaceName"])
            if (places.TryGetRow(id, out var place)) Add(place.Name.ExtractText(), display);
        foreach (var (id, display) in source["BNpcName"])
            if (monsters.TryGetRow(id, out var monster)) Add(monster.Singular.ExtractText(), display);
        foreach (var (id, display) in source["Mount"])
            if (mounts.TryGetRow(id, out var mount)) Add(mount.Singular.ExtractText(), display);
        Add(AutomaticHunting.DataManager.GetExcelSheet<Lumina.Excel.Sheets.Action>().GetRow(16461).Name.ExtractText(), "調停");
        var keep = new HashSet<string>(StringComparer.OrdinalIgnoreCase) {
            "Lifestream", "vnavmesh", "BossModReborn", "AutomaticHunting", "GitHub"
        };
        foreach (var world in AutomaticHunting.DataManager.GetExcelSheet<World>())
            if (world.IsPublic) keep.Add(world.Name.ExtractText());
        foreach (var dc in AutomaticHunting.DataManager.GetExcelSheet<WorldDCGroupType>()) keep.Add(dc.Name.ExtractText());
        formatter = new TraditionalNameFormatter(names, keep);

        void Add(string native, string display) {
            if (!string.IsNullOrWhiteSpace(native) && native.Length > 2) names.TryAdd(native, display);
        }
    }

    internal static string Translate(string? text) => formatter.Translate(text);
    internal static void Text(string? text) => ImGui.TextUnformatted(Translate(text));
    internal static void TextUnformatted(string? text) => ImGui.TextUnformatted(Translate(text));
    internal static void TextWrapped(string? text) => ImGui.TextWrapped(Translate(text));
    internal static void TextColored(Vector4 color, string? text) => ImGui.TextColored(color, Translate(text));
    internal static void SetTooltip(string? text) => ImGui.SetTooltip(Translate(text));
    internal static Vector2 CalcTextSize(string? text) => ImGui.CalcTextSize(Translate(text));
    internal static bool BeginCombo(string label, string preview, ImGuiComboFlags flags = ImGuiComboFlags.None) =>
        ImGui.BeginCombo(label, Translate(preview), flags);
    internal static bool Selectable(string label, bool selected = false) {
        var display = Translate(label);
        if (display != label && !label.Contains("##", StringComparison.Ordinal)) display += "###" + label;
        return ImGui.Selectable(display, selected);
    }
    internal static void AddText(ImDrawListPtr list, Vector2 position, uint color, string? text) =>
        list.AddText(position, color, Translate(text));
    internal static void AddText(ImDrawListPtr list, ImFontPtr font, float size, Vector2 position, uint color, string? text) =>
        list.AddText(font, size, position, color, Translate(text));
}
