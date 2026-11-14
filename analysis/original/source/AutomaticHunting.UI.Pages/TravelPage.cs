using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using AutomaticHunting.UI.Components;
using Dalamud.Bindings.ImGui;

namespace AutomaticHunting.UI.Pages;

public sealed class TravelPage(AutomaticHunting plugin) : SettingsPage(plugin)
{
	[Serializable]
	[CompilerGenerated]
	private sealed class _00A0
	{
		public static readonly _00A0 _2009_202D = new _00A0();

		public static Func<WorldTravelData.MapEntry, string> _2009_202E;

		public static Func<(uint Id, Vector3 Position, string Name), string> _2009_202F;

		internal string _00A0(WorldTravelData.MapEntry P_0)
		{
			return P_0.Name;
		}

		internal string _00A0((uint Id, Vector3 Position, string Name) a)
		{
			return a.Name;
		}
	}

	[CompilerGenerated]
	private sealed class _1680
	{
		public string _2009_2032;

		public string _2009_2035;

		internal bool _00A0(WorldTravelData.MapEntry P_0)
		{
			return P_0.Name == _2009_2032;
		}

		internal bool _00A0((uint Id, Vector3 Position, string Name) a)
		{
			return a.Name == _2009_2035;
		}
	}

	[CompilerGenerated]
	private sealed class _2000
	{
		public string _2009_2033;

		public TravelPage _2009_2036;

		internal bool _00A0(WorldTravelData.DcEntry P_0)
		{
			return P_0.Name == _2009_2033;
		}

		internal bool _00A0(WorldTravelData.MapEntry P_0)
		{
			return P_0.RowId == _2009_2036.Config.ManualMapRowId;
		}

		internal bool _1680(WorldTravelData.MapEntry P_0)
		{
			return P_0.RowId == _2009_2036.Config.ManualMapRowId;
		}

		internal bool _00A0((uint Id, Vector3 Position, string Name) a)
		{
			return a.Id == _2009_2036.Config.ManualAetheryteId;
		}

		internal bool _1680((uint Id, Vector3 Position, string Name) a)
		{
			return a.Id == _2009_2036.Config.ManualAetheryteId;
		}
	}

	[CompilerGenerated]
	private sealed class _2001
	{
		public TravelPage _2009_203E;

		public WorldTravelPreset _2009_2047;

		internal bool _00A0((uint Id, Vector3 Position, string Name) a)
		{
			return a.Id == _2009_203E.Config.ManualAetheryteId;
		}

		internal bool _00A0(WorldTravelPreset P_0)
		{
			return TravelPage._00A0(P_0, _2009_2047);
		}
	}

	[CompilerGenerated]
	private sealed class _2002
	{
		public string _2009_2048;

		public int _2009_2049;

		public List<(int Index, WorldTravelPreset Preset)> _2009_204A;

		public TravelPage _2009_204B;

		public List<string> _2009_204C;

		public List<WorldTravelPreset> _2009_204D;

		public Action<int> _2009_204E;

		internal bool _00A0(WorldTravelData.DcEntry P_0)
		{
			return P_0.Name == _2009_2048;
		}

		internal void _00A0(int P_0)
		{
			_2009_2049 = _2009_204A[P_0].Index;
		}

		internal void _00A0(int P_0, int P_1)
		{
			int item = _2009_204A[P_0].Index;
			int item2 = _2009_204A[P_1].Index;
			if (UiCardList.MoveItem(_2009_204D, item, item2))
			{
				_2009_204B.Save();
			}
		}
	}

	[CompilerGenerated]
	private sealed class _2003
	{
		public WorldTravelPreset _2009_204F;

		public _2002 _2009_2050;

		internal void _00A0(int P_0)
		{
			_2009_2050._2009_204B._00A0(_2009_204F, _2009_2050._2009_204C);
		}
	}

	[CompilerGenerated]
	private sealed class _2004
	{
		public WorldTravelPreset _2009_2051;

		internal bool _00A0(WorldTravelData.MapEntry P_0)
		{
			return P_0.RowId == _2009_2051.MapRowId;
		}

		internal bool _00A0((uint Id, Vector3 Position, string Name) a)
		{
			return a.Id == _2009_2051.AetheryteId;
		}
	}

	private readonly UiCardList _2008_2047 = new UiCardList();

	public override void Draw()
	{
		_00A0();
		UiSection.Gap();
		_1680();
	}

	private void _00A0()
	{
		List<WorldTravelData.DcEntry> dcs = WorldTravelData.GetDcs();
		string currentDcName = WorldTravelData.GetCurrentDcName();
		UiSection.Header("一键跨服", string.IsNullOrEmpty(currentDcName) ? "读取大区中..." : currentDcName);
		List<string> list = dcs.FirstOrDefault((WorldTravelData.DcEntry P_0) => P_0.Name == currentDcName)?.Servers ?? new List<string>();
		UiForm.Begin("##travelDest");
		UiForm.Row("服务器");
		string selected;
		if (list.Count == 0)
		{
			ImGui.TextColored(Theme.Bad, "当前大区无可跨服服务器");
		}
		else if (UiCombo.Draw("##WTServer", Config.ManualServerName, list, out selected))
		{
			Config.ManualServerName = selected;
			Save();
		}
		UiForm.Row("版本");
		string[] array = new string[3] { "5.0", "6.0", "7.0" };
		int[] array2 = new int[3] { 3, 4, 5 };
		int num = Math.Max(0, Array.IndexOf(array2, Config.ManualExVersion));
		if (UiCombo.Draw("##WTVersion", array[num], array, out string selected2))
		{
			Config.ManualExVersion = array2[Array.IndexOf(array, selected2)];
			_00A0(Config.ManualExVersion);
			Save();
		}
		List<WorldTravelData.MapEntry> maps = WorldTravelData.GetMaps(Config.ManualExVersion);
		WorldTravelData.MapEntry mapEntry = maps.FirstOrDefault((WorldTravelData.MapEntry P_0) => P_0.RowId == Config.ManualMapRowId);
		UiForm.Row("地图");
		if (maps.Count == 0)
		{
			ImGui.TextDisabled("该版本无可传送地图");
		}
		else
		{
			List<string> list2 = maps.Select((WorldTravelData.MapEntry P_0) => P_0.Name).ToList();
			int num2 = maps.FindIndex((WorldTravelData.MapEntry P_0) => P_0.RowId == Config.ManualMapRowId);
			string preview = ((num2 >= 0) ? maps[num2].Name : "");
			if (UiCombo.Draw("##WTMap", preview, list2, out string selected3))
			{
				Config.ManualMapRowId = maps[list2.IndexOf(selected3)].RowId;
				Config.ManualAetheryteId = 0u;
				Save();
			}
		}
		List<(uint, Vector3, string)> list3 = ((mapEntry.Territory != 0) ? WorldTravelData.GetAetherytes(mapEntry) : new List<(uint, Vector3, string)>());
		UiForm.Row("水晶");
		if (mapEntry.Territory == 0)
		{
			ImGui.TextDisabled("先选择地图");
		}
		else if (list3.Count == 0)
		{
			ImGui.TextDisabled("该地图无可传送水晶");
		}
		else
		{
			List<string> list4 = list3.Select(((uint Id, Vector3 Position, string Name) a) => a.Name).ToList();
			int num3 = list3.FindIndex(((uint Id, Vector3 Position, string Name) a) => a.Id == Config.ManualAetheryteId);
			string preview2 = ((num3 >= 0) ? list4[num3] : "");
			if (UiCombo.Draw("##WTAetheryte", preview2, list4, out string selected4))
			{
				Config.ManualAetheryteId = list3[list4.IndexOf(selected4)].Item1;
				Save();
			}
		}
		UiForm.End();
		bool hasServer = !string.IsNullOrEmpty(Config.ManualServerName) && list.Contains(Config.ManualServerName);
		bool hasMap = mapEntry.Territory != 0;
		bool hasAetheryte = list3.Any(((uint Id, Vector3 Position, string Name) a) => a.Id == Config.ManualAetheryteId);
		bool flag = TravelSavePolicy.CanSave(hasServer, hasMap, hasAetheryte);
		Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
		float x = ImGui.GetContentRegionAvail().X;
		Vector2 vector = new Vector2(x, Theme.ControlHeight);
		bool flag2 = UiButton.Draw("一键跨服", UiVariant.Accent, vector, "左键：执行当前选择的跨服与水晶传送。\n右键：保存当前选择（快捷操作）。");
		bool flag3 = ImGui.GetIO().MouseClicked[1] && ImGui.IsMouseHoveringRect(cursorScreenPos, cursorScreenPos + vector, clip: true);
		bool flag4 = UiButton.Draw(tooltip: flag ? "把当前的服务器 / 地图 / 水晶保存到下方列表" : (TravelSavePolicy.DisabledReason(hasServer, hasMap, hasAetheryte) ?? "请先完成选择"), label: "保存当前选择", variant: UiVariant.Neutral, size: new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight), enabled: flag);
		if (flag2)
		{
			_2000(list, mapEntry, list3);
		}
		else if (flag4 | flag3)
		{
			_1680(list, mapEntry, list3);
		}
	}

	private bool _00A0(List<string> P_0, WorldTravelData.MapEntry P_1, List<(uint Id, Vector3 Position, string Name)> aetherytes)
	{
		if (!string.IsNullOrEmpty(Config.ManualServerName) && P_0.Contains(Config.ManualServerName) && P_1.Territory != 0)
		{
			return aetherytes.Any(((uint Id, Vector3 Position, string Name) a) => a.Id == Config.ManualAetheryteId);
		}
		return false;
	}

	private void _1680(List<string> P_0, WorldTravelData.MapEntry P_1, List<(uint Id, Vector3 Position, string Name)> aetherytes)
	{
		if (!_00A0(P_0, P_1, aetherytes))
		{
			Plugin.PrintEcho("一键跨服: 请先选择服务器/地图/水晶");
			return;
		}
		(uint, Vector3, string) tuple = aetherytes.FirstOrDefault(((uint Id, Vector3 Position, string Name) a) => a.Id == Config.ManualAetheryteId);
		WorldTravelPreset worldTravelPreset = new WorldTravelPreset
		{
			ServerName = Config.ManualServerName,
			ExVersion = Config.ManualExVersion,
			MapRowId = P_1.RowId,
			MapName = P_1.Name,
			AetheryteId = tuple.Item1,
			AetheryteName = tuple.Item3
		};
		if (Config.SavedWorldTravels.Any((WorldTravelPreset worldTravelPreset2) => _00A0(worldTravelPreset2, worldTravelPreset)))
		{
			AutomaticHunting.PrintEchoAlways("该一键跨服配置已保存，请勿重复保存");
			return;
		}
		Config.SavedWorldTravels.Add(worldTravelPreset);
		Save();
		Plugin.PrintEcho("已保存一键跨服配置");
	}

	private void _2000(List<string> P_0, WorldTravelData.MapEntry P_1, List<(uint Id, Vector3 Position, string Name)> aetherytes)
	{
		if (!_00A0(P_0, P_1, aetherytes))
		{
			Plugin.PrintEcho("一键跨服: 请先选择服务器/地图/水晶");
			return;
		}
		(uint, Vector3, string) tuple = aetherytes.FirstOrDefault(((uint Id, Vector3 Position, string Name) a) => a.Id == Config.ManualAetheryteId);
		Plugin.StartManualWorldTravel(Config.ManualServerName, P_1.Territory, tuple.Item1, tuple.Item2);
	}

	private void _1680()
	{
		UiSection.Header("已保存配置");
		List<WorldTravelPreset> savedWorldTravels = Config.SavedWorldTravels;
		string currentDcName = WorldTravelData.GetCurrentDcName();
		List<(int Index, WorldTravelPreset Preset)> list = new List<(int, WorldTravelPreset)>();
		for (int i = 0; i < savedWorldTravels.Count; i++)
		{
			if (string.IsNullOrEmpty(currentDcName) || WorldTravelData.GetDcOfServer(savedWorldTravels[i].ServerName) == currentDcName)
			{
				list.Add((i, savedWorldTravels[i]));
			}
		}
		if (list.Count == 0)
		{
			UiEmpty.Show("还没有保存的一键跨服配置", "选好服务器 / 地图 / 水晶后，点击上方「保存当前选择」即可保存到这里（右键「一键跨服」为快捷操作）");
			return;
		}
		List<string> list2 = WorldTravelData.GetDcs().FirstOrDefault((WorldTravelData.DcEntry P_0) => P_0.Name == currentDcName)?.Servers ?? new List<string>();
		int num = -1;
		List<UiListCard> list3 = new List<UiListCard>(list.Count);
		for (int num2 = 0; num2 < list.Count; num2++)
		{
			(int, WorldTravelPreset) tuple = list[num2];
			int item = tuple.Item1;
			WorldTravelPreset item2 = tuple.Item2;
			string tooltip = SettingsPage.FormatPreset(item2);
			string text = $"{item2.ExVersion + 2}.0";
			UiListCard uiListCard = new UiListCard
			{
				Id = $"wt{item}",
				Title = item2.MapName + " · " + item2.AetheryteName,
				Tooltip = tooltip,
				Draggable = true,
				DeleteTooltip = "删除此一键跨服配置",
				OnDelete = (int P_0) =>
				{
					num = list[P_0].Index;
				}
			};
			uiListCard.Prefixes.Add(new UiListCardPrefix
			{
				Text = item2.ServerName,
				Chars = 4f,
				Center = false,
				Color = Theme.TextSecondary
			});
			uiListCard.Prefixes.Add(new UiListCardPrefix
			{
				Text = text,
				Chars = 0f,
				Center = true,
				Box = true,
				Color = Theme.TextSecondary
			});
			uiListCard.Actions.Add(new UiListCardAction
			{
				Label = "一键就位",
				Variant = UiVariant.Success,
				Tooltip = "切换到该服务器并传送到记录的水晶",
				OnClick = delegate
				{
					_00A0(item2, list2);
				}
			});
			list3.Add(uiListCard);
		}
		_2008_2047.Draw("##wtCard", "WT_REORDER", list3, (int P_0, int P_1) =>
		{
			int item3 = list[P_0].Index;
			int item4 = list[P_1].Index;
			if (UiCardList.MoveItem(savedWorldTravels, item3, item4))
			{
				Save();
			}
		});
		if (num >= 0 && num < savedWorldTravels.Count)
		{
			savedWorldTravels.RemoveAt(num);
			Save();
			Plugin.PrintEcho("已删除一键跨服配置");
		}
	}

	private void _00A0(WorldTravelPreset P_0, List<string> P_1)
	{
		WorldTravelData.MapEntry map = WorldTravelData.GetMaps(P_0.ExVersion).FirstOrDefault((WorldTravelData.MapEntry mapEntry) => mapEntry.RowId == P_0.MapRowId);
		(uint, Vector3, string) tuple = WorldTravelData.GetAetherytes(map).FirstOrDefault(((uint Id, Vector3 Position, string Name) a) => a.Id == P_0.AetheryteId);
		if (!P_1.Contains(P_0.ServerName) || map.Territory == 0 || tuple.Item1 == 0)
		{
			Plugin.PrintEcho("保存的跨服配置在当前大区不可用或目标已失效，请重新选择后点击「保存当前选择」");
		}
		else
		{
			Plugin.StartManualWorldTravel(P_0.ServerName, map.Territory, tuple.Item1, tuple.Item2);
		}
	}

	private static bool _00A0(WorldTravelPreset P_0, WorldTravelPreset P_1)
	{
		if (P_0.ServerName == P_1.ServerName && P_0.ExVersion == P_1.ExVersion && P_0.MapRowId == P_1.MapRowId)
		{
			return P_0.AetheryteId == P_1.AetheryteId;
		}
		return false;
	}

	private void _00A0(int P_0)
	{
		Config.ManualMapRowId = 0u;
		Config.ManualAetheryteId = 0u;
		var (text, text2) = P_0 switch
		{
			5 => ("奥阔帕恰山", "瓦丘恩佩洛"), 
			4 => ("萨维奈岛", "新港"), 
			3 => ("雷克兰德", "乔布要塞"), 
			_ => (null, null), 
		};
		if (text == null || text2 == null)
		{
			return;
		}
		WorldTravelData.MapEntry map = WorldTravelData.GetMaps(P_0).FirstOrDefault((WorldTravelData.MapEntry mapEntry) => mapEntry.Name == text);
		if (map.Territory != 0)
		{
			Config.ManualMapRowId = map.RowId;
			Config.ManualAetheryteId = WorldTravelData.GetAetherytes(map).FirstOrDefault(((uint Id, Vector3 Position, string Name) a) => a.Name == text2).Id;
		}
	}

	[CompilerGenerated]
	private bool _00A0((uint Id, Vector3 Position, string Name) a)
	{
		return a.Id == Config.ManualAetheryteId;
	}

	[CompilerGenerated]
	private bool _1680((uint Id, Vector3 Position, string Name) a)
	{
		return a.Id == Config.ManualAetheryteId;
	}
}
