using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using Dalamud.Game.ClientState.Objects.SubKinds;
using Lumina.Excel;
using Lumina.Excel.Sheets;
using Recovered;

namespace AutomaticHunting;

public static class WorldTravelData
{
	public sealed class DcEntry
	{
		public string Name = "";

		public List<string> Servers = new List<string>();
	}

	public readonly struct MapEntry : IEquatable<MapEntry>
	{
		[CompilerGenerated]
		private readonly uint field_651;

		[CompilerGenerated]
		private readonly string field_652;

		[CompilerGenerated]
		private readonly uint field_653;

		public uint RowId
		{
			[CompilerGenerated]
			get
			{
				return field_651;
			}
			[CompilerGenerated]
			init
			{
				field_651 = value;
			}
		}

		public string Name
		{
			[CompilerGenerated]
			get
			{
				return field_652;
			}
			[CompilerGenerated]
			init
			{
				field_652 = value;
			}
		}

		public uint Territory
		{
			[CompilerGenerated]
			get
			{
				return field_653;
			}
			[CompilerGenerated]
			init
			{
				field_653 = value;
			}
		}

		public MapEntry(uint arg0, string arg1, uint arg2)
		{
			field_651 = arg0;
			field_652 = arg1;
			field_653 = arg2;
		}

		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("MapEntry");
			stringBuilder.Append(" { ");
			if (Method_653(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		[CompilerGenerated]
		private bool Method_653(StringBuilder arg0)
		{
			arg0.Append("RowId = ");
			arg0.Append(RowId.ToString());
			arg0.Append(", Name = ");
			arg0.Append((object?)Name);
			arg0.Append(", Territory = ");
			arg0.Append(Territory.ToString());
			return true;
		}

		[CompilerGenerated]
		public static bool operator !=(MapEntry arg0, MapEntry arg1)
		{
			return !(arg0 == arg1);
		}

		[CompilerGenerated]
		public static bool operator ==(MapEntry arg0, MapEntry arg1)
		{
			return arg0.Equals(arg1);
		}

		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (EqualityComparer<uint>.Default.GetHashCode(field_651) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(field_652)) * -1521134295 + EqualityComparer<uint>.Default.GetHashCode(field_653);
		}

		[CompilerGenerated]
		public override bool Equals(object arg0)
		{
			if (arg0 is MapEntry)
			{
				return Equals((MapEntry)arg0);
			}
			return false;
		}

		[CompilerGenerated]
		public bool Equals(MapEntry arg0)
		{
			if (EqualityComparer<uint>.Default.Equals(field_651, arg0.field_651) && EqualityComparer<string>.Default.Equals(field_652, arg0.field_652))
			{
				return EqualityComparer<uint>.Default.Equals(field_653, arg0.field_653);
			}
			return false;
		}

		[CompilerGenerated]
		public void Deconstruct(out uint arg0, out string arg1, out uint arg2)
		{
			arg0 = RowId;
			arg1 = Name;
			arg2 = Territory;
		}
	}

	public readonly struct AetheryteEntry : IEquatable<AetheryteEntry>
	{
		[CompilerGenerated]
		private readonly uint field_654;

		[CompilerGenerated]
		private readonly string field_655;

		[CompilerGenerated]
		private readonly Vector3 field_656;

		public uint Id
		{
			[CompilerGenerated]
			get
			{
				return field_654;
			}
			[CompilerGenerated]
			init
			{
				field_654 = value;
			}
		}

		public string Name
		{
			[CompilerGenerated]
			get
			{
				return field_655;
			}
			[CompilerGenerated]
			init
			{
				field_655 = value;
			}
		}

		public Vector3 Position
		{
			[CompilerGenerated]
			get
			{
				return field_656;
			}
			[CompilerGenerated]
			init
			{
				field_656 = value;
			}
		}

		public AetheryteEntry(uint arg0, string arg1, Vector3 arg2)
		{
			field_654 = arg0;
			field_655 = arg1;
			field_656 = arg2;
		}

		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("AetheryteEntry");
			stringBuilder.Append(" { ");
			if (Method_668(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		[CompilerGenerated]
		private bool Method_668(StringBuilder arg0)
		{
			arg0.Append("Id = ");
			arg0.Append(Id.ToString());
			arg0.Append(", Name = ");
			arg0.Append((object?)Name);
			arg0.Append(", Position = ");
			arg0.Append(Position.ToString());
			return true;
		}

		[CompilerGenerated]
		public static bool operator !=(AetheryteEntry arg0, AetheryteEntry arg1)
		{
			return !(arg0 == arg1);
		}

		[CompilerGenerated]
		public static bool operator ==(AetheryteEntry arg0, AetheryteEntry arg1)
		{
			return arg0.Equals(arg1);
		}

		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (EqualityComparer<uint>.Default.GetHashCode(field_654) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(field_655)) * -1521134295 + EqualityComparer<Vector3>.Default.GetHashCode(field_656);
		}

		[CompilerGenerated]
		public override bool Equals(object arg0)
		{
			if (arg0 is AetheryteEntry)
			{
				return Equals((AetheryteEntry)arg0);
			}
			return false;
		}

		[CompilerGenerated]
		public bool Equals(AetheryteEntry arg0)
		{
			if (EqualityComparer<uint>.Default.Equals(field_654, arg0.field_654) && EqualityComparer<string>.Default.Equals(field_655, arg0.field_655))
			{
				return EqualityComparer<Vector3>.Default.Equals(field_656, arg0.field_656);
			}
			return false;
		}

		[CompilerGenerated]
		public void Deconstruct(out uint arg0, out string arg1, out Vector3 arg2)
		{
			arg0 = Id;
			arg1 = Name;
			arg2 = Position;
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_105
	{
		public List<(uint Id, Vector3 Position, string Name)> field_657;

		internal IEnumerable<(uint Id, Vector3 Position, string Name)> Method_676(string arg0)
		{
			return field_657.Where(((uint Id, Vector3 Position, string Name) tuple) => tuple.Name == arg0);
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_106
	{
		public string field_658;

		internal bool Method_678((uint Id, Vector3 Position, string Name) arg0)
		{
			return arg0.Name == field_658;
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_107
	{
		public string field_659;

		internal bool Method_680(DcEntry arg0)
		{
			return arg0.Servers.Contains(field_659);
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_108
	{
		public string[] field_660;

		internal int Method_682(MapEntry arg0, MapEntry arg1)
		{
			return Array.IndexOf(field_660, arg0.Name).CompareTo(Array.IndexOf(field_660, arg1.Name));
		}
	}

	private static List<DcEntry>? field_646;

	private static readonly Dictionary<int, List<MapEntry>> field_647 = new Dictionary<int, List<MapEntry>>();

	private static readonly Dictionary<uint, List<(uint Id, Vector3 Position, string Name)>> field_648 = new Dictionary<uint, List<(uint, Vector3, string)>>();

	public static List<DcEntry> GetDcs()
	{
		if (field_646 != null)
		{
			return field_646;
		}
		List<DcEntry> list = new List<DcEntry>();
		try
		{
			(string, string[])[] field_ = RecoveredType_100.field_643;
			for (int i = 0; i < field_.Length; i++)
			{
				(string, string[]) tuple = field_[i];
				list.Add(new DcEntry
				{
					Name = tuple.Item1,
					Servers = tuple.Item2.Where(CrossWorldData.ServerWorldTravel.ContainsKey).ToList()
				});
			}
		}
		catch (Exception exception)
		{
			AutomaticHunting.Log.Warning(exception, "AutomaticHunting: 构建大区列表失败");
		}
		field_646 = list;
		return list;
	}

	public static string GetDcOfServer(string arg0)
	{
		if (string.IsNullOrEmpty(arg0))
		{
			return "";
		}
		(string, string[])[] field_ = RecoveredType_100.field_643;
		for (int i = 0; i < field_.Length; i++)
		{
			(string, string[]) tuple = field_[i];
			if (Array.IndexOf(tuple.Item2, arg0) >= 0)
			{
				return tuple.Item1;
			}
		}
		return "";
	}

	public static string GetCurrentDcName()
	{
		try
		{
			IPlayerCharacter localPlayer = AutomaticHunting.ObjectTable.LocalPlayer;
			if (localPlayer == null)
			{
				return "";
			}
			ExcelSheet<World> excelSheet = AutomaticHunting.DataManager.GetExcelSheet<World>();
			if (excelSheet == null || !excelSheet.TryGetRow(localPlayer.CurrentWorld.RowId, out var row))
			{
				return "";
			}
			string field_659 = row.Name.ExtractText();
			return GetDcs().FirstOrDefault((DcEntry arg0) => arg0.Servers.Contains(field_659))?.Name ?? "";
		}
		catch (Exception exception)
		{
			AutomaticHunting.Log.Warning(exception, "AutomaticHunting: 读取当前大区失败");
			return "";
		}
	}

	public static List<MapEntry> GetMaps(int arg0)
	{
		if (!RecoveredType_100.field_644.TryGetValue(arg0, out string[] field_660))
		{
			return new List<MapEntry>();
		}
		if (field_647.TryGetValue(arg0, out List<MapEntry> value))
		{
			return value;
		}
		List<MapEntry> list = new List<MapEntry>();
		try
		{
			ExcelSheet<Map> excelSheet = AutomaticHunting.DataManager.GetExcelSheet<Map>();
			ExcelSheet<TerritoryType> excelSheet2 = AutomaticHunting.DataManager.GetExcelSheet<TerritoryType>();
			ExcelSheet<PlaceName> excelSheet3 = AutomaticHunting.DataManager.GetExcelSheet<PlaceName>();
			if (excelSheet != null && excelSheet2 != null && excelSheet3 != null)
			{
				HashSet<uint> hashSet = new HashSet<uint>();
				foreach (Map item in excelSheet)
				{
					if (item.IsEvent)
					{
						continue;
					}
					uint rowId = item.TerritoryType.RowId;
					if (rowId != 0 && excelSheet2.TryGetRow(rowId, out var row) && row.ExVersion.RowId == (uint)arg0 && excelSheet3.TryGetRow(row.PlaceName.RowId, out var row2))
					{
						string text = row2.Name.ExtractText();
						if (!string.IsNullOrEmpty(text) && field_660.Contains(text) && AutomaticHunting.Method_127(rowId).Count != 0 && hashSet.Add(rowId))
						{
							list.Add(new MapEntry(item.RowId, text, rowId));
						}
					}
				}
				list.Sort((MapEntry mapEntry, MapEntry mapEntry2) => Array.IndexOf(field_660, mapEntry.Name).CompareTo(Array.IndexOf(field_660, mapEntry2.Name)));
			}
		}
		catch (Exception exception)
		{
			AutomaticHunting.Log.Warning(exception, $"AutomaticHunting: 构建地图列表失败(ExVersion={arg0})");
		}
		field_647[arg0] = list;
		return list;
	}

	public static List<(uint Id, Vector3 Position, string Name)> GetAetherytes(MapEntry arg0)
	{
		if (arg0.Territory == 0 || string.IsNullOrEmpty(arg0.Name) || !RecoveredType_100.field_645.TryGetValue(arg0.Name, out string[] value))
		{
			return new List<(uint, Vector3, string)>();
		}
		if (field_648.TryGetValue(arg0.RowId, out List<(uint, Vector3, string)> value2))
		{
			return value2;
		}
		List<(uint Id, Vector3 Position, string Name)> field_657 = AutomaticHunting.Method_127(arg0.Territory);
		List<(uint, Vector3, string)> list = value.SelectMany((string text) => field_657.Where(((uint Id, Vector3 Position, string Name) tuple) => tuple.Name == text)).ToList();
		field_648[arg0.RowId] = list;
		return list;
	}
}
