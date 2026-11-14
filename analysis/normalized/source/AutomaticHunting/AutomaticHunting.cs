using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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
		[StructLayout(LayoutKind.Auto)]
		public struct RecoveredType_13 : IAsyncStateMachine
		{
			public int field_153;

			public AsyncTaskMethodBuilder field_154;

			private TaskAwaiter field_155;

			private void Method_282()
			{
				int num = field_153;
				try
				{
					try
					{
						TaskAwaiter awaiter;
						if (num != 0)
						{
							awaiter = Task.Delay(5000).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (field_153 = 0);
								field_155 = awaiter;
								field_154.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							awaiter = field_155;
							field_155 = default;
							num = (field_153 = -1);
						}
						awaiter.GetResult();
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
				catch (Exception exception2)
				{
					field_153 = -2;
					field_154.SetException(exception2);
					return;
				}
				field_153 = -2;
				field_154.SetResult();
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in Method_282
				this.Method_282();
			}

			[DebuggerHidden]
			private void Method_283(IAsyncStateMachine arg0)
			{
				field_154.SetStateMachine(arg0);
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine arg0)
			{
				//ILSpy generated this explicit interface implementation from .override directive in Method_283
				this.Method_283(arg0);
			}
		}

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

		[AsyncStateMachine(typeof(RecoveredType_13))]
		internal Task? Method_264()
		{
			RecoveredType_13 stateMachine = default;
			stateMachine.field_154 = AsyncTaskMethodBuilder.Create();
			stateMachine.field_153 = -1;
			stateMachine.field_154.Start(ref stateMachine);
			return stateMachine.field_154.Task;
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
				CommandManager.ProcessCommand(field_158);
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
				CommandManager.ProcessCommand($"/pdr insc {field_160}");
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
		[StructLayout(LayoutKind.Auto)]
		public struct RecoveredType_20 : IAsyncStateMachine
		{
			public int field_165;

			public AsyncTaskMethodBuilder field_166;

			public RecoveredType_19 field_167;

			private TaskAwaiter field_168;

			private void Method_297()
			{
				int num = field_165;
				RecoveredType_19 recoveredType_ = field_167;
				try
				{
					TaskAwaiter awaiter;
					if (num != 0)
					{
						awaiter = Task.Delay(2000).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (field_165 = 0);
							field_168 = awaiter;
							field_166.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = field_168;
						field_168 = default;
						num = (field_165 = -1);
					}
					awaiter.GetResult();
					if (recoveredType_.field_163.SMonitor == recoveredType_.field_164)
					{
						recoveredType_.field_163.SMonitor = null;
					}
				}
				catch (Exception exception)
				{
					field_165 = -2;
					field_166.SetException(exception);
					return;
				}
				field_165 = -2;
				field_166.SetResult();
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in Method_297
				this.Method_297();
			}

			[DebuggerHidden]
			private void Method_298(IAsyncStateMachine arg0)
			{
				field_166.SetStateMachine(arg0);
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine arg0)
			{
				//ILSpy generated this explicit interface implementation from .override directive in Method_298
				this.Method_298(arg0);
			}
		}

		public AutomaticHunting field_163;

		public SMonitorStatus field_164;

		[AsyncStateMachine(typeof(RecoveredType_20))]
		internal Task? Method_296()
		{
			RecoveredType_20 stateMachine = default;
			stateMachine.field_166 = AsyncTaskMethodBuilder.Create();
			stateMachine.field_167 = this;
			stateMachine.field_165 = -1;
			stateMachine.field_166.Start(ref stateMachine);
			return stateMachine.field_166.Task;
		}
	}

	[CompilerGenerated]
	public sealed class RecoveredType_21
	{
		public string field_169;

		internal bool Method_300(string arg0)
		{
			return field_169.Contains(arg0);
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
				CommandManager.ProcessCommand(field_176);
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
		[StructLayout(LayoutKind.Auto)]
		public struct RecoveredType_33 : IAsyncStateMachine
		{
			public int field_196;

			public AsyncTaskMethodBuilder field_197;

			public RecoveredType_32 field_198;

			private TaskAwaiter field_199;

			private void Method_325()
			{
				int num = field_196;
				RecoveredType_32 recoveredType_ = field_198;
				try
				{
					TaskAwaiter awaiter;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = field_199;
							field_199 = default;
							num = (field_196 = -1);
							goto IL_00e6;
						}
						if (recoveredType_.field_192 == null)
						{
							goto IL_0084;
						}
					}
					try
					{
						if (num != 0)
						{
							awaiter = recoveredType_.field_192.GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (field_196 = 0);
								field_199 = awaiter;
								field_197.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							awaiter = field_199;
							field_199 = default;
							num = (field_196 = -1);
						}
						awaiter.GetResult();
					}
					catch
					{
					}
					goto IL_0084;
					IL_0084:
					awaiter = recoveredType_.field_193.Method_158(recoveredType_.field_194, recoveredType_.field_195).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (field_196 = 1);
						field_199 = awaiter;
						field_197.AwaitUnsafeOnCompleted(ref awaiter, ref this);
						return;
					}
					goto IL_00e6;
					IL_00e6:
					awaiter.GetResult();
				}
				catch (Exception exception)
				{
					field_196 = -2;
					field_197.SetException(exception);
					return;
				}
				field_196 = -2;
				field_197.SetResult();
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in Method_325
				this.Method_325();
			}

			[DebuggerHidden]
			private void Method_326(IAsyncStateMachine arg0)
			{
				field_197.SetStateMachine(arg0);
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine arg0)
			{
				//ILSpy generated this explicit interface implementation from .override directive in Method_326
				this.Method_326(arg0);
			}
		}

		public Task field_192;

		public AutomaticHunting field_193;

		public RecoveredType_3 field_194;

		public CancellationTokenSource field_195;

		[AsyncStateMachine(typeof(RecoveredType_33))]
		internal Task? Method_324()
		{
			RecoveredType_33 stateMachine = default;
			stateMachine.field_197 = AsyncTaskMethodBuilder.Create();
			stateMachine.field_198 = this;
			stateMachine.field_196 = -1;
			stateMachine.field_197.Start(ref stateMachine);
			return stateMachine.field_197.Task;
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
					CommandManager.ProcessCommand(content);
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
				CommandManager.ProcessCommand(field_229);
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
		[StructLayout(LayoutKind.Auto)]
		public struct RecoveredType_49 : IAsyncStateMachine
		{
			public int field_236;

			public AsyncTaskMethodBuilder field_237;

			public RecoveredType_48 field_238;

			private TaskAwaiter field_239;

			private void Method_360()
			{
				int num = field_236;
				RecoveredType_48 recoveredType_ = field_238;
				try
				{
					TaskAwaiter awaiter;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = field_239;
							field_239 = default;
							num = (field_236 = -1);
							goto IL_00ec;
						}
						if (recoveredType_.field_231 == null)
						{
							goto IL_0084;
						}
					}
					try
					{
						if (num != 0)
						{
							awaiter = recoveredType_.field_231.GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (field_236 = 0);
								field_239 = awaiter;
								field_237.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							awaiter = field_239;
							field_239 = default;
							num = (field_236 = -1);
						}
						awaiter.GetResult();
					}
					catch
					{
					}
					goto IL_0084;
					IL_0084:
					awaiter = recoveredType_.field_232.Method_192(recoveredType_.field_233, recoveredType_.field_234, recoveredType_.field_235).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (field_236 = 1);
						field_239 = awaiter;
						field_237.AwaitUnsafeOnCompleted(ref awaiter, ref this);
						return;
					}
					goto IL_00ec;
					IL_00ec:
					awaiter.GetResult();
				}
				catch (Exception exception)
				{
					field_236 = -2;
					field_237.SetException(exception);
					return;
				}
				field_236 = -2;
				field_237.SetResult();
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in Method_360
				this.Method_360();
			}

			[DebuggerHidden]
			private void Method_361(IAsyncStateMachine arg0)
			{
				field_237.SetStateMachine(arg0);
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine arg0)
			{
				//ILSpy generated this explicit interface implementation from .override directive in Method_361
				this.Method_361(arg0);
			}
		}

		public Task field_231;

		public AutomaticHunting field_232;

		public uint field_233;

		public Vector3 field_234;

		public CancellationTokenSource field_235;

		[AsyncStateMachine(typeof(RecoveredType_49))]
		internal Task? Method_359()
		{
			RecoveredType_49 stateMachine = default;
			stateMachine.field_237 = AsyncTaskMethodBuilder.Create();
			stateMachine.field_238 = this;
			stateMachine.field_236 = -1;
			stateMachine.field_237.Start(ref stateMachine);
			return stateMachine.field_237.Task;
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
			return field_243.Contains(arg0);
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
			return field_247.Contains(arg0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	public struct RecoveredType_56 : IAsyncStateMachine
	{
		public int field_248;

		public AsyncTaskMethodBuilder field_249;

		public AutomaticHunting field_250;

		public CancellationToken field_251;

		private int field_252;

		private TaskAwaiter field_253;

		private void Method_375()
		{
			int num = field_248;
			AutomaticHunting automaticHunting = field_250;
			try
			{
				if (num != 0)
				{
					field_252 = 0;
					goto IL_0092;
				}
				TaskAwaiter awaiter = field_253;
				field_253 = default;
				num = (field_248 = -1);
				goto IL_007b;
				IL_007b:
				awaiter.GetResult();
				field_252++;
				goto IL_0092;
				IL_0092:
				if (field_252 < 5)
				{
					automaticHunting.ExecuteDashSkill();
					awaiter = Task.Delay(500, field_251).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (field_248 = 0);
						field_253 = awaiter;
						field_249.AwaitUnsafeOnCompleted(ref awaiter, ref this);
						return;
					}
					goto IL_007b;
				}
			}
			catch (Exception exception)
			{
				field_248 = -2;
				field_249.SetException(exception);
				return;
			}
			field_248 = -2;
			field_249.SetResult();
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_375
			this.Method_375();
		}

		[DebuggerHidden]
		private void Method_376(IAsyncStateMachine arg0)
		{
			field_249.SetStateMachine(arg0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine arg0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_376
			this.Method_376(arg0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	public struct RecoveredType_57 : IAsyncStateMachine
	{
		public int field_254;

		public AsyncTaskMethodBuilder field_255;

		public int field_256;

		public AutomaticHunting field_257;

		private RecoveredType_17 field_258;

		private bool field_259;

		private TaskAwaiter<bool> field_260;

		private TaskAwaiter field_261;

		private void Method_377()
		{
			int num = field_254;
			AutomaticHunting automaticHunting = field_257;
			try
			{
				if ((uint)num > 3u)
				{
					field_258 = new RecoveredType_17();
					field_258.field_160 = field_256;
				}
				try
				{
					TaskAwaiter awaiter2;
					TaskAwaiter<bool> awaiter;
					switch (num)
					{
					default:
						awaiter = automaticHunting.Method_66(field_258.field_160, TimeSpan.FromSeconds(5L)).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (field_254 = 0);
							field_260 = awaiter;
							field_255.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_00ab;
					case 0:
						awaiter = field_260;
						field_260 = default;
						num = (field_254 = -1);
						goto IL_00ab;
					case 1:
						awaiter2 = field_261;
						field_261 = default;
						num = (field_254 = -1);
						goto IL_013a;
					case 2:
						awaiter2 = field_261;
						field_261 = default;
						num = (field_254 = -1);
						goto IL_01df;
					case 3:
						{
							awaiter = field_260;
							field_260 = default;
							num = (field_254 = -1);
							break;
						}
						IL_01e6:
						awaiter = automaticHunting.Method_66(field_258.field_160, TimeSpan.FromSeconds(field_259 ? 20 : 10)).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (field_254 = 3);
							field_260 = awaiter;
							field_255.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						break;
						IL_00ab:
						if (!awaiter.GetResult())
						{
							field_259 = automaticHunting.Config.SwitchRetryAtAetheryte;
							if (field_259)
							{
								awaiter2 = automaticHunting.Method_69().GetAwaiter();
								if (!awaiter2.IsCompleted)
								{
									num = (field_254 = 1);
									field_261 = awaiter2;
									field_255.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
									return;
								}
								goto IL_013a;
							}
							goto IL_01e6;
						}
						automaticHunting.Method_67(field_258.field_160);
						goto end_IL_002f;
						IL_01df:
						awaiter2.GetResult();
						goto IL_01e6;
						IL_013a:
						awaiter2.GetResult();
						automaticHunting.PrintEcho($"切线未确认，重发命令 /pdr insc {field_258.field_160}");
						awaiter2 = Framework.RunOnFrameworkThread(() =>
						{
							try
							{
								CommandManager.ProcessCommand($"/pdr insc {field_258.field_160}");
							}
							catch (Exception exception3)
							{
								Log.Warning(exception3, $"AutomaticHunting: 切线重发失败 {field_258.field_160}");
							}
						}).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (field_254 = 2);
							field_261 = awaiter2;
							field_255.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_01df;
					}
					if (awaiter.GetResult())
					{
						automaticHunting.Method_67(field_258.field_160);
					}
					else
					{
						automaticHunting.PrintEcho($"切线确认超时，真实线未达到目标 {field_258.field_160} 线");
						Log.Warning($"AutomaticHunting: 切线确认超时(目标 {field_258.field_160} 线)");
					}
					end_IL_002f:;
				}
				catch (Exception exception)
				{
					Log.Warning(exception, "AutomaticHunting: 切线确认失败");
				}
			}
			catch (Exception exception2)
			{
				field_254 = -2;
				field_258 = null;
				field_255.SetException(exception2);
				return;
			}
			field_254 = -2;
			field_258 = null;
			field_255.SetResult();
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_377
			this.Method_377();
		}

		[DebuggerHidden]
		private void Method_378(IAsyncStateMachine arg0)
		{
			field_255.SetStateMachine(arg0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine arg0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_378
			this.Method_378(arg0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	public struct RecoveredType_58 : IAsyncStateMachine
	{
		public int field_262;

		public AsyncTaskMethodBuilder<bool> field_263;

		public CancellationToken field_264;

		private int field_265;

		private TaskAwaiter<bool> field_266;

		private TaskAwaiter field_267;

		private void Method_379()
		{
			int num = field_262;
			bool result;
			try
			{
				TaskAwaiter awaiter2;
				TaskAwaiter<bool> awaiter;
				switch (num)
				{
				default:
					field_265 = 0;
					goto IL_01a2;
				case 0:
					awaiter = field_266;
					field_266 = default;
					num = (field_262 = -1);
					goto IL_00a0;
				case 1:
					awaiter = field_266;
					field_266 = default;
					num = (field_262 = -1);
					goto IL_0123;
				case 2:
					awaiter2 = field_267;
					field_267 = default;
					num = (field_262 = -1);
					goto IL_0189;
				case 3:
					{
						awaiter = field_266;
						field_266 = default;
						num = (field_262 = -1);
						break;
					}
					IL_01a2:
					if (field_265 < 10)
					{
						awaiter = Framework.RunOnFrameworkThread(() => Condition[ConditionFlag.Mounted]).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (field_262 = 0);
							field_266 = awaiter;
							field_263.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_00a0;
					}
					awaiter = Framework.RunOnFrameworkThread(() => Condition[ConditionFlag.Mounted]).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (field_262 = 3);
						field_266 = awaiter;
						field_263.AwaitUnsafeOnCompleted(ref awaiter, ref this);
						return;
					}
					break;
					IL_0189:
					awaiter2.GetResult();
					field_265++;
					goto IL_01a2;
					IL_00a0:
					if (awaiter.GetResult())
					{
						awaiter = Framework.RunOnFrameworkThread((Func<bool>)UseDismount).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (field_262 = 1);
							field_266 = awaiter;
							field_263.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_0123;
					}
					result = true;
					goto end_IL_0007;
					IL_0123:
					awaiter.GetResult();
					awaiter2 = Task.Delay(500, field_264).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						num = (field_262 = 2);
						field_267 = awaiter2;
						field_263.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
						return;
					}
					goto IL_0189;
				}
				result = !awaiter.GetResult();
				end_IL_0007:;
			}
			catch (Exception exception)
			{
				field_262 = -2;
				field_263.SetException(exception);
				return;
			}
			field_262 = -2;
			field_263.SetResult(result);
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_379
			this.Method_379();
		}

		[DebuggerHidden]
		private void Method_380(IAsyncStateMachine arg0)
		{
			field_263.SetStateMachine(arg0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine arg0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_380
			this.Method_380(arg0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	public struct RecoveredType_59 : IAsyncStateMachine
	{
		public int field_268;

		public AsyncTaskMethodBuilder field_269;

		public AutomaticHunting field_270;

		public float field_271;

		public RecoveredType_5 field_272;

		public CancellationToken field_273;

		private TaskAwaiter<bool> field_274;

		private TaskAwaiter field_275;

		private void Method_381()
		{
			int num = field_268;
			AutomaticHunting automaticHunting = field_270;
			try
			{
				TaskAwaiter<bool> awaiter2;
				TaskAwaiter awaiter;
				bool result;
				switch (num)
				{
				default:
				{
					automaticHunting.Method_179("/vnav stop");
					SMonitorStatus sMonitor = automaticHunting.SMonitor;
					if (sMonitor != null)
					{
						sMonitor.Phase = "开怪中";
						sMonitor.HpPercent = field_271;
					}
					else
					{
						automaticHunting.BeginSMonitor(field_272.Name, "开怪中", field_271, automaticHunting.Config.SHpThreshold);
					}
					if (!automaticHunting.Config.SPathToEngage)
					{
						automaticHunting.PrintEcho($"S怪血量{field_271:0.0}%≤{automaticHunting.Config.SHpThreshold:0}%，原地开怪: 下坐骑");
						awaiter2 = automaticHunting.Method_167(field_273).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (field_268 = 0);
							field_274 = awaiter2;
							field_269.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_0150;
					}
					string value = ((automaticHunting.Method_114(field_272.Position) == Vector3.Zero) ? "" : "（含落点偏移）");
					automaticHunting.PrintEcho($"S怪血量{field_271:0.0}%≤{automaticHunting.Config.SHpThreshold:0}%，寻路开怪: 前往 {field_272.Name} 坐标{value}");
					awaiter2 = automaticHunting.Method_169(field_272, field_273).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						num = (field_268 = 2);
						field_274 = awaiter2;
						field_269.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
						return;
					}
					goto IL_0301;
				}
				case 0:
					awaiter2 = field_274;
					field_274 = default;
					num = (field_268 = -1);
					goto IL_0150;
				case 1:
					awaiter = field_275;
					field_275 = default;
					num = (field_268 = -1);
					goto IL_01de;
				case 2:
					awaiter2 = field_274;
					field_274 = default;
					num = (field_268 = -1);
					goto IL_0301;
				case 3:
					awaiter2 = field_274;
					field_274 = default;
					num = (field_268 = -1);
					goto IL_037d;
				case 4:
					{
						awaiter = field_275;
						field_275 = default;
						num = (field_268 = -1);
						break;
					}
					IL_0150:
					if (awaiter2.GetResult())
					{
						automaticHunting.PrintEcho("已下坐骑，执行突进: " + automaticHunting.Config.SDashCommand);
					}
					else
					{
						automaticHunting.PrintEcho("下坐骑失败(5s)，仍执行突进");
					}
					awaiter = automaticHunting.Method_168(field_273).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (field_268 = 1);
						field_275 = awaiter;
						field_269.AwaitUnsafeOnCompleted(ref awaiter, ref this);
						return;
					}
					goto IL_01de;
					IL_01de:
					awaiter.GetResult();
					goto end_IL_000e;
					IL_037d:
					if (awaiter2.GetResult())
					{
						automaticHunting.PrintEcho("已下坐骑，执行突进: " + automaticHunting.Config.SDashCommand);
					}
					else
					{
						automaticHunting.PrintEcho("下坐骑失败(5s)，仍执行突进");
					}
					awaiter = automaticHunting.Method_168(field_273).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (field_268 = 4);
						field_275 = awaiter;
						field_269.AwaitUnsafeOnCompleted(ref awaiter, ref this);
						return;
					}
					break;
					IL_0301:
					result = awaiter2.GetResult();
					automaticHunting.Method_179("/vnav stop");
					if (!result)
					{
						automaticHunting.PrintEcho("寻路开怪: 未到达S怪坐标(失败/超时)，原地下坐骑开怪");
					}
					awaiter2 = automaticHunting.Method_167(field_273).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						num = (field_268 = 3);
						field_274 = awaiter2;
						field_269.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
						return;
					}
					goto IL_037d;
				}
				awaiter.GetResult();
				end_IL_000e:;
			}
			catch (Exception exception)
			{
				field_268 = -2;
				field_269.SetException(exception);
				return;
			}
			field_268 = -2;
			field_269.SetResult();
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_381
			this.Method_381();
		}

		[DebuggerHidden]
		private void Method_382(IAsyncStateMachine arg0)
		{
			field_269.SetStateMachine(arg0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine arg0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_382
			this.Method_382(arg0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	public struct RecoveredType_60 : IAsyncStateMachine
	{
		public int field_276;

		public AsyncTaskMethodBuilder field_277;

		public AutomaticHunting field_278;

		public CancellationToken field_279;

		private RecoveredType_23 field_280;

		private Vector3 field_281;

		private string field_282;

		private TaskAwaiter field_283;

		private TaskAwaiter<List<(uint Id, Vector3 Position, string Name)>> field_284;

		private void Method_383()
		{
			int num = field_276;
			AutomaticHunting automaticHunting = field_278;
			try
			{
				TaskAwaiter<List<(uint, Vector3, string)>> awaiter2;
				TaskAwaiter awaiter;
				List<(uint, Vector3, string)> result;
				switch (num)
				{
				default:
				{
					field_280 = new RecoveredType_23();
					object field_ = automaticHunting.field_62;
					bool lockTaken = false;
					uint field_2;
					uint field_3;
					Vector3 field_4;
					try
					{
						Monitor.Enter(field_, ref lockTaken);
						field_2 = automaticHunting.field_70;
						field_3 = automaticHunting.field_71;
						field_4 = automaticHunting.field_72;
					}
					finally
					{
						if (num < 0 && lockTaken)
						{
							Monitor.Exit(field_);
						}
					}
					if (field_2 != 0 && field_3 != 0)
					{
						awaiter = automaticHunting.Method_105(field_2, field_3, field_4, field_279).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (field_276 = 0);
							field_283 = awaiter;
							field_277.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_00ce;
					}
					automaticHunting.PrintEcho("开始处理目标Flag");
					field_ = automaticHunting.field_62;
					lockTaken = false;
					try
					{
						Monitor.Enter(field_, ref lockTaken);
						field_282 = automaticHunting.field_69;
						field_280.field_174 = automaticHunting.field_67;
						field_281 = automaticHunting.field_68;
					}
					finally
					{
						if (num < 0 && lockTaken)
						{
							Monitor.Exit(field_);
						}
					}
					if (field_280.field_174 != 0)
					{
						automaticHunting.LastFlagPosition = field_281;
						automaticHunting.field_28 = field_280.field_174;
						awaiter2 = Framework.RunOnFrameworkThread(() => Method_127(field_280.field_174)).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (field_276 = 1);
							field_284 = awaiter2;
							field_277.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_01df;
					}
					automaticHunting.PrintEcho("跨服完成但无目标Flag，跳过");
					goto end_IL_000e;
				}
				case 0:
					awaiter = field_283;
					field_283 = default;
					num = (field_276 = -1);
					goto IL_00ce;
				case 1:
					awaiter2 = field_284;
					field_284 = default;
					num = (field_276 = -1);
					goto IL_01df;
				case 2:
					{
						awaiter = field_283;
						field_283 = default;
						num = (field_276 = -1);
						break;
					}
					IL_01df:
					result = awaiter2.GetResult();
					automaticHunting.field_31 = result;
					awaiter = automaticHunting.Method_106(field_280.field_174, field_281, field_282).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (field_276 = 2);
						field_283 = awaiter;
						field_277.AwaitUnsafeOnCompleted(ref awaiter, ref this);
						return;
					}
					break;
					IL_00ce:
					awaiter.GetResult();
					goto end_IL_000e;
				}
				awaiter.GetResult();
				end_IL_000e:;
			}
			catch (Exception exception)
			{
				field_276 = -2;
				field_280 = null;
				field_282 = null;
				field_277.SetException(exception);
				return;
			}
			field_276 = -2;
			field_280 = null;
			field_282 = null;
			field_277.SetResult();
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_383
			this.Method_383();
		}

		[DebuggerHidden]
		private void Method_384(IAsyncStateMachine arg0)
		{
			field_277.SetStateMachine(arg0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine arg0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_384
			this.Method_384(arg0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	public struct RecoveredType_61 : IAsyncStateMachine
	{
		public int field_285;

		public AsyncTaskMethodBuilder<string> field_286;

		private TaskAwaiter<string> field_287;

		private void Method_385()
		{
			int num = field_285;
			string result;
			try
			{
				TaskAwaiter<string> awaiter;
				if (num != 0)
				{
					awaiter = Framework.RunOnFrameworkThread(() =>
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
						catch (Exception exception2)
						{
							Log.Warning(exception2, "AutomaticHunting: 读取当前服务器名失败");
						}
						return string.Empty;
					}).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (field_285 = 0);
						field_287 = awaiter;
						field_286.AwaitUnsafeOnCompleted(ref awaiter, ref this);
						return;
					}
				}
				else
				{
					awaiter = field_287;
					field_287 = default;
					num = (field_285 = -1);
				}
				result = awaiter.GetResult();
			}
			catch (Exception exception)
			{
				field_285 = -2;
				field_286.SetException(exception);
				return;
			}
			field_285 = -2;
			field_286.SetResult(result);
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_385
			this.Method_385();
		}

		[DebuggerHidden]
		private void Method_386(IAsyncStateMachine arg0)
		{
			field_286.SetStateMachine(arg0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine arg0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_386
			this.Method_386(arg0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	public struct RecoveredType_62 : IAsyncStateMachine
	{
		public int field_288;

		public AsyncTaskMethodBuilder<uint> field_289;

		private TaskAwaiter<uint> field_290;

		private void Method_387()
		{
			int num = field_288;
			uint result;
			try
			{
				TaskAwaiter<uint> awaiter;
				if (num != 0)
				{
					awaiter = Framework.RunOnFrameworkThread(() =>
					{
						try
						{
							IPlayerCharacter localPlayer = ObjectTable.LocalPlayer;
							if (localPlayer != null)
							{
								return localPlayer.CurrentWorld.RowId;
							}
						}
						catch (Exception exception2)
						{
							Log.Warning(exception2, "AutomaticHunting: 读取当前服务器失败");
						}
						return 0u;
					}).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (field_288 = 0);
						field_290 = awaiter;
						field_289.AwaitUnsafeOnCompleted(ref awaiter, ref this);
						return;
					}
				}
				else
				{
					awaiter = field_290;
					field_290 = default;
					num = (field_288 = -1);
				}
				result = awaiter.GetResult();
			}
			catch (Exception exception)
			{
				field_288 = -2;
				field_289.SetException(exception);
				return;
			}
			field_288 = -2;
			field_289.SetResult(result);
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_387
			this.Method_387();
		}

		[DebuggerHidden]
		private void Method_388(IAsyncStateMachine arg0)
		{
			field_289.SetStateMachine(arg0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine arg0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_388
			this.Method_388(arg0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	public struct RecoveredType_63 : IAsyncStateMachine
	{
		public int field_291;

		public AsyncTaskMethodBuilder<Vector3?> field_292;

		public AutomaticHunting field_293;

		public Vector3 field_294;

		public bool field_295;

		private TaskAwaiter<Vector3?> field_296;

		private void Method_389()
		{
			int num = field_291;
			Vector3? result;
			try
			{
				TaskAwaiter<Vector3?> awaiter;
				if (num != 0)
				{
					AutomaticHunting field_223 = field_293;
					Vector3 field_224 = field_294;
					bool field_225 = field_295;
					awaiter = Framework.RunOnFrameworkThread(() =>
					{
						if (!field_223.Method_171(field_224, field_225, out var arg))
						{
							return (Vector3?)null;
						}
						Vector3 vector = arg + field_223.Method_114(arg);
						Vector3 arg2 = field_223.Method_118(vector) ?? vector;
						ICallGateSubscriber<bool>? field_226 = field_223.field_24;
						if (field_226 != null && field_226.InvokeFunc())
						{
							ICallGateSubscriber<Vector3, bool, bool>? field_227 = field_223.field_25;
							if (field_227 != null && field_227.InvokeFunc(arg2, arg2: true))
							{
								return vector;
							}
						}
						return (Vector3?)null;
					}).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (field_291 = 0);
						field_296 = awaiter;
						field_292.AwaitUnsafeOnCompleted(ref awaiter, ref this);
						return;
					}
				}
				else
				{
					awaiter = field_296;
					field_296 = default;
					num = (field_291 = -1);
				}
				result = awaiter.GetResult();
			}
			catch (Exception exception)
			{
				field_291 = -2;
				field_292.SetException(exception);
				return;
			}
			field_291 = -2;
			field_292.SetResult(result);
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_389
			this.Method_389();
		}

		[DebuggerHidden]
		private void Method_390(IAsyncStateMachine arg0)
		{
			field_292.SetStateMachine(arg0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine arg0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_390
			this.Method_390(arg0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	public struct RecoveredType_64 : IAsyncStateMachine
	{
		public int field_297;

		public AsyncTaskMethodBuilder field_298;

		public uint field_299;

		public CancellationToken field_300;

		public AutomaticHunting field_301;

		public uint field_302;

		private RecoveredType_24 field_303;

		public Vector3 field_304;

		private TaskAwaiter<bool> field_305;

		private TaskAwaiter<List<(uint Id, Vector3 Position, string Name)>> field_306;

		private void Method_391()
		{
			int num = field_297;
			AutomaticHunting automaticHunting = field_301;
			try
			{
				if ((uint)num > 2u)
				{
					field_303 = new RecoveredType_24();
					field_303.field_175 = field_299;
				}
				try
				{
					TaskAwaiter<bool> awaiter2;
					TaskAwaiter<List<(uint, Vector3, string)>> awaiter;
					bool result;
					List<(uint, Vector3, string)> result2;
					switch (num)
					{
					default:
						field_300.ThrowIfCancellationRequested();
						automaticHunting.PrintEcho("跨服完成，传送到所选水晶...");
						awaiter2 = automaticHunting.Method_123(field_302, field_300).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (field_297 = 0);
							field_305 = awaiter2;
							field_298.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_00b7;
					case 0:
						awaiter2 = field_305;
						field_305 = default;
						num = (field_297 = -1);
						goto IL_00b7;
					case 1:
						awaiter2 = field_305;
						field_305 = default;
						num = (field_297 = -1);
						goto IL_0127;
					case 2:
						{
							awaiter = field_306;
							field_306 = default;
							num = (field_297 = -1);
							goto IL_01c3;
						}
						IL_0127:
						result = awaiter2.GetResult();
						goto IL_012f;
						IL_00b7:
						result = awaiter2.GetResult();
						if (result)
						{
							awaiter2 = automaticHunting.Method_163(field_303.field_175, field_300).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								num = (field_297 = 1);
								field_305 = awaiter2;
								field_298.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
								return;
							}
							goto IL_0127;
						}
						goto IL_012f;
						IL_012f:
						if (result)
						{
							automaticHunting.LastFlagPosition = field_304;
							automaticHunting.field_28 = field_303.field_175;
							awaiter = Framework.RunOnFrameworkThread(() => Method_127(field_303.field_175)).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (field_297 = 2);
								field_306 = awaiter;
								field_298.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
							goto IL_01c3;
						}
						automaticHunting.PrintEcho("传送到所选水晶失败");
						break;
						IL_01c3:
						result2 = awaiter.GetResult();
						automaticHunting.field_31 = result2;
						automaticHunting.PrintEcho("已到达目标地图的所选水晶");
						break;
					}
				}
				catch (OperationCanceledException)
				{
					automaticHunting.PrintEcho("一键跨服传送已中断");
				}
				catch (Exception ex2)
				{
					Log.Warning(ex2, "AutomaticHunting: 一键跨服传送异常");
					automaticHunting.PrintEcho("一键跨服传送异常: " + ex2.Message);
				}
				finally
				{
					if (num < 0)
					{
						object field_ = automaticHunting.field_62;
						bool result = false;
						try
						{
							Monitor.Enter(field_, ref result);
							automaticHunting.field_70 = 0u;
							automaticHunting.field_71 = 0u;
							automaticHunting.field_72 = default;
						}
						finally
						{
							if (num < 0 && result)
							{
								Monitor.Exit(field_);
							}
						}
					}
				}
			}
			catch (Exception exception)
			{
				field_297 = -2;
				field_303 = null;
				field_298.SetException(exception);
				return;
			}
			field_297 = -2;
			field_303 = null;
			field_298.SetResult();
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_391
			this.Method_391();
		}

		[DebuggerHidden]
		private void Method_392(IAsyncStateMachine arg0)
		{
			field_298.SetStateMachine(arg0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine arg0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_392
			this.Method_392(arg0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	public struct RecoveredType_65 : IAsyncStateMachine
	{
		public int field_307;

		public AsyncTaskMethodBuilder field_308;

		public AutomaticHunting field_309;

		public CancellationToken field_310;

		public Vector3 field_311;

		public CancellationTokenSource field_312;

		private bool field_313;

		private int field_314;

		private float field_315;

		private TaskAwaiter field_316;

		private TaskAwaiter<Vector3?> field_317;

		private TaskAwaiter<RecoveredType_4?> field_318;

		private TaskAwaiter<RecoveredType_5?> field_319;

		private void Method_393()
		{
			int num = field_307;
			AutomaticHunting automaticHunting = field_309;
			try
			{
				TaskAwaiter<Vector3?> awaiter4;
				TaskAwaiter<RecoveredType_4?> awaiter3;
				TaskAwaiter<RecoveredType_5?> awaiter2;
				TaskAwaiter awaiter;
				RecoveredType_5? result;
				RecoveredType_4? result2;
				Vector3? result3;
				switch (num)
				{
				default:
					field_313 = automaticHunting.Config.LandingMode;
					field_314 = 0;
					goto IL_0442;
				case 0:
					awaiter = field_316;
					field_316 = default;
					num = (field_307 = -1);
					goto IL_00aa;
				case 1:
					awaiter4 = field_317;
					field_317 = default;
					num = (field_307 = -1);
					goto IL_0127;
				case 2:
					awaiter3 = field_318;
					field_318 = default;
					num = (field_307 = -1);
					goto IL_01ca;
				case 3:
					awaiter = field_316;
					field_316 = default;
					num = (field_307 = -1);
					goto IL_0287;
				case 4:
					awaiter2 = field_319;
					field_319 = default;
					num = (field_307 = -1);
					goto IL_030a;
				case 5:
					{
						awaiter = field_316;
						field_316 = default;
						num = (field_307 = -1);
						goto IL_0402;
					}
					IL_0442:
					if (field_314 < 240)
					{
						awaiter = Task.Delay(500, field_310).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (field_307 = 0);
							field_316 = awaiter;
							field_308.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_00aa;
					}
					automaticHunting.Method_179("/vnav stop");
					automaticHunting.PrintEcho("到达Flag附近未发现A怪");
					break;
					IL_0287:
					awaiter.GetResult();
					break;
					IL_030a:
					result = awaiter2.GetResult();
					if (result.HasValue)
					{
						automaticHunting.Method_179("/vnav stop");
						automaticHunting.PrintEcho($"未发现A怪，发现S怪: {result.Value.Name} 血量{result.Value.HpPercent:0.0}%，开始悬停监控");
						awaiter = automaticHunting.Method_165(result.Value, field_312, field_310).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (field_307 = 5);
							field_316 = awaiter;
							field_308.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_0402;
					}
					goto IL_040b;
					IL_0430:
					field_314++;
					goto IL_0442;
					IL_00aa:
					awaiter.GetResult();
					awaiter4 = Framework.RunOnFrameworkThread((Func<Vector3?>)GetLocalPlayerPosition).GetAwaiter();
					if (!awaiter4.IsCompleted)
					{
						num = (field_307 = 1);
						field_317 = awaiter4;
						field_308.AwaitUnsafeOnCompleted(ref awaiter4, ref this);
						return;
					}
					goto IL_0127;
					IL_0402:
					awaiter.GetResult();
					break;
					IL_01ca:
					result2 = awaiter3.GetResult();
					if (result2.HasValue)
					{
						automaticHunting.Method_179("/vnav stop");
						automaticHunting.PrintEcho("发现A怪: " + result2.Value.Name + "，切换到真实坐标");
						awaiter = automaticHunting.Method_194(result2.Value.Position, field_313, field_312, field_310).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (field_307 = 3);
							field_316 = awaiter;
							field_308.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_0287;
					}
					if (automaticHunting.Config.SEnabled)
					{
						awaiter2 = Framework.RunOnFrameworkThread((Func<RecoveredType_5?>)automaticHunting.Method_202).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (field_307 = 4);
							field_319 = awaiter2;
							field_308.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_030a;
					}
					goto IL_040b;
					IL_040b:
					if (field_315 < 50f)
					{
						automaticHunting.Method_179("/vnav stop");
						automaticHunting.PrintEcho("到达Flag附近未发现A怪");
						break;
					}
					goto IL_0430;
					IL_0127:
					result3 = awaiter4.GetResult();
					if (result3.HasValue)
					{
						field_315 = Method_197(result3.Value, field_311);
						if (!(field_315 >= 100f))
						{
							awaiter3 = Framework.RunOnFrameworkThread((Func<RecoveredType_4?>)automaticHunting.Method_2).GetAwaiter();
							if (!awaiter3.IsCompleted)
							{
								num = (field_307 = 2);
								field_318 = awaiter3;
								field_308.AwaitUnsafeOnCompleted(ref awaiter3, ref this);
								return;
							}
							goto IL_01ca;
						}
					}
					goto IL_0430;
				}
			}
			catch (Exception exception)
			{
				field_307 = -2;
				field_308.SetException(exception);
				return;
			}
			field_307 = -2;
			field_308.SetResult();
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_393
			this.Method_393();
		}

		[DebuggerHidden]
		private void Method_394(IAsyncStateMachine arg0)
		{
			field_308.SetStateMachine(arg0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine arg0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_394
			this.Method_394(arg0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	public struct RecoveredType_66 : IAsyncStateMachine
	{
		public int field_320;

		public AsyncTaskMethodBuilder field_321;

		public AutomaticHunting field_322;

		private float field_323;

		private DateTime field_324;

		private TaskAwaiter field_325;

		private void Method_395()
		{
			int num = field_320;
			AutomaticHunting automaticHunting = field_322;
			try
			{
				_ = 1;
				try
				{
					TaskAwaiter awaiter;
					if (num == 0)
					{
						awaiter = field_325;
						field_325 = default;
						num = (field_320 = -1);
						goto IL_0134;
					}
					if (num == 1)
					{
						awaiter = field_325;
						field_325 = default;
						num = (field_320 = -1);
						goto IL_036d;
					}
					IGameObject target = TargetManager.Target;
					if (target != null && target is IBattleChara)
					{
						automaticHunting.BeginSMonitor(target.Name.TextValue, "血量监控", Method_206(string.Empty, out var arg) ? arg : (-1f), automaticHunting.Config.SHpThreshold);
						automaticHunting.PrintEcho($"开始监控当前目标血量(阈值{automaticHunting.Config.SHpThreshold:0}%)...");
						field_323 = -1f;
						field_324 = DateTime.UtcNow;
						goto IL_00d9;
					}
					automaticHunting.PrintEcho("未选中目标，请先选中一个怪再监控血量");
					goto end_IL_0013;
					IL_0134:
					awaiter.GetResult();
					SMonitorStatus sMonitor = automaticHunting.SMonitor;
					if (sMonitor != null && sMonitor.Finished)
					{
						automaticHunting.PrintEcho("监控已停止");
					}
					else
					{
						IGameObject target2 = TargetManager.Target;
						if (target2 == null)
						{
							automaticHunting.PrintEcho("目标丢失，监控结束");
							automaticHunting.FinishSMonitor("目标丢失");
						}
						else
						{
							if (!(target2 is IBattleChara { CurrentHp: 0u }))
							{
								if (Method_206(string.Empty, out var arg2))
								{
									SMonitorStatus sMonitor2 = automaticHunting.SMonitor;
									if (sMonitor2 != null)
									{
										sMonitor2.Phase = "血量监控";
										sMonitor2.HpPercent = arg2;
										sMonitor2.WaitedSeconds = (float)(DateTime.UtcNow - field_324).TotalSeconds;
									}
									if (Math.Abs(arg2 - field_323) >= 5f)
									{
										field_323 = arg2;
										automaticHunting.PrintEcho($"监控中: 当前目标血量 {arg2:0.0}%");
									}
									if (arg2 <= automaticHunting.Config.SHpThreshold)
									{
										RecoveredType_5 arg3 = new RecoveredType_5(target2.Name.TextValue, target2.Position, arg2);
										automaticHunting.PrintEcho($"血量{arg2:0.0}%≤{automaticHunting.Config.SHpThreshold:0}%，开始开怪(与S怪流程一致): {automaticHunting.Config.SDashCommand}");
										awaiter = automaticHunting.Method_166(arg3, arg2, CancellationToken.None).GetAwaiter();
										if (!awaiter.IsCompleted)
										{
											num = (field_320 = 1);
											field_325 = awaiter;
											field_321.AwaitUnsafeOnCompleted(ref awaiter, ref this);
											return;
										}
										goto IL_036d;
									}
								}
								goto IL_00d9;
							}
							automaticHunting.PrintEcho("目标已死亡，监控结束");
							automaticHunting.FinishSMonitor("目标已死亡");
						}
					}
					goto end_IL_0013;
					IL_00d9:
					awaiter = Task.Delay(500).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (field_320 = 0);
						field_325 = awaiter;
						field_321.AwaitUnsafeOnCompleted(ref awaiter, ref this);
						return;
					}
					goto IL_0134;
					IL_036d:
					awaiter.GetResult();
					automaticHunting.FinishSMonitor("已开怪");
					end_IL_0013:;
				}
				catch (Exception exception)
				{
					Log.Warning(exception, "AutomaticHunting: Target HP monitor failed");
					automaticHunting.FinishSMonitor("监控异常");
				}
			}
			catch (Exception exception2)
			{
				field_320 = -2;
				field_321.SetException(exception2);
				return;
			}
			field_320 = -2;
			field_321.SetResult();
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_395
			this.Method_395();
		}

		[DebuggerHidden]
		private void Method_396(IAsyncStateMachine arg0)
		{
			field_321.SetStateMachine(arg0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine arg0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_396
			this.Method_396(arg0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	public struct RecoveredType_67 : IAsyncStateMachine
	{
		public int field_326;

		public AsyncTaskMethodBuilder field_327;

		public AutomaticHunting field_328;

		public CancellationTokenSource field_329;

		public Vector3 field_330;

		private RecoveredType_50 field_331;

		public CancellationToken field_332;

		public bool field_333;

		private bool field_334;

		private int field_335;

		private TaskAwaiter<bool> field_336;

		private TaskAwaiter field_337;

		private TaskAwaiter<Vector3?> field_338;

		private TaskAwaiter<string> field_339;

		private void Method_397()
		{
			int num = field_326;
			AutomaticHunting automaticHunting = field_328;
			try
			{
				TaskAwaiter awaiter4;
				TaskAwaiter<Vector3?> awaiter3;
				TaskAwaiter<string> awaiter2;
				TaskAwaiter<bool> awaiter;
				bool result;
				string result2;
				Vector3? result3;
				switch (num)
				{
				default:
					field_331 = new RecoveredType_50();
					field_331.field_240 = field_328;
					automaticHunting.Method_196(RecoveredType_8.field_120, field_329);
					field_331.field_241 = field_330 + automaticHunting.Method_114(field_330);
					field_334 = false;
					field_335 = 1;
					goto IL_01bc;
				case 0:
					awaiter = field_336;
					field_336 = default;
					num = (field_326 = -1);
					goto IL_0112;
				case 1:
					awaiter4 = field_337;
					field_337 = default;
					num = (field_326 = -1);
					goto IL_01a3;
				case 2:
					awaiter4 = field_337;
					field_337 = default;
					num = (field_326 = -1);
					goto IL_0258;
				case 3:
					awaiter3 = field_338;
					field_338 = default;
					num = (field_326 = -1);
					goto IL_02d5;
				case 4:
					awaiter2 = field_339;
					field_339 = default;
					num = (field_326 = -1);
					goto IL_037d;
				case 5:
					{
						awaiter = field_336;
						field_336 = default;
						num = (field_326 = -1);
						goto IL_0410;
					}
					IL_01bc:
					if (field_335 <= 2)
					{
						awaiter = Framework.RunOnFrameworkThread(() =>
						{
							Vector3 valueOrDefault = field_331.field_240.Method_118(field_331.field_241).GetValueOrDefault(field_331.field_241);
							ICallGateSubscriber<bool>? field_ = field_331.field_240.field_24;
							return field_ != null && field_.InvokeFunc() && (field_331.field_240.field_25?.InvokeFunc(valueOrDefault, arg2: true) ?? false);
						}).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (field_326 = 0);
							field_336 = awaiter;
							field_327.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_0112;
					}
					goto IL_01c8;
					IL_01aa:
					field_335++;
					goto IL_01bc;
					IL_0410:
					if (awaiter.GetResult())
					{
						automaticHunting.PrintEcho("已下坐骑，开始战斗");
					}
					else
					{
						automaticHunting.PrintEcho("下坐骑失败(5s)，仍开始战斗");
					}
					break;
					IL_01c8:
					if (!field_334)
					{
						automaticHunting.PrintEcho("A怪导航失败: vnavmesh未就绪或无法规划路径");
						break;
					}
					automaticHunting.PrintEcho("导航已开始(前往A怪落点)");
					field_335 = 0;
					goto IL_0452;
					IL_0258:
					awaiter4.GetResult();
					awaiter3 = Framework.RunOnFrameworkThread((Func<Vector3?>)GetLocalPlayerPosition).GetAwaiter();
					if (!awaiter3.IsCompleted)
					{
						num = (field_326 = 3);
						field_338 = awaiter3;
						field_327.AwaitUnsafeOnCompleted(ref awaiter3, ref this);
						return;
					}
					goto IL_02d5;
					IL_0112:
					result = awaiter.GetResult();
					field_334 = result;
					if (!field_334)
					{
						if (field_335 < 2)
						{
							automaticHunting.PrintEcho("A怪寻路失败，2秒后重新寻路...");
							awaiter4 = Task.Delay(TimeSpan.FromSeconds(2L), field_332).GetAwaiter();
							if (!awaiter4.IsCompleted)
							{
								num = (field_326 = 1);
								field_337 = awaiter4;
								field_327.AwaitUnsafeOnCompleted(ref awaiter4, ref this);
								return;
							}
							goto IL_01a3;
						}
						goto IL_01aa;
					}
					goto IL_01c8;
					IL_0452:
					if (field_335 < 240)
					{
						awaiter4 = Task.Delay(500, field_332).GetAwaiter();
						if (!awaiter4.IsCompleted)
						{
							num = (field_326 = 2);
							field_337 = awaiter4;
							field_327.AwaitUnsafeOnCompleted(ref awaiter4, ref this);
							return;
						}
						goto IL_0258;
					}
					automaticHunting.PrintEcho("120秒未到达A怪落点，导航结束");
					break;
					IL_037d:
					result2 = awaiter2.GetResult();
					if (!string.IsNullOrEmpty(result2))
					{
						automaticHunting.PrintEcho("已选中A怪: " + result2);
					}
					else
					{
						automaticHunting.PrintEcho("落点附近未扫描到A怪");
					}
					if (field_333)
					{
						awaiter = automaticHunting.Method_167(field_332).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (field_326 = 5);
							field_336 = awaiter;
							field_327.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_0410;
					}
					automaticHunting.PrintEcho("悬停在A怪上方，保持坐骑");
					break;
					IL_02d5:
					result3 = awaiter3.GetResult();
					if (result3.HasValue && !(Method_197(result3.Value, field_331.field_241) >= 3f))
					{
						automaticHunting.Method_179("/vnav stop");
						awaiter2 = Framework.RunOnFrameworkThread((Func<string>)automaticHunting.Method_3).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (field_326 = 4);
							field_339 = awaiter2;
							field_327.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_037d;
					}
					field_335++;
					goto IL_0452;
					IL_01a3:
					awaiter4.GetResult();
					goto IL_01aa;
				}
			}
			catch (Exception exception)
			{
				field_326 = -2;
				field_331 = null;
				field_327.SetException(exception);
				return;
			}
			field_326 = -2;
			field_331 = null;
			field_327.SetResult();
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_397
			this.Method_397();
		}

		[DebuggerHidden]
		private void Method_398(IAsyncStateMachine arg0)
		{
			field_327.SetStateMachine(arg0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine arg0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_398
			this.Method_398(arg0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	public struct RecoveredType_68 : IAsyncStateMachine
	{
		public int field_340;

		public AsyncTaskMethodBuilder field_341;

		public AutomaticHunting field_342;

		public RecoveredType_3 field_343;

		public CancellationTokenSource field_344;

		private RecoveredType_38 field_345;

		public CancellationToken field_346;

		private bool field_347;

		private int field_348;

		private bool field_349;

		private bool field_350;

		private bool field_351;

		private int field_352;

		private int field_353;

		private TaskAwaiter<bool> field_354;

		private TaskAwaiter field_355;

		private float field_356;

		private TaskAwaiter<Vector3?> field_357;

		private TaskAwaiter<RecoveredType_4?> field_358;

		private TaskAwaiter<RecoveredType_5?> field_359;

		private TaskAwaiter<string> field_360;

		private void Method_399()
		{
			int num = field_340;
			AutomaticHunting automaticHunting = field_342;
			try
			{
				TaskAwaiter<Vector3?> awaiter6;
				TaskAwaiter<RecoveredType_4?> awaiter5;
				TaskAwaiter<RecoveredType_5?> awaiter4;
				TaskAwaiter awaiter3;
				TaskAwaiter<string> awaiter2;
				TaskAwaiter<bool> awaiter;
				string result;
				RecoveredType_4? result2;
				bool result3;
				RecoveredType_5? result4;
				Vector3? result5;
				switch (num)
				{
				default:
					field_345 = new RecoveredType_38();
					field_345.field_208 = field_342;
					field_345.field_210 = field_343;
					field_347 = automaticHunting.Config.LandingMode;
					field_345.field_209 = field_345.field_210.Position;
					field_348 = 0;
					goto IL_09e2;
				case 0:
					awaiter = field_354;
					field_354 = default;
					num = (field_340 = -1);
					goto IL_0176;
				case 1:
					awaiter = field_354;
					field_354 = default;
					num = (field_340 = -1);
					goto IL_01e4;
				case 2:
					awaiter = field_354;
					field_354 = default;
					num = (field_340 = -1);
					goto IL_0285;
				case 3:
					awaiter3 = field_355;
					field_355 = default;
					num = (field_340 = -1);
					goto IL_02fa;
				case 4:
					awaiter3 = field_355;
					field_355 = default;
					num = (field_340 = -1);
					goto IL_03a0;
				case 5:
					awaiter3 = field_355;
					field_355 = default;
					num = (field_340 = -1);
					goto IL_0472;
				case 6:
					awaiter6 = field_357;
					field_357 = default;
					num = (field_340 = -1);
					goto IL_04ef;
				case 7:
					awaiter5 = field_358;
					field_358 = default;
					num = (field_340 = -1);
					goto IL_0609;
				case 8:
					awaiter4 = field_359;
					field_359 = default;
					num = (field_340 = -1);
					goto IL_0704;
				case 9:
					awaiter3 = field_355;
					field_355 = default;
					num = (field_340 = -1);
					goto IL_07fd;
				case 10:
					awaiter2 = field_360;
					field_360 = default;
					num = (field_340 = -1);
					goto IL_08bf;
				case 11:
					{
						awaiter = field_354;
						field_354 = default;
						num = (field_340 = -1);
						goto IL_0953;
					}
					IL_09e2:
					if (field_348 < 2)
					{
						field_349 = field_348 == 1;
						automaticHunting.Method_196(field_349 ? RecoveredType_8.field_120 : RecoveredType_8.field_119, field_344);
						field_350 = false;
						field_353 = 1;
						goto IL_03b9;
					}
					automaticHunting.Method_179("/vnav stop");
					automaticHunting.PrintEcho("120秒未到达A怪落点，导航结束");
					break;
					IL_0953:
					if (awaiter.GetResult())
					{
						automaticHunting.PrintEcho("已下坐骑，开始战斗");
					}
					else
					{
						automaticHunting.PrintEcho("下坐骑失败(5s)，仍开始战斗");
					}
					break;
					IL_08bf:
					result = awaiter2.GetResult();
					if (!string.IsNullOrEmpty(result))
					{
						automaticHunting.PrintEcho("已选中A怪: " + result);
					}
					else
					{
						automaticHunting.PrintEcho("落点附近未扫描到A怪");
					}
					if (field_347)
					{
						awaiter = automaticHunting.Method_167(field_346).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (field_340 = 11);
							field_354 = awaiter;
							field_341.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_0953;
					}
					automaticHunting.PrintEcho("悬停在A怪上方，保持坐骑");
					break;
					IL_02fa:
					awaiter3.GetResult();
					automaticHunting.Method_179("/vnav flyflag");
					automaticHunting.PrintEcho("开始导航: /vnav flyflag");
					field_350 = true;
					goto IL_031e;
					IL_031e:
					if (!field_350)
					{
						if (field_353 < 2)
						{
							automaticHunting.PrintEcho("寻路失败，2秒后重新寻路...");
							awaiter3 = Task.Delay(TimeSpan.FromSeconds(2L), field_346).GetAwaiter();
							if (!awaiter3.IsCompleted)
							{
								num = (field_340 = 4);
								field_355 = awaiter3;
								field_341.AwaitUnsafeOnCompleted(ref awaiter3, ref this);
								return;
							}
							goto IL_03a0;
						}
						goto IL_03a7;
					}
					goto IL_03c5;
					IL_0472:
					awaiter3.GetResult();
					awaiter6 = Framework.RunOnFrameworkThread((Func<Vector3?>)GetLocalPlayerPosition).GetAwaiter();
					if (!awaiter6.IsCompleted)
					{
						num = (field_340 = 6);
						field_357 = awaiter6;
						field_341.AwaitUnsafeOnCompleted(ref awaiter6, ref this);
						return;
					}
					goto IL_04ef;
					IL_03b9:
					if (field_353 <= 2)
					{
						if (field_349)
						{
							awaiter = Framework.RunOnFrameworkThread(() =>
							{
								Vector3 valueOrDefault = field_345.field_208.Method_118(field_345.field_209).GetValueOrDefault(field_345.field_209);
								ICallGateSubscriber<bool>? field_ = field_345.field_208.field_24;
								return field_ != null && field_.InvokeFunc() && (field_345.field_208.field_25?.InvokeFunc(valueOrDefault, arg2: true) ?? false);
							}).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (field_340 = 0);
								field_354 = awaiter;
								field_341.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
							goto IL_0176;
						}
						awaiter = automaticHunting.Method_181(field_346).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (field_340 = 1);
							field_354 = awaiter;
							field_341.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_01e4;
					}
					goto IL_03c5;
					IL_0609:
					result2 = awaiter5.GetResult();
					if (result2.HasValue)
					{
						automaticHunting.Method_179("/vnav stop");
						field_345.field_209 = result2.Value.Position + automaticHunting.Method_114(result2.Value.Position);
						field_351 = true;
						automaticHunting.PrintEcho("发现A怪: " + result2.Value.Name + "，切换到落点(偏移后)");
						goto IL_09a8;
					}
					if (automaticHunting.Config.SEnabled)
					{
						awaiter4 = Framework.RunOnFrameworkThread((Func<RecoveredType_5?>)automaticHunting.Method_202).GetAwaiter();
						if (!awaiter4.IsCompleted)
						{
							num = (field_340 = 8);
							field_359 = awaiter4;
							field_341.AwaitUnsafeOnCompleted(ref awaiter4, ref this);
							return;
						}
						goto IL_0704;
					}
					goto IL_0809;
					IL_07fd:
					awaiter3.GetResult();
					break;
					IL_03a7:
					field_353++;
					goto IL_03b9;
					IL_03a0:
					awaiter3.GetResult();
					goto IL_03a7;
					IL_03c5:
					if (!field_350)
					{
						automaticHunting.PrintEcho("导航失败: vnavmesh未就绪或无法规划路径");
						break;
					}
					automaticHunting.PrintEcho(field_349 ? "导航已开始(前往A怪真实坐标)" : "导航已开始(前往标点)");
					field_351 = false;
					field_352 = 0;
					field_353 = 0;
					goto IL_0998;
					IL_0176:
					result3 = awaiter.GetResult();
					field_350 = result3;
					goto IL_031e;
					IL_0704:
					result4 = awaiter4.GetResult();
					if (result4.HasValue)
					{
						automaticHunting.Method_179("/vnav stop");
						automaticHunting.PrintEcho($"未发现A怪，发现S怪: {result4.Value.Name} 血量{result4.Value.HpPercent:0.0}%，开始悬停监控");
						awaiter3 = automaticHunting.Method_165(result4.Value, field_344, field_346).GetAwaiter();
						if (!awaiter3.IsCompleted)
						{
							num = (field_340 = 9);
							field_355 = awaiter3;
							field_341.AwaitUnsafeOnCompleted(ref awaiter3, ref this);
							return;
						}
						goto IL_07fd;
					}
					goto IL_0809;
					IL_04ef:
					result5 = awaiter6.GetResult();
					if (result5.HasValue)
					{
						field_356 = Method_197(result5.Value, field_345.field_209);
						if (!field_349 && field_353 - field_352 >= 60)
						{
							field_352 = field_353;
							automaticHunting.PrintEcho($"飞向标点中(距标点{field_356:0.0}m)");
						}
						if (!field_349 && field_356 < 100f)
						{
							awaiter5 = Framework.RunOnFrameworkThread((Func<RecoveredType_4?>)automaticHunting.Method_2).GetAwaiter();
							if (!awaiter5.IsCompleted)
							{
								num = (field_340 = 7);
								field_358 = awaiter5;
								field_341.AwaitUnsafeOnCompleted(ref awaiter5, ref this);
								return;
							}
							goto IL_0609;
						}
						goto IL_0831;
					}
					goto IL_0986;
					IL_01e4:
					if (!awaiter.GetResult())
					{
						automaticHunting.PrintEcho("启动导航失败: vnavmesh未就绪(等待加载超时)");
						break;
					}
					awaiter = Framework.RunOnFrameworkThread(() => !field_345.field_208.Config.SetFlagOnNavigation || Method_126(field_345.field_210.TerritoryType, 0u, field_345.field_210.Position)).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (field_340 = 2);
						field_354 = awaiter;
						field_341.AwaitUnsafeOnCompleted(ref awaiter, ref this);
						return;
					}
					goto IL_0285;
					IL_09a8:
					if (!field_349 && !field_351)
					{
						automaticHunting.Method_179("/vnav stop");
						automaticHunting.PrintEcho("120秒未接近标点，导航结束");
						break;
					}
					field_348++;
					goto IL_09e2;
					IL_0809:
					if (field_356 < 50f)
					{
						automaticHunting.Method_179("/vnav stop");
						automaticHunting.PrintEcho("到达标点附近未发现A怪");
						break;
					}
					goto IL_0831;
					IL_0831:
					if (field_349 && field_356 < 3f)
					{
						automaticHunting.Method_179("/vnav stop");
						awaiter2 = Framework.RunOnFrameworkThread((Func<string>)automaticHunting.Method_3).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (field_340 = 10);
							field_360 = awaiter2;
							field_341.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_08bf;
					}
					goto IL_0986;
					IL_0986:
					field_353++;
					goto IL_0998;
					IL_0998:
					if (field_353 < 240)
					{
						awaiter3 = Task.Delay(500, field_346).GetAwaiter();
						if (!awaiter3.IsCompleted)
						{
							num = (field_340 = 5);
							field_355 = awaiter3;
							field_341.AwaitUnsafeOnCompleted(ref awaiter3, ref this);
							return;
						}
						goto IL_0472;
					}
					goto IL_09a8;
					IL_0285:
					if (!awaiter.GetResult())
					{
						automaticHunting.PrintEcho("设置地图Flag失败");
					}
					awaiter3 = Task.Delay(150, field_346).GetAwaiter();
					if (!awaiter3.IsCompleted)
					{
						num = (field_340 = 3);
						field_355 = awaiter3;
						field_341.AwaitUnsafeOnCompleted(ref awaiter3, ref this);
						return;
					}
					goto IL_02fa;
				}
			}
			catch (Exception exception)
			{
				field_340 = -2;
				field_345 = null;
				field_341.SetException(exception);
				return;
			}
			field_340 = -2;
			field_345 = null;
			field_341.SetResult();
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_399
			this.Method_399();
		}

		[DebuggerHidden]
		private void Method_400(IAsyncStateMachine arg0)
		{
			field_341.SetStateMachine(arg0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine arg0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_400
			this.Method_400(arg0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	public struct RecoveredType_69 : IAsyncStateMachine
	{
		public int field_361;

		public AsyncTaskMethodBuilder<bool> field_362;

		public AutomaticHunting field_363;

		public RecoveredType_5 field_364;

		private RecoveredType_42 field_365;

		public CancellationToken field_366;

		private int field_367;

		private Vector3 field_368;

		private DateTime field_369;

		private DateTime field_370;

		private DateTime field_371;

		private float field_372;

		private TaskAwaiter<Vector3?> field_373;

		private TaskAwaiter field_374;

		private float field_375;

		private TaskAwaiter<(Vector3? player, Vector3? mob)> field_376;

		private void Method_401()
		{
			int num = field_361;
			AutomaticHunting automaticHunting = field_363;
			bool result;
			try
			{
				TaskAwaiter awaiter3;
				TaskAwaiter<(Vector3?, Vector3?)> awaiter2;
				TaskAwaiter<Vector3?> awaiter;
				Vector3? result2;
				(Vector3?, Vector3?) result3;
				Vector3? result4;
				switch (num)
				{
				default:
					field_365 = new RecoveredType_42();
					field_365.field_220 = field_363;
					field_365.field_221 = field_364.Position;
					field_367 = 1;
					goto IL_051d;
				case 0:
					awaiter = field_373;
					field_373 = default;
					num = (field_361 = -1);
					goto IL_00c9;
				case 1:
					awaiter3 = field_374;
					field_374 = default;
					num = (field_361 = -1);
					goto IL_0157;
				case 2:
					awaiter3 = field_374;
					field_374 = default;
					num = (field_361 = -1);
					goto IL_0203;
				case 3:
					awaiter2 = field_376;
					field_376 = default;
					num = (field_361 = -1);
					goto IL_0295;
				case 4:
					{
						awaiter = field_373;
						field_373 = default;
						num = (field_361 = -1);
						goto IL_043c;
					}
					IL_051d:
					if (field_367 <= 2)
					{
						awaiter = automaticHunting.Method_170(field_365.field_221, arg1: true).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (field_361 = 0);
							field_373 = awaiter;
							field_362.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_00c9;
					}
					result = false;
					break;
					IL_0157:
					awaiter3.GetResult();
					goto IL_050b;
					IL_050b:
					field_367++;
					goto IL_051d;
					IL_04d9:
					if ((DateTime.UtcNow - field_369).TotalSeconds < 60.0)
					{
						awaiter3 = Task.Delay(500, field_366).GetAwaiter();
						if (!awaiter3.IsCompleted)
						{
							num = (field_361 = 2);
							field_374 = awaiter3;
							field_362.AwaitUnsafeOnCompleted(ref awaiter3, ref this);
							return;
						}
						goto IL_0203;
					}
					automaticHunting.PrintEcho("寻路开怪: 60秒未到达，重试寻路...");
					goto IL_050b;
					IL_00c9:
					result2 = awaiter.GetResult();
					if (!result2.HasValue)
					{
						if (field_367 < 2)
						{
							automaticHunting.PrintEcho("寻路开怪: 寻路失败，2秒后重试...");
							awaiter3 = Task.Delay(TimeSpan.FromSeconds(2L), field_366).GetAwaiter();
							if (!awaiter3.IsCompleted)
							{
								num = (field_361 = 1);
								field_374 = awaiter3;
								field_362.AwaitUnsafeOnCompleted(ref awaiter3, ref this);
								return;
							}
							goto IL_0157;
						}
						goto IL_050b;
					}
					field_368 = result2.Value;
					field_369 = DateTime.UtcNow;
					field_370 = field_369;
					field_371 = DateTime.MinValue;
					field_372 = 0f;
					goto IL_04d9;
					IL_0203:
					awaiter3.GetResult();
					awaiter2 = Framework.RunOnFrameworkThread(() =>
					{
						Vector3? localPlayerPosition = GetLocalPlayerPosition();
						Vector3? item = (field_365.field_220.Method_171(field_365.field_221, arg1: false, out var arg) ? new Vector3?(arg) : ((Vector3?)null));
						return (player: localPlayerPosition, mob: item);
					}).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						num = (field_361 = 3);
						field_376 = awaiter2;
						field_362.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
						return;
					}
					goto IL_0295;
					IL_0295:
					result3 = awaiter2.GetResult();
					if (result3.Item1.HasValue)
					{
						if (!result3.Item2.HasValue)
						{
							field_372 += 0.5f;
							if (field_372 >= 5f)
							{
								automaticHunting.PrintEcho($"寻路开怪: S怪已消失{field_372:0}秒，中止");
								result = false;
								break;
							}
						}
						else
						{
							field_372 = 0f;
							field_365.field_221 = result3.Item2.Value;
							Vector3 vector = result3.Item2.Value + automaticHunting.Method_114(result3.Item2.Value);
							if (Method_197(result3.Item1.Value, vector) <= 3f)
							{
								result = true;
								break;
							}
							field_375 = Method_197(vector, field_368);
							if (!(field_375 <= 4f) && !((DateTime.UtcNow - field_370).TotalSeconds < 1.5))
							{
								awaiter = automaticHunting.Method_170(field_365.field_221, arg1: true).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									num = (field_361 = 4);
									field_373 = awaiter;
									field_362.AwaitUnsafeOnCompleted(ref awaiter, ref this);
									return;
								}
								goto IL_043c;
							}
						}
					}
					goto IL_04d9;
					IL_043c:
					result4 = awaiter.GetResult();
					if (result4.HasValue)
					{
						field_368 = result4.Value;
						field_370 = DateTime.UtcNow;
						if (!((DateTime.UtcNow - field_371).TotalSeconds < 3.0))
						{
							field_371 = DateTime.UtcNow;
							automaticHunting.PrintEcho($"寻路开怪: 目标已移动 {field_375:0.0}m，重新寻路");
						}
					}
					goto IL_04d9;
				}
			}
			catch (Exception exception)
			{
				field_361 = -2;
				field_365 = null;
				field_362.SetException(exception);
				return;
			}
			field_361 = -2;
			field_365 = null;
			field_362.SetResult(result);
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_401
			this.Method_401();
		}

		[DebuggerHidden]
		private void Method_402(IAsyncStateMachine arg0)
		{
			field_362.SetStateMachine(arg0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine arg0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_402
			this.Method_402(arg0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	public struct RecoveredType_70 : IAsyncStateMachine
	{
		public int field_377;

		public AsyncTaskMethodBuilder<bool> field_378;

		public AutomaticHunting field_379;

		public int field_380;

		public TimeSpan field_381;

		private DateTime field_382;

		private TaskAwaiter field_383;

		private TaskAwaiter<int> field_384;

		private void Method_403()
		{
			int num = field_377;
			AutomaticHunting CS_0024_003C_003E8__locals1 = field_379;
			bool result;
			try
			{
				TaskAwaiter<int> awaiter;
				if (num != 0)
				{
					if (num != 1)
					{
						field_382 = DateTime.UtcNow;
						goto IL_0101;
					}
					awaiter = field_384;
					field_384 = default;
					num = (field_377 = -1);
					goto IL_00ee;
				}
				TaskAwaiter awaiter2 = field_383;
				field_383 = default;
				num = (field_377 = -1);
				goto IL_0080;
				IL_0101:
				if (DateTime.UtcNow - field_382 < field_381)
				{
					awaiter2 = Task.Delay(500).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						num = (field_377 = 0);
						field_383 = awaiter2;
						field_378.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
						return;
					}
					goto IL_0080;
				}
				result = false;
				goto end_IL_000e;
				IL_00ee:
				if (awaiter.GetResult() != field_380)
				{
					goto IL_0101;
				}
				result = true;
				goto end_IL_000e;
				IL_0080:
				awaiter2.GetResult();
				awaiter = Framework.RunOnFrameworkThread(() => (!Condition[ConditionFlag.BetweenAreas] && !Condition[ConditionFlag.BetweenAreas51]) ? CS_0024_003C_003E8__locals1.GetCurrentInstanceNumber() : (-1)).GetAwaiter();
				if (!awaiter.IsCompleted)
				{
					num = (field_377 = 1);
					field_384 = awaiter;
					field_378.AwaitUnsafeOnCompleted(ref awaiter, ref this);
					return;
				}
				goto IL_00ee;
				end_IL_000e:;
			}
			catch (Exception exception)
			{
				field_377 = -2;
				field_378.SetException(exception);
				return;
			}
			field_377 = -2;
			field_378.SetResult(result);
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_403
			this.Method_403();
		}

		[DebuggerHidden]
		private void Method_404(IAsyncStateMachine arg0)
		{
			field_378.SetStateMachine(arg0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine arg0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_404
			this.Method_404(arg0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	public struct RecoveredType_71 : IAsyncStateMachine
	{
		public int field_385;

		public AsyncTaskMethodBuilder field_386;

		public uint field_387;

		public Vector3 field_388;

		public AutomaticHunting field_389;

		public string field_390;

		private CancellationTokenSource field_391;

		private CancellationToken field_392;

		private RecoveredType_3 field_393;

		private TaskAwaiter<RecoveredType_10> field_394;

		private TaskAwaiter<bool> field_395;

		private TaskAwaiter field_396;

		private void Method_405()
		{
			int num = field_385;
			AutomaticHunting automaticHunting = field_389;
			try
			{
				if ((uint)num > 8u)
				{
					field_391 = new CancellationTokenSource();
				}
				try
				{
					TaskAwaiter<RecoveredType_10> awaiter3;
					TaskAwaiter<bool> awaiter2;
					TaskAwaiter awaiter;
					RecoveredType_10 result;
					switch (num)
					{
					default:
						field_392 = field_391.Token;
						field_393 = new RecoveredType_3(field_387, field_388);
						awaiter3 = automaticHunting.Method_159(field_393, field_392).GetAwaiter();
						if (!awaiter3.IsCompleted)
						{
							num = (field_385 = 0);
							field_394 = awaiter3;
							field_386.AwaitUnsafeOnCompleted(ref awaiter3, ref this);
							return;
						}
						goto IL_00d0;
					case 0:
						awaiter3 = field_394;
						field_394 = default;
						num = (field_385 = -1);
						goto IL_00d0;
					case 1:
						awaiter2 = field_395;
						field_395 = default;
						num = (field_385 = -1);
						goto IL_0155;
					case 2:
						awaiter = field_396;
						field_396 = default;
						num = (field_385 = -1);
						goto IL_01c2;
					case 3:
						awaiter = field_396;
						field_396 = default;
						num = (field_385 = -1);
						goto IL_0231;
					case 4:
						awaiter = field_396;
						field_396 = default;
						num = (field_385 = -1);
						goto IL_02bb;
					case 5:
						awaiter = field_396;
						field_396 = default;
						num = (field_385 = -1);
						goto IL_031d;
					case 6:
						awaiter2 = field_395;
						field_395 = default;
						num = (field_385 = -1);
						goto IL_038e;
					case 7:
						awaiter = field_396;
						field_396 = default;
						num = (field_385 = -1);
						goto IL_0408;
					case 8:
						{
							awaiter = field_396;
							field_396 = default;
							num = (field_385 = -1);
							break;
						}
						IL_0231:
						awaiter.GetResult();
						if (!string.IsNullOrWhiteSpace(field_390))
						{
							automaticHunting.PrintEcho("跨服目标切线: " + field_390);
							awaiter = automaticHunting.Method_108(field_390).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (field_385 = 4);
								field_396 = awaiter;
								field_386.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
							goto IL_02bb;
						}
						goto IL_0324;
						IL_00d0:
						result = awaiter3.GetResult();
						if (result != RecoveredType_10.field_130)
						{
							if (result == RecoveredType_10.field_129)
							{
								automaticHunting.Method_196(RecoveredType_8.field_117, field_391);
								awaiter2 = automaticHunting.Method_163(field_387, field_392).GetAwaiter();
								if (!awaiter2.IsCompleted)
								{
									num = (field_385 = 1);
									field_395 = awaiter2;
									field_386.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
									return;
								}
								goto IL_0155;
							}
							goto IL_0163;
						}
						goto end_IL_001e;
						IL_0324:
						automaticHunting.Method_196(RecoveredType_8.field_118, field_391);
						awaiter2 = automaticHunting.Method_182(field_392).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (field_385 = 6);
							field_395 = awaiter2;
							field_386.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_038e;
						IL_0408:
						awaiter.GetResult();
						awaiter = automaticHunting.Method_164(field_393, field_392, field_391).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (field_385 = 8);
							field_396 = awaiter;
							field_386.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						break;
						IL_02bb:
						awaiter.GetResult();
						awaiter = Task.Delay(3000).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (field_385 = 5);
							field_396 = awaiter;
							field_386.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_031d;
						IL_0155:
						if (awaiter2.GetResult())
						{
							goto IL_0163;
						}
						goto end_IL_001e;
						IL_0163:
						awaiter = automaticHunting.Method_107(TimeSpan.FromSeconds(45L)).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (field_385 = 2);
							field_396 = awaiter;
							field_386.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_01c2;
						IL_038e:
						if (awaiter2.GetResult())
						{
							awaiter = Task.Delay(500, field_392).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (field_385 = 7);
								field_396 = awaiter;
								field_386.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
							goto IL_0408;
						}
						automaticHunting.PrintEcho("召唤坐骑失败，跨服导航已取消");
						goto end_IL_001e;
						IL_01c2:
						awaiter.GetResult();
						awaiter = Task.Delay((int)(automaticHunting.Config.WaitAfterTeleport * 1000f)).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (field_385 = 3);
							field_396 = awaiter;
							field_386.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_0231;
						IL_031d:
						awaiter.GetResult();
						goto IL_0324;
					}
					awaiter.GetResult();
					field_392 = default;
					end_IL_001e:;
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
					if (num < 0)
					{
						field_391.Dispose();
					}
				}
			}
			catch (Exception exception2)
			{
				field_385 = -2;
				field_391 = null;
				field_386.SetException(exception2);
				return;
			}
			field_385 = -2;
			field_391 = null;
			field_386.SetResult();
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_405
			this.Method_405();
		}

		[DebuggerHidden]
		private void Method_406(IAsyncStateMachine arg0)
		{
			field_386.SetStateMachine(arg0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine arg0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_406
			this.Method_406(arg0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	public struct RecoveredType_72 : IAsyncStateMachine
	{
		public int field_397;

		public AsyncTaskMethodBuilder<RecoveredType_10> field_398;

		public AutomaticHunting field_399;

		public RecoveredType_3 field_400;

		public CancellationToken field_401;

		private (RecoveredType_10 Preparation, uint AetheryteId, bool SameTerritory) field_402;

		private TaskAwaiter<(RecoveredType_10 Preparation, uint AetheryteId, bool SameTerritory)> field_403;

		private TaskAwaiter<bool> field_404;

		private void Method_407()
		{
			int num = field_397;
			AutomaticHunting automaticHunting = field_399;
			RecoveredType_10 result2;
			try
			{
				TaskAwaiter<bool> awaiter;
				TaskAwaiter<(RecoveredType_10, uint, bool)> awaiter2;
				if (num != 0)
				{
					if (num == 1)
					{
						awaiter = field_404;
						field_404 = default;
						num = (field_397 = -1);
						goto IL_0135;
					}
					AutomaticHunting field_200 = field_399;
					RecoveredType_3 field_201 = field_400;
					awaiter2 = Framework.RunOnFrameworkThread(() => (Preparation: field_200.Method_160(field_201, out var arg), AetheryteId: arg, SameTerritory: Method_116() == field_201.TerritoryType)).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						num = (field_397 = 0);
						field_403 = awaiter2;
						field_398.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
						return;
					}
				}
				else
				{
					awaiter2 = field_403;
					field_403 = default;
					num = (field_397 = -1);
				}
				(RecoveredType_10, uint, bool) result = awaiter2.GetResult();
				field_402 = result;
				if (field_402.Preparation == RecoveredType_10.field_129)
				{
					awaiter = automaticHunting.Method_123(field_402.AetheryteId, field_401).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (field_397 = 1);
						field_404 = awaiter;
						field_398.AwaitUnsafeOnCompleted(ref awaiter, ref this);
						return;
					}
					goto IL_0135;
				}
				result2 = field_402.Preparation;
				goto end_IL_000e;
				IL_0135:
				if (awaiter.GetResult())
				{
					result2 = RecoveredType_10.field_129;
				}
				else if (field_402.SameTerritory)
				{
					automaticHunting.PrintEcho("无法传送，改用直接导航");
					result2 = RecoveredType_10.field_128;
				}
				else
				{
					automaticHunting.PrintEcho("跨图传送未开始，导航已取消");
					result2 = RecoveredType_10.field_130;
				}
				end_IL_000e:;
			}
			catch (Exception exception)
			{
				field_397 = -2;
				field_398.SetException(exception);
				return;
			}
			field_397 = -2;
			field_398.SetResult(result2);
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_407
			this.Method_407();
		}

		[DebuggerHidden]
		private void Method_408(IAsyncStateMachine arg0)
		{
			field_398.SetStateMachine(arg0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine arg0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_408
			this.Method_408(arg0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	public struct RecoveredType_73 : IAsyncStateMachine
	{
		public int field_405;

		public AsyncTaskMethodBuilder field_406;

		public uint field_407;

		public AutomaticHunting field_408;

		public string field_409;

		public CancellationToken field_410;

		public string field_411;

		private uint field_412;

		private DateTime field_413;

		private DateTime field_414;

		private DateTime field_415;

		private bool field_416;

		private bool field_417;

		private bool field_418;

		private int field_419;

		private int field_420;

		private TaskAwaiter<uint> field_421;

		private TaskAwaiter field_422;

		private TaskAwaiter<string> field_423;

		private bool field_424;

		private bool field_425;

		private TaskAwaiter<(bool, bool)> field_426;

		private void Method_409()
		{
			int num = field_405;
			AutomaticHunting automaticHunting = field_408;
			try
			{
				_ = 11;
				try
				{
					TaskAwaiter<(bool, bool)> awaiter4;
					TaskAwaiter<uint> awaiter3;
					TaskAwaiter<string> awaiter2;
					TaskAwaiter awaiter;
					uint result;
					bool flag;
					uint result2;
					switch (num)
					{
					default:
						if (field_407 != 0)
						{
							field_412 = 0u;
							field_420 = 0;
							goto IL_016d;
						}
						automaticHunting.PrintEcho("无法识别目标服务器: " + field_409 + "，跨服中止");
						goto end_IL_0014;
					case 0:
						awaiter3 = field_421;
						field_421 = default;
						num = (field_405 = -1);
						goto IL_00d9;
					case 1:
						awaiter = field_422;
						field_422 = default;
						num = (field_405 = -1);
						goto IL_0154;
					case 2:
						awaiter2 = field_423;
						field_423 = default;
						num = (field_405 = -1);
						goto IL_01fc;
					case 3:
						awaiter = field_422;
						field_422 = default;
						num = (field_405 = -1);
						goto IL_029b;
					case 4:
						awaiter = field_422;
						field_422 = default;
						num = (field_405 = -1);
						goto IL_031a;
					case 5:
						awaiter = field_422;
						field_422 = default;
						num = (field_405 = -1);
						goto IL_03cf;
					case 6:
						awaiter4 = field_426;
						field_426 = default;
						num = (field_405 = -1);
						goto IL_0450;
					case 7:
						awaiter3 = field_421;
						field_421 = default;
						num = (field_405 = -1);
						goto IL_04ff;
					case 8:
						awaiter2 = field_423;
						field_423 = default;
						num = (field_405 = -1);
						goto IL_0584;
					case 9:
						awaiter = field_422;
						field_422 = default;
						num = (field_405 = -1);
						goto IL_0648;
					case 10:
						awaiter = field_422;
						field_422 = default;
						num = (field_405 = -1);
						goto IL_07e2;
					case 11:
						{
							awaiter = field_422;
							field_422 = default;
							num = (field_405 = -1);
							break;
						}
						IL_029b:
						awaiter.GetResult();
						goto end_IL_0014;
						IL_0450:
						(field_424, field_425) = awaiter4.GetResult();
						if (field_424 && !field_417)
						{
							field_417 = true;
							automaticHunting.PrintEcho("检测到跨服状态92(ReadyingVisitOtherWorld)：跨服指令已生效");
						}
						if (field_424 | field_425)
						{
							field_418 = true;
						}
						awaiter3 = automaticHunting.Method_109().GetAwaiter();
						if (!awaiter3.IsCompleted)
						{
							num = (field_405 = 7);
							field_421 = awaiter3;
							field_406.AwaitUnsafeOnCompleted(ref awaiter3, ref this);
							return;
						}
						goto IL_04ff;
						IL_016d:
						if (field_420 < 3)
						{
							awaiter3 = automaticHunting.Method_109().GetAwaiter();
							if (!awaiter3.IsCompleted)
							{
								num = (field_405 = 0);
								field_421 = awaiter3;
								field_406.AwaitUnsafeOnCompleted(ref awaiter3, ref this);
								return;
							}
							goto IL_00d9;
						}
						goto IL_0179;
						IL_0598:
						if (flag)
						{
							field_416 = true;
							goto IL_0768;
						}
						if (!field_418 && DateTime.UtcNow - field_414 > TimeSpan.FromSeconds(5L) && !field_424 && !field_425)
						{
							awaiter = automaticHunting.Method_108(field_411).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (field_405 = 9);
								field_422 = awaiter;
								field_406.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
							goto IL_0648;
						}
						goto IL_06c8;
						IL_031a:
						awaiter.GetResult();
						field_413 = DateTime.UtcNow;
						field_414 = DateTime.UtcNow;
						field_415 = DateTime.UtcNow;
						field_416 = false;
						field_417 = false;
						field_418 = false;
						field_419 = 0;
						goto IL_0746;
						IL_00d9:
						result = awaiter3.GetResult();
						field_412 = result;
						if (field_412 == 0)
						{
							awaiter = Task.Delay(1000, field_410).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (field_405 = 1);
								field_422 = awaiter;
								field_406.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
							goto IL_0154;
						}
						goto IL_0179;
						IL_0746:
						if (DateTime.UtcNow - field_413 < TimeSpan.FromMinutes(10L))
						{
							field_410.ThrowIfCancellationRequested();
							awaiter = Task.Delay(200, field_410).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (field_405 = 5);
								field_422 = awaiter;
								field_406.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
							goto IL_03cf;
						}
						goto IL_0768;
						IL_0648:
						awaiter.GetResult();
						field_414 = DateTime.UtcNow;
						field_419++;
						if (field_419 == 1 || field_419 % 6 == 0)
						{
							automaticHunting.PrintEcho($"跨服指令未受理，重发第{field_419}次: {field_411}");
						}
						goto IL_06c8;
						IL_0154:
						awaiter.GetResult();
						field_420++;
						goto IL_016d;
						IL_0179:
						if (field_412 != 0)
						{
							flag = field_412 == field_407;
							if (!flag)
							{
								awaiter2 = automaticHunting.Method_110().GetAwaiter();
								if (!awaiter2.IsCompleted)
								{
									num = (field_405 = 2);
									field_423 = awaiter2;
									field_406.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
									return;
								}
								goto IL_01fc;
							}
							goto IL_0210;
						}
						automaticHunting.PrintEcho("无法读取当前服务器，跨服中止");
						goto end_IL_0014;
						IL_06c8:
						if (DateTime.UtcNow - field_415 > TimeSpan.FromSeconds(60L))
						{
							field_415 = DateTime.UtcNow;
							int value = (int)(DateTime.UtcNow - field_413).TotalSeconds;
							automaticHunting.PrintEcho($"跨服等待中: 已等待{value}/600秒");
						}
						goto IL_0746;
						IL_03cf:
						awaiter.GetResult();
						awaiter4 = Framework.RunOnFrameworkThread(() => (Condition[ConditionFlag.ReadyingVisitOtherWorld], Condition[ConditionFlag.BetweenAreas] || Condition[ConditionFlag.BetweenAreas51])).GetAwaiter();
						if (!awaiter4.IsCompleted)
						{
							num = (field_405 = 6);
							field_426 = awaiter4;
							field_406.AwaitUnsafeOnCompleted(ref awaiter4, ref this);
							return;
						}
						goto IL_0450;
						IL_0584:
						flag = awaiter2.GetResult() == field_409;
						goto IL_0598;
						IL_07e2:
						awaiter.GetResult();
						automaticHunting.PrintEcho("跨服完成，已到达服务器[" + field_409 + "]");
						automaticHunting.Method_97(field_409);
						awaiter = automaticHunting.Method_104(field_410).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (field_405 = 11);
							field_422 = awaiter;
							field_406.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						break;
						IL_01fc:
						flag = awaiter2.GetResult() == field_409;
						goto IL_0210;
						IL_0210:
						if (flag)
						{
							automaticHunting.PrintEcho("已在服务器[" + field_409 + "]，跳过跨服");
							automaticHunting.Method_97(field_409);
							awaiter = automaticHunting.Method_104(field_410).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (field_405 = 3);
								field_422 = awaiter;
								field_406.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
							goto IL_029b;
						}
						automaticHunting.PrintEcho("执行跨服: " + field_411);
						awaiter = automaticHunting.Method_108(field_411).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (field_405 = 4);
							field_422 = awaiter;
							field_406.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_031a;
						IL_04ff:
						result2 = awaiter3.GetResult();
						if (result2 != 0 && result2 == field_407)
						{
							field_416 = true;
							goto IL_0768;
						}
						flag = result2 != 0;
						if (flag)
						{
							awaiter2 = automaticHunting.Method_110().GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								num = (field_405 = 8);
								field_423 = awaiter2;
								field_406.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
								return;
							}
							goto IL_0584;
						}
						goto IL_0598;
						IL_0768:
						if (field_416)
						{
							awaiter = Task.Delay(5000, field_410).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (field_405 = 10);
								field_422 = awaiter;
								field_406.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
							goto IL_07e2;
						}
						automaticHunting.PrintEcho("跨服超时未到达目标服务器(10分钟)，流程中止");
						goto end_IL_0014;
					}
					awaiter.GetResult();
					end_IL_0014:;
				}
				catch (OperationCanceledException)
				{
					Log.Information("AutomaticHunting: 跨服流程被手动中断");
					automaticHunting.PrintEcho("跨服流程已中断");
				}
				catch (Exception ex2)
				{
					Log.Warning(ex2, "AutomaticHunting: 跨服流程异常");
					automaticHunting.PrintEcho("跨服流程异常: " + ex2.Message);
				}
				finally
				{
					if (num < 0)
					{
						object field_ = automaticHunting.field_62;
						bool flag = false;
						try
						{
							Monitor.Enter(field_, ref flag);
							automaticHunting.field_65 = false;
							automaticHunting.field_66 = DateTime.MinValue;
							automaticHunting.field_67 = 0u;
							automaticHunting.field_68 = default;
							automaticHunting.field_69 = null;
							automaticHunting.field_70 = 0u;
							automaticHunting.field_71 = 0u;
							automaticHunting.field_72 = default;
							automaticHunting.field_64?.Dispose();
							automaticHunting.field_64 = null;
							automaticHunting.field_63 = null;
						}
						finally
						{
							if (num < 0 && flag)
							{
								Monitor.Exit(field_);
							}
						}
					}
				}
			}
			catch (Exception exception)
			{
				field_405 = -2;
				field_406.SetException(exception);
				return;
			}
			field_405 = -2;
			field_406.SetResult();
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_409
			this.Method_409();
		}

		[DebuggerHidden]
		private void Method_410(IAsyncStateMachine arg0)
		{
			field_406.SetStateMachine(arg0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine arg0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_410
			this.Method_410(arg0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	public struct RecoveredType_74 : IAsyncStateMachine
	{
		public int field_427;

		public AsyncTaskMethodBuilder field_428;

		public CancellationTokenSource field_429;

		public uint field_430;

		public Vector3 field_431;

		public AutomaticHunting field_432;

		private CancellationToken field_433;

		private TaskAwaiter<RecoveredType_10> field_434;

		private TaskAwaiter<bool> field_435;

		private TaskAwaiter field_436;

		private void Method_411()
		{
			int num = field_427;
			AutomaticHunting automaticHunting = field_432;
			try
			{
				_ = 5;
				try
				{
					TaskAwaiter<RecoveredType_10> awaiter3;
					TaskAwaiter<bool> awaiter2;
					TaskAwaiter awaiter;
					RecoveredType_10 result;
					switch (num)
					{
					default:
					{
						field_433 = field_429.Token;
						RecoveredType_3 arg = new RecoveredType_3(field_430, field_431);
						awaiter3 = automaticHunting.Method_159(arg, field_433).GetAwaiter();
						if (!awaiter3.IsCompleted)
						{
							num = (field_427 = 0);
							field_434 = awaiter3;
							field_428.AwaitUnsafeOnCompleted(ref awaiter3, ref this);
							return;
						}
						goto IL_00b3;
					}
					case 0:
						awaiter3 = field_434;
						field_434 = default;
						num = (field_427 = -1);
						goto IL_00b3;
					case 1:
						awaiter2 = field_435;
						field_435 = default;
						num = (field_427 = -1);
						goto IL_0138;
					case 2:
						awaiter2 = field_435;
						field_435 = default;
						num = (field_427 = -1);
						goto IL_01b0;
					case 3:
						awaiter = field_436;
						field_436 = default;
						num = (field_427 = -1);
						goto IL_0227;
					case 4:
						awaiter2 = field_435;
						field_435 = default;
						num = (field_427 = -1);
						goto IL_028b;
					case 5:
						{
							awaiter = field_436;
							field_436 = default;
							num = (field_427 = -1);
							break;
						}
						IL_0146:
						automaticHunting.Method_196(RecoveredType_8.field_118, field_429);
						awaiter2 = automaticHunting.Method_182(field_433).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (field_427 = 2);
							field_435 = awaiter2;
							field_428.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_01b0;
						IL_00b3:
						result = awaiter3.GetResult();
						if (result != RecoveredType_10.field_130)
						{
							if (result == RecoveredType_10.field_129)
							{
								automaticHunting.Method_196(RecoveredType_8.field_117, field_429);
								awaiter2 = automaticHunting.Method_163(field_430, field_433).GetAwaiter();
								if (!awaiter2.IsCompleted)
								{
									num = (field_427 = 1);
									field_435 = awaiter2;
									field_428.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
									return;
								}
								goto IL_0138;
							}
							goto IL_0146;
						}
						goto end_IL_0013;
						IL_0227:
						awaiter.GetResult();
						awaiter2 = automaticHunting.Method_181(field_433).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (field_427 = 4);
							field_435 = awaiter2;
							field_428.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_028b;
						IL_01b0:
						if (awaiter2.GetResult())
						{
							awaiter = Task.Delay(100, field_433).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (field_427 = 3);
								field_436 = awaiter;
								field_428.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
							goto IL_0227;
						}
						automaticHunting.PrintEcho("召唤坐骑失败，导航已取消");
						goto end_IL_0013;
						IL_028b:
						if (awaiter2.GetResult())
						{
							automaticHunting.Method_196(RecoveredType_8.field_119, field_429);
							automaticHunting.PrintEcho("开始导航: /vnav flyflag");
							automaticHunting.Method_179("/vnav flyflag");
							awaiter = automaticHunting.Method_193(field_431, field_429, field_433).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (field_427 = 5);
								field_436 = awaiter;
								field_428.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
							break;
						}
						automaticHunting.PrintEcho("启动导航失败: vnavmesh未就绪");
						goto end_IL_0013;
						IL_0138:
						if (awaiter2.GetResult())
						{
							goto IL_0146;
						}
						goto end_IL_0013;
					}
					awaiter.GetResult();
					field_433 = default;
					end_IL_0013:;
				}
				catch (OperationCanceledException)
				{
					Log.Information("AutomaticHunting: FlyFlag navigation cancelled");
				}
				catch (Exception ex2)
				{
					Log.Warning(ex2, "AutomaticHunting: FlyFlag navigation failed");
					automaticHunting.PrintEcho("导航异常: " + ex2.Message);
				}
				finally
				{
					if (num < 0)
					{
						object field_ = automaticHunting.field_53;
						bool lockTaken = false;
						try
						{
							Monitor.Enter(field_, ref lockTaken);
							if (automaticHunting.field_54 == field_429)
							{
								automaticHunting.field_54 = null;
								automaticHunting.field_55 = null;
								automaticHunting.field_56 = RecoveredType_8.field_116;
							}
						}
						finally
						{
							if (num < 0 && lockTaken)
							{
								Monitor.Exit(field_);
							}
						}
						field_429.Dispose();
					}
				}
			}
			catch (Exception exception)
			{
				field_427 = -2;
				field_428.SetException(exception);
				return;
			}
			field_427 = -2;
			field_428.SetResult();
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_411
			this.Method_411();
		}

		[DebuggerHidden]
		private void Method_412(IAsyncStateMachine arg0)
		{
			field_428.SetStateMachine(arg0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine arg0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_412
			this.Method_412(arg0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	public struct RecoveredType_75 : IAsyncStateMachine
	{
		public int field_437;

		public AsyncTaskMethodBuilder field_438;

		public CancellationTokenSource field_439;

		public AutomaticHunting field_440;

		public RecoveredType_3 field_441;

		private CancellationToken field_442;

		private TaskAwaiter<RecoveredType_10> field_443;

		private TaskAwaiter<bool> field_444;

		private TaskAwaiter field_445;

		private void Method_413()
		{
			int num = field_437;
			AutomaticHunting automaticHunting = field_440;
			try
			{
				_ = 4;
				try
				{
					TaskAwaiter<RecoveredType_10> awaiter3;
					TaskAwaiter<bool> awaiter2;
					TaskAwaiter awaiter;
					RecoveredType_10 result;
					switch (num)
					{
					default:
						field_442 = field_439.Token;
						awaiter3 = automaticHunting.Method_159(field_441, field_442).GetAwaiter();
						if (!awaiter3.IsCompleted)
						{
							num = (field_437 = 0);
							field_443 = awaiter3;
							field_438.AwaitUnsafeOnCompleted(ref awaiter3, ref this);
							return;
						}
						goto IL_009e;
					case 0:
						awaiter3 = field_443;
						field_443 = default;
						num = (field_437 = -1);
						goto IL_009e;
					case 1:
						awaiter2 = field_444;
						field_444 = default;
						num = (field_437 = -1);
						goto IL_012b;
					case 2:
						awaiter2 = field_444;
						field_444 = default;
						num = (field_437 = -1);
						goto IL_01ae;
					case 3:
						awaiter = field_445;
						field_445 = default;
						num = (field_437 = -1);
						goto IL_0225;
					case 4:
						{
							awaiter = field_445;
							field_445 = default;
							num = (field_437 = -1);
							break;
						}
						IL_012b:
						if (awaiter2.GetResult())
						{
							goto IL_0139;
						}
						goto end_IL_0013;
						IL_009e:
						result = awaiter3.GetResult();
						if (result != RecoveredType_10.field_130)
						{
							if (result == RecoveredType_10.field_129)
							{
								automaticHunting.Method_196(RecoveredType_8.field_117, field_439);
								awaiter2 = automaticHunting.Method_163(field_441.TerritoryType, field_442).GetAwaiter();
								if (!awaiter2.IsCompleted)
								{
									num = (field_437 = 1);
									field_444 = awaiter2;
									field_438.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
									return;
								}
								goto IL_012b;
							}
							goto IL_0139;
						}
						goto end_IL_0013;
						IL_0139:
						field_442.ThrowIfCancellationRequested();
						automaticHunting.Method_196(RecoveredType_8.field_118, field_439);
						awaiter2 = automaticHunting.Method_182(field_442).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (field_437 = 2);
							field_444 = awaiter2;
							field_438.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_01ae;
						IL_0225:
						awaiter.GetResult();
						awaiter = automaticHunting.Method_164(field_441, field_442, field_439).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (field_437 = 4);
							field_445 = awaiter;
							field_438.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						break;
						IL_01ae:
						if (awaiter2.GetResult())
						{
							awaiter = Task.Delay(100, field_442).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (field_437 = 3);
								field_445 = awaiter;
								field_438.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
							goto IL_0225;
						}
						automaticHunting.PrintEcho("召唤坐骑失败，导航已取消");
						goto end_IL_0013;
					}
					awaiter.GetResult();
					field_442 = default;
					end_IL_0013:;
				}
				catch (OperationCanceledException)
				{
					Log.Information("AutomaticHunting: Navigation cancelled");
					automaticHunting.FinishSMonitor("已停止");
				}
				catch (Exception ex2)
				{
					Log.Warning(ex2, "AutomaticHunting: Navigation workflow failed");
					automaticHunting.PrintEcho("导航异常: " + ex2.Message);
					automaticHunting.FinishSMonitor("导航异常");
				}
				finally
				{
					if (num < 0)
					{
						object field_ = automaticHunting.field_53;
						bool lockTaken = false;
						try
						{
							Monitor.Enter(field_, ref lockTaken);
							if (automaticHunting.field_54 == field_439)
							{
								automaticHunting.field_54 = null;
								automaticHunting.field_55 = null;
								automaticHunting.field_56 = RecoveredType_8.field_116;
							}
						}
						finally
						{
							if (num < 0 && lockTaken)
							{
								Monitor.Exit(field_);
							}
						}
						field_439.Dispose();
					}
				}
			}
			catch (Exception exception)
			{
				field_437 = -2;
				field_438.SetException(exception);
				return;
			}
			field_437 = -2;
			field_438.SetResult();
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_413
			this.Method_413();
		}

		[DebuggerHidden]
		private void Method_414(IAsyncStateMachine arg0)
		{
			field_438.SetStateMachine(arg0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine arg0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_414
			this.Method_414(arg0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	public struct RecoveredType_76 : IAsyncStateMachine
	{
		public int field_446;

		public AsyncTaskMethodBuilder field_447;

		public AutomaticHunting field_448;

		public CancellationTokenSource field_449;

		public RecoveredType_5 field_450;

		private RecoveredType_39 field_451;

		public CancellationToken field_452;

		private RecoveredType_40 field_453;

		private bool field_454;

		private int field_455;

		private TaskAwaiter<bool> field_456;

		private TaskAwaiter field_457;

		private float field_458;

		private TaskAwaiter<Vector3?> field_459;

		private TaskAwaiter<RecoveredType_5?> field_460;

		private float field_461;

		private float field_462;

		private float field_463;

		private float field_464;

		private bool field_465;

		private DateTime field_466;

		private DateTime field_467;

		private TaskAwaiter<string> field_468;

		private (int kind, float hp, Vector3 pos, Vector3? player, bool isS) field_469;

		private TaskAwaiter<(int kind, float hp, Vector3 pos, Vector3? player, bool isS)> field_470;

		private float field_471;

		private void Method_415()
		{
			int num = field_446;
			AutomaticHunting CS_0024_003C_003E8__locals41 = field_448;
			try
			{
				if ((uint)num > 10u)
				{
					CS_0024_003C_003E8__locals41.field_33 = true;
				}
				try
				{
					TaskAwaiter<Vector3?> awaiter6;
					TaskAwaiter<RecoveredType_5?> awaiter5;
					TaskAwaiter<string> awaiter4;
					TaskAwaiter<(int, float, Vector3, Vector3?, bool)> awaiter3;
					TaskAwaiter<bool> awaiter2;
					TaskAwaiter awaiter;
					bool result;
					Vector3? result2;
					bool result3;
					RecoveredType_5? result4;
					string result5;
					(int, float, Vector3, Vector3?, bool) result6;
					SMonitorStatus sMonitor2;
					float item;
					SMonitorStatus sMonitor3;
					switch (num)
					{
					default:
						field_451 = new RecoveredType_39();
						field_451.field_214 = CS_0024_003C_003E8__locals41;
						CS_0024_003C_003E8__locals41.Method_196(RecoveredType_8.field_121, field_449);
						CS_0024_003C_003E8__locals41.BeginSMonitor(field_450.Name, "前往S怪", field_450.HpPercent, CS_0024_003C_003E8__locals41.Config.SHpThreshold);
						field_451.field_213 = new Vector3(field_450.Position.X, field_450.Position.Y + CS_0024_003C_003E8__locals41.Config.SHoverHeight, field_450.Position.Z);
						field_454 = false;
						field_455 = 1;
						goto IL_0230;
					case 0:
						awaiter2 = field_456;
						field_456 = default;
						num = (field_446 = -1);
						goto IL_0186;
					case 1:
						awaiter = field_457;
						field_457 = default;
						num = (field_446 = -1);
						goto IL_0217;
					case 2:
						awaiter = field_457;
						field_457 = default;
						num = (field_446 = -1);
						goto IL_032e;
					case 3:
						awaiter6 = field_459;
						field_459 = default;
						num = (field_446 = -1);
						goto IL_03ab;
					case 4:
						awaiter5 = field_460;
						field_460 = default;
						num = (field_446 = -1);
						goto IL_0444;
					case 5:
						awaiter = field_457;
						field_457 = default;
						num = (field_446 = -1);
						goto IL_054c;
					case 6:
						awaiter4 = field_468;
						field_468 = default;
						num = (field_446 = -1);
						goto IL_05f0;
					case 7:
						awaiter = field_457;
						field_457 = default;
						num = (field_446 = -1);
						goto IL_076a;
					case 8:
						awaiter3 = field_470;
						field_470 = default;
						num = (field_446 = -1);
						goto IL_07ea;
					case 9:
						awaiter2 = field_456;
						field_456 = default;
						num = (field_446 = -1);
						goto IL_09d7;
					case 10:
						{
							awaiter = field_457;
							field_457 = default;
							num = (field_446 = -1);
							goto IL_0b6f;
						}
						IL_0230:
						if (field_455 <= 2)
						{
							awaiter2 = Framework.RunOnFrameworkThread(() =>
							{
								Vector3? vector = field_451.field_214.Method_118(field_451.field_213);
								Vector3 arg = (vector.HasValue ? new Vector3(vector.Value.X, field_451.field_213.Y, vector.Value.Z) : field_451.field_213);
								ICallGateSubscriber<bool>? field_221 = field_451.field_214.field_24;
								return field_221 != null && field_221.InvokeFunc() && (field_451.field_214.field_25?.InvokeFunc(arg, arg2: true) ?? false);
							}).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								num = (field_446 = 0);
								field_456 = awaiter2;
								field_447.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
								return;
							}
							goto IL_0186;
						}
						goto IL_023c;
						IL_032e:
						awaiter.GetResult();
						awaiter6 = Framework.RunOnFrameworkThread((Func<Vector3?>)GetLocalPlayerPosition).GetAwaiter();
						if (!awaiter6.IsCompleted)
						{
							num = (field_446 = 3);
							field_459 = awaiter6;
							field_447.AwaitUnsafeOnCompleted(ref awaiter6, ref this);
							return;
						}
						goto IL_03ab;
						IL_076a:
						awaiter.GetResult();
						field_463 += 0.5f;
						awaiter3 = Framework.RunOnFrameworkThread(() =>
						{
							Vector3? localPlayerPosition = GetLocalPlayerPosition();
							if (TargetManager.Target is IBattleChara { MaxHp: >0u, CurrentHp: >0u } battleChara)
							{
								return (kind: 1, hp: (float)battleChara.CurrentHp / (float)battleChara.MaxHp * 100f, pos: battleChara.Position, player: localPlayerPosition, isS: CS_0024_003C_003E8__locals41.Method_203(battleChara.Name.TextValue));
							}
							RecoveredType_5? recoveredType_ = CS_0024_003C_003E8__locals41.Method_202();
							if (recoveredType_.HasValue)
							{
								if (recoveredType_.Value.HpPercent <= 0f)
								{
									return (kind: 2, hp: 0f, pos: recoveredType_.Value.Position, player: localPlayerPosition, isS: true);
								}
								CS_0024_003C_003E8__locals41.Method_204(recoveredType_.Value);
								return (kind: 3, hp: recoveredType_.Value.HpPercent, pos: recoveredType_.Value.Position, player: localPlayerPosition, isS: true);
							}
							return (kind: 0, hp: -1f, pos: Vector3.Zero, player: localPlayerPosition, isS: false);
						}).GetAwaiter();
						if (!awaiter3.IsCompleted)
						{
							num = (field_446 = 8);
							field_470 = awaiter3;
							field_447.AwaitUnsafeOnCompleted(ref awaiter3, ref this);
							return;
						}
						goto IL_07ea;
						IL_054c:
						awaiter.GetResult();
						CS_0024_003C_003E8__locals41.FinishSMonitor("已开怪");
						break;
						IL_0b6f:
						awaiter.GetResult();
						CS_0024_003C_003E8__locals41.FinishSMonitor("已开怪");
						break;
						IL_0186:
						result = awaiter2.GetResult();
						field_454 = result;
						if (!field_454)
						{
							if (field_455 < 2)
							{
								CS_0024_003C_003E8__locals41.PrintEcho("S怪寻路失败，2秒后重新寻路...");
								awaiter = Task.Delay(TimeSpan.FromSeconds(2L), field_452).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									num = (field_446 = 1);
									field_457 = awaiter;
									field_447.AwaitUnsafeOnCompleted(ref awaiter, ref this);
									return;
								}
								goto IL_0217;
							}
							goto IL_021e;
						}
						goto IL_023c;
						IL_03ab:
						result2 = awaiter6.GetResult();
						if (result2.HasValue)
						{
							field_458 = Method_197(result2.Value, field_450.Position);
							awaiter5 = Framework.RunOnFrameworkThread((Func<RecoveredType_5?>)CS_0024_003C_003E8__locals41.Method_202).GetAwaiter();
							if (!awaiter5.IsCompleted)
							{
								num = (field_446 = 4);
								field_460 = awaiter5;
								field_447.AwaitUnsafeOnCompleted(ref awaiter5, ref this);
								return;
							}
							goto IL_0444;
						}
						goto IL_0c80;
						IL_09d7:
						result3 = awaiter2.GetResult();
						field_466 = DateTime.UtcNow;
						if (result3 && (DateTime.UtcNow - field_467).TotalSeconds >= 10.0)
						{
							field_467 = DateTime.UtcNow;
							CS_0024_003C_003E8__locals41.PrintEcho($"S怪已移动，距离 {field_471:0.0}m，重新悬停跟随");
						}
						goto IL_0a5b;
						IL_0c80:
						field_455++;
						goto IL_0c92;
						IL_0217:
						awaiter.GetResult();
						goto IL_021e;
						IL_021e:
						field_455++;
						goto IL_0230;
						IL_023c:
						if (!field_454)
						{
							CS_0024_003C_003E8__locals41.PrintEcho("S怪导航失败: vnavmesh未就绪或无法规划路径");
							CS_0024_003C_003E8__locals41.FinishSMonitor("寻路失败");
							break;
						}
						CS_0024_003C_003E8__locals41.PrintEcho($"S怪导航已开始(悬停高度{CS_0024_003C_003E8__locals41.Config.SHoverHeight:0}m)，悬停期间监控血量...");
						field_455 = 0;
						goto IL_0c92;
						IL_0444:
						result4 = awaiter5.GetResult();
						field_453.field_216 = result4;
						if (field_453.field_216.HasValue)
						{
							SMonitorStatus sMonitor = CS_0024_003C_003E8__locals41.SMonitor;
							if (sMonitor != null)
							{
								sMonitor.HpPercent = field_453.field_216.Value.HpPercent;
							}
							if (field_453.field_216.Value.HpPercent <= CS_0024_003C_003E8__locals41.Config.SHpThreshold)
							{
								awaiter = CS_0024_003C_003E8__locals41.Method_166(field_453.field_216.Value, field_453.field_216.Value.HpPercent, field_452).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									num = (field_446 = 5);
									field_457 = awaiter;
									field_447.AwaitUnsafeOnCompleted(ref awaiter, ref this);
									return;
								}
								goto IL_054c;
							}
							if (field_458 <= CS_0024_003C_003E8__locals41.Config.SHoverDistance)
							{
								CS_0024_003C_003E8__locals41.Method_179("/vnav stop");
								awaiter4 = Framework.RunOnFrameworkThread(() => field_453.field_217.field_214.Method_204(field_453.field_216.Value)).GetAwaiter();
								if (!awaiter4.IsCompleted)
								{
									num = (field_446 = 6);
									field_468 = awaiter4;
									field_447.AwaitUnsafeOnCompleted(ref awaiter4, ref this);
									return;
								}
								goto IL_05f0;
							}
							field_453 = null;
						}
						goto IL_0c80;
						IL_05f0:
						result5 = awaiter4.GetResult();
						CS_0024_003C_003E8__locals41.PrintEcho($"已悬停到S怪附近({field_458:0.0}m)，已选中S怪:{result5 ?? field_453.field_216.Value.Name}，当前血量:{field_453.field_216.Value.HpPercent:0.0}%，等待血量降至{CS_0024_003C_003E8__locals41.Config.SHpThreshold:0}%");
						field_461 = -1f;
						field_462 = 0f;
						field_463 = 0f;
						field_464 = 0f;
						field_465 = false;
						field_466 = DateTime.MinValue;
						field_467 = DateTime.MinValue;
						goto IL_0709;
						IL_0c92:
						if (field_455 < 120)
						{
							field_453 = new RecoveredType_40();
							field_453.field_217 = field_451;
							awaiter = Task.Delay(500, field_452).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (field_446 = 2);
								field_457 = awaiter;
								field_447.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
							goto IL_032e;
						}
						CS_0024_003C_003E8__locals41.PrintEcho("S怪悬停监控超时");
						CS_0024_003C_003E8__locals41.FinishSMonitor("悬停导航超时");
						field_451 = null;
						break;
						IL_07ea:
						result6 = awaiter3.GetResult();
						field_469 = result6;
						if (field_469.kind == 2)
						{
							CS_0024_003C_003E8__locals41.PrintEcho("S怪已死亡，悬停监控结束");
							CS_0024_003C_003E8__locals41.FinishSMonitor("S怪已死亡");
							break;
						}
						if (field_469.kind == 1 || field_469.kind == 3)
						{
							field_464 = 0f;
							if (field_465 && field_469.kind == 3)
							{
								CS_0024_003C_003E8__locals41.PrintEcho("目标已重新选中: " + field_450.Name);
							}
							field_465 = false;
							field_471 = ((field_469.player.HasValue && field_469.isS) ? Method_197(field_469.player.Value, field_469.pos) : 0f);
							if (field_471 > CS_0024_003C_003E8__locals41.Config.SHoverDistance + 5f && (DateTime.UtcNow - field_466).TotalSeconds >= 3.0)
							{
								RecoveredType_40 field_219 = field_453;
								Vector3 field_220 = new Vector3(field_469.pos.X, field_469.pos.Y + CS_0024_003C_003E8__locals41.Config.SHoverHeight, field_469.pos.Z);
								awaiter2 = Framework.RunOnFrameworkThread(() =>
								{
									Vector3? vector = field_219.field_217.field_214.Method_118(field_220);
									Vector3 arg = (vector.HasValue ? new Vector3(vector.Value.X, field_220.Y, vector.Value.Z) : field_220);
									ICallGateSubscriber<bool>? field_221 = field_219.field_217.field_214.field_24;
									return field_221 != null && field_221.InvokeFunc() && (field_219.field_217.field_214.field_25?.InvokeFunc(arg, arg2: true) ?? false);
								}).GetAwaiter();
								if (!awaiter2.IsCompleted)
								{
									num = (field_446 = 9);
									field_456 = awaiter2;
									field_447.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
									return;
								}
								goto IL_09d7;
							}
							goto IL_0a5b;
						}
						field_464 += 0.5f;
						field_465 = true;
						sMonitor2 = CS_0024_003C_003E8__locals41.SMonitor;
						if (sMonitor2 != null)
						{
							sMonitor2.Phase = "目标丢失";
							sMonitor2.TargetLost = true;
							sMonitor2.LostSeconds = field_464;
						}
						if (field_464 >= 30f)
						{
							CS_0024_003C_003E8__locals41.PrintEcho("目标丢失30秒，悬停监控结束");
							CS_0024_003C_003E8__locals41.FinishSMonitor("目标丢失30秒");
							break;
						}
						goto IL_0709;
						IL_0a5b:
						item = field_469.hp;
						sMonitor3 = CS_0024_003C_003E8__locals41.SMonitor;
						if (sMonitor3 != null)
						{
							sMonitor3.Phase = "悬停监控";
							sMonitor3.HpPercent = item;
							sMonitor3.WaitedSeconds = field_463;
							sMonitor3.TargetLost = false;
						}
						if (Math.Abs(item - field_461) >= 5f)
						{
							field_461 = item;
							CS_0024_003C_003E8__locals41.PrintEcho($"悬停监控中: S怪血量 {item:0.0}%");
						}
						if (item <= CS_0024_003C_003E8__locals41.Config.SHpThreshold)
						{
							awaiter = CS_0024_003C_003E8__locals41.Method_166(field_450, item, field_452).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (field_446 = 10);
								field_457 = awaiter;
								field_447.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
							goto IL_0b6f;
						}
						if (field_463 - field_462 >= 60f)
						{
							field_462 = field_463;
							CS_0024_003C_003E8__locals41.PrintEcho($"悬停监控中: 已等待{field_463:0}秒, S怪血量 {item:0.0}%");
						}
						goto IL_0709;
						IL_0709:
						awaiter = Task.Delay(500, field_452).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (field_446 = 7);
							field_457 = awaiter;
							field_447.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_076a;
					}
				}
				finally
				{
					if (num < 0)
					{
						CS_0024_003C_003E8__locals41.field_33 = false;
					}
				}
			}
			catch (Exception exception)
			{
				field_446 = -2;
				field_447.SetException(exception);
				return;
			}
			field_446 = -2;
			field_447.SetResult();
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_415
			this.Method_415();
		}

		[DebuggerHidden]
		private void Method_416(IAsyncStateMachine arg0)
		{
			field_447.SetStateMachine(arg0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine arg0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_416
			this.Method_416(arg0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	public struct RecoveredType_77 : IAsyncStateMachine
	{
		public int field_472;

		public AsyncTaskMethodBuilder<bool> field_473;

		public AutomaticHunting field_474;

		public CancellationToken field_475;

		public uint field_476;

		private RecoveredType_26 field_477;

		private DateTime field_478;

		private DateTime field_479;

		private TaskAwaiter field_480;

		private TaskAwaiter<(bool Between, bool Foreign)> field_481;

		private void Method_417()
		{
			int num = field_472;
			AutomaticHunting automaticHunting = field_474;
			bool result;
			try
			{
				TaskAwaiter<(bool, bool)> awaiter2;
				TaskAwaiter awaiter;
				(bool, bool) result2;
				switch (num)
				{
				default:
					field_477 = new RecoveredType_26();
					field_477.field_177 = field_474;
					field_477.field_178 = field_475;
					field_477.field_185 = field_476;
					awaiter = Framework.RunOnFrameworkThread(() =>
					{
						field_477.field_177.Method_179("/vnav stop");
					}).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (field_472 = 0);
						field_480 = awaiter;
						field_473.AwaitUnsafeOnCompleted(ref awaiter, ref this);
						return;
					}
					goto IL_00cb;
				case 0:
					awaiter = field_480;
					field_480 = default;
					num = (field_472 = -1);
					goto IL_00cb;
				case 1:
					awaiter2 = field_481;
					field_481 = default;
					num = (field_472 = -1);
					goto IL_01f3;
				case 2:
					awaiter = field_480;
					field_480 = default;
					num = (field_472 = -1);
					goto IL_0275;
				case 3:
					{
						awaiter = field_480;
						field_480 = default;
						num = (field_472 = -1);
						goto IL_036f;
					}
					IL_0275:
					awaiter.GetResult();
					goto IL_0376;
					IL_00cb:
					awaiter.GetResult();
					field_478 = DateTime.UtcNow;
					field_477.field_183 = field_478;
					field_479 = field_478.AddSeconds(10.0);
					field_477.field_182 = null;
					field_477.field_184 = 0;
					field_477.field_179 = 0;
					field_477.field_180 = null;
					field_477.field_181 = null;
					goto IL_0376;
					IL_0376:
					if (DateTime.UtcNow - field_478 < TimeSpan.FromSeconds(60L))
					{
						field_477.field_178.ThrowIfCancellationRequested();
						awaiter2 = Framework.RunOnFrameworkThread(() =>
						{
							field_477.field_178.ThrowIfCancellationRequested();
							if (Condition[ConditionFlag.BetweenAreas] || Condition[ConditionFlag.BetweenAreas51])
							{
								return (Between: field_477.field_179 > 0, Foreign: field_477.field_179 == 0);
							}
							if (Method_121())
							{
								DateTime valueOrDefault = field_477.field_180.GetValueOrDefault();
								if (!field_477.field_180.HasValue)
								{
									valueOrDefault = DateTime.UtcNow;
									field_477.field_180 = valueOrDefault;
								}
								field_477.field_181 = null;
								return (Between: false, Foreign: false);
							}
							if (field_477.field_180.HasValue)
							{
								DateTime valueOrDefault = field_477.field_181.GetValueOrDefault();
								if (!field_477.field_181.HasValue)
								{
									valueOrDefault = DateTime.UtcNow;
									field_477.field_181 = valueOrDefault;
								}
								if (DateTime.UtcNow - field_477.field_181.Value < TimeSpan.FromSeconds(2L))
								{
									return (Between: false, Foreign: false);
								}
							}
							Vector3? localPlayerPosition = GetLocalPlayerPosition();
							bool num2 = localPlayerPosition.HasValue && field_477.field_182.HasValue && Vector3.DistanceSquared(localPlayerPosition.Value, field_477.field_182.Value) < 0.0001f;
							field_477.field_182 = localPlayerPosition;
							if (!num2 || Method_133() || Method_121() || Condition[ConditionFlag.MountOrOrnamentTransition] || DateTime.UtcNow < field_477.field_183 || field_477.field_177.Method_122() || !Method_120())
							{
								return (Between: false, Foreign: false);
							}
							field_477.field_184++;
							bool flag = field_477.field_177.Method_119(field_477.field_185);
							if (flag)
							{
								field_477.field_179++;
							}
							field_477.field_183 = DateTime.UtcNow.AddSeconds((!flag) ? 1 : 2);
							return (Between: false, Foreign: false);
						}).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (field_472 = 1);
							field_481 = awaiter2;
							field_473.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_01f3;
					}
					automaticHunting.PrintEcho($"传送失败: 60秒内未进入过图(id={field_477.field_185})");
					result = false;
					break;
					IL_01f3:
					result2 = awaiter2.GetResult();
					if (result2.Item1)
					{
						result = true;
						break;
					}
					if (result2.Item2)
					{
						awaiter = Task.Delay(100, field_477.field_178).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (field_472 = 2);
							field_480 = awaiter;
							field_473.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_0275;
					}
					if (DateTime.UtcNow >= field_479)
					{
						automaticHunting.PrintEcho($"等待传送开始(id={field_477.field_185}，已尝试{field_477.field_184}次)...");
						field_479 = DateTime.UtcNow.AddSeconds(10.0);
					}
					awaiter = Task.Delay(100, field_477.field_178).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (field_472 = 3);
						field_480 = awaiter;
						field_473.AwaitUnsafeOnCompleted(ref awaiter, ref this);
						return;
					}
					goto IL_036f;
					IL_036f:
					awaiter.GetResult();
					goto IL_0376;
				}
			}
			catch (Exception exception)
			{
				field_472 = -2;
				field_477 = null;
				field_473.SetException(exception);
				return;
			}
			field_472 = -2;
			field_477 = null;
			field_473.SetResult(result);
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_417
			this.Method_417();
		}

		[DebuggerHidden]
		private void Method_418(IAsyncStateMachine arg0)
		{
			field_473.SetStateMachine(arg0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine arg0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_418
			this.Method_418(arg0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	public struct RecoveredType_78 : IAsyncStateMachine
	{
		public int field_482;

		public AsyncTaskMethodBuilder<bool> field_483;

		public CancellationToken field_484;

		public AutomaticHunting field_485;

		private bool field_486;

		private TaskAwaiter<bool> field_487;

		private int field_488;

		private TaskAwaiter field_489;

		private DateTime field_490;

		private int field_491;

		private void Method_419()
		{
			int num = field_482;
			AutomaticHunting CS_0024_003C_003E8__locals7 = field_485;
			bool result;
			try
			{
				TaskAwaiter<bool> awaiter2;
				TaskAwaiter awaiter;
				switch (num)
				{
				default:
					awaiter2 = Framework.RunOnFrameworkThread(() => Condition[ConditionFlag.Mounted]).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						num = (field_482 = 0);
						field_487 = awaiter2;
						field_483.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
						return;
					}
					goto IL_00bf;
				case 0:
					awaiter2 = field_487;
					field_487 = default;
					num = (field_482 = -1);
					goto IL_00bf;
				case 1:
					awaiter2 = field_487;
					field_487 = default;
					num = (field_482 = -1);
					goto IL_0159;
				case 2:
					awaiter = field_489;
					field_489 = default;
					num = (field_482 = -1);
					goto IL_01cd;
				case 3:
					awaiter = field_489;
					field_489 = default;
					num = (field_482 = -1);
					goto IL_025c;
				case 4:
					awaiter2 = field_487;
					field_487 = default;
					num = (field_482 = -1);
					goto IL_02c7;
				case 5:
					awaiter = field_489;
					field_489 = default;
					num = (field_482 = -1);
					goto IL_034f;
				case 6:
					awaiter2 = field_487;
					field_487 = default;
					num = (field_482 = -1);
					goto IL_03ba;
				case 7:
					awaiter2 = field_487;
					field_487 = default;
					num = (field_482 = -1);
					goto IL_045f;
				case 8:
					awaiter = field_489;
					field_489 = default;
					num = (field_482 = -1);
					goto IL_04c9;
				case 9:
					awaiter2 = field_487;
					field_487 = default;
					num = (field_482 = -1);
					goto IL_0535;
				case 10:
					awaiter2 = field_487;
					field_487 = default;
					num = (field_482 = -1);
					goto IL_05c5;
				case 11:
					awaiter = field_489;
					field_489 = default;
					num = (field_482 = -1);
					goto IL_0634;
				case 12:
					{
						awaiter = field_489;
						field_489 = default;
						num = (field_482 = -1);
						goto IL_06c2;
					}
					IL_03c3:
					if (DateTime.UtcNow - field_490 < TimeSpan.FromSeconds(5L))
					{
						awaiter = Task.Delay(200, field_484).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (field_482 = 5);
							field_489 = awaiter;
							field_483.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_034f;
					}
					goto IL_03e4;
					IL_06c2:
					awaiter.GetResult();
					goto IL_06c9;
					IL_04c9:
					awaiter.GetResult();
					awaiter2 = Framework.RunOnFrameworkThread((Func<bool>)CS_0024_003C_003E8__locals7.Method_183).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						num = (field_482 = 9);
						field_487 = awaiter2;
						field_483.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
						return;
					}
					goto IL_0535;
					IL_00bf:
					if (awaiter2.GetResult())
					{
						result = true;
						break;
					}
					field_486 = false;
					field_488 = 0;
					goto IL_01e6;
					IL_0634:
					awaiter.GetResult();
					field_491++;
					goto IL_064d;
					IL_034f:
					awaiter.GetResult();
					awaiter2 = Framework.RunOnFrameworkThread((Func<bool>)CS_0024_003C_003E8__locals7.Method_184).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						num = (field_482 = 6);
						field_487 = awaiter2;
						field_483.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
						return;
					}
					goto IL_03ba;
					IL_01e6:
					if (field_488 < 30)
					{
						awaiter2 = Framework.RunOnFrameworkThread(() => Method_133()).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (field_482 = 1);
							field_487 = awaiter2;
							field_483.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_0159;
					}
					goto IL_01f3;
					IL_0535:
					if (awaiter2.GetResult())
					{
						field_491 = 0;
						goto IL_064d;
					}
					goto IL_065a;
					IL_03e4:
					CS_0024_003C_003E8__locals7.PrintEcho("召唤坐骑...");
					field_488 = 1;
					goto IL_06db;
					IL_03ba:
					if (!awaiter2.GetResult())
					{
						goto IL_03c3;
					}
					goto IL_03e4;
					IL_06db:
					if (field_488 <= 5)
					{
						awaiter2 = Framework.RunOnFrameworkThread(() => (CS_0024_003C_003E8__locals7.Config.SelectedMountId != 0) ? Method_198(CS_0024_003C_003E8__locals7.Config.SelectedMountId) : Method_199()).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (field_482 = 7);
							field_487 = awaiter2;
							field_483.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_045f;
					}
					result = false;
					break;
					IL_0159:
					if (awaiter2.GetResult())
					{
						field_486 = true;
						awaiter = Task.Delay(500, field_484).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (field_482 = 2);
							field_489 = awaiter;
							field_483.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_01cd;
					}
					goto IL_01f3;
					IL_05c5:
					if (awaiter2.GetResult())
					{
						result = true;
						break;
					}
					awaiter = Task.Delay(100, field_484).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (field_482 = 11);
						field_489 = awaiter;
						field_483.AwaitUnsafeOnCompleted(ref awaiter, ref this);
						return;
					}
					goto IL_0634;
					IL_064d:
					if (field_491 < 30)
					{
						awaiter2 = Framework.RunOnFrameworkThread(() => Condition[ConditionFlag.Mounted]).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (field_482 = 10);
							field_487 = awaiter2;
							field_483.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_05c5;
					}
					goto IL_065a;
					IL_01cd:
					awaiter.GetResult();
					field_488++;
					goto IL_01e6;
					IL_01f3:
					if (field_486)
					{
						awaiter = Task.Delay(1000, field_484).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (field_482 = 3);
							field_489 = awaiter;
							field_483.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_025c;
					}
					goto IL_0263;
					IL_065a:
					if (field_488 < 5)
					{
						awaiter = Task.Delay(500, field_484).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (field_482 = 12);
							field_489 = awaiter;
							field_483.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_06c2;
					}
					goto IL_06c9;
					IL_045f:
					if (awaiter2.GetResult())
					{
						awaiter = Task.Delay(100, field_484).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (field_482 = 8);
							field_489 = awaiter;
							field_483.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_04c9;
					}
					goto IL_065a;
					IL_025c:
					awaiter.GetResult();
					goto IL_0263;
					IL_0263:
					awaiter2 = Framework.RunOnFrameworkThread((Func<bool>)CS_0024_003C_003E8__locals7.Method_184).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						num = (field_482 = 4);
						field_487 = awaiter2;
						field_483.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
						return;
					}
					goto IL_02c7;
					IL_06c9:
					field_488++;
					goto IL_06db;
					IL_02c7:
					if (!awaiter2.GetResult())
					{
						CS_0024_003C_003E8__locals7.PrintEcho("等待区域加载/动作就绪...");
						field_490 = DateTime.UtcNow;
						goto IL_03c3;
					}
					goto IL_03e4;
				}
			}
			catch (Exception exception)
			{
				field_482 = -2;
				field_483.SetException(exception);
				return;
			}
			field_482 = -2;
			field_483.SetResult(result);
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_419
			this.Method_419();
		}

		[DebuggerHidden]
		private void Method_420(IAsyncStateMachine arg0)
		{
			field_483.SetStateMachine(arg0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine arg0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_420
			this.Method_420(arg0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	public struct RecoveredType_79 : IAsyncStateMachine
	{
		public int field_492;

		public AsyncTaskMethodBuilder field_493;

		public AutomaticHunting field_494;

		private TaskAwaiter<bool> field_495;

		private void Method_421()
		{
			int num = field_492;
			AutomaticHunting automaticHunting = field_494;
			try
			{
				try
				{
					TaskAwaiter<bool> awaiter;
					if (num != 0)
					{
						awaiter = automaticHunting.Method_182(CancellationToken.None).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (field_492 = 0);
							field_495 = awaiter;
							field_493.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = field_495;
						field_495 = default;
						num = (field_492 = -1);
					}
					awaiter.GetResult();
				}
				catch (Exception exception)
				{
					Log.Warning(exception, "AutomaticHunting: Manual mount summon failed");
				}
			}
			catch (Exception exception2)
			{
				field_492 = -2;
				field_493.SetException(exception2);
				return;
			}
			field_492 = -2;
			field_493.SetResult();
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_421
			this.Method_421();
		}

		[DebuggerHidden]
		private void Method_422(IAsyncStateMachine arg0)
		{
			field_493.SetStateMachine(arg0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine arg0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_422
			this.Method_422(arg0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	public struct RecoveredType_80 : IAsyncStateMachine
	{
		public int field_496;

		public AsyncTaskMethodBuilder field_497;

		public AutomaticHunting field_498;

		public int field_499;

		private DateTime field_500;

		private TaskAwaiter field_501;

		private TaskAwaiter<bool> field_502;

		private int field_503;

		private void Method_423()
		{
			int num = field_496;
			AutomaticHunting automaticHunting = field_498;
			try
			{
				_ = 4;
				try
				{
					TaskAwaiter<bool> awaiter2;
					TaskAwaiter awaiter;
					switch (num)
					{
					default:
						field_500 = DateTime.UtcNow;
						goto IL_013a;
					case 0:
						awaiter = field_501;
						field_501 = default;
						num = (field_496 = -1);
						goto IL_0095;
					case 1:
						awaiter2 = field_502;
						field_502 = default;
						num = (field_496 = -1);
						goto IL_0113;
					case 2:
						awaiter = field_501;
						field_501 = default;
						num = (field_496 = -1);
						goto IL_01c1;
					case 3:
						awaiter2 = field_502;
						field_502 = default;
						num = (field_496 = -1);
						goto IL_024b;
					case 4:
						{
							awaiter = field_501;
							field_501 = default;
							num = (field_496 = -1);
							goto IL_02ac;
						}
						IL_013a:
						if (DateTime.UtcNow - field_500 < TimeSpan.FromSeconds(45L))
						{
							awaiter = Task.Delay(500).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (field_496 = 0);
								field_501 = awaiter;
								field_497.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
							goto IL_0095;
						}
						goto IL_015c;
						IL_02ac:
						awaiter.GetResult();
						field_503++;
						goto IL_02c5;
						IL_015c:
						awaiter = Task.Delay((int)(automaticHunting.Config.WaitAfterTeleport * 1000f)).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (field_496 = 2);
							field_501 = awaiter;
							field_497.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_01c1;
						IL_0095:
						awaiter.GetResult();
						awaiter2 = Framework.RunOnFrameworkThread(() => Condition[ConditionFlag.BetweenAreas] || Condition[ConditionFlag.BetweenAreas51]).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (field_496 = 1);
							field_502 = awaiter2;
							field_497.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_0113;
						IL_01c1:
						awaiter.GetResult();
						field_503 = 0;
						goto IL_02c5;
						IL_024b:
						if (!awaiter2.GetResult())
						{
							break;
						}
						awaiter = Task.Delay(500).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (field_496 = 4);
							field_501 = awaiter;
							field_497.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_02ac;
						IL_02c5:
						if (field_503 >= 30)
						{
							break;
						}
						awaiter2 = Framework.RunOnFrameworkThread(() => Method_133()).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (field_496 = 3);
							field_502 = awaiter2;
							field_497.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_024b;
						IL_0113:
						if (awaiter2.GetResult() || !(DateTime.UtcNow - field_500 > TimeSpan.FromSeconds(2L)))
						{
							goto IL_013a;
						}
						goto IL_015c;
					}
					automaticHunting.PrintEcho("进入多线地图: 区域已稳定，执行切线");
					automaticHunting.SwitchToInstance(field_499);
				}
				catch (Exception exception)
				{
					Log.Warning(exception, "AutomaticHunting: 等待稳定后切线失败");
				}
			}
			catch (Exception exception2)
			{
				field_496 = -2;
				field_497.SetException(exception2);
				return;
			}
			field_496 = -2;
			field_497.SetResult();
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_423
			this.Method_423();
		}

		[DebuggerHidden]
		private void Method_424(IAsyncStateMachine arg0)
		{
			field_497.SetStateMachine(arg0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine arg0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_424
			this.Method_424(arg0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	public struct RecoveredType_81 : IAsyncStateMachine
	{
		public int field_504;

		public AsyncTaskMethodBuilder field_505;

		private RecoveredType_18 field_506;

		public AutomaticHunting field_507;

		private List<(uint Id, Vector3 Position, string Name)> field_508;

		private TaskAwaiter<List<(uint Id, Vector3 Position, string Name)>> field_509;

		private TaskAwaiter<Vector3?> field_510;

		private TaskAwaiter<bool> field_511;

		private void Method_425()
		{
			int num = field_504;
			AutomaticHunting automaticHunting = field_507;
			try
			{
				_ = 3;
				try
				{
					TaskAwaiter<List<(uint, Vector3, string)>> awaiter3;
					TaskAwaiter<Vector3?> awaiter2;
					TaskAwaiter<bool> awaiter;
					Vector3? result;
					List<(uint, Vector3, string)> result2;
					switch (num)
					{
					default:
						field_506 = new RecoveredType_18();
						field_506.field_161 = Method_116();
						awaiter3 = Framework.RunOnFrameworkThread(() => Method_127(field_506.field_161)).GetAwaiter();
						if (!awaiter3.IsCompleted)
						{
							num = (field_504 = 0);
							field_509 = awaiter3;
							field_505.AwaitUnsafeOnCompleted(ref awaiter3, ref this);
							return;
						}
						goto IL_00b0;
					case 0:
						awaiter3 = field_509;
						field_509 = default;
						num = (field_504 = -1);
						goto IL_00b0;
					case 1:
						awaiter2 = field_510;
						field_510 = default;
						num = (field_504 = -1);
						goto IL_0147;
					case 2:
						awaiter = field_511;
						field_511 = default;
						num = (field_504 = -1);
						goto IL_0235;
					case 3:
						{
							awaiter = field_511;
							field_511 = default;
							num = (field_504 = -1);
							break;
						}
						IL_0147:
						result = awaiter2.GetResult();
						field_506.field_162 = result;
						if (field_506.field_162.HasValue)
						{
							(uint, Vector3, string) tuple = field_508.MinBy(((uint Id, Vector3 Position, string Name) arg0) => Vector3.DistanceSquared(arg0.Position, field_506.field_162.Value));
							if (!(Vector3.Distance(tuple.Item2, field_506.field_162.Value) <= 60f))
							{
								automaticHunting.PrintEcho("切线未生效，传送到最近以太之光(" + tuple.Item3 + ")后重试");
								awaiter = automaticHunting.Method_123(tuple.Item1, CancellationToken.None).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									num = (field_504 = 2);
									field_511 = awaiter;
									field_505.AwaitUnsafeOnCompleted(ref awaiter, ref this);
									return;
								}
								goto IL_0235;
							}
						}
						goto end_IL_0013;
						IL_00b0:
						result2 = awaiter3.GetResult();
						field_508 = result2;
						if (field_508.Count != 0)
						{
							awaiter2 = Framework.RunOnFrameworkThread((Func<Vector3?>)GetLocalPlayerPosition).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								num = (field_504 = 1);
								field_510 = awaiter2;
								field_505.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
								return;
							}
							goto IL_0147;
						}
						goto end_IL_0013;
						IL_0235:
						if (awaiter.GetResult())
						{
							awaiter = automaticHunting.Method_163(field_506.field_161, CancellationToken.None).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (field_504 = 3);
								field_511 = awaiter;
								field_505.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
							break;
						}
						goto end_IL_0013;
					}
					awaiter.GetResult();
					field_506 = null;
					field_508 = null;
					end_IL_0013:;
				}
				catch (Exception exception)
				{
					Log.Warning(exception, "AutomaticHunting: 切线重试传送到以太之光失败");
				}
			}
			catch (Exception exception2)
			{
				field_504 = -2;
				field_505.SetException(exception2);
				return;
			}
			field_504 = -2;
			field_505.SetResult();
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_425
			this.Method_425();
		}

		[DebuggerHidden]
		private void Method_426(IAsyncStateMachine arg0)
		{
			field_505.SetStateMachine(arg0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine arg0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_426
			this.Method_426(arg0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	public struct RecoveredType_82 : IAsyncStateMachine
	{
		public int field_512;

		public AsyncTaskMethodBuilder<bool> field_513;

		public AutomaticHunting field_514;

		public CancellationToken field_515;

		public uint field_516;

		private TaskAwaiter field_517;

		private TaskAwaiter<bool> field_518;

		private void Method_427()
		{
			int num = field_512;
			AutomaticHunting automaticHunting = field_514;
			bool result;
			try
			{
				TaskAwaiter awaiter;
				if (num != 0)
				{
					if (num == 1)
					{
						goto IL_007c;
					}
					awaiter = automaticHunting.field_77.WaitAsync(field_515).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (field_512 = 0);
						field_517 = awaiter;
						field_513.AwaitUnsafeOnCompleted(ref awaiter, ref this);
						return;
					}
				}
				else
				{
					awaiter = field_517;
					field_517 = default;
					num = (field_512 = -1);
				}
				awaiter.GetResult();
				goto IL_007c;
				IL_007c:
				try
				{
					TaskAwaiter<bool> awaiter2;
					if (num != 1)
					{
						awaiter2 = automaticHunting.Method_124(field_516, field_515).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (field_512 = 1);
							field_518 = awaiter2;
							field_513.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
					}
					else
					{
						awaiter2 = field_518;
						field_518 = default;
						num = (field_512 = -1);
					}
					result = awaiter2.GetResult();
				}
				finally
				{
					if (num < 0)
					{
						automaticHunting.field_77.Release();
					}
				}
			}
			catch (Exception exception)
			{
				field_512 = -2;
				field_513.SetException(exception);
				return;
			}
			field_512 = -2;
			field_513.SetResult(result);
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_427
			this.Method_427();
		}

		[DebuggerHidden]
		private void Method_428(IAsyncStateMachine arg0)
		{
			field_513.SetStateMachine(arg0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine arg0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_428
			this.Method_428(arg0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	public struct RecoveredType_83 : IAsyncStateMachine
	{
		public int field_519;

		public AsyncTaskMethodBuilder field_520;

		public TimeSpan field_521;

		private DateTime field_522;

		private TaskAwaiter<bool> field_523;

		private TaskAwaiter field_524;

		private void Method_429()
		{
			int num = field_519;
			try
			{
				TaskAwaiter awaiter;
				if (num != 0)
				{
					if (num != 1)
					{
						field_522 = DateTime.UtcNow;
						goto IL_0102;
					}
					awaiter = field_524;
					field_524 = default;
					num = (field_519 = -1);
					goto IL_00fb;
				}
				TaskAwaiter<bool> awaiter2 = field_523;
				field_523 = default;
				num = (field_519 = -1);
				goto IL_0098;
				IL_0102:
				if (DateTime.UtcNow - field_522 < field_521)
				{
					awaiter2 = Framework.RunOnFrameworkThread(() => Method_133()).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						num = (field_519 = 0);
						field_523 = awaiter2;
						field_520.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
						return;
					}
					goto IL_0098;
				}
				goto end_IL_0007;
				IL_00fb:
				awaiter.GetResult();
				goto IL_0102;
				IL_0098:
				if (awaiter2.GetResult())
				{
					awaiter = Task.Delay(500).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (field_519 = 1);
						field_524 = awaiter;
						field_520.AwaitUnsafeOnCompleted(ref awaiter, ref this);
						return;
					}
					goto IL_00fb;
				}
				end_IL_0007:;
			}
			catch (Exception exception)
			{
				field_519 = -2;
				field_520.SetException(exception);
				return;
			}
			field_519 = -2;
			field_520.SetResult();
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_429
			this.Method_429();
		}

		[DebuggerHidden]
		private void Method_430(IAsyncStateMachine arg0)
		{
			field_520.SetStateMachine(arg0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine arg0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_430
			this.Method_430(arg0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	public struct RecoveredType_84 : IAsyncStateMachine
	{
		public int field_525;

		public AsyncTaskMethodBuilder<bool> field_526;

		public uint field_527;

		public AutomaticHunting field_528;

		public CancellationToken field_529;

		private RecoveredType_37 field_530;

		private DateTime field_531;

		private TaskAwaiter field_532;

		private TaskAwaiter<bool> field_533;

		private int field_534;

		private void Method_431()
		{
			int num = field_525;
			AutomaticHunting automaticHunting = field_528;
			bool result;
			try
			{
				TaskAwaiter<bool> awaiter2;
				TaskAwaiter awaiter;
				bool flag;
				switch (num)
				{
				default:
					field_530 = new RecoveredType_37();
					field_530.field_205 = field_527;
					automaticHunting.PrintEcho("等待传送完成...");
					field_531 = DateTime.UtcNow;
					goto IL_03ed;
				case 0:
					awaiter = field_532;
					field_532 = default;
					num = (field_525 = -1);
					goto IL_00c1;
				case 1:
					awaiter2 = field_533;
					field_533 = default;
					num = (field_525 = -1);
					goto IL_0142;
				case 2:
					awaiter2 = field_533;
					field_533 = default;
					num = (field_525 = -1);
					goto IL_01e0;
				case 3:
					awaiter = field_532;
					field_532 = default;
					num = (field_525 = -1);
					goto IL_02a1;
				case 4:
					awaiter2 = field_533;
					field_533 = default;
					num = (field_525 = -1);
					goto IL_033f;
				case 5:
					{
						awaiter = field_532;
						field_532 = default;
						num = (field_525 = -1);
						goto IL_03b8;
					}
					IL_03ed:
					if (DateTime.UtcNow - field_531 < TimeSpan.FromSeconds(45L))
					{
						awaiter = Task.Delay(500, field_529).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (field_525 = 0);
							field_532 = awaiter;
							field_526.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_00c1;
					}
					automaticHunting.PrintEcho("等待传送超时(45s)，导航已取消");
					result = false;
					break;
					IL_033f:
					if (awaiter2.GetResult())
					{
						automaticHunting.PrintEcho("传送完成");
						result = true;
						break;
					}
					awaiter = Task.Delay(500, field_529).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (field_525 = 5);
						field_532 = awaiter;
						field_526.AwaitUnsafeOnCompleted(ref awaiter, ref this);
						return;
					}
					goto IL_03b8;
					IL_02a1:
					awaiter.GetResult();
					field_534 = 0;
					goto IL_03d1;
					IL_03b8:
					awaiter.GetResult();
					field_534++;
					goto IL_03d1;
					IL_00c1:
					awaiter.GetResult();
					awaiter2 = Framework.RunOnFrameworkThread(() => Condition[ConditionFlag.BetweenAreas] || Condition[ConditionFlag.BetweenAreas51]).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						num = (field_525 = 1);
						field_533 = awaiter2;
						field_526.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
						return;
					}
					goto IL_0142;
					IL_01e0:
					flag = awaiter2.GetResult();
					goto IL_01e9;
					IL_03d1:
					if (field_534 < 30)
					{
						awaiter2 = Framework.RunOnFrameworkThread(() => !Method_133() && !Condition[ConditionFlag.Casting] && Method_116() == field_530.field_205 && GetLocalPlayerPosition().HasValue).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (field_525 = 4);
							field_533 = awaiter2;
							field_526.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_033f;
					}
					automaticHunting.PrintEcho("传送后状态未稳定，导航已取消");
					result = false;
					break;
					IL_01e9:
					if (flag)
					{
						automaticHunting.PrintEcho($"传送读条结束，等待{automaticHunting.Config.WaitAfterTeleport:0}秒状态稳定...");
						awaiter = Task.Delay((int)(automaticHunting.Config.WaitAfterTeleport * 1000f), field_529).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (field_525 = 3);
							field_532 = awaiter;
							field_526.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_02a1;
					}
					goto IL_03ed;
					IL_0142:
					flag = !awaiter2.GetResult();
					if (flag)
					{
						awaiter2 = Framework.RunOnFrameworkThread(() => Method_116() == field_530.field_205 && GetLocalPlayerPosition().HasValue).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (field_525 = 2);
							field_533 = awaiter2;
							field_526.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_01e0;
					}
					goto IL_01e9;
				}
			}
			catch (Exception exception)
			{
				field_525 = -2;
				field_530 = null;
				field_526.SetException(exception);
				return;
			}
			field_525 = -2;
			field_530 = null;
			field_526.SetResult(result);
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_431
			this.Method_431();
		}

		[DebuggerHidden]
		private void Method_432(IAsyncStateMachine arg0)
		{
			field_526.SetStateMachine(arg0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine arg0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_432
			this.Method_432(arg0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	public struct RecoveredType_85 : IAsyncStateMachine
	{
		public int field_535;

		public AsyncTaskMethodBuilder<bool> field_536;

		public AutomaticHunting field_537;

		public CancellationToken field_538;

		private int field_539;

		private TaskAwaiter<bool> field_540;

		private TaskAwaiter field_541;

		private void Method_433()
		{
			int num = field_535;
			AutomaticHunting CS_0024_003C_003E8__locals1 = field_537;
			bool result;
			try
			{
				TaskAwaiter awaiter;
				if (num != 0)
				{
					if (num != 1)
					{
						field_539 = 0;
						goto IL_010f;
					}
					awaiter = field_541;
					field_541 = default;
					num = (field_535 = -1);
					goto IL_00f6;
				}
				TaskAwaiter<bool> awaiter2 = field_540;
				field_540 = default;
				num = (field_535 = -1);
				goto IL_0088;
				IL_010f:
				if (field_539 < 60)
				{
					awaiter2 = Framework.RunOnFrameworkThread(() => CS_0024_003C_003E8__locals1.field_24?.InvokeFunc() ?? false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						num = (field_535 = 0);
						field_540 = awaiter2;
						field_536.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
						return;
					}
					goto IL_0088;
				}
				result = false;
				goto end_IL_000e;
				IL_00f6:
				awaiter.GetResult();
				field_539++;
				goto IL_010f;
				IL_0088:
				if (!awaiter2.GetResult())
				{
					awaiter = Task.Delay(500, field_538).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (field_535 = 1);
						field_541 = awaiter;
						field_536.AwaitUnsafeOnCompleted(ref awaiter, ref this);
						return;
					}
					goto IL_00f6;
				}
				result = true;
				end_IL_000e:;
			}
			catch (Exception exception)
			{
				field_535 = -2;
				field_536.SetException(exception);
				return;
			}
			field_535 = -2;
			field_536.SetResult(result);
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_433
			this.Method_433();
		}

		[DebuggerHidden]
		private void Method_434(IAsyncStateMachine arg0)
		{
			field_536.SetStateMachine(arg0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine arg0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in Method_434
			this.Method_434(arg0);
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
				if (value.Any((string arg0) => field_169.Contains(arg0)))
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
			if ((flag && value != null) ? value.Any((string arg0) => field_247.Contains(arg0)) : (item is IBattleChara))
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
			Task.Run([AsyncStateMachine(typeof(RecoveredType_12.RecoveredType_13))] () =>
			{
				RecoveredType_12.RecoveredType_13 stateMachine = default;
				stateMachine.field_154 = AsyncTaskMethodBuilder.Create();
				stateMachine.field_153 = -1;
				stateMachine.field_154.Start(ref stateMachine);
				return stateMachine.field_154.Task;
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
				CommandManager.ProcessCommand("/bmrai on");
				PrintEcho("BMR AI 已开启");
			}
			else if (!flag && field_35)
			{
				CommandManager.ProcessCommand("/bmrai off");
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

	[AsyncStateMachine(typeof(RecoveredType_80))]
	private Task Method_61(int arg0)
	{
		RecoveredType_80 stateMachine = default;
		stateMachine.field_497 = AsyncTaskMethodBuilder.Create();
		stateMachine.field_498 = this;
		stateMachine.field_499 = arg0;
		stateMachine.field_496 = -1;
		stateMachine.field_497.Start(ref stateMachine);
		return stateMachine.field_497.Task;
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
		int num = Math.Clamp(arg0, 1, 10);
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
		string field_158 = $"/pdr insc {num}";
		Framework.RunOnFrameworkThread(() =>
		{
			try
			{
				CommandManager.ProcessCommand(field_158);
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

	[AsyncStateMachine(typeof(RecoveredType_57))]
	private Task Method_65(int arg0)
	{
		RecoveredType_57 stateMachine = default;
		stateMachine.field_255 = AsyncTaskMethodBuilder.Create();
		stateMachine.field_257 = this;
		stateMachine.field_256 = arg0;
		stateMachine.field_254 = -1;
		stateMachine.field_255.Start(ref stateMachine);
		return stateMachine.field_255.Task;
	}

	[AsyncStateMachine(typeof(RecoveredType_70))]
	private Task<bool> Method_66(int arg0, TimeSpan arg1)
	{
		RecoveredType_70 stateMachine = default;
		stateMachine.field_378 = AsyncTaskMethodBuilder<bool>.Create();
		stateMachine.field_379 = this;
		stateMachine.field_380 = arg0;
		stateMachine.field_381 = arg1;
		stateMachine.field_377 = -1;
		stateMachine.field_378.Start(ref stateMachine);
		return stateMachine.field_378.Task;
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

	[AsyncStateMachine(typeof(RecoveredType_81))]
	private Task Method_69()
	{
		RecoveredType_81 stateMachine = default;
		stateMachine.field_505 = AsyncTaskMethodBuilder.Create();
		stateMachine.field_507 = this;
		stateMachine.field_504 = -1;
		stateMachine.field_505.Start(ref stateMachine);
		return stateMachine.field_505.Task;
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
		RecoveredType_19 CS_0024_003C_003E8__locals7 = new RecoveredType_19();
		CS_0024_003C_003E8__locals7.field_163 = this;
		CS_0024_003C_003E8__locals7.field_164 = SMonitor;
		if (CS_0024_003C_003E8__locals7.field_164 != null && !CS_0024_003C_003E8__locals7.field_164.Finished)
		{
			CS_0024_003C_003E8__locals7.field_164.Finished = true;
			CS_0024_003C_003E8__locals7.field_164.Phase = arg0;
			Task.Run([AsyncStateMachine(typeof(RecoveredType_19.RecoveredType_20))] () =>
			{
				RecoveredType_19.RecoveredType_20 stateMachine = default;
				stateMachine.field_166 = AsyncTaskMethodBuilder.Create();
				stateMachine.field_167 = CS_0024_003C_003E8__locals7;
				stateMachine.field_165 = -1;
				stateMachine.field_166.Start(ref stateMachine);
				return stateMachine.field_166.Task;
			});
		}
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
		string text = Method_82(arg0);
		if (string.IsNullOrWhiteSpace(text) || !text.Contains("当前所在副本区为"))
		{
			return;
		}
		string text2 = Method_88(text);
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

	[AsyncStateMachine(typeof(RecoveredType_73))]
	private Task Method_103(string arg0, uint arg1, string arg2, CancellationToken arg3)
	{
		RecoveredType_73 stateMachine = default;
		stateMachine.field_406 = AsyncTaskMethodBuilder.Create();
		stateMachine.field_408 = this;
		stateMachine.field_409 = arg0;
		stateMachine.field_407 = arg1;
		stateMachine.field_411 = arg2;
		stateMachine.field_410 = arg3;
		stateMachine.field_405 = -1;
		stateMachine.field_406.Start(ref stateMachine);
		return stateMachine.field_406.Task;
	}

	[AsyncStateMachine(typeof(RecoveredType_60))]
	private Task Method_104(CancellationToken arg0)
	{
		RecoveredType_60 stateMachine = default;
		stateMachine.field_277 = AsyncTaskMethodBuilder.Create();
		stateMachine.field_278 = this;
		stateMachine.field_279 = arg0;
		stateMachine.field_276 = -1;
		stateMachine.field_277.Start(ref stateMachine);
		return stateMachine.field_277.Task;
	}

	[AsyncStateMachine(typeof(RecoveredType_64))]
	private Task Method_105(uint arg0, uint arg1, Vector3 arg2, CancellationToken arg3)
	{
		RecoveredType_64 stateMachine = default;
		stateMachine.field_298 = AsyncTaskMethodBuilder.Create();
		stateMachine.field_301 = this;
		stateMachine.field_299 = arg0;
		stateMachine.field_302 = arg1;
		stateMachine.field_304 = arg2;
		stateMachine.field_300 = arg3;
		stateMachine.field_297 = -1;
		stateMachine.field_298.Start(ref stateMachine);
		return stateMachine.field_298.Task;
	}

	[AsyncStateMachine(typeof(RecoveredType_71))]
	private Task Method_106(uint arg0, Vector3 arg1, string? arg2)
	{
		RecoveredType_71 stateMachine = default;
		stateMachine.field_386 = AsyncTaskMethodBuilder.Create();
		stateMachine.field_389 = this;
		stateMachine.field_387 = arg0;
		stateMachine.field_388 = arg1;
		stateMachine.field_390 = arg2;
		stateMachine.field_385 = -1;
		stateMachine.field_386.Start(ref stateMachine);
		return stateMachine.field_386.Task;
	}

	[AsyncStateMachine(typeof(RecoveredType_83))]
	private Task Method_107(TimeSpan arg0)
	{
		RecoveredType_83 stateMachine = default;
		stateMachine.field_520 = AsyncTaskMethodBuilder.Create();
		stateMachine.field_521 = arg0;
		stateMachine.field_519 = -1;
		stateMachine.field_520.Start(ref stateMachine);
		return stateMachine.field_520.Task;
	}

	private Task Method_108(string arg0)
	{
		return Framework.RunOnFrameworkThread(() =>
		{
			try
			{
				CommandManager.ProcessCommand(arg0);
			}
			catch (Exception exception)
			{
				Log.Warning(exception, "AutomaticHunting: 执行跨服命令失败 " + arg0);
			}
		});
	}

	[AsyncStateMachine(typeof(RecoveredType_62))]
	private Task<uint> Method_109()
	{
		RecoveredType_62 stateMachine = default;
		stateMachine.field_289 = AsyncTaskMethodBuilder<uint>.Create();
		stateMachine.field_288 = -1;
		stateMachine.field_289.Start(ref stateMachine);
		return stateMachine.field_289.Task;
	}

	[AsyncStateMachine(typeof(RecoveredType_61))]
	private Task<string> Method_110()
	{
		RecoveredType_61 stateMachine = default;
		stateMachine.field_286 = AsyncTaskMethodBuilder<string>.Create();
		stateMachine.field_285 = -1;
		stateMachine.field_286.Start(ref stateMachine);
		return stateMachine.field_286.Task;
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

	[AsyncStateMachine(typeof(RecoveredType_82))]
	private Task<bool> Method_123(uint arg0, CancellationToken arg1)
	{
		RecoveredType_82 stateMachine = default;
		stateMachine.field_513 = AsyncTaskMethodBuilder<bool>.Create();
		stateMachine.field_514 = this;
		stateMachine.field_516 = arg0;
		stateMachine.field_515 = arg1;
		stateMachine.field_512 = -1;
		stateMachine.field_513.Start(ref stateMachine);
		return stateMachine.field_513.Task;
	}

	[AsyncStateMachine(typeof(RecoveredType_77))]
	private Task<bool> Method_124(uint arg0, CancellationToken arg1)
	{
		RecoveredType_77 stateMachine = default;
		stateMachine.field_473 = AsyncTaskMethodBuilder<bool>.Create();
		stateMachine.field_474 = this;
		stateMachine.field_476 = arg0;
		stateMachine.field_475 = arg1;
		stateMachine.field_472 = -1;
		stateMachine.field_473.Start(ref stateMachine);
		return stateMachine.field_473.Task;
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
		RecoveredType_32 CS_0024_003C_003E8__locals6 = new RecoveredType_32();
		CS_0024_003C_003E8__locals6.field_193 = this;
		CS_0024_003C_003E8__locals6.field_194 = arg0;
		CS_0024_003C_003E8__locals6.field_195 = new CancellationTokenSource();
		lock (field_53)
		{
			CancellationTokenSource? cancellationTokenSource = field_54;
			CS_0024_003C_003E8__locals6.field_192 = field_55;
			field_54 = CS_0024_003C_003E8__locals6.field_195;
			field_56 = RecoveredType_8.field_116;
			cancellationTokenSource?.Cancel();
			field_55 = Task.Run([AsyncStateMachine(typeof(RecoveredType_32.RecoveredType_33))] () =>
			{
				RecoveredType_32.RecoveredType_33 stateMachine = default;
				stateMachine.field_197 = AsyncTaskMethodBuilder.Create();
				stateMachine.field_198 = CS_0024_003C_003E8__locals6;
				stateMachine.field_196 = -1;
				stateMachine.field_197.Start(ref stateMachine);
				return stateMachine.field_197.Task;
			});
		}
	}

	[AsyncStateMachine(typeof(RecoveredType_75))]
	private Task Method_158(RecoveredType_3 arg0, CancellationTokenSource arg1)
	{
		RecoveredType_75 stateMachine = default;
		stateMachine.field_438 = AsyncTaskMethodBuilder.Create();
		stateMachine.field_440 = this;
		stateMachine.field_441 = arg0;
		stateMachine.field_439 = arg1;
		stateMachine.field_437 = -1;
		stateMachine.field_438.Start(ref stateMachine);
		return stateMachine.field_438.Task;
	}

	[AsyncStateMachine(typeof(RecoveredType_72))]
	private Task<RecoveredType_10> Method_159(RecoveredType_3 arg0, CancellationToken arg1)
	{
		RecoveredType_72 stateMachine = default;
		stateMachine.field_398 = AsyncTaskMethodBuilder<RecoveredType_10>.Create();
		stateMachine.field_399 = this;
		stateMachine.field_400 = arg0;
		stateMachine.field_401 = arg1;
		stateMachine.field_397 = -1;
		stateMachine.field_398.Start(ref stateMachine);
		return stateMachine.field_398.Task;
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

	[AsyncStateMachine(typeof(RecoveredType_84))]
	private Task<bool> Method_163(uint arg0, CancellationToken arg1)
	{
		RecoveredType_84 stateMachine = default;
		stateMachine.field_526 = AsyncTaskMethodBuilder<bool>.Create();
		stateMachine.field_528 = this;
		stateMachine.field_527 = arg0;
		stateMachine.field_529 = arg1;
		stateMachine.field_525 = -1;
		stateMachine.field_526.Start(ref stateMachine);
		return stateMachine.field_526.Task;
	}

	[AsyncStateMachine(typeof(RecoveredType_68))]
	private Task Method_164(RecoveredType_3 arg0, CancellationToken arg1, CancellationTokenSource arg2)
	{
		RecoveredType_68 stateMachine = default;
		stateMachine.field_341 = AsyncTaskMethodBuilder.Create();
		stateMachine.field_342 = this;
		stateMachine.field_343 = arg0;
		stateMachine.field_346 = arg1;
		stateMachine.field_344 = arg2;
		stateMachine.field_340 = -1;
		stateMachine.field_341.Start(ref stateMachine);
		return stateMachine.field_341.Task;
	}

	[AsyncStateMachine(typeof(RecoveredType_76))]
	private Task Method_165(RecoveredType_5 arg0, CancellationTokenSource arg1, CancellationToken arg2)
	{
		RecoveredType_76 stateMachine = default;
		stateMachine.field_447 = AsyncTaskMethodBuilder.Create();
		stateMachine.field_448 = this;
		stateMachine.field_450 = arg0;
		stateMachine.field_449 = arg1;
		stateMachine.field_452 = arg2;
		stateMachine.field_446 = -1;
		stateMachine.field_447.Start(ref stateMachine);
		return stateMachine.field_447.Task;
	}

	[AsyncStateMachine(typeof(RecoveredType_59))]
	private Task Method_166(RecoveredType_5 arg0, float arg1, CancellationToken arg2)
	{
		RecoveredType_59 stateMachine = default;
		stateMachine.field_269 = AsyncTaskMethodBuilder.Create();
		stateMachine.field_270 = this;
		stateMachine.field_272 = arg0;
		stateMachine.field_271 = arg1;
		stateMachine.field_273 = arg2;
		stateMachine.field_268 = -1;
		stateMachine.field_269.Start(ref stateMachine);
		return stateMachine.field_269.Task;
	}

	[AsyncStateMachine(typeof(RecoveredType_58))]
	private Task<bool> Method_167(CancellationToken arg0)
	{
		RecoveredType_58 stateMachine = default;
		stateMachine.field_263 = AsyncTaskMethodBuilder<bool>.Create();
		stateMachine.field_264 = arg0;
		stateMachine.field_262 = -1;
		stateMachine.field_263.Start(ref stateMachine);
		return stateMachine.field_263.Task;
	}

	[AsyncStateMachine(typeof(RecoveredType_56))]
	private Task Method_168(CancellationToken arg0)
	{
		RecoveredType_56 stateMachine = default;
		stateMachine.field_249 = AsyncTaskMethodBuilder.Create();
		stateMachine.field_250 = this;
		stateMachine.field_251 = arg0;
		stateMachine.field_248 = -1;
		stateMachine.field_249.Start(ref stateMachine);
		return stateMachine.field_249.Task;
	}

	[AsyncStateMachine(typeof(RecoveredType_69))]
	private Task<bool> Method_169(RecoveredType_5 arg0, CancellationToken arg1)
	{
		RecoveredType_69 stateMachine = default;
		stateMachine.field_362 = AsyncTaskMethodBuilder<bool>.Create();
		stateMachine.field_363 = this;
		stateMachine.field_364 = arg0;
		stateMachine.field_366 = arg1;
		stateMachine.field_361 = -1;
		stateMachine.field_362.Start(ref stateMachine);
		return stateMachine.field_362.Task;
	}

	[AsyncStateMachine(typeof(RecoveredType_63))]
	private Task<Vector3?> Method_170(Vector3 arg0, bool arg1)
	{
		RecoveredType_63 stateMachine = default;
		stateMachine.field_292 = AsyncTaskMethodBuilder<Vector3?>.Create();
		stateMachine.field_293 = this;
		stateMachine.field_294 = arg0;
		stateMachine.field_295 = arg1;
		stateMachine.field_291 = -1;
		stateMachine.field_292.Start(ref stateMachine);
		return stateMachine.field_292.Task;
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
		string[] array = new string[4] { "/pdr load LimitlessTargetDashAction", "/pdr load AutoDismount", "/pdr load FastWorldTravel", "/pdr load InstantDismount" };
		foreach (string text in array)
		{
			try
			{
				CommandManager.ProcessCommand(text);
				PrintEcho("已执行: " + text);
			}
			catch (Exception exception)
			{
				Log.Warning(exception, "AutomaticHunting: 执行DR前置命令失败 " + text);
				PrintEcho("执行失败: " + text);
			}
		}
	}

	public void LoadFastInstanceSwitch()
	{
		try
		{
			CommandManager.ProcessCommand("/pdr load FastInstanceZoneChange");
			PrintEcho("已执行: /pdr load FastInstanceZoneChange");
		}
		catch (Exception exception)
		{
			Log.Warning(exception, "AutomaticHunting: 执行DR快捷副本区切换失败 /pdr load FastInstanceZoneChange");
			PrintEcho("执行失败: /pdr load FastInstanceZoneChange");
		}
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
			if (instanceCount > 0 && instanceCount <= 10)
			{
				num2 = multiLineMapConfig.InstanceCount;
				goto IL_0043;
			}
		}
		num2 = 10;
		goto IL_0043;
		IL_0043:
		int num3 = num2;
		if (num > num3)
		{
			num = 1;
		}
		Config.CurrentInstanceNumber = num;
		Method_176();
		CommandManager.ProcessCommand($"/pdr insc {num}");
		PrintEcho($"正在切换到 {num} 线 (/pdr insc {num})");
		Method_65(num);
	}

	private void Method_176()
	{
		PluginInterface.SavePluginConfig(Config);
	}

	[AsyncStateMachine(typeof(RecoveredType_66))]
	private Task Method_177()
	{
		RecoveredType_66 stateMachine = default;
		stateMachine.field_321 = AsyncTaskMethodBuilder.Create();
		stateMachine.field_322 = this;
		stateMachine.field_320 = -1;
		stateMachine.field_321.Start(ref stateMachine);
		return stateMachine.field_321.Task;
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
					CommandManager.ProcessCommand(content);
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
				CommandManager.ProcessCommand(arg0);
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
					Lumina.Excel.Sheets.Action action = excelSheet.FirstOrDefault((Lumina.Excel.Sheets.Action action2) => action2.Name.ExtractText() == arg0);
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

	[AsyncStateMachine(typeof(RecoveredType_85))]
	private Task<bool> Method_181(CancellationToken arg0)
	{
		RecoveredType_85 stateMachine = default;
		stateMachine.field_536 = AsyncTaskMethodBuilder<bool>.Create();
		stateMachine.field_537 = this;
		stateMachine.field_538 = arg0;
		stateMachine.field_535 = -1;
		stateMachine.field_536.Start(ref stateMachine);
		return stateMachine.field_536.Task;
	}

	[AsyncStateMachine(typeof(RecoveredType_78))]
	private Task<bool> Method_182(CancellationToken arg0)
	{
		RecoveredType_78 stateMachine = default;
		stateMachine.field_483 = AsyncTaskMethodBuilder<bool>.Create();
		stateMachine.field_485 = this;
		stateMachine.field_484 = arg0;
		stateMachine.field_482 = -1;
		stateMachine.field_483.Start(ref stateMachine);
		return stateMachine.field_483.Task;
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

	[AsyncStateMachine(typeof(RecoveredType_79))]
	private Task Method_186()
	{
		RecoveredType_79 stateMachine = default;
		stateMachine.field_493 = AsyncTaskMethodBuilder.Create();
		stateMachine.field_494 = this;
		stateMachine.field_492 = -1;
		stateMachine.field_493.Start(ref stateMachine);
		return stateMachine.field_493.Task;
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
		RecoveredType_48 CS_0024_003C_003E8__locals10 = new RecoveredType_48();
		CS_0024_003C_003E8__locals10.field_232 = this;
		CS_0024_003C_003E8__locals10.field_233 = arg0;
		CS_0024_003C_003E8__locals10.field_234 = arg1;
		LastFlagPosition = CS_0024_003C_003E8__locals10.field_234;
		field_28 = CS_0024_003C_003E8__locals10.field_233;
		field_31 = Method_127(CS_0024_003C_003E8__locals10.field_233);
		CS_0024_003C_003E8__locals10.field_235 = new CancellationTokenSource();
		lock (field_53)
		{
			CancellationTokenSource? cancellationTokenSource = field_54;
			CS_0024_003C_003E8__locals10.field_231 = field_55;
			field_54 = CS_0024_003C_003E8__locals10.field_235;
			field_56 = RecoveredType_8.field_116;
			cancellationTokenSource?.Cancel();
			field_55 = Task.Run([AsyncStateMachine(typeof(RecoveredType_48.RecoveredType_49))] () =>
			{
				RecoveredType_48.RecoveredType_49 stateMachine = default;
				stateMachine.field_237 = AsyncTaskMethodBuilder.Create();
				stateMachine.field_238 = CS_0024_003C_003E8__locals10;
				stateMachine.field_236 = -1;
				stateMachine.field_237.Start(ref stateMachine);
				return stateMachine.field_237.Task;
			});
		}
	}

	[AsyncStateMachine(typeof(RecoveredType_74))]
	private Task Method_192(uint arg0, Vector3 arg1, CancellationTokenSource arg2)
	{
		RecoveredType_74 stateMachine = default;
		stateMachine.field_428 = AsyncTaskMethodBuilder.Create();
		stateMachine.field_432 = this;
		stateMachine.field_430 = arg0;
		stateMachine.field_431 = arg1;
		stateMachine.field_429 = arg2;
		stateMachine.field_427 = -1;
		stateMachine.field_428.Start(ref stateMachine);
		return stateMachine.field_428.Task;
	}

	[AsyncStateMachine(typeof(RecoveredType_65))]
	private Task Method_193(Vector3 arg0, CancellationTokenSource arg1, CancellationToken arg2)
	{
		RecoveredType_65 stateMachine = default;
		stateMachine.field_308 = AsyncTaskMethodBuilder.Create();
		stateMachine.field_309 = this;
		stateMachine.field_311 = arg0;
		stateMachine.field_312 = arg1;
		stateMachine.field_310 = arg2;
		stateMachine.field_307 = -1;
		stateMachine.field_308.Start(ref stateMachine);
		return stateMachine.field_308.Task;
	}

	[AsyncStateMachine(typeof(RecoveredType_67))]
	private Task Method_194(Vector3 arg0, bool arg1, CancellationTokenSource arg2, CancellationToken arg3)
	{
		RecoveredType_67 stateMachine = default;
		stateMachine.field_327 = AsyncTaskMethodBuilder.Create();
		stateMachine.field_328 = this;
		stateMachine.field_330 = arg0;
		stateMachine.field_333 = arg1;
		stateMachine.field_329 = arg2;
		stateMachine.field_332 = arg3;
		stateMachine.field_326 = -1;
		stateMachine.field_327.Start(ref stateMachine);
		return stateMachine.field_327.Task;
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
				if (value.Any((string value2) => field_243.Contains(value2)))
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
		return value.Any((string value2) => arg0.Contains(value2));
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
