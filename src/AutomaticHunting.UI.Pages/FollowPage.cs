using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using AutomaticHunting.UI.Components;
using Dalamud.Bindings.ImGui;
using Dalamud.Game.ClientState.Objects.Types;

namespace AutomaticHunting.UI.Pages;

public sealed class FollowPage : SettingsPage
{
	private readonly UiLeaderPicker field_891 = new UiLeaderPicker();

	private DateTime field_892 = DateTime.MinValue;

	public FollowPage(AutomaticHunting arg0)
		: base(arg0)
	{
	}

	public override void Draw()
	{
		Method_912();
		UiSection.Gap();
		Method_913();
		UiSection.Gap();
		Method_915();
	}

	private void Method_912()
	{
		UiSection.Header("跟車設定", "車頭名用於識別指令與 地圖標記");
		LeaderRoster.EnsureMigrated(Config);
		UiForm.BeginActionMeasured("##followLeader", 32f, "車頭名稱");
		UiForm.Row("車頭名稱", "輸入名字後按回車儲存；點右側箭頭展開已儲存列表，只有勾選的才生效");
		field_891.Draw("##leaderPicker", Config, Save);
		UiForm.ActionCell();
		if (UiForm.DrawResetButton("##resetLeaderName", string.Empty) && LeaderRoster.ClearChecks(Config))
		{
			Save();
		}
		UiForm.End();
		UiForm.BeginMeasured("##followLeaderActions", "快速設定");
		UiForm.Row("快速設定");
		UiLayout.EqualColumns("##followLeaderBtns", 3, 8f, (int arg0) =>
		{
			Vector2 value = new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight);
			switch (arg0)
			{
			case 0:
				if (UiButton.Draw("目前目標", UiVariant.Neutral, value, "把目前選中的目標加進車頭列表並勾選"))
				{
					IGameObject target = AutomaticHunting.TargetManager.Target;
					if (target != null)
					{
						LeaderRoster.Add(Config, target.Name.TextValue);
						Save();
					}
					else
					{
						Plugin.PrintEcho("未選中目標");
					}
				}
				break;
			case 1:
				if (UiButton.Draw("設定自己", UiVariant.Neutral, value, "把自己加進車頭列表並勾選"))
				{
					Plugin.SetLeaderToSelf();
				}
				break;
			default:
			{
				bool flag = DateTime.UtcNow < field_892;
				if (UiButton.Draw(flag ? "確認清空" : "清除車頭", flag ? UiVariant.Danger : UiVariant.Neutral, value, flag ? "再點一次將刪除全部車頭（不可撤銷）" : "刪除全部已儲存的車頭（需點兩次確認）；只想停用請用左側的「恢復預設值」"))
				{
					if (flag)
					{
						field_892 = DateTime.MinValue;
						int num = Config.LeaderEntries?.Count ?? 0;
						LeaderRoster.ClearAll(Config);
						Save();
						Plugin.PrintEcho((num > 0) ? $"已清空 {num} 個車頭" : "車頭列表本來就是空的");
					}
					else
					{
						field_892 = UiDeleteStyle.ArmNow();
					}
				}
				break;
			}
			}
		});
		UiForm.End();
	}

	private void Method_913()
	{
		UiSection.Header("導航與戰鬥");
		UiForm.BeginMeasured("##followNav", "自動傳送", "A 怪搜尋半徑", "飛行導航", "設定 地圖標記", "目標上空落地", "自動 BMR AI");
		UiForm.Row("自動傳送", "開啟：依標點選擇水晶傳送後導航。關閉：不主動傳送；同圖仍自動飛行，跨圖標點等待隊伍或手動傳送抵達後再導航，並略過自動切線");
		Method_914("##AutoTeleportToFlag", Config.AutoTeleportToFlag, (bool value) =>
		{
			Config.AutoTeleportToFlag = value;
			Save();
		}, Config.AutoTeleportToFlag ? "自動傳送後導航" : "同圖導航，跨圖等待");
		UiForm.Row("A 怪搜尋半徑", "以角色為中心的水平距離，50～500 公尺；預設 200 公尺。接近標點到此距離內就開始搜尋，只能辨識遊戲已載入的怪物");
		UiForm.SliderWithReset("##AMarkSearchRadius", () => AMarkSearchPolicy.NormalizeRadius(Config.AMarkSearchRadius), (float value) =>
		{
			Config.AMarkSearchRadius = AMarkSearchPolicy.NormalizeRadius(value);
			Save();
		}, AMarkSearchPolicy.MinimumRadius, AMarkSearchPolicy.MaximumRadius, "%.0fm", AMarkSearchPolicy.DefaultRadius);
		UiForm.Row("飛行導航", "使用飛行坐騎導航；關閉則使用陸上移動");
		Method_914("##UseFlight", Config.UseFlightNavigation, (bool arg0) =>
		{
			Config.UseFlightNavigation = arg0;
			Save();
		}, Config.UseFlightNavigation ? "使用飛行坐騎" : "使用地面移動");
		UiForm.Row("設定 地圖標記", "導航時在地圖上標記目標點");
		Method_914("##SetFlag", Config.SetFlagOnNavigation, (bool arg0) =>
		{
			Config.SetFlagOnNavigation = arg0;
			Save();
		}, Config.SetFlagOnNavigation ? "導航時標記" : "不標記");
		UiForm.Row("目標上空落地", "到達目標後下坐騎；關閉則懸停在目標上方");
		Method_914("##LandingMode", Config.LandingMode, (bool arg0) =>
		{
			Config.LandingMode = arg0;
			Save();
		}, Config.LandingMode ? "到達後落地" : "懸停在上方");
		UiForm.Row("自動 BMR AI", "進戰鬥開 / 脫戰關");
		Method_914("##AutoBmr", Config.AutoBmrAi, (bool arg0) =>
		{
			Config.AutoBmrAi = arg0;
			Save();
		}, Config.AutoBmrAi ? "僅在野外生效" : "全域性生效");
		UiForm.End();
	}

	private static void Method_914(string arg0, bool arg1, Action<bool> arg2, string arg3)
	{
		UiForm.ToggleWithState(arg0, arg1, arg2, arg3);
	}

	private void Method_915()
	{
		UiSection.Header("S怪設定", "血量達標後突進開怪");
		UiForm.BeginActionMeasured("##followS", 32f, "啟用 S怪", "尋路開怪", "血量檢測", "懸停距離", "懸停高度", "突進技能", "跨服車頭");
		UiForm.Row("啟用 S怪", "先掃 A 怪，無 A 怪再掃 S 怪");
		bool v = Config.SEnabled;
		if (ImGui.Checkbox("##SEnabled", ref v))
		{
			Config.SEnabled = v;
			Save();
		}
		UiForm.Row("尋路開怪", "勾選後血量達標會把 S 當 A 尋路過去；不勾選則直接用突進技能");
		bool v2 = Config.SPathToEngage;
		if (ImGui.Checkbox("##SPathEngage", ref v2))
		{
			Config.SPathToEngage = v2;
			Save();
		}
		if (!v)
		{
			UiForm.End();
			UiEmpty.Show("S怪監控未啟用", "勾選上方「啟用 S怪」後出現門檻等設定");
			return;
		}
		UiForm.Row("血量檢測", "檢測 S 怪血量，低於該百分比時執行突進技能");
		UiForm.SliderWithReset("##SHp", () => Config.SHpThreshold, (float arg0) =>
		{
			Config.SHpThreshold = Math.Clamp(arg0, 0f, 100f);
			Save();
		}, 0f, 100f, "%.0f%%", 80f);
		UiForm.Row("懸停距離", "距 S 怪位置的 XZ 直線距離");
		UiForm.SliderWithReset("##SDist", () => Config.SHoverDistance, (float arg0) =>
		{
			Config.SHoverDistance = Math.Clamp(arg0, 0f, 100f);
			Save();
		}, 0f, 100f, "%.0fm", 50f);
		UiForm.Row("懸停高度", "Y 軸高度");
		UiForm.SliderWithReset("##SHeight", () => Config.SHoverHeight, (float arg0) =>
		{
			Config.SHoverHeight = Math.Clamp(arg0, 0f, 100f);
			Save();
		}, 0f, 100f, "%.0fm", 50f);
		UiForm.Row("突進技能", "血量達標時執行的技能，建議用突進技能");
		UiForm.Fill();
		string buf = Config.SDashCommand;
		if (ImGui.InputText("##SDashCmd", ref buf, 128))
		{
			Config.SDashCommand = buf;
			Save();
		}
		UiForm.ActionCell();
		if (UiForm.DrawResetButton("##resetSDashCmd", GlobalGameData.DefaultDashCommand))
		{
			Config.SDashCommand = GlobalGameData.DefaultDashCommand;
			Save();
		}
		UiForm.Row("跨服車頭（暫時廢棄）", "該功能已暫時停用：不再識別【伺服器名+flag點位+1X】這類跨服指令");
		UiForm.Fill();
		ImGui.BeginDisabled();
		string buf2 = Config.CrossWorldLeader;
		bool flag = ImGui.InputText("##CrossWorldLeader", ref buf2, 128);
		ImGui.EndDisabled();
		if (flag && FeatureFlags.CrossWorldLeaderEnabled)
		{
			Config.CrossWorldLeader = buf2;
			Save();
		}
		UiForm.ActionCell();
		ImGui.BeginDisabled();
		bool flag2 = UiForm.DrawResetButton("##resetCrossWorldLeader", string.Empty);
		ImGui.EndDisabled();
		if (flag2)
		{
			Config.CrossWorldLeader = string.Empty;
			Save();
		}
		UiForm.End();
		UiForm.BeginMeasured("##followSActions", "工具");
		UiForm.Row("工具");
		UiLayout.EqualColumns("##followSActionBtns", 2, 8f, (int arg0) =>
		{
			Vector2 value = new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight);
			if (arg0 == 0)
			{
				if (UiButton.Draw("檢查 國際服 前置", UiVariant.Brand, value, "檢查 Lifestream 和 vnavmesh 的載入狀態"))
				{
					Plugin.LoadDrPresets();
				}
			}
			else if (UiButton.Draw("監控目前目標血量", UiVariant.Neutral, value, "監控目前選中目標的血量，降到門檻後用突進技能開怪"))
			{
				Plugin.StartTargetHpMonitor();
			}
		});
		UiForm.End();
	}

	[CompilerGenerated]
	private void Method_916(int arg0)
	{
		Vector2 value = new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight);
		switch (arg0)
		{
		case 0:
			if (UiButton.Draw("目前目標", UiVariant.Neutral, value, "把目前選中的目標加進車頭列表並勾選"))
			{
				IGameObject target = AutomaticHunting.TargetManager.Target;
				if (target != null)
				{
					LeaderRoster.Add(Config, target.Name.TextValue);
					Save();
				}
				else
				{
					Plugin.PrintEcho("未選中目標");
				}
			}
			return;
		case 1:
			if (UiButton.Draw("設定自己", UiVariant.Neutral, value, "把自己加進車頭列表並勾選"))
			{
				Plugin.SetLeaderToSelf();
			}
			return;
		}
		bool flag = DateTime.UtcNow < field_892;
		if (UiButton.Draw(flag ? "確認清空" : "清除車頭", flag ? UiVariant.Danger : UiVariant.Neutral, value, flag ? "再點一次將刪除全部車頭（不可撤銷）" : "刪除全部已儲存的車頭（需點兩次確認）；只想停用請用左側的「恢復預設值」"))
		{
			if (flag)
			{
				field_892 = DateTime.MinValue;
				int num = Config.LeaderEntries?.Count ?? 0;
				LeaderRoster.ClearAll(Config);
				Save();
				Plugin.PrintEcho((num > 0) ? $"已清空 {num} 個車頭" : "車頭列表本來就是空的");
			}
			else
			{
				field_892 = UiDeleteStyle.ArmNow();
			}
		}
	}

	[CompilerGenerated]
	private void Method_917(bool arg0)
	{
		Config.UseFlightNavigation = arg0;
		Save();
	}

	[CompilerGenerated]
	private void Method_918(bool arg0)
	{
		Config.SetFlagOnNavigation = arg0;
		Save();
	}

	[CompilerGenerated]
	private void Method_919(bool arg0)
	{
		Config.LandingMode = arg0;
		Save();
	}

	[CompilerGenerated]
	private void Method_920(bool arg0)
	{
		Config.AutoBmrAi = arg0;
		Save();
	}

	[CompilerGenerated]
	private float Method_921()
	{
		return Config.SHpThreshold;
	}

	[CompilerGenerated]
	private void Method_922(float arg0)
	{
		Config.SHpThreshold = Math.Clamp(arg0, 0f, 100f);
		Save();
	}

	[CompilerGenerated]
	private float Method_923()
	{
		return Config.SHoverDistance;
	}

	[CompilerGenerated]
	private void Method_924(float arg0)
	{
		Config.SHoverDistance = Math.Clamp(arg0, 0f, 100f);
		Save();
	}

	[CompilerGenerated]
	private float Method_925()
	{
		return Config.SHoverHeight;
	}

	[CompilerGenerated]
	private void Method_926(float arg0)
	{
		Config.SHoverHeight = Math.Clamp(arg0, 0f, 100f);
		Save();
	}

	[CompilerGenerated]
	private void Method_927(int arg0)
	{
		Vector2 value = new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight);
		if (arg0 == 0)
		{
			if (UiButton.Draw("檢查 國際服 前置", UiVariant.Brand, value, "檢查 Lifestream 和 vnavmesh 的載入狀態"))
			{
				Plugin.LoadDrPresets();
			}
		}
		else if (UiButton.Draw("監控目前目標血量", UiVariant.Neutral, value, "監控目前選中目標的血量，降到門檻後用突進技能開怪"))
		{
			Plugin.StartTargetHpMonitor();
		}
	}
}
