using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using AutomaticHunting.Services;
using AutomaticHunting.UI;
using AutomaticHunting.UI.Components;
using Dalamud.Game.Chat;
using Dalamud.Game.ClientState.Conditions;
using Dalamud.Game.ClientState.Objects.SubKinds;
using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Game.Command;
using Dalamud.Game.Text;
using Dalamud.Game.Text.SeStringHandling;
using Dalamud.Game.Text.SeStringHandling.Payloads;
using Dalamud.Interface.Windowing;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Plugin.Ipc;
using Dalamud.Plugin.Services;
using ECommons;
using FFXIVClientStructs.FFXIV.Client.Game;
using FFXIVClientStructs.FFXIV.Client.Game.Character;
using FFXIVClientStructs.FFXIV.Client.Game.Control;
using FFXIVClientStructs.FFXIV.Client.Game.UI;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using Lumina.Excel;
using Lumina.Excel.Sheets;
using Recovered;

namespace AutomaticHunting;

public sealed class AutomaticHunting : IDalamudPlugin, IDisposable
{
	public readonly struct RecoveredType_3 : IEquatable<RecoveredType_3>
	{
		[CompilerGenerated]
		private readonly uint field_97;

		[CompilerGenerated]
		private readonly Vector3 field_98;

		public uint TerritoryType
		{
			[CompilerGenerated]
			get
			{
				return field_97;
			}
			[CompilerGenerated]
			init
			{
				field_97 = value;
			}
		}

		public Vector3 Position
		{
			[CompilerGenerated]
			get
			{
				return field_98;
			}
			[CompilerGenerated]
			init
			{
				field_98 = value;
			}
		}

		public RecoveredType_3(uint arg0, Vector3 arg1)
		{
			field_97 = arg0;
			field_98 = arg1;
		}

		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("NavigationRequest");
			stringBuilder.Append(" { ");
			if (Method_219(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		[CompilerGenerated]
		private bool Method_219(StringBuilder arg0)
		{
			arg0.Append("TerritoryType = ");
			arg0.Append(TerritoryType.ToString());
			arg0.Append(", Position = ");
			arg0.Append(Position.ToString());
			return true;
		}

		[CompilerGenerated]
		public static bool operator !=(RecoveredType_3 arg0, RecoveredType_3 arg1)
		{
			return !(arg0 == arg1);
		}

		[CompilerGenerated]
		public static bool operator ==(RecoveredType_3 arg0, RecoveredType_3 arg1)
		{
			return arg0.Equals(arg1);
		}

		[CompilerGenerated]
		public override int GetHashCode()
		{
			return EqualityComparer<uint>.Default.GetHashCode(field_97) * -1521134295 + EqualityComparer<Vector3>.Default.GetHashCode(field_98);
		}

		[CompilerGenerated]
		public override bool Equals(object arg0)
		{
			if (arg0 is RecoveredType_3)
			{
				return Equals((RecoveredType_3)arg0);
			}
			return false;
		}

		[CompilerGenerated]
		public bool Equals(RecoveredType_3 arg0)
		{
			if (EqualityComparer<uint>.Default.Equals(field_97, arg0.field_97))
			{
				return EqualityComparer<Vector3>.Default.Equals(field_98, arg0.field_98);
			}
			return false;
		}

		[CompilerGenerated]
		public void Method_225(out uint arg0, out Vector3 arg1)
		{
			arg0 = TerritoryType;
			arg1 = Position;
		}
	}

	public readonly struct RecoveredType_4 : IEquatable<RecoveredType_4>
	{
		[CompilerGenerated]
		private readonly string field_99;

		[CompilerGenerated]
		private readonly Vector3 field_100;

		public string Name
		{
			[CompilerGenerated]
			get
			{
				return field_99;
			}
			[CompilerGenerated]
			init
			{
				field_99 = value;
			}
		}

		public Vector3 Position
		{
			[CompilerGenerated]
			get
			{
				return field_100;
			}
			[CompilerGenerated]
			init
			{
				field_100 = value;
			}
		}

		public RecoveredType_4(string arg0, Vector3 arg1)
		{
			field_99 = arg0;
			field_100 = arg1;
		}

		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("MobSnapshot");
			stringBuilder.Append(" { ");
			if (Method_232(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		[CompilerGenerated]
		private bool Method_232(StringBuilder arg0)
		{
			arg0.Append("Name = ");
			arg0.Append((object?)Name);
			arg0.Append(", Position = ");
			arg0.Append(Position.ToString());
			return true;
		}

		[CompilerGenerated]
		public static bool operator !=(RecoveredType_4 arg0, RecoveredType_4 arg1)
		{
			return !(arg0 == arg1);
		}

		[CompilerGenerated]
		public static bool operator ==(RecoveredType_4 arg0, RecoveredType_4 arg1)
		{
			return arg0.Equals(arg1);
		}

		[CompilerGenerated]
		public override int GetHashCode()
		{
			return EqualityComparer<string>.Default.GetHashCode(field_99) * -1521134295 + EqualityComparer<Vector3>.Default.GetHashCode(field_100);
		}

		[CompilerGenerated]
		public override bool Equals(object arg0)
		{
			if (arg0 is RecoveredType_4)
			{
				return Equals((RecoveredType_4)arg0);
			}
			return false;
		}

		[CompilerGenerated]
		public bool Equals(RecoveredType_4 arg0)
		{
			if (EqualityComparer<string>.Default.Equals(field_99, arg0.field_99))
			{
				return EqualityComparer<Vector3>.Default.Equals(field_100, arg0.field_100);
			}
			return false;
		}

		[CompilerGenerated]
		public void Method_238(out string arg0, out Vector3 arg1)
		{
			arg0 = Name;
			arg1 = Position;
		}
	}

	public readonly struct RecoveredType_5 : IEquatable<RecoveredType_5>
	{
		[CompilerGenerated]
		private readonly string field_101;

		[CompilerGenerated]
		private readonly Vector3 field_102;

		[CompilerGenerated]
		private readonly float field_103;

		public string Name
		{
			[CompilerGenerated]
			get
			{
				return field_101;
			}
			[CompilerGenerated]
			init
			{
				field_101 = value;
			}
		}

		public Vector3 Position
		{
			[CompilerGenerated]
			get
			{
				return field_102;
			}
			[CompilerGenerated]
			init
			{
				field_102 = value;
			}
		}

		public float HpPercent
		{
			[CompilerGenerated]
			get
			{
				return field_103;
			}
			[CompilerGenerated]
			init
			{
				field_103 = value;
			}
		}

		public RecoveredType_5(string arg0, Vector3 arg1, float arg2)
		{
			field_101 = arg0;
			field_102 = arg1;
			field_103 = arg2;
		}

		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SMobSnapshot");
			stringBuilder.Append(" { ");
			if (Method_247(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		[CompilerGenerated]
		private bool Method_247(StringBuilder arg0)
		{
			arg0.Append("Name = ");
			arg0.Append((object?)Name);
			arg0.Append(", Position = ");
			arg0.Append(Position.ToString());
			arg0.Append(", HpPercent = ");
			arg0.Append(HpPercent.ToString());
			return true;
		}

		[CompilerGenerated]
		public static bool operator !=(RecoveredType_5 arg0, RecoveredType_5 arg1)
		{
			return !(arg0 == arg1);
		}

		[CompilerGenerated]
		public static bool operator ==(RecoveredType_5 arg0, RecoveredType_5 arg1)
		{
			return arg0.Equals(arg1);
		}

		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (EqualityComparer<string>.Default.GetHashCode(field_101) * -1521134295 + EqualityComparer<Vector3>.Default.GetHashCode(field_102)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(field_103);
		}

		[CompilerGenerated]
		public override bool Equals(object arg0)
		{
			if (arg0 is RecoveredType_5)
			{
				return Equals((RecoveredType_5)arg0);
			}
			return false;
		}

		[CompilerGenerated]
		public bool Equals(RecoveredType_5 arg0)
		{
			if (EqualityComparer<string>.Default.Equals(field_101, arg0.field_101) && EqualityComparer<Vector3>.Default.Equals(field_102, arg0.field_102))
			{
				return EqualityComparer<float>.Default.Equals(field_103, arg0.field_103);
			}
			return false;
		}

		[CompilerGenerated]
		public void Method_253(out string arg0, out Vector3 arg1, out float arg2)
		{
			arg0 = Name;
			arg1 = Position;
			arg2 = HpPercent;
		}
	}

	public sealed class RecoveredType_6
	{
		[CompilerGenerated]
		private string field_104 = string.Empty;

		[CompilerGenerated]
		private Vector3 field_105;

		[CompilerGenerated]
		private bool field_106;

		public string Name
		{
			[CompilerGenerated]
			get
			{
				return field_104;
			}
			[CompilerGenerated]
			set
			{
				field_104 = value;
			}
		}

		public Vector3 Position
		{
			[CompilerGenerated]
			get
			{
				return field_105;
			}
			[CompilerGenerated]
			set
			{
				field_105 = value;
			}
		}

		public bool HasBeenDamaged
		{
			[CompilerGenerated]
			get
			{
				return field_106;
			}
			[CompilerGenerated]
			set
			{
				field_106 = value;
			}
		}
	}

	public sealed class SMonitorStatus
	{
		public string MobName = "";

		public string Phase = "";

		public float HpPercent;

		public float Threshold;

		public float WaitedSeconds;

		public float LostSeconds;

		public bool TargetLost;

		public bool Finished;
	}

	public enum RecoveredType_8
	{
		field_116,
		field_117,
		field_118,
		field_119,
		field_120,
		field_121
	}

	public enum RecoveredType_9
	{
		field_123,
		field_124,
		field_125,
		field_126
	}

	public enum RecoveredType_10
	{
		field_128,
		field_129,
		field_130
	}

	[CompilerGenerated]
	public static class RecoveredType_11
	{
		public static Func<Vector3?> field_131;

		public static Func<HuntChainStep, HuntChainStep> field_132;

		public static Func<bool> field_133;
	}

	[Serializable]
	[CompilerGenerated]
	public sealed class RecoveredType_12
	{
		public static readonly RecoveredType_12 field_134 = new RecoveredType_12();

		public static Func<Task?> field_135;

		public static Func<bool> field_136;

		public static Func<bool> field_137;

		public static Func<IExposedPlugin, bool> field_138;

		public static Comparison<MountInfo> field_139;

		public static Func<MountInfo, string> field_140;

		public static Func<string, List<string>> field_141;

		public static Func<(bool, bool)> field_142;

		public static Func<bool> field_143;

		public static Func<uint> field_144;

		public static Func<string> field_145;

		public static Func<((uint Id, Vector3 Position, string Name) Aetheryte, float Corrected), float> field_146;

		public static Func<bool> field_147;

		public static Func<bool> field_148;

		public static Func<bool> field_149;

		public static Func<bool> field_150;

		public static Func<bool> field_151;

		public static Func<bool> field_152;

		internal async Task? Method_264()
		{
			try
			{
				await Task.Delay(5000);
				List<string> list = new List<string>();
				if (!IsPluginLoaded("Lifestream"))
				{
					list.Add("Lifestream");
				}
				if (!IsPluginLoaded("vnavmesh"))
				{
					list.Add("vnavmesh");
				}
				if (list.Count == 0)
				{
					PrintEchoAlways("前置插件已就绪");
				}
				else
				{
					PrintEchoAlways("前置插件仍未加载: " + string.Join(", ", list) + "（相关功能不可用，见设置-前置插件）");
				}
			}
			catch (Exception exception)
			{
				Log.Warning(exception, "AutomaticHunting: 前置插件复查失败");
			}
		}

		internal bool Method_265()
		{
			if (!Condition[ConditionFlag.BetweenAreas])
			{
				return Condition[ConditionFlag.BetweenAreas51];
			}
			return true;
		}

		internal bool Method_266()
		{
			return Method_133();
		}

		internal bool Method_267(IExposedPlugin arg0)
		{
			if (arg0.IsLoaded)
			{
				if (!arg0.InternalName.Equals("BossModReborn", StringComparison.OrdinalIgnoreCase))
				{
					return arg0.InternalName.Equals("BossMod", StringComparison.OrdinalIgnoreCase);
				}
				return true;
			}
			return false;
		}

		internal int Method_268(MountInfo arg0, MountInfo arg1)
		{
			return string.Compare(arg0.Name, arg1.Name, StringComparison.OrdinalIgnoreCase);
		}

		internal string Method_269(MountInfo arg0)
		{
			return arg0.Name;
		}

		internal List<string> Method_270(string arg0)
		{
			List<string> list = new List<string>();
			try
			{
				ExcelSheet<PlaceName> excelSheet = DataManager.GetExcelSheet<PlaceName>();
				if (excelSheet != null)
				{
					foreach (PlaceName item in excelSheet)
					{
						string text = item.Name.ExtractText();
						if (text.Length > arg0.Length && text.Contains(arg0))
						{
							list.Add(text);
						}
					}
				}
			}
			catch (Exception exception)
			{
				Log.Warning(exception, "AutomaticHunting: 构建地名剔除表失败 [" + arg0 + "]");
			}
			return list;
		}

		internal (bool, bool) Method_271()
		{
			return (Condition[ConditionFlag.ReadyingVisitOtherWorld], Condition[ConditionFlag.BetweenAreas] || Condition[ConditionFlag.BetweenAreas51]);
		}

		internal bool Method_272()
		{
			return Method_133();
		}

		internal uint Method_273()
		{
			try
			{
				IPlayerCharacter localPlayer = ObjectTable.LocalPlayer;
				if (localPlayer != null)
				{
					return localPlayer.CurrentWorld.RowId;
				}
			}
			catch (Exception exception)
			{
				Log.Warning(exception, "AutomaticHunting: 读取当前服务器失败");
			}
			return 0u;
		}

		internal string Method_274()
		{
			try
			{
				IPlayerCharacter localPlayer = ObjectTable.LocalPlayer;
				if (localPlayer != null)
				{
					World? world = DataManager.GetExcelSheet<World>()?.GetRow(localPlayer.CurrentWorld.RowId);
					if (world.HasValue)
					{
						return world.Value.Name.ExtractText();
					}
				}
			}
			catch (Exception exception)
			{
				Log.Warning(exception, "AutomaticHunting: 读取当前服务器名失败");
			}
			return string.Empty;
		}

		internal float Method_275(((uint Id, Vector3 Position, string Name) Aetheryte, float Corrected) arg0)
		{
			return arg0.Corrected;
		}

		internal bool Method_276()
		{
			if (!Condition[ConditionFlag.BetweenAreas])
			{
				return Condition[ConditionFlag.BetweenAreas51];
			}
			return true;
		}

		internal bool Method_277()
		{
			return Condition[ConditionFlag.Mounted];
		}

		internal bool Method_278()
		{
			return Condition[ConditionFlag.Mounted];
		}

		internal bool Method_279()
		{
			return Condition[ConditionFlag.Mounted];
		}

		internal bool Method_280()
		{
			return Method_133();
		}

		internal bool Method_281()
		{
			return Condition[ConditionFlag.Mounted];
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_14
	{
		public uint field_156;

		internal bool Method_285(MultiLineMapConfig arg0)
		{
			if (!string.IsNullOrWhiteSpace(arg0.MapId) && uint.TryParse(arg0.MapId.Trim(), out var result))
			{
				return result == field_156;
			}
			return false;
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_15
	{
		public uint field_157;

		internal bool Method_287(MultiLineMapConfig arg0)
		{
			if (!string.IsNullOrWhiteSpace(arg0.MapId) && uint.TryParse(arg0.MapId.Trim(), out var result))
			{
				return result == field_157;
			}
			return false;
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_16
	{
		public string field_158;

		public AutomaticHunting field_159;

		internal void Method_289()
		{
			try
			{
				GlobalCommands.Process(field_158);
				Log.Information("AutomaticHunting: 执行切线命令 " + field_158);
			}
			catch (Exception exception)
			{
				field_159.PrintEcho("切线命令执行失败: " + field_158);
				Log.Warning(exception, "AutomaticHunting: 切线命令失败 " + field_158);
			}
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_17
	{
		public int field_160;

		internal void Method_291()
		{
			try
			{
				GlobalCommands.Process($"/ahglobal instance {field_160}");
			}
			catch (Exception exception)
			{
				Log.Warning(exception, $"AutomaticHunting: 切线重发失败 {field_160}");
			}
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_18
	{
		public uint field_161;

		public Vector3? field_162;

		internal List<(uint Id, Vector3 Position, string Name)> Method_293()
		{
			return Method_127(field_161);
		}

		internal float Method_294((uint Id, Vector3 Position, string Name) arg0)
		{
			return Vector3.DistanceSquared(arg0.Position, field_162.Value);
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_19
	{
		public AutomaticHunting field_163;

		public SMonitorStatus field_164;

		internal async Task? Method_296()
		{
			await Task.Delay(2000);
			if (field_163.SMonitor == field_164)
			{
				field_163.SMonitor = null;
			}
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_21
	{
		public string field_169;

		internal bool Method_300(string arg0)
		{
			return GlobalCommandPolicy.IsMark(field_169, arg0);
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_22
	{
		public AutomaticHunting field_170;

		public string field_171;

		public uint field_172;

		public string field_173;

		internal Task? Method_302()
		{
			return field_170.Method_103(field_171, field_172, field_173, field_170.field_64.Token);
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_23
	{
		public uint field_174;

		internal List<(uint Id, Vector3 Position, string Name)> Method_304()
		{
			return Method_127(field_174);
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_24
	{
		public uint field_175;

		internal List<(uint Id, Vector3 Position, string Name)> Method_306()
		{
			return Method_127(field_175);
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_25
	{
		public string field_176;

		internal void Method_308()
		{
			try
			{
				GlobalCommands.Process(field_176);
			}
			catch (Exception exception)
			{
				Log.Warning(exception, "AutomaticHunting: 执行跨服命令失败 " + field_176);
			}
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_26
	{
		public AutomaticHunting field_177;

		public CancellationToken field_178;

		public int field_179;

		public DateTime? field_180;

		public DateTime? field_181;

		public Vector3? field_182;

		public DateTime field_183;

		public int field_184;

		public uint field_185;

		public Func<(bool Between, bool Foreign)> field_186;

		internal void Method_310()
		{
			field_177.Method_179("/vnav stop");
		}

		internal (bool Between, bool Foreign) Method_311()
		{
			field_178.ThrowIfCancellationRequested();
			if (Condition[ConditionFlag.BetweenAreas] || Condition[ConditionFlag.BetweenAreas51])
			{
				return (Between: field_179 > 0, Foreign: field_179 == 0);
			}
			if (Method_121())
			{
				DateTime valueOrDefault = field_180.GetValueOrDefault();
				if (!field_180.HasValue)
				{
					valueOrDefault = DateTime.UtcNow;
					field_180 = valueOrDefault;
				}
				field_181 = null;
				return (Between: false, Foreign: false);
			}
			if (field_180.HasValue)
			{
				DateTime valueOrDefault = field_181.GetValueOrDefault();
				if (!field_181.HasValue)
				{
					valueOrDefault = DateTime.UtcNow;
					field_181 = valueOrDefault;
				}
				if (DateTime.UtcNow - field_181.Value < TimeSpan.FromSeconds(2L))
				{
					return (Between: false, Foreign: false);
				}
			}
			Vector3? localPlayerPosition = GetLocalPlayerPosition();
			bool num = localPlayerPosition.HasValue && field_182.HasValue && Vector3.DistanceSquared(localPlayerPosition.Value, field_182.Value) < 0.0001f;
			field_182 = localPlayerPosition;
			if (!num || Method_133() || Method_121() || Condition[ConditionFlag.MountOrOrnamentTransition] || DateTime.UtcNow < field_183 || field_177.Method_122() || !Method_120())
			{
				return (Between: false, Foreign: false);
			}
			field_184++;
			bool flag = field_177.Method_119(field_185);
			if (flag)
			{
				field_179++;
			}
			field_183 = DateTime.UtcNow.AddSeconds((!flag) ? 1 : 2);
			return (Between: false, Foreign: false);
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_27
	{
		public uint field_187;

		internal bool Method_313(Lumina.Excel.Sheets.Map arg0)
		{
			return arg0.TerritoryType.RowId == field_187;
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_28
	{
		public Aetheryte field_188;

		internal bool Method_315(Lumina.Excel.Sheets.Map arg0)
		{
			return arg0.TerritoryType.RowId == field_188.Territory.RowId;
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_29
	{
		public string field_189;

		internal bool Method_317(IExposedPlugin arg0)
		{
			if (arg0.InternalName == field_189)
			{
				return arg0.IsLoaded;
			}
			return false;
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_30
	{
		public uint field_190;

		internal bool Method_319(Lumina.Excel.Sheets.Map arg0)
		{
			return arg0.TerritoryType.RowId == field_190;
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_31
	{
		public WorldTravelPreset field_191;

		internal bool Method_321(WorldTravelData.MapEntry arg0)
		{
			return arg0.RowId == field_191.MapRowId;
		}

		internal bool Method_322((uint Id, Vector3 Position, string Name) arg0)
		{
			return arg0.Id == field_191.AetheryteId;
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_32
	{
		public Task field_192;

		public AutomaticHunting field_193;

		public RecoveredType_3 field_194;

		public CancellationTokenSource field_195;

		internal async Task? Method_324()
		{
			if (field_192 != null)
			{
				try
				{
					await field_192;
				}
				catch
				{
				}
			}
			await field_193.Method_158(field_194, field_195);
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_34
	{
		public AutomaticHunting field_200;

		public RecoveredType_3 field_201;

		internal (RecoveredType_10 Preparation, uint AetheryteId, bool SameTerritory) Method_328()
		{
			uint arg;
			return (Preparation: field_200.Method_160(field_201, out arg), AetheryteId: arg, SameTerritory: Method_116() == field_201.TerritoryType);
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_35
	{
		public AutomaticHunting field_202;

		public RecoveredType_3 field_203;

		internal ((uint Id, Vector3 Position, string Name) Aetheryte, float Corrected) Method_330((uint Id, Vector3 Position, string Name) arg0)
		{
			return (Aetheryte: arg0, Corrected: field_202.Method_161(arg0.Id, arg0.Position, field_203.Position));
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_36
	{
		public uint field_204;

		internal bool Method_332((uint Id, Vector3 Position, string Name) arg0)
		{
			return arg0.Id == field_204;
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_37
	{
		public uint field_205;

		public Func<bool> field_206;

		public Func<bool> field_207;

		internal bool Method_334()
		{
			if (Method_116() == field_205)
			{
				return GetLocalPlayerPosition().HasValue;
			}
			return false;
		}

		internal bool Method_335()
		{
			if (!Method_133() && !Condition[ConditionFlag.Casting] && Method_116() == field_205)
			{
				return GetLocalPlayerPosition().HasValue;
			}
			return false;
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_38
	{
		public AutomaticHunting field_208;

		public Vector3 field_209;

		public RecoveredType_3 field_210;

		public Func<bool> field_211;

		public Func<bool> field_212;

		internal bool Method_337()
		{
			Vector3 valueOrDefault = field_208.Method_118(field_209).GetValueOrDefault(field_209);
			ICallGateSubscriber<bool>? field_ = field_208.field_24;
			if (field_ != null && field_.InvokeFunc())
			{
				return field_208.field_25?.InvokeFunc(valueOrDefault, arg2: true) ?? false;
			}
			return false;
		}

		internal bool Method_338()
		{
			if (field_208.Config.SetFlagOnNavigation)
			{
				return Method_126(field_210.TerritoryType, 0u, field_210.Position);
			}
			return true;
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_39
	{
		public Vector3 field_213;

		public AutomaticHunting field_214;

		public Func<bool> field_215;

		internal bool Method_340()
		{
			Vector3? vector = field_214.Method_118(field_213);
			Vector3 arg = (vector.HasValue ? new Vector3(vector.Value.X, field_213.Y, vector.Value.Z) : field_213);
			ICallGateSubscriber<bool>? field_ = field_214.field_24;
			if (field_ != null && field_.InvokeFunc())
			{
				return field_214.field_25?.InvokeFunc(arg, arg2: true) ?? false;
			}
			return false;
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_40
	{
		public RecoveredType_5? field_216;

		public RecoveredType_39 field_217;

		internal string Method_342()
		{
			return field_217.field_214.Method_204(field_216.Value);
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_41
	{
		public Vector3 field_218;

		public RecoveredType_40 field_219;

		internal bool Method_344()
		{
			Vector3? vector = field_219.field_217.field_214.Method_118(field_218);
			Vector3 arg = (vector.HasValue ? new Vector3(vector.Value.X, field_218.Y, vector.Value.Z) : field_218);
			ICallGateSubscriber<bool>? field_ = field_219.field_217.field_214.field_24;
			if (field_ != null && field_.InvokeFunc())
			{
				return field_219.field_217.field_214.field_25?.InvokeFunc(arg, arg2: true) ?? false;
			}
			return false;
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_42
	{
		public AutomaticHunting field_220;

		public Vector3 field_221;

		public Func<(Vector3? player, Vector3? mob)> field_222;

		internal (Vector3? player, Vector3? mob) Method_346()
		{
			Vector3? localPlayerPosition = GetLocalPlayerPosition();
			Vector3? item = (field_220.Method_171(field_221, arg1: false, out var arg) ? new Vector3?(arg) : ((Vector3?)null));
			return (player: localPlayerPosition, mob: item);
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_43
	{
		public AutomaticHunting field_223;

		public Vector3 field_224;

		public bool field_225;

		internal Vector3? Method_348()
		{
			if (!field_223.Method_171(field_224, field_225, out var arg))
			{
				return null;
			}
			Vector3 vector = arg + field_223.Method_114(arg);
			Vector3 arg2 = field_223.Method_118(vector) ?? vector;
			ICallGateSubscriber<bool>? field_ = field_223.field_24;
			if (field_ != null && field_.InvokeFunc())
			{
				ICallGateSubscriber<Vector3, bool, bool>? field_2 = field_223.field_25;
				if (field_2 != null && field_2.InvokeFunc(arg2, arg2: true))
				{
					return vector;
				}
			}
			return null;
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_44
	{
		public RecoveredType_4? field_226;

		internal bool Method_350(IGameObject arg0)
		{
			return arg0.Name.TextValue == field_226.Value.Name;
		}

		internal float Method_351(IGameObject arg0)
		{
			return Method_197(arg0.Position, field_226.Value.Position);
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_45
	{
		public string field_227;

		public string field_228;

		internal void Method_353()
		{
			try
			{
				if (field_227 == null || !Method_180(field_227))
				{
					string content = ((!field_228.StartsWith('/')) ? ("/" + field_228) : field_228);
					GlobalCommands.Process(content);
				}
			}
			catch (Exception exception)
			{
				Log.Warning(exception, "AutomaticHunting: 执行突进技能失败 " + field_228);
			}
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_46
	{
		public string field_229;

		internal void Method_355()
		{
			try
			{
				GlobalCommands.Process(field_229);
			}
			catch (Exception exception)
			{
				Log.Warning(exception, "AutomaticHunting: 执行命令失败 " + field_229);
			}
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_47
	{
		public string field_230;

		internal bool Method_357(Lumina.Excel.Sheets.Action arg0)
		{
			return arg0.Name.ExtractText() == field_230;
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_48
	{
		public Task field_231;

		public AutomaticHunting field_232;

		public uint field_233;

		public Vector3 field_234;

		public CancellationTokenSource field_235;

		internal async Task? Method_359()
		{
			if (field_231 != null)
			{
				try
				{
					await field_231;
				}
				catch
				{
				}
			}
			await field_232.Method_192(field_233, field_234, field_235);
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_50
	{
		public AutomaticHunting field_240;

		public Vector3 field_241;

		public Func<bool> field_242;

		internal bool Method_363()
		{
			Vector3 valueOrDefault = field_240.Method_118(field_241).GetValueOrDefault(field_241);
			ICallGateSubscriber<bool>? field_ = field_240.field_24;
			if (field_ != null && field_.InvokeFunc())
			{
				return field_240.field_25?.InvokeFunc(valueOrDefault, arg2: true) ?? false;
			}
			return false;
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_51
	{
		public string field_243;

		internal bool Method_365(string arg0)
		{
			return GlobalCommandPolicy.IsMark(field_243, arg0);
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_52
	{
		public string field_244;

		internal bool Method_367(string arg0)
		{
			return field_244.Contains(arg0);
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_53
	{
		public RecoveredType_5 field_245;

		internal bool Method_369(IGameObject arg0)
		{
			return arg0.Name.TextValue == field_245.Name;
		}

		internal float Method_370(IGameObject arg0)
		{
			return Method_197(arg0.Position, field_245.Position);
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_54
	{
		public HashSet<string> field_246;

		internal bool Method_372(KeyValuePair<string, RecoveredType_6> arg0)
		{
			return !field_246.Contains(arg0.Key);
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_55
	{
		public string field_247;

		internal bool Method_374(string arg0)
		{
			return GlobalCommandPolicy.IsMark(field_247, arg0);
		}
	}

	public static readonly Vector4 UiColorRun = new Vector4(0.42f, 0.88f, 0.58f, 1f);

	public static readonly Vector4 UiColorNav = new Vector4(0.4f, 0.65f, 1f, 1f);

	public static readonly Vector4 UiColorWait = new Vector4(1f, 0.8f, 0.2f, 1f);

	public static readonly Vector4 UiColorError = new Vector4(0.95f, 0.35f, 0.35f, 1f);

	public static readonly Vector4 UiColorDisabled = new Vector4(0.65f, 0.67f, 0.7f, 1f);

	public static readonly Vector4 UiColorGold = new Vector4(0.86f, 0.71f, 0.31f, 1f);

	[CompilerGenerated]
	private static IPluginLog field_7 = null;

	[CompilerGenerated]
	private static ICommandManager field_8 = null;

	[CompilerGenerated]
	private static IChatGui field_9 = null;

	[CompilerGenerated]
	private static IClientState field_10 = null;

	[CompilerGenerated]
	private static IObjectTable field_11 = null;

	[CompilerGenerated]
	private static IDataManager field_12 = null;

	[CompilerGenerated]
	private static IDalamudPluginInterface field_13 = null;

	[CompilerGenerated]
	private static ITargetManager field_14 = null;

	[CompilerGenerated]
	private static ICondition field_15 = null;

	[CompilerGenerated]
	private static IFramework field_16 = null;

	[CompilerGenerated]
	private static IContextMenu field_17 = null;

	public PluginConfiguration Config;

	[CompilerGenerated]
	private ConfigService field_19;

	[CompilerGenerated]
	private HudWindow field_20;

	[CompilerGenerated]
	private SettingsWindow field_21;

	[CompilerGenerated]
	private OnboardingWindow field_22;

	[CompilerGenerated]
	private HelpWindow field_23;

	private ICallGateSubscriber<bool>? field_24;

	private ICallGateSubscriber<Vector3, bool, bool>? field_25;

	private ICallGateSubscriber<Vector3, float, float, Vector3?>? field_26;

	private string field_27 = string.Empty;

	private uint field_28;

	private DateTime field_29 = DateTime.MinValue;

	public Vector3? LastFlagPosition;

	private List<(uint Id, Vector3 Position, string Name)> field_31 = new List<(uint, Vector3, string)>();

	private RecoveredType_3? field_32;

	private volatile bool field_33;

	private volatile bool field_34;

	private bool field_35;

	private readonly Dictionary<string, RecoveredType_6> field_36 = new Dictionary<string, RecoveredType_6>();

	private readonly HashSet<string> field_37 = new HashSet<string>();

	private int field_38;

	private string? field_39;

	private int field_40;

	private string? field_41;

	private object field_42 = new object();

	private RecoveredType_3? field_43;

	private string? field_44;

	private DateTime field_45 = DateTime.MinValue;

	private DateTime field_46 = DateTime.MinValue;

	private WindowSystem field_47;

	private HuntUIController field_48;

	public List<MountInfo> OwnedMounts = new List<MountInfo>();

	public string[] MountNames = Array.Empty<string>();

	public int SelectedMountIndex;

	public string MountSearchText = string.Empty;

	private readonly object field_53 = new object();

	private CancellationTokenSource? field_54;

	private Task? field_55;

	private RecoveredType_8 field_56;

	private const float field_57 = 50f;

	private bool field_58;

	public volatile SMonitorStatus? SMonitor;

	private DateTime field_60 = DateTime.MinValue;

	private static readonly Regex field_61 = new Regex("[(（]?\\s*(\\d{1,2})[.．](\\d)\\s*[,，、]\\s*(\\d{1,2})[.．](\\d)\\s*[)）]?", RegexOptions.Compiled);

	private object field_62 = new object();

	private Task? field_63;

	private CancellationTokenSource? field_64;

	private bool field_65;

	private DateTime field_66 = DateTime.MinValue;

	private uint field_67;

	private Vector3 field_68;

	private string? field_69;

	private uint field_70;

	private uint field_71;

	private Vector3 field_72;

	private (string Server, uint Territory, uint AetheryteId, Vector3 Pos)? field_73;

	private string? field_74;

	private DateTime field_75 = DateTime.MinValue;

	private static readonly ConcurrentDictionary<string, List<string>> field_76 = new ConcurrentDictionary<string, List<string>>();

	private readonly SemaphoreSlim field_77 = new SemaphoreSlim(1, 1);

	private RecoveredType_9 field_78;

	private List<HuntChainStep> field_79 = new List<HuntChainStep>();

	private int field_80 = -1;

	private int field_81;

	private bool field_82;

	private DateTime field_83 = DateTime.MinValue;

	private DateTime field_84 = DateTime.MinValue;

	private string field_85 = string.Empty;

	private const float field_86 = 100f;

	private const float field_87 = 3f;

	private const float field_88 = 50f;

	private const float field_89 = 4f;

	private const float field_90 = 1.5f;

	private const float field_91 = 5f;

	private const float field_92 = 3f;

	private const float field_93 = 10f;

	private const float field_94 = 5f;

	private static readonly Dictionary<string, uint> field_95 = new Dictionary<string, uint>(StringComparer.OrdinalIgnoreCase);

	private static readonly object field_96 = new object();

	public string Name => "自动狩猎跟车";

	[PluginService]
	internal static IPluginLog Log
	{
		[CompilerGenerated]
		get
		{
			return field_7;
		}
		[CompilerGenerated]
		private set
		{
			field_7 = value;
		}
	}

	[PluginService]
	internal static ICommandManager CommandManager
	{
		[CompilerGenerated]
		get
		{
			return field_8;
		}
		[CompilerGenerated]
		private set
		{
			field_8 = value;
		}
	}

	[PluginService]
	internal static IChatGui ChatGui
	{
		[CompilerGenerated]
		get
		{
			return field_9;
		}
		[CompilerGenerated]
		private set
		{
			field_9 = value;
		}
	}

	[PluginService]
	internal static IClientState ClientState
	{
		[CompilerGenerated]
		get
		{
			return field_10;
		}
		[CompilerGenerated]
		private set
		{
			field_10 = value;
		}
	}

	[PluginService]
	internal static IObjectTable ObjectTable
	{
		[CompilerGenerated]
		get
		{
			return field_11;
		}
		[CompilerGenerated]
		private set
		{
			field_11 = value;
		}
	}

	[PluginService]
	internal static IDataManager DataManager
	{
		[CompilerGenerated]
		get
		{
			return field_12;
		}
		[CompilerGenerated]
		private set
		{
			field_12 = value;
		}
	}

	[PluginService]
	internal static IDalamudPluginInterface PluginInterface
	{
		[CompilerGenerated]
		get
		{
			return field_13;
		}
		[CompilerGenerated]
		private set
		{
			field_13 = value;
		}
	}

	[PluginService]
	internal static ITargetManager TargetManager
	{
		[CompilerGenerated]
		get
		{
			return field_14;
		}
		[CompilerGenerated]
		private set
		{
			field_14 = value;
		}
	}

	[PluginService]
	internal static ICondition Condition
	{
		[CompilerGenerated]
		get
		{
			return field_15;
		}
		[CompilerGenerated]
		private set
		{
			field_15 = value;
		}
	}

	[PluginService]
	internal static IFramework Framework
	{
		[CompilerGenerated]
		get
		{
			return field_16;
		}
		[CompilerGenerated]
		private set
		{
			field_16 = value;
		}
	}

	[PluginService]
	internal static IContextMenu ContextMenu
	{
		[CompilerGenerated]
		get
		{
			return field_17;
		}
		[CompilerGenerated]
		private set
		{
			field_17 = value;
		}
	}

	public ConfigService ConfigService
	{
		[CompilerGenerated]
		get
		{
			return field_19;
		}
		[CompilerGenerated]
		private set
		{
			field_19 = value;
		}
	}

	public HudWindow HudWindow
	{
		[CompilerGenerated]
		get
		{
			return field_20;
		}
		[CompilerGenerated]
		private set
		{
			field_20 = value;
		}
	}

	public SettingsWindow SettingsWindow
	{
		[CompilerGenerated]
		get
		{
			return field_21;
		}
		[CompilerGenerated]
		private set
		{
			field_21 = value;
		}
	}

	public OnboardingWindow OnboardingWindow
	{
		[CompilerGenerated]
		get
		{
			return field_22;
		}
		[CompilerGenerated]
		private set
		{
			field_22 = value;
		}
	}

	public HelpWindow HelpWindow
	{
		[CompilerGenerated]
		get
		{
			return field_23;
		}
		[CompilerGenerated]
		private set
		{
			field_23 = value;
		}
	}

	public bool IsNavigating
	{
		get
		{
			lock (field_53)
			{
				Task task = field_55;
				return task != null && !task.IsCompleted;
			}
		}
	}

	public bool CrossWorldInProgress
	{
		get
		{
			lock (field_62)
			{
				if (field_65 && field_66 != DateTime.MinValue && DateTime.UtcNow - field_66 > TimeSpan.FromSeconds(900L))
				{
					field_65 = false;
					field_66 = DateTime.MinValue;
					field_67 = 0u;
					field_68 = default;
					field_69 = null;
				}
				return field_65;
			}
		}
	}

	public bool IsHuntChainRunning
	{
		get
		{
			RecoveredType_9 recoveredType_ = field_78;
			if ((uint)(recoveredType_ - 1) <= 1u)
			{
				return true;
			}
			return false;
		}
	}

	public bool CanAdjustHuntChainKills
	{
		get
		{
			if (field_78 == RecoveredType_9.field_125 && Config.AutomationEnabled && Config.HuntChain.Enabled && field_80 >= 0)
			{
				return field_80 < field_79.Count;
			}
			return false;
		}
	}

	public bool CanDecreaseHuntChainKills
	{
		get
		{
			if (CanAdjustHuntChainKills)
			{
				return field_81 > 0;
			}
			return false;
		}
	}

	public void ScanAndSelectTarget()
	{
		string currentTerritoryName = GetCurrentTerritoryName();
		if (string.IsNullOrWhiteSpace(currentTerritoryName))
		{
			PrintEcho("扫描A怪: 无法获取当前区域名称");
			return;
		}
		if (!Config.ARealmMarks.TryGetValue(currentTerritoryName, out string[] value))
		{
			PrintEcho("扫描A怪: 当前区域 [" + currentTerritoryName + "] 不在A怪列表中");
			return;
		}
		PrintEcho($"扫描A怪: 区域={currentTerritoryName}, 目标={string.Join("/", value)}, 范围={100f:0}m, 附近对象={ObjectTable.Count()}");
		string text = Method_3();
		PrintEcho(string.IsNullOrEmpty(text) ? $"扫描A怪: {100f:0}m内未找到匹配的目标" : ("已选中A怪: " + text));
	}

	private RecoveredType_4? Method_2()
	{
		Vector3? localPlayerPosition = GetLocalPlayerPosition();
		if (!localPlayerPosition.HasValue)
		{
			return null;
		}
		string currentTerritoryName = GetCurrentTerritoryName();
		if (string.IsNullOrWhiteSpace(currentTerritoryName) || !Config.ARealmMarks.TryGetValue(currentTerritoryName, out string[] value))
		{
			return null;
		}
		RecoveredType_4? result = null;
		float num = 100f;
		foreach (IGameObject item in ObjectTable)
		{
			float num2 = Method_197(localPlayerPosition.Value, item.Position);
			if (!(num2 > num))
			{
				string field_169 = item.Name.TextValue;
				if (value.Any((string arg0) => GlobalCommandPolicy.IsMark(field_169, arg0)))
				{
					num = num2;
					result = new RecoveredType_4(field_169, item.Position);
				}
			}
		}
		return result;
	}

	private string? Method_3()
	{
		RecoveredType_4? field_226 = Method_2();
		if (!field_226.HasValue)
		{
			return null;
		}
		IGameObject gameObject = (from arg0 in ObjectTable
			where arg0.Name.TextValue == field_226.Value.Name
			orderby Method_197(arg0.Position, field_226.Value.Position)
			select arg0).FirstOrDefault();
		if (gameObject == null)
		{
			return null;
		}
		TargetManager.Target = gameObject;
		return field_226.Value.Name;
	}

	private void Method_4()
	{
		field_36.Clear();
		field_37.Clear();
		field_38 = 0;
	}

	private void Method_5()
	{
		string currentTerritoryName = GetCurrentTerritoryName();
		if (!string.IsNullOrWhiteSpace(currentTerritoryName) && currentTerritoryName != field_39)
		{
			if (field_39 != null)
			{
				Method_4();
			}
			field_39 = currentTerritoryName;
		}
		if (string.IsNullOrWhiteSpace(currentTerritoryName))
		{
			return;
		}
		int currentInstanceNumber = GetCurrentInstanceNumber();
		if (currentInstanceNumber > 0)
		{
			if (field_40 > 0 && currentInstanceNumber != field_40)
			{
				Method_4();
				PrintEcho($"检测到分线变化({field_40}→{currentInstanceNumber})，已重置A怪计数");
			}
			field_40 = currentInstanceNumber;
		}
		bool flag = Config.ARealmMarks.TryGetValue(currentTerritoryName, out string[] value);
		if (!flag && !Config.DebugCountAInAllMaps)
		{
			return;
		}
		HashSet<string> field_246 = new HashSet<string>();
		foreach (IGameObject item in ObjectTable)
		{
			string field_247 = item.Name.TextValue;
			if ((flag && value != null) ? value.Any((string arg0) => GlobalCommandPolicy.IsMark(field_247, arg0)) : (item is IBattleChara))
			{
				string text = field_247;
				field_246.Add(text);
				if (!field_36.TryGetValue(text, out RecoveredType_6 value2))
				{
					value2 = new RecoveredType_6
					{
						Name = field_247,
						Position = item.Position
					};
					field_36[text] = value2;
				}
				else
				{
					value2.Position = item.Position;
				}
				if (!value2.HasBeenDamaged && Method_205(item) < 80f)
				{
					value2.HasBeenDamaged = true;
					PrintEcho("A怪已被攻击: " + field_247);
				}
				if (Method_205(item) <= 1f && field_37.Add(field_247))
				{
					field_38++;
					Method_147();
					PrintEcho($"已击杀A怪: {field_247} (本图累计:{field_38})");
				}
			}
		}
		if (field_36.Count <= 0)
		{
			return;
		}
		foreach (KeyValuePair<string, RecoveredType_6> item2 in field_36.Where((KeyValuePair<string, RecoveredType_6> arg0) => !field_246.Contains(arg0.Key)).ToList())
		{
			if (item2.Value.HasBeenDamaged && field_37.Add(item2.Value.Name))
			{
				field_38++;
				Method_147();
				PrintEcho($"已击杀A怪: {item2.Value.Name} (本图累计:{field_38})");
			}
			field_36.Remove(item2.Key);
		}
	}

	public AutomaticHunting()
	{
		Config = (PluginInterface.GetPluginConfig() as PluginConfiguration) ?? new PluginConfiguration();
		GlobalGameData.Migrate(Config);
		ConfigService = new ConfigService(Config, PluginInterface);
		ECommonsMain.Init(PluginInterface, this);
		if (Config.BlacklistedAetheryteIds.Add(175u))
		{
			ConfigService.Save();
		}
		List<string> list = new List<string>();
		if (!IsPluginLoaded("Lifestream"))
		{
			list.Add("Lifestream");
		}
		if (!IsPluginLoaded("vnavmesh"))
		{
			list.Add("vnavmesh");
		}
		if (list.Count > 0)
		{
			Log.Warning("AutomaticHunting: 前置插件尚未就绪: " + string.Join(", ", list) + "（继续初始化，就绪前相关功能降级）");
			PrintEchoAlways("前置插件尚未加载: " + string.Join(", ", list) + "，5秒后复查提示");
			Task.Run(async () =>
			{
				try
				{
					await Task.Delay(5000);
					List<string> list2 = new List<string>();
					if (!IsPluginLoaded("Lifestream"))
					{
						list2.Add("Lifestream");
					}
					if (!IsPluginLoaded("vnavmesh"))
					{
						list2.Add("vnavmesh");
					}
					if (list2.Count == 0)
					{
						PrintEchoAlways("前置插件已就绪");
					}
					else
					{
						PrintEchoAlways("前置插件仍未加载: " + string.Join(", ", list2) + "（相关功能不可用，见设置-前置插件）");
					}
				}
				catch (Exception exception)
				{
					Log.Warning(exception, "AutomaticHunting: 前置插件复查失败");
				}
			});
		}
		CommandManager.AddHandler("/auht", new CommandInfo(Method_48)
		{
			HelpMessage = "自动狩猎跟车 - /auht help"
		});
		Method_44();
		ChatGui.ChatMessage += Method_81;
		Framework.Update += Method_57;
		ClientState.TerritoryChanged += Method_59;
		field_47 = new WindowSystem("AutomaticHunting");
		SettingsWindow = new SettingsWindow(this);
		HudWindow = new HudWindow(this);
		OnboardingWindow = new OnboardingWindow(this);
		HelpWindow = new HelpWindow(this);
		HudWindow.IsOpen = Config.HudVisible;
		field_47.AddWindow(SettingsWindow);
		field_47.AddWindow(HudWindow);
		field_47.AddWindow(OnboardingWindow);
		field_47.AddWindow(HelpWindow);
		PluginInterface.UiBuilder.Draw += field_47.Draw;
		PluginInterface.UiBuilder.OpenConfigUi += Method_42;
		PluginInterface.UiBuilder.OpenMainUi += Method_43;
		field_48 = new HuntUIController(this);
		field_58 = true;
		if (!Config.DisclaimerAccepted)
		{
			if (Config.Enabled)
			{
				Config.Enabled = false;
				ConfigService.Save();
			}
			Log.Warning("AutomaticHunting: 未确认免责声明，进入受限模式（所有自动化入口已关闭）");
		}
		if (Config.DisclaimerAccepted && Config.DisclaimerAcceptedUtc == 0L)
		{
			Config.DisclaimerAcceptedUtc = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
			ConfigService.Save();
			Log.Information("AutomaticHunting: 已补写免责声明同意时间戳（审计字段）");
		}
		if (LeaderRoster.EnsureMigrated(Config))
		{
			ConfigService.Save();
			Log.Information("AutomaticHunting: 已迁移老车头配置到车头名单");
		}
		if (!Config.OnboardingCompleted)
		{
			OnboardingWindow.Open();
			PrintEchoAlways("首次运行：请完成 4 步快速设置（或用 /auht setup 随时打开向导）");
		}
		Log.Information("AutomaticHunting: 插件加载完成");
	}

	public void Dispose()
	{
		if (field_58)
		{
			ChatGui.ChatMessage -= Method_81;
			Framework.Update -= Method_57;
			ClientState.TerritoryChanged -= Method_59;
			PluginInterface.UiBuilder.Draw -= field_47.Draw;
			PluginInterface.UiBuilder.OpenConfigUi -= Method_42;
			PluginInterface.UiBuilder.OpenMainUi -= Method_43;
			CommandManager.RemoveHandler("/auht");
			field_48.Dispose();
			StopNavigation();
			ConfigService.Save();
			Theme.PopAll();
			ECommonsMain.Dispose();
			Log.Information("AutomaticHunting: 插件已卸载");
		}
	}

	private void Method_42()
	{
		SettingsWindow.IsOpen = true;
	}

	private void Method_43()
	{
		HudWindow.IsOpen = true;
	}

	private void Method_44()
	{
		try
		{
			field_24 = PluginInterface.GetIpcSubscriber<bool>("vnavmesh.Nav.IsReady");
			field_25 = PluginInterface.GetIpcSubscriber<Vector3, bool, bool>("vnavmesh.SimpleMove.PathfindAndMoveTo");
			field_26 = PluginInterface.GetIpcSubscriber<Vector3, float, float, Vector3?>("vnavmesh.Query.Mesh.NearestPoint");
			Log.Information("AutomaticHunting: vnavmesh IPC initialized");
		}
		catch (Exception exception)
		{
			Log.Warning(exception, "AutomaticHunting: vnavmesh IPC init failed");
		}
	}

	private void Method_45(bool arg0)
	{
		Config.Enabled = arg0;
		ConfigService.Save();
		PrintEcho(Config.Enabled ? "已启用" : "已禁用");
	}

	private void Method_46()
	{
		HudWindow.IsOpen = !HudWindow.IsOpen;
		Config.HudVisible = HudWindow.IsOpen;
		ConfigService.Save();
		PrintEcho(HudWindow.IsOpen ? "悬浮窗已显示" : "悬浮窗已隐藏");
	}

	public void PrintDisclaimer()
	{
		PrintEcho("自动化功能只限跟落地开的车，ET和倒数车产生不可预知的行为玩家自行承担后果。反馈bug及建议去DC频道：DR-插件讨论/omni-社区插件 搜索AutomaticHunting。");
		ChatGui.Print("[自动狩猎] 自动化功能只限跟落地开的车，ET和倒数车产生不可预知的行为玩家自行承担后果。反馈bug及建议去DC频道：DR-插件讨论/omni-社区插件 搜索AutomaticHunting。");
	}

	private void Method_48(string arg0, string arg1)
	{
		string[] source = arg1.Split(' ', StringSplitOptions.RemoveEmptyEntries);
		switch (source.FirstOrDefault()?.ToLower() ?? string.Empty)
		{
		case "on":
			Method_45(arg0: true);
			break;
		case "off":
			Method_45(arg0: false);
			break;
		case "leader":
			Method_53(arg0, string.Join(" ", source.Skip(1)));
			break;
		case "stop":
			if (Method_51())
			{
				StopNavigation();
				PrintEcho("导航已停止");
			}
			break;
		case "overlay":
			Method_46();
			break;
		case "status":
			Method_55();
			break;
		case "mount":
			if (Method_51())
			{
				SummonMount();
			}
			break;
		case "setup":
			OnboardingWindow.Open();
			PrintEcho("已打开首次运行向导");
			break;
		case "help":
			HelpWindow.IsOpen = true;
			break;
		case "diag":
			if (Diagnostics.CopyReport(this))
			{
				PrintEcho("诊断信息已复制到剪贴板");
			}
			break;
		case "lock":
			Config.HudLocked = !Config.HudLocked;
			ConfigService.Save();
			PrintEcho(Config.HudLocked ? "HUD 位置已锁定" : "HUD 位置已解锁");
			break;
		case "compact":
			Config.HudCompact = !Config.HudCompact;
			ConfigService.Save();
			PrintEcho(Config.HudCompact ? "HUD 已切换到紧凑模式" : "HUD 已切换到完整模式");
			break;
		case "opacity":
			Method_49(source.Skip(1).FirstOrDefault());
			break;
		case "hud":
			Method_50(source.Skip(1).ToArray());
			break;
		default:
			Method_54();
			break;
		}
	}

	private void Method_49(string? arg0)
	{
		if (string.IsNullOrWhiteSpace(arg0) || !float.TryParse(arg0, NumberStyles.Float, CultureInfo.InvariantCulture, out var result))
		{
			PrintEcho($"用法: /auht opacity <{0.62f:0.00}-1.00>（当前 {Config.HudAlpha:0.00}）");
		}
		else
		{
			Config.HudAlpha = Math.Clamp(result, 0.62f, 1f);
			ConfigService.Save();
			PrintEcho($"HUD 透明度已设为 {Config.HudAlpha:0.00}");
		}
	}

	private void Method_50(string[] arg0)
	{
		switch ((arg0.Length != 0) ? arg0[0].ToLowerInvariant() : string.Empty)
		{
		case "reset":
			HudWindow.ResetPosition();
			PrintEcho("HUD 位置已复位到左上角");
			break;
		case "show":
			HudWindow.IsOpen = true;
			Config.HudVisible = true;
			ConfigService.Save();
			PrintEcho("HUD 已显示");
			break;
		case "hide":
			HudWindow.IsOpen = false;
			Config.HudVisible = false;
			ConfigService.Save();
			PrintEcho("HUD 已隐藏");
			break;
		default:
			PrintEcho("用法: /auht hud reset|show|hide");
			break;
		}
	}

	private bool Method_51()
	{
		if (!Config.Enabled)
		{
			PrintEcho("自动狩猎未启用，无法操作");
			return false;
		}
		if (!Config.DisclaimerAccepted)
		{
			PrintEcho("请先在设置中同意免责声明");
			return false;
		}
		return true;
	}

	public unsafe bool SetLeaderToSelf()
	{
		string empty = string.Empty;
		BattleChara* localPlayer = Control.GetLocalPlayer();
		if (localPlayer == null)
		{
			PrintEcho("无法获取角色信息");
			return false;
		}
		fixed (byte* bytes = localPlayer->Name)
		{
			empty = Encoding.UTF8.GetString(bytes, 64).TrimEnd('\0');
		}
		if (string.IsNullOrWhiteSpace(empty))
		{
			PrintEcho("无法获取角色信息");
			return false;
		}
		LeaderRoster.Add(Config, empty);
		ConfigService.Save();
		PrintEcho("车头已设置为自己: " + empty);
		return true;
	}

	private void Method_53(string arg0, string arg1)
	{
		string text = arg1.Split(' ', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault()?.ToLower() ?? string.Empty;
		if (!(text == "target"))
		{
			if (text == "me")
			{
				SetLeaderToSelf();
			}
			else if (!string.IsNullOrWhiteSpace(arg1))
			{
				string text2 = arg1.Trim();
				LeaderRoster.Add(Config, text2);
				ConfigService.Save();
				PrintEcho("车头已设置为: " + text2);
			}
			else
			{
				PrintEcho("用法: /auht leader target|me|<名字>");
			}
		}
		else
		{
			IGameObject target = TargetManager.Target;
			if (target != null)
			{
				LeaderRoster.Add(Config, target.Name.TextValue);
				ConfigService.Save();
				PrintEcho("车头已设置为当前目标: " + target.Name.TextValue);
			}
			else
			{
				PrintEcho("未选中目标");
			}
		}
	}

	private void Method_54()
	{
		PrintEcho("=== 自动狩猎跟车 帮助 ===");
		PrintEcho("/auht on - 启用插件");
		PrintEcho("/auht off - 禁用插件");
		PrintEcho("/auht stop - 停止导航");
		PrintEcho("/auht status - 显示状态");
		PrintEcho("/auht mount - 召唤坐骑");
		PrintEcho("/auht overlay - 显示/隐藏悬浮窗");
		PrintEcho("/auht setup - 打开首次运行向导");
		PrintEcho("/auht help - 打开使用说明窗口");
		PrintEcho("/auht diag - 复制诊断信息");
		PrintEcho("/auht lock - 锁定/解锁 HUD 位置");
		PrintEcho("/auht compact - 切换 HUD 紧凑模式");
		PrintEcho("/auht opacity <0.62-1.00> - 设置 HUD 透明度");
		PrintEcho("/auht hud reset|show|hide - HUD 位置 / 显隐");
	}

	private void Method_55()
	{
		PrintEcho("状态: " + (Config.Enabled ? "启用" : "禁用"));
		PrintEcho("车头: " + (string.IsNullOrWhiteSpace(Config.LeaderName) ? "未设置" : Config.LeaderName));
		PrintEcho("导航: " + GetNavigationStageText());
		PrintEcho($"传送阈值: {Config.TeleportThreshold:0}m");
	}

	public static void OpenUsageDoc()
	{
		try
		{
			string directoryName = Path.GetDirectoryName(PluginInterface.AssemblyLocation.FullName);
			if (string.IsNullOrEmpty(directoryName))
			{
				Log.Warning("AutomaticHunting: 无法定位插件安装目录");
				return;
			}
			List<string> list = new List<string>
			{
				Path.Combine(directoryName, "使用说明.md"),
				Path.Combine(directoryName, "..", "使用说明.md"),
				Path.Combine(directoryName, "..", "..", "使用说明.md")
			};
			InlineArray5<string> arg = default;
			RecoveredType_202.Method_1214<InlineArray5<string>, string>(ref arg, 0) = directoryName;
			RecoveredType_202.Method_1214<InlineArray5<string>, string>(ref arg, 1) = "..";
			RecoveredType_202.Method_1214<InlineArray5<string>, string>(ref arg, 2) = "..";
			RecoveredType_202.Method_1214<InlineArray5<string>, string>(ref arg, 3) = "..";
			RecoveredType_202.Method_1214<InlineArray5<string>, string>(ref arg, 4) = "使用说明.md";
			list.Add(Path.Combine(RecoveredType_202.Method_1213<InlineArray5<string>, string>(in arg, 5)));
			string text = null;
			foreach (string item in list)
			{
				if (File.Exists(item))
				{
					text = item;
					break;
				}
			}
			if (text == null)
			{
				PrintEchoAlways("未找到使用说明（已检查 " + directoryName + "）");
				return;
			}
			try
			{
				Process.Start(new ProcessStartInfo(text)
				{
					UseShellExecute = true
				});
			}
			catch
			{
				Process.Start(new ProcessStartInfo("notepad.exe", "\"" + text + "\"")
				{
					UseShellExecute = true
				});
			}
			Log.Information("AutomaticHunting: 已打开使用说明 " + text);
			PrintEchoAlways("已打开使用说明: " + Path.GetFileName(text));
		}
		catch (Exception exception)
		{
			Log.Warning(exception, "AutomaticHunting: 打开使用说明失败");
		}
	}

	private void Method_57(IFramework arg0)
	{
		ConfigService.Tick();
		Method_146();
		if (!Config.AutomationEnabled)
		{
			return;
		}
		Method_5();
		bool flag = Condition[ConditionFlag.InCombat];
		if (!flag && field_32.HasValue && !IsNavigating)
		{
			RecoveredType_3 value = field_32.Value;
			field_32 = null;
			PrintEcho(field_34 ? "S怪监控结束，开始执行延迟的Flag" : "战斗结束，开始执行延迟的导航");
			field_34 = false;
			Method_156(value.TerritoryType, value.Position);
		}
		if (!flag && field_73.HasValue && !CrossWorldInProgress)
		{
			(string, uint, uint, Vector3) value2 = field_73.Value;
			field_73 = null;
			PrintEcho("战斗结束，开始执行一键跨服");
			StartManualWorldTravel(value2.Item1, value2.Item2, value2.Item3, value2.Item4);
		}
		if (Config.AutoBmrAi && Method_132())
		{
			if (flag && !field_35)
			{
				GlobalCommands.Process("/bmrai on");
				PrintEcho("BMR AI 已开启");
			}
			else if (!flag && field_35)
			{
				GlobalCommands.Process("/bmrai off");
				PrintEcho("BMR AI 已关闭");
			}
			field_35 = flag;
		}
	}

	private MultiLineMapConfig? Method_58()
	{
		uint field_156 = Method_116();
		if (field_156 == 0)
		{
			return null;
		}
		return Config.MultiLineMaps.FirstOrDefault((MultiLineMapConfig arg0) => !string.IsNullOrWhiteSpace(arg0.MapId) && uint.TryParse(arg0.MapId.Trim(), out var result) && result == field_156);
	}

	private void Method_59(uint arg0)
	{
		Method_60(arg0);
	}

	private void Method_60(uint arg0)
	{
		if (arg0 == 0 || Config.MultiLineMaps.Count == 0)
		{
			return;
		}
		if (!Method_62(arg0))
		{
			PrintEcho($"多线检查: 当前区域未配置(terr={arg0}, 配置数={Config.MultiLineMaps.Count})");
			return;
		}
		DateTime utcNow = DateTime.UtcNow;
		if (utcNow - field_46 < TimeSpan.FromSeconds(8L))
		{
			PrintEcho("多线检查: 防抖中，跳过重复切线");
			return;
		}
		field_46 = utcNow;
		Method_4();
		int currentInstanceNumber = GetCurrentInstanceNumber();
		if (currentInstanceNumber > 0 && currentInstanceNumber == 1)
		{
			Config.CurrentInstanceNumber = 1;
			Method_176();
			PrintEcho($"进入多线地图(terr={arg0}): 已在1线，无需切线");
		}
		else
		{
			PrintEcho($"进入多线地图(terr={arg0}): 等待稳定后切换到1线开始打A怪");
			Method_61(1);
		}
	}

	private async Task Method_61(int arg0)
	{
		_ = 4;
		try
		{
			DateTime utcNow = DateTime.UtcNow;
			while (DateTime.UtcNow - utcNow < TimeSpan.FromSeconds(45L))
			{
				await Task.Delay(500);
				if (!(await Framework.RunOnFrameworkThread(() => Condition[ConditionFlag.BetweenAreas] || Condition[ConditionFlag.BetweenAreas51])) && DateTime.UtcNow - utcNow > TimeSpan.FromSeconds(2L))
				{
					break;
				}
			}
			await Task.Delay((int)(Config.WaitAfterTeleport * 1000f));
			for (int num = 0; num < 30; num++)
			{
				if (!(await Framework.RunOnFrameworkThread(() => Method_133())))
				{
					break;
				}
				await Task.Delay(500);
			}
			PrintEcho("进入多线地图: 区域已稳定，执行切线");
			SwitchToInstance(arg0);
		}
		catch (Exception exception)
		{
			Log.Warning(exception, "AutomaticHunting: 等待稳定后切线失败");
		}
	}

	private bool Method_62(uint arg0)
	{
		return Config.MultiLineMaps.Any((MultiLineMapConfig multiLineMapConfig) => !string.IsNullOrWhiteSpace(multiLineMapConfig.MapId) && uint.TryParse(multiLineMapConfig.MapId.Trim(), out var result) && result == arg0);
	}

	public unsafe int GetCurrentInstanceNumber()
	{
		try
		{
			UIState* ptr = UIState.Instance();
			if (ptr == null)
			{
				return 0;
			}
			return (int)ptr->PublicInstance.InstanceId;
		}
		catch (Exception exception)
		{
			Log.Warning(exception, "AutomaticHunting: 读取当前分线失败");
			return 0;
		}
	}

	public void SwitchToInstance(int arg0)
	{
		int num = Math.Clamp(arg0, 1, 9);
		int currentInstanceNumber = GetCurrentInstanceNumber();
		if (currentInstanceNumber > 0 && currentInstanceNumber == num)
		{
			Config.CurrentInstanceNumber = num;
			Method_176();
			PrintEcho($"已在 {num} 线，无需切线");
			return;
		}
		Config.CurrentInstanceNumber = num;
		Method_176();
		string field_158 = $"/ahglobal instance {num}";
		Framework.RunOnFrameworkThread(() =>
		{
			try
			{
				GlobalCommands.Process(field_158);
				Log.Information("AutomaticHunting: 执行切线命令 " + field_158);
			}
			catch (Exception exception)
			{
				PrintEcho("切线命令执行失败: " + field_158);
				Log.Warning(exception, "AutomaticHunting: 切线命令失败 " + field_158);
			}
		});
		PrintEcho("正在切换副本线: " + field_158);
		Method_65(num);
	}

	private async Task Method_65(int arg0)
	{
		try
		{
			if (await Method_66(arg0, TimeSpan.FromSeconds(5L)))
			{
				Method_67(arg0);
				return;
			}
			bool switchRetryAtAetheryte = Config.SwitchRetryAtAetheryte;
			if (switchRetryAtAetheryte)
			{
				await Method_69();
				PrintEcho($"切线未确认，重发命令 /ahglobal instance {arg0}");
				await Framework.RunOnFrameworkThread(() =>
				{
					try
					{
						GlobalCommands.Process($"/ahglobal instance {arg0}");
					}
					catch (Exception exception2)
					{
						Log.Warning(exception2, $"AutomaticHunting: 切线重发失败 {arg0}");
					}
				});
			}
			if (await Method_66(arg0, TimeSpan.FromSeconds(switchRetryAtAetheryte ? 20 : 10)))
			{
				Method_67(arg0);
				return;
			}
			PrintEcho($"切线确认超时，真实线未达到目标 {arg0} 线");
			Log.Warning($"AutomaticHunting: 切线确认超时(目标 {arg0} 线)");
		}
		catch (Exception exception)
		{
			Log.Warning(exception, "AutomaticHunting: 切线确认失败");
		}
	}

	private async Task<bool> Method_66(int arg0, TimeSpan arg1)
	{
		DateTime utcNow = DateTime.UtcNow;
		while (DateTime.UtcNow - utcNow < arg1)
		{
			await Task.Delay(500);
			if (await Framework.RunOnFrameworkThread(() => (!Condition[ConditionFlag.BetweenAreas] && !Condition[ConditionFlag.BetweenAreas51]) ? GetCurrentInstanceNumber() : (-1)) == arg0)
			{
				return true;
			}
		}
		return false;
	}

	private void Method_67(int arg0)
	{
		Config.CurrentInstanceNumber = arg0;
		Method_176();
		PrintEcho($"切线完成，已确认在 {arg0} 线");
		Method_68(arg0);
	}

	private void Method_68(int arg0)
	{
		RecoveredType_3 value;
		lock (field_42)
		{
			if (!field_43.HasValue)
			{
				return;
			}
			value = field_43.Value;
			field_43 = null;
			field_44 = null;
			field_45 = DateTime.MinValue;
		}
		PrintEcho($"真实线已确认在 {arg0} 线，开始导航到Flag");
		Method_156(value.TerritoryType, value.Position);
	}

	private async Task Method_69()
	{
		_ = 3;
		try
		{
			uint field_161 = Method_116();
			List<(uint Id, Vector3 Position, string Name)> list = await Framework.RunOnFrameworkThread(() => Method_127(field_161));
			if (list.Count == 0)
			{
				return;
			}
			Vector3? field_162 = await Framework.RunOnFrameworkThread((Func<Vector3?>)GetLocalPlayerPosition);
			if (!field_162.HasValue)
			{
				return;
			}
			(uint, Vector3, string) tuple = list.MinBy(((uint Id, Vector3 Position, string Name) arg0) => Vector3.DistanceSquared(arg0.Position, field_162.Value));
			if (!(Vector3.Distance(tuple.Item2, field_162.Value) <= 60f))
			{
				PrintEcho("切线未生效，传送到最近以太之光(" + tuple.Item3 + ")后重试");
				if (await Method_123(tuple.Item1, CancellationToken.None))
				{
					await Method_163(field_161, CancellationToken.None);
				}
			}
		}
		catch (Exception exception)
		{
			Log.Warning(exception, "AutomaticHunting: 切线重试传送到以太之光失败");
		}
	}

	public bool ShouldSwitchLineOnSameMap(uint arg0)
	{
		MultiLineMapConfig multiLineMapConfig = Method_58();
		if (multiLineMapConfig == null)
		{
			return false;
		}
		return field_38 >= multiLineMapConfig.AMobCount;
	}

	public void ResetInstanceKillCount()
	{
		Method_4();
	}

	public string GetNavigationStageText()
	{
		lock (field_53)
		{
			return field_56 switch
			{
				RecoveredType_8.field_117 => "等待传送", 
				RecoveredType_8.field_118 => "召唤坐骑", 
				RecoveredType_8.field_119 => "前往Flag", 
				RecoveredType_8.field_120 => "前往A怪", 
				RecoveredType_8.field_121 => "前往S怪", 
				_ => "空闲", 
			};
		}
	}

	public (string Label, Vector4 Color, float? Distance) GetTargetInfo()
	{
		Vector3? localPlayerPosition = GetLocalPlayerPosition();
		if (!localPlayerPosition.HasValue)
		{
			return (Label: "等待目标", Color: UiColorDisabled, Distance: null);
		}
		RecoveredType_4? recoveredType_ = Method_2();
		if (recoveredType_.HasValue)
		{
			float value = Method_197(localPlayerPosition.Value, recoveredType_.Value.Position);
			return (Label: "★ A怪 · " + recoveredType_.Value.Name, Color: UiColorGold, Distance: value);
		}
		if (LastFlagPosition.HasValue)
		{
			float value2 = Method_197(localPlayerPosition.Value, LastFlagPosition.Value);
			return (Label: "▸ Flag 目标", Color: UiColorNav, Distance: value2);
		}
		return (Label: "等待目标", Color: UiColorDisabled, Distance: null);
	}

	public void BeginSMonitor(string arg0, string arg1, float arg2, float arg3)
	{
		SMonitor = new SMonitorStatus
		{
			MobName = arg0,
			Phase = arg1,
			HpPercent = arg2,
			Threshold = arg3
		};
	}

	public void FinishSMonitor(string arg0)
	{
		SMonitorStatus field_164 = SMonitor;
		if (field_164 == null || field_164.Finished)
		{
			return;
		}
		field_164.Finished = true;
		field_164.Phase = arg0;
		Task.Run(async () =>
		{
			await Task.Delay(2000);
			if (SMonitor == field_164)
			{
				SMonitor = null;
			}
		});
	}

	public static Vector4 GetSMonitorColor(SMonitorStatus arg0)
	{
		if (arg0.Finished)
		{
			return UiColorDisabled;
		}
		if (arg0.Phase.Contains("开怪"))
		{
			return UiColorError;
		}
		if (arg0.TargetLost)
		{
			return UiColorWait;
		}
		return UiColorGold;
	}

	public float? GetLeaderDistance()
	{
		Vector3? localPlayerPosition = GetLocalPlayerPosition();
		Vector3? leaderPosition = GetLeaderPosition();
		if (!localPlayerPosition.HasValue || !leaderPosition.HasValue)
		{
			return null;
		}
		return Method_197(localPlayerPosition.Value, leaderPosition.Value);
	}

	public bool IsBmrLoaded()
	{
		if (IsPluginLoaded("BossModReborn"))
		{
			return true;
		}
		return PluginInterface.InstalledPlugins.Any((IExposedPlugin arg0) => arg0.IsLoaded && (arg0.InternalName.Equals("BossModReborn", StringComparison.OrdinalIgnoreCase) || arg0.InternalName.Equals("BossMod", StringComparison.OrdinalIgnoreCase)));
	}

	public unsafe void RefreshMountList()
	{
		try
		{
			List<MountInfo> list = new List<MountInfo>();
			PlayerState* ptr = PlayerState.Instance();
			if (ptr == null)
			{
				OwnedMounts = list;
				MountNames = new string[1] { "随机飞行坐骑" };
				SelectedMountIndex = 0;
				return;
			}
			ExcelSheet<Mount> excelSheet = DataManager.GetExcelSheet<Mount>();
			if (excelSheet == null)
			{
				OwnedMounts = list;
				MountNames = new string[1] { "随机飞行坐骑" };
				SelectedMountIndex = 0;
				return;
			}
			foreach (Mount item in excelSheet)
			{
				if (item.RowId != 0 && ptr->IsMountUnlocked(item.RowId))
				{
					string text = item.Singular.ExtractText();
					if (!string.IsNullOrWhiteSpace(text))
					{
						list.Add(new MountInfo
						{
							RowId = item.RowId,
							Name = text,
							IconId = item.Icon
						});
					}
				}
			}
			list.Sort((MountInfo arg0, MountInfo arg1) => string.Compare(arg0.Name, arg1.Name, StringComparison.OrdinalIgnoreCase));
			OwnedMounts = list;
			List<string> list2 = new List<string> { "随机飞行坐骑" };
			list2.AddRange(list.Select((MountInfo arg0) => arg0.Name));
			MountNames = list2.ToArray();
			if (Config.SelectedMountId == 0)
			{
				SelectedMountIndex = 0;
			}
			else
			{
				int num = list.FindIndex((MountInfo arg0) => arg0.RowId == Config.SelectedMountId);
				SelectedMountIndex = ((num >= 0) ? (num + 1) : 0);
			}
			Log.Information($"AutomaticHunting: Refreshed mount list, {list.Count} mounts found");
		}
		catch (Exception exception)
		{
			Log.Warning(exception, "AutomaticHunting: Failed to refresh mount list");
			OwnedMounts = new List<MountInfo>();
			MountNames = new string[1] { "随机飞行坐骑" };
			SelectedMountIndex = 0;
		}
	}

	public void UpdateMountCommand()
	{
		if (SelectedMountIndex == 0)
		{
			Config.SelectedMountId = 0u;
		}
		else if (SelectedMountIndex - 1 < OwnedMounts.Count)
		{
			MountInfo mountInfo = OwnedMounts[SelectedMountIndex - 1];
			Config.SelectedMountId = mountInfo.RowId;
		}
		ConfigService.Save();
	}

	private void Method_81(IHandleableChatMessage arg0)
	{
		try
		{
			Method_86(arg0);
			Method_89(arg0);
		}
		catch (Exception exception)
		{
			Log.Warning(exception, "AutomaticHunting: chat message handler failed");
		}
	}

	private static string Method_82(IHandleableChatMessage arg0)
	{
		string text = arg0.OriginalMessage.ToString();
		if (!string.IsNullOrWhiteSpace(text))
		{
			return text;
		}
		return arg0.Message.TextValue;
	}

	private static string Method_83(IHandleableChatMessage arg0)
	{
		StringBuilder stringBuilder = new StringBuilder();
		foreach (Payload payload in arg0.Message.Payloads)
		{
			if (!(payload is MapLinkPayload) && payload is ITextProvider textProvider)
			{
				stringBuilder.Append(textProvider.Text);
			}
		}
		return stringBuilder.ToString();
	}

	private static string? Method_84(IHandleableChatMessage arg0)
	{
		foreach (Payload payload in arg0.Sender.Payloads)
		{
			if (payload is PlayerPayload playerPayload && !string.IsNullOrWhiteSpace(playerPayload.PlayerName))
			{
				return playerPayload.PlayerName;
			}
		}
		string text = arg0.OriginalSender.ToString();
		if (!string.IsNullOrWhiteSpace(text))
		{
			return text;
		}
		return null;
	}

	private static bool Method_85(IHandleableChatMessage arg0, out MapLinkPayload arg1)
	{
		foreach (Payload payload in arg0.Message.Payloads)
		{
			if (payload is MapLinkPayload mapLinkPayload)
			{
				arg1 = mapLinkPayload;
				return true;
			}
		}
		arg1 = null;
		return false;
	}

	private void Method_86(IHandleableChatMessage arg0)
	{
		int instance = GetCurrentInstanceNumber();
		if (instance <= 0) return;
		string text2 = instance.ToString(CultureInfo.InvariantCulture);
		if (text2 == Config.CurrentInstanceFingerprint) return;
		if (!string.IsNullOrWhiteSpace(text2))
		{
			field_41 = text2;
			Config.CurrentInstanceFingerprint = text2;
			PrintEcho($"副本区指纹: '{text2}' 当前目标线:{Config.CurrentInstanceNumber}");
			string text3 = field_44;
			RecoveredType_3 arg2;
			if (field_43.HasValue && !string.IsNullOrEmpty(text3) && text3 != text2 && Method_87(out var arg1))
			{
				PrintEcho($"副本区已切换到位(指纹变化 '{text3}'→'{text2}')，开始导航到Flag");
				Method_156(arg1.TerritoryType, arg1.Position);
			}
			else if (field_43.HasValue && field_45 != DateTime.MinValue && DateTime.UtcNow - field_45 > TimeSpan.FromSeconds(15L) && Method_87(out arg2))
			{
				PrintEcho("切换副本线确认超时(15s)，改按当前指纹 '" + text2 + "' 导航到Flag");
				Method_156(arg2.TerritoryType, arg2.Position);
			}
		}
	}

	private bool Method_87(out RecoveredType_3 arg0)
	{
		lock (field_42)
		{
			if (!field_43.HasValue)
			{
				arg0 = default;
				return false;
			}
			arg0 = field_43.Value;
			field_43 = null;
			field_44 = null;
			field_45 = DateTime.MinValue;
			return true;
		}
	}

	private static string? Method_88(string arg0)
	{
		int num = arg0.IndexOf('"');
		int num2 = arg0.LastIndexOf('"');
		if (num < 0 || num2 <= num + 1)
		{
			return null;
		}
		int num3 = num + 1;
		string text = arg0.Substring(num3, num2 - num3);
		if (text.Length < 2)
		{
			return null;
		}
		return text[text.Length - 1].ToString();
	}

	private void Method_89(IHandleableChatMessage arg0)
	{
		string text = Method_84(arg0);
		if (Method_91(arg0, text))
		{
			return;
		}
		if (CrossWorldInProgress)
		{
			if (DateTime.UtcNow - field_60 > TimeSpan.FromSeconds(3L))
			{
				field_60 = DateTime.UtcNow;
				PrintEcho("跨服流程进行中，忽略普通车头指令");
			}
		}
		else
		{
			if (!Config.AutomationEnabled || string.IsNullOrWhiteSpace(Config.LeaderName))
			{
				return;
			}
			if (!Method_94(text, Config.LeaderName))
			{
				Log.Debug($"AutomaticHunting: 车头消息发送者不匹配: sender='{text}' (norm='{Method_95(text ?? "")}') vs leader='{Config.LeaderName}' (norm='{Method_95(Config.LeaderName)}')");
				return;
			}
			if (Config.LeaderYellOnly && arg0.LogKind != XivChatType.Shout)
			{
				Log.Debug($"AutomaticHunting: 忽略非喊话频道车头消息 kind={arg0.LogKind}");
				return;
			}
			uint arg2;
			uint arg3;
			Vector3 arg4;
			string text2;
			if (Method_85(arg0, out MapLinkPayload arg1))
			{
				arg2 = arg1.TerritoryType.RowId;
				arg3 = arg1.Map.RowId;
				arg4 = new Vector3((float)arg1.RawX / 1000f, 0f, (float)arg1.RawY / 1000f);
				text2 = $"{arg2}|{arg3}|{arg1.RawX / 1000}|{arg1.RawY / 1000}";
			}
			else
			{
				if (!Method_90(arg0, out arg2, out arg3, out arg4))
				{
					return;
				}
				text2 = $"{arg2}|{arg3}|{(int)arg4.X}|{(int)arg4.Z}";
			}
			bool flag = DateTime.UtcNow - field_29 < TimeSpan.FromSeconds(60L);
			if ((text2 == field_27) & flag)
			{
				PrintEcho("忽略60秒内重复Flag");
				return;
			}
			field_27 = text2;
			field_29 = DateTime.UtcNow;
			if (flag && LastFlagPosition.HasValue)
			{
				float num = Vector2.Distance(new Vector2(LastFlagPosition.Value.X, LastFlagPosition.Value.Z), new Vector2(arg4.X, arg4.Z));
				if (num < 50f)
				{
					PrintEcho($"忽略附近重复Flag（距上次{num:0.0}m）");
					return;
				}
			}
			if (Condition[ConditionFlag.InCombat] && LastFlagPosition.HasValue)
			{
				float num2 = Vector2.Distance(new Vector2(LastFlagPosition.Value.X, LastFlagPosition.Value.Z), new Vector2(arg4.X, arg4.Z));
				if (num2 < 50f)
				{
					PrintEcho($"疑似误触坐标（距当前{num2:0.0}m），不执行");
					return;
				}
			}
			LastFlagPosition = arg4;
			field_28 = arg2;
			field_31 = Method_127(arg2);
			if (Config.SetFlagOnNavigation && !Method_126(arg2, arg3, arg4))
			{
				PrintEcho("设置地图Flag失败");
			}
			Method_92(arg2, arg3, arg4);
		}
	}

	private bool Method_90(IHandleableChatMessage arg0, out uint arg1, out uint arg2, out Vector3 arg3)
	{
		arg1 = 0u;
		arg2 = 0u;
		arg3 = default;
		string text = Method_82(arg0);
		if (string.IsNullOrWhiteSpace(text))
		{
			return false;
		}
		Match match = field_61.Match(text);
		if (!match.Success)
		{
			return false;
		}
		float num = (float)int.Parse(match.Groups[1].Value) + (float)int.Parse(match.Groups[2].Value) / 10f;
		float num2 = (float)int.Parse(match.Groups[3].Value) + (float)int.Parse(match.Groups[4].Value) / 10f;
		Lumina.Excel.Sheets.Map? map = Method_135();
		if (!map.HasValue)
		{
			return false;
		}
		Vector2 vector = Method_136(num, num2, map.Value);
		arg1 = ClientState.TerritoryType;
		arg2 = map.Value.RowId;
		arg3 = new Vector3(vector.X, 0f, vector.Y);
		Log.Debug($"AutomaticHunting: 文本坐标解析 ({num:0.0},{num2:0.0}) → 世界 ({vector.X:0.0},{vector.Y:0.0})");
		return true;
	}

	private bool Method_91(IHandleableChatMessage arg0, string? arg1)
	{
		if (!FeatureFlags.CrossWorldLeaderEnabled)
		{
			return false;
		}
		if (!Config.AutomationEnabled || string.IsNullOrWhiteSpace(Config.CrossWorldLeader))
		{
			return false;
		}
		if (string.IsNullOrWhiteSpace(arg1))
		{
			return false;
		}
		if (!string.Equals(Method_95(arg1), Method_95(Config.CrossWorldLeader), StringComparison.OrdinalIgnoreCase))
		{
			return false;
		}
		string arg2 = Method_82(arg0);
		string arg3 = Method_83(arg0);
		Vector3? arg4 = null;
		uint arg5 = 0u;
		if (Method_85(arg0, out MapLinkPayload arg6))
		{
			arg5 = arg6.TerritoryType.RowId;
			arg4 = new Vector3((float)arg6.RawX / 1000f, 0f, (float)arg6.RawY / 1000f);
		}
		Method_99(arg1, arg2, arg3, arg4, arg5);
		return true;
	}

	private void Method_92(uint arg0, uint arg1, Vector3 arg2)
	{
		if (arg0 == Method_116() && ShouldSwitchLineOnSameMap(arg0))
		{
			ResetInstanceKillCount();
			Method_93(arg0, arg2);
		}
		else
		{
			Method_156(arg0, arg2);
		}
	}

	private void Method_93(uint arg0, Vector3 arg1)
	{
		field_44 = Config.CurrentInstanceFingerprint;
		field_43 = new RecoveredType_3(arg0, arg1);
		field_45 = DateTime.UtcNow;
		SwitchToNextInstance();
		PrintEcho($"{Config.CurrentInstanceNumber}线A怪击杀达标，已切换，等待副本区确认后导航");
	}

	private static bool Method_94(string? arg0, string arg1)
	{
		if (string.IsNullOrWhiteSpace(arg0))
		{
			return false;
		}
		string text = Method_95(arg0);
		if (text.Length == 0)
		{
			return false;
		}
		string[] array = Method_95(arg1).Split(' ', StringSplitOptions.RemoveEmptyEntries);
		foreach (string text2 in array)
		{
			if (text2.Length != 0 && string.Equals(text, text2, StringComparison.OrdinalIgnoreCase))
			{
				return true;
			}
		}
		return false;
	}

	private static string Method_95(string arg0)
	{
		if (string.IsNullOrEmpty(arg0))
		{
			return string.Empty;
		}
		int num = arg0.IndexOf('@');
		if (num >= 0)
		{
			arg0 = arg0.Substring(0, num);
		}
		StringBuilder stringBuilder = new StringBuilder(arg0.Length);
		string text = arg0;
		foreach (char c in text)
		{
			if (!char.IsControl(c) && ((c != '\u00a0' && c != '\u3000') || 1 == 0))
			{
				stringBuilder.Append(c);
			}
		}
		return stringBuilder.ToString().Trim().TrimEnd(new char[2] { '：', ':' });
	}

	private void Method_97(string arg0)
	{
		lock (field_62)
		{
			field_74 = arg0;
			field_75 = DateTime.UtcNow;
		}
	}

	private static string Method_98(string arg0, string arg1)
	{
		if (string.IsNullOrEmpty(arg0))
		{
			return arg0;
		}
		foreach (string item2 in field_76.GetOrAdd(arg1, (string text2) =>
		{
			List<string> list = new List<string>();
			try
			{
				ExcelSheet<PlaceName> excelSheet = DataManager.GetExcelSheet<PlaceName>();
				if (excelSheet != null)
				{
					foreach (PlaceName item in excelSheet)
					{
						string text = item.Name.ExtractText();
						if (text.Length > text2.Length && text.Contains(text2))
						{
							list.Add(text);
						}
					}
				}
			}
			catch (Exception exception)
			{
				Log.Warning(exception, "AutomaticHunting: 构建地名剔除表失败 [" + text2 + "]");
			}
			return list;
		}))
		{
			if (arg0.Contains(item2))
			{
				arg0 = arg0.Replace(item2, " ");
			}
		}
		return arg0;
	}

	private void Method_99(string arg0, string arg1, string arg2, Vector3? arg3, uint arg4)
	{
		if (!FeatureFlags.CrossWorldLeaderEnabled || !Config.AutomationEnabled || string.IsNullOrWhiteSpace(Config.CrossWorldLeader) || !string.Equals(Method_95(arg0), Method_95(Config.CrossWorldLeader), StringComparison.OrdinalIgnoreCase))
		{
			return;
		}
		bool flag = false;
		foreach (KeyValuePair<string, string> item in CrossWorldData.ServerWorldTravel)
		{
			if (Method_98(arg2, item.Key).Contains(item.Key) && !flag)
			{
				Method_100(item.Key, item.Value);
				flag = true;
			}
		}
		foreach (KeyValuePair<string, string> item2 in CrossWorldData.InstanceSwitch)
		{
			if (arg1.Contains(item2.Key))
			{
				lock (field_62)
				{
					field_69 = item2.Value;
				}
				break;
			}
		}
		if (!arg3.HasValue || arg4 == 0)
		{
			return;
		}
		lock (field_62)
		{
			field_67 = arg4;
			field_68 = arg3.Value;
		}
	}

	private void Method_100(string arg0, string arg1, bool arg2 = false)
	{
		lock (field_62)
		{
			if (field_65)
			{
				PrintEcho("跨服流程正在进行中，忽略新的跨服指令");
				return;
			}
			if (!arg2 && string.Equals(arg0, field_74, StringComparison.OrdinalIgnoreCase) && DateTime.UtcNow - field_75 < TimeSpan.FromSeconds(60L))
			{
				Log.Debug("AutomaticHunting: 已在服务器[" + arg0 + "]（60秒冷却内），忽略重复跨服指令");
				return;
			}
			field_65 = true;
			field_66 = DateTime.UtcNow;
		}
		StopNavigation();
		ResetInstanceKillCount();
		uint field_172 = Method_111(arg0);
		field_64 = new CancellationTokenSource();
		field_63 = Task.Run(() => Method_103(arg0, field_172, arg1, field_64.Token));
	}

	public void StartManualWorldTravel(string arg0, uint arg1, uint arg2, Vector3 arg3, bool arg4 = false)
	{
		if (!arg4 && IsHuntChainRunning)
		{
			Method_145();
			PrintEcho("连车: 检测到手动一键跨服，连车已停止");
		}
		if (Condition[ConditionFlag.InCombat])
		{
			field_73 = (arg0, arg1, arg2, arg3);
			PrintEcho("战斗中，已保存一键跨服，战斗结束后执行");
			return;
		}
		if (CrossWorldInProgress)
		{
			PrintEcho("跨服流程正在进行中，忽略一键跨服");
			return;
		}
		if (!CrossWorldData.ServerWorldTravel.TryGetValue(arg0, out string value))
		{
			PrintEcho("服务器[" + arg0 + "]无跨服命令，无法跨服");
			return;
		}
		lock (field_62)
		{
			if (field_65)
			{
				PrintEcho("跨服流程正在进行中，忽略一键跨服");
				return;
			}
			field_70 = arg1;
			field_71 = arg2;
			field_72 = arg3;
			Method_100(arg0, value, arg2: true);
		}
	}

	public void StopCrossWorld()
	{
		lock (field_62)
		{
			field_64?.Cancel();
			field_64?.Dispose();
			field_64 = null;
			field_65 = false;
			field_66 = DateTime.MinValue;
			field_67 = 0u;
			field_68 = default;
			field_69 = null;
			field_63 = null;
		}
		Method_145();
		StopNavigation();
		PrintEcho("已停止跨服流程，恢复普通车头识别");
	}

	private async Task Method_103(string arg0, uint arg1, string arg2, CancellationToken arg3)
	{
		_ = 11;
		try
		{
			if (arg1 == 0)
			{
				PrintEcho("无法识别目标服务器: " + arg0 + "，跨服中止");
				return;
			}
			uint num = 0u;
			for (int i = 0; i < 3; i++)
			{
				num = await Method_109();
				if (num != 0)
				{
					break;
				}
				await Task.Delay(1000, arg3);
			}
			if (num == 0)
			{
				PrintEcho("无法读取当前服务器，跨服中止");
				return;
			}
			bool flag = num == arg1;
			if (!flag)
			{
				flag = await Method_110() == arg0;
			}
			if (flag)
			{
				PrintEcho("已在服务器[" + arg0 + "]，跳过跨服");
				Method_97(arg0);
				await Method_104(arg3);
				return;
			}
			PrintEcho("执行跨服: " + arg2);
			await Method_108(arg2);
			DateTime utcNow = DateTime.UtcNow;
			DateTime utcNow2 = DateTime.UtcNow;
			DateTime utcNow3 = DateTime.UtcNow;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			int num2 = 0;
			while (DateTime.UtcNow - utcNow < TimeSpan.FromMinutes(10L))
			{
				arg3.ThrowIfCancellationRequested();
				await Task.Delay(200, arg3);
				var (flag5, flag6) = await Framework.RunOnFrameworkThread(() => (Condition[ConditionFlag.ReadyingVisitOtherWorld], Condition[ConditionFlag.BetweenAreas] || Condition[ConditionFlag.BetweenAreas51]));
				if (flag5 && !flag3)
				{
					flag3 = true;
					PrintEcho("检测到跨服状态92(ReadyingVisitOtherWorld)：跨服指令已生效");
				}
				if (flag5 | flag6)
				{
					flag4 = true;
				}
				uint num3 = await Method_109();
				if (num3 != 0 && num3 == arg1)
				{
					flag2 = true;
					break;
				}
				flag = num3 != 0;
				if (flag)
				{
					flag = await Method_110() == arg0;
				}
				if (flag)
				{
					flag2 = true;
					break;
				}
				if (!flag4 && DateTime.UtcNow - utcNow2 > TimeSpan.FromSeconds(5L) && !flag5 && !flag6)
				{
					await Method_108(arg2);
					utcNow2 = DateTime.UtcNow;
					num2++;
					if (num2 == 1 || num2 % 6 == 0)
					{
						PrintEcho($"跨服指令未受理，重发第{num2}次: {arg2}");
					}
				}
				if (DateTime.UtcNow - utcNow3 > TimeSpan.FromSeconds(60L))
				{
					utcNow3 = DateTime.UtcNow;
					int value = (int)(DateTime.UtcNow - utcNow).TotalSeconds;
					PrintEcho($"跨服等待中: 已等待{value}/600秒");
				}
			}
			if (!flag2)
			{
				PrintEcho("跨服超时未到达目标服务器(10分钟)，流程中止");
				return;
			}
			await Task.Delay(5000, arg3);
			PrintEcho("跨服完成，已到达服务器[" + arg0 + "]");
			Method_97(arg0);
			await Method_104(arg3);
		}
		catch (OperationCanceledException)
		{
			Log.Information("AutomaticHunting: 跨服流程被手动中断");
			PrintEcho("跨服流程已中断");
		}
		catch (Exception ex2)
		{
			Log.Warning(ex2, "AutomaticHunting: 跨服流程异常");
			PrintEcho("跨服流程异常: " + ex2.Message);
		}
		finally
		{
			lock (field_62)
			{
				field_65 = false;
				field_66 = DateTime.MinValue;
				field_67 = 0u;
				field_68 = default;
				field_69 = null;
				field_70 = 0u;
				field_71 = 0u;
				field_72 = default;
				field_64?.Dispose();
				field_64 = null;
				field_63 = null;
			}
		}
	}

	private async Task Method_104(CancellationToken arg0)
	{
		uint num;
		uint num2;
		Vector3 arg1;
		lock (field_62)
		{
			num = field_70;
			num2 = field_71;
			arg1 = field_72;
		}
		if (num != 0 && num2 != 0)
		{
			await Method_105(num, num2, arg1, arg0);
			return;
		}
		PrintEcho("开始处理目标Flag");
		string arg2;
		uint field_174;
		Vector3 vector;
		lock (field_62)
		{
			arg2 = field_69;
			field_174 = field_67;
			vector = field_68;
		}
		if (field_174 == 0)
		{
			PrintEcho("跨服完成但无目标Flag，跳过");
			return;
		}
		LastFlagPosition = vector;
		field_28 = field_174;
		field_31 = await Framework.RunOnFrameworkThread(() => Method_127(field_174));
		await Method_106(field_174, vector, arg2);
	}

	private async Task Method_105(uint arg0, uint arg1, Vector3 arg2, CancellationToken arg3)
	{
		try
		{
			arg3.ThrowIfCancellationRequested();
			PrintEcho("跨服完成，传送到所选水晶...");
			bool flag = await Method_123(arg1, arg3);
			if (flag)
			{
				flag = await Method_163(arg0, arg3);
			}
			if (flag)
			{
				LastFlagPosition = arg2;
				field_28 = arg0;
				field_31 = await Framework.RunOnFrameworkThread(() => Method_127(arg0));
				PrintEcho("已到达目标地图的所选水晶");
			}
			else
			{
				PrintEcho("传送到所选水晶失败");
			}
		}
		catch (OperationCanceledException)
		{
			PrintEcho("一键跨服传送已中断");
		}
		catch (Exception ex2)
		{
			Log.Warning(ex2, "AutomaticHunting: 一键跨服传送异常");
			PrintEcho("一键跨服传送异常: " + ex2.Message);
		}
		finally
		{
			lock (field_62)
			{
				field_70 = 0u;
				field_71 = 0u;
				field_72 = default;
			}
		}
	}

	private async Task Method_106(uint arg0, Vector3 arg1, string? arg2)
	{
		CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
		try
		{
			CancellationToken token = cancellationTokenSource.Token;
			RecoveredType_3 arg3 = new RecoveredType_3(arg0, arg1);
			switch (await Method_159(arg3, token))
			{
			case RecoveredType_10.field_130:
				return;
			case RecoveredType_10.field_129:
				Method_196(RecoveredType_8.field_117, cancellationTokenSource);
				if (!(await Method_163(arg0, token)))
				{
					return;
				}
				break;
			}
			await Method_107(TimeSpan.FromSeconds(45L));
			await Task.Delay((int)(Config.WaitAfterTeleport * 1000f));
			if (!string.IsNullOrWhiteSpace(arg2))
			{
				PrintEcho("跨服目标切线: " + arg2);
				await Method_108(arg2);
				await Task.Delay(3000);
			}
			Method_196(RecoveredType_8.field_118, cancellationTokenSource);
			if (!(await Method_182(token)))
			{
				PrintEcho("召唤坐骑失败，跨服导航已取消");
				return;
			}
			await Task.Delay(500, token);
			await Method_164(arg3, token, cancellationTokenSource);
		}
		catch (OperationCanceledException)
		{
			Log.Information("AutomaticHunting: 跨服导航取消");
		}
		catch (Exception exception)
		{
			Log.Warning(exception, "AutomaticHunting: 跨服导航失败");
		}
		finally
		{
			cancellationTokenSource.Dispose();
		}
	}

	private async Task Method_107(TimeSpan arg0)
	{
		DateTime utcNow = DateTime.UtcNow;
		while (DateTime.UtcNow - utcNow < arg0 && await Framework.RunOnFrameworkThread(() => Method_133()))
		{
			await Task.Delay(500);
		}
	}

	private Task Method_108(string arg0)
	{
		return Framework.RunOnFrameworkThread(() =>
		{
			try
			{
				GlobalCommands.Process(arg0);
			}
			catch (Exception exception)
			{
				Log.Warning(exception, "AutomaticHunting: 执行跨服命令失败 " + arg0);
			}
		});
	}

	private async Task<uint> Method_109()
	{
		return await Framework.RunOnFrameworkThread(() =>
		{
			try
			{
				IPlayerCharacter localPlayer = ObjectTable.LocalPlayer;
				if (localPlayer != null)
				{
					return localPlayer.CurrentWorld.RowId;
				}
			}
			catch (Exception exception)
			{
				Log.Warning(exception, "AutomaticHunting: 读取当前服务器失败");
			}
			return 0u;
		});
	}

	private async Task<string> Method_110()
	{
		return await Framework.RunOnFrameworkThread(() =>
		{
			try
			{
				IPlayerCharacter localPlayer = ObjectTable.LocalPlayer;
				if (localPlayer != null)
				{
					World? world = DataManager.GetExcelSheet<World>()?.GetRow(localPlayer.CurrentWorld.RowId);
					if (world.HasValue)
					{
						return world.Value.Name.ExtractText();
					}
				}
			}
			catch (Exception exception)
			{
				Log.Warning(exception, "AutomaticHunting: 读取当前服务器名失败");
			}
			return string.Empty;
		});
	}

	private uint Method_111(string arg0)
	{
		try
		{
			ExcelSheet<World> excelSheet = DataManager.GetExcelSheet<World>();
			if (excelSheet == null)
			{
				return 0u;
			}
			uint num = 0u;
			foreach (World item in excelSheet)
			{
				if (!(item.Name.ExtractText() != arg0))
				{
					if (item.IsPublic)
					{
						return item.RowId;
					}
					if (num == 0)
					{
						num = item.RowId;
					}
				}
			}
			return num;
		}
		catch (Exception exception)
		{
			Log.Warning(exception, "AutomaticHunting: 查询服务器[" + arg0 + "]失败");
		}
		return 0u;
	}

	public unsafe static Vector3? GetLocalPlayerPosition()
	{
		BattleChara* localPlayer = Control.GetLocalPlayer();
		if (localPlayer == null)
		{
			return null;
		}
		return localPlayer->Position;
	}

	private unsafe uint Method_113()
	{
		BattleChara* localPlayer = Control.GetLocalPlayer();
		if (localPlayer == null)
		{
			return 0u;
		}
		string text;
		fixed (byte* bytes = localPlayer->Name)
		{
			text = Encoding.UTF8.GetString(bytes, 64).TrimEnd('\0');
		}
		if (string.IsNullOrEmpty(text))
		{
			return 0u;
		}
		uint num = 2166136261u;
		string text2 = text;
		foreach (char c in text2)
		{
			num ^= c;
			num *= 16777619;
		}
		return num;
	}

	private Vector3 Method_114(Vector3 arg0)
	{
		uint num = Method_113();
		if (num == 0)
		{
			return Vector3.Zero;
		}
		float x = (float)(num % 3600) / 3600f * (float)Math.PI * 2f;
		float num2 = 2f + (float)((num >> 8) % 4);
		return new Vector3(MathF.Cos(x) * num2, 0f, MathF.Sin(x) * num2);
	}

	public Vector3? GetLeaderPosition()
	{
		if (string.IsNullOrWhiteSpace(Config.LeaderName))
		{
			return null;
		}
		string[] array = Method_95(Config.LeaderName).Split(' ', StringSplitOptions.RemoveEmptyEntries);
		foreach (string b in array)
		{
			foreach (IGameObject item in ObjectTable)
			{
				if (item?.Name != null && string.Equals(Method_95(item.Name.TextValue), b, StringComparison.OrdinalIgnoreCase))
				{
					return item.Position;
				}
			}
		}
		return null;
	}

	private static uint Method_116()
	{
		return ClientState.TerritoryType;
	}

	public static string GetCurrentTerritoryName()
	{
		uint num = Method_116();
		if (num == 0)
		{
			return string.Empty;
		}
		ExcelSheet<TerritoryType> excelSheet = DataManager.GetExcelSheet<TerritoryType>();
		if (excelSheet == null)
		{
			return string.Empty;
		}
		PlaceName? valueNullable = excelSheet.GetRow(num).PlaceName.ValueNullable;
		if (valueNullable.HasValue)
		{
			return valueNullable.Value.Name.ExtractText();
		}
		return string.Empty;
	}

	private Vector3? Method_118(Vector3 arg0)
	{
		if (field_26 == null)
		{
			return null;
		}
		try
		{
			return field_26.InvokeFunc(arg0, 120f, 300f) ?? field_26.InvokeFunc(arg0, 180f, 600f) ?? field_26.InvokeFunc(arg0, 260f, 1000f);
		}
		catch (Exception exception)
		{
			Log.Warning(exception, "AutomaticHunting: Failed to snap position to navmesh");
			return null;
		}
	}

	private unsafe bool Method_119(uint arg0)
	{
		Telepo* ptr = Telepo.Instance();
		if (ptr != null)
		{
			return ptr->Teleport(arg0, 0);
		}
		return false;
	}

	private unsafe static bool Method_120()
	{
		ActionManager* ptr = ActionManager.Instance();
		if (ptr == null)
		{
			return false;
		}
		if (ptr->GetActionStatus(ActionType.Action, 5u, 3758096384uL, checkRecastActive: true, checkCastingActive: true, null) != 0)
		{
			return false;
		}
		BattleChara* localPlayer = Control.GetLocalPlayer();
		if (localPlayer != null)
		{
			return !localPlayer->IsCasting;
		}
		return false;
	}

	private unsafe static bool Method_121()
	{
		if (Condition[ConditionFlag.Casting])
		{
			return true;
		}
		BattleChara* localPlayer = Control.GetLocalPlayer();
		if (localPlayer != null)
		{
			return localPlayer->IsCasting;
		}
		return false;
	}

	private bool Method_122()
	{
		if (!IsPluginLoaded("Lifestream"))
		{
			return false;
		}
		try
		{
			return PluginInterface.GetIpcSubscriber<bool>("Lifestream.IsBusy").InvokeFunc();
		}
		catch (Exception exception)
		{
			Log.Warning(exception, "AutomaticHunting: Unable to query Lifestream busy state");
			return true;
		}
	}

	private async Task<bool> Method_123(uint arg0, CancellationToken arg1)
	{
		await field_77.WaitAsync(arg1);
		try
		{
			return await Method_124(arg0, arg1);
		}
		finally
		{
			field_77.Release();
		}
	}

	private async Task<bool> Method_124(uint arg0, CancellationToken arg1)
	{
		await Framework.RunOnFrameworkThread(() =>
		{
			Method_179("/vnav stop");
		});
		DateTime field_183;
		DateTime dateTime = (field_183 = DateTime.UtcNow);
		DateTime dateTime2 = dateTime.AddSeconds(10.0);
		Vector3? field_184 = null;
		int field_185 = 0;
		int field_186 = 0;
		DateTime? field_187 = null;
		DateTime? field_188 = null;
		while (DateTime.UtcNow - dateTime < TimeSpan.FromSeconds(60L))
		{
			arg1.ThrowIfCancellationRequested();
			(bool, bool) tuple = await Framework.RunOnFrameworkThread(() =>
			{
				arg1.ThrowIfCancellationRequested();
				if (Condition[ConditionFlag.BetweenAreas] || Condition[ConditionFlag.BetweenAreas51])
				{
					return (Between: field_186 > 0, Foreign: field_186 == 0);
				}
				if (Method_121())
				{
					DateTime valueOrDefault = field_187.GetValueOrDefault();
					if (!field_187.HasValue)
					{
						valueOrDefault = DateTime.UtcNow;
						field_187 = valueOrDefault;
					}
					field_188 = null;
					return (Between: false, Foreign: false);
				}
				if (field_187.HasValue)
				{
					DateTime valueOrDefault = field_188.GetValueOrDefault();
					if (!field_188.HasValue)
					{
						valueOrDefault = DateTime.UtcNow;
						field_188 = valueOrDefault;
					}
					if (DateTime.UtcNow - field_188.Value < TimeSpan.FromSeconds(2L))
					{
						return (Between: false, Foreign: false);
					}
				}
				Vector3? localPlayerPosition = GetLocalPlayerPosition();
				bool num = localPlayerPosition.HasValue && field_184.HasValue && Vector3.DistanceSquared(localPlayerPosition.Value, field_184.Value) < 0.0001f;
				field_184 = localPlayerPosition;
				if (!num || Method_133() || Method_121() || Condition[ConditionFlag.MountOrOrnamentTransition] || DateTime.UtcNow < field_183 || Method_122() || !Method_120())
				{
					return (Between: false, Foreign: false);
				}
				field_185++;
				bool flag = Method_119(arg0);
				if (flag)
				{
					field_186++;
				}
				field_183 = DateTime.UtcNow.AddSeconds((!flag) ? 1 : 2);
				return (Between: false, Foreign: false);
			});
			if (tuple.Item1)
			{
				return true;
			}
			if (tuple.Item2)
			{
				await Task.Delay(100, arg1);
				continue;
			}
			if (DateTime.UtcNow >= dateTime2)
			{
				PrintEcho($"等待传送开始(id={arg0}，已尝试{field_185}次)...");
				dateTime2 = DateTime.UtcNow.AddSeconds(10.0);
			}
			await Task.Delay(100, arg1);
		}
		PrintEcho($"传送失败: 60秒内未进入过图(id={arg0})");
		return false;
	}

	private bool Method_125(uint arg0, uint arg1, float arg2, float arg3, out Vector3 arg4)
	{
		arg4 = default;
		if (arg0 == 0)
		{
			return false;
		}
		ExcelSheet<Lumina.Excel.Sheets.Map> excelSheet = DataManager.GetExcelSheet<Lumina.Excel.Sheets.Map>();
		if (excelSheet == null)
		{
			return false;
		}
		if (!excelSheet.TryGetRow(arg1, out var row) || row.SizeFactor <= 0)
		{
			row = excelSheet.FirstOrDefault((Lumina.Excel.Sheets.Map map) => map.TerritoryType.RowId == arg0);
			if (row.RowId == 0 || row.SizeFactor <= 0)
			{
				return false;
			}
		}
		arg4 = new Vector3(50f * arg2 - (float)row.OffsetX - 102400f / (float)(int)row.SizeFactor - 50f, 0f, 50f * arg3 - (float)row.OffsetY - 102400f / (float)(int)row.SizeFactor - 50f);
		return true;
	}

	private unsafe static bool Method_126(uint arg0, uint arg1, Vector3 arg2)
	{
		try
		{
			AgentMap* ptr = AgentMap.Instance();
			if (ptr == null)
			{
				return false;
			}
			ptr->SetFlagMapMarker(arg0, arg1, arg2);
			return true;
		}
		catch (Exception exception)
		{
			Log.Warning(exception, "AutomaticHunting: Failed to set map flag");
			return false;
		}
	}

	internal static List<(uint Id, Vector3 Position, string Name)> Method_127(uint arg0)
	{
		List<(uint, Vector3, string)> list = new List<(uint, Vector3, string)>();
		foreach (Aetheryte item2 in DataManager.GetExcelSheet<Aetheryte>())
		{
			if (item2.IsAetheryte && item2.Territory.RowId == arg0 && Method_128(item2, out var arg1))
			{
				string item = item2.PlaceName.ValueNullable?.Name.ExtractText() ?? string.Empty;
				list.Add((item2.RowId, arg1, item));
			}
		}
		return list;
	}

	private static bool Method_128(Aetheryte arg0, out Vector3 arg1)
	{
		arg1 = default;
		ExcelSheet<Lumina.Excel.Sheets.Map> excelSheet = DataManager.GetExcelSheet<Lumina.Excel.Sheets.Map>();
		if (excelSheet == null)
		{
			return false;
		}
		Lumina.Excel.Sheets.Map map = excelSheet.FirstOrDefault((Lumina.Excel.Sheets.Map map2) => map2.TerritoryType.RowId == arg0.Territory.RowId);
		if (map.RowId == 0 || map.SizeFactor <= 0)
		{
			return false;
		}
		SubrowExcelSheet<MapMarker> subrowExcelSheet = DataManager.GetSubrowExcelSheet<MapMarker>();
		if (subrowExcelSheet == null)
		{
			return false;
		}
		foreach (SubrowCollection<MapMarker> item in subrowExcelSheet)
		{
			foreach (MapMarker item2 in item)
			{
				if (item2.DataType == 3 && item2.DataKey.RowId == arg0.RowId)
				{
					arg1 = new Vector3(Method_129(item2.X, (int)map.SizeFactor), 0f, Method_129(item2.Y, (int)map.SizeFactor));
					return true;
				}
			}
		}
		return false;
	}

	private static float Method_129(int arg0, float arg1)
	{
		return ((float)arg0 - 1024f) / (arg1 / 100f);
	}

	public void PrintEcho(string arg0)
	{
		Log.Information(arg0);
		if (Config.EchoMessages)
		{
			ChatGui.Print(new XivChatEntry
			{
				Type = XivChatType.Echo,
				Message = new SeStringBuilder().AddUiForeground("[自动狩猎] ", 37).AddText(arg0).Build()
			});
		}
	}

	public static void PrintEchoAlways(string arg0)
	{
		Log.Information(arg0);
		ChatGui.Print(new XivChatEntry
		{
			Type = XivChatType.Echo,
			Message = new SeStringBuilder().AddUiForeground("[自动狩猎] ", 37).AddText(arg0).Build()
		});
	}

	private static bool Method_132()
	{
		uint num = Method_116();
		if (num == 0)
		{
			return false;
		}
		ExcelSheet<TerritoryType> excelSheet = DataManager.GetExcelSheet<TerritoryType>();
		if (excelSheet == null)
		{
			return false;
		}
		return excelSheet.GetRow(num).TerritoryIntendedUse.RowId == 1;
	}

	private static bool Method_133()
	{
		return Condition.Any(ConditionFlag.BetweenAreas, ConditionFlag.BetweenAreas51, ConditionFlag.Occupied, ConditionFlag.Occupied30, ConditionFlag.Occupied33, ConditionFlag.Occupied38, ConditionFlag.Occupied39, ConditionFlag.InCombat);
	}

	public static bool IsPluginLoaded(string arg0)
	{
		return PluginInterface.InstalledPlugins.Any((IExposedPlugin exposedPlugin) => exposedPlugin.InternalName == arg0 && exposedPlugin.IsLoaded);
	}

	private Lumina.Excel.Sheets.Map? Method_135()
	{
		uint field_190 = ClientState.TerritoryType;
		if (field_190 == 0)
		{
			return null;
		}
		ExcelSheet<Lumina.Excel.Sheets.Map> excelSheet = DataManager.GetExcelSheet<Lumina.Excel.Sheets.Map>();
		if (excelSheet == null)
		{
			return null;
		}
		Lumina.Excel.Sheets.Map value = excelSheet.FirstOrDefault((Lumina.Excel.Sheets.Map arg0) => arg0.TerritoryType.RowId == field_190);
		if (value.RowId == 0 || value.SizeFactor <= 0)
		{
			return null;
		}
		return value;
	}

	private static Vector2 Method_136(float arg0, float arg1, Lumina.Excel.Sheets.Map arg2)
	{
		float num = (float)(int)arg2.SizeFactor / 100f;
		float x = ((arg0 - 1f) * num / 41f * 2048f - 1024f) / num - (float)arg2.OffsetX;
		float y = ((arg1 - 1f) * num / 41f * 2048f - 1024f) / num - (float)arg2.OffsetY;
		return new Vector2(x, y);
	}

	public void AdjustHuntChainKills(int arg0)
	{
		if (CanAdjustHuntChainKills && (arg0 == 1 || arg0 == -1))
		{
			field_81 = (int)Math.Clamp((long)field_81 + (long)arg0, 0L, 2147483647L);
		}
	}

	public string GetHuntChainStatusText(bool arg0 = false)
	{
		string text = (arg0 ? string.Empty : "连车: ");
		List<HuntChainStep> list = field_79;
		if (field_80 < 0 || field_80 >= list.Count)
		{
			return text + ((field_78 == RecoveredType_9.field_126) ? "已完成全部站点" : "未运行");
		}
		string text2 = $"第 {field_80 + 1}/{list.Count} 站";
		int killTarget = list[field_80].KillTarget;
		return field_78 switch
		{
			RecoveredType_9.field_124 => text + text2 + " · 前往中", 
			RecoveredType_9.field_125 => arg0 ? $"{text}{text2} · {field_81}/{killTarget}" : $"{text}{text2} · 击杀 {field_81}/{killTarget}", 
			RecoveredType_9.field_126 => text + "已完成全部站点", 
			_ => text + "未运行", 
		};
	}

	public bool StartHuntChain(IReadOnlyList<HuntChainStep>? arg0 = null)
	{
		if (!Config.AutomationEnabled)
		{
			PrintEcho("连车: 请先开启自动狩猎");
			return false;
		}
		if (IsHuntChainRunning)
		{
			PrintEcho("连车: 正在运行中");
			return false;
		}
		IReadOnlyList<HuntChainStep> readOnlyList = arg0 ?? Config.HuntChain.Steps;
		if (readOnlyList.Count == 0)
		{
			PrintEcho("连车: 请先添加连车步骤");
			return false;
		}
		string currentDcName = WorldTravelData.GetCurrentDcName();
		if (string.IsNullOrEmpty(currentDcName))
		{
			PrintEcho("连车: 无法识别当前大区");
			return false;
		}
		for (int i = 0; i < readOnlyList.Count; i++)
		{
			HuntChainStep huntChainStep = readOnlyList[i];
			if (huntChainStep.KillTarget < 0)
			{
				PrintEcho($"连车: 第 {i + 1} 站击杀数不能小于 0");
				return false;
			}
			if (huntChainStep.KillTarget != 0)
			{
				if (string.IsNullOrWhiteSpace(huntChainStep.Preset.ServerName) || WorldTravelData.GetDcOfServer(huntChainStep.Preset.ServerName) != currentDcName)
				{
					PrintEcho($"连车: 第 {i + 1} 站不在当前大区({currentDcName})，请重新选择");
					return false;
				}
				if (!CrossWorldData.ServerWorldTravel.ContainsKey(huntChainStep.Preset.ServerName))
				{
					PrintEcho($"连车: 第 {i + 1} 站服务器[{huntChainStep.Preset.ServerName}]无跨服命令");
					return false;
				}
				if (!Method_153(huntChainStep.Preset, out var _, out var _, out var _))
				{
					PrintEcho($"连车: 第 {i + 1} 站配置已失效，请重新选择");
					return false;
				}
				if (!Config.ARealmMarks.ContainsKey(huntChainStep.Preset.MapName))
				{
					PrintEcho($"连车: 第 {i + 1} 站地图[{huntChainStep.Preset.MapName}]未配置A怪名单，无法计数");
					return false;
				}
			}
		}
		field_79 = readOnlyList.Select(Method_143).ToList();
		field_85 = currentDcName;
		field_80 = 0;
		field_81 = 0;
		field_82 = false;
		field_83 = DateTime.MinValue;
		field_84 = DateTime.MinValue;
		field_78 = RecoveredType_9.field_124;
		PrintEcho($"连车: 已开始，共 {field_79.Count} 站");
		Method_152();
		return true;
	}

	private static HuntChainStep Method_143(HuntChainStep arg0)
	{
		return new HuntChainStep
		{
			Preset = new WorldTravelPreset
			{
				ServerName = arg0.Preset.ServerName,
				ExVersion = arg0.Preset.ExVersion,
				MapRowId = arg0.Preset.MapRowId,
				MapName = arg0.Preset.MapName,
				AetheryteId = arg0.Preset.AetheryteId,
				AetheryteName = arg0.Preset.AetheryteName
			},
			KillTarget = arg0.KillTarget
		};
	}

	public void StopHuntChain(string? arg0 = null)
	{
		bool flag = field_82;
		Method_145();
		if (!string.IsNullOrEmpty(arg0))
		{
			PrintEcho(arg0);
		}
		if (flag)
		{
			StopCrossWorld();
		}
	}

	private void Method_145()
	{
		field_78 = RecoveredType_9.field_123;
		field_79.Clear();
		field_80 = -1;
		field_81 = 0;
		field_82 = false;
		field_83 = DateTime.MinValue;
		field_84 = DateTime.MinValue;
		field_85 = string.Empty;
	}

	private void Method_146()
	{
		Method_149();
		RecoveredType_9 recoveredType_ = field_78;
		if ((recoveredType_ == RecoveredType_9.field_123 || recoveredType_ == RecoveredType_9.field_126) ? true : false)
		{
			return;
		}
		if (!Config.AutomationEnabled)
		{
			StopHuntChain("连车: 自动狩猎已关闭，连车停止");
			return;
		}
		if (!string.IsNullOrEmpty(field_85))
		{
			string currentDcName = WorldTravelData.GetCurrentDcName();
			if (!string.IsNullOrEmpty(currentDcName) && currentDcName != field_85)
			{
				StopHuntChain($"连车: 大区已变化({field_85}→{currentDcName})，连车停止");
				return;
			}
		}
		List<HuntChainStep> list = field_79;
		if (field_80 < 0 || field_80 >= list.Count)
		{
			StopHuntChain("连车: 步骤数据异常，连车停止");
		}
		else if (field_78 == RecoveredType_9.field_125)
		{
			Method_150(list[field_80]);
		}
		else
		{
			Method_151(list[field_80]);
		}
	}

	private void Method_147()
	{
		if (field_78 == RecoveredType_9.field_125)
		{
			field_81++;
		}
	}

	private void Method_148()
	{
		if (Config.HuntChain.CreatePfAfterTravel)
		{
			field_84 = DateTime.UtcNow.AddSeconds(1.0);
		}
	}

	private void Method_149()
	{
		if (!(field_84 == DateTime.MinValue) && !(DateTime.UtcNow < field_84))
		{
			field_84 = DateTime.MinValue;
			if (field_78 != RecoveredType_9.field_123 && Config.AutomationEnabled && Config.HuntChain.CreatePfAfterTravel)
			{
				PrintEcho("连车: 已到达站点，创建狩猎招募");
				HuntPfController.Create(Config.PfComment);
			}
		}
	}

	private void Method_150(HuntChainStep arg0)
	{
		if (field_81 >= arg0.KillTarget)
		{
			List<HuntChainStep> list = field_79;
			if (field_80 + 1 >= list.Count)
			{
				field_78 = RecoveredType_9.field_126;
				field_82 = false;
				PrintEcho("连车: 最后一站击杀达标，全部完成");
				return;
			}
			field_80++;
			PrintEcho($"连车: 第 {field_80} 站达标，前往第 {field_80 + 1} 站");
			Method_152();
		}
	}

	private void Method_151(HuntChainStep arg0)
	{
		if (!Method_153(arg0.Preset, out var arg1, out var arg2, out var _))
		{
			StopHuntChain($"连车: 第 {field_80 + 1} 站配置已失效，连车停止");
		}
		else if (Method_154(arg0.Preset, arg1))
		{
			if (!CrossWorldInProgress && !Condition[ConditionFlag.BetweenAreas] && !Condition[ConditionFlag.BetweenAreas51])
			{
				field_78 = RecoveredType_9.field_125;
				field_81 = 0;
				field_82 = false;
				field_83 = DateTime.MinValue;
				PrintEcho($"连车: 已到达第 {field_80 + 1} 站 {arg0.Preset.ServerName} {arg0.Preset.MapName}，开始计数");
				Method_148();
			}
		}
		else if (field_82)
		{
			if (DateTime.UtcNow > field_83)
			{
				StopHuntChain("连车: 前往下一站超时，连车停止");
			}
		}
		else if (!Condition[ConditionFlag.InCombat] && !CrossWorldInProgress)
		{
			Method_153(arg0.Preset, out arg2, out var arg4, out var arg5);
			field_82 = true;
			field_83 = DateTime.UtcNow.AddSeconds(300.0);
			StartManualWorldTravel(arg0.Preset.ServerName, arg1, arg4, arg5, arg4: true);
		}
	}

	private void Method_152()
	{
		field_81 = 0;
		field_82 = false;
		field_83 = DateTime.MinValue;
		field_78 = RecoveredType_9.field_124;
		List<HuntChainStep> list = field_79;
		if (field_80 < 0 || field_80 >= list.Count)
		{
			StopHuntChain("连车: 步骤数据异常，连车停止");
			return;
		}
		while (field_80 < list.Count && list[field_80].KillTarget == 0)
		{
			PrintEcho($"连车: 第 {field_80 + 1} 站数量为 0，已跳过");
			field_80++;
		}
		if (field_80 >= list.Count)
		{
			field_78 = RecoveredType_9.field_126;
			PrintEcho("连车: 已无需要执行的站点，全部完成");
			return;
		}
		HuntChainStep huntChainStep = list[field_80];
		if (Method_153(huntChainStep.Preset, out var arg, out var _, out var _) && Method_154(huntChainStep.Preset, arg) && !CrossWorldInProgress)
		{
			field_78 = RecoveredType_9.field_125;
			PrintEcho($"连车: 第 {field_80 + 1} 站已在目标地图，开始计数");
			Method_148();
		}
		else
		{
			PrintEcho($"连车: 前往第 {field_80 + 1} 站 {huntChainStep.Preset.ServerName} {huntChainStep.Preset.MapName} · {huntChainStep.Preset.AetheryteName}");
		}
	}

	private static bool Method_153(WorldTravelPreset arg0, out uint arg1, out uint arg2, out Vector3 arg3)
	{
		arg1 = 0u;
		arg2 = 0u;
		arg3 = default;
		WorldTravelData.MapEntry arg4 = WorldTravelData.GetMaps(arg0.ExVersion).FirstOrDefault((WorldTravelData.MapEntry mapEntry) => mapEntry.RowId == arg0.MapRowId);
		if (arg4.Territory == 0)
		{
			return false;
		}
		(uint, Vector3, string) tuple = WorldTravelData.GetAetherytes(arg4).FirstOrDefault(((uint Id, Vector3 Position, string Name) tuple3) => tuple3.Id == arg0.AetheryteId);
		if (tuple.Item1 == 0)
		{
			return false;
		}
		arg1 = arg4.Territory;
		(arg2, arg3, _) = tuple;
		return true;
	}

	private bool Method_154(WorldTravelPreset arg0, uint arg1)
	{
		if (ClientState.TerritoryType != arg1)
		{
			return false;
		}
		string text = Method_155();
		if (text.Length != 0)
		{
			return text == arg0.ServerName;
		}
		return true;
	}

	private string Method_155()
	{
		try
		{
			IPlayerCharacter localPlayer = ObjectTable.LocalPlayer;
			if (localPlayer != null)
			{
				World? world = DataManager.GetExcelSheet<World>()?.GetRow(localPlayer.CurrentWorld.RowId);
				if (world.HasValue)
				{
					return world.Value.Name.ExtractText();
				}
			}
		}
		catch (Exception exception)
		{
			Log.Warning(exception, "AutomaticHunting: 读取当前服务器名失败");
		}
		return string.Empty;
	}

	private void Method_156(uint arg0, Vector3 arg1)
	{
		if (!Config.AutomationEnabled)
		{
			PrintEcho("自动狩猎未启用，无法导航");
			return;
		}
		RecoveredType_3 recoveredType_ = new RecoveredType_3(arg0, arg1);
		if (Condition[ConditionFlag.InCombat])
		{
			field_32 = recoveredType_;
			field_34 = false;
			PrintEcho("战斗中，已保存最新Flag，战斗结束后执行");
		}
		else if (field_33)
		{
			field_32 = recoveredType_;
			field_34 = true;
			PrintEcho("S怪监控中，已保存最新Flag，S怪结束后执行");
		}
		else
		{
			Method_157(recoveredType_);
		}
	}

	private void Method_157(RecoveredType_3 arg0)
	{
		CancellationTokenSource field_195 = new CancellationTokenSource();
		lock (field_53)
		{
			CancellationTokenSource? cancellationTokenSource = field_54;
			Task field_196 = field_55;
			field_54 = field_195;
			field_56 = RecoveredType_8.field_116;
			cancellationTokenSource?.Cancel();
			field_55 = Task.Run(async () =>
			{
				if (field_196 != null)
				{
					try
					{
						await field_196;
					}
					catch
					{
					}
				}
				await Method_158(arg0, field_195);
			});
		}
	}

	private async Task Method_158(RecoveredType_3 arg0, CancellationTokenSource arg1)
	{
		_ = 4;
		try
		{
			CancellationToken token = arg1.Token;
			switch (await Method_159(arg0, token))
			{
			case RecoveredType_10.field_130:
				return;
			case RecoveredType_10.field_129:
				Method_196(RecoveredType_8.field_117, arg1);
				if (!(await Method_163(arg0.TerritoryType, token)))
				{
					return;
				}
				break;
			}
			token.ThrowIfCancellationRequested();
			Method_196(RecoveredType_8.field_118, arg1);
			if (!(await Method_182(token)))
			{
				PrintEcho("召唤坐骑失败，导航已取消");
				return;
			}
			await Task.Delay(100, token);
			await Method_164(arg0, token, arg1);
		}
		catch (OperationCanceledException)
		{
			Log.Information("AutomaticHunting: Navigation cancelled");
			FinishSMonitor("已停止");
		}
		catch (Exception ex2)
		{
			Log.Warning(ex2, "AutomaticHunting: Navigation workflow failed");
			PrintEcho("导航异常: " + ex2.Message);
			FinishSMonitor("导航异常");
		}
		finally
		{
			lock (field_53)
			{
				if (field_54 == arg1)
				{
					field_54 = null;
					field_55 = null;
					field_56 = RecoveredType_8.field_116;
				}
			}
			arg1.Dispose();
		}
	}

	private async Task<RecoveredType_10> Method_159(RecoveredType_3 arg0, CancellationToken arg1)
	{
		(RecoveredType_10 Preparation, uint AetheryteId, bool SameTerritory) tuple = await Framework.RunOnFrameworkThread(() => (Preparation: Method_160(arg0, out var arg2), AetheryteId: arg2, SameTerritory: Method_116() == arg0.TerritoryType));
		if (tuple.Preparation != RecoveredType_10.field_129)
		{
			return tuple.Preparation;
		}
		if (await Method_123(tuple.AetheryteId, arg1))
		{
			return RecoveredType_10.field_129;
		}
		if (tuple.SameTerritory)
		{
			PrintEcho("无法传送，改用直接导航");
			return RecoveredType_10.field_128;
		}
		PrintEcho("跨图传送未开始，导航已取消");
		return RecoveredType_10.field_130;
	}

	private RecoveredType_10 Method_160(RecoveredType_3 arg0, out uint arg1)
	{
		arg1 = 0u;
		uint num = Method_116();
		Vector3? localPlayerPosition = GetLocalPlayerPosition();
		List<(uint, Vector3, string)> list = (field_31 = Method_127(arg0.TerritoryType));
		if (list.Count == 0)
		{
			if (num != arg0.TerritoryType)
			{
				PrintEcho("目标区域没有可用水晶，导航已取消");
				return RecoveredType_10.field_130;
			}
			return RecoveredType_10.field_128;
		}
		(uint, Vector3, string) item = (from tuple3 in list.Select(((uint Id, Vector3 Position, string Name) item2) => (Aetheryte: item2, Corrected: Method_161(item2.Id, item2.Position, arg0.Position)))
			orderby tuple3.Corrected
			select tuple3).First().Aetheryte;
		item = Method_162(item, list);
		if (num != arg0.TerritoryType)
		{
			(arg1, _, _) = item;
			PrintEcho("传送到 " + item.Item3 + " (距标点最近，修正后)");
			return RecoveredType_10.field_129;
		}
		if (!localPlayerPosition.HasValue)
		{
			return RecoveredType_10.field_130;
		}
		float num2 = Method_197(localPlayerPosition.Value, arg0.Position);
		float num3 = Method_161(item.Item1, item.Item2, arg0.Position);
		if (num2 - num3 <= Config.TeleportThreshold || string.IsNullOrWhiteSpace(item.Item3))
		{
			return RecoveredType_10.field_128;
		}
		(arg1, _, _) = item;
		PrintEcho($"传送更快: 玩家到Flag {num2:0.0}m，水晶到Flag {num3:0.0}m");
		return RecoveredType_10.field_129;
	}

	private float Method_161(uint arg0, Vector3 arg1, Vector3 arg2)
	{
		float num = Method_197(arg1, arg2);
		if (arg0 == Config.SpecialSmallAetheryteId)
		{
			return num + 400f;
		}
		return num;
	}

	private (uint Id, Vector3 Position, string Name) Method_162((uint Id, Vector3 Position, string Name) arg0, List<(uint Id, Vector3 Position, string Name)> arg1)
	{
		if (!Config.BlacklistedAetheryteIds.Contains(arg0.Id))
		{
			return arg0;
		}
		if (!Config.BlacklistReplacements.TryGetValue(arg0.Id, out var field_204))
		{
			return arg0;
		}
		(uint, Vector3, string) result = arg1.FirstOrDefault(((uint Id, Vector3 Position, string Name) tuple) => tuple.Id == field_204);
		if (result.Item1 == 0)
		{
			return arg0;
		}
		PrintEcho($"水晶{arg0.Id}已拉黑，替换为{result.Item3}");
		return result;
	}

	private async Task<bool> Method_163(uint arg0, CancellationToken arg1)
	{
		PrintEcho("等待传送完成...");
		DateTime utcNow = DateTime.UtcNow;
		while (DateTime.UtcNow - utcNow < TimeSpan.FromSeconds(45L))
		{
			await Task.Delay(500, arg1);
			bool flag = !(await Framework.RunOnFrameworkThread(() => Condition[ConditionFlag.BetweenAreas] || Condition[ConditionFlag.BetweenAreas51]));
			if (flag)
			{
				flag = await Framework.RunOnFrameworkThread(() => Method_116() == arg0 && GetLocalPlayerPosition().HasValue);
			}
			if (!flag)
			{
				continue;
			}
			PrintEcho($"传送读条结束，等待{Config.WaitAfterTeleport:0}秒状态稳定...");
			await Task.Delay((int)(Config.WaitAfterTeleport * 1000f), arg1);
			for (int num = 0; num < 30; num++)
			{
				if (await Framework.RunOnFrameworkThread(() => !Method_133() && !Condition[ConditionFlag.Casting] && Method_116() == arg0 && GetLocalPlayerPosition().HasValue))
				{
					PrintEcho("传送完成");
					return true;
				}
				await Task.Delay(500, arg1);
			}
			PrintEcho("传送后状态未稳定，导航已取消");
			return false;
		}
		PrintEcho("等待传送超时(45s)，导航已取消");
		return false;
	}

	private async Task Method_164(RecoveredType_3 arg0, CancellationToken arg1, CancellationTokenSource arg2)
	{
		bool landingMode = Config.LandingMode;
		Vector3 field_209 = arg0.Position;
		for (int i = 0; i < 2; i++)
		{
			bool flag = i == 1;
			Method_196(flag ? RecoveredType_8.field_120 : RecoveredType_8.field_119, arg2);
			bool flag2 = false;
			for (int j = 1; j <= 2; j++)
			{
				if (flag)
				{
					flag2 = await Framework.RunOnFrameworkThread(() =>
					{
						Vector3 valueOrDefault = Method_118(field_209).GetValueOrDefault(field_209);
						ICallGateSubscriber<bool>? callGateSubscriber = field_24;
						return callGateSubscriber != null && callGateSubscriber.InvokeFunc() && (field_25?.InvokeFunc(valueOrDefault, arg2: true) ?? false);
					});
				}
				else
				{
					if (!(await Method_181(arg1)))
					{
						PrintEcho("启动导航失败: vnavmesh未就绪(等待加载超时)");
						return;
					}
					if (!(await Framework.RunOnFrameworkThread(() => !Config.SetFlagOnNavigation || Method_126(arg0.TerritoryType, 0u, arg0.Position))))
					{
						PrintEcho("设置地图Flag失败");
					}
					await Task.Delay(150, arg1);
					Method_179("/vnav flyflag");
					PrintEcho("开始导航: /vnav flyflag");
					flag2 = true;
				}
				if (flag2)
				{
					break;
				}
				if (j < 2)
				{
					PrintEcho("寻路失败，2秒后重新寻路...");
					await Task.Delay(TimeSpan.FromSeconds(2L), arg1);
				}
			}
			if (!flag2)
			{
				PrintEcho("导航失败: vnavmesh未就绪或无法规划路径");
				return;
			}
			PrintEcho(flag ? "导航已开始(前往A怪真实坐标)" : "导航已开始(前往标点)");
			bool flag3 = false;
			int num = 0;
			for (int j = 0; j < 240; j++)
			{
				await Task.Delay(500, arg1);
				Vector3? vector = await Framework.RunOnFrameworkThread((Func<Vector3?>)GetLocalPlayerPosition);
				if (!vector.HasValue)
				{
					continue;
				}
				float num2 = Method_197(vector.Value, field_209);
				if (!flag && j - num >= 60)
				{
					num = j;
					PrintEcho($"飞向标点中(距标点{num2:0.0}m)");
				}
				if (!flag && num2 < 100f)
				{
					RecoveredType_4? recoveredType_ = await Framework.RunOnFrameworkThread((Func<RecoveredType_4?>)Method_2);
					if (recoveredType_.HasValue)
					{
						Method_179("/vnav stop");
						field_209 = recoveredType_.Value.Position + Method_114(recoveredType_.Value.Position);
						flag3 = true;
						PrintEcho("发现A怪: " + recoveredType_.Value.Name + "，切换到落点(偏移后)");
						break;
					}
					if (Config.SEnabled)
					{
						RecoveredType_5? recoveredType_2 = await Framework.RunOnFrameworkThread((Func<RecoveredType_5?>)Method_202);
						if (recoveredType_2.HasValue)
						{
							Method_179("/vnav stop");
							PrintEcho($"未发现A怪，发现S怪: {recoveredType_2.Value.Name} 血量{recoveredType_2.Value.HpPercent:0.0}%，开始悬停监控");
							await Method_165(recoveredType_2.Value, arg2, arg1);
							return;
						}
					}
					if (num2 < 50f)
					{
						Method_179("/vnav stop");
						PrintEcho("到达标点附近未发现A怪");
						return;
					}
				}
				if (!flag || !(num2 < 3f))
				{
					continue;
				}
				Method_179("/vnav stop");
				string text = await Framework.RunOnFrameworkThread((Func<string>)Method_3);
				if (!string.IsNullOrEmpty(text))
				{
					PrintEcho("已选中A怪: " + text);
				}
				else
				{
					PrintEcho("落点附近未扫描到A怪");
				}
				if (landingMode)
				{
					if (await Method_167(arg1))
					{
						PrintEcho("已下坐骑，开始战斗");
					}
					else
					{
						PrintEcho("下坐骑失败(5s)，仍开始战斗");
					}
				}
				else
				{
					PrintEcho("悬停在A怪上方，保持坐骑");
				}
				return;
			}
			if (!flag && !flag3)
			{
				Method_179("/vnav stop");
				PrintEcho("120秒未接近标点，导航结束");
				return;
			}
		}
		Method_179("/vnav stop");
		PrintEcho("120秒未到达A怪落点，导航结束");
	}

	private async Task Method_165(RecoveredType_5 arg0, CancellationTokenSource arg1, CancellationToken arg2)
	{
		field_33 = true;
		try
		{
			Method_196(RecoveredType_8.field_121, arg1);
			BeginSMonitor(arg0.Name, "前往S怪", arg0.HpPercent, Config.SHpThreshold);
			Vector3 field_213 = new Vector3(arg0.Position.X, arg0.Position.Y + Config.SHoverHeight, arg0.Position.Z);
			bool flag = false;
			for (int i = 1; i <= 2; i++)
			{
				flag = await Framework.RunOnFrameworkThread(() =>
				{
					Vector3? vector2 = Method_118(field_213);
					Vector3 arg3 = (vector2.HasValue ? new Vector3(vector2.Value.X, field_213.Y, vector2.Value.Z) : field_213);
					ICallGateSubscriber<bool>? callGateSubscriber = field_24;
					return callGateSubscriber != null && callGateSubscriber.InvokeFunc() && (field_25?.InvokeFunc(arg3, arg2: true) ?? false);
				});
				if (flag)
				{
					break;
				}
				if (i < 2)
				{
					PrintEcho("S怪寻路失败，2秒后重新寻路...");
					await Task.Delay(TimeSpan.FromSeconds(2L), arg2);
				}
			}
			if (!flag)
			{
				PrintEcho("S怪导航失败: vnavmesh未就绪或无法规划路径");
				FinishSMonitor("寻路失败");
				return;
			}
			PrintEcho($"S怪导航已开始(悬停高度{Config.SHoverHeight:0}m)，悬停期间监控血量...");
			for (int i = 0; i < 120; i++)
			{
				await Task.Delay(500, arg2);
				Vector3? vector = await Framework.RunOnFrameworkThread((Func<Vector3?>)GetLocalPlayerPosition);
				if (!vector.HasValue)
				{
					continue;
				}
				float num = Method_197(vector.Value, arg0.Position);
				RecoveredType_5? field_216 = await Framework.RunOnFrameworkThread((Func<RecoveredType_5?>)Method_202);
				if (!field_216.HasValue)
				{
					continue;
				}
				SMonitorStatus sMonitor = SMonitor;
				if (sMonitor != null)
				{
					sMonitor.HpPercent = field_216.Value.HpPercent;
				}
				if (field_216.Value.HpPercent <= Config.SHpThreshold)
				{
					await Method_166(field_216.Value, field_216.Value.HpPercent, arg2);
					FinishSMonitor("已开怪");
					return;
				}
				if (!(num <= Config.SHoverDistance))
				{
					continue;
				}
				Method_179("/vnav stop");
				string text = await Framework.RunOnFrameworkThread(() => Method_204(field_216.Value));
				PrintEcho($"已悬停到S怪附近({num:0.0}m)，已选中S怪:{text ?? field_216.Value.Name}，当前血量:{field_216.Value.HpPercent:0.0}%，等待血量降至{Config.SHpThreshold:0}%");
				float num2 = -1f;
				float num3 = 0f;
				float num4 = 0f;
				float num5 = 0f;
				bool flag2 = false;
				DateTime dateTime = DateTime.MinValue;
				DateTime dateTime2 = DateTime.MinValue;
				while (true)
				{
					await Task.Delay(500, arg2);
					num4 += 0.5f;
					(int kind, float hp, Vector3 pos, Vector3? player, bool isS) tuple = await Framework.RunOnFrameworkThread(() =>
					{
						Vector3? localPlayerPosition = GetLocalPlayerPosition();
						if (TargetManager.Target is IBattleChara { MaxHp: >0u, CurrentHp: >0u } battleChara)
						{
							return (kind: 1, hp: (float)battleChara.CurrentHp / (float)battleChara.MaxHp * 100f, pos: battleChara.Position, player: localPlayerPosition, isS: Method_203(battleChara.Name.TextValue));
						}
						RecoveredType_5? recoveredType_ = Method_202();
						if (recoveredType_.HasValue)
						{
							if (recoveredType_.Value.HpPercent <= 0f)
							{
								return (kind: 2, hp: 0f, pos: recoveredType_.Value.Position, player: localPlayerPosition, isS: true);
							}
							Method_204(recoveredType_.Value);
							return (kind: 3, hp: recoveredType_.Value.HpPercent, pos: recoveredType_.Value.Position, player: localPlayerPosition, isS: true);
						}
						return (kind: 0, hp: -1f, pos: Vector3.Zero, player: localPlayerPosition, isS: false);
					});
					if (tuple.kind == 2)
					{
						PrintEcho("S怪已死亡，悬停监控结束");
						FinishSMonitor("S怪已死亡");
						return;
					}
					if (tuple.kind == 1 || tuple.kind == 3)
					{
						num5 = 0f;
						if (flag2 && tuple.kind == 3)
						{
							PrintEcho("目标已重新选中: " + arg0.Name);
						}
						flag2 = false;
						float num6 = ((tuple.player.HasValue && tuple.isS) ? Method_197(tuple.player.Value, tuple.pos) : 0f);
						if (num6 > Config.SHoverDistance + 5f && (DateTime.UtcNow - dateTime).TotalSeconds >= 3.0)
						{
							Vector3 field_218 = new Vector3(tuple.pos.X, tuple.pos.Y + Config.SHoverHeight, tuple.pos.Z);
							bool flag3 = await Framework.RunOnFrameworkThread(() =>
							{
								Vector3? vector2 = Method_118(field_218);
								Vector3 arg3 = (vector2.HasValue ? new Vector3(vector2.Value.X, field_218.Y, vector2.Value.Z) : field_218);
								ICallGateSubscriber<bool>? callGateSubscriber = field_24;
								return callGateSubscriber != null && callGateSubscriber.InvokeFunc() && (field_25?.InvokeFunc(arg3, arg2: true) ?? false);
							});
							dateTime = DateTime.UtcNow;
							if (flag3 && (DateTime.UtcNow - dateTime2).TotalSeconds >= 10.0)
							{
								dateTime2 = DateTime.UtcNow;
								PrintEcho($"S怪已移动，距离 {num6:0.0}m，重新悬停跟随");
							}
						}
						float item = tuple.hp;
						SMonitorStatus sMonitor2 = SMonitor;
						if (sMonitor2 != null)
						{
							sMonitor2.Phase = "悬停监控";
							sMonitor2.HpPercent = item;
							sMonitor2.WaitedSeconds = num4;
							sMonitor2.TargetLost = false;
						}
						if (Math.Abs(item - num2) >= 5f)
						{
							num2 = item;
							PrintEcho($"悬停监控中: S怪血量 {item:0.0}%");
						}
						if (item <= Config.SHpThreshold)
						{
							await Method_166(arg0, item, arg2);
							FinishSMonitor("已开怪");
							return;
						}
						if (num4 - num3 >= 60f)
						{
							num3 = num4;
							PrintEcho($"悬停监控中: 已等待{num4:0}秒, S怪血量 {item:0.0}%");
						}
					}
					else
					{
						num5 += 0.5f;
						flag2 = true;
						SMonitorStatus sMonitor3 = SMonitor;
						if (sMonitor3 != null)
						{
							sMonitor3.Phase = "目标丢失";
							sMonitor3.TargetLost = true;
							sMonitor3.LostSeconds = num5;
						}
						if (num5 >= 30f)
						{
							break;
						}
					}
				}
				PrintEcho("目标丢失30秒，悬停监控结束");
				FinishSMonitor("目标丢失30秒");
				return;
			}
			PrintEcho("S怪悬停监控超时");
			FinishSMonitor("悬停导航超时");
		}
		finally
		{
			field_33 = false;
		}
	}

	private async Task Method_166(RecoveredType_5 arg0, float arg1, CancellationToken arg2)
	{
		Method_179("/vnav stop");
		SMonitorStatus sMonitor = SMonitor;
		if (sMonitor != null)
		{
			sMonitor.Phase = "开怪中";
			sMonitor.HpPercent = arg1;
		}
		else
		{
			BeginSMonitor(arg0.Name, "开怪中", arg1, Config.SHpThreshold);
		}
		if (!Config.SPathToEngage)
		{
			PrintEcho($"S怪血量{arg1:0.0}%≤{Config.SHpThreshold:0}%，原地开怪: 下坐骑");
			if (await Method_167(arg2))
			{
				PrintEcho("已下坐骑，执行突进: " + Config.SDashCommand);
			}
			else
			{
				PrintEcho("下坐骑失败(5s)，仍执行突进");
			}
			await Method_168(arg2);
			return;
		}
		string value = ((Method_114(arg0.Position) == Vector3.Zero) ? "" : "（含落点偏移）");
		PrintEcho($"S怪血量{arg1:0.0}%≤{Config.SHpThreshold:0}%，寻路开怪: 前往 {arg0.Name} 坐标{value}");
		bool flag = await Method_169(arg0, arg2);
		Method_179("/vnav stop");
		if (!flag)
		{
			PrintEcho("寻路开怪: 未到达S怪坐标(失败/超时)，原地下坐骑开怪");
		}
		if (await Method_167(arg2))
		{
			PrintEcho("已下坐骑，执行突进: " + Config.SDashCommand);
		}
		else
		{
			PrintEcho("下坐骑失败(5s)，仍执行突进");
		}
		await Method_168(arg2);
	}

	private async Task<bool> Method_167(CancellationToken arg0)
	{
		for (int i = 0; i < 10; i++)
		{
			if (!(await Framework.RunOnFrameworkThread(() => Condition[ConditionFlag.Mounted])))
			{
				return true;
			}
			await Framework.RunOnFrameworkThread((Func<bool>)UseDismount);
			await Task.Delay(500, arg0);
		}
		return !(await Framework.RunOnFrameworkThread(() => Condition[ConditionFlag.Mounted]));
	}

	private async Task Method_168(CancellationToken arg0)
	{
		for (int i = 0; i < 5; i++)
		{
			ExecuteDashSkill();
			await Task.Delay(500, arg0);
		}
	}

	private async Task<bool> Method_169(RecoveredType_5 arg0, CancellationToken arg1)
	{
		Vector3 field_221 = arg0.Position;
		for (int i = 1; i <= 2; i++)
		{
			Vector3? vector = await Method_170(field_221, arg1: true);
			if (!vector.HasValue)
			{
				if (i < 2)
				{
					PrintEcho("寻路开怪: 寻路失败，2秒后重试...");
					await Task.Delay(TimeSpan.FromSeconds(2L), arg1);
				}
				continue;
			}
			Vector3 value = vector.Value;
			DateTime utcNow = DateTime.UtcNow;
			DateTime dateTime = utcNow;
			DateTime dateTime2 = DateTime.MinValue;
			float num = 0f;
			while ((DateTime.UtcNow - utcNow).TotalSeconds < 60.0)
			{
				await Task.Delay(500, arg1);
				(Vector3?, Vector3?) tuple = await Framework.RunOnFrameworkThread(() =>
				{
					Vector3? localPlayerPosition = GetLocalPlayerPosition();
					Vector3? item = (Method_171(field_221, arg1: false, out var arg2) ? new Vector3?(arg2) : ((Vector3?)null));
					return (player: localPlayerPosition, mob: item);
				});
				if (!tuple.Item1.HasValue)
				{
					continue;
				}
				if (!tuple.Item2.HasValue)
				{
					num += 0.5f;
					if (num >= 5f)
					{
						PrintEcho($"寻路开怪: S怪已消失{num:0}秒，中止");
						return false;
					}
					continue;
				}
				num = 0f;
				field_221 = tuple.Item2.Value;
				Vector3 vector2 = tuple.Item2.Value + Method_114(tuple.Item2.Value);
				if (Method_197(tuple.Item1.Value, vector2) <= 3f)
				{
					return true;
				}
				float num2 = Method_197(vector2, value);
				if (num2 <= 4f || (DateTime.UtcNow - dateTime).TotalSeconds < 1.5)
				{
					continue;
				}
				Vector3? vector3 = await Method_170(field_221, arg1: true);
				if (vector3.HasValue)
				{
					value = vector3.Value;
					dateTime = DateTime.UtcNow;
					if (!((DateTime.UtcNow - dateTime2).TotalSeconds < 3.0))
					{
						dateTime2 = DateTime.UtcNow;
						PrintEcho($"寻路开怪: 目标已移动 {num2:0.0}m，重新寻路");
					}
				}
			}
			PrintEcho("寻路开怪: 60秒未到达，重试寻路...");
		}
		return false;
	}

	private async Task<Vector3?> Method_170(Vector3 arg0, bool arg1)
	{
		return await Framework.RunOnFrameworkThread(() =>
		{
			if (!Method_171(arg0, arg1, out var arg2))
			{
				return (Vector3?)null;
			}
			Vector3 vector = arg2 + Method_114(arg2);
			Vector3 arg3 = Method_118(vector) ?? vector;
			ICallGateSubscriber<bool>? callGateSubscriber = field_24;
			if (callGateSubscriber != null && callGateSubscriber.InvokeFunc())
			{
				ICallGateSubscriber<Vector3, bool, bool>? callGateSubscriber2 = field_25;
				if (callGateSubscriber2 != null && callGateSubscriber2.InvokeFunc(arg3, arg2: true))
				{
					return vector;
				}
			}
			return (Vector3?)null;
		});
	}

	private bool Method_171(Vector3 arg0, bool arg1, out Vector3 arg2)
	{
		RecoveredType_5? recoveredType_ = Method_202();
		if (recoveredType_.HasValue)
		{
			arg2 = recoveredType_.Value.Position;
			return true;
		}
		if (TargetManager.Target is IBattleChara { CurrentHp: >0u } battleChara && Method_203(battleChara.Name.TextValue))
		{
			arg2 = battleChara.Position;
			return true;
		}
		arg2 = arg0;
		return arg1;
	}

	public void StartTargetHpMonitor()
	{
		Method_177();
	}

	public void LoadDrPresets()
	{
		PrintEcho("Global 前置：Lifestream " + (IsPluginLoaded("Lifestream") ? "已載入" : "未載入") + "；vnavmesh " + (IsPluginLoaded("vnavmesh") ? "已載入" : "未載入"));
	}

	public void LoadFastInstanceSwitch()
	{
		PrintEcho("Global 切線使用 Lifestream：" + (IsPluginLoaded("Lifestream") ? "已載入；靠近水晶後即可切線" : "請先安裝並啟用 Lifestream"));
	}

	public void SwitchToNextInstance()
	{
		int currentInstanceNumber = GetCurrentInstanceNumber();
		int num = ((currentInstanceNumber > 0) ? currentInstanceNumber : Config.CurrentInstanceNumber) + 1;
		MultiLineMapConfig multiLineMapConfig = Method_58();
		int num2;
		if (multiLineMapConfig != null)
		{
			int instanceCount = multiLineMapConfig.InstanceCount;
			if (instanceCount > 0 && instanceCount <= 9)
			{
				num2 = multiLineMapConfig.InstanceCount;
				goto IL_0043;
			}
		}
		num2 = 9;
		goto IL_0043;
		IL_0043:
		int num3 = num2;
		if (num > num3)
		{
			num = 1;
		}
		Config.CurrentInstanceNumber = num;
		Method_176();
		GlobalCommands.Process($"/ahglobal instance {num}");
		PrintEcho($"正在切换到 {num} 线 (/ahglobal instance {num})");
		Method_65(num);
	}

	private void Method_176()
	{
		PluginInterface.SavePluginConfig(Config);
	}

	private async Task Method_177()
	{
		_ = 1;
		try
		{
			IGameObject target = TargetManager.Target;
			if (target == null || !(target is IBattleChara))
			{
				PrintEcho("未选中目标，请先选中一个怪再监控血量");
				return;
			}
			BeginSMonitor(target.Name.TextValue, "血量监控", Method_206(string.Empty, out var arg) ? arg : (-1f), Config.SHpThreshold);
			PrintEcho($"开始监控当前目标血量(阈值{Config.SHpThreshold:0}%)...");
			float num = -1f;
			DateTime utcNow = DateTime.UtcNow;
			IGameObject target2;
			float arg2;
			while (true)
			{
				await Task.Delay(500);
				SMonitorStatus sMonitor = SMonitor;
				if (sMonitor != null && sMonitor.Finished)
				{
					PrintEcho("监控已停止");
					return;
				}
				target2 = TargetManager.Target;
				if (target2 == null)
				{
					PrintEcho("目标丢失，监控结束");
					FinishSMonitor("目标丢失");
					return;
				}
				if (target2 is IBattleChara { CurrentHp: 0u })
				{
					PrintEcho("目标已死亡，监控结束");
					FinishSMonitor("目标已死亡");
					return;
				}
				if (Method_206(string.Empty, out arg2))
				{
					SMonitorStatus sMonitor2 = SMonitor;
					if (sMonitor2 != null)
					{
						sMonitor2.Phase = "血量监控";
						sMonitor2.HpPercent = arg2;
						sMonitor2.WaitedSeconds = (float)(DateTime.UtcNow - utcNow).TotalSeconds;
					}
					if (Math.Abs(arg2 - num) >= 5f)
					{
						num = arg2;
						PrintEcho($"监控中: 当前目标血量 {arg2:0.0}%");
					}
					if (arg2 <= Config.SHpThreshold)
					{
						break;
					}
				}
			}
			RecoveredType_5 arg3 = new RecoveredType_5(target2.Name.TextValue, target2.Position, arg2);
			PrintEcho($"血量{arg2:0.0}%≤{Config.SHpThreshold:0}%，开始开怪(与S怪流程一致): {Config.SDashCommand}");
			await Method_166(arg3, arg2, CancellationToken.None);
			FinishSMonitor("已开怪");
		}
		catch (Exception exception)
		{
			Log.Warning(exception, "AutomaticHunting: Target HP monitor failed");
			FinishSMonitor("监控异常");
		}
	}

	public void ExecuteDashSkill()
	{
		string field_228 = Config.SDashCommand?.Trim() ?? string.Empty;
		if (string.IsNullOrWhiteSpace(field_228))
		{
			return;
		}
		string field_229 = null;
		string[] array = new string[3] { "/ac", "/action", "/技能" };
		foreach (string text in array)
		{
			if (field_228.StartsWith(text, StringComparison.OrdinalIgnoreCase) && (field_228.Length == text.Length || char.IsWhiteSpace(field_228[text.Length])))
			{
				field_229 = field_228.Substring(text.Length).Trim().Trim('"');
				break;
			}
		}
		Framework.RunOnFrameworkThread(() =>
		{
			try
			{
				if (field_229 == null || !Method_180(field_229))
				{
					string content = ((!field_228.StartsWith('/')) ? ("/" + field_228) : field_228);
					GlobalCommands.Process(content);
				}
			}
			catch (Exception exception)
			{
				Log.Warning(exception, "AutomaticHunting: 执行突进技能失败 " + field_228);
			}
		});
	}

	private void Method_179(string arg0)
	{
		Framework.RunOnFrameworkThread(() =>
		{
			try
			{
				GlobalCommands.Process(arg0);
			}
			catch (Exception exception)
			{
				Log.Warning(exception, "AutomaticHunting: 执行命令失败 " + arg0);
			}
		});
	}

	private unsafe static bool Method_180(string arg0)
	{
		try
		{
			uint value;
			lock (field_96)
			{
				if (!field_95.TryGetValue(arg0, out value))
				{
					ExcelSheet<Lumina.Excel.Sheets.Action> excelSheet = DataManager.GetExcelSheet<Lumina.Excel.Sheets.Action>();
					if (excelSheet == null)
					{
						return false;
					}
					Lumina.Excel.Sheets.Action action = excelSheet.FirstOrDefault((Lumina.Excel.Sheets.Action action2) => string.Equals(action2.Name.ExtractText(), arg0, StringComparison.OrdinalIgnoreCase));
					if (action.RowId == 0)
					{
						Log.Warning("AutomaticHunting: 未找到技能 " + arg0);
						return false;
					}
					value = action.RowId;
					field_95[arg0] = value;
				}
			}
			ActionManager* ptr = ActionManager.Instance();
			if (ptr == null)
			{
				return false;
			}
			ulong num = TargetManager.Target?.GameObjectId ?? 0;
			bool flag = ptr->UseAction(ActionType.Action, value, num, 0u, ActionManager.UseActionMode.None, 0u, null);
			Log.Information($"AutomaticHunting: 使用技能 {arg0} (id={value}, target={num}) => {flag}");
			return flag;
		}
		catch (Exception exception)
		{
			Log.Warning(exception, "AutomaticHunting: 查找或使用技能失败 " + arg0);
		}
		return false;
	}

	private async Task<bool> Method_181(CancellationToken arg0)
	{
		for (int i = 0; i < 60; i++)
		{
			if (await Framework.RunOnFrameworkThread(() => field_24?.InvokeFunc() ?? false))
			{
				return true;
			}
			await Task.Delay(500, arg0);
		}
		return false;
	}

	private async Task<bool> Method_182(CancellationToken arg0)
	{
		if (await Framework.RunOnFrameworkThread(() => Condition[ConditionFlag.Mounted]))
		{
			return true;
		}
		bool flag = false;
		for (int num = 0; num < 30; num++)
		{
			if (!(await Framework.RunOnFrameworkThread(() => Method_133())))
			{
				break;
			}
			flag = true;
			await Task.Delay(500, arg0);
		}
		if (flag)
		{
			await Task.Delay(1000, arg0);
		}
		if (!(await Framework.RunOnFrameworkThread((Func<bool>)Method_184)))
		{
			PrintEcho("等待区域加载/动作就绪...");
			DateTime utcNow = DateTime.UtcNow;
			while (DateTime.UtcNow - utcNow < TimeSpan.FromSeconds(5L))
			{
				await Task.Delay(200, arg0);
				if (await Framework.RunOnFrameworkThread((Func<bool>)Method_184))
				{
					break;
				}
			}
		}
		PrintEcho("召唤坐骑...");
		for (int num = 1; num <= 5; num++)
		{
			if (await Framework.RunOnFrameworkThread(() => (Config.SelectedMountId != 0) ? Method_198(Config.SelectedMountId) : Method_199()))
			{
				await Task.Delay(100, arg0);
				if (await Framework.RunOnFrameworkThread((Func<bool>)Method_183))
				{
					for (int num2 = 0; num2 < 30; num2++)
					{
						if (await Framework.RunOnFrameworkThread(() => Condition[ConditionFlag.Mounted]))
						{
							return true;
						}
						await Task.Delay(100, arg0);
					}
				}
			}
			if (num < 5)
			{
				await Task.Delay(500, arg0);
			}
		}
		return false;
	}

	private unsafe bool Method_183()
	{
		if (Condition[ConditionFlag.Mounted])
		{
			return true;
		}
		if (Condition[ConditionFlag.InCombat])
		{
			return false;
		}
		if (Condition[ConditionFlag.Casting])
		{
			return true;
		}
		BattleChara* localPlayer = Control.GetLocalPlayer();
		if (localPlayer != null)
		{
			return localPlayer->IsCasting;
		}
		return false;
	}

	private unsafe bool Method_184()
	{
		if (Condition[ConditionFlag.BetweenAreas] || Condition[ConditionFlag.BetweenAreas51])
		{
			return false;
		}
		if (Condition[ConditionFlag.MountOrOrnamentTransition] || Condition[ConditionFlag.Casting])
		{
			return false;
		}
		if (Method_133())
		{
			return false;
		}
		if (Control.GetLocalPlayer() == null)
		{
			return false;
		}
		ActionManager* ptr = ActionManager.Instance();
		if (ptr == null)
		{
			return false;
		}
		ActionType actionType = ((Config.SelectedMountId == 0) ? ActionType.GeneralAction : ActionType.Mount);
		uint actionId = ((Config.SelectedMountId == 0) ? 24u : Config.SelectedMountId);
		return ptr->GetActionStatus(actionType, actionId, 3758096384uL, checkRecastActive: true, checkCastingActive: true, null) == 0;
	}

	public void SummonMount()
	{
		Method_186();
	}

	private async Task Method_186()
	{
		try
		{
			await Method_182(CancellationToken.None);
		}
		catch (Exception exception)
		{
			Log.Warning(exception, "AutomaticHunting: Manual mount summon failed");
		}
	}

	public void StopNavigation()
	{
		lock (field_53)
		{
			CancellationTokenSource? cancellationTokenSource = field_54;
			field_56 = RecoveredType_8.field_116;
			cancellationTokenSource?.Cancel();
		}
		Method_195();
		Method_188();
	}

	private void Method_188()
	{
		FinishSMonitor("已停止");
		LastFlagPosition = null;
		field_28 = 0u;
		field_32 = null;
		field_34 = false;
		field_73 = null;
		field_27 = string.Empty;
		field_29 = DateTime.MinValue;
		field_43 = null;
		field_44 = null;
		field_45 = DateTime.MinValue;
	}

	public void StartNavigation()
	{
		uint arg;
		uint arg2;
		Vector3 arg3;
		if (!Config.AutomationEnabled)
		{
			PrintEcho("自动狩猎未启用，无法导航");
		}
		else if (!Method_190(out arg, out arg2, out arg3))
		{
			PrintEcho("当前没有 Flag 或 vnav 未就绪");
		}
		else
		{
			Method_191(arg, arg3);
		}
	}

	private unsafe bool Method_190(out uint arg0, out uint arg1, out Vector3 arg2)
	{
		arg0 = 0u;
		arg1 = 0u;
		arg2 = default;
		AgentMap* ptr = AgentMap.Instance();
		if (ptr == null || ptr->FlagMarkerCount == 0)
		{
			return false;
		}
		FlagMapMarker flagMapMarker = ptr->FlagMapMarkers[0];
		if (flagMapMarker.TerritoryId == 0 || flagMapMarker.MapId == 0)
		{
			return false;
		}
		uint territoryId = flagMapMarker.TerritoryId;
		uint mapId = flagMapMarker.MapId;
		int rawX = (int)(flagMapMarker.XFloat * 1000f);
		int rawY = (int)(flagMapMarker.YFloat * 1000f);
		MapLinkPayload mapLinkPayload = new MapLinkPayload(territoryId, mapId, rawX, rawY);
		if (!Method_125(territoryId, mapId, mapLinkPayload.XCoord, mapLinkPayload.YCoord, out var arg3))
		{
			return false;
		}
		arg0 = territoryId;
		arg1 = mapId;
		arg2 = arg3;
		return true;
	}

	private void Method_191(uint arg0, Vector3 arg1)
	{
		LastFlagPosition = arg1;
		field_28 = arg0;
		field_31 = Method_127(arg0);
		CancellationTokenSource field_235 = new CancellationTokenSource();
		lock (field_53)
		{
			CancellationTokenSource? cancellationTokenSource = field_54;
			Task field_236 = field_55;
			field_54 = field_235;
			field_56 = RecoveredType_8.field_116;
			cancellationTokenSource?.Cancel();
			field_55 = Task.Run(async () =>
			{
				if (field_236 != null)
				{
					try
					{
						await field_236;
					}
					catch
					{
					}
				}
				await Method_192(arg0, arg1, field_235);
			});
		}
	}

	private async Task Method_192(uint arg0, Vector3 arg1, CancellationTokenSource arg2)
	{
		_ = 5;
		try
		{
			CancellationToken token = arg2.Token;
			RecoveredType_3 arg3 = new RecoveredType_3(arg0, arg1);
			switch (await Method_159(arg3, token))
			{
			case RecoveredType_10.field_130:
				return;
			case RecoveredType_10.field_129:
				Method_196(RecoveredType_8.field_117, arg2);
				if (!(await Method_163(arg0, token)))
				{
					return;
				}
				break;
			}
			Method_196(RecoveredType_8.field_118, arg2);
			if (!(await Method_182(token)))
			{
				PrintEcho("召唤坐骑失败，导航已取消");
				return;
			}
			await Task.Delay(100, token);
			if (!(await Method_181(token)))
			{
				PrintEcho("启动导航失败: vnavmesh未就绪");
				return;
			}
			Method_196(RecoveredType_8.field_119, arg2);
			PrintEcho("开始导航: /vnav flyflag");
			Method_179("/vnav flyflag");
			await Method_193(arg1, arg2, token);
		}
		catch (OperationCanceledException)
		{
			Log.Information("AutomaticHunting: FlyFlag navigation cancelled");
		}
		catch (Exception ex2)
		{
			Log.Warning(ex2, "AutomaticHunting: FlyFlag navigation failed");
			PrintEcho("导航异常: " + ex2.Message);
		}
		finally
		{
			lock (field_53)
			{
				if (field_54 == arg2)
				{
					field_54 = null;
					field_55 = null;
					field_56 = RecoveredType_8.field_116;
				}
			}
			arg2.Dispose();
		}
	}

	private async Task Method_193(Vector3 arg0, CancellationTokenSource arg1, CancellationToken arg2)
	{
		bool landingMode = Config.LandingMode;
		for (int i = 0; i < 240; i++)
		{
			await Task.Delay(500, arg2);
			Vector3? vector = await Framework.RunOnFrameworkThread((Func<Vector3?>)GetLocalPlayerPosition);
			if (!vector.HasValue)
			{
				continue;
			}
			float num = Method_197(vector.Value, arg0);
			if (num >= 100f)
			{
				continue;
			}
			RecoveredType_4? recoveredType_ = await Framework.RunOnFrameworkThread((Func<RecoveredType_4?>)Method_2);
			if (recoveredType_.HasValue)
			{
				Method_179("/vnav stop");
				PrintEcho("发现A怪: " + recoveredType_.Value.Name + "，切换到真实坐标");
				await Method_194(recoveredType_.Value.Position, landingMode, arg1, arg2);
				return;
			}
			if (Config.SEnabled)
			{
				RecoveredType_5? recoveredType_2 = await Framework.RunOnFrameworkThread((Func<RecoveredType_5?>)Method_202);
				if (recoveredType_2.HasValue)
				{
					Method_179("/vnav stop");
					PrintEcho($"未发现A怪，发现S怪: {recoveredType_2.Value.Name} 血量{recoveredType_2.Value.HpPercent:0.0}%，开始悬停监控");
					await Method_165(recoveredType_2.Value, arg1, arg2);
					return;
				}
			}
			if (num < 50f)
			{
				Method_179("/vnav stop");
				PrintEcho("到达Flag附近未发现A怪");
				return;
			}
		}
		Method_179("/vnav stop");
		PrintEcho("到达Flag附近未发现A怪");
	}

	private async Task Method_194(Vector3 arg0, bool arg1, CancellationTokenSource arg2, CancellationToken arg3)
	{
		Method_196(RecoveredType_8.field_120, arg2);
		Vector3 field_241 = arg0 + Method_114(arg0);
		bool flag = false;
		for (int i = 1; i <= 2; i++)
		{
			flag = await Framework.RunOnFrameworkThread(() =>
			{
				Vector3 valueOrDefault = Method_118(field_241).GetValueOrDefault(field_241);
				ICallGateSubscriber<bool>? callGateSubscriber = field_24;
				return callGateSubscriber != null && callGateSubscriber.InvokeFunc() && (field_25?.InvokeFunc(valueOrDefault, arg2: true) ?? false);
			});
			if (flag)
			{
				break;
			}
			if (i < 2)
			{
				PrintEcho("A怪寻路失败，2秒后重新寻路...");
				await Task.Delay(TimeSpan.FromSeconds(2L), arg3);
			}
		}
		if (!flag)
		{
			PrintEcho("A怪导航失败: vnavmesh未就绪或无法规划路径");
			return;
		}
		PrintEcho("导航已开始(前往A怪落点)");
		for (int i = 0; i < 240; i++)
		{
			await Task.Delay(500, arg3);
			Vector3? vector = await Framework.RunOnFrameworkThread((Func<Vector3?>)GetLocalPlayerPosition);
			if (!vector.HasValue || Method_197(vector.Value, field_241) >= 3f)
			{
				continue;
			}
			Method_179("/vnav stop");
			string text = await Framework.RunOnFrameworkThread((Func<string>)Method_3);
			if (!string.IsNullOrEmpty(text))
			{
				PrintEcho("已选中A怪: " + text);
			}
			else
			{
				PrintEcho("落点附近未扫描到A怪");
			}
			if (arg1)
			{
				if (await Method_167(arg3))
				{
					PrintEcho("已下坐骑，开始战斗");
				}
				else
				{
					PrintEcho("下坐骑失败(5s)，仍开始战斗");
				}
			}
			else
			{
				PrintEcho("悬停在A怪上方，保持坐骑");
			}
			return;
		}
		PrintEcho("120秒未到达A怪落点，导航结束");
	}

	private Task Method_195()
	{
		Method_179("/vnav stop");
		return Task.CompletedTask;
	}

	private void Method_196(RecoveredType_8 arg0, CancellationTokenSource arg1)
	{
		lock (field_53)
		{
			if (field_54 == arg1)
			{
				field_56 = arg0;
			}
		}
	}

	private static float Method_197(Vector3 arg0, Vector3 arg1)
	{
		return Vector2.Distance(new Vector2(arg0.X, arg0.Z), new Vector2(arg1.X, arg1.Z));
	}

	private unsafe static bool Method_198(uint arg0)
	{
		ActionManager* ptr = ActionManager.Instance();
		if (ptr != null)
		{
			return ptr->UseAction(ActionType.Mount, arg0, 3758096384uL, 0u, ActionManager.UseActionMode.None, 0u, null);
		}
		return false;
	}

	private unsafe static bool Method_199()
	{
		ActionManager* ptr = ActionManager.Instance();
		if (ptr != null)
		{
			return ptr->UseAction(ActionType.GeneralAction, 24u, 3758096384uL, 0u, ActionManager.UseActionMode.None, 0u, null);
		}
		return false;
	}

	public unsafe static bool UseDismount()
	{
		ActionManager* ptr = ActionManager.Instance();
		if (ptr != null)
		{
			return ptr->UseAction(ActionType.GeneralAction, 23u, 3758096384uL, 0u, ActionManager.UseActionMode.None, 0u, null);
		}
		return false;
	}

	public void ScanAndSelectTargetS()
	{
		string currentTerritoryName = GetCurrentTerritoryName();
		if (string.IsNullOrWhiteSpace(currentTerritoryName))
		{
			PrintEcho("扫描S怪: 无法获取当前区域名称");
			return;
		}
		if (!Config.SRealmMarks.TryGetValue(currentTerritoryName, out string[] value))
		{
			PrintEcho("扫描S怪: 当前区域 [" + currentTerritoryName + "] 不在S怪列表中");
			return;
		}
		PrintEcho($"扫描S怪: 区域={currentTerritoryName}, 目标={string.Join("/", value)}, 范围={100f:0}m");
		RecoveredType_5? recoveredType_ = Method_202();
		PrintEcho(recoveredType_.HasValue ? $"已找到S怪: {recoveredType_.Value.Name} 血量 {recoveredType_.Value.HpPercent:0.0}%" : $"扫描S怪: {100f:0}m内未找到匹配的目标");
	}

	private RecoveredType_5? Method_202()
	{
		Vector3? localPlayerPosition = GetLocalPlayerPosition();
		if (!localPlayerPosition.HasValue)
		{
			return null;
		}
		string currentTerritoryName = GetCurrentTerritoryName();
		if (string.IsNullOrWhiteSpace(currentTerritoryName) || !Config.SRealmMarks.TryGetValue(currentTerritoryName, out string[] value))
		{
			return null;
		}
		RecoveredType_5? result = null;
		float num = 100f;
		foreach (IGameObject item in ObjectTable)
		{
			float num2 = Method_197(localPlayerPosition.Value, item.Position);
			if (!(num2 > num))
			{
				string field_243 = item.Name.TextValue;
				if (value.Any((string value2) => GlobalCommandPolicy.IsMark(field_243, value2)))
				{
					float arg = Method_205(item);
					num = num2;
					result = new RecoveredType_5(field_243, item.Position, arg);
				}
			}
		}
		return result;
	}

	private bool Method_203(string? arg0)
	{
		if (string.IsNullOrWhiteSpace(arg0))
		{
			return false;
		}
		string currentTerritoryName = GetCurrentTerritoryName();
		if (string.IsNullOrWhiteSpace(currentTerritoryName) || !Config.SRealmMarks.TryGetValue(currentTerritoryName, out string[] value))
		{
			return false;
		}
		return value.Any((string value2) => GlobalCommandPolicy.IsMark(arg0, value2));
	}

	private string? Method_204(RecoveredType_5 arg0)
	{
		IGameObject gameObject = (from gameObject2 in ObjectTable
			where gameObject2.Name.TextValue == arg0.Name
			orderby Method_197(gameObject2.Position, arg0.Position)
			select gameObject2).FirstOrDefault();
		if (gameObject == null)
		{
			return null;
		}
		TargetManager.Target = gameObject;
		return gameObject.Name.TextValue;
	}

	private static float Method_205(IGameObject arg0)
	{
		try
		{
			if (arg0 is IBattleChara battleChara)
			{
				float num = battleChara.MaxHp;
				if (num > 0f)
				{
					return (float)battleChara.CurrentHp / num * 100f;
				}
			}
		}
		catch (Exception exception)
		{
			Log.Warning(exception, "AutomaticHunting: Failed to read S mob HP");
		}
		return 100f;
	}

	private static bool Method_206(string arg0, out float arg1)
	{
		if (TargetManager.Target is IBattleChara { CurrentHp: >0u } battleChara)
		{
			float num = battleChara.MaxHp;
			if (num > 0f)
			{
				arg1 = (float)battleChara.CurrentHp / num * 100f;
				return true;
			}
		}
		arg1 = 100f;
		return false;
	}

	[CompilerGenerated]
	private int Method_208()
	{
		if (!Condition[ConditionFlag.BetweenAreas] && !Condition[ConditionFlag.BetweenAreas51])
		{
			return GetCurrentInstanceNumber();
		}
		return -1;
	}

	[CompilerGenerated]
	private bool Method_209(MountInfo arg0)
	{
		return arg0.RowId == Config.SelectedMountId;
	}

	[CompilerGenerated]
	private (int kind, float hp, Vector3 pos, Vector3? player, bool isS) Method_210()
	{
		Vector3? localPlayerPosition = GetLocalPlayerPosition();
		if (TargetManager.Target is IBattleChara { MaxHp: >0u, CurrentHp: >0u } battleChara)
		{
			return (kind: 1, hp: (float)battleChara.CurrentHp / (float)battleChara.MaxHp * 100f, pos: battleChara.Position, player: localPlayerPosition, isS: Method_203(battleChara.Name.TextValue));
		}
		RecoveredType_5? recoveredType_ = Method_202();
		if (recoveredType_.HasValue)
		{
			if (recoveredType_.Value.HpPercent <= 0f)
			{
				return (kind: 2, hp: 0f, pos: recoveredType_.Value.Position, player: localPlayerPosition, isS: true);
			}
			Method_204(recoveredType_.Value);
			return (kind: 3, hp: recoveredType_.Value.HpPercent, pos: recoveredType_.Value.Position, player: localPlayerPosition, isS: true);
		}
		return (kind: 0, hp: -1f, pos: Vector3.Zero, player: localPlayerPosition, isS: false);
	}

	[CompilerGenerated]
	private bool Method_211()
	{
		return field_24?.InvokeFunc() ?? false;
	}

	[CompilerGenerated]
	private bool Method_212()
	{
		if (Config.SelectedMountId != 0)
		{
			return Method_198(Config.SelectedMountId);
		}
		return Method_199();
	}
}
