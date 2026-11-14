using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using AutomaticHunting.UI.Components;
using Dalamud.Bindings.ImGui;

namespace AutomaticHunting.UI.Pages;

public sealed class TravelPage : SettingsPage
{
	[Serializable]
	[CompilerGenerated]
	public sealed class RecoveredType_146
	{
		public static readonly RecoveredType_146 field_896 = new RecoveredType_146();

		public static Func<WorldTravelData.MapEntry, string> field_897;

		public static Func<(uint Id, Vector3 Position, string Name), string> field_898;

		internal string Method_964(WorldTravelData.MapEntry arg0)
		{
			return arg0.Name;
		}

		internal string Method_965((uint Id, Vector3 Position, string Name) arg0)
		{
			return arg0.Name;
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_147
	{
		public string field_899;

		public string field_900;

		internal bool Method_967(WorldTravelData.MapEntry arg0)
		{
			return arg0.Name == field_899;
		}

		internal bool Method_968((uint Id, Vector3 Position, string Name) arg0)
		{
			return arg0.Name == field_900;
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_148
	{
		public string field_901;

		public TravelPage field_902;

		internal bool Method_970(WorldTravelData.DcEntry arg0)
		{
			return arg0.Name == field_901;
		}

		internal bool Method_971(WorldTravelData.MapEntry arg0)
		{
			return arg0.RowId == field_902.Config.ManualMapRowId;
		}

		internal bool Method_972(WorldTravelData.MapEntry arg0)
		{
			return arg0.RowId == field_902.Config.ManualMapRowId;
		}

		internal bool Method_973((uint Id, Vector3 Position, string Name) arg0)
		{
			return arg0.Id == field_902.Config.ManualAetheryteId;
		}

		internal bool Method_974((uint Id, Vector3 Position, string Name) arg0)
		{
			return arg0.Id == field_902.Config.ManualAetheryteId;
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_149
	{
		public TravelPage field_903;

		public WorldTravelPreset field_904;

		internal bool Method_976((uint Id, Vector3 Position, string Name) arg0)
		{
			return arg0.Id == field_903.Config.ManualAetheryteId;
		}

		internal bool Method_977(WorldTravelPreset arg0)
		{
			return Method_958(arg0, field_904);
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_150
	{
		public string field_905;

		public int field_906;

		public List<(int Index, WorldTravelPreset Preset)> field_907;

		public TravelPage field_908;

		public List<string> field_909;

		public List<WorldTravelPreset> field_910;

		public Action<int> field_911;

		internal bool Method_979(WorldTravelData.DcEntry arg0)
		{
			return arg0.Name == field_905;
		}

		internal void Method_980(int arg0)
		{
			field_906 = field_907[arg0].Index;
		}

		internal void Method_981(int arg0, int arg1)
		{
			int item = field_907[arg0].Index;
			int item2 = field_907[arg1].Index;
			if (UiCardList.MoveItem(field_910, item, item2))
			{
				field_908.Save();
			}
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_151
	{
		public WorldTravelPreset field_912;

		public RecoveredType_150 field_913;

		internal void Method_983(int arg0)
		{
			field_913.field_908.Method_957(field_912, field_913.field_909);
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_152
	{
		public WorldTravelPreset field_914;

		internal bool Method_985(WorldTravelData.MapEntry arg0)
		{
			return arg0.RowId == field_914.MapRowId;
		}

		internal bool Method_986((uint Id, Vector3 Position, string Name) arg0)
		{
			return arg0.Id == field_914.AetheryteId;
		}
	}

	private readonly UiCardList field_895 = new UiCardList();

	public TravelPage(AutomaticHunting arg0)
		: base(arg0)
	{
	}

	public override void Draw()
	{
		Method_952();
		UiSection.Gap();
		Method_956();
	}

	private void Method_952()
	{
		List<WorldTravelData.DcEntry> dcs = WorldTravelData.GetDcs();
		string field_901 = WorldTravelData.GetCurrentDcName();
		UiSection.Header("一鍵跨服", string.IsNullOrEmpty(field_901) ? "讀取資料中心中..." : field_901);
		List<string> list = dcs.FirstOrDefault((WorldTravelData.DcEntry dcEntry) => dcEntry.Name == field_901)?.Servers ?? new List<string>();
		UiForm.Begin("##travelDest");
		UiForm.Row("伺服器");
		string arg;
		if (list.Count == 0)
		{
			TraditionalText.TextColored(Theme.Bad, "目前資料中心無可跨服伺服器");
		}
		else if (UiCombo.Draw("##WTServer", Config.ManualServerName, list, out arg))
		{
			Config.ManualServerName = arg;
			Save();
		}
		UiForm.Row("版本");
		string[] array = new string[3] { "5.0", "6.0", "7.0" };
		int[] array2 = new int[3] { 3, 4, 5 };
		int num = Math.Max(0, Array.IndexOf(array2, Config.ManualExVersion));
		if (UiCombo.Draw("##WTVersion", array[num], array, out string arg2))
		{
			Config.ManualExVersion = array2[Array.IndexOf(array, arg2)];
			Method_959(Config.ManualExVersion);
			Save();
		}
		List<WorldTravelData.MapEntry> maps = WorldTravelData.GetMaps(Config.ManualExVersion);
		WorldTravelData.MapEntry mapEntry = maps.FirstOrDefault((WorldTravelData.MapEntry mapEntry2) => mapEntry2.RowId == Config.ManualMapRowId);
		UiForm.Row("地圖");
		if (maps.Count == 0)
		{
			ImGui.TextDisabled("該版本無可傳送地圖");
		}
		else
		{
			List<string> list2 = maps.Select((WorldTravelData.MapEntry mapEntry2) => mapEntry2.Name).ToList();
			int num2 = maps.FindIndex((WorldTravelData.MapEntry mapEntry2) => mapEntry2.RowId == Config.ManualMapRowId);
			string arg3 = ((num2 >= 0) ? maps[num2].Name : "");
			if (UiCombo.Draw("##WTMap", arg3, list2, out string arg4))
			{
				Config.ManualMapRowId = maps[list2.IndexOf(arg4)].RowId;
				Config.ManualAetheryteId = 0u;
				Save();
			}
		}
		List<(uint, Vector3, string)> list3 = ((mapEntry.Territory != 0) ? WorldTravelData.GetAetherytes(mapEntry) : new List<(uint, Vector3, string)>());
		UiForm.Row("水晶");
		if (mapEntry.Territory == 0)
		{
			ImGui.TextDisabled("先選擇地圖");
		}
		else if (list3.Count == 0)
		{
			ImGui.TextDisabled("該地圖無可傳送水晶");
		}
		else
		{
			List<string> list4 = list3.Select(((uint Id, Vector3 Position, string Name) tuple) => tuple.Name).ToList();
			int num3 = list3.FindIndex(((uint Id, Vector3 Position, string Name) tuple) => tuple.Id == Config.ManualAetheryteId);
			string arg5 = ((num3 >= 0) ? list4[num3] : "");
			if (UiCombo.Draw("##WTAetheryte", arg5, list4, out string arg6))
			{
				Config.ManualAetheryteId = list3[list4.IndexOf(arg6)].Item1;
				Save();
			}
		}
		UiForm.End();
		bool arg7 = !string.IsNullOrEmpty(Config.ManualServerName) && list.Contains(Config.ManualServerName);
		bool arg8 = mapEntry.Territory != 0;
		bool arg9 = list3.Any(((uint Id, Vector3 Position, string Name) tuple) => tuple.Id == Config.ManualAetheryteId);
		bool flag = TravelSavePolicy.CanSave(arg7, arg8, arg9);
		Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
		float x = ImGui.GetContentRegionAvail().X;
		Vector2 vector = new Vector2(x, Theme.ControlHeight);
		bool flag2 = UiButton.Draw("一鍵跨服", UiVariant.Accent, vector, "左鍵：執行目前選擇的跨服與水晶傳送。\n右鍵：儲存目前選擇（快捷操作）。");
		bool flag3 = ImGui.GetIO().MouseClicked[1] && ImGui.IsMouseHoveringRect(cursorScreenPos, cursorScreenPos + vector, clip: true);
		bool flag4 = UiButton.Draw(arg3: flag ? "把目前的伺服器 / 地圖 / 水晶儲存到下方列表" : (TravelSavePolicy.DisabledReason(arg7, arg8, arg9) ?? "請先完成選擇"), arg0: "儲存目前選擇", arg1: UiVariant.Neutral, arg2: new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight), arg4: flag);
		if (flag2)
		{
			Method_955(list, mapEntry, list3);
		}
		else if (flag4 | flag3)
		{
			Method_954(list, mapEntry, list3);
		}
	}

	private bool Method_953(List<string> arg0, WorldTravelData.MapEntry arg1, List<(uint Id, Vector3 Position, string Name)> arg2)
	{
		if (!string.IsNullOrEmpty(Config.ManualServerName) && arg0.Contains(Config.ManualServerName) && arg1.Territory != 0)
		{
			return arg2.Any(((uint Id, Vector3 Position, string Name) tuple) => tuple.Id == Config.ManualAetheryteId);
		}
		return false;
	}

	private void Method_954(List<string> arg0, WorldTravelData.MapEntry arg1, List<(uint Id, Vector3 Position, string Name)> arg2)
	{
		if (!Method_953(arg0, arg1, arg2))
		{
			Plugin.PrintEcho("一鍵跨服: 請先選擇伺服器/地圖/水晶");
			return;
		}
		(uint, Vector3, string) tuple = arg2.FirstOrDefault(((uint Id, Vector3 Position, string Name) tuple2) => tuple2.Id == Config.ManualAetheryteId);
		WorldTravelPreset field_904 = new WorldTravelPreset
		{
			ServerName = Config.ManualServerName,
			ExVersion = Config.ManualExVersion,
			MapRowId = arg1.RowId,
			MapName = arg1.Name,
			AetheryteId = tuple.Item1,
			AetheryteName = tuple.Item3
		};
		if (Config.SavedWorldTravels.Any((WorldTravelPreset arg3) => Method_958(arg3, field_904)))
		{
			AutomaticHunting.PrintEchoAlways("該一鍵跨服設定已儲存，請勿重複儲存");
			return;
		}
		Config.SavedWorldTravels.Add(field_904);
		Save();
		Plugin.PrintEcho("已儲存一鍵跨服設定");
	}

	private void Method_955(List<string> arg0, WorldTravelData.MapEntry arg1, List<(uint Id, Vector3 Position, string Name)> arg2)
	{
		if (!Method_953(arg0, arg1, arg2))
		{
			Plugin.PrintEcho("一鍵跨服: 請先選擇伺服器/地圖/水晶");
			return;
		}
		(uint, Vector3, string) tuple = arg2.FirstOrDefault(((uint Id, Vector3 Position, string Name) tuple2) => tuple2.Id == Config.ManualAetheryteId);
		Plugin.StartManualWorldTravel(Config.ManualServerName, arg1.Territory, tuple.Item1, tuple.Item2);
	}

	private void Method_956()
	{
		UiSection.Header("已儲存設定");
		List<WorldTravelPreset> field_910 = Config.SavedWorldTravels;
		string field_911 = WorldTravelData.GetCurrentDcName();
		List<(int Index, WorldTravelPreset Preset)> field_912 = new List<(int, WorldTravelPreset)>();
		for (int i = 0; i < field_910.Count; i++)
		{
			if (string.IsNullOrEmpty(field_911) || WorldTravelData.GetDcOfServer(field_910[i].ServerName) == field_911)
			{
				field_912.Add((i, field_910[i]));
			}
		}
		if (field_912.Count == 0)
		{
			UiEmpty.Show("還沒有儲存的一鍵跨服設定", "選好伺服器 / 地圖 / 水晶後，點擊上方「儲存目前選擇」即可儲存到這裡（右鍵「一鍵跨服」為快捷操作）");
			return;
		}
		List<string> field_913 = WorldTravelData.GetDcs().FirstOrDefault((WorldTravelData.DcEntry arg0) => arg0.Name == field_911)?.Servers ?? new List<string>();
		int field_914 = -1;
		List<UiListCard> list = new List<UiListCard>(field_912.Count);
		for (int num = 0; num < field_912.Count; num++)
		{
			(int, WorldTravelPreset) tuple = field_912[num];
			int item = tuple.Item1;
			WorldTravelPreset field_915 = tuple.Item2;
			string tooltip = SettingsPage.FormatPreset(field_915);
			string text = $"{field_915.ExVersion + 2}.0";
			UiListCard uiListCard = new UiListCard
			{
				Id = $"wt{item}",
				Title = field_915.MapName + " · " + field_915.AetheryteName,
				Tooltip = tooltip,
				Draggable = true,
				DeleteTooltip = "刪除此一鍵跨服設定",
				OnDelete = (int arg0) =>
				{
					field_914 = field_912[arg0].Index;
				}
			};
			uiListCard.Prefixes.Add(new UiListCardPrefix
			{
				Text = field_915.ServerName,
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
				Label = "一鍵就位",
				Variant = UiVariant.Success,
				Tooltip = "切換到該伺服器並傳送到記錄的水晶",
				OnClick = (int arg0) =>
				{
					Method_957(field_915, field_913);
				}
			});
			list.Add(uiListCard);
		}
		field_895.Draw("##wtCard", "WT_REORDER", list, (int arg0, int arg1) =>
		{
			int item2 = field_912[arg0].Index;
			int item3 = field_912[arg1].Index;
			if (UiCardList.MoveItem(field_910, item2, item3))
			{
				Save();
			}
		});
		if (field_914 >= 0 && field_914 < field_910.Count)
		{
			field_910.RemoveAt(field_914);
			Save();
			Plugin.PrintEcho("已刪除一鍵跨服設定");
		}
	}

	private void Method_957(WorldTravelPreset arg0, List<string> arg1)
	{
		WorldTravelData.MapEntry arg2 = WorldTravelData.GetMaps(arg0.ExVersion).FirstOrDefault((WorldTravelData.MapEntry mapEntry) => mapEntry.RowId == arg0.MapRowId);
		(uint, Vector3, string) tuple = WorldTravelData.GetAetherytes(arg2).FirstOrDefault(((uint Id, Vector3 Position, string Name) tuple2) => tuple2.Id == arg0.AetheryteId);
		if (!arg1.Contains(arg0.ServerName) || arg2.Territory == 0 || tuple.Item1 == 0)
		{
			Plugin.PrintEcho("儲存的跨服設定在目前資料中心不可用或目標已失效，請重新選擇後點擊「儲存目前選擇」");
		}
		else
		{
			Plugin.StartManualWorldTravel(arg0.ServerName, arg2.Territory, tuple.Item1, tuple.Item2);
		}
	}

	private static bool Method_958(WorldTravelPreset arg0, WorldTravelPreset arg1)
	{
		if (arg0.ServerName == arg1.ServerName && arg0.ExVersion == arg1.ExVersion && arg0.MapRowId == arg1.MapRowId)
		{
			return arg0.AetheryteId == arg1.AetheryteId;
		}
		return false;
	}

	private void Method_959(int arg0)
	{
		Config.ManualMapRowId = 0u;
		Config.ManualAetheryteId = 0u;
		var (field_899, field_900) = arg0 switch
		{
			5 => ("奧闊帕恰山", "瓦丘恩佩洛"), 
			4 => ("薩維奈島", "新港"), 
			3 => ("雷克蘭德", "喬布要塞"), 
			_ => (null, null), 
		};
		if (field_899 == null || field_900 == null)
		{
			return;
		}
		WorldTravelData.MapEntry arg1 = WorldTravelData.GetMaps(arg0).FirstOrDefault((WorldTravelData.MapEntry mapEntry) => mapEntry.Name == field_899);
		if (arg1.Territory != 0)
		{
			Config.ManualMapRowId = arg1.RowId;
			Config.ManualAetheryteId = WorldTravelData.GetAetherytes(arg1).FirstOrDefault(((uint Id, Vector3 Position, string Name) tuple2) => tuple2.Name == field_900).Id;
		}
	}

	[CompilerGenerated]
	private bool Method_960((uint Id, Vector3 Position, string Name) arg0)
	{
		return arg0.Id == Config.ManualAetheryteId;
	}

	[CompilerGenerated]
	private bool Method_961((uint Id, Vector3 Position, string Name) arg0)
	{
		return arg0.Id == Config.ManualAetheryteId;
	}
}
