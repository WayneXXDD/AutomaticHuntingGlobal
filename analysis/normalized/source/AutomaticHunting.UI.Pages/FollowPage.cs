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
		UiSection.Header("跟车设置", "车头名用于识别指令与 Flag");
		LeaderRoster.EnsureMigrated(Config);
		UiForm.BeginActionMeasured("##followLeader", 32f, "车头名称");
		UiForm.Row("车头名称", "输入名字后按回车保存；点右侧箭头展开已保存列表，只有勾选的才生效");
		field_891.Draw("##leaderPicker", Config, Save);
		UiForm.ActionCell();
		if (UiForm.DrawResetButton("##resetLeaderName", string.Empty) && LeaderRoster.ClearChecks(Config))
		{
			Save();
		}
		UiForm.End();
		UiForm.BeginMeasured("##followLeaderActions", "快速设置");
		UiForm.Row("快速设置");
		UiLayout.EqualColumns("##followLeaderBtns", 3, 8f, (int arg0) =>
		{
			Vector2 value = new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight);
			switch (arg0)
			{
			case 0:
				if (UiButton.Draw("当前目标", UiVariant.Neutral, value, "把当前选中的目标加进车头列表并勾选"))
				{
					IGameObject target = AutomaticHunting.TargetManager.Target;
					if (target != null)
					{
						LeaderRoster.Add(Config, target.Name.TextValue);
						Save();
					}
					else
					{
						Plugin.PrintEcho("未选中目标");
					}
				}
				break;
			case 1:
				if (UiButton.Draw("设置自己", UiVariant.Neutral, value, "把自己加进车头列表并勾选"))
				{
					Plugin.SetLeaderToSelf();
				}
				break;
			default:
			{
				bool flag = DateTime.UtcNow < field_892;
				if (UiButton.Draw(flag ? "确认清空" : "清除车头", flag ? UiVariant.Danger : UiVariant.Neutral, value, flag ? "再点一次将删除全部车头（不可撤销）" : "删除全部已保存的车头（需点两次确认）；只想停用请用左侧的「恢复默认值」"))
				{
					if (flag)
					{
						field_892 = DateTime.MinValue;
						int num = Config.LeaderEntries?.Count ?? 0;
						LeaderRoster.ClearAll(Config);
						Save();
						Plugin.PrintEcho((num > 0) ? $"已清空 {num} 个车头" : "车头列表本来就是空的");
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
		UiSection.Header("导航与战斗");
		UiForm.BeginMeasured("##followNav", "飞行导航", "设置 Flag", "目标上空落地", "自动 BMR AI");
		UiForm.Row("飞行导航", "使用飞行坐骑导航；关闭则使用陆上移动");
		Method_914("##UseFlight", Config.UseFlightNavigation, (bool arg0) =>
		{
			Config.UseFlightNavigation = arg0;
			Save();
		}, Config.UseFlightNavigation ? "使用飞行坐骑" : "使用地面移动");
		UiForm.Row("设置 Flag", "导航时在地图上标记目标点");
		Method_914("##SetFlag", Config.SetFlagOnNavigation, (bool arg0) =>
		{
			Config.SetFlagOnNavigation = arg0;
			Save();
		}, Config.SetFlagOnNavigation ? "导航时标记" : "不标记");
		UiForm.Row("目标上空落地", "到达目标后下坐骑；关闭则悬停在目标上方");
		Method_914("##LandingMode", Config.LandingMode, (bool arg0) =>
		{
			Config.LandingMode = arg0;
			Save();
		}, Config.LandingMode ? "到达后落地" : "悬停在上方");
		UiForm.Row("自动 BMR AI", "进战斗开 / 脱战关");
		Method_914("##AutoBmr", Config.AutoBmrAi, (bool arg0) =>
		{
			Config.AutoBmrAi = arg0;
			Save();
		}, Config.AutoBmrAi ? "仅在野外生效" : "全局生效");
		UiForm.End();
	}

	private static void Method_914(string arg0, bool arg1, Action<bool> arg2, string arg3)
	{
		UiForm.ToggleWithState(arg0, arg1, arg2, arg3);
	}

	private void Method_915()
	{
		UiSection.Header("S怪配置", "血量达标后突进开怪");
		UiForm.BeginActionMeasured("##followS", 32f, "启用 S怪", "寻路开怪", "血量检测", "悬停距离", "悬停高度", "突进技能", "跨服车头");
		UiForm.Row("启用 S怪", "先扫 A 怪，无 A 怪再扫 S 怪");
		bool v = Config.SEnabled;
		if (ImGui.Checkbox("##SEnabled", ref v))
		{
			Config.SEnabled = v;
			Save();
		}
		UiForm.Row("寻路开怪", "勾选后血量达标会把 S 当 A 寻路过去；不勾选则直接用突进技能");
		bool v2 = Config.SPathToEngage;
		if (ImGui.Checkbox("##SPathEngage", ref v2))
		{
			Config.SPathToEngage = v2;
			Save();
		}
		if (!v)
		{
			UiForm.End();
			UiEmpty.Show("S怪监控未启用", "勾选上方「启用 S怪」后出现阈值等设置");
			return;
		}
		UiForm.Row("血量检测", "检测 S 怪血量，低于该百分比时执行突进技能");
		UiForm.SliderWithReset("##SHp", () => Config.SHpThreshold, (float arg0) =>
		{
			Config.SHpThreshold = Math.Clamp(arg0, 0f, 100f);
			Save();
		}, 0f, 100f, "%.0f%%", 80f);
		UiForm.Row("悬停距离", "距 S 怪位置的 XZ 直线距离");
		UiForm.SliderWithReset("##SDist", () => Config.SHoverDistance, (float arg0) =>
		{
			Config.SHoverDistance = Math.Clamp(arg0, 0f, 100f);
			Save();
		}, 0f, 100f, "%.0fm", 50f);
		UiForm.Row("悬停高度", "Y 轴高度");
		UiForm.SliderWithReset("##SHeight", () => Config.SHoverHeight, (float arg0) =>
		{
			Config.SHoverHeight = Math.Clamp(arg0, 0f, 100f);
			Save();
		}, 0f, 100f, "%.0fm", 50f);
		UiForm.Row("突进技能", "血量达标时执行的技能，建议用突进技能");
		UiForm.Fill();
		string buf = Config.SDashCommand;
		if (ImGui.InputText("##SDashCmd", ref buf, 128))
		{
			Config.SDashCommand = buf;
			Save();
		}
		UiForm.ActionCell();
		if (UiForm.DrawResetButton("##resetSDashCmd", "/ac 调停"))
		{
			Config.SDashCommand = "/ac 调停";
			Save();
		}
		UiForm.Row("跨服车头（暂时废弃）", "该功能已暂时停用：不再识别【服务器名+flag点位+1X】这类跨服指令");
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
				if (UiButton.Draw("启动 DR 前置", UiVariant.Brand, value, "加载 /pdr LimitlessTargetDashAction / AutoDismount / FastWorldTravel / InstantDismount 前置"))
				{
					Plugin.LoadDrPresets();
				}
			}
			else if (UiButton.Draw("监控当前目标血量", UiVariant.Neutral, value, "监控当前选中目标的血量，降到阈值后用突进技能开怪"))
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
			if (UiButton.Draw("当前目标", UiVariant.Neutral, value, "把当前选中的目标加进车头列表并勾选"))
			{
				IGameObject target = AutomaticHunting.TargetManager.Target;
				if (target != null)
				{
					LeaderRoster.Add(Config, target.Name.TextValue);
					Save();
				}
				else
				{
					Plugin.PrintEcho("未选中目标");
				}
			}
			return;
		case 1:
			if (UiButton.Draw("设置自己", UiVariant.Neutral, value, "把自己加进车头列表并勾选"))
			{
				Plugin.SetLeaderToSelf();
			}
			return;
		}
		bool flag = DateTime.UtcNow < field_892;
		if (UiButton.Draw(flag ? "确认清空" : "清除车头", flag ? UiVariant.Danger : UiVariant.Neutral, value, flag ? "再点一次将删除全部车头（不可撤销）" : "删除全部已保存的车头（需点两次确认）；只想停用请用左侧的「恢复默认值」"))
		{
			if (flag)
			{
				field_892 = DateTime.MinValue;
				int num = Config.LeaderEntries?.Count ?? 0;
				LeaderRoster.ClearAll(Config);
				Save();
				Plugin.PrintEcho((num > 0) ? $"已清空 {num} 个车头" : "车头列表本来就是空的");
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
			if (UiButton.Draw("启动 DR 前置", UiVariant.Brand, value, "加载 /pdr LimitlessTargetDashAction / AutoDismount / FastWorldTravel / InstantDismount 前置"))
			{
				Plugin.LoadDrPresets();
			}
		}
		else if (UiButton.Draw("监控当前目标血量", UiVariant.Neutral, value, "监控当前选中目标的血量，降到阈值后用突进技能开怪"))
		{
			Plugin.StartTargetHpMonitor();
		}
	}
}
