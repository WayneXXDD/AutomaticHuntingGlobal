using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using AutomaticHunting.UI.Components;
using Dalamud.Bindings.ImGui;

namespace AutomaticHunting.UI.Pages;

public sealed class AdvancedPage : SettingsPage
{
	[CompilerGenerated]
	private sealed class RecoveredType_129
	{
		public int field_850;

		public AdvancedPage field_851;

		public List<MultiLineMapConfig> field_852;

		public Action<int> field_853;

		internal void Method_869(int arg0)
		{
			field_850 = arg0;
		}

		internal void Method_870(int arg0)
		{
			Vector2 value = new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight);
			if (arg0 == 0)
			{
				if (UiButton.Draw("添加多线地图配置", UiVariant.Neutral, value, "新增一条多线地图配置"))
				{
					field_852.Add(new MultiLineMapConfig());
					field_851.Save();
				}
			}
			else if (UiButton.Draw("开启 DR 快捷副本区切换", UiVariant.Brand, value, "执行 /pdr load FastInstanceZoneChange"))
			{
				field_851.Plugin.LoadFastInstanceSwitch();
			}
		}
	}

	[CompilerGenerated]
	private sealed class RecoveredType_130
	{
		public MultiLineMapConfig field_854;

		public RecoveredType_129 field_855;

		internal bool Method_872(string arg0)
		{
			ImGui.SetNextItemWidth(-1f);
			string buf = field_854.MapId;
			if (!ImGui.InputText(arg0, ref buf, 32))
			{
				return false;
			}
			field_854.MapId = buf;
			field_855.field_851.Save();
			return true;
		}

		internal bool Method_873(string arg0)
		{
			ImGui.SetNextItemWidth(-1f);
			int v = field_854.InstanceCount;
			if (!ImGui.SliderInt(arg0, ref v, 1, 10, "%d条线"))
			{
				return false;
			}
			field_854.InstanceCount = Math.Clamp(v, 1, 10);
			field_855.field_851.Save();
			return true;
		}

		internal bool Method_874(string arg0)
		{
			ImGui.SetNextItemWidth(-1f);
			int v = field_854.AMobCount;
			if (!ImGui.SliderInt(arg0, ref v, 1, 2, "%d只"))
			{
				return false;
			}
			field_854.AMobCount = Math.Clamp(v, 1, 2);
			field_855.field_851.Save();
			return true;
		}
	}

	private readonly UiCardList field_846 = new UiCardList();

	private readonly UiConfirm field_847 = new UiConfirm();

	private readonly UiConfirm field_848 = new UiConfirm();

	private bool field_849;

	public AdvancedPage(AutomaticHunting arg0)
		: base(arg0)
	{
	}

	public override void Draw()
	{
		Method_849();
		UiSection.Gap();
		Method_850();
		UiSection.Gap();
		Method_851();
		UiSection.Gap();
		Method_852();
		UiSection.Gap();
		Method_853();
		UiSection.Gap();
		Method_854();
		field_848.Draw();
		field_847.Draw();
	}

	private void Method_849()
	{
		UiSection.Header("准备配置");
		UiForm.BeginActionMeasured("##advPrep", 32f, "传送阈值", "等待时间", "车头监听频道", "右键菜单");
		UiForm.Row("传送阈值", "直接前往标点的距离比经以太之晶传送后远超过此值时自动传送。值大更倾向直接导航，值小更倾向传送");
		UiForm.SliderWithReset("##Threshold", () => Config.TeleportThreshold, (float arg0) =>
		{
			Config.TeleportThreshold = Math.Clamp(arg0, 100f, 1000f);
			Save();
		}, 100f, 1000f, "%.0fm", 300f);
		UiForm.Row("等待时间", "传送读条结束后等待的时间，用于等待地图状态稳定");
		UiForm.SliderWithReset("##Wait", () => Config.WaitAfterTeleport, (float arg0) =>
		{
			Config.WaitAfterTeleport = Math.Clamp(arg0, 0f, 120f);
			Save();
		}, 0f, 120f, "%.0fs", 1f);
		UiForm.Row("车头监听频道", "只喊话=仅识别喊话(/sh,全图)频道；全部=任意频道都识别。/y(呼喊)不是喊话频道");
		UiLayout.EqualColumns("##advYellRow", 2, 8f, (int arg0) =>
		{
			if (arg0 == 0)
			{
				if (ImGui.RadioButton("只喊话##LeaderYellOnly", Config.LeaderYellOnly))
				{
					Config.LeaderYellOnly = true;
					Save();
				}
			}
			else if (ImGui.RadioButton("全部##LeaderYellAll", !Config.LeaderYellOnly))
			{
				Config.LeaderYellOnly = false;
				Save();
			}
		});
		UiForm.ActionCell();
		UiForm.Row("右键菜单", "在玩家右键菜单中显示「AH 添加为车头」");
		bool v = Config.EnableContextMenu;
		if (ImGui.Checkbox("##EnableCtx", ref v))
		{
			Config.EnableContextMenu = v;
			Save();
		}
		UiForm.End();
	}

	private void Method_850()
	{
		UiSection.Header("多线地图", "仅配置了地图ID的地图会启用多线流程");
		List<MultiLineMapConfig> field_852 = Config.MultiLineMaps;
		int field_853 = -1;
		if (field_852.Count == 0)
		{
			UiEmpty.Show("还没有多线地图配置", "填入地图ID后启用切线与A怪计数流程");
		}
		else
		{
			List<UiListCard> list = new List<UiListCard>(field_852.Count);
			for (int i = 0; i < field_852.Count; i++)
			{
				MultiLineMapConfig field_854 = field_852[i];
				UiListCard uiListCard = new UiListCard
				{
					Id = $"ml{i}",
					Title = (string.IsNullOrWhiteSpace(field_854.MapId) ? "（未填写地图 ID）" : field_854.MapId),
					Subtitle = $"{field_854.InstanceCount} 条线 · {field_854.AMobCount} 只 A 怪",
					Draggable = false,
					DeleteTooltip = "移除这条多线地图配置",
					OnDelete = (int arg0) =>
					{
						field_853 = arg0;
					}
				};
				uiListCard.ExtraColumns.Add((string arg0) =>
				{
					ImGui.SetNextItemWidth(-1f);
					string buf = field_854.MapId;
					if (!ImGui.InputText(arg0, ref buf, 32))
					{
						return false;
					}
					field_854.MapId = buf;
					Save();
					return true;
				});
				uiListCard.ExtraColumns.Add((string arg0) =>
				{
					ImGui.SetNextItemWidth(-1f);
					int v2 = field_854.InstanceCount;
					if (!ImGui.SliderInt(arg0, ref v2, 1, 10, "%d条线"))
					{
						return false;
					}
					field_854.InstanceCount = Math.Clamp(v2, 1, 10);
					Save();
					return true;
				});
				uiListCard.ExtraColumns.Add((string arg0) =>
				{
					ImGui.SetNextItemWidth(-1f);
					int v2 = field_854.AMobCount;
					if (!ImGui.SliderInt(arg0, ref v2, 1, 2, "%d只"))
					{
						return false;
					}
					field_854.AMobCount = Math.Clamp(v2, 1, 2);
					Save();
					return true;
				});
				list.Add(uiListCard);
			}
			field_846.Draw("##mlCard", "ML_REORDER", list);
		}
		if (field_853 >= 0 && field_853 < field_852.Count)
		{
			field_852.RemoveAt(field_853);
			Save();
		}
		UiForm.BeginMeasured("##advMultiLine", "切线兜底");
		UiForm.Row("切线兜底", "发送切线命令5秒后真实线未变化，先传送到最近以太之光再重发一次");
		bool v = Config.SwitchRetryAtAetheryte;
		if (ImGui.Checkbox("##SwitchRetry", ref v))
		{
			Config.SwitchRetryAtAetheryte = v;
			Save();
		}
		UiForm.End();
		UiLayout.EqualColumns("##advMultiLineBtns", 2, 8f, (int arg0) =>
		{
			Vector2 value = new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight);
			if (arg0 == 0)
			{
				if (UiButton.Draw("添加多线地图配置", UiVariant.Neutral, value, "新增一条多线地图配置"))
				{
					field_852.Add(new MultiLineMapConfig());
					Save();
				}
			}
			else if (UiButton.Draw("开启 DR 快捷副本区切换", UiVariant.Brand, value, "执行 /pdr load FastInstanceZoneChange"))
			{
				Plugin.LoadFastInstanceSwitch();
			}
		});
	}

	private void Method_851()
	{
		UiSection.Header("坐骑", "选择导航使用的坐骑");
		if (!field_849)
		{
			Plugin.RefreshMountList();
			field_849 = true;
		}
		if (Plugin.SelectedMountIndex >= 0 && Plugin.SelectedMountIndex < Plugin.MountNames.Length)
		{
			ImU8String text = new ImU8String(3, 1);
			text.AppendLiteral("当前：");
			text.AppendFormatted(Plugin.MountNames[Plugin.SelectedMountIndex]);
			ImGui.TextDisabled(text);
		}
		UiLayout.EqualColumns("##mountSearchRow", 2, 8f, (int arg0) =>
		{
			if (arg0 == 0)
			{
				string buf = Plugin.MountSearchText;
				ImGui.SetNextItemWidth(-1f);
				if (ImGui.InputTextWithHint("##MountSearch", "搜索坐骑名称...", ref buf, 64))
				{
					Plugin.MountSearchText = buf;
				}
			}
			else if (UiButton.Draw("刷新", UiVariant.Neutral, new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight), "重新读取可用坐骑列表"))
			{
				Plugin.RefreshMountList();
				field_849 = true;
			}
		});
		List<int> list = new List<int>();
		for (int num = 0; num < Plugin.MountNames.Length; num++)
		{
			if (string.IsNullOrWhiteSpace(Plugin.MountSearchText) || Plugin.MountNames[num].Contains(Plugin.MountSearchText, StringComparison.OrdinalIgnoreCase))
			{
				list.Add(num);
			}
		}
		if (list.Count == 0)
		{
			UiEmpty.Show("没有匹配的坐骑", "换个关键词，或点「刷新」重新读取列表");
			return;
		}
		float y = (float)Math.Min(list.Count, 6) * ImGui.GetTextLineHeightWithSpacing();
		if (!ImGui.BeginListBox("##MountList", new Vector2(ImGui.GetContentRegionAvail().X, y)))
		{
			return;
		}
		foreach (int item in list)
		{
			bool selected = item == Plugin.SelectedMountIndex;
			if (ImGui.Selectable(Plugin.MountNames[item], selected))
			{
				Plugin.SelectedMountIndex = item;
				Plugin.UpdateMountCommand();
			}
		}
		ImGui.EndListBox();
	}

	private void Method_852()
	{
		UiSection.Header("狩猎招募", "创建招募时使用的描述");
		ImGui.TextDisabled("描述可留空；≤2 行、UTF-8 ≤192 字节。");
		string buf = Config.PfComment ?? string.Empty;
		if (ImGui.InputTextMultiline("##PfComment", ref buf, 256, new Vector2(ImGui.GetContentRegionAvail().X, 62f)))
		{
			Config.PfComment = buf;
			Save();
		}
		if (UiButton.Draw("创建狩猎招募", UiVariant.Brand, new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight), "立即创建一次狩猎招募"))
		{
			HuntPfController.Create(Config.PfComment);
		}
	}

	private void Method_853()
	{
		UiSection.Header("调试工具");
		UiForm.BeginMeasured("##advDebugFlags", "输出日志", "显示坐标距离", "A怪计数");
		UiForm.Row("输出日志", "把插件运行日志输出到聊天框");
		bool v = Config.EchoMessages;
		if (ImGui.Checkbox("##Echo", ref v))
		{
			Config.EchoMessages = v;
			Save();
		}
		UiForm.Row("显示坐标距离", "在调试区显示玩家与 Flag 的坐标距离");
		bool v2 = Config.ShowDistances;
		if (ImGui.Checkbox("##ShowDist", ref v2))
		{
			Config.ShowDistances = v2;
			Save();
		}
		UiForm.Row("A怪计数", "勾选后，未配置多线地图的地图也进行 A 怪计数（方便测试）");
		bool v3 = Config.DebugCountAInAllMaps;
		if (ImGui.Checkbox("##DebugCountA", ref v3))
		{
			Config.DebugCountAInAllMaps = v3;
			Save();
		}
		UiForm.End();
		UiLayout.EqualColumns("##advDebugBtns1", 3, 8f, (int arg0) =>
		{
			Vector2 value2 = new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight);
			switch (arg0)
			{
			case 0:
				if (UiButton.Draw("切线", UiVariant.Neutral, value2, "切到下一条副本线"))
				{
					Plugin.SwitchToNextInstance();
				}
				break;
			case 1:
				if (UiButton.Draw("当前线路", UiVariant.Neutral, value2, "在聊天框打印当前与目标线路号"))
				{
					int currentInstanceNumber = Plugin.GetCurrentInstanceNumber();
					int currentInstanceNumber2 = Config.CurrentInstanceNumber;
					string value3 = ((currentInstanceNumber > 0) ? $"{currentInstanceNumber}线" : "非分线/读取失败");
					Plugin.PrintEcho($"当前线路: {value3} (目标线: {currentInstanceNumber2})");
				}
				break;
			default:
				if (UiButton.Draw("召唤坐骑", UiVariant.Neutral, value2, "立即召唤当前配置的坐骑"))
				{
					Plugin.SummonMount();
				}
				break;
			}
		});
		UiLayout.EqualColumns("##advDebugBtns2", 2, 8f, (int arg0) =>
		{
			Vector2 value2 = new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight);
			if (arg0 == 0)
			{
				if (UiButton.Draw("下坐骑", UiVariant.Neutral, value2, "立即下坐骑"))
				{
					AutomaticHunting.UseDismount();
				}
			}
			else if (UiButton.Draw("选中A怪", UiVariant.Neutral, value2, "扫描并选中附近的 A 怪"))
			{
				Plugin.ScanAndSelectTarget();
			}
		});
		ImGui.Spacing();
		if (UiButton.Draw("测试突进：" + Config.SDashCommand, UiVariant.Neutral, new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight), "立即执行一次突进技能命令（不影响自动流程）"))
		{
			Plugin.ExecuteDashSkill();
		}
		string currentTerritoryName = AutomaticHunting.GetCurrentTerritoryName();
		if (!string.IsNullOrWhiteSpace(currentTerritoryName) && Config.ARealmMarks.TryGetValue(currentTerritoryName, out string[] value))
		{
			ImGui.Spacing();
			ImU8String text = new ImU8String(5, 1);
			text.AppendLiteral("当前区域：");
			text.AppendFormatted(currentTerritoryName);
			ImGui.TextDisabled(text);
			ImU8String text2 = new ImU8String(3, 1);
			text2.AppendLiteral("A怪：");
			text2.AppendFormatted(string.Join(", ", value));
			ImGui.TextDisabled(text2);
		}
		ImU8String text3 = new ImU8String(6, 1);
		text3.AppendLiteral("已启用车头：");
		text3.AppendFormatted(string.IsNullOrWhiteSpace(Config.LeaderName) ? "未设置" : Config.LeaderName);
		ImGui.TextDisabled(text3);
		if (Config.ShowDistances)
		{
			Method_857();
		}
	}

	private void Method_854()
	{
		UiSection.Header("诊断与导出");
		UiLayout.EqualColumns("##diagBtns1", 3, 8f, (int arg0) =>
		{
			Vector2 value = new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight);
			switch (arg0)
			{
			case 0:
				if (UiButton.Draw("复制诊断信息", UiVariant.Neutral, value, "把状态 / 前置 / 配置摘要复制到剪贴板") && Diagnostics.CopyReport(Plugin))
				{
					Plugin.PrintEcho("诊断信息已复制到剪贴板");
				}
				break;
			case 1:
				if (UiButton.Draw("导出日志", UiVariant.Neutral, value, "把 Dalamud 日志导出到插件目录"))
				{
					string text2 = Diagnostics.ExportLog(Plugin);
					if (!string.IsNullOrEmpty(text2))
					{
						Plugin.PrintEcho("日志已导出: " + text2);
					}
				}
				break;
			default:
				if (UiButton.Draw("导出配置", UiVariant.Neutral, value, "把当前配置导出为 JSON 文件"))
				{
					string text = Diagnostics.ExportConfig(Plugin);
					if (!string.IsNullOrEmpty(text))
					{
						Plugin.PrintEcho("配置已导出: " + text);
					}
				}
				break;
			}
		});
		UiLayout.EqualColumns("##diagBtns2", 2, 8f, (int arg0) =>
		{
			Vector2 value = new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight);
			if (arg0 == 0)
			{
				if (UiButton.Draw("恢复默认", UiVariant.Neutral, value, "只重置行为开关（不影响已保存的配置与步骤）"))
				{
					field_848.Show("恢复默认设置", "将重置以下行为开关为出厂默认值。已保存的跨服配置、连车配置与车头名称不受影响。", new string[4] { "传送阈值 / 等待时间", "导航与战斗选项", "S怪监控选项", "多线 / 招募 / 右键菜单开关" }, string.Empty, "恢复默认", UiVariant.Danger, Method_855);
				}
			}
			else if (UiButton.Draw("清空全部", UiVariant.DangerSolid, value, "清空所有配置（含已保存的跨服与连车配置）"))
			{
				field_847.Show("清空全部配置", "此操作不可撤销：所有配置项（含车头、已保存的一键跨服配置、连车配置、多线地图）都会被清空，插件回到首次运行状态。", new string[4] { "车头名称与全部行为开关", "已保存的一键跨服配置", "已保存的连车配置", "多线地图与黑名单" }, "清空", "确认清空", UiVariant.DangerSolid, Method_856);
			}
		});
	}

	private void Method_855()
	{
		Config.TeleportThreshold = 300f;
		Config.WaitAfterTeleport = 1f;
		Config.UseFlightNavigation = true;
		Config.EchoMessages = true;
		Config.SetFlagOnNavigation = true;
		Config.ShowOverlay = true;
		Config.ShowDistances = false;
		Config.LandingMode = true;
		Config.AutoBmrAi = true;
		Config.SEnabled = false;
		Config.SHpThreshold = 80f;
		Config.SHoverDistance = 50f;
		Config.SHoverHeight = 50f;
		Config.SDashCommand = "/ac 调停";
		Config.SPathToEngage = false;
		Config.EnableContextMenu = true;
		Config.LeaderYellOnly = true;
		Config.SwitchRetryAtAetheryte = true;
		Config.MultiLineMaps.Clear();
		Config.HuntChain.Enabled = true;
		Config.HuntChain.CreatePfAfterTravel = false;
		Config.HuntChain.Steps.Clear();
		Config.PfComment = string.Empty;
		Save();
		Plugin.PrintEcho("已恢复默认行为开关（已保存的配置与车头未改动）");
	}

	private void Method_856()
	{
		LeaderRoster.ClearAll(Config);
		Config.CrossWorldLeader = string.Empty;
		Config.ManualServerName = string.Empty;
		Config.ManualMapRowId = 0u;
		Config.ManualAetheryteId = 0u;
		Config.SavedWorldTravels.Clear();
		Config.SavedHuntChains.Clear();
		Config.HuntChain.Steps.Clear();
		Config.MultiLineMaps.Clear();
		Config.PfComment = string.Empty;
		Config.OnboardingCompleted = false;
		Config.OnboardingStep = 0;
		Config.DisclaimerAccepted = false;
		Config.Enabled = false;
		Config.DataClearAcknowledged = true;
		Save();
		Plugin.PrintEcho("已清空全部配置，插件回到首次运行状态（请重新完成向导）");
		Plugin.OnboardingWindow.Open();
	}

	private void Method_857()
	{
		Vector3? localPlayerPosition = AutomaticHunting.GetLocalPlayerPosition();
		if (!localPlayerPosition.HasValue)
		{
			ImGui.TextDisabled("无法获取玩家位置");
			return;
		}
		ImGui.Spacing();
		ImGui.TextColored(Theme.Brand, "坐标距离");
		ImU8String text = new ImU8String(7, 2);
		text.AppendLiteral("玩家：(");
		text.AppendFormatted(localPlayerPosition.Value.X, "0.0");
		text.AppendLiteral(", ");
		text.AppendFormatted(localPlayerPosition.Value.Z, "0.0");
		text.AppendLiteral(")");
		ImGui.Text(text);
		Vector3? lastFlagPosition = Plugin.LastFlagPosition;
		if (lastFlagPosition.HasValue)
		{
			float value = Vector2.Distance(new Vector2(localPlayerPosition.Value.X, localPlayerPosition.Value.Z), new Vector2(lastFlagPosition.Value.X, lastFlagPosition.Value.Z));
			ImU8String text2 = new ImU8String(9, 2);
			text2.AppendLiteral("Flag：(");
			text2.AppendFormatted(lastFlagPosition.Value.X, "0.0");
			text2.AppendLiteral(", ");
			text2.AppendFormatted(lastFlagPosition.Value.Z, "0.0");
			text2.AppendLiteral(")");
			ImGui.Text(text2);
			ImU8String text3 = new ImU8String(6, 1);
			text3.AppendLiteral("  距离 ");
			text3.AppendFormatted(value, "0.0");
			text3.AppendLiteral("m");
			ImGui.TextDisabled(text3);
		}
		else
		{
			ImGui.TextDisabled("Flag：未设置");
		}
	}

	[CompilerGenerated]
	private float Method_858()
	{
		return Config.TeleportThreshold;
	}

	[CompilerGenerated]
	private void Method_859(float arg0)
	{
		Config.TeleportThreshold = Math.Clamp(arg0, 100f, 1000f);
		Save();
	}

	[CompilerGenerated]
	private float Method_860()
	{
		return Config.WaitAfterTeleport;
	}

	[CompilerGenerated]
	private void Method_861(float arg0)
	{
		Config.WaitAfterTeleport = Math.Clamp(arg0, 0f, 120f);
		Save();
	}

	[CompilerGenerated]
	private void Method_862(int arg0)
	{
		if (arg0 == 0)
		{
			if (ImGui.RadioButton("只喊话##LeaderYellOnly", Config.LeaderYellOnly))
			{
				Config.LeaderYellOnly = true;
				Save();
			}
		}
		else if (ImGui.RadioButton("全部##LeaderYellAll", !Config.LeaderYellOnly))
		{
			Config.LeaderYellOnly = false;
			Save();
		}
	}

	[CompilerGenerated]
	private void Method_863(int arg0)
	{
		if (arg0 == 0)
		{
			string buf = Plugin.MountSearchText;
			ImGui.SetNextItemWidth(-1f);
			if (ImGui.InputTextWithHint("##MountSearch", "搜索坐骑名称...", ref buf, 64))
			{
				Plugin.MountSearchText = buf;
			}
		}
		else if (UiButton.Draw("刷新", UiVariant.Neutral, new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight), "重新读取可用坐骑列表"))
		{
			Plugin.RefreshMountList();
			field_849 = true;
		}
	}

	[CompilerGenerated]
	private void Method_864(int arg0)
	{
		Vector2 value = new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight);
		switch (arg0)
		{
		case 0:
			if (UiButton.Draw("切线", UiVariant.Neutral, value, "切到下一条副本线"))
			{
				Plugin.SwitchToNextInstance();
			}
			break;
		case 1:
			if (UiButton.Draw("当前线路", UiVariant.Neutral, value, "在聊天框打印当前与目标线路号"))
			{
				int currentInstanceNumber = Plugin.GetCurrentInstanceNumber();
				int currentInstanceNumber2 = Config.CurrentInstanceNumber;
				string value2 = ((currentInstanceNumber > 0) ? $"{currentInstanceNumber}线" : "非分线/读取失败");
				Plugin.PrintEcho($"当前线路: {value2} (目标线: {currentInstanceNumber2})");
			}
			break;
		default:
			if (UiButton.Draw("召唤坐骑", UiVariant.Neutral, value, "立即召唤当前配置的坐骑"))
			{
				Plugin.SummonMount();
			}
			break;
		}
	}

	[CompilerGenerated]
	private void Method_865(int arg0)
	{
		Vector2 value = new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight);
		if (arg0 == 0)
		{
			if (UiButton.Draw("下坐骑", UiVariant.Neutral, value, "立即下坐骑"))
			{
				AutomaticHunting.UseDismount();
			}
		}
		else if (UiButton.Draw("选中A怪", UiVariant.Neutral, value, "扫描并选中附近的 A 怪"))
		{
			Plugin.ScanAndSelectTarget();
		}
	}

	[CompilerGenerated]
	private void Method_866(int arg0)
	{
		Vector2 value = new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight);
		switch (arg0)
		{
		case 0:
			if (UiButton.Draw("复制诊断信息", UiVariant.Neutral, value, "把状态 / 前置 / 配置摘要复制到剪贴板") && Diagnostics.CopyReport(Plugin))
			{
				Plugin.PrintEcho("诊断信息已复制到剪贴板");
			}
			return;
		case 1:
			if (UiButton.Draw("导出日志", UiVariant.Neutral, value, "把 Dalamud 日志导出到插件目录"))
			{
				string text = Diagnostics.ExportLog(Plugin);
				if (!string.IsNullOrEmpty(text))
				{
					Plugin.PrintEcho("日志已导出: " + text);
				}
			}
			return;
		}
		if (UiButton.Draw("导出配置", UiVariant.Neutral, value, "把当前配置导出为 JSON 文件"))
		{
			string text2 = Diagnostics.ExportConfig(Plugin);
			if (!string.IsNullOrEmpty(text2))
			{
				Plugin.PrintEcho("配置已导出: " + text2);
			}
		}
	}

	[CompilerGenerated]
	private void Method_867(int arg0)
	{
		Vector2 value = new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight);
		if (arg0 == 0)
		{
			if (UiButton.Draw("恢复默认", UiVariant.Neutral, value, "只重置行为开关（不影响已保存的配置与步骤）"))
			{
				field_848.Show("恢复默认设置", "将重置以下行为开关为出厂默认值。已保存的跨服配置、连车配置与车头名称不受影响。", new string[4] { "传送阈值 / 等待时间", "导航与战斗选项", "S怪监控选项", "多线 / 招募 / 右键菜单开关" }, string.Empty, "恢复默认", UiVariant.Danger, Method_855);
			}
		}
		else if (UiButton.Draw("清空全部", UiVariant.DangerSolid, value, "清空所有配置（含已保存的跨服与连车配置）"))
		{
			field_847.Show("清空全部配置", "此操作不可撤销：所有配置项（含车头、已保存的一键跨服配置、连车配置、多线地图）都会被清空，插件回到首次运行状态。", new string[4] { "车头名称与全部行为开关", "已保存的一键跨服配置", "已保存的连车配置", "多线地图与黑名单" }, "清空", "确认清空", UiVariant.DangerSolid, Method_856);
		}
	}
}
