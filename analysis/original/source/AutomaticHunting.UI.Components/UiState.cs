using System.Numerics;
using Dalamud.Game.ClientState.Conditions;

namespace AutomaticHunting.UI.Components;

public static class UiState
{
	public static (string Text, string Glyph, Vector4 Color) RunStatus(AutomaticHunting plugin)
	{
		PluginConfiguration config = plugin.Config;
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
		if (plugin.IsNavigating)
		{
			return (Text: "导航中", Glyph: "▶", Color: Theme.StatusInfo);
		}
		if (string.IsNullOrWhiteSpace(config.LeaderName))
		{
			return (Text: "待设置车头", Glyph: "▸", Color: Theme.StatusIdle);
		}
		return (Text: "运行中", Glyph: "●", Color: Theme.StatusSuccess);
	}

	public static string SanitizeGlyphs(string? text)
	{
		if (string.IsNullOrEmpty(text))
		{
			return string.Empty;
		}
		return text.Replace('★', '◆').Replace('⚠', '✕').Replace('⚙', '▸')
			.Replace('↻', '✓');
	}

	public static bool IsRestricted(AutomaticHunting plugin)
	{
		return !plugin.Config.DisclaimerAccepted;
	}

	public static bool IsOnboardingPending(AutomaticHunting plugin)
	{
		return !plugin.Config.OnboardingCompleted;
	}

	public static Vector4 TargetColor(string targetLabel)
	{
		if (string.IsNullOrEmpty(targetLabel))
		{
			return Theme.StatusIdle;
		}
		if (targetLabel.Contains("A怪"))
		{
			return Theme.BrandGold;
		}
		if (targetLabel.Contains("Flag"))
		{
			return Theme.StatusInfo;
		}
		return Theme.StatusIdle;
	}

	public static Vector4 SMobColor(AutomaticHunting.SMonitorStatus s)
	{
		if (s.Finished)
		{
			return Theme.StatusIdle;
		}
		if (s.Phase.Contains("开怪"))
		{
			return Theme.StatusDanger;
		}
		if (s.TargetLost)
		{
			return Theme.StatusWarning;
		}
		return Theme.BrandGold;
	}

	public static string SMobGlyph(AutomaticHunting.SMonitorStatus s)
	{
		if (s.Finished)
		{
			return "□";
		}
		if (s.Phase.Contains("开怪"))
		{
			return "■";
		}
		if (s.TargetLost)
		{
			return "◆";
		}
		return "●";
	}

	public static (string Text, string Glyph, Vector4 Color) DependencyState(string name, bool loaded, bool required)
	{
		if (loaded)
		{
			return (Text: "已加载", Glyph: "✓", Color: Theme.StatusSuccess);
		}
		if (required)
		{
			return (Text: "未加载", Glyph: "✕", Color: Theme.StatusDanger);
		}
		return (Text: "未安装（选装）", Glyph: "□", Color: Theme.StatusIdle);
	}
}
