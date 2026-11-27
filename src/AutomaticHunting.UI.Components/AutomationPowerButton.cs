using System;
using System.Numerics;

namespace AutomaticHunting.UI.Components;

public static class AutomationPowerButton
{
    public static void Draw(bool enabled, Action? toggle, Vector2 size, string id = "hud")
    {
        if (UiButton.Draw(
            (enabled ? "關閉插件" : "啟動插件") + "###automation-power-" + id,
            enabled ? UiVariant.Danger : UiVariant.Primary,
            size,
            enabled ? "停止跟車、導航與待執行的動作" : "開啟後等待車頭訊息；每次載入預設關閉"))
            toggle?.Invoke();
    }
}
