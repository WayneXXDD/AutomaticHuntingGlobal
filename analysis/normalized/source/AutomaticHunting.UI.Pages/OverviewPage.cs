using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using AutomaticHunting.UI.Components;
using Dalamud.Bindings.ImGui;

namespace AutomaticHunting.UI.Pages;

public sealed class OverviewPage : SettingsPage
{
	private static readonly string[] field_893 = new string[6] { "启用", "当前区域", "车头", "导航状态", "S怪", "车头距离" };

	public OverviewPage(AutomaticHunting arg0)
		: base(arg0)
	{
	}

	public override void Draw()
	{
		Method_930();
		UiSection.Gap();
		Method_931();
		UiSection.Gap();
		Method_932();
		UiSection.Gap();
		Method_933();
		UiSection.Gap();
		Method_935();
		UiSection.Gap();
		Method_936();
		UiSection.Gap();
		Method_937();
	}

	private void Method_930()
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

	private void Method_931()
	{
		UiSection.Header("自动狩猎", "总开关");
		UiForm.BeginMeasured("##overviewEnable", field_893);
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

	private void Method_932()
	{
		UiSection.Header("运行状态");
		UiForm.BeginMeasured("##overviewStatus", field_893);
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
		var (arg, arg2, arg3) = UiState.RunStatus(Plugin);
		UiBadge.Dot(arg2, arg, arg3);
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
			ImU8String text2 = new ImU8String(1, 1);
			text2.AppendFormatted(leaderDistance.Value, "0.0");
			text2.AppendLiteral("m");
			ImGui.Text(text2);
		}
		UiForm.End();
	}

	private void Method_933()
	{
		UiSection.Header("前置插件", "Lifestream / vnavmesh 必需");
		UiForm.BeginMeasured("##overviewDeps", "Lifestream", "vnavmesh", "BossModReborn");
		Method_934("Lifestream", AutomaticHunting.IsPluginLoaded("Lifestream"), arg2: true);
		Method_934("vnavmesh", AutomaticHunting.IsPluginLoaded("vnavmesh"), arg2: true);
		Method_934("BossModReborn", Plugin.IsBmrLoaded(), arg2: false);
		UiForm.End();
	}

	private static void Method_934(string arg0, bool arg1, bool arg2)
	{
		var (arg3, arg4, arg5) = UiState.DependencyState(arg0, arg1, arg2);
		UiForm.Row(arg0);
		UiBadge.Dot(arg4, arg3, arg5);
	}

	private void Method_935()
	{
		UiSection.Header("当前目标");
		UiForm.BeginMeasured("##overviewTarget", "目标");
		var (arg, _, num) = Plugin.GetTargetInfo();
		UiForm.Row("目标");
		ImGui.TextColored(UiState.TargetColor(arg), UiState.SanitizeGlyphs(arg));
		if (num.HasValue)
		{
			ImU8String text = new ImU8String(4, 1);
			text.AppendLiteral("   ");
			text.AppendFormatted(num.Value, "0.0");
			text.AppendLiteral("m");
			ImGui.TextDisabled(text);
		}
		UiForm.End();
	}

	private void Method_936()
	{
		UiSection.Header("悬浮窗外观", "常驻 HUD");
		UiForm.BeginActionMeasured("##overviewHud", 32f, "透明度", "紧凑模式", "位置锁定");
		UiForm.Row("透明度", "面板底不透明度（0.62–1.00）");
		UiForm.SliderWithReset("##hudAlpha", () => Config.HudAlpha, (float arg0) =>
		{
			Config.HudAlpha = Math.Clamp(arg0, 0.62f, 1f);
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
		UiLayout.EqualColumns("##hudBtns", 2, 8f, (int arg0) =>
		{
			Vector2 value = new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight);
			if (arg0 == 0)
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

	private void Method_937()
	{
		UiSection.Header("帮助与诊断");
		UiForm.BeginMeasured("##overviewHelp", "入口");
		UiForm.Row("入口");
		UiLayout.EqualColumns("##overviewHelpBtns", 2, 8f, (int arg0) =>
		{
			Vector2 value = new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight);
			if (arg0 == 0)
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
	private float Method_939()
	{
		return Config.HudAlpha;
	}

	[CompilerGenerated]
	private void Method_940(float arg0)
	{
		Config.HudAlpha = Math.Clamp(arg0, 0.62f, 1f);
		Save();
	}

	[CompilerGenerated]
	private void Method_941(int arg0)
	{
		Vector2 value = new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight);
		if (arg0 == 0)
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
	private void Method_942(int arg0)
	{
		Vector2 value = new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight);
		if (arg0 == 0)
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
