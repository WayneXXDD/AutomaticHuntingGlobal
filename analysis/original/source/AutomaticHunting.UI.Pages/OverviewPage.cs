using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using AutomaticHunting.UI.Components;
using Dalamud.Bindings.ImGui;

namespace AutomaticHunting.UI.Pages;

public sealed class OverviewPage : SettingsPage
{
	private static readonly string[] _2008_203E = new string[6] { "启用", "当前区域", "车头", "导航状态", "S怪", "车头距离" };

	public OverviewPage(AutomaticHunting plugin)
		: base(plugin)
	{
	}

	public override void Draw()
	{
		_00A0();
		UiSection.Gap();
		_1680();
		UiSection.Gap();
		_2000();
		UiSection.Gap();
		_2001();
		UiSection.Gap();
		_2002();
		UiSection.Gap();
		_2003();
		UiSection.Gap();
		_2004();
	}

	private void _00A0()
	{
		if (!Config.OnboardingCompleted)
		{
			UiBanner.Show("尚未完成首次设置", "自动化功能保持关闭，完成向导后即可正常使用。", Theme.StatusWarning);
			if (UiButton.Draw("打开首次运行向导", UiVariant.Primary, new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight), "4 步快速设置：免责声明 → 前置检查 → 车头 → 完成"))
			{
				Plugin.OnboardingWindow.Open();
			}
		}
		else if (!Config.DisclaimerAccepted)
		{
			UiBanner.Show("受限模式", "未确认免责声明，所有自动化入口已关闭（不会自动跟车 / 传送 / 跨服）。", Theme.StatusDanger);
			if (UiButton.Draw("查看免责声明并恢复", UiVariant.Danger, new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight), "打开向导重新确认免责声明"))
			{
				Plugin.OnboardingWindow.Open();
			}
		}
	}

	private void _1680()
	{
		UiSection.Header("自动狩猎", "总开关");
		UiForm.BeginMeasured("##overviewEnable", _2008_203E);
		UiForm.Row("启用", "关闭后不再监听车头，也不会自动导航");
		bool v = Config.Enabled;
		if (ImGui.Checkbox("##autoHuntEnabled", ref v))
		{
			bool enabled = Config.Enabled;
			Config.Enabled = v;
			Save();
			if (v && !enabled)
			{
				Plugin.PrintDisclaimer();
			}
		}
		UiForm.End();
	}

	private void _2000()
	{
		UiSection.Header("运行状态");
		UiForm.BeginMeasured("##overviewStatus", _2008_203E);
		string currentTerritoryName = AutomaticHunting.GetCurrentTerritoryName();
		uint territoryType = AutomaticHunting.ClientState.TerritoryType;
		UiForm.Row("当前区域");
		string? text;
		if (territoryType != 0)
		{
			text = (string.IsNullOrWhiteSpace(currentTerritoryName) ? territoryType.ToString() : $"{currentTerritoryName} ({territoryType})");
		}
		else
		{
			text = "未知";
		}
		ImGui.TextUnformatted(text);
		UiForm.Row("车头");
		ImGui.TextUnformatted(string.IsNullOrWhiteSpace(Config.LeaderName) ? "未设置" : Config.LeaderName);
		UiForm.Row("导航状态");
		var (text2, glyph, color) = UiState.RunStatus(Plugin);
		UiBadge.Dot(glyph, text2, color);
		AutomaticHunting.SMonitorStatus sMonitor = Plugin.SMonitor;
		if (sMonitor != null)
		{
			UiForm.Row("S怪");
			ImGui.TextColored(UiState.SMobColor(sMonitor), sMonitor.Finished ? sMonitor.Phase : $"{sMonitor.MobName} · {sMonitor.Phase} · {sMonitor.HpPercent:0.0}%");
		}
		float? leaderDistance = Plugin.GetLeaderDistance();
		if (leaderDistance.HasValue)
		{
			UiForm.Row("车头距离");
			ImU8String text3 = new ImU8String(1, 1);
			text3.AppendFormatted(leaderDistance.Value, "0.0");
			text3.AppendLiteral("m");
			ImGui.Text(text3);
		}
		UiForm.End();
	}

	private void _2001()
	{
		UiSection.Header("前置插件", "Lifestream / vnavmesh 必需");
		UiForm.BeginMeasured("##overviewDeps", "Lifestream", "vnavmesh", "BossModReborn");
		_00A0("Lifestream", AutomaticHunting.IsPluginLoaded("Lifestream"), true);
		_00A0("vnavmesh", AutomaticHunting.IsPluginLoaded("vnavmesh"), true);
		_00A0("BossModReborn", Plugin.IsBmrLoaded(), false);
		UiForm.End();
	}

	private static void _00A0(string P_0, bool P_1, bool P_2)
	{
		var (text, glyph, color) = UiState.DependencyState(P_0, P_1, P_2);
		UiForm.Row(P_0);
		UiBadge.Dot(glyph, text, color);
	}

	private void _2002()
	{
		UiSection.Header("当前目标");
		UiForm.BeginMeasured("##overviewTarget", "目标");
		var (text, _, num) = Plugin.GetTargetInfo();
		UiForm.Row("目标");
		ImGui.TextColored(UiState.TargetColor(text), UiState.SanitizeGlyphs(text));
		if (num.HasValue)
		{
			ImU8String text2 = new ImU8String(4, 1);
			text2.AppendLiteral("   ");
			text2.AppendFormatted(num.Value, "0.0");
			text2.AppendLiteral("m");
			ImGui.TextDisabled(text2);
		}
		UiForm.End();
	}

	private void _2003()
	{
		UiSection.Header("悬浮窗外观", "常驻 HUD");
		UiForm.BeginActionMeasured("##overviewHud", 32f, "透明度", "紧凑模式", "位置锁定");
		UiForm.Row("透明度", "面板底不透明度（0.62–1.00）");
		UiForm.SliderWithReset("##hudAlpha", () => Config.HudAlpha, (float P_0) =>
		{
			Config.HudAlpha = Math.Clamp(P_0, 0.62f, 1f);
			Save();
		}, 0.62f, 1f, "%.2f", 0.88f);
		UiForm.Row("紧凑模式", "常驻时只显示一行摘要");
		bool v = Config.HudCompact;
		if (ImGui.Checkbox("##hudCompact", ref v))
		{
			Config.HudCompact = v;
			Save();
		}
		UiForm.Row("位置锁定", "锁定后 HUD 不可拖动");
		bool v2 = Config.HudLocked;
		if (ImGui.Checkbox("##hudLocked", ref v2))
		{
			Config.HudLocked = v2;
			Save();
		}
		UiForm.End();
		UiForm.BeginMeasured("##overviewHudBtns", "位置");
		UiForm.Row("位置");
		UiLayout.EqualColumns("##hudBtns", 2, 8f, (int P_0) =>
		{
			Vector2 value = new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight);
			if (P_0 == 0)
			{
				if (UiButton.Draw("复位位置", UiVariant.Neutral, value, "把 HUD 位置复位到左上角"))
				{
					Plugin.HudWindow.ResetPosition();
					Plugin.PrintEcho("HUD 位置已复位");
				}
			}
			else
			{
				bool isOpen = Plugin.HudWindow.IsOpen;
				if (UiButton.Draw(isOpen ? "隐藏 HUD" : "显示 HUD", UiVariant.Neutral, value, "显示 / 隐藏悬浮窗"))
				{
					Plugin.HudWindow.IsOpen = !isOpen;
					Config.HudVisible = Plugin.HudWindow.IsOpen;
					Save();
				}
			}
		});
		UiForm.End();
	}

	private void _2004()
	{
		UiSection.Header("帮助与诊断");
		UiForm.BeginMeasured("##overviewHelp", "入口");
		UiForm.Row("入口");
		UiLayout.EqualColumns("##overviewHelpBtns", 2, 8f, (int P_0) =>
		{
			Vector2 value = new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight);
			if (P_0 == 0)
			{
				if (UiButton.Draw("使用说明", UiVariant.Neutral, value, "打开内嵌的使用说明窗口"))
				{
					Plugin.HelpWindow.IsOpen = true;
				}
			}
			else if (UiButton.Draw("复制诊断信息", UiVariant.Neutral, value, "把状态 / 前置 / 配置摘要复制到剪贴板") && Diagnostics.CopyReport(Plugin))
			{
				Plugin.PrintEcho("诊断信息已复制到剪贴板");
			}
		});
		UiForm.End();
	}

	[CompilerGenerated]
	private float _2005()
	{
		return Config.HudAlpha;
	}

	[CompilerGenerated]
	private void _00A0(float P_0)
	{
		Config.HudAlpha = Math.Clamp(P_0, 0.62f, 1f);
		Save();
	}

	[CompilerGenerated]
	private void _00A0(int P_0)
	{
		Vector2 value = new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight);
		if (P_0 == 0)
		{
			if (UiButton.Draw("复位位置", UiVariant.Neutral, value, "把 HUD 位置复位到左上角"))
			{
				Plugin.HudWindow.ResetPosition();
				Plugin.PrintEcho("HUD 位置已复位");
			}
			return;
		}
		bool isOpen = Plugin.HudWindow.IsOpen;
		if (UiButton.Draw(isOpen ? "隐藏 HUD" : "显示 HUD", UiVariant.Neutral, value, "显示 / 隐藏悬浮窗"))
		{
			Plugin.HudWindow.IsOpen = !isOpen;
			Config.HudVisible = Plugin.HudWindow.IsOpen;
			Save();
		}
	}

	[CompilerGenerated]
	private void _1680(int P_0)
	{
		Vector2 value = new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight);
		if (P_0 == 0)
		{
			if (UiButton.Draw("使用说明", UiVariant.Neutral, value, "打开内嵌的使用说明窗口"))
			{
				Plugin.HelpWindow.IsOpen = true;
			}
		}
		else if (UiButton.Draw("复制诊断信息", UiVariant.Neutral, value, "把状态 / 前置 / 配置摘要复制到剪贴板") && Diagnostics.CopyReport(Plugin))
		{
			Plugin.PrintEcho("诊断信息已复制到剪贴板");
		}
	}
}
