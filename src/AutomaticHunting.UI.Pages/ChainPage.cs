using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using AutomaticHunting.UI.Components;
using Dalamud.Bindings.ImGui;

namespace AutomaticHunting.UI.Pages;

public sealed class ChainPage : SettingsPage
{
	[CompilerGenerated]
	public static class RecoveredType_132
	{
		public static Func<WorldTravelPreset, string> field_864;

		public static Func<HuntChainStep, HuntChainStep> field_865;
	}

	[Serializable]
	[CompilerGenerated]
	public sealed class RecoveredType_133
	{
		public static readonly RecoveredType_133 field_866 = new RecoveredType_133();

		public static Func<HuntChainStep, int, string> field_867;

		internal string Method_886(HuntChainStep arg0, int arg1)
		{
			return $"{arg1 + 1}. {SettingsPage.FormatPreset(arg0.Preset)}  擊殺{arg0.KillTarget}";
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_134
	{
		public List<WorldTravelPreset> field_868;

		public ChainPage field_869;

		public List<HuntChainStep> field_870;

		internal void Method_888(int arg0, int arg1)
		{
			HuntChainStep item = field_870[arg0];
			HuntChainStep item2 = field_870[arg1];
			field_870.RemoveAt(arg0);
			int index = field_870.IndexOf(item2) + ((arg0 < arg1) ? 1 : 0);
			field_870.Insert(index, item);
			field_869.Save();
		}

		internal bool Method_889(HuntChainPreset arg0)
		{
			return arg0.Name == field_869.field_862;
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_135
	{
		public int field_871;

		public List<string> field_872;

		public RecoveredType_134 field_873;

		public Action<int> field_874;

		internal void Method_891(int arg0)
		{
			field_871 = arg0;
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_136
	{
		public HuntChainStep field_875;

		public RecoveredType_135 field_876;

		internal bool Method_893(string arg0)
		{
			string arg1 = SettingsPage.FormatPreset(field_875.Preset);
			if (!UiCombo.Draw(arg0, arg1, field_876.field_872, out string arg2))
			{
				return false;
			}
			int num = field_876.field_872.IndexOf(arg2);
			if (num < 0)
			{
				return false;
			}
			field_875.Preset = SettingsPage.ClonePreset(field_876.field_873.field_868[num]);
			field_876.field_873.field_869.Save();
			return true;
		}

		internal bool Method_894(string arg0)
		{
			int data = field_875.KillTarget;
			ImGui.SetNextItemWidth(-1f);
			bool flag = ImGui.InputInt(arg0, ref data);
			if (ImGui.IsItemHovered())
			{
				TraditionalText.SetTooltip("本站需要擊殺的A怪數量（跨線、跨圖累計）；設為 0 時直接跳過本站");
			}
			if (flag)
			{
				field_875.KillTarget = Math.Clamp(data, 0, 999);
				field_876.field_873.field_869.Save();
			}
			return flag;
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_137
	{
		public int field_877;

		public ChainPage field_878;

		public List<HuntChainPreset> field_879;

		public Action<int> field_880;

		public Action<int> field_881;

		internal void Method_896(int arg0)
		{
			field_877 = arg0;
		}

		internal void Method_897(int arg0)
		{
			field_878.Plugin.StopHuntChain("連車: 已停止");
		}

		internal void Method_898(int arg0, int arg1)
		{
			if (UiCardList.MoveItem(field_879, arg0, arg1))
			{
				field_878.Save();
			}
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_138
	{
		public HuntChainPreset field_882;

		public RecoveredType_137 field_883;

		internal void Method_900(int arg0)
		{
			field_883.field_878.Method_881(field_882);
		}

		internal void Method_901(int arg0)
		{
			if (field_883.field_878.Plugin.StartHuntChain(field_882.Steps))
			{
				field_883.field_878.field_863 = field_882.Name;
			}
		}

		internal void Method_902(int arg0)
		{
			field_883.field_878.field_859 = arg0;
			field_883.field_878.field_860 = field_882.Name;
			field_883.field_878.field_861 = true;
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_139
	{
		public string field_884;

		public ChainPage field_885;

		internal void Method_904(int arg0)
		{
			Vector2 value = new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight);
			if (arg0 == 0)
			{
				if (field_884.Length == 0)
				{
					ImGui.BeginDisabled();
				}
				if (UiButton.Draw("確定", UiVariant.Primary, value))
				{
					if (field_885.field_859 >= 0 && field_885.field_859 < field_885.Config.SavedHuntChains.Count)
					{
						HuntChainPreset huntChainPreset = field_885.Config.SavedHuntChains[field_885.field_859];
						string name = huntChainPreset.Name;
						huntChainPreset.Name = field_884;
						if (field_885.field_863 == name)
						{
							field_885.field_863 = field_884;
						}
						if (field_885.field_862 == name)
						{
							field_885.field_862 = field_884;
						}
						field_885.Save();
						field_885.Plugin.PrintEcho("連車設定已重新命名：" + name + " → " + field_884);
					}
					else
					{
						field_885.Plugin.PrintEcho($"連車設定改名失敗：目標下標 {field_885.field_859} 無效");
					}
					field_885.field_859 = -1;
					ImGui.CloseCurrentPopup();
				}
				if (field_884.Length == 0)
				{
					ImGui.EndDisabled();
				}
			}
			else if (UiButton.Draw("取消", UiVariant.Neutral, value))
			{
				field_885.field_859 = -1;
				ImGui.CloseCurrentPopup();
			}
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_140
	{
		public int field_886;

		internal bool Method_906(HuntChainPreset arg0)
		{
			return arg0.Name == $"連車設定 {field_886}";
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_141
	{
		public string field_887;

		public HuntChainConfig field_888;

		public ChainPage field_889;

		public bool field_890;

		internal bool Method_908(WorldTravelPreset arg0)
		{
			if (!string.IsNullOrEmpty(field_887))
			{
				return WorldTravelData.GetDcOfServer(arg0.ServerName) == field_887;
			}
			return true;
		}

		internal void Method_909(bool arg0)
		{
			field_888.Enabled = arg0;
			field_889.Save();
			if (!arg0 & field_890)
			{
				field_889.Plugin.StopHuntChain("連車: 已關閉連車開關，連車停止");
			}
		}
	}

	private const string field_856 = "重新命名連車設定##HuntChainRenamePopup";

	private readonly UiCardList field_857 = new UiCardList();

	private readonly UiAdjustRow field_858 = new UiAdjustRow();

	private int field_859 = -1;

	private string field_860 = string.Empty;

	private bool field_861;

	private string field_862 = string.Empty;

	private string field_863 = string.Empty;

	public ChainPage(AutomaticHunting arg0)
		: base(arg0)
	{
		field_858.OnAdjust = (int arg1) =>
		{
			Plugin.AdjustHuntChainKills(arg1);
		};
	}

	public override void Draw()
	{
		HuntChainConfig field_888 = Config.HuntChain;
		List<HuntChainStep> steps = field_888.Steps;
		bool field_890 = Plugin.IsHuntChainRunning;
		if (!field_890)
		{
			field_863 = string.Empty;
		}
		string field_891 = WorldTravelData.GetCurrentDcName();
		List<WorldTravelPreset> list = Config.SavedWorldTravels.Where((WorldTravelPreset arg0) => string.IsNullOrEmpty(field_891) || WorldTravelData.GetDcOfServer(arg0.ServerName) == field_891).ToList();
		UiSection.Header("連車設定", string.IsNullOrEmpty(field_891) ? "讀取資料中心中..." : field_891);
		UiForm.BeginMeasured("##chainOptions", "啟用連車", "到站建招募");
		UiForm.Row("啟用連車", "關閉後無法開始連車；執行中關閉會立即停止");
		UiForm.ToggleWithState("##HuntChainEnabled", field_888.Enabled, (bool arg0) =>
		{
			field_888.Enabled = arg0;
			Save();
			if (!arg0 & field_890)
			{
				Plugin.StopHuntChain("連車: 已關閉連車開關，連車停止");
			}
		}, field_888.Enabled ? "已啟用" : "已關閉");
		UiForm.Row("到站建招募", "每站到達後等待 1 秒建立狩獵招募；描述取「高級」頁的招募描述");
		bool v = field_888.CreatePfAfterTravel;
		if (ImGui.Checkbox("##HuntChainCreatePf", ref v))
		{
			field_888.CreatePfAfterTravel = v;
			Save();
		}
		UiForm.End();
		if (!Config.AutomationEnabled)
		{
			TraditionalText.TextColored(Theme.Warn, "需先在「概覽」開啟「自動狩獵」才能開始連車");
		}
		if (list.Count == 0)
		{
			TraditionalText.TextColored(Theme.Warn, "請先在「跨服」頁儲存至少一個一鍵跨服設定");
		}
		UiSection.Gap();
		Method_877(steps, list, field_890);
		UiSection.Gap();
		Method_878(list, field_890);
		Method_880();
	}

	private void Method_877(List<HuntChainStep> arg0, List<WorldTravelPreset> arg1, bool arg2)
	{
		UiSection.Header("連車步驟", (field_862.Length > 0) ? ("正在編輯：" + field_862) : null);
		if (arg0.Count == 0)
		{
			UiEmpty.Show("尚未新增連車步驟", (arg1.Count > 0) ? "點擊下方「新增連車步驟」開始編排" : "需要先在「跨服」頁儲存一個設定");
		}
		else
		{
			int field_871 = -1;
			List<string> field_872 = arg1.Select(SettingsPage.FormatPreset).ToList();
			List<UiListCard> list = new List<UiListCard>(arg0.Count);
			for (int i = 0; i < arg0.Count; i++)
			{
				HuntChainStep field_875 = arg0[i];
				UiListCard uiListCard = new UiListCard
				{
					Id = $"hc{i}",
					Title = ((i == 0) ? "起始位置" : "一鍵跨服"),
					Tooltip = "拖動左側手柄可調整連車順序",
					DeleteTooltip = "刪除這一站",
					OnDelete = (int num2) =>
					{
						field_871 = num2;
					}
				};
				uiListCard.ExtraColumns.Add((string arg4) =>
				{
					string arg3 = SettingsPage.FormatPreset(field_875.Preset);
					if (!UiCombo.Draw(arg4, arg3, field_872, out string arg5))
					{
						return false;
					}
					int num2 = field_872.IndexOf(arg5);
					if (num2 < 0)
					{
						return false;
					}
					field_875.Preset = SettingsPage.ClonePreset(arg1[num2]);
					Save();
					return true;
				});
				uiListCard.ExtraColumns.Add((string text2) =>
				{
					int data = field_875.KillTarget;
					ImGui.SetNextItemWidth(-1f);
					bool flag = ImGui.InputInt(text2, ref data);
					if (ImGui.IsItemHovered())
					{
						TraditionalText.SetTooltip("本站需要擊殺的A怪數量（跨線、跨圖累計）；設為 0 時直接跳過本站");
					}
					if (flag)
					{
						field_875.KillTarget = Math.Clamp(data, 0, 999);
						Save();
					}
					return flag;
				});
				list.Add(uiListCard);
			}
			if (arg2)
			{
				ImGui.BeginDisabled();
			}
			field_857.Draw("##hcCard", "HC_REORDER", list, (int num2, int num3) =>
			{
				HuntChainStep item = arg0[num2];
				HuntChainStep item2 = arg0[num3];
				arg0.RemoveAt(num2);
				int index = arg0.IndexOf(item2) + ((num2 < num3) ? 1 : 0);
				arg0.Insert(index, item);
				Save();
			});
			if (arg2)
			{
				ImGui.EndDisabled();
			}
			if (field_871 >= 0 && field_871 < arg0.Count)
			{
				arg0.RemoveAt(field_871);
				Save();
			}
		}
		if (arg1.Count > 0)
		{
			if (arg2)
			{
				ImGui.BeginDisabled();
			}
			if (UiButton.Draw("新增連車步驟", UiVariant.Neutral, new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight), "新增一站，預設使用目前資料中心第一個已儲存設定，預設擊殺 12"))
			{
				arg0.Add(new HuntChainStep
				{
					Preset = SettingsPage.ClonePreset(arg1[0]),
					KillTarget = 12
				});
				Save();
			}
			if (arg2)
			{
				ImGui.EndDisabled();
			}
		}
		int num;
		if (!arg2)
		{
			num = ((arg0.Count > 0) ? 1 : 0);
			if (num != 0)
			{
				goto IL_0308;
			}
		}
		else
		{
			num = 0;
		}
		ImGui.BeginDisabled();
		goto IL_0308;
		IL_0308:
		if (UiButton.Draw((field_862.Length > 0) ? "更新連車設定" : "儲存為連車設定", UiVariant.Primary, new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight), "直接儲存上方步驟（未修改過已儲存設定時自動命名）"))
		{
			List<HuntChainStep> steps = arg0.Select(SettingsPage.CloneStep).ToList();
			HuntChainPreset huntChainPreset = (string.IsNullOrEmpty(field_862) ? null : Config.SavedHuntChains.FirstOrDefault((HuntChainPreset huntChainPreset2) => huntChainPreset2.Name == field_862));
			if (huntChainPreset != null)
			{
				huntChainPreset.Steps = steps;
				Plugin.PrintEcho("已更新連車設定：" + huntChainPreset.Name);
			}
			else
			{
				string text = Method_882();
				Config.SavedHuntChains.Add(new HuntChainPreset
				{
					Name = text,
					Steps = steps
				});
				Plugin.PrintEcho("已儲存連車設定：" + text);
			}
			arg0.Clear();
			field_862 = string.Empty;
			Save();
		}
		if (num == 0)
		{
			ImGui.EndDisabled();
		}
	}

	private void Method_878(List<WorldTravelPreset> arg0, bool arg1)
	{
		UiSection.Header("已儲存連車設定");
		List<HuntChainPreset> field_879 = Config.SavedHuntChains;
		if (field_879.Count == 0)
		{
			UiEmpty.Show("還沒有儲存的連車設定", "編排好步驟後點上方「儲存為連車設定」");
			return;
		}
		int field_880 = -1;
		List<UiListCard> list = new List<UiListCard>(field_879.Count);
		bool enabled = !arg1 && Config.HuntChain.Enabled && Config.AutomationEnabled;
		for (int i = 0; i < field_879.Count; i++)
		{
			HuntChainPreset field_882 = field_879[i];
			string text = string.Join("\n", field_882.Steps.Select((HuntChainStep huntChainStep, int num) => $"{num + 1}. {SettingsPage.FormatPreset(huntChainStep.Preset)}  擊殺{huntChainStep.KillTarget}"));
			UiListCard uiListCard = new UiListCard
			{
				Id = $"hcSaved{i}",
				Title = $"{field_882.Name} · {field_882.Steps.Count} 站",
				Tooltip = text + "\n\n左鍵：載入到上方編輯器",
				Selected = (arg1 && field_863 == field_882.Name),
				DeleteTooltip = "刪除此連車設定",
				OnDelete = (int num) =>
				{
					field_880 = num;
				}
			};
			uiListCard.Actions.Add(new UiListCardAction
			{
				Label = "載入",
				Variant = UiVariant.Neutral,
				Tooltip = "把此設定載入到上方編輯器",
				OnClick = (int num) =>
				{
					Method_881(field_882);
				}
			});
			uiListCard.Actions.Add(new UiListCardAction
			{
				Label = "開始",
				Variant = UiVariant.Success,
				Tooltip = "只開始此設定，不覆蓋上方內容",
				Enabled = enabled,
				OnClick = (int num) =>
				{
					if (Plugin.StartHuntChain(field_882.Steps))
					{
						field_863 = field_882.Name;
					}
				}
			});
			uiListCard.Actions.Add(new UiListCardAction
			{
				Label = "停止",
				Variant = UiVariant.Danger,
				Tooltip = "停止目前連車",
				Enabled = arg1,
				OnClick = (int num) =>
				{
					Plugin.StopHuntChain("連車: 已停止");
				}
			});
			uiListCard.Actions.Add(new UiListCardAction
			{
				Label = "改名",
				Variant = UiVariant.Neutral,
				Tooltip = "重新命名此設定",
				OnClick = (int num) =>
				{
					field_859 = num;
					field_860 = field_882.Name;
					field_861 = true;
				}
			});
			list.Add(uiListCard);
		}
		field_857.Draw("##hcSavedCard", "HC_PRESET_REORDER", list, (int arg2, int arg3) =>
		{
			if (UiCardList.MoveItem(field_879, arg2, arg3))
			{
				Save();
			}
		});
		if (field_880 >= 0 && field_880 < field_879.Count)
		{
			string name = field_879[field_880].Name;
			if (arg1 && field_863 == name)
			{
				field_863 = string.Empty;
				Plugin.StopHuntChain("連車: 執行中的設定已被刪除，連車停止");
			}
			if (field_862 == name)
			{
				field_862 = string.Empty;
			}
			field_879.RemoveAt(field_880);
			Save();
			Plugin.PrintEcho("已刪除連車設定");
		}
		Method_879();
	}

	private void Method_879()
	{
		ImGui.Spacing();
		bool canAdjustHuntChainKills = Plugin.CanAdjustHuntChainKills;
		field_858.Text = Plugin.GetHuntChainStatusText();
		field_858.ShowIncrease = canAdjustHuntChainKills;
		field_858.ShowDecrease = canAdjustHuntChainKills;
		field_858.IncreaseEnabled = canAdjustHuntChainKills;
		field_858.DecreaseEnabled = canAdjustHuntChainKills && Plugin.CanDecreaseHuntChainKills;
		field_858.IncreaseTooltip = "目前站已擊殺數量 +1；達標後自動進入下一站";
		field_858.DecreaseTooltip = "目前站已擊殺數量 -1，最低為 0";
		field_858.Draw("##chainKillAdjust");
	}

	private void Method_880()
	{
		if (field_861)
		{
			field_861 = false;
			ImGui.OpenPopup("重新命名連車設定##HuntChainRenamePopup");
		}
		if (!ImGui.BeginPopupModal("重新命名連車設定##HuntChainRenamePopup", ImGuiWindowFlags.AlwaysAutoResize))
		{
			return;
		}
		TraditionalText.TextUnformatted("設定名稱");
		ImGui.SetNextItemWidth(260f);
		ImGui.InputText("##huntChainRenameName", ref field_860, 64);
		string field_884 = field_860.Trim();
		UiLayout.EqualColumns("##huntChainRenameBtns", 2, 8f, (int arg0) =>
		{
			Vector2 value = new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight);
			if (arg0 == 0)
			{
				if (field_884.Length == 0)
				{
					ImGui.BeginDisabled();
				}
				if (UiButton.Draw("確定", UiVariant.Primary, value))
				{
					if (field_859 >= 0 && field_859 < Config.SavedHuntChains.Count)
					{
						HuntChainPreset huntChainPreset = Config.SavedHuntChains[field_859];
						string name = huntChainPreset.Name;
						huntChainPreset.Name = field_884;
						if (field_863 == name)
						{
							field_863 = field_884;
						}
						if (field_862 == name)
						{
							field_862 = field_884;
						}
						Save();
						Plugin.PrintEcho("連車設定已重新命名：" + name + " → " + field_884);
					}
					else
					{
						Plugin.PrintEcho($"連車設定改名失敗：目標下標 {field_859} 無效");
					}
					field_859 = -1;
					ImGui.CloseCurrentPopup();
				}
				if (field_884.Length == 0)
				{
					ImGui.EndDisabled();
				}
			}
			else if (UiButton.Draw("取消", UiVariant.Neutral, value))
			{
				field_859 = -1;
				ImGui.CloseCurrentPopup();
			}
		});
		ImGui.EndPopup();
	}

	private void Method_881(HuntChainPreset arg0)
	{
		List<HuntChainStep> steps = Config.HuntChain.Steps;
		steps.Clear();
		steps.AddRange(arg0.Steps.Select(SettingsPage.CloneStep));
		field_862 = arg0.Name;
		Save();
	}

	private string Method_882()
	{
		int field_886 = Config.SavedHuntChains.Count + 1;
		while (Config.SavedHuntChains.Any((HuntChainPreset arg0) => arg0.Name == $"連車設定 {field_886}"))
		{
			field_886++;
		}
		return $"連車設定 {field_886}";
	}

	[CompilerGenerated]
	private void Method_883(int arg0)
	{
		Plugin.AdjustHuntChainKills(arg0);
	}
}
