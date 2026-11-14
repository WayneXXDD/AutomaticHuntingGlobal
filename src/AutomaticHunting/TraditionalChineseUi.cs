using System;
using System.IO;
using Dalamud.Interface;
using Dalamud.Interface.ManagedFontAtlas;

namespace AutomaticHunting;

public sealed partial class AutomaticHunting
{
    private IFontHandle? traditionalFont;
    private bool fontFailureReported;

    private void InitializeTraditionalChinese()
    {
        TraditionalText.Initialize();
        var directory = PluginInterface.AssemblyLocation.DirectoryName
            ?? throw new InvalidOperationException("無法取得插件安裝位置。");
        var path = Path.Combine(directory, "fonts", "NotoSansCJKtc-Regular.otf");
        if (!File.Exists(path)) throw new FileNotFoundException("找不到繁體中文字型，請重新安裝插件。", path);
        traditionalFont = PluginInterface.UiBuilder.FontAtlas.NewDelegateFontHandle(step => step.OnPreBuild(toolkit => {
            var baseFont = toolkit.AddDalamudDefaultFont(UiBuilder.DefaultFontSizePx);
            toolkit.AddFontFromFile(path, new SafeFontConfig {
                SizePx = UiBuilder.DefaultFontSizePx,
                MergeFont = baseFont,
                GlyphRanges = new ushort[] { 0x0020, 0x00FF, 0x2000, 0x9FFF, 0xFF00, 0xFFEF, 0 }
            });
            toolkit.Font = baseFont;
        }));
    }

    private void DrawTraditionalUi()
    {
        if (traditionalFont?.Available != true) {
            if (!fontFailureReported && traditionalFont?.LoadException is { } exception) {
                fontFailureReported = true;
                Log.Error(exception, "自動狩獵跟車：繁體中文字型載入失敗，請重新安裝插件。");
            }
            return;
        }
        TraditionalText.PreservePlayerNames(Config);
        using (traditionalFont.Push()) field_47.Draw();
    }
}
