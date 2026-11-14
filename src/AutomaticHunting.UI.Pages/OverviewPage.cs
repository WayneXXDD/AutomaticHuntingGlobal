using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using AutomaticHunting.UI.Components;
using Dalamud.Bindings.ImGui;

namespace AutomaticHunting.UI.Pages;

public sealed class OverviewPage : SettingsPage
{
	private static readonly string[] field_893 = new string[6] { "啟用", "目前區域", "車頭", "導航狀態", "S怪", "車頭距離" };

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
			UiBanner.Show("尚未完成首次設定", "自動化功能保持關閉，完成設定精靈後即可正常使用。", Theme.StatusWarning);
			if (UiButton.Draw("開啟首次執行設定精靈", UiVariant.Primary, new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight), "4 步快速設定：免責宣告 → 前置檢查 → 車頭 → 完成"))
			{
				Plugin.OnboardingWindow.Open();
			}
		}
		else if (!Config.DisclaimerAccepted)
		{
			UiBanner.Show("受限模式", "未確認免責宣告，所有自動化入口已關閉（不會自動跟車 / 傳送 / 跨服）。", Theme.StatusDanger);
			if (UiButton.Draw("檢視免責宣告並恢復", UiVariant.Danger, new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight), "開啟設定精靈重新確認免責宣告"))
			{
				Plugin.OnboardingWindow.Open();
			}
		}
	}

	private void Method_931()
	{
		UiSection.Header("自動狩獵", "總開關");
		UiForm.BeginMeasured("##overviewEnable", field_893);
		UiForm.Row("啟用", "關閉後不再監聽車頭，也不會自動導航");
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
		UiSection.Header("執行狀態");
		UiForm.BeginMeasured("##overviewStatus", field_893);
		string currentTerritoryName = AutomaticHunting.GetCurrentTerritoryName();
		uint territoryType = AutomaticHunting.ClientState.TerritoryType;
		UiForm.Row("目前區域");
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
		UiForm.Row("車頭");
		TraditionalText.TextUnformatted(string.IsNullOrWhiteSpace(Config.LeaderName) ? "未設定" : Config.LeaderName);
		UiForm.Row("導航狀態");
		var (arg, arg2, arg3) = UiState.RunStatus(Plugin);
		UiBadge.Dot(arg2, arg, arg3);
		AutomaticHunting.SMonitorStatus sMonitor = Plugin.SMonitor;
		if (sMonitor != null)
		{
			UiForm.Row("S怪");
			TraditionalText.TextColored(UiState.SMobColor(sMonitor), sMonitor.Finished ? sMonitor.Phase : $"{sMonitor.MobName} · {sMonitor.Phase} · {sMonitor.HpPercent:0.0}%");
		}
		float? leaderDistance = Plugin.GetLeaderDistance();
		if (leaderDistance.HasValue)
		{
			UiForm.Row("車頭距離");
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
		UiSection.Header("目前目標");
		UiForm.BeginMeasured("##overviewTarget", "目標");
		var (arg, _, num) = Plugin.GetTargetInfo();
		UiForm.Row("目標");
		TraditionalText.TextColored(UiState.TargetColor(arg), UiState.SanitizeGlyphs(arg));
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
		UiSection.Header("狀態浮窗外觀", "常駐 狀態浮窗");
		UiForm.BeginActionMeasured("##overviewHud", 32f, "透明度", "精簡模式", "位置鎖定");
		UiForm.Row("透明度", "面板底不透明度（0.62–1.00）");
		UiForm.SliderWithReset("##hudAlpha", () => Config.HudAlpha, (float arg0) =>
		{
			Config.HudAlpha = Math.Clamp(arg0, 0.62f, 1f);
			Save();
		}, 0.62f, 1f, "%.2f", 0.88f);
		UiForm.Row("精簡模式", "常駐時只顯示一行摘要");
		bool v = Config.HudCompact;
		if (ImGui.Checkbox("##hudCompact", ref v))
		{
			Config.HudCompact = v;
			Save();
		}
		UiForm.Row("位置鎖定", "鎖定後 狀態浮窗 不可拖動");
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
				if (UiButton.Draw("復位位置", UiVariant.Neutral, value, "把 狀態浮窗 位置復位到左上角"))
				{
					Plugin.HudWindow.ResetPosition();
					Plugin.PrintEcho("狀態浮窗 位置已復位");
				}
			}
			else
			{
				bool isOpen = Plugin.HudWindow.IsOpen;
				if (UiButton.Draw(isOpen ? "隱藏 狀態浮窗" : "顯示 狀態浮窗", UiVariant.Neutral, value, "顯示 / 隱藏狀態浮窗"))
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
		UiSection.Header("幫助與診斷");
		UiForm.BeginMeasured("##overviewHelp", "入口");
		UiForm.Row("入口");
		UiLayout.EqualColumns("##overviewHelpBtns", 2, 8f, (int arg0) =>
		{
			Vector2 value = new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight);
			if (arg0 == 0)
			{
				if (UiButton.Draw("使用說明", UiVariant.Neutral, value, "開啟內嵌的使用說明視窗"))
				{
					Plugin.HelpWindow.IsOpen = true;
				}
			}
			else if (UiButton.Draw("複製診斷資訊", UiVariant.Neutral, value, "把狀態 / 前置 / 設定摘要複製到剪貼簿") && Diagnostics.CopyReport(Plugin))
			{
				Plugin.PrintEcho("診斷資訊已複製到剪貼簿");
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
			if (UiButton.Draw("復位位置", UiVariant.Neutral, value, "把 狀態浮窗 位置復位到左上角"))
			{
				Plugin.HudWindow.ResetPosition();
				Plugin.PrintEcho("狀態浮窗 位置已復位");
			}
			return;
		}
		bool isOpen = Plugin.HudWindow.IsOpen;
		if (UiButton.Draw(isOpen ? "隱藏 狀態浮窗" : "顯示 狀態浮窗", UiVariant.Neutral, value, "顯示 / 隱藏狀態浮窗"))
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
			if (UiButton.Draw("使用說明", UiVariant.Neutral, value, "開啟內嵌的使用說明視窗"))
			{
				Plugin.HelpWindow.IsOpen = true;
			}
		}
		else if (UiButton.Draw("複製診斷資訊", UiVariant.Neutral, value, "把狀態 / 前置 / 設定摘要複製到剪貼簿") && Diagnostics.CopyReport(Plugin))
		{
			Plugin.PrintEcho("診斷資訊已複製到剪貼簿");
		}
	}
}
