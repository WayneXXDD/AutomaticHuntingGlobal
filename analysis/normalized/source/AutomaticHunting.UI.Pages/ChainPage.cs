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
			return $"{arg1 + 1}. {SettingsPage.FormatPreset(arg0.Preset)}  击杀{arg0.KillTarget}";
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
				ImGui.SetTooltip("本站需要击杀的A怪数量（跨线、跨图累计）；设为 0 时直接跳过本站");
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
			field_878.Plugin.StopHuntChain("连车: 已停止");
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
				if (UiButton.Draw("确定", UiVariant.Primary, value))
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
						field_885.Plugin.PrintEcho("连车配置已重命名：" + name + " → " + field_884);
					}
					else
					{
						field_885.Plugin.PrintEcho($"连车配置改名失败：目标下标 {field_885.field_859} 无效");
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
			return arg0.Name == $"连车配置 {field_886}";
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
				field_889.Plugin.StopHuntChain("连车: 已关闭连车开关，连车停止");
			}
		}
	}

	private const string field_856 = "重命名连车配置##HuntChainRenamePopup";

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
		UiSection.Header("连车配置", string.IsNullOrEmpty(field_891) ? "读取大区中..." : field_891);
		UiForm.BeginMeasured("##chainOptions", "启用连车", "到站建招募");
		UiForm.Row("启用连车", "关闭后无法开始连车；运行中关闭会立即停止");
		UiForm.ToggleWithState("##HuntChainEnabled", field_888.Enabled, (bool arg0) =>
		{
			field_888.Enabled = arg0;
			Save();
			if (!arg0 & field_890)
			{
				Plugin.StopHuntChain("连车: 已关闭连车开关，连车停止");
			}
		}, field_888.Enabled ? "已启用" : "已关闭");
		UiForm.Row("到站建招募", "每站到达后等待 1 秒创建狩猎招募；描述取「高级」页的招募描述");
		bool v = field_888.CreatePfAfterTravel;
		if (ImGui.Checkbox("##HuntChainCreatePf", ref v))
		{
			field_888.CreatePfAfterTravel = v;
			Save();
		}
		UiForm.End();
		if (!Config.AutomationEnabled)
		{
			ImGui.TextColored(Theme.Warn, "需先在「概览」开启「自动狩猎」才能开始连车");
		}
		if (list.Count == 0)
		{
			ImGui.TextColored(Theme.Warn, "请先在「跨服」页保存至少一个一键跨服配置");
		}
		UiSection.Gap();
		Method_877(steps, list, field_890);
		UiSection.Gap();
		Method_878(list, field_890);
		Method_880();
	}

	private void Method_877(List<HuntChainStep> arg0, List<WorldTravelPreset> arg1, bool arg2)
	{
		UiSection.Header("连车步骤", (field_862.Length > 0) ? ("正在编辑：" + field_862) : null);
		if (arg0.Count == 0)
		{
			UiEmpty.Show("尚未添加连车步骤", (arg1.Count > 0) ? "点击下方「添加连车步骤」开始编排" : "需要先在「跨服」页保存一个配置");
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
					Title = ((i == 0) ? "起始位置" : "一键跨服"),
					Tooltip = "拖动左侧手柄可调整连车顺序",
					DeleteTooltip = "删除这一站",
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
						ImGui.SetTooltip("本站需要击杀的A怪数量（跨线、跨图累计）；设为 0 时直接跳过本站");
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
			if (UiButton.Draw("添加连车步骤", UiVariant.Neutral, new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight), "新增一站，默认使用当前大区第一个已保存配置，默认击杀 12"))
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
		if (UiButton.Draw((field_862.Length > 0) ? "更新连车配置" : "保存为连车配置", UiVariant.Primary, new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight), "直接保存上方步骤（未修改过已保存配置时自动命名）"))
		{
			List<HuntChainStep> steps = arg0.Select(SettingsPage.CloneStep).ToList();
			HuntChainPreset huntChainPreset = (string.IsNullOrEmpty(field_862) ? null : Config.SavedHuntChains.FirstOrDefault((HuntChainPreset huntChainPreset2) => huntChainPreset2.Name == field_862));
			if (huntChainPreset != null)
			{
				huntChainPreset.Steps = steps;
				Plugin.PrintEcho("已更新连车配置：" + huntChainPreset.Name);
			}
			else
			{
				string text = Method_882();
				Config.SavedHuntChains.Add(new HuntChainPreset
				{
					Name = text,
					Steps = steps
				});
				Plugin.PrintEcho("已保存连车配置：" + text);
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
		UiSection.Header("已保存连车配置");
		List<HuntChainPreset> field_879 = Config.SavedHuntChains;
		if (field_879.Count == 0)
		{
			UiEmpty.Show("还没有保存的连车配置", "编排好步骤后点上方「保存为连车配置」");
			return;
		}
		int field_880 = -1;
		List<UiListCard> list = new List<UiListCard>(field_879.Count);
		bool enabled = !arg1 && Config.HuntChain.Enabled && Config.AutomationEnabled;
		for (int i = 0; i < field_879.Count; i++)
		{
			HuntChainPreset field_882 = field_879[i];
			string text = string.Join("\n", field_882.Steps.Select((HuntChainStep huntChainStep, int num) => $"{num + 1}. {SettingsPage.FormatPreset(huntChainStep.Preset)}  击杀{huntChainStep.KillTarget}"));
			UiListCard uiListCard = new UiListCard
			{
				Id = $"hcSaved{i}",
				Title = $"{field_882.Name} · {field_882.Steps.Count} 站",
				Tooltip = text + "\n\n左键：载入到上方编辑器",
				Selected = (arg1 && field_863 == field_882.Name),
				DeleteTooltip = "删除此连车配置",
				OnDelete = (int num) =>
				{
					field_880 = num;
				}
			};
			uiListCard.Actions.Add(new UiListCardAction
			{
				Label = "载入",
				Variant = UiVariant.Neutral,
				Tooltip = "把此配置载入到上方编辑器",
				OnClick = (int num) =>
				{
					Method_881(field_882);
				}
			});
			uiListCard.Actions.Add(new UiListCardAction
			{
				Label = "开始",
				Variant = UiVariant.Success,
				Tooltip = "只开始此配置，不覆盖上方内容",
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
				Tooltip = "停止当前连车",
				Enabled = arg1,
				OnClick = (int num) =>
				{
					Plugin.StopHuntChain("连车: 已停止");
				}
			});
			uiListCard.Actions.Add(new UiListCardAction
			{
				Label = "改名",
				Variant = UiVariant.Neutral,
				Tooltip = "重命名此配置",
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
				Plugin.StopHuntChain("连车: 运行中的配置已被删除，连车停止");
			}
			if (field_862 == name)
			{
				field_862 = string.Empty;
			}
			field_879.RemoveAt(field_880);
			Save();
			Plugin.PrintEcho("已删除连车配置");
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
		field_858.IncreaseTooltip = "当前站已击杀数量 +1；达标后自动进入下一站";
		field_858.DecreaseTooltip = "当前站已击杀数量 -1，最低为 0";
		field_858.Draw("##chainKillAdjust");
	}

	private void Method_880()
	{
		if (field_861)
		{
			field_861 = false;
			ImGui.OpenPopup("重命名连车配置##HuntChainRenamePopup");
		}
		if (!ImGui.BeginPopupModal("重命名连车配置##HuntChainRenamePopup", ImGuiWindowFlags.AlwaysAutoResize))
		{
			return;
		}
		ImGui.TextUnformatted("配置名称");
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
				if (UiButton.Draw("确定", UiVariant.Primary, value))
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
						Plugin.PrintEcho("连车配置已重命名：" + name + " → " + field_884);
					}
					else
					{
						Plugin.PrintEcho($"连车配置改名失败：目标下标 {field_859} 无效");
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
		while (Config.SavedHuntChains.Any((HuntChainPreset arg0) => arg0.Name == $"连车配置 {field_886}"))
		{
			field_886++;
		}
		return $"连车配置 {field_886}";
	}

	[CompilerGenerated]
	private void Method_883(int arg0)
	{
		Plugin.AdjustHuntChainKills(arg0);
	}
}
