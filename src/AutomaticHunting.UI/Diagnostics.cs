using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using AutomaticHunting.UI.Components;
using Dalamud.Bindings.ImGui;

namespace AutomaticHunting.UI;

public static class Diagnostics
{
	[CompilerGenerated]
	public static class RecoveredType_110
	{
		public static Func<string, DateTime> field_661;
	}

	public static string BuildReport(AutomaticHunting arg0)
	{
		PluginConfiguration config = arg0.Config;
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("=== 自動狩獵跟車 · 診斷資訊 ===");
		stringBuilder.Append("產生時間: ").AppendLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture));
		stringBuilder.Append("插件版本: ").AppendLine(typeof(Diagnostics).Assembly.GetName().Version?.ToString() ?? "未知");
		var (value, value2, _) = UiState.RunStatus(arg0);
		stringBuilder.Append("執行狀態: ").Append(value2).Append(' ')
			.AppendLine(value);
		stringBuilder.Append("總開關 Enabled: ").AppendLine(config.Enabled ? "開" : "關（受限模式）");
		stringBuilder.Append("免責宣告: ").AppendLine(config.DisclaimerAccepted ? "已同意" : "未同意");
		stringBuilder.Append("設定精靈完成: ").AppendLine(config.OnboardingCompleted ? "是" : "否");
		stringBuilder.AppendLine();
		stringBuilder.AppendLine("--- 前置插件 ---");
		stringBuilder.Append("Lifestream: ").AppendLine(AutomaticHunting.IsPluginLoaded("Lifestream") ? "已載入" : "未載入");
		stringBuilder.Append("vnavmesh: ").AppendLine(AutomaticHunting.IsPluginLoaded("vnavmesh") ? "已載入" : "未載入");
		stringBuilder.Append("BossModReborn: ").AppendLine(arg0.IsBmrLoaded() ? "已載入" : "未載入");
		stringBuilder.AppendLine();
		stringBuilder.AppendLine("--- 目前狀態 ---");
		stringBuilder.Append("區域: ").AppendLine(AutomaticHunting.GetCurrentTerritoryName());
		stringBuilder.Append("車頭: ").AppendLine(string.IsNullOrWhiteSpace(config.LeaderName) ? "未設定" : config.LeaderName);
		float? leaderDistance = arg0.GetLeaderDistance();
		stringBuilder.Append("車頭距離: ").AppendLine(leaderDistance.HasValue ? $"{leaderDistance.Value:0.0}m" : "未知");
		var (value3, _, num) = arg0.GetTargetInfo();
		stringBuilder.Append("目標: ").Append(value3);
		stringBuilder.Append(num.HasValue ? $"  ({num.Value:0.0}m)" : string.Empty).AppendLine();
		stringBuilder.Append("導航中: ").AppendLine(arg0.IsNavigating ? "是" : "否");
		stringBuilder.Append("導航階段: ").AppendLine(arg0.GetNavigationStageText());
		stringBuilder.Append("跨服進行中: ").AppendLine(arg0.CrossWorldInProgress ? "是" : "否");
		AutomaticHunting.SMonitorStatus sMonitor = arg0.SMonitor;
		StringBuilder stringBuilder2;
		StringBuilder.AppendInterpolatedStringHandler handler;
		if (sMonitor != null)
		{
			stringBuilder2 = stringBuilder.Append("S怪監控: ");
			StringBuilder stringBuilder3 = stringBuilder2;
			handler = new StringBuilder.AppendInterpolatedStringHandler(7, 3, stringBuilder2);
			handler.AppendFormatted(sMonitor.MobName);
			handler.AppendLiteral(" / ");
			handler.AppendFormatted(sMonitor.Phase);
			handler.AppendLiteral(" / ");
			handler.AppendFormatted(sMonitor.HpPercent, "0.0");
			handler.AppendLiteral("%");
			stringBuilder3.AppendLine(ref handler);
		}
		stringBuilder.AppendLine();
		stringBuilder.AppendLine("--- 介面 ---");
		stringBuilder.Append("狀態浮窗 可見: ").AppendLine(config.HudVisible ? "是" : "否");
		stringBuilder.Append("狀態浮窗 透明度: ").AppendLine(config.HudAlpha.ToString("0.00", CultureInfo.InvariantCulture));
		stringBuilder.Append("狀態浮窗 精簡: ").AppendLine(config.HudCompact ? "是" : "否");
		stringBuilder.Append("狀態浮窗 鎖定: ").AppendLine(config.HudLocked ? "是" : "否");
		stringBuilder2 = stringBuilder.Append("狀態浮窗 位置: ");
		StringBuilder stringBuilder4 = stringBuilder2;
		handler = new StringBuilder.AppendInterpolatedStringHandler(2, 2, stringBuilder2);
		handler.AppendFormatted(config.HudPosX, "0");
		handler.AppendLiteral(", ");
		handler.AppendFormatted(config.HudPosY, "0");
		stringBuilder4.AppendLine(ref handler);
		try
		{
			stringBuilder.Append("介面 縮放: ").AppendLine(Theme.Scale.ToString("0.00", CultureInfo.InvariantCulture));
		}
		catch (Exception ex)
		{
			stringBuilder.Append("介面 縮放: 讀取失敗 ").AppendLine(ex.Message);
		}
		stringBuilder.AppendLine();
		stringBuilder.AppendLine("--- 關鍵設定 ---");
		stringBuilder2 = stringBuilder.Append("傳送門檻: ");
		StringBuilder stringBuilder5 = stringBuilder2;
		handler = new StringBuilder.AppendInterpolatedStringHandler(1, 1, stringBuilder2);
		handler.AppendFormatted(config.TeleportThreshold, "0");
		handler.AppendLiteral("m");
		stringBuilder5.AppendLine(ref handler);
		stringBuilder.Append("飛行導航: ").AppendLine(config.UseFlightNavigation ? "開" : "關");
		stringBuilder.Append("設定 地圖標記: ").AppendLine(config.SetFlagOnNavigation ? "開" : "關");
		stringBuilder.Append("落地模式: ").AppendLine(config.LandingMode ? "開" : "關");
		stringBuilder.Append("S怪監控: ").AppendLine(config.SEnabled ? "開" : "關");
		stringBuilder2 = stringBuilder.Append("S怪血量門檻: ");
		StringBuilder stringBuilder6 = stringBuilder2;
		handler = new StringBuilder.AppendInterpolatedStringHandler(1, 1, stringBuilder2);
		handler.AppendFormatted(config.SHpThreshold, "0");
		handler.AppendLiteral("%");
		stringBuilder6.AppendLine(ref handler);
		stringBuilder.Append("跨服車頭: ").AppendLine(string.IsNullOrWhiteSpace(config.CrossWorldLeader) ? "（空）" : config.CrossWorldLeader);
		stringBuilder2 = stringBuilder.Append("已存跨服設定: ");
		StringBuilder stringBuilder7 = stringBuilder2;
		handler = new StringBuilder.AppendInterpolatedStringHandler(2, 1, stringBuilder2);
		handler.AppendFormatted(config.SavedWorldTravels.Count);
		handler.AppendLiteral(" 條");
		stringBuilder7.AppendLine(ref handler);
		stringBuilder2 = stringBuilder.Append("已存連車設定: ");
		StringBuilder stringBuilder8 = stringBuilder2;
		handler = new StringBuilder.AppendInterpolatedStringHandler(2, 1, stringBuilder2);
		handler.AppendFormatted(config.SavedHuntChains.Count);
		handler.AppendLiteral(" 條");
		stringBuilder8.AppendLine(ref handler);
		stringBuilder2 = stringBuilder.Append("多線地圖: ");
		StringBuilder stringBuilder9 = stringBuilder2;
		handler = new StringBuilder.AppendInterpolatedStringHandler(2, 1, stringBuilder2);
		handler.AppendFormatted(config.MultiLineMaps.Count);
		handler.AppendLiteral(" 條");
		stringBuilder9.AppendLine(ref handler);
		stringBuilder.Append("右鍵選單: ").AppendLine(config.EnableContextMenu ? "開" : "關");
		return TraditionalText.Translate(stringBuilder.ToString());
	}

	public static bool CopyReport(AutomaticHunting arg0)
	{
		try
		{
			ImGui.SetClipboardText(BuildReport(arg0));
			return true;
		}
		catch (Exception exception)
		{
			AutomaticHunting.Log.Error(exception, "自動狩獵跟車：複製診斷資訊失敗");
			return false;
		}
	}

	public static string ExportConfig(AutomaticHunting arg0)
	{
		try
		{
			string text = Path.Combine(Method_687(), "AutomaticHunting-config-" + Method_690() + ".json");
			string contents = JsonSerializer.Serialize(options: new JsonSerializerOptions
			{
				WriteIndented = true
			}, value: arg0.Config);
			File.WriteAllText(text, contents, Encoding.UTF8);
			return text;
		}
		catch (Exception exception)
		{
			AutomaticHunting.Log.Error(exception, "自動狩獵跟車：匯出設定失敗");
			return string.Empty;
		}
	}

	public static string ExportLog(AutomaticHunting arg0)
	{
		try
		{
			string path = Method_687();
			string text = Method_689();
			if (!string.IsNullOrEmpty(text))
			{
				string text2 = Path.Combine(path, "AutomaticHunting-log-" + Method_690() + ".log");
				File.Copy(text, text2, overwrite: true);
				return text2;
			}
			string text3 = Path.Combine(path, "AutomaticHunting-diag-" + Method_690() + ".txt");
			File.WriteAllText(text3, BuildReport(arg0), Encoding.UTF8);
			return text3;
		}
		catch (Exception exception)
		{
			AutomaticHunting.Log.Error(exception, "自動狩獵跟車：匯出日誌失敗");
			return string.Empty;
		}
	}

	private static string Method_687()
	{
		string text = Path.Combine(GetPluginDirectory(), "exports");
		Directory.CreateDirectory(text);
		return text;
	}

	public static string GetPluginDirectory()
	{
		try
		{
			DirectoryInfo configDirectory = AutomaticHunting.PluginInterface.ConfigDirectory;
			if (configDirectory != null && !string.IsNullOrEmpty(configDirectory.FullName))
			{
				Directory.CreateDirectory(configDirectory.FullName);
				return configDirectory.FullName;
			}
		}
		catch (Exception)
		{
		}
		string text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "XIVLauncher", "pluginConfigs", "AutomaticHunting");
		Directory.CreateDirectory(text);
		return text;
	}

	private static string Method_689()
	{
		foreach (string item in new List<string>
		{
			Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "XIVLauncher", "log"),
			Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "XIVLauncherCN", "log"),
			Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "XIVLauncher", "logs"),
			Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "XIVLauncherCN", "logs")
		})
		{
			if (Directory.Exists(item))
			{
				string text = Directory.EnumerateFiles(item, "*.log").OrderByDescending(File.GetLastWriteTimeUtc).FirstOrDefault();
				if (text != null)
				{
					return text;
				}
			}
		}
		return string.Empty;
	}

	private static string Method_690()
	{
		return DateTime.Now.ToString("yyyyMMdd-HHmmss", CultureInfo.InvariantCulture);
	}
}
