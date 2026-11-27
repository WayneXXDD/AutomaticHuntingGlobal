using System.Numerics;
using Dalamud.Game.ClientState.Conditions;

namespace AutomaticHunting.UI.Components;

public static class UiState
{
	public static (string Text, string Glyph, Vector4 Color) RunStatus(AutomaticHunting arg0)
	{
		PluginConfiguration config = arg0.Config;
		if (!config.Enabled) return (Text: "已關閉", Glyph: "□", Color: Theme.StatusIdle);
		if (!config.OnboardingCompleted)
		{
			return (Text: "待設定", Glyph: "◇", Color: Theme.StatusWarning);
		}
		if (!config.DisclaimerAccepted)
		{
			return (Text: "受限", Glyph: "■", Color: Theme.StatusDanger);
		}
		if (!config.AutomationEnabled)
		{
			return (Text: "已關閉", Glyph: "□", Color: Theme.StatusIdle);
		}
		if (!AutomaticHunting.IsPluginLoaded("Lifestream") || !AutomaticHunting.IsPluginLoaded("vnavmesh"))
		{
			return (Text: "前置缺失", Glyph: "x", Color: Theme.StatusDanger);
		}
		if (AutomaticHunting.Condition[ConditionFlag.InCombat])
		{
			return (Text: "戰鬥中", Glyph: "◆", Color: Theme.StatusDanger);
		}
		if (arg0.IsRecoveringAfterTravel)
		{
			return (Text: "等待重新規劃", Glyph: ">", Color: Theme.StatusWarning);
		}
		if (arg0.IsWaitingForFlagTerritory)
		{
			return (Text: "等待手動傳送", Glyph: ">", Color: Theme.StatusWarning);
		}
		if (arg0.IsNavigating)
		{
			return (Text: "導航中", Glyph: "▶", Color: Theme.StatusInfo);
		}
		if (string.IsNullOrWhiteSpace(config.LeaderName))
		{
			return (Text: "待設定車頭", Glyph: ">", Color: Theme.StatusIdle);
		}
		return (Text: "已開啟・待命", Glyph: "●", Color: Theme.StatusSuccess);
	}

	public static string SanitizeGlyphs(string? arg0)
	{
		if (string.IsNullOrEmpty(arg0))
		{
			return string.Empty;
		}
		return arg0.Replace('★', '◆').Replace('⚠', '!').Replace('⚙', '>')
			.Replace('↻', '+');
	}

	public static bool IsRestricted(AutomaticHunting arg0)
	{
		return !arg0.Config.DisclaimerAccepted;
	}

	public static bool IsOnboardingPending(AutomaticHunting arg0)
	{
		return !arg0.Config.OnboardingCompleted;
	}

	public static Vector4 TargetColor(string arg0)
	{
		if (string.IsNullOrEmpty(arg0))
		{
			return Theme.StatusIdle;
		}
		if (arg0.Contains("A怪"))
		{
			return Theme.BrandGold;
		}
		if (arg0.Contains("地圖標記"))
		{
			return Theme.StatusInfo;
		}
		return Theme.StatusIdle;
	}

	public static Vector4 SMobColor(AutomaticHunting.SMonitorStatus arg0)
	{
		if (arg0.Finished)
		{
			return Theme.StatusIdle;
		}
		if (arg0.Phase.Contains("開怪"))
		{
			return Theme.StatusDanger;
		}
		if (arg0.TargetLost)
		{
			return Theme.StatusWarning;
		}
		return Theme.BrandGold;
	}

	public static string SMobGlyph(AutomaticHunting.SMonitorStatus arg0)
	{
		if (arg0.Finished)
		{
			return "□";
		}
		if (arg0.Phase.Contains("開怪"))
		{
			return "■";
		}
		if (arg0.TargetLost)
		{
			return "◆";
		}
		return "●";
	}

	public static (string Text, string Glyph, Vector4 Color) DependencyState(string arg0, bool arg1, bool arg2)
	{
		if (arg1)
		{
			return (Text: "已載入", Glyph: "+", Color: Theme.StatusSuccess);
		}
		if (arg2)
		{
			return (Text: "未載入", Glyph: "x", Color: Theme.StatusDanger);
		}
		return (Text: "未安裝（選裝）", Glyph: "□", Color: Theme.StatusIdle);
	}
}
