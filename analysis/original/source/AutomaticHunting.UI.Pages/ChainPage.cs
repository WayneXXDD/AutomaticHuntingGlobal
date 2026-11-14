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
	private static class _00A0
	{
		public static Func<WorldTravelPreset, string> _2009_2000;

		public static Func<HuntChainStep, HuntChainStep> _2009_2001;
	}

	[Serializable]
	[CompilerGenerated]
	private sealed class _1680
	{
		public static readonly _1680 _2009_2002 = new _1680();

		public static Func<HuntChainStep, int, string> _2009_2003;

		internal string _00A0(HuntChainStep P_0, int P_1)
		{
			return $"{P_1 + 1}. {SettingsPage.FormatPreset(P_0.Preset)}  击杀{P_0.KillTarget}";
		}
	}

	[CompilerGenerated]
	private sealed class _2000
	{
		public List<WorldTravelPreset> _2009_2004;

		public ChainPage _2009_2005;

		public List<HuntChainStep> _2009_2006;

		internal void _00A0(int P_0, int P_1)
		{
			HuntChainStep item = _2009_2006[P_0];
			HuntChainStep item2 = _2009_2006[P_1];
			_2009_2006.RemoveAt(P_0);
			int index = _2009_2006.IndexOf(item2) + ((P_0 < P_1) ? 1 : 0);
			_2009_2006.Insert(index, item);
			_2009_2005.Save();
		}

		internal bool _00A0(HuntChainPreset P_0)
		{
			return P_0.Name == _2009_2005._2008_2032;
		}
	}

	[CompilerGenerated]
	private sealed class _2001
	{
		public int _2009_2007;

		public List<string> _2009_2008;

		public _2000 _2009_2009;

		public Action<int> _2009_200A;

		internal void _00A0(int P_0)
		{
			_2009_2007 = P_0;
		}
	}

	[CompilerGenerated]
	private sealed class _2002
	{
		public HuntChainStep _2009_200B;

		public _2001 _2009_2010;

		internal bool _00A0(string P_0)
		{
			string preview = SettingsPage.FormatPreset(_2009_200B.Preset);
			if (!UiCombo.Draw(P_0, preview, _2009_2010._2009_2008, out string selected))
			{
				return false;
			}
			int num = _2009_2010._2009_2008.IndexOf(selected);
			if (num < 0)
			{
				return false;
			}
			_2009_200B.Preset = SettingsPage.ClonePreset(_2009_2010._2009_2009._2009_2004[num]);
			_2009_2010._2009_2009._2009_2005.Save();
			return true;
		}

		internal bool _1680(string P_0)
		{
			int data = _2009_200B.KillTarget;
			ImGui.SetNextItemWidth(-1f);
			bool flag = ImGui.InputInt(P_0, ref data);
			if (ImGui.IsItemHovered())
			{
				ImGui.SetTooltip("本站需要击杀的A怪数量（跨线、跨图累计）；设为 0 时直接跳过本站");
			}
			if (flag)
			{
				_2009_200B.KillTarget = Math.Clamp(data, 0, 999);
				_2009_2010._2009_2009._2009_2005.Save();
			}
			return flag;
		}
	}

	[CompilerGenerated]
	private sealed class _2003
	{
		public int _2009_2011;

		public ChainPage _2009_2012;

		public List<HuntChainPreset> _2009_2013;

		public Action<int> _2009_2014;

		public Action<int> _2009_2015;

		internal void _00A0(int P_0)
		{
			_2009_2011 = P_0;
		}

		internal void _1680(int P_0)
		{
			_2009_2012.Plugin.StopHuntChain("连车: 已停止");
		}

		internal void _00A0(int P_0, int P_1)
		{
			if (UiCardList.MoveItem(_2009_2013, P_0, P_1))
			{
				_2009_2012.Save();
			}
		}
	}

	[CompilerGenerated]
	private sealed class _2004
	{
		public HuntChainPreset _2009_2022;

		public _2003 _2009_2024;

		internal void _00A0(int P_0)
		{
			_2009_2024._2009_2012._00A0(_2009_2022);
		}

		internal void _1680(int P_0)
		{
			if (_2009_2024._2009_2012.Plugin.StartHuntChain(_2009_2022.Steps))
			{
				_2009_2024._2009_2012._2008_2035 = _2009_2022.Name;
			}
		}

		internal void _2000(int P_0)
		{
			_2009_2024._2009_2012._2008_202D = P_0;
			_2009_2024._2009_2012._2008_202E = _2009_2022.Name;
			_2009_2024._2009_2012._2008_202F = true;
		}
	}

	[CompilerGenerated]
	private sealed class _2005
	{
		public string _2009_2025;

		public ChainPage _2009_2027;

		internal void _00A0(int P_0)
		{
			Vector2 value = new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight);
			if (P_0 == 0)
			{
				if (_2009_2025.Length == 0)
				{
					ImGui.BeginDisabled();
				}
				if (UiButton.Draw("确定", UiVariant.Primary, value))
				{
					if (_2009_2027._2008_202D >= 0 && _2009_2027._2008_202D < _2009_2027.Config.SavedHuntChains.Count)
					{
						HuntChainPreset huntChainPreset = _2009_2027.Config.SavedHuntChains[_2009_2027._2008_202D];
						string name = huntChainPreset.Name;
						huntChainPreset.Name = _2009_2025;
						if (_2009_2027._2008_2035 == name)
						{
							_2009_2027._2008_2035 = _2009_2025;
						}
						if (_2009_2027._2008_2032 == name)
						{
							_2009_2027._2008_2032 = _2009_2025;
						}
						_2009_2027.Save();
						_2009_2027.Plugin.PrintEcho("连车配置已重命名：" + name + " → " + _2009_2025);
					}
					else
					{
						_2009_2027.Plugin.PrintEcho($"连车配置改名失败：目标下标 {_2009_2027._2008_202D} 无效");
					}
					_2009_2027._2008_202D = -1;
					ImGui.CloseCurrentPopup();
				}
				if (_2009_2025.Length == 0)
				{
					ImGui.EndDisabled();
				}
			}
			else if (UiButton.Draw("取消", UiVariant.Neutral, value))
			{
				_2009_2027._2008_202D = -1;
				ImGui.CloseCurrentPopup();
			}
		}
	}

	[CompilerGenerated]
	private sealed class _2006
	{
		public int _2009_2028;

		internal bool _00A0(HuntChainPreset P_0)
		{
			return P_0.Name == $"连车配置 {_2009_2028}";
		}
	}

	[CompilerGenerated]
	private sealed class _2007
	{
		public string _2009_2029;

		public HuntChainConfig _2009_202A;

		public ChainPage _2009_202B;

		public bool _2009_202C;

		internal bool _00A0(WorldTravelPreset P_0)
		{
			if (!string.IsNullOrEmpty(_2009_2029))
			{
				return WorldTravelData.GetDcOfServer(P_0.ServerName) == _2009_2029;
			}
			return true;
		}

		internal void _00A0(bool P_0)
		{
			_2009_202A.Enabled = P_0;
			_2009_202B.Save();
			if (!P_0 & _2009_202C)
			{
				_2009_202B.Plugin.StopHuntChain("连车: 已关闭连车开关，连车停止");
			}
		}
	}

	private const string _2008_202A = "重命名连车配置##HuntChainRenamePopup";

	private readonly UiCardList _2008_202B = new UiCardList();

	private readonly UiAdjustRow _2008_202C = new UiAdjustRow();

	private int _2008_202D = -1;

	private string _2008_202E = string.Empty;

	private bool _2008_202F;

	private string _2008_2032 = string.Empty;

	private string _2008_2035 = string.Empty;

	public ChainPage(AutomaticHunting plugin)
		: base(plugin)
	{
		_2008_202C.OnAdjust = (int P_0) =>
		{
			Plugin.AdjustHuntChainKills(P_0);
		};
	}

	public override void Draw()
	{
		HuntChainConfig huntChain = Config.HuntChain;
		List<HuntChainStep> steps = huntChain.Steps;
		bool isHuntChainRunning = Plugin.IsHuntChainRunning;
		if (!isHuntChainRunning)
		{
			_2008_2035 = string.Empty;
		}
		string currentDcName = WorldTravelData.GetCurrentDcName();
		List<WorldTravelPreset> list = Config.SavedWorldTravels.Where((WorldTravelPreset P_0) => string.IsNullOrEmpty(currentDcName) || WorldTravelData.GetDcOfServer(P_0.ServerName) == currentDcName).ToList();
		UiSection.Header("连车配置", string.IsNullOrEmpty(currentDcName) ? "读取大区中..." : currentDcName);
		UiForm.BeginMeasured("##chainOptions", "启用连车", "到站建招募");
		UiForm.Row("启用连车", "关闭后无法开始连车；运行中关闭会立即停止");
		UiForm.ToggleWithState("##HuntChainEnabled", huntChain.Enabled, (bool P_0) =>
		{
			huntChain.Enabled = P_0;
			Save();
			if (!P_0 & isHuntChainRunning)
			{
				Plugin.StopHuntChain("连车: 已关闭连车开关，连车停止");
			}
		}, huntChain.Enabled ? "已启用" : "已关闭");
		UiForm.Row("到站建招募", "每站到达后等待 1 秒创建狩猎招募；描述取「高级」页的招募描述");
		bool v = huntChain.CreatePfAfterTravel;
		if (ImGui.Checkbox("##HuntChainCreatePf", ref v))
		{
			huntChain.CreatePfAfterTravel = v;
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
		_00A0(steps, list, isHuntChainRunning);
		UiSection.Gap();
		_00A0(list, isHuntChainRunning);
		_1680();
	}

	private void _00A0(List<HuntChainStep> P_0, List<WorldTravelPreset> P_1, bool P_2)
	{
		UiSection.Header("连车步骤", (_2008_2032.Length > 0) ? ("正在编辑：" + _2008_2032) : null);
		if (P_0.Count == 0)
		{
			UiEmpty.Show("尚未添加连车步骤", (P_1.Count > 0) ? "点击下方「添加连车步骤」开始编排" : "需要先在「跨服」页保存一个配置");
		}
		else
		{
			int num = -1;
			List<string> list = P_1.Select(SettingsPage.FormatPreset).ToList();
			List<UiListCard> list2 = new List<UiListCard>(P_0.Count);
			for (int i = 0; i < P_0.Count; i++)
			{
				HuntChainStep huntChainStep = P_0[i];
				UiListCard uiListCard = new UiListCard
				{
					Id = $"hc{i}",
					Title = ((i == 0) ? "起始位置" : "一键跨服"),
					Tooltip = "拖动左侧手柄可调整连车顺序",
					DeleteTooltip = "删除这一站",
					OnDelete = (int num3) =>
					{
						num = num3;
					}
				};
				uiListCard.ExtraColumns.Add((string label) =>
				{
					string preview = SettingsPage.FormatPreset(huntChainStep.Preset);
					if (!UiCombo.Draw(label, preview, list, out string selected))
					{
						return false;
					}
					int num3 = list.IndexOf(selected);
					if (num3 < 0)
					{
						return false;
					}
					huntChainStep.Preset = SettingsPage.ClonePreset(P_1[num3]);
					Save();
					return true;
				});
				uiListCard.ExtraColumns.Add((string text2) =>
				{
					int data = huntChainStep.KillTarget;
					ImGui.SetNextItemWidth(-1f);
					bool flag = ImGui.InputInt(text2, ref data);
					if (ImGui.IsItemHovered())
					{
						ImGui.SetTooltip("本站需要击杀的A怪数量（跨线、跨图累计）；设为 0 时直接跳过本站");
					}
					if (flag)
					{
						huntChainStep.KillTarget = Math.Clamp(data, 0, 999);
						Save();
					}
					return flag;
				});
				list2.Add(uiListCard);
			}
			if (P_2)
			{
				ImGui.BeginDisabled();
			}
			_2008_202B.Draw("##hcCard", "HC_REORDER", list2, (int num3, int num4) =>
			{
				HuntChainStep item = P_0[num3];
				HuntChainStep item2 = P_0[num4];
				P_0.RemoveAt(num3);
				int index = P_0.IndexOf(item2) + ((num3 < num4) ? 1 : 0);
				P_0.Insert(index, item);
				Save();
			});
			if (P_2)
			{
				ImGui.EndDisabled();
			}
			if (num >= 0 && num < P_0.Count)
			{
				P_0.RemoveAt(num);
				Save();
			}
		}
		if (P_1.Count > 0)
		{
			if (P_2)
			{
				ImGui.BeginDisabled();
			}
			if (UiButton.Draw("添加连车步骤", UiVariant.Neutral, new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight), "新增一站，默认使用当前大区第一个已保存配置，默认击杀 12"))
			{
				P_0.Add(new HuntChainStep
				{
					Preset = SettingsPage.ClonePreset(P_1[0]),
					KillTarget = 12
				});
				Save();
			}
			if (P_2)
			{
				ImGui.EndDisabled();
			}
		}
		int num2;
		if (!P_2)
		{
			num2 = ((P_0.Count > 0) ? 1 : 0);
			if (num2 != 0)
			{
				goto IL_0308;
			}
		}
		else
		{
			num2 = 0;
		}
		ImGui.BeginDisabled();
		goto IL_0308;
		IL_0308:
		if (UiButton.Draw((_2008_2032.Length > 0) ? "更新连车配置" : "保存为连车配置", UiVariant.Primary, new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight), "直接保存上方步骤（未修改过已保存配置时自动命名）"))
		{
			List<HuntChainStep> steps = P_0.Select(SettingsPage.CloneStep).ToList();
			HuntChainPreset huntChainPreset = (string.IsNullOrEmpty(_2008_2032) ? null : Config.SavedHuntChains.FirstOrDefault((HuntChainPreset huntChainPreset2) => huntChainPreset2.Name == _2008_2032));
			if (huntChainPreset != null)
			{
				huntChainPreset.Steps = steps;
				Plugin.PrintEcho("已更新连车配置：" + huntChainPreset.Name);
			}
			else
			{
				string text = _2000();
				Config.SavedHuntChains.Add(new HuntChainPreset
				{
					Name = text,
					Steps = steps
				});
				Plugin.PrintEcho("已保存连车配置：" + text);
			}
			P_0.Clear();
			_2008_2032 = string.Empty;
			Save();
		}
		if (num2 == 0)
		{
			ImGui.EndDisabled();
		}
	}

	private void _00A0(List<WorldTravelPreset> P_0, bool P_1)
	{
		UiSection.Header("已保存连车配置");
		List<HuntChainPreset> savedHuntChains = Config.SavedHuntChains;
		if (savedHuntChains.Count == 0)
		{
			UiEmpty.Show("还没有保存的连车配置", "编排好步骤后点上方「保存为连车配置」");
			return;
		}
		int num = -1;
		List<UiListCard> list = new List<UiListCard>(savedHuntChains.Count);
		bool enabled = !P_1 && Config.HuntChain.Enabled && Config.AutomationEnabled;
		for (int i = 0; i < savedHuntChains.Count; i++)
		{
			HuntChainPreset huntChainPreset = savedHuntChains[i];
			string text = string.Join("\n", huntChainPreset.Steps.Select((HuntChainStep huntChainStep, int num2) => $"{num2 + 1}. {SettingsPage.FormatPreset(huntChainStep.Preset)}  击杀{huntChainStep.KillTarget}"));
			UiListCard uiListCard = new UiListCard
			{
				Id = $"hcSaved{i}",
				Title = $"{huntChainPreset.Name} · {huntChainPreset.Steps.Count} 站",
				Tooltip = text + "\n\n左键：载入到上方编辑器",
				Selected = (P_1 && _2008_2035 == huntChainPreset.Name),
				DeleteTooltip = "删除此连车配置",
				OnDelete = (int num2) =>
				{
					num = num2;
				}
			};
			uiListCard.Actions.Add(new UiListCardAction
			{
				Label = "载入",
				Variant = UiVariant.Neutral,
				Tooltip = "把此配置载入到上方编辑器",
				OnClick = delegate
				{
					_00A0(huntChainPreset);
				}
			});
			uiListCard.Actions.Add(new UiListCardAction
			{
				Label = "开始",
				Variant = UiVariant.Success,
				Tooltip = "只开始此配置，不覆盖上方内容",
				Enabled = enabled,
				OnClick = delegate
				{
					if (Plugin.StartHuntChain(huntChainPreset.Steps))
					{
						_2008_2035 = huntChainPreset.Name;
					}
				}
			});
			uiListCard.Actions.Add(new UiListCardAction
			{
				Label = "停止",
				Variant = UiVariant.Danger,
				Tooltip = "停止当前连车",
				Enabled = P_1,
				OnClick = delegate
				{
					Plugin.StopHuntChain("连车: 已停止");
				}
			});
			uiListCard.Actions.Add(new UiListCardAction
			{
				Label = "改名",
				Variant = UiVariant.Neutral,
				Tooltip = "重命名此配置",
				OnClick = (int num2) =>
				{
					_2008_202D = num2;
					_2008_202E = huntChainPreset.Name;
					_2008_202F = true;
				}
			});
			list.Add(uiListCard);
		}
		_2008_202B.Draw("##hcSavedCard", "HC_PRESET_REORDER", list, (int num2, int to) =>
		{
			if (UiCardList.MoveItem(savedHuntChains, num2, to))
			{
				Save();
			}
		});
		if (num >= 0 && num < savedHuntChains.Count)
		{
			string name = savedHuntChains[num].Name;
			if (P_1 && _2008_2035 == name)
			{
				_2008_2035 = string.Empty;
				Plugin.StopHuntChain("连车: 运行中的配置已被删除，连车停止");
			}
			if (_2008_2032 == name)
			{
				_2008_2032 = string.Empty;
			}
			savedHuntChains.RemoveAt(num);
			Save();
			Plugin.PrintEcho("已删除连车配置");
		}
		_00A0();
	}

	private void _00A0()
	{
		ImGui.Spacing();
		bool canAdjustHuntChainKills = Plugin.CanAdjustHuntChainKills;
		_2008_202C.Text = Plugin.GetHuntChainStatusText();
		_2008_202C.ShowIncrease = canAdjustHuntChainKills;
		_2008_202C.ShowDecrease = canAdjustHuntChainKills;
		_2008_202C.IncreaseEnabled = canAdjustHuntChainKills;
		_2008_202C.DecreaseEnabled = canAdjustHuntChainKills && Plugin.CanDecreaseHuntChainKills;
		_2008_202C.IncreaseTooltip = "当前站已击杀数量 +1；达标后自动进入下一站";
		_2008_202C.DecreaseTooltip = "当前站已击杀数量 -1，最低为 0";
		_2008_202C.Draw("##chainKillAdjust");
	}

	private void _1680()
	{
		if (_2008_202F)
		{
			_2008_202F = false;
			ImGui.OpenPopup("重命名连车配置##HuntChainRenamePopup");
		}
		if (!ImGui.BeginPopupModal("重命名连车配置##HuntChainRenamePopup", ImGuiWindowFlags.AlwaysAutoResize))
		{
			return;
		}
		ImGui.TextUnformatted("配置名称");
		ImGui.SetNextItemWidth(260f);
		ImGui.InputText("##huntChainRenameName", ref _2008_202E, 64);
		string text = _2008_202E.Trim();
		UiLayout.EqualColumns("##huntChainRenameBtns", 2, 8f, (int P_0) =>
		{
			Vector2 value = new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight);
			if (P_0 == 0)
			{
				if (text.Length == 0)
				{
					ImGui.BeginDisabled();
				}
				if (UiButton.Draw("确定", UiVariant.Primary, value))
				{
					if (_2008_202D >= 0 && _2008_202D < Config.SavedHuntChains.Count)
					{
						HuntChainPreset huntChainPreset = Config.SavedHuntChains[_2008_202D];
						string name = huntChainPreset.Name;
						huntChainPreset.Name = text;
						if (_2008_2035 == name)
						{
							_2008_2035 = text;
						}
						if (_2008_2032 == name)
						{
							_2008_2032 = text;
						}
						Save();
						Plugin.PrintEcho("连车配置已重命名：" + name + " → " + text);
					}
					else
					{
						Plugin.PrintEcho($"连车配置改名失败：目标下标 {_2008_202D} 无效");
					}
					_2008_202D = -1;
					ImGui.CloseCurrentPopup();
				}
				if (text.Length == 0)
				{
					ImGui.EndDisabled();
				}
			}
			else if (UiButton.Draw("取消", UiVariant.Neutral, value))
			{
				_2008_202D = -1;
				ImGui.CloseCurrentPopup();
			}
		});
		ImGui.EndPopup();
	}

	private void _00A0(HuntChainPreset P_0)
	{
		List<HuntChainStep> steps = Config.HuntChain.Steps;
		steps.Clear();
		steps.AddRange(P_0.Steps.Select(SettingsPage.CloneStep));
		_2008_2032 = P_0.Name;
		Save();
	}

	private string _2000()
	{
		int value = Config.SavedHuntChains.Count + 1;
		while (Config.SavedHuntChains.Any((HuntChainPreset P_0) => P_0.Name == $"连车配置 {value}"))
		{
			value++;
		}
		return $"连车配置 {value}";
	}

	[CompilerGenerated]
	private void _00A0(int P_0)
	{
		Plugin.AdjustHuntChainKills(P_0);
	}
}
