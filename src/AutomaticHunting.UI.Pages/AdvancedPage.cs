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
	public sealed class RecoveredType_129
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
				if (UiButton.Draw("新增多線地圖設定", UiVariant.Neutral, value, "新增一條多線地圖設定"))
				{
					field_852.Add(new MultiLineMapConfig());
					field_851.Save();
				}
			}
			else if (UiButton.Draw("檢查 國際服 切線前置", UiVariant.Brand, value, "使用 Lifestream 切線；需靠近水晶"))
			{
				field_851.Plugin.LoadFastInstanceSwitch();
			}
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_130
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
			if (!ImGui.SliderInt(arg0, ref v, 1, 9, "%d條線"))
			{
				return false;
			}
			field_854.InstanceCount = Math.Clamp(v, 1, 9);
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
		UiSection.Header("準備設定");
		UiForm.BeginActionMeasured("##advPrep", 32f, "傳送門檻", "等待時間", "車頭監聽頻道", "右鍵選單");
		UiForm.Row("傳送門檻", "直接前往標點的距離比經以太之晶傳送後遠超過此值時自動傳送。值大更傾向直接導航，值小更傾向傳送");
		UiForm.SliderWithReset("##Threshold", () => Config.TeleportThreshold, (float arg0) =>
		{
			Config.TeleportThreshold = Math.Clamp(arg0, 100f, 1000f);
			Save();
		}, 100f, 1000f, "%.0fm", 300f);
		UiForm.Row("等待時間", "傳送讀條結束後等待的時間，用於等待地圖狀態穩定");
		UiForm.SliderWithReset("##Wait", () => Config.WaitAfterTeleport, (float arg0) =>
		{
			Config.WaitAfterTeleport = Math.Clamp(arg0, 0f, 120f);
			Save();
		}, 0f, 120f, "%.0fs", 1f);
		UiForm.Row("車頭監聽頻道", "只喊話=僅識別喊話(/sh,全圖)頻道；全部=任意頻道都識別。/y(呼喊)不是喊話頻道");
		UiLayout.EqualColumns("##advYellRow", 2, 8f, (int arg0) =>
		{
			if (arg0 == 0)
			{
				if (ImGui.RadioButton("只喊話##LeaderYellOnly", Config.LeaderYellOnly))
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
		UiForm.Row("右鍵選單", "在玩家右鍵選單中顯示「AH 新增為車頭」");
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
		UiSection.Header("多線地圖", "僅設定了地圖ID的地圖會啟用多線流程");
		List<MultiLineMapConfig> field_852 = Config.MultiLineMaps;
		int field_853 = -1;
		if (field_852.Count == 0)
		{
			UiEmpty.Show("還沒有多線地圖設定", "填入地圖ID後啟用切線與A怪計數流程");
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
					Title = (string.IsNullOrWhiteSpace(field_854.MapId) ? "（未填寫地圖 ID）" : field_854.MapId),
					Subtitle = $"{field_854.InstanceCount} 條線 · {field_854.AMobCount} 只 A 怪",
					Draggable = false,
					DeleteTooltip = "移除這條多線地圖設定",
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
					if (!ImGui.SliderInt(arg0, ref v2, 1, 9, "%d條線"))
					{
						return false;
					}
					field_854.InstanceCount = Math.Clamp(v2, 1, 9);
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
		UiForm.BeginMeasured("##advMultiLine", "切線兜底");
		UiForm.Row("切線兜底", "傳送切線命令5秒後真實線未變化，先傳送到最近以太之光再重發一次");
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
				if (UiButton.Draw("新增多線地圖設定", UiVariant.Neutral, value, "新增一條多線地圖設定"))
				{
					field_852.Add(new MultiLineMapConfig());
					Save();
				}
			}
			else if (UiButton.Draw("檢查 國際服 切線前置", UiVariant.Brand, value, "使用 Lifestream 切線；需靠近水晶"))
			{
				Plugin.LoadFastInstanceSwitch();
			}
		});
	}

	private void Method_851()
	{
		UiSection.Header("坐騎", "選擇導航使用的坐騎");
		if (!field_849)
		{
			Plugin.RefreshMountList();
			field_849 = true;
		}
		if (Plugin.SelectedMountIndex >= 0 && Plugin.SelectedMountIndex < Plugin.MountNames.Length)
		{
			ImU8String text = new ImU8String(3, 1);
			text.AppendLiteral("目前：");
			text.AppendFormatted(Plugin.MountNames[Plugin.SelectedMountIndex]);
			ImGui.TextDisabled(text);
		}
		UiLayout.EqualColumns("##mountSearchRow", 2, 8f, (int arg0) =>
		{
			if (arg0 == 0)
			{
				string buf = Plugin.MountSearchText;
				ImGui.SetNextItemWidth(-1f);
				if (ImGui.InputTextWithHint("##MountSearch", "搜尋坐騎名稱...", ref buf, 64))
				{
					Plugin.MountSearchText = buf;
				}
			}
			else if (UiButton.Draw("重新整理", UiVariant.Neutral, new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight), "重新讀取可用坐騎列表"))
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
			UiEmpty.Show("沒有匹配的坐騎", "換個關鍵詞，或點「重新整理」重新讀取列表");
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
			if (TraditionalText.Selectable(Plugin.MountNames[item], selected))
			{
				Plugin.SelectedMountIndex = item;
				Plugin.UpdateMountCommand();
			}
		}
		ImGui.EndListBox();
	}

	private void Method_852()
	{
		UiSection.Header("狩獵招募", "建立招募時使用的描述");
		ImGui.TextDisabled("描述可留空；≤2 行、UTF-8 ≤192 位元組。");
		string buf = Config.PfComment ?? string.Empty;
		if (ImGui.InputTextMultiline("##PfComment", ref buf, 256, new Vector2(ImGui.GetContentRegionAvail().X, 62f)))
		{
			Config.PfComment = buf;
			Save();
		}
		if (UiButton.Draw("建立狩獵招募", UiVariant.Brand, new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight), "立即建立一次狩獵招募"))
		{
			HuntPfController.Create(Config.PfComment);
		}
	}

	private void Method_853()
	{
		UiSection.Header("除錯工具");
		UiForm.BeginMeasured("##advDebugFlags", "輸出日誌", "顯示座標距離", "A怪計數");
		UiForm.Row("輸出日誌", "把插件執行日誌輸出到聊天框");
		bool v = Config.EchoMessages;
		if (ImGui.Checkbox("##Echo", ref v))
		{
			Config.EchoMessages = v;
			Save();
		}
		UiForm.Row("顯示座標距離", "在除錯區顯示玩家與 地圖標記 的座標距離");
		bool v2 = Config.ShowDistances;
		if (ImGui.Checkbox("##ShowDist", ref v2))
		{
			Config.ShowDistances = v2;
			Save();
		}
		UiForm.Row("A怪計數", "勾選後，未設定多線地圖的地圖也進行 A 怪計數（方便測試）");
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
				if (UiButton.Draw("切線", UiVariant.Neutral, value2, "切到下一條副本線"))
				{
					Plugin.SwitchToNextInstance();
				}
				break;
			case 1:
				if (UiButton.Draw("目前線路", UiVariant.Neutral, value2, "在聊天框列印目前與目標線路號"))
				{
					int currentInstanceNumber = Plugin.GetCurrentInstanceNumber();
					int currentInstanceNumber2 = Config.CurrentInstanceNumber;
					string value3 = ((currentInstanceNumber > 0) ? $"{currentInstanceNumber}線" : "非分線/讀取失敗");
					Plugin.PrintEcho($"目前線路: {value3} (目標線: {currentInstanceNumber2})");
				}
				break;
			default:
				if (UiButton.Draw("召喚坐騎", UiVariant.Neutral, value2, "立即召喚目前設定的坐騎"))
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
				if (UiButton.Draw("下坐騎", UiVariant.Neutral, value2, "立即下坐騎"))
				{
					AutomaticHunting.UseDismount();
				}
			}
			else if (UiButton.Draw("選中A怪", UiVariant.Neutral, value2, "掃描並選中附近的 A 怪"))
			{
				Plugin.ScanAndSelectTarget();
			}
		});
		ImGui.Spacing();
		if (UiButton.Draw("測試突進：" + Config.SDashCommand, UiVariant.Neutral, new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight), "立即執行一次突進技能命令（不影響自動流程）"))
		{
			Plugin.ExecuteDashSkill();
		}
		string currentTerritoryName = AutomaticHunting.GetCurrentTerritoryName();
		if (!string.IsNullOrWhiteSpace(currentTerritoryName) && Config.ARealmMarks.TryGetValue(currentTerritoryName, out string[] value))
		{
			ImGui.Spacing();
			ImU8String text = new ImU8String(5, 1);
			text.AppendLiteral("目前區域：");
			text.AppendFormatted(currentTerritoryName);
			ImGui.TextDisabled(text);
			ImU8String text2 = new ImU8String(3, 1);
			text2.AppendLiteral("A怪：");
			text2.AppendFormatted(string.Join(", ", value));
			ImGui.TextDisabled(text2);
		}
		ImU8String text3 = new ImU8String(6, 1);
		text3.AppendLiteral("已啟用車頭：");
		text3.AppendFormatted(string.IsNullOrWhiteSpace(Config.LeaderName) ? "未設定" : Config.LeaderName);
		ImGui.TextDisabled(text3);
		if (Config.ShowDistances)
		{
			Method_857();
		}
	}

	private void Method_854()
	{
		UiSection.Header("診斷與匯出");
		UiLayout.EqualColumns("##diagBtns1", 3, 8f, (int arg0) =>
		{
			Vector2 value = new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight);
			switch (arg0)
			{
			case 0:
				if (UiButton.Draw("複製診斷資訊", UiVariant.Neutral, value, "把狀態 / 前置 / 設定摘要複製到剪貼簿") && Diagnostics.CopyReport(Plugin))
				{
					Plugin.PrintEcho("診斷資訊已複製到剪貼簿");
				}
				break;
			case 1:
				if (UiButton.Draw("匯出日誌", UiVariant.Neutral, value, "把 Dalamud 日誌匯出到插件目錄"))
				{
					string text2 = Diagnostics.ExportLog(Plugin);
					if (!string.IsNullOrEmpty(text2))
					{
						Plugin.PrintEcho("日誌已匯出: " + text2);
					}
				}
				break;
			default:
				if (UiButton.Draw("匯出設定", UiVariant.Neutral, value, "把目前設定匯出為 JSON 檔案"))
				{
					string text = Diagnostics.ExportConfig(Plugin);
					if (!string.IsNullOrEmpty(text))
					{
						Plugin.PrintEcho("設定已匯出: " + text);
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
				if (UiButton.Draw("恢復預設", UiVariant.Neutral, value, "只重置行為開關（不影響已儲存的設定與步驟）"))
				{
					field_848.Show("恢復預設設定", "將重置以下行為開關為出廠預設值。已儲存的跨服設定、連車設定與車頭名稱不受影響。", new string[4] { "傳送門檻 / 等待時間", "導航與戰鬥選項", "S怪監控選項", "多線 / 招募 / 右鍵選單開關" }, string.Empty, "恢復預設", UiVariant.Danger, Method_855);
				}
			}
			else if (UiButton.Draw("清空全部", UiVariant.DangerSolid, value, "清空所有設定（含已儲存的跨服與連車設定）"))
			{
				field_847.Show("清空全部設定", "此操作不可撤銷：所有設定項（含車頭、已儲存的一鍵跨服設定、連車設定、多線地圖）都會被清空，插件回到首次執行狀態。", new string[4] { "車頭名稱與全部行為開關", "已儲存的一鍵跨服設定", "已儲存的連車設定", "多線地圖與黑名單" }, "清空", "確認清空", UiVariant.DangerSolid, Method_856);
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
		Config.SDashCommand = GlobalGameData.DefaultDashCommand;
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
		Plugin.PrintEcho("已恢復預設行為開關（已儲存的設定與車頭未改動）");
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
		Plugin.PrintEcho("已清空全部設定，插件回到首次執行狀態（請重新完成設定精靈）");
		Plugin.OnboardingWindow.Open();
	}

	private void Method_857()
	{
		Vector3? localPlayerPosition = AutomaticHunting.GetLocalPlayerPosition();
		if (!localPlayerPosition.HasValue)
		{
			ImGui.TextDisabled("無法取得玩家位置");
			return;
		}
		ImGui.Spacing();
		TraditionalText.TextColored(Theme.Brand, "座標距離");
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
			text2.AppendLiteral("地圖標記：(");
			text2.AppendFormatted(lastFlagPosition.Value.X, "0.0");
			text2.AppendLiteral(", ");
			text2.AppendFormatted(lastFlagPosition.Value.Z, "0.0");
			text2.AppendLiteral(")");
			ImGui.Text(text2);
			ImU8String text3 = new ImU8String(6, 1);
			text3.AppendLiteral("  距離 ");
			text3.AppendFormatted(value, "0.0");
			text3.AppendLiteral("m");
			ImGui.TextDisabled(text3);
		}
		else
		{
			ImGui.TextDisabled("地圖標記：未設定");
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
			if (ImGui.RadioButton("只喊話##LeaderYellOnly", Config.LeaderYellOnly))
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
			if (ImGui.InputTextWithHint("##MountSearch", "搜尋坐騎名稱...", ref buf, 64))
			{
				Plugin.MountSearchText = buf;
			}
		}
		else if (UiButton.Draw("重新整理", UiVariant.Neutral, new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight), "重新讀取可用坐騎列表"))
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
			if (UiButton.Draw("切線", UiVariant.Neutral, value, "切到下一條副本線"))
			{
				Plugin.SwitchToNextInstance();
			}
			break;
		case 1:
			if (UiButton.Draw("目前線路", UiVariant.Neutral, value, "在聊天框列印目前與目標線路號"))
			{
				int currentInstanceNumber = Plugin.GetCurrentInstanceNumber();
				int currentInstanceNumber2 = Config.CurrentInstanceNumber;
				string value2 = ((currentInstanceNumber > 0) ? $"{currentInstanceNumber}線" : "非分線/讀取失敗");
				Plugin.PrintEcho($"目前線路: {value2} (目標線: {currentInstanceNumber2})");
			}
			break;
		default:
			if (UiButton.Draw("召喚坐騎", UiVariant.Neutral, value, "立即召喚目前設定的坐騎"))
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
			if (UiButton.Draw("下坐騎", UiVariant.Neutral, value, "立即下坐騎"))
			{
				AutomaticHunting.UseDismount();
			}
		}
		else if (UiButton.Draw("選中A怪", UiVariant.Neutral, value, "掃描並選中附近的 A 怪"))
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
			if (UiButton.Draw("複製診斷資訊", UiVariant.Neutral, value, "把狀態 / 前置 / 設定摘要複製到剪貼簿") && Diagnostics.CopyReport(Plugin))
			{
				Plugin.PrintEcho("診斷資訊已複製到剪貼簿");
			}
			return;
		case 1:
			if (UiButton.Draw("匯出日誌", UiVariant.Neutral, value, "把 Dalamud 日誌匯出到插件目錄"))
			{
				string text = Diagnostics.ExportLog(Plugin);
				if (!string.IsNullOrEmpty(text))
				{
					Plugin.PrintEcho("日誌已匯出: " + text);
				}
			}
			return;
		}
		if (UiButton.Draw("匯出設定", UiVariant.Neutral, value, "把目前設定匯出為 JSON 檔案"))
		{
			string text2 = Diagnostics.ExportConfig(Plugin);
			if (!string.IsNullOrEmpty(text2))
			{
				Plugin.PrintEcho("設定已匯出: " + text2);
			}
		}
	}

	[CompilerGenerated]
	private void Method_867(int arg0)
	{
		Vector2 value = new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight);
		if (arg0 == 0)
		{
			if (UiButton.Draw("恢復預設", UiVariant.Neutral, value, "只重置行為開關（不影響已儲存的設定與步驟）"))
			{
				field_848.Show("恢復預設設定", "將重置以下行為開關為出廠預設值。已儲存的跨服設定、連車設定與車頭名稱不受影響。", new string[4] { "傳送門檻 / 等待時間", "導航與戰鬥選項", "S怪監控選項", "多線 / 招募 / 右鍵選單開關" }, string.Empty, "恢復預設", UiVariant.Danger, Method_855);
			}
		}
		else if (UiButton.Draw("清空全部", UiVariant.DangerSolid, value, "清空所有設定（含已儲存的跨服與連車設定）"))
		{
			field_847.Show("清空全部設定", "此操作不可撤銷：所有設定項（含車頭、已儲存的一鍵跨服設定、連車設定、多線地圖）都會被清空，插件回到首次執行狀態。", new string[4] { "車頭名稱與全部行為開關", "已儲存的一鍵跨服設定", "已儲存的連車設定", "多線地圖與黑名單" }, "清空", "確認清空", UiVariant.DangerSolid, Method_856);
		}
	}
}
