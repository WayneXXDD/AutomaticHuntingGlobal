using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using Dalamud.Game.ClientState.Objects.SubKinds;
using Lumina.Excel;
using Lumina.Excel.Sheets;
using _00A0;

namespace AutomaticHunting;

public static class WorldTravelData
{
	public sealed class DcEntry
	{
		public string Name = "";

		public List<string> Servers = new List<string>();
	}

	public readonly struct MapEntry(uint RowId, string Name, uint Territory) : IEquatable<MapEntry>
	{
		[CompilerGenerated]
		private readonly uint _2007_2062 = RowId;

		[CompilerGenerated]
		private readonly string _2007_2063 = Name;

		[CompilerGenerated]
		private readonly uint _2007_2064 = Territory;

		public uint RowId
		{
			[CompilerGenerated]
			get
			{
				return _2007_2062;
			}
			[CompilerGenerated]
			init
			{
				_2007_2062 = value;
			}
		}

		public string Name
		{
			[CompilerGenerated]
			get
			{
				return _2007_2063;
			}
			[CompilerGenerated]
			init
			{
				_2007_2063 = value;
			}
		}

		public uint Territory
		{
			[CompilerGenerated]
			get
			{
				return _2007_2064;
			}
			[CompilerGenerated]
			init
			{
				_2007_2064 = value;
			}
		}

		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("MapEntry");
			stringBuilder.Append(" { ");
			if (_00A0(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		[CompilerGenerated]
		private bool _00A0(StringBuilder P_0)
		{
			P_0.Append("RowId = ");
			P_0.Append(RowId.ToString());
			P_0.Append(", Name = ");
			P_0.Append((object?)Name);
			P_0.Append(", Territory = ");
			P_0.Append(Territory.ToString());
			return true;
		}

		[CompilerGenerated]
		public static bool operator !=(MapEntry left, MapEntry right)
		{
			return !(left == right);
		}

		[CompilerGenerated]
		public static bool operator ==(MapEntry left, MapEntry right)
		{
			return left.Equals(right);
		}

		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (EqualityComparer<uint>.Default.GetHashCode(_2007_2062) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_2007_2063)) * -1521134295 + EqualityComparer<uint>.Default.GetHashCode(_2007_2064);
		}

		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			if (obj is MapEntry)
			{
				return Equals((MapEntry)obj);
			}
			return false;
		}

		[CompilerGenerated]
		public bool Equals(MapEntry other)
		{
			if (EqualityComparer<uint>.Default.Equals(_2007_2062, other._2007_2062) && EqualityComparer<string>.Default.Equals(_2007_2063, other._2007_2063))
			{
				return EqualityComparer<uint>.Default.Equals(_2007_2064, other._2007_2064);
			}
			return false;
		}

		[CompilerGenerated]
		public void Deconstruct(out uint RowId, out string Name, out uint Territory)
		{
			RowId = this.RowId;
			Name = this.Name;
			Territory = this.Territory;
		}
	}

	public readonly struct AetheryteEntry(uint Id, string Name, Vector3 Position) : IEquatable<AetheryteEntry>
	{
		[CompilerGenerated]
		private readonly uint _2007_206A = Id;

		[CompilerGenerated]
		private readonly string _2007_206B = Name;

		[CompilerGenerated]
		private readonly Vector3 _2007_206C = Position;

		public uint Id
		{
			[CompilerGenerated]
			get
			{
				return _2007_206A;
			}
			[CompilerGenerated]
			init
			{
				_2007_206A = value;
			}
		}

		public string Name
		{
			[CompilerGenerated]
			get
			{
				return _2007_206B;
			}
			[CompilerGenerated]
			init
			{
				_2007_206B = value;
			}
		}

		public Vector3 Position
		{
			[CompilerGenerated]
			get
			{
				return _2007_206C;
			}
			[CompilerGenerated]
			init
			{
				_2007_206C = value;
			}
		}

		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("AetheryteEntry");
			stringBuilder.Append(" { ");
			if (_00A0(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		[CompilerGenerated]
		private bool _00A0(StringBuilder P_0)
		{
			P_0.Append("Id = ");
			P_0.Append(Id.ToString());
			P_0.Append(", Name = ");
			P_0.Append((object?)Name);
			P_0.Append(", Position = ");
			P_0.Append(Position.ToString());
			return true;
		}

		[CompilerGenerated]
		public static bool operator !=(AetheryteEntry left, AetheryteEntry right)
		{
			return !(left == right);
		}

		[CompilerGenerated]
		public static bool operator ==(AetheryteEntry left, AetheryteEntry right)
		{
			return left.Equals(right);
		}

		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (EqualityComparer<uint>.Default.GetHashCode(_2007_206A) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_2007_206B)) * -1521134295 + EqualityComparer<Vector3>.Default.GetHashCode(_2007_206C);
		}

		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			if (obj is AetheryteEntry)
			{
				return Equals((AetheryteEntry)obj);
			}
			return false;
		}

		[CompilerGenerated]
		public bool Equals(AetheryteEntry other)
		{
			if (EqualityComparer<uint>.Default.Equals(_2007_206A, other._2007_206A) && EqualityComparer<string>.Default.Equals(_2007_206B, other._2007_206B))
			{
				return EqualityComparer<Vector3>.Default.Equals(_2007_206C, other._2007_206C);
			}
			return false;
		}

		[CompilerGenerated]
		public void Deconstruct(out uint Id, out string Name, out Vector3 Position)
		{
			Id = this.Id;
			Name = this.Name;
			Position = this.Position;
		}
	}

	[CompilerGenerated]
	private sealed class _2001
	{
		public List<(uint Id, Vector3 Position, string Name)> _2007_206D;

		internal IEnumerable<(uint Id, Vector3 Position, string Name)> _00A0(string P_0)
		{
			return _2007_206D.Where(((uint Id, Vector3 Position, string Name) a) => a.Name == P_0);
		}
	}

	[CompilerGenerated]
	private sealed class _2002
	{
		public string _2007_206E;

		internal bool _00A0((uint Id, Vector3 Position, string Name) a)
		{
			return a.Name == _2007_206E;
		}
	}

	[CompilerGenerated]
	private sealed class _2003
	{
		public string _2007_206F;

		internal bool _00A0(DcEntry P_0)
		{
			return P_0.Servers.Contains(_2007_206F);
		}
	}

	[CompilerGenerated]
	private sealed class _2004
	{
		public string[] _2007_3000;

		internal int _00A0(MapEntry P_0, MapEntry P_1)
		{
			return Array.IndexOf(_2007_3000, P_0.Name).CompareTo(Array.IndexOf(_2007_3000, P_1.Name));
		}
	}

	private static List<DcEntry>? _2000_202B;

	private static readonly Dictionary<int, List<MapEntry>> _2000_202C = new Dictionary<int, List<MapEntry>>();

	private static readonly Dictionary<uint, List<(uint Id, Vector3 Position, string Name)>> _2000_202D = new Dictionary<uint, List<(uint, Vector3, string)>>();

	public static List<DcEntry> GetDcs()
	{
		if (_2000_202B != null)
		{
			return _2000_202B;
		}
		List<DcEntry> list = new List<DcEntry>();
		try
		{
			(string, string[])[] array = global::_00A0._00A0._2000_2028;
			for (int i = 0; i < array.Length; i++)
			{
				(string, string[]) tuple = array[i];
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
		_2000_202B = list;
		return list;
	}

	public static string GetDcOfServer(string serverName)
	{
		if (string.IsNullOrEmpty(serverName))
		{
			return "";
		}
		(string, string[])[] array = global::_00A0._00A0._2000_2028;
		for (int i = 0; i < array.Length; i++)
		{
			(string, string[]) tuple = array[i];
			if (Array.IndexOf(tuple.Item2, serverName) >= 0)
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
			string item = row.Name.ExtractText();
			return GetDcs().FirstOrDefault((DcEntry P_0) => P_0.Servers.Contains(item))?.Name ?? "";
		}
		catch (Exception exception)
		{
			AutomaticHunting.Log.Warning(exception, "AutomaticHunting: 读取当前大区失败");
			return "";
		}
	}

	public static List<MapEntry> GetMaps(int exVersion)
	{
		if (!global::_00A0._00A0._2000_2029.TryGetValue(exVersion, out string[] value))
		{
			return new List<MapEntry>();
		}
		if (_2000_202C.TryGetValue(exVersion, out List<MapEntry> value2))
		{
			return value2;
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
					if (rowId != 0 && excelSheet2.TryGetRow(rowId, out var row) && row.ExVersion.RowId == (uint)exVersion && excelSheet3.TryGetRow(row.PlaceName.RowId, out var row2))
					{
						string text = row2.Name.ExtractText();
						if (!string.IsNullOrEmpty(text) && value.Contains(text) && AutomaticHunting._2002(rowId).Count != 0 && hashSet.Add(rowId))
						{
							list.Add(new MapEntry(item.RowId, text, rowId));
						}
					}
				}
				list.Sort((MapEntry P_0, MapEntry P_1) => Array.IndexOf(value, P_0.Name).CompareTo(Array.IndexOf(value, P_1.Name)));
			}
		}
		catch (Exception exception)
		{
			AutomaticHunting.Log.Warning(exception, $"AutomaticHunting: 构建地图列表失败(ExVersion={exVersion})");
		}
		_2000_202C[exVersion] = list;
		return list;
	}

	public static List<(uint Id, Vector3 Position, string Name)> GetAetherytes(MapEntry map)
	{
		if (map.Territory == 0 || string.IsNullOrEmpty(map.Name) || !global::_00A0._00A0._2000_202A.TryGetValue(map.Name, out string[] value))
		{
			return new List<(uint, Vector3, string)>();
		}
		if (_2000_202D.TryGetValue(map.RowId, out List<(uint, Vector3, string)> value2))
		{
			return value2;
		}
		List<(uint Id, Vector3 Position, string Name)> source = AutomaticHunting._2002(map.Territory);
		List<(uint, Vector3, string)> list = value.SelectMany((string P_0) => source.Where(((uint Id, Vector3 Position, string Name) a) => a.Name == P_0)).ToList();
		_2000_202D[map.RowId] = list;
		return list;
	}
}
