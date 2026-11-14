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
		stringBuilder.AppendLine("=== 自动狩猎跟车 · 诊断信息 ===");
		stringBuilder.Append("生成时间: ").AppendLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture));
		stringBuilder.Append("插件版本: ").AppendLine(typeof(Diagnostics).Assembly.GetName().Version?.ToString() ?? "未知");
		var (value, value2, _) = UiState.RunStatus(arg0);
		stringBuilder.Append("运行状态: ").Append(value2).Append(' ')
			.AppendLine(value);
		stringBuilder.Append("总开关 Enabled: ").AppendLine(config.Enabled ? "开" : "关（受限模式）");
		stringBuilder.Append("免责声明: ").AppendLine(config.DisclaimerAccepted ? "已同意" : "未同意");
		stringBuilder.Append("向导完成: ").AppendLine(config.OnboardingCompleted ? "是" : "否");
		stringBuilder.AppendLine();
		stringBuilder.AppendLine("--- 前置插件 ---");
		stringBuilder.Append("Lifestream: ").AppendLine(AutomaticHunting.IsPluginLoaded("Lifestream") ? "已加载" : "未加载");
		stringBuilder.Append("vnavmesh: ").AppendLine(AutomaticHunting.IsPluginLoaded("vnavmesh") ? "已加载" : "未加载");
		stringBuilder.Append("BossModReborn: ").AppendLine(arg0.IsBmrLoaded() ? "已加载" : "未加载");
		stringBuilder.AppendLine();
		stringBuilder.AppendLine("--- 当前状态 ---");
		stringBuilder.Append("区域: ").AppendLine(AutomaticHunting.GetCurrentTerritoryName());
		stringBuilder.Append("车头: ").AppendLine(string.IsNullOrWhiteSpace(config.LeaderName) ? "未设置" : config.LeaderName);
		float? leaderDistance = arg0.GetLeaderDistance();
		stringBuilder.Append("车头距离: ").AppendLine(leaderDistance.HasValue ? $"{leaderDistance.Value:0.0}m" : "未知");
		var (value3, _, num) = arg0.GetTargetInfo();
		stringBuilder.Append("目标: ").Append(value3);
		stringBuilder.Append(num.HasValue ? $"  ({num.Value:0.0}m)" : string.Empty).AppendLine();
		stringBuilder.Append("导航中: ").AppendLine(arg0.IsNavigating ? "是" : "否");
		stringBuilder.Append("导航阶段: ").AppendLine(arg0.GetNavigationStageText());
		stringBuilder.Append("跨服进行中: ").AppendLine(arg0.CrossWorldInProgress ? "是" : "否");
		AutomaticHunting.SMonitorStatus sMonitor = arg0.SMonitor;
		StringBuilder stringBuilder2;
		StringBuilder.AppendInterpolatedStringHandler handler;
		if (sMonitor != null)
		{
			stringBuilder2 = stringBuilder.Append("S怪监控: ");
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
		stringBuilder.AppendLine("--- 界面 ---");
		stringBuilder.Append("HUD 可见: ").AppendLine(config.HudVisible ? "是" : "否");
		stringBuilder.Append("HUD 透明度: ").AppendLine(config.HudAlpha.ToString("0.00", CultureInfo.InvariantCulture));
		stringBuilder.Append("HUD 紧凑: ").AppendLine(config.HudCompact ? "是" : "否");
		stringBuilder.Append("HUD 锁定: ").AppendLine(config.HudLocked ? "是" : "否");
		stringBuilder2 = stringBuilder.Append("HUD 位置: ");
		StringBuilder stringBuilder4 = stringBuilder2;
		handler = new StringBuilder.AppendInterpolatedStringHandler(2, 2, stringBuilder2);
		handler.AppendFormatted(config.HudPosX, "0");
		handler.AppendLiteral(", ");
		handler.AppendFormatted(config.HudPosY, "0");
		stringBuilder4.AppendLine(ref handler);
		try
		{
			stringBuilder.Append("UI 缩放: ").AppendLine(Theme.Scale.ToString("0.00", CultureInfo.InvariantCulture));
		}
		catch (Exception ex)
		{
			stringBuilder.Append("UI 缩放: 读取失败 ").AppendLine(ex.Message);
		}
		stringBuilder.AppendLine();
		stringBuilder.AppendLine("--- 关键配置 ---");
		stringBuilder2 = stringBuilder.Append("传送阈值: ");
		StringBuilder stringBuilder5 = stringBuilder2;
		handler = new StringBuilder.AppendInterpolatedStringHandler(1, 1, stringBuilder2);
		handler.AppendFormatted(config.TeleportThreshold, "0");
		handler.AppendLiteral("m");
		stringBuilder5.AppendLine(ref handler);
		stringBuilder.Append("飞行导航: ").AppendLine(config.UseFlightNavigation ? "开" : "关");
		stringBuilder.Append("设置 Flag: ").AppendLine(config.SetFlagOnNavigation ? "开" : "关");
		stringBuilder.Append("落地模式: ").AppendLine(config.LandingMode ? "开" : "关");
		stringBuilder.Append("S怪监控: ").AppendLine(config.SEnabled ? "开" : "关");
		stringBuilder2 = stringBuilder.Append("S怪血量阈值: ");
		StringBuilder stringBuilder6 = stringBuilder2;
		handler = new StringBuilder.AppendInterpolatedStringHandler(1, 1, stringBuilder2);
		handler.AppendFormatted(config.SHpThreshold, "0");
		handler.AppendLiteral("%");
		stringBuilder6.AppendLine(ref handler);
		stringBuilder.Append("跨服车头: ").AppendLine(string.IsNullOrWhiteSpace(config.CrossWorldLeader) ? "（空）" : config.CrossWorldLeader);
		stringBuilder2 = stringBuilder.Append("已存跨服配置: ");
		StringBuilder stringBuilder7 = stringBuilder2;
		handler = new StringBuilder.AppendInterpolatedStringHandler(2, 1, stringBuilder2);
		handler.AppendFormatted(config.SavedWorldTravels.Count);
		handler.AppendLiteral(" 条");
		stringBuilder7.AppendLine(ref handler);
		stringBuilder2 = stringBuilder.Append("已存连车配置: ");
		StringBuilder stringBuilder8 = stringBuilder2;
		handler = new StringBuilder.AppendInterpolatedStringHandler(2, 1, stringBuilder2);
		handler.AppendFormatted(config.SavedHuntChains.Count);
		handler.AppendLiteral(" 条");
		stringBuilder8.AppendLine(ref handler);
		stringBuilder2 = stringBuilder.Append("多线地图: ");
		StringBuilder stringBuilder9 = stringBuilder2;
		handler = new StringBuilder.AppendInterpolatedStringHandler(2, 1, stringBuilder2);
		handler.AppendFormatted(config.MultiLineMaps.Count);
		handler.AppendLiteral(" 条");
		stringBuilder9.AppendLine(ref handler);
		stringBuilder.Append("右键菜单: ").AppendLine(config.EnableContextMenu ? "开" : "关");
		return stringBuilder.ToString();
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
			AutomaticHunting.Log.Error(exception, "AutomaticHunting: 复制诊断信息失败");
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
			AutomaticHunting.Log.Error(exception, "AutomaticHunting: 导出配置失败");
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
			AutomaticHunting.Log.Error(exception, "AutomaticHunting: 导出日志失败");
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
