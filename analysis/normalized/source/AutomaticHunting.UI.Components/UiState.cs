using System.Numerics;
using Dalamud.Game.ClientState.Conditions;

namespace AutomaticHunting.UI.Components;

public static class UiState
{
	public static (string Text, string Glyph, Vector4 Color) RunStatus(AutomaticHunting arg0)
	{
		PluginConfiguration config = arg0.Config;
		if (!config.OnboardingCompleted)
		{
			return (Text: "待配置", Glyph: "◇", Color: Theme.StatusWarning);
		}
		if (!config.DisclaimerAccepted)
		{
			return (Text: "受限", Glyph: "■", Color: Theme.StatusDanger);
		}
		if (!config.AutomationEnabled)
		{
			return (Text: "已禁用", Glyph: "□", Color: Theme.StatusIdle);
		}
		if (!AutomaticHunting.IsPluginLoaded("Lifestream") || !AutomaticHunting.IsPluginLoaded("vnavmesh"))
		{
			return (Text: "前置缺失", Glyph: "✕", Color: Theme.StatusDanger);
		}
		if (AutomaticHunting.Condition[ConditionFlag.InCombat])
		{
			return (Text: "战斗中", Glyph: "◆", Color: Theme.StatusDanger);
		}
		if (arg0.IsNavigating)
		{
			return (Text: "导航中", Glyph: "▶", Color: Theme.StatusInfo);
		}
		if (string.IsNullOrWhiteSpace(config.LeaderName))
		{
			return (Text: "待设置车头", Glyph: "▸", Color: Theme.StatusIdle);
		}
		return (Text: "运行中", Glyph: "●", Color: Theme.StatusSuccess);
	}

	public static string SanitizeGlyphs(string? arg0)
	{
		if (string.IsNullOrEmpty(arg0))
		{
			return string.Empty;
		}
		return arg0.Replace('★', '◆').Replace('⚠', '✕').Replace('⚙', '▸')
			.Replace('↻', '✓');
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
		if (arg0.Contains("Flag"))
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
		if (arg0.Phase.Contains("开怪"))
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
		if (arg0.Phase.Contains("开怪"))
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
			return (Text: "已加载", Glyph: "✓", Color: Theme.StatusSuccess);
		}
		if (arg2)
		{
			return (Text: "未加载", Glyph: "✕", Color: Theme.StatusDanger);
		}
		return (Text: "未安装（选装）", Glyph: "□", Color: Theme.StatusIdle);
	}
}
