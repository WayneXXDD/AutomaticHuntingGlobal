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
using _00A0;

namespace AutomaticHunting;

public sealed class AutomaticHunting : IDalamudPlugin, IDisposable
{
	private readonly struct _00A0(uint P_0, Vector3 P_1) : IEquatable<_00A0>
	{
		[CompilerGenerated]
		private readonly uint _2002_2004 = P_0;

		[CompilerGenerated]
		private readonly Vector3 _2002_2005 = P_1;

		public uint TerritoryType
		{
			[CompilerGenerated]
			get
			{
				return _2002_2004;
			}
			[CompilerGenerated]
			init
			{
				_2002_2004 = num;
			}
		}

		public Vector3 Position
		{
			[CompilerGenerated]
			get
			{
				return _2002_2005;
			}
			[CompilerGenerated]
			init
			{
				_2002_2005 = vector;
			}
		}

		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("NavigationRequest");
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
			P_0.Append("TerritoryType = ");
			P_0.Append(TerritoryType.ToString());
			P_0.Append(", Position = ");
			P_0.Append(Position.ToString());
			return true;
		}

		[CompilerGenerated]
		public static bool operator !=(_00A0 P_0, _00A0 P_1)
		{
			return !(P_0 == P_1);
		}

		[CompilerGenerated]
		public static bool operator ==(_00A0 P_0, _00A0 P_1)
		{
			return P_0.Equals(P_1);
		}

		[CompilerGenerated]
		public override int GetHashCode()
		{
			return EqualityComparer<uint>.Default.GetHashCode(_2002_2004) * -1521134295 + EqualityComparer<Vector3>.Default.GetHashCode(_2002_2005);
		}

		[CompilerGenerated]
		public override bool Equals(object P_0)
		{
			if (P_0 is _00A0)
			{
				return Equals((_00A0)P_0);
			}
			return false;
		}

		[CompilerGenerated]
		public bool Equals(_00A0 P_0)
		{
			if (EqualityComparer<uint>.Default.Equals(_2002_2004, P_0._2002_2004))
			{
				return EqualityComparer<Vector3>.Default.Equals(_2002_2005, P_0._2002_2005);
			}
			return false;
		}

		[CompilerGenerated]
		public void _00A0(out uint P_0, out Vector3 P_1)
		{
			P_0 = TerritoryType;
			P_1 = Position;
		}
	}

	private readonly struct _1680(string P_0, Vector3 P_1) : IEquatable<_1680>
	{
		[CompilerGenerated]
		private readonly string _2002_2006 = P_0;

		[CompilerGenerated]
		private readonly Vector3 _2002_2007 = P_1;

		public string Name
		{
			[CompilerGenerated]
			get
			{
				return _2002_2006;
			}
			[CompilerGenerated]
			init
			{
				_2002_2006 = text;
			}
		}

		public Vector3 Position
		{
			[CompilerGenerated]
			get
			{
				return _2002_2007;
			}
			[CompilerGenerated]
			init
			{
				_2002_2007 = vector;
			}
		}

		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("MobSnapshot");
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
			P_0.Append("Name = ");
			P_0.Append((object?)Name);
			P_0.Append(", Position = ");
			P_0.Append(Position.ToString());
			return true;
		}

		[CompilerGenerated]
		public static bool operator !=(_1680 P_0, _1680 P_1)
		{
			return !(P_0 == P_1);
		}

		[CompilerGenerated]
		public static bool operator ==(_1680 P_0, _1680 P_1)
		{
			return P_0.Equals(P_1);
		}

		[CompilerGenerated]
		public override int GetHashCode()
		{
			return EqualityComparer<string>.Default.GetHashCode(_2002_2006) * -1521134295 + EqualityComparer<Vector3>.Default.GetHashCode(_2002_2007);
		}

		[CompilerGenerated]
		public override bool Equals(object P_0)
		{
			if (P_0 is _1680)
			{
				return Equals((_1680)P_0);
			}
			return false;
		}

		[CompilerGenerated]
		public bool Equals(_1680 P_0)
		{
			if (EqualityComparer<string>.Default.Equals(_2002_2006, P_0._2002_2006))
			{
				return EqualityComparer<Vector3>.Default.Equals(_2002_2007, P_0._2002_2007);
			}
			return false;
		}

		[CompilerGenerated]
		public void _00A0(out string P_0, out Vector3 P_1)
		{
			P_0 = Name;
			P_1 = Position;
		}
	}

	private readonly struct _2000(string P_0, Vector3 P_1, float P_2) : IEquatable<_2000>
	{
		[CompilerGenerated]
		private readonly string _2002_2008 = P_0;

		[CompilerGenerated]
		private readonly Vector3 _2002_2009 = P_1;

		[CompilerGenerated]
		private readonly float _2002_200A = P_2;

		public string Name
		{
			[CompilerGenerated]
			get
			{
				return _2002_2008;
			}
			[CompilerGenerated]
			init
			{
				_2002_2008 = text;
			}
		}

		public Vector3 Position
		{
			[CompilerGenerated]
			get
			{
				return _2002_2009;
			}
			[CompilerGenerated]
			init
			{
				_2002_2009 = vector;
			}
		}

		public float HpPercent
		{
			[CompilerGenerated]
			get
			{
				return _2002_200A;
			}
			[CompilerGenerated]
			init
			{
				_2002_200A = num;
			}
		}

		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SMobSnapshot");
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
			P_0.Append("Name = ");
			P_0.Append((object?)Name);
			P_0.Append(", Position = ");
			P_0.Append(Position.ToString());
			P_0.Append(", HpPercent = ");
			P_0.Append(HpPercent.ToString());
			return true;
		}

		[CompilerGenerated]
		public static bool operator !=(_2000 P_0, _2000 P_1)
		{
			return !(P_0 == P_1);
		}

		[CompilerGenerated]
		public static bool operator ==(_2000 P_0, _2000 P_1)
		{
			return P_0.Equals(P_1);
		}

		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (EqualityComparer<string>.Default.GetHashCode(_2002_2008) * -1521134295 + EqualityComparer<Vector3>.Default.GetHashCode(_2002_2009)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(_2002_200A);
		}

		[CompilerGenerated]
		public override bool Equals(object P_0)
		{
			if (P_0 is _2000)
			{
				return Equals((_2000)P_0);
			}
			return false;
		}

		[CompilerGenerated]
		public bool Equals(_2000 P_0)
		{
			if (EqualityComparer<string>.Default.Equals(_2002_2008, P_0._2002_2008) && EqualityComparer<Vector3>.Default.Equals(_2002_2009, P_0._2002_2009))
			{
				return EqualityComparer<float>.Default.Equals(_2002_200A, P_0._2002_200A);
			}
			return false;
		}

		[CompilerGenerated]
		public void _00A0(out string P_0, out Vector3 P_1, out float P_2)
		{
			P_0 = Name;
			P_1 = Position;
			P_2 = HpPercent;
		}
	}

	private sealed class _2001
	{
		[CompilerGenerated]
		private string _2002_200B = string.Empty;

		[CompilerGenerated]
		private Vector3 _2002_2010;

		[CompilerGenerated]
		private bool _2002_2011;

		public string Name
		{
			[CompilerGenerated]
			get
			{
				return _2002_200B;
			}
			[CompilerGenerated]
			set
			{
				_2002_200B = text;
			}
		}

		public Vector3 Position
		{
			[CompilerGenerated]
			get
			{
				return _2002_2010;
			}
			[CompilerGenerated]
			set
			{
				_2002_2010 = vector;
			}
		}

		public bool HasBeenDamaged
		{
			[CompilerGenerated]
			get
			{
				return _2002_2011;
			}
			[CompilerGenerated]
			set
			{
				_2002_2011 = flag;
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

	private enum _2003
	{
		_2002_2012,
		_2002_2013,
		_2002_2014,
		_2002_2015,
		_2002_2022,
		_2002_2024
	}

	private enum _2004
	{
		_2002_2025,
		_2002_2027,
		_2002_2028,
		_2002_2029
	}

	private enum _2005
	{
		_2002_202A,
		_2002_202B,
		_2002_202C
	}

	[CompilerGenerated]
	private static class _2006
	{
		public static Func<Vector3?> _2002_202D;

		public static Func<HuntChainStep, HuntChainStep> _2002_202E;

		public static Func<bool> _2002_202F;
	}

	[Serializable]
	[CompilerGenerated]
	private sealed class _2007
	{
		[StructLayout(LayoutKind.Auto)]
		private struct _00A0 : IAsyncStateMachine
		{
			public int _2008_2059;

			public AsyncTaskMethodBuilder _2008_205A;

			private TaskAwaiter _2008_205B;

			private void _00A0()
			{
				int num = _2008_2059;
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
								num = (_2008_2059 = 0);
								_2008_205B = awaiter;
								_2008_205A.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							awaiter = _2008_205B;
							_2008_205B = default;
							num = (_2008_2059 = -1);
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
					_2008_2059 = -2;
					_2008_205A.SetException(exception2);
					return;
				}
				_2008_2059 = -2;
				_2008_205A.SetResult();
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in  
				this._00A0();
			}

			[DebuggerHidden]
			private void _00A0(IAsyncStateMachine P_0)
			{
				_2008_205A.SetStateMachine(P_0);
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine P_0)
			{
				//ILSpy generated this explicit interface implementation from .override directive in  
				this._00A0(P_0);
			}
		}

		public static readonly _2007 _2002_2032 = new _2007();

		public static Func<Task?> _2002_2035;

		public static Func<bool> _2002_2033;

		public static Func<bool> _2002_2036;

		public static Func<IExposedPlugin, bool> _2002_203E;

		public static Comparison<MountInfo> _2002_2047;

		public static Func<MountInfo, string> _2002_2048;

		public static Func<string, List<string>> _2002_2049;

		public static Func<(bool, bool)> _2002_204A;

		public static Func<bool> _2002_204B;

		public static Func<uint> _2002_204C;

		public static Func<string> _2002_204D;

		public static Func<((uint Id, Vector3 Position, string Name) Aetheryte, float Corrected), float> _2002_204E;

		public static Func<bool> _2002_204F;

		public static Func<bool> _2002_2050;

		public static Func<bool> _2002_2051;

		public static Func<bool> _2002_2052;

		public static Func<bool> _2002_2053;

		public static Func<bool> _2002_2054;

		[AsyncStateMachine(typeof(_00A0))]
		internal Task? _00A0()
		{
			_00A0 stateMachine = default;
			stateMachine._2008_205A = AsyncTaskMethodBuilder.Create();
			stateMachine._2008_2059 = -1;
			stateMachine._2008_205A.Start(ref stateMachine);
			return stateMachine._2008_205A.Task;
		}

		internal bool _1680()
		{
			if (!Condition[ConditionFlag.BetweenAreas])
			{
				return Condition[ConditionFlag.BetweenAreas51];
			}
			return true;
		}

		internal bool _2000()
		{
			return _2024();
		}

		internal bool _00A0(IExposedPlugin P_0)
		{
			if (P_0.IsLoaded)
			{
				if (!P_0.InternalName.Equals("BossModReborn", StringComparison.OrdinalIgnoreCase))
				{
					return P_0.InternalName.Equals("BossMod", StringComparison.OrdinalIgnoreCase);
				}
				return true;
			}
			return false;
		}

		internal int _00A0(MountInfo P_0, MountInfo P_1)
		{
			return string.Compare(P_0.Name, P_1.Name, StringComparison.OrdinalIgnoreCase);
		}

		internal string _00A0(MountInfo P_0)
		{
			return P_0.Name;
		}

		internal List<string> _00A0(string P_0)
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
						if (text.Length > P_0.Length && text.Contains(P_0))
						{
							list.Add(text);
						}
					}
				}
			}
			catch (Exception exception)
			{
				Log.Warning(exception, "AutomaticHunting: 构建地名剔除表失败 [" + P_0 + "]");
			}
			return list;
		}

		internal (bool, bool) _2001()
		{
			return (Condition[ConditionFlag.ReadyingVisitOtherWorld], Condition[ConditionFlag.BetweenAreas] || Condition[ConditionFlag.BetweenAreas51]);
		}

		internal bool _2002()
		{
			return _2024();
		}

		internal uint _2003()
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

		internal string _2004()
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

		internal float _00A0(((uint Id, Vector3 Position, string Name) Aetheryte, float Corrected) x)
		{
			return x.Corrected;
		}

		internal bool _2005()
		{
			if (!Condition[ConditionFlag.BetweenAreas])
			{
				return Condition[ConditionFlag.BetweenAreas51];
			}
			return true;
		}

		internal bool _2006()
		{
			return Condition[ConditionFlag.Mounted];
		}

		internal bool _2007()
		{
			return Condition[ConditionFlag.Mounted];
		}

		internal bool _2008()
		{
			return Condition[ConditionFlag.Mounted];
		}

		internal bool _2009()
		{
			return _2024();
		}

		internal bool _200A()
		{
			return Condition[ConditionFlag.Mounted];
		}
	}

	[CompilerGenerated]
	private sealed class _2008
	{
		public uint _2002_2055;

		internal bool _00A0(MultiLineMapConfig P_0)
		{
			if (!string.IsNullOrWhiteSpace(P_0.MapId) && uint.TryParse(P_0.MapId.Trim(), out var result))
			{
				return result == _2002_2055;
			}
			return false;
		}
	}

	[CompilerGenerated]
	private sealed class _2009
	{
		public uint _2002_2056;

		internal bool _00A0(MultiLineMapConfig P_0)
		{
			if (!string.IsNullOrWhiteSpace(P_0.MapId) && uint.TryParse(P_0.MapId.Trim(), out var result))
			{
				return result == _2002_2056;
			}
			return false;
		}
	}

	[CompilerGenerated]
	private sealed class _200A
	{
		public string _2002_2057;

		public AutomaticHunting _2002_2058;

		internal void _00A0()
		{
			try
			{
				CommandManager.ProcessCommand(_2002_2057);
				Log.Information("AutomaticHunting: 执行切线命令 " + _2002_2057);
			}
			catch (Exception exception)
			{
				_2002_2058.PrintEcho("切线命令执行失败: " + _2002_2057);
				Log.Warning(exception, "AutomaticHunting: 切线命令失败 " + _2002_2057);
			}
		}
	}

	[CompilerGenerated]
	private sealed class _200B
	{
		public int _2002_2059;

		internal void _00A0()
		{
			try
			{
				CommandManager.ProcessCommand($"/pdr insc {_2002_2059}");
			}
			catch (Exception exception)
			{
				Log.Warning(exception, $"AutomaticHunting: 切线重发失败 {_2002_2059}");
			}
		}
	}

	[CompilerGenerated]
	private sealed class _2010
	{
		public uint _2002_205A;

		public Vector3? _2002_205B;

		internal List<(uint Id, Vector3 Position, string Name)> _00A0()
		{
			return _2002(_2002_205A);
		}

		internal float _00A0((uint Id, Vector3 Position, string Name) a)
		{
			return Vector3.DistanceSquared(a.Position, _2002_205B.Value);
		}
	}

	[CompilerGenerated]
	private sealed class _2011
	{
		[StructLayout(LayoutKind.Auto)]
		private struct _00A0 : IAsyncStateMachine
		{
			public int _2008_205C;

			public AsyncTaskMethodBuilder _2008_205D;

			public _2011 _2008_205E;

			private TaskAwaiter _2008_205F;

			private void _00A0()
			{
				int num = _2008_205C;
				_2011 obj = _2008_205E;
				try
				{
					TaskAwaiter awaiter;
					if (num != 0)
					{
						awaiter = Task.Delay(2000).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_2008_205C = 0);
							_2008_205F = awaiter;
							_2008_205D.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = _2008_205F;
						_2008_205F = default;
						num = (_2008_205C = -1);
					}
					awaiter.GetResult();
					if (obj._2002_205C.SMonitor == obj._2002_205D)
					{
						obj._2002_205C.SMonitor = null;
					}
				}
				catch (Exception exception)
				{
					_2008_205C = -2;
					_2008_205D.SetException(exception);
					return;
				}
				_2008_205C = -2;
				_2008_205D.SetResult();
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in  
				this._00A0();
			}

			[DebuggerHidden]
			private void _00A0(IAsyncStateMachine P_0)
			{
				_2008_205D.SetStateMachine(P_0);
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine P_0)
			{
				//ILSpy generated this explicit interface implementation from .override directive in  
				this._00A0(P_0);
			}
		}

		public AutomaticHunting _2002_205C;

		public SMonitorStatus _2002_205D;

		[AsyncStateMachine(typeof(_00A0))]
		internal Task? _00A0()
		{
			_00A0 stateMachine = default;
			stateMachine._2008_205D = AsyncTaskMethodBuilder.Create();
			stateMachine._2008_205E = this;
			stateMachine._2008_205C = -1;
			stateMachine._2008_205D.Start(ref stateMachine);
			return stateMachine._2008_205D.Task;
		}
	}

	[CompilerGenerated]
	private sealed class _2012
	{
		public string _2002_205E;

		internal bool _00A0(string P_0)
		{
			return _2002_205E.Contains(P_0);
		}
	}

	[CompilerGenerated]
	private sealed class _2013
	{
		public AutomaticHunting _2002_205F;

		public string _2002_2060;

		public uint _2002_2061;

		public string _2002_2062;

		internal Task? _00A0()
		{
			return _2002_205F._00A0(_2002_2060, _2002_2061, _2002_2062, _2002_205F.m__2054.Token);
		}
	}

	[CompilerGenerated]
	private sealed class _2014
	{
		public uint _2002_2063;

		internal List<(uint Id, Vector3 Position, string Name)> _00A0()
		{
			return _2002(_2002_2063);
		}
	}

	[CompilerGenerated]
	private sealed class _2015
	{
		public uint _2002_2064;

		internal List<(uint Id, Vector3 Position, string Name)> _00A0()
		{
			return _2002(_2002_2064);
		}
	}

	[CompilerGenerated]
	private sealed class _2022
	{
		public string _2002_206A;

		internal void _00A0()
		{
			try
			{
				CommandManager.ProcessCommand(_2002_206A);
			}
			catch (Exception exception)
			{
				Log.Warning(exception, "AutomaticHunting: 执行跨服命令失败 " + _2002_206A);
			}
		}
	}

	[CompilerGenerated]
	private sealed class _2024
	{
		public AutomaticHunting _2002_206B;

		public CancellationToken _2002_206C;

		public int _2002_206D;

		public DateTime? _2002_206E;

		public DateTime? _2002_206F;

		public Vector3? _2002_3000;

		public DateTime _2003_00A0;

		public int _2003_1680;

		public uint _2003_2000;

		public Func<(bool Between, bool Foreign)> _2003_2001;

		internal void _00A0()
		{
			_2002_206B._2004("/vnav stop");
		}

		internal (bool Between, bool Foreign) _1680()
		{
			_2002_206C.ThrowIfCancellationRequested();
			if (Condition[ConditionFlag.BetweenAreas] || Condition[ConditionFlag.BetweenAreas51])
			{
				return (Between: _2002_206D > 0, Foreign: _2002_206D == 0);
			}
			if (_2014())
			{
				DateTime valueOrDefault = _2002_206E.GetValueOrDefault();
				if (!_2002_206E.HasValue)
				{
					valueOrDefault = DateTime.UtcNow;
					_2002_206E = valueOrDefault;
				}
				_2002_206F = null;
				return (Between: false, Foreign: false);
			}
			if (_2002_206E.HasValue)
			{
				DateTime valueOrDefault = _2002_206F.GetValueOrDefault();
				if (!_2002_206F.HasValue)
				{
					valueOrDefault = DateTime.UtcNow;
					_2002_206F = valueOrDefault;
				}
				if (DateTime.UtcNow - _2002_206F.Value < TimeSpan.FromSeconds(2L))
				{
					return (Between: false, Foreign: false);
				}
			}
			Vector3? localPlayerPosition = GetLocalPlayerPosition();
			bool num = localPlayerPosition.HasValue && _2002_3000.HasValue && Vector3.DistanceSquared(localPlayerPosition.Value, _2002_3000.Value) < 0.0001f;
			_2002_3000 = localPlayerPosition;
			if (!num || _2024() || _2014() || Condition[ConditionFlag.MountOrOrnamentTransition] || DateTime.UtcNow < _2003_00A0 || _2002_206B._2015() || !_2013())
			{
				return (Between: false, Foreign: false);
			}
			_2003_1680++;
			bool flag = _2002_206B._2001(_2003_2000);
			if (flag)
			{
				_2002_206D++;
			}
			_2003_00A0 = DateTime.UtcNow.AddSeconds((!flag) ? 1 : 2);
			return (Between: false, Foreign: false);
		}
	}

	[CompilerGenerated]
	private sealed class _2025
	{
		public uint _2003_2002;

		internal bool _00A0(Lumina.Excel.Sheets.Map P_0)
		{
			return P_0.TerritoryType.RowId == _2003_2002;
		}
	}

	[CompilerGenerated]
	private sealed class _2027
	{
		public Aetheryte _2003_2003;

		internal bool _00A0(Lumina.Excel.Sheets.Map P_0)
		{
			return P_0.TerritoryType.RowId == _2003_2003.Territory.RowId;
		}
	}

	[CompilerGenerated]
	private sealed class _2028
	{
		public string _2003_2004;

		internal bool _00A0(IExposedPlugin P_0)
		{
			if (P_0.InternalName == _2003_2004)
			{
				return P_0.IsLoaded;
			}
			return false;
		}
	}

	[CompilerGenerated]
	private sealed class _2029
	{
		public uint _2003_2005;

		internal bool _00A0(Lumina.Excel.Sheets.Map P_0)
		{
			return P_0.TerritoryType.RowId == _2003_2005;
		}
	}

	[CompilerGenerated]
	private sealed class _202A
	{
		public WorldTravelPreset _2003_2006;

		internal bool _00A0(WorldTravelData.MapEntry P_0)
		{
			return P_0.RowId == _2003_2006.MapRowId;
		}

		internal bool _00A0((uint Id, Vector3 Position, string Name) a)
		{
			return a.Id == _2003_2006.AetheryteId;
		}
	}

	[CompilerGenerated]
	private sealed class _202B
	{
		[StructLayout(LayoutKind.Auto)]
		private struct _00A0 : IAsyncStateMachine
		{
			public int _2008_2060;

			public AsyncTaskMethodBuilder _2008_2061;

			public _202B _2008_2062;

			private TaskAwaiter _2008_2063;

			private void _00A0()
			{
				int num = _2008_2060;
				_202B obj = _2008_2062;
				try
				{
					TaskAwaiter awaiter;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = _2008_2063;
							_2008_2063 = default;
							num = (_2008_2060 = -1);
							goto IL_00e6;
						}
						if (obj._2003_2007 == null)
						{
							goto IL_0084;
						}
					}
					try
					{
						if (num != 0)
						{
							awaiter = obj._2003_2007.GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (_2008_2060 = 0);
								_2008_2063 = awaiter;
								_2008_2061.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							awaiter = _2008_2063;
							_2008_2063 = default;
							num = (_2008_2060 = -1);
						}
						awaiter.GetResult();
					}
					catch
					{
					}
					goto IL_0084;
					IL_0084:
					awaiter = obj._2003_2008._00A0(obj._2003_2009, obj._2003_200A).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (_2008_2060 = 1);
						_2008_2063 = awaiter;
						_2008_2061.AwaitUnsafeOnCompleted(ref awaiter, ref this);
						return;
					}
					goto IL_00e6;
					IL_00e6:
					awaiter.GetResult();
				}
				catch (Exception exception)
				{
					_2008_2060 = -2;
					_2008_2061.SetException(exception);
					return;
				}
				_2008_2060 = -2;
				_2008_2061.SetResult();
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in  
				this._00A0();
			}

			[DebuggerHidden]
			private void _00A0(IAsyncStateMachine P_0)
			{
				_2008_2061.SetStateMachine(P_0);
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine P_0)
			{
				//ILSpy generated this explicit interface implementation from .override directive in  
				this._00A0(P_0);
			}
		}

		public Task _2003_2007;

		public AutomaticHunting _2003_2008;

		public AutomaticHunting._00A0 _2003_2009;

		public CancellationTokenSource _2003_200A;

		[AsyncStateMachine(typeof(_00A0))]
		internal Task? _00A0()
		{
			_00A0 stateMachine = default;
			stateMachine._2008_2061 = AsyncTaskMethodBuilder.Create();
			stateMachine._2008_2062 = this;
			stateMachine._2008_2060 = -1;
			stateMachine._2008_2061.Start(ref stateMachine);
			return stateMachine._2008_2061.Task;
		}
	}

	[CompilerGenerated]
	private sealed class _202C
	{
		public AutomaticHunting _2003_200B;

		public _00A0 _2003_2010;

		internal (_2005 Preparation, uint AetheryteId, bool SameTerritory) _00A0()
		{
			uint item;
			return (Preparation: _2003_200B._00A0(_2003_2010, out item), AetheryteId: item, SameTerritory: _2012() == _2003_2010.TerritoryType);
		}
	}

	[CompilerGenerated]
	private sealed class _202D
	{
		public AutomaticHunting _2003_2011;

		public _00A0 _2003_2012;

		internal ((uint Id, Vector3 Position, string Name) Aetheryte, float Corrected) _00A0((uint Id, Vector3 Position, string Name) a)
		{
			return (Aetheryte: a, Corrected: _2003_2011._00A0(a.Id, a.Position, _2003_2012.Position));
		}
	}

	[CompilerGenerated]
	private sealed class _202E
	{
		public uint _2003_2013;

		internal bool _00A0((uint Id, Vector3 Position, string Name) a)
		{
			return a.Id == _2003_2013;
		}
	}

	[CompilerGenerated]
	private sealed class _202F
	{
		public uint _2003_2014;

		public Func<bool> _2003_2015;

		public Func<bool> _2003_2022;

		internal bool _00A0()
		{
			if (_2012() == _2003_2014)
			{
				return GetLocalPlayerPosition().HasValue;
			}
			return false;
		}

		internal bool _1680()
		{
			if (!_2024() && !Condition[ConditionFlag.Casting] && _2012() == _2003_2014)
			{
				return GetLocalPlayerPosition().HasValue;
			}
			return false;
		}
	}

	[CompilerGenerated]
	private sealed class _2032
	{
		public AutomaticHunting _2003_2024;

		public Vector3 _2003_2025;

		public _00A0 _2003_2027;

		public Func<bool> _2003_2028;

		public Func<bool> _2003_2029;

		internal bool _00A0()
		{
			Vector3 valueOrDefault = _2003_2024._1680(_2003_2025).GetValueOrDefault(_2003_2025);
			ICallGateSubscriber<bool>? callGateSubscriber = _2003_2024.m__2012;
			if (callGateSubscriber != null && callGateSubscriber.InvokeFunc())
			{
				return _2003_2024.m__2013?.InvokeFunc(valueOrDefault, arg2: true) ?? false;
			}
			return false;
		}

		internal bool _1680()
		{
			if (_2003_2024.Config.SetFlagOnNavigation)
			{
				return AutomaticHunting._1680(_2003_2027.TerritoryType, 0u, _2003_2027.Position);
			}
			return true;
		}
	}

	[CompilerGenerated]
	private sealed class _2035
	{
		public Vector3 _2003_202A;

		public AutomaticHunting _2003_202B;

		public Func<bool> _2003_202C;

		internal bool _00A0()
		{
			Vector3? vector = _2003_202B._1680(_2003_202A);
			Vector3 arg = (vector.HasValue ? new Vector3(vector.Value.X, _2003_202A.Y, vector.Value.Z) : _2003_202A);
			ICallGateSubscriber<bool>? callGateSubscriber = _2003_202B.m__2012;
			if (callGateSubscriber != null && callGateSubscriber.InvokeFunc())
			{
				return _2003_202B.m__2013?.InvokeFunc(arg, arg2: true) ?? false;
			}
			return false;
		}
	}

	[CompilerGenerated]
	private sealed class _2033
	{
		public _2000? _2003_202D;

		public _2035 _2003_202E;

		internal string _00A0()
		{
			return _2003_202E._2003_202B._00A0(_2003_202D.Value);
		}
	}

	[CompilerGenerated]
	private sealed class _2036
	{
		public Vector3 _2003_202F;

		public _2033 _2003_2032;

		internal bool _00A0()
		{
			Vector3? vector = _2003_2032._2003_202E._2003_202B._1680(_2003_202F);
			Vector3 arg = (vector.HasValue ? new Vector3(vector.Value.X, _2003_202F.Y, vector.Value.Z) : _2003_202F);
			ICallGateSubscriber<bool>? callGateSubscriber = _2003_2032._2003_202E._2003_202B.m__2012;
			if (callGateSubscriber != null && callGateSubscriber.InvokeFunc())
			{
				return _2003_2032._2003_202E._2003_202B.m__2013?.InvokeFunc(arg, arg2: true) ?? false;
			}
			return false;
		}
	}

	[CompilerGenerated]
	private sealed class _203E
	{
		public AutomaticHunting _2003_2035;

		public Vector3 _2003_2033;

		public Func<(Vector3? player, Vector3? mob)> _2003_2036;

		internal (Vector3? player, Vector3? mob) _00A0()
		{
			Vector3? localPlayerPosition = GetLocalPlayerPosition();
			Vector3? item = (_2003_2035._00A0(_2003_2033, false, out var value) ? new Vector3?(value) : ((Vector3?)null));
			return (player: localPlayerPosition, mob: item);
		}
	}

	[CompilerGenerated]
	private sealed class _2047
	{
		public AutomaticHunting _2003_203E;

		public Vector3 _2003_2047;

		public bool _2003_2048;

		internal Vector3? _00A0()
		{
			if (!_2003_203E._00A0(_2003_2047, _2003_2048, out var vector))
			{
				return null;
			}
			Vector3 vector2 = vector + _2003_203E._00A0(vector);
			Vector3 arg = _2003_203E._1680(vector2) ?? vector2;
			ICallGateSubscriber<bool>? callGateSubscriber = _2003_203E.m__2012;
			if (callGateSubscriber != null && callGateSubscriber.InvokeFunc())
			{
				ICallGateSubscriber<Vector3, bool, bool>? callGateSubscriber2 = _2003_203E.m__2013;
				if (callGateSubscriber2 != null && callGateSubscriber2.InvokeFunc(arg, arg2: true))
				{
					return vector2;
				}
			}
			return null;
		}
	}

	[CompilerGenerated]
	private sealed class _2048
	{
		public _1680? _2003_2049;

		internal bool _00A0(IGameObject P_0)
		{
			return P_0.Name.TextValue == _2003_2049.Value.Name;
		}

		internal float _1680(IGameObject P_0)
		{
			return AutomaticHunting._00A0(P_0.Position, _2003_2049.Value.Position);
		}
	}

	[CompilerGenerated]
	private sealed class _2049
	{
		public string _2003_204A;

		public string _2003_204B;

		internal void _00A0()
		{
			try
			{
				if (_2003_204A == null || !_2005(_2003_204A))
				{
					string content = ((!_2003_204B.StartsWith('/')) ? ("/" + _2003_204B) : _2003_204B);
					CommandManager.ProcessCommand(content);
				}
			}
			catch (Exception exception)
			{
				Log.Warning(exception, "AutomaticHunting: 执行突进技能失败 " + _2003_204B);
			}
		}
	}

	[CompilerGenerated]
	private sealed class _204A
	{
		public string _2003_204C;

		internal void _00A0()
		{
			try
			{
				CommandManager.ProcessCommand(_2003_204C);
			}
			catch (Exception exception)
			{
				Log.Warning(exception, "AutomaticHunting: 执行命令失败 " + _2003_204C);
			}
		}
	}

	[CompilerGenerated]
	private sealed class _204B
	{
		public string _2003_204D;

		internal bool _00A0(Lumina.Excel.Sheets.Action P_0)
		{
			return P_0.Name.ExtractText() == _2003_204D;
		}
	}

	[CompilerGenerated]
	private sealed class _204C
	{
		[StructLayout(LayoutKind.Auto)]
		private struct _00A0 : IAsyncStateMachine
		{
			public int _2008_2064;

			public AsyncTaskMethodBuilder _2008_206A;

			public _204C _2008_206B;

			private TaskAwaiter _2008_206C;

			private void _00A0()
			{
				int num = _2008_2064;
				_204C obj = _2008_206B;
				try
				{
					TaskAwaiter awaiter;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = _2008_206C;
							_2008_206C = default;
							num = (_2008_2064 = -1);
							goto IL_00ec;
						}
						if (obj._2003_204E == null)
						{
							goto IL_0084;
						}
					}
					try
					{
						if (num != 0)
						{
							awaiter = obj._2003_204E.GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (_2008_2064 = 0);
								_2008_206C = awaiter;
								_2008_206A.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							awaiter = _2008_206C;
							_2008_206C = default;
							num = (_2008_2064 = -1);
						}
						awaiter.GetResult();
					}
					catch
					{
					}
					goto IL_0084;
					IL_0084:
					awaiter = obj._2003_204F._00A0(obj._2003_2050, obj._2003_2051, obj._2003_2052).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (_2008_2064 = 1);
						_2008_206C = awaiter;
						_2008_206A.AwaitUnsafeOnCompleted(ref awaiter, ref this);
						return;
					}
					goto IL_00ec;
					IL_00ec:
					awaiter.GetResult();
				}
				catch (Exception exception)
				{
					_2008_2064 = -2;
					_2008_206A.SetException(exception);
					return;
				}
				_2008_2064 = -2;
				_2008_206A.SetResult();
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in  
				this._00A0();
			}

			[DebuggerHidden]
			private void _00A0(IAsyncStateMachine P_0)
			{
				_2008_206A.SetStateMachine(P_0);
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine P_0)
			{
				//ILSpy generated this explicit interface implementation from .override directive in  
				this._00A0(P_0);
			}
		}

		public Task _2003_204E;

		public AutomaticHunting _2003_204F;

		public uint _2003_2050;

		public Vector3 _2003_2051;

		public CancellationTokenSource _2003_2052;

		[AsyncStateMachine(typeof(_00A0))]
		internal Task? _00A0()
		{
			_00A0 stateMachine = default;
			stateMachine._2008_206A = AsyncTaskMethodBuilder.Create();
			stateMachine._2008_206B = this;
			stateMachine._2008_2064 = -1;
			stateMachine._2008_206A.Start(ref stateMachine);
			return stateMachine._2008_206A.Task;
		}
	}

	[CompilerGenerated]
	private sealed class _204D
	{
		public AutomaticHunting _2003_2053;

		public Vector3 _2003_2054;

		public Func<bool> _2003_2055;

		internal bool _00A0()
		{
			Vector3 valueOrDefault = _2003_2053._1680(_2003_2054).GetValueOrDefault(_2003_2054);
			ICallGateSubscriber<bool>? callGateSubscriber = _2003_2053.m__2012;
			if (callGateSubscriber != null && callGateSubscriber.InvokeFunc())
			{
				return _2003_2053.m__2013?.InvokeFunc(valueOrDefault, arg2: true) ?? false;
			}
			return false;
		}
	}

	[CompilerGenerated]
	private sealed class _204E
	{
		public string _2003_2056;

		internal bool _00A0(string P_0)
		{
			return _2003_2056.Contains(P_0);
		}
	}

	[CompilerGenerated]
	private sealed class _204F
	{
		public string _2003_2057;

		internal bool _00A0(string P_0)
		{
			return _2003_2057.Contains(P_0);
		}
	}

	[CompilerGenerated]
	private sealed class _2050
	{
		public _2000 _2003_2058;

		internal bool _00A0(IGameObject P_0)
		{
			return P_0.Name.TextValue == _2003_2058.Name;
		}

		internal float _1680(IGameObject P_0)
		{
			return AutomaticHunting._00A0(P_0.Position, _2003_2058.Position);
		}
	}

	[CompilerGenerated]
	private sealed class _2051
	{
		public HashSet<string> _2003_2059;

		internal bool _00A0(KeyValuePair<string, _2001> P_0)
		{
			return !_2003_2059.Contains(P_0.Key);
		}
	}

	[CompilerGenerated]
	private sealed class _2052
	{
		public string _2003_205A;

		internal bool _00A0(string P_0)
		{
			return _2003_205A.Contains(P_0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	private struct _2053 : IAsyncStateMachine
	{
		public int _2003_205B;

		public AsyncTaskMethodBuilder _2003_205C;

		public AutomaticHunting _2003_205D;

		public CancellationToken _2003_205E;

		private int _2003_205F;

		private TaskAwaiter _2003_2060;

		private void _00A0()
		{
			int num = _2003_205B;
			AutomaticHunting automaticHunting = _2003_205D;
			try
			{
				if (num != 0)
				{
					_2003_205F = 0;
					goto IL_0092;
				}
				TaskAwaiter awaiter = _2003_2060;
				_2003_2060 = default;
				num = (_2003_205B = -1);
				goto IL_007b;
				IL_007b:
				awaiter.GetResult();
				_2003_205F++;
				goto IL_0092;
				IL_0092:
				if (_2003_205F < 5)
				{
					automaticHunting.ExecuteDashSkill();
					awaiter = Task.Delay(500, _2003_205E).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (_2003_205B = 0);
						_2003_2060 = awaiter;
						_2003_205C.AwaitUnsafeOnCompleted(ref awaiter, ref this);
						return;
					}
					goto IL_007b;
				}
			}
			catch (Exception exception)
			{
				_2003_205B = -2;
				_2003_205C.SetException(exception);
				return;
			}
			_2003_205B = -2;
			_2003_205C.SetResult();
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0();
		}

		[DebuggerHidden]
		private void _00A0(IAsyncStateMachine P_0)
		{
			_2003_205C.SetStateMachine(P_0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine P_0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0(P_0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	private struct _2054 : IAsyncStateMachine
	{
		public int _2003_2061;

		public AsyncTaskMethodBuilder _2003_2062;

		public int _2003_2063;

		public AutomaticHunting _2003_2064;

		private _200B _2003_206A;

		private bool _2003_206B;

		private TaskAwaiter<bool> _2003_206C;

		private TaskAwaiter _2003_206D;

		private void _00A0()
		{
			int num = _2003_2061;
			AutomaticHunting automaticHunting = _2003_2064;
			try
			{
				if ((uint)num > 3u)
				{
					_2003_206A = new _200B();
					_2003_206A._2002_2059 = _2003_2063;
				}
				try
				{
					TaskAwaiter awaiter2;
					TaskAwaiter<bool> awaiter;
					switch (num)
					{
					default:
						awaiter = automaticHunting._00A0(_2003_206A._2002_2059, TimeSpan.FromSeconds(5L)).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_2003_2061 = 0);
							_2003_206C = awaiter;
							_2003_2062.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_00ab;
					case 0:
						awaiter = _2003_206C;
						_2003_206C = default;
						num = (_2003_2061 = -1);
						goto IL_00ab;
					case 1:
						awaiter2 = _2003_206D;
						_2003_206D = default;
						num = (_2003_2061 = -1);
						goto IL_013a;
					case 2:
						awaiter2 = _2003_206D;
						_2003_206D = default;
						num = (_2003_2061 = -1);
						goto IL_01df;
					case 3:
						{
							awaiter = _2003_206C;
							_2003_206C = default;
							num = (_2003_2061 = -1);
							break;
						}
						IL_01e6:
						awaiter = automaticHunting._00A0(_2003_206A._2002_2059, TimeSpan.FromSeconds(_2003_206B ? 20 : 10)).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_2003_2061 = 3);
							_2003_206C = awaiter;
							_2003_2062.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						break;
						IL_00ab:
						if (!awaiter.GetResult())
						{
							_2003_206B = automaticHunting.Config.SwitchRetryAtAetheryte;
							if (_2003_206B)
							{
								awaiter2 = automaticHunting._200A().GetAwaiter();
								if (!awaiter2.IsCompleted)
								{
									num = (_2003_2061 = 1);
									_2003_206D = awaiter2;
									_2003_2062.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
									return;
								}
								goto IL_013a;
							}
							goto IL_01e6;
						}
						automaticHunting._2000(_2003_206A._2002_2059);
						goto end_IL_002f;
						IL_01df:
						awaiter2.GetResult();
						goto IL_01e6;
						IL_013a:
						awaiter2.GetResult();
						automaticHunting.PrintEcho($"切线未确认，重发命令 /pdr insc {_2003_206A._2002_2059}");
						awaiter2 = Framework.RunOnFrameworkThread(() =>
						{
							try
							{
								CommandManager.ProcessCommand($"/pdr insc {_2003_206A._2002_2059}");
							}
							catch (Exception exception3)
							{
								Log.Warning(exception3, $"AutomaticHunting: 切线重发失败 {_2003_206A._2002_2059}");
							}
						}).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (_2003_2061 = 2);
							_2003_206D = awaiter2;
							_2003_2062.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_01df;
					}
					if (awaiter.GetResult())
					{
						automaticHunting._2000(_2003_206A._2002_2059);
					}
					else
					{
						automaticHunting.PrintEcho($"切线确认超时，真实线未达到目标 {_2003_206A._2002_2059} 线");
						Log.Warning($"AutomaticHunting: 切线确认超时(目标 {_2003_206A._2002_2059} 线)");
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
				_2003_2061 = -2;
				_2003_206A = null;
				_2003_2062.SetException(exception2);
				return;
			}
			_2003_2061 = -2;
			_2003_206A = null;
			_2003_2062.SetResult();
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0();
		}

		[DebuggerHidden]
		private void _00A0(IAsyncStateMachine P_0)
		{
			_2003_2062.SetStateMachine(P_0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine P_0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0(P_0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	private struct _2055 : IAsyncStateMachine
	{
		public int _2003_206E;

		public AsyncTaskMethodBuilder<bool> _2003_206F;

		public CancellationToken _2003_3000;

		private int _2004_00A0;

		private TaskAwaiter<bool> _2004_1680;

		private TaskAwaiter _2004_2000;

		private void _00A0()
		{
			int num = _2003_206E;
			bool result;
			try
			{
				TaskAwaiter awaiter2;
				TaskAwaiter<bool> awaiter;
				switch (num)
				{
				default:
					_2004_00A0 = 0;
					goto IL_01a2;
				case 0:
					awaiter = _2004_1680;
					_2004_1680 = default;
					num = (_2003_206E = -1);
					goto IL_00a0;
				case 1:
					awaiter = _2004_1680;
					_2004_1680 = default;
					num = (_2003_206E = -1);
					goto IL_0123;
				case 2:
					awaiter2 = _2004_2000;
					_2004_2000 = default;
					num = (_2003_206E = -1);
					goto IL_0189;
				case 3:
					{
						awaiter = _2004_1680;
						_2004_1680 = default;
						num = (_2003_206E = -1);
						break;
					}
					IL_01a2:
					if (_2004_00A0 < 10)
					{
						awaiter = Framework.RunOnFrameworkThread(() => Condition[ConditionFlag.Mounted]).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_2003_206E = 0);
							_2004_1680 = awaiter;
							_2003_206F.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_00a0;
					}
					awaiter = Framework.RunOnFrameworkThread(() => Condition[ConditionFlag.Mounted]).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (_2003_206E = 3);
						_2004_1680 = awaiter;
						_2003_206F.AwaitUnsafeOnCompleted(ref awaiter, ref this);
						return;
					}
					break;
					IL_0189:
					awaiter2.GetResult();
					_2004_00A0++;
					goto IL_01a2;
					IL_00a0:
					if (awaiter.GetResult())
					{
						awaiter = Framework.RunOnFrameworkThread((Func<bool>)UseDismount).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_2003_206E = 1);
							_2004_1680 = awaiter;
							_2003_206F.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_0123;
					}
					result = true;
					goto end_IL_0007;
					IL_0123:
					awaiter.GetResult();
					awaiter2 = Task.Delay(500, _2003_3000).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						num = (_2003_206E = 2);
						_2004_2000 = awaiter2;
						_2003_206F.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
						return;
					}
					goto IL_0189;
				}
				result = !awaiter.GetResult();
				end_IL_0007:;
			}
			catch (Exception exception)
			{
				_2003_206E = -2;
				_2003_206F.SetException(exception);
				return;
			}
			_2003_206E = -2;
			_2003_206F.SetResult(result);
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0();
		}

		[DebuggerHidden]
		private void _00A0(IAsyncStateMachine P_0)
		{
			_2003_206F.SetStateMachine(P_0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine P_0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0(P_0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	private struct _2056 : IAsyncStateMachine
	{
		public int _2004_2001;

		public AsyncTaskMethodBuilder _2004_2002;

		public AutomaticHunting _2004_2003;

		public float _2004_2004;

		public _2000 _2004_2005;

		public CancellationToken _2004_2006;

		private TaskAwaiter<bool> _2004_2007;

		private TaskAwaiter _2004_2008;

		private void _00A0()
		{
			int num = _2004_2001;
			AutomaticHunting automaticHunting = _2004_2003;
			try
			{
				TaskAwaiter<bool> awaiter2;
				TaskAwaiter awaiter;
				bool result;
				switch (num)
				{
				default:
				{
					automaticHunting._2004("/vnav stop");
					SMonitorStatus sMonitor = automaticHunting.SMonitor;
					if (sMonitor != null)
					{
						sMonitor.Phase = "开怪中";
						sMonitor.HpPercent = _2004_2004;
					}
					else
					{
						automaticHunting.BeginSMonitor(_2004_2005.Name, "开怪中", _2004_2004, automaticHunting.Config.SHpThreshold);
					}
					if (!automaticHunting.Config.SPathToEngage)
					{
						automaticHunting.PrintEcho($"S怪血量{_2004_2004:0.0}%≤{automaticHunting.Config.SHpThreshold:0}%，原地开怪: 下坐骑");
						awaiter2 = automaticHunting._1680(_2004_2006).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (_2004_2001 = 0);
							_2004_2007 = awaiter2;
							_2004_2002.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_0150;
					}
					string value = ((automaticHunting._00A0(_2004_2005.Position) == Vector3.Zero) ? "" : "（含落点偏移）");
					automaticHunting.PrintEcho($"S怪血量{_2004_2004:0.0}%≤{automaticHunting.Config.SHpThreshold:0}%，寻路开怪: 前往 {_2004_2005.Name} 坐标{value}");
					awaiter2 = automaticHunting._00A0(_2004_2005, _2004_2006).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						num = (_2004_2001 = 2);
						_2004_2007 = awaiter2;
						_2004_2002.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
						return;
					}
					goto IL_0301;
				}
				case 0:
					awaiter2 = _2004_2007;
					_2004_2007 = default;
					num = (_2004_2001 = -1);
					goto IL_0150;
				case 1:
					awaiter = _2004_2008;
					_2004_2008 = default;
					num = (_2004_2001 = -1);
					goto IL_01de;
				case 2:
					awaiter2 = _2004_2007;
					_2004_2007 = default;
					num = (_2004_2001 = -1);
					goto IL_0301;
				case 3:
					awaiter2 = _2004_2007;
					_2004_2007 = default;
					num = (_2004_2001 = -1);
					goto IL_037d;
				case 4:
					{
						awaiter = _2004_2008;
						_2004_2008 = default;
						num = (_2004_2001 = -1);
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
					awaiter = automaticHunting._2000(_2004_2006).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (_2004_2001 = 1);
						_2004_2008 = awaiter;
						_2004_2002.AwaitUnsafeOnCompleted(ref awaiter, ref this);
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
					awaiter = automaticHunting._2000(_2004_2006).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (_2004_2001 = 4);
						_2004_2008 = awaiter;
						_2004_2002.AwaitUnsafeOnCompleted(ref awaiter, ref this);
						return;
					}
					break;
					IL_0301:
					result = awaiter2.GetResult();
					automaticHunting._2004("/vnav stop");
					if (!result)
					{
						automaticHunting.PrintEcho("寻路开怪: 未到达S怪坐标(失败/超时)，原地下坐骑开怪");
					}
					awaiter2 = automaticHunting._1680(_2004_2006).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						num = (_2004_2001 = 3);
						_2004_2007 = awaiter2;
						_2004_2002.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
						return;
					}
					goto IL_037d;
				}
				awaiter.GetResult();
				end_IL_000e:;
			}
			catch (Exception exception)
			{
				_2004_2001 = -2;
				_2004_2002.SetException(exception);
				return;
			}
			_2004_2001 = -2;
			_2004_2002.SetResult();
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0();
		}

		[DebuggerHidden]
		private void _00A0(IAsyncStateMachine P_0)
		{
			_2004_2002.SetStateMachine(P_0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine P_0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0(P_0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	private struct _2057 : IAsyncStateMachine
	{
		public int _2004_2009;

		public AsyncTaskMethodBuilder _2004_200A;

		public AutomaticHunting _2004_200B;

		public CancellationToken _2004_2010;

		private _2014 _2004_2011;

		private Vector3 _2004_2012;

		private string _2004_2013;

		private TaskAwaiter _2004_2014;

		private TaskAwaiter<List<(uint Id, Vector3 Position, string Name)>> _2004_2015;

		private void _00A0()
		{
			int num = _2004_2009;
			AutomaticHunting automaticHunting = _2004_200B;
			try
			{
				TaskAwaiter<List<(uint, Vector3, string)>> awaiter2;
				TaskAwaiter awaiter;
				List<(uint, Vector3, string)> result;
				switch (num)
				{
				default:
				{
					_2004_2011 = new _2014();
					object obj = automaticHunting.m__2052;
					bool lockTaken = false;
					uint num2;
					uint num3;
					Vector3 vector;
					try
					{
						Monitor.Enter(obj, ref lockTaken);
						num2 = automaticHunting.m__205A;
						num3 = automaticHunting.m__205B;
						vector = automaticHunting.m__205C;
					}
					finally
					{
						if (num < 0 && lockTaken)
						{
							Monitor.Exit(obj);
						}
					}
					if (num2 != 0 && num3 != 0)
					{
						awaiter = automaticHunting._00A0(num2, num3, vector, _2004_2010).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_2004_2009 = 0);
							_2004_2014 = awaiter;
							_2004_200A.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_00ce;
					}
					automaticHunting.PrintEcho("开始处理目标Flag");
					obj = automaticHunting.m__2052;
					lockTaken = false;
					try
					{
						Monitor.Enter(obj, ref lockTaken);
						_2004_2013 = automaticHunting.m__2059;
						_2004_2011._2002_2063 = automaticHunting.m__2057;
						_2004_2012 = automaticHunting.m__2058;
					}
					finally
					{
						if (num < 0 && lockTaken)
						{
							Monitor.Exit(obj);
						}
					}
					if (_2004_2011._2002_2063 != 0)
					{
						automaticHunting.LastFlagPosition = _2004_2012;
						automaticHunting.m__2022 = _2004_2011._2002_2063;
						awaiter2 = Framework.RunOnFrameworkThread(() => _2002(_2004_2011._2002_2063)).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (_2004_2009 = 1);
							_2004_2015 = awaiter2;
							_2004_200A.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_01df;
					}
					automaticHunting.PrintEcho("跨服完成但无目标Flag，跳过");
					goto end_IL_000e;
				}
				case 0:
					awaiter = _2004_2014;
					_2004_2014 = default;
					num = (_2004_2009 = -1);
					goto IL_00ce;
				case 1:
					awaiter2 = _2004_2015;
					_2004_2015 = default;
					num = (_2004_2009 = -1);
					goto IL_01df;
				case 2:
					{
						awaiter = _2004_2014;
						_2004_2014 = default;
						num = (_2004_2009 = -1);
						break;
					}
					IL_01df:
					result = awaiter2.GetResult();
					automaticHunting.m__2025 = result;
					awaiter = automaticHunting._00A0(_2004_2011._2002_2063, _2004_2012, _2004_2013).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (_2004_2009 = 2);
						_2004_2014 = awaiter;
						_2004_200A.AwaitUnsafeOnCompleted(ref awaiter, ref this);
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
				_2004_2009 = -2;
				_2004_2011 = null;
				_2004_2013 = null;
				_2004_200A.SetException(exception);
				return;
			}
			_2004_2009 = -2;
			_2004_2011 = null;
			_2004_2013 = null;
			_2004_200A.SetResult();
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0();
		}

		[DebuggerHidden]
		private void _00A0(IAsyncStateMachine P_0)
		{
			_2004_200A.SetStateMachine(P_0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine P_0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0(P_0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	private struct _2058 : IAsyncStateMachine
	{
		public int _2004_2022;

		public AsyncTaskMethodBuilder<string> _2004_2024;

		private TaskAwaiter<string> _2004_2025;

		private void _00A0()
		{
			int num = _2004_2022;
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
						num = (_2004_2022 = 0);
						_2004_2025 = awaiter;
						_2004_2024.AwaitUnsafeOnCompleted(ref awaiter, ref this);
						return;
					}
				}
				else
				{
					awaiter = _2004_2025;
					_2004_2025 = default;
					num = (_2004_2022 = -1);
				}
				result = awaiter.GetResult();
			}
			catch (Exception exception)
			{
				_2004_2022 = -2;
				_2004_2024.SetException(exception);
				return;
			}
			_2004_2022 = -2;
			_2004_2024.SetResult(result);
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0();
		}

		[DebuggerHidden]
		private void _00A0(IAsyncStateMachine P_0)
		{
			_2004_2024.SetStateMachine(P_0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine P_0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0(P_0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	private struct _2059 : IAsyncStateMachine
	{
		public int _2004_2027;

		public AsyncTaskMethodBuilder<uint> _2004_2028;

		private TaskAwaiter<uint> _2004_2029;

		private void _00A0()
		{
			int num = _2004_2027;
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
						num = (_2004_2027 = 0);
						_2004_2029 = awaiter;
						_2004_2028.AwaitUnsafeOnCompleted(ref awaiter, ref this);
						return;
					}
				}
				else
				{
					awaiter = _2004_2029;
					_2004_2029 = default;
					num = (_2004_2027 = -1);
				}
				result = awaiter.GetResult();
			}
			catch (Exception exception)
			{
				_2004_2027 = -2;
				_2004_2028.SetException(exception);
				return;
			}
			_2004_2027 = -2;
			_2004_2028.SetResult(result);
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0();
		}

		[DebuggerHidden]
		private void _00A0(IAsyncStateMachine P_0)
		{
			_2004_2028.SetStateMachine(P_0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine P_0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0(P_0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	private struct _205A : IAsyncStateMachine
	{
		public int _2004_202A;

		public AsyncTaskMethodBuilder<Vector3?> _2004_202B;

		public AutomaticHunting _2004_202C;

		public Vector3 _2004_202D;

		public bool _2004_202E;

		private TaskAwaiter<Vector3?> _2004_202F;

		private void _00A0()
		{
			int num = _2004_202A;
			Vector3? result;
			try
			{
				TaskAwaiter<Vector3?> awaiter;
				if (num != 0)
				{
					AutomaticHunting automaticHunting = _2004_202C;
					Vector3 vector = _2004_202D;
					bool flag = _2004_202E;
					awaiter = Framework.RunOnFrameworkThread(() =>
					{
						if (!automaticHunting._00A0(vector, flag, out var vector2))
						{
							return (Vector3?)null;
						}
						Vector3 vector3 = vector2 + automaticHunting._00A0(vector2);
						Vector3 arg = automaticHunting._1680(vector3) ?? vector3;
						ICallGateSubscriber<bool>? callGateSubscriber = automaticHunting.m__2012;
						if (callGateSubscriber != null && callGateSubscriber.InvokeFunc())
						{
							ICallGateSubscriber<Vector3, bool, bool>? callGateSubscriber2 = automaticHunting.m__2013;
							if (callGateSubscriber2 != null && callGateSubscriber2.InvokeFunc(arg, arg2: true))
							{
								return vector3;
							}
						}
						return (Vector3?)null;
					}).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (_2004_202A = 0);
						_2004_202F = awaiter;
						_2004_202B.AwaitUnsafeOnCompleted(ref awaiter, ref this);
						return;
					}
				}
				else
				{
					awaiter = _2004_202F;
					_2004_202F = default;
					num = (_2004_202A = -1);
				}
				result = awaiter.GetResult();
			}
			catch (Exception exception)
			{
				_2004_202A = -2;
				_2004_202B.SetException(exception);
				return;
			}
			_2004_202A = -2;
			_2004_202B.SetResult(result);
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0();
		}

		[DebuggerHidden]
		private void _00A0(IAsyncStateMachine P_0)
		{
			_2004_202B.SetStateMachine(P_0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine P_0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0(P_0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	private struct _205B : IAsyncStateMachine
	{
		public int _2004_2032;

		public AsyncTaskMethodBuilder _2004_2035;

		public uint _2004_2033;

		public CancellationToken _2004_2036;

		public AutomaticHunting _2004_203E;

		public uint _2004_2047;

		private _2015 _2004_2048;

		public Vector3 _2004_2049;

		private TaskAwaiter<bool> _2004_204A;

		private TaskAwaiter<List<(uint Id, Vector3 Position, string Name)>> _2004_204B;

		private void _00A0()
		{
			int num = _2004_2032;
			AutomaticHunting automaticHunting = _2004_203E;
			try
			{
				if ((uint)num > 2u)
				{
					_2004_2048 = new _2015();
					_2004_2048._2002_2064 = _2004_2033;
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
						_2004_2036.ThrowIfCancellationRequested();
						automaticHunting.PrintEcho("跨服完成，传送到所选水晶...");
						awaiter2 = automaticHunting._00A0(_2004_2047, _2004_2036).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (_2004_2032 = 0);
							_2004_204A = awaiter2;
							_2004_2035.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_00b7;
					case 0:
						awaiter2 = _2004_204A;
						_2004_204A = default;
						num = (_2004_2032 = -1);
						goto IL_00b7;
					case 1:
						awaiter2 = _2004_204A;
						_2004_204A = default;
						num = (_2004_2032 = -1);
						goto IL_0127;
					case 2:
						{
							awaiter = _2004_204B;
							_2004_204B = default;
							num = (_2004_2032 = -1);
							goto IL_01c3;
						}
						IL_0127:
						result = awaiter2.GetResult();
						goto IL_012f;
						IL_00b7:
						result = awaiter2.GetResult();
						if (result)
						{
							awaiter2 = automaticHunting._2000(_2004_2048._2002_2064, _2004_2036).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								num = (_2004_2032 = 1);
								_2004_204A = awaiter2;
								_2004_2035.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
								return;
							}
							goto IL_0127;
						}
						goto IL_012f;
						IL_012f:
						if (result)
						{
							automaticHunting.LastFlagPosition = _2004_2049;
							automaticHunting.m__2022 = _2004_2048._2002_2064;
							awaiter = Framework.RunOnFrameworkThread(() => _2002(_2004_2048._2002_2064)).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (_2004_2032 = 2);
								_2004_204B = awaiter;
								_2004_2035.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
							goto IL_01c3;
						}
						automaticHunting.PrintEcho("传送到所选水晶失败");
						break;
						IL_01c3:
						result2 = awaiter.GetResult();
						automaticHunting.m__2025 = result2;
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
						object obj = automaticHunting.m__2052;
						bool result = false;
						try
						{
							Monitor.Enter(obj, ref result);
							automaticHunting.m__205A = 0u;
							automaticHunting.m__205B = 0u;
							automaticHunting.m__205C = default;
						}
						finally
						{
							if (num < 0 && result)
							{
								Monitor.Exit(obj);
							}
						}
					}
				}
			}
			catch (Exception exception)
			{
				_2004_2032 = -2;
				_2004_2048 = null;
				_2004_2035.SetException(exception);
				return;
			}
			_2004_2032 = -2;
			_2004_2048 = null;
			_2004_2035.SetResult();
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0();
		}

		[DebuggerHidden]
		private void _00A0(IAsyncStateMachine P_0)
		{
			_2004_2035.SetStateMachine(P_0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine P_0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0(P_0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	private struct _205C : IAsyncStateMachine
	{
		public int _2004_204C;

		public AsyncTaskMethodBuilder _2004_204D;

		public AutomaticHunting _2004_204E;

		public CancellationToken _2004_204F;

		public Vector3 _2004_2050;

		public CancellationTokenSource _2004_2051;

		private bool _2004_2052;

		private int _2004_2053;

		private float _2004_2054;

		private TaskAwaiter _2004_2055;

		private TaskAwaiter<Vector3?> _2004_2056;

		private TaskAwaiter<_1680?> _2004_2057;

		private TaskAwaiter<_2000?> _2004_2058;

		private void _00A0()
		{
			int num = _2004_204C;
			AutomaticHunting automaticHunting = _2004_204E;
			try
			{
				TaskAwaiter<Vector3?> awaiter4;
				TaskAwaiter<_1680?> awaiter3;
				TaskAwaiter<_2000?> awaiter2;
				TaskAwaiter awaiter;
				_2000? result;
				_1680? result2;
				Vector3? result3;
				switch (num)
				{
				default:
					_2004_2052 = automaticHunting.Config.LandingMode;
					_2004_2053 = 0;
					goto IL_0442;
				case 0:
					awaiter = _2004_2055;
					_2004_2055 = default;
					num = (_2004_204C = -1);
					goto IL_00aa;
				case 1:
					awaiter4 = _2004_2056;
					_2004_2056 = default;
					num = (_2004_204C = -1);
					goto IL_0127;
				case 2:
					awaiter3 = _2004_2057;
					_2004_2057 = default;
					num = (_2004_204C = -1);
					goto IL_01ca;
				case 3:
					awaiter = _2004_2055;
					_2004_2055 = default;
					num = (_2004_204C = -1);
					goto IL_0287;
				case 4:
					awaiter2 = _2004_2058;
					_2004_2058 = default;
					num = (_2004_204C = -1);
					goto IL_030a;
				case 5:
					{
						awaiter = _2004_2055;
						_2004_2055 = default;
						num = (_2004_204C = -1);
						goto IL_0402;
					}
					IL_0442:
					if (_2004_2053 < 240)
					{
						awaiter = Task.Delay(500, _2004_204F).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_2004_204C = 0);
							_2004_2055 = awaiter;
							_2004_204D.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_00aa;
					}
					automaticHunting._2004("/vnav stop");
					automaticHunting.PrintEcho("到达Flag附近未发现A怪");
					break;
					IL_0287:
					awaiter.GetResult();
					break;
					IL_030a:
					result = awaiter2.GetResult();
					if (result.HasValue)
					{
						automaticHunting._2004("/vnav stop");
						automaticHunting.PrintEcho($"未发现A怪，发现S怪: {result.Value.Name} 血量{result.Value.HpPercent:0.0}%，开始悬停监控");
						awaiter = automaticHunting._00A0(result.Value, _2004_2051, _2004_204F).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_2004_204C = 5);
							_2004_2055 = awaiter;
							_2004_204D.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_0402;
					}
					goto IL_040b;
					IL_0430:
					_2004_2053++;
					goto IL_0442;
					IL_00aa:
					awaiter.GetResult();
					awaiter4 = Framework.RunOnFrameworkThread((Func<Vector3?>)GetLocalPlayerPosition).GetAwaiter();
					if (!awaiter4.IsCompleted)
					{
						num = (_2004_204C = 1);
						_2004_2056 = awaiter4;
						_2004_204D.AwaitUnsafeOnCompleted(ref awaiter4, ref this);
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
						automaticHunting._2004("/vnav stop");
						automaticHunting.PrintEcho("发现A怪: " + result2.Value.Name + "，切换到真实坐标");
						awaiter = automaticHunting._00A0(result2.Value.Position, _2004_2052, _2004_2051, _2004_204F).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_2004_204C = 3);
							_2004_2055 = awaiter;
							_2004_204D.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_0287;
					}
					if (automaticHunting.Config.SEnabled)
					{
						awaiter2 = Framework.RunOnFrameworkThread((Func<_2000?>)automaticHunting._2048).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (_2004_204C = 4);
							_2004_2058 = awaiter2;
							_2004_204D.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_030a;
					}
					goto IL_040b;
					IL_040b:
					if (_2004_2054 < 50f)
					{
						automaticHunting._2004("/vnav stop");
						automaticHunting.PrintEcho("到达Flag附近未发现A怪");
						break;
					}
					goto IL_0430;
					IL_0127:
					result3 = awaiter4.GetResult();
					if (result3.HasValue)
					{
						_2004_2054 = AutomaticHunting._00A0(result3.Value, _2004_2050);
						if (!(_2004_2054 >= 100f))
						{
							awaiter3 = Framework.RunOnFrameworkThread((Func<_1680?>)automaticHunting._00A0).GetAwaiter();
							if (!awaiter3.IsCompleted)
							{
								num = (_2004_204C = 2);
								_2004_2057 = awaiter3;
								_2004_204D.AwaitUnsafeOnCompleted(ref awaiter3, ref this);
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
				_2004_204C = -2;
				_2004_204D.SetException(exception);
				return;
			}
			_2004_204C = -2;
			_2004_204D.SetResult();
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0();
		}

		[DebuggerHidden]
		private void _00A0(IAsyncStateMachine P_0)
		{
			_2004_204D.SetStateMachine(P_0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine P_0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0(P_0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	private struct _205D : IAsyncStateMachine
	{
		public int _2004_2059;

		public AsyncTaskMethodBuilder _2004_205A;

		public AutomaticHunting _2004_205B;

		private float _2004_205C;

		private DateTime _2004_205D;

		private TaskAwaiter _2004_205E;

		private void _00A0()
		{
			int num = _2004_2059;
			AutomaticHunting automaticHunting = _2004_205B;
			try
			{
				_ = 1;
				try
				{
					TaskAwaiter awaiter;
					if (num == 0)
					{
						awaiter = _2004_205E;
						_2004_205E = default;
						num = (_2004_2059 = -1);
						goto IL_0134;
					}
					if (num == 1)
					{
						awaiter = _2004_205E;
						_2004_205E = default;
						num = (_2004_2059 = -1);
						goto IL_036d;
					}
					IGameObject target = TargetManager.Target;
					if (target != null && target is IBattleChara)
					{
						automaticHunting.BeginSMonitor(target.Name.TextValue, "血量监控", AutomaticHunting._00A0(string.Empty, out float num2) ? num2 : (-1f), automaticHunting.Config.SHpThreshold);
						automaticHunting.PrintEcho($"开始监控当前目标血量(阈值{automaticHunting.Config.SHpThreshold:0}%)...");
						_2004_205C = -1f;
						_2004_205D = DateTime.UtcNow;
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
								if (AutomaticHunting._00A0(string.Empty, out float num3))
								{
									SMonitorStatus sMonitor2 = automaticHunting.SMonitor;
									if (sMonitor2 != null)
									{
										sMonitor2.Phase = "血量监控";
										sMonitor2.HpPercent = num3;
										sMonitor2.WaitedSeconds = (float)(DateTime.UtcNow - _2004_205D).TotalSeconds;
									}
									if (Math.Abs(num3 - _2004_205C) >= 5f)
									{
										_2004_205C = num3;
										automaticHunting.PrintEcho($"监控中: 当前目标血量 {num3:0.0}%");
									}
									if (num3 <= automaticHunting.Config.SHpThreshold)
									{
										_2000 obj = new _2000(target2.Name.TextValue, target2.Position, num3);
										automaticHunting.PrintEcho($"血量{num3:0.0}%≤{automaticHunting.Config.SHpThreshold:0}%，开始开怪(与S怪流程一致): {automaticHunting.Config.SDashCommand}");
										awaiter = automaticHunting._00A0(obj, num3, CancellationToken.None).GetAwaiter();
										if (!awaiter.IsCompleted)
										{
											num = (_2004_2059 = 1);
											_2004_205E = awaiter;
											_2004_205A.AwaitUnsafeOnCompleted(ref awaiter, ref this);
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
						num = (_2004_2059 = 0);
						_2004_205E = awaiter;
						_2004_205A.AwaitUnsafeOnCompleted(ref awaiter, ref this);
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
				_2004_2059 = -2;
				_2004_205A.SetException(exception2);
				return;
			}
			_2004_2059 = -2;
			_2004_205A.SetResult();
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0();
		}

		[DebuggerHidden]
		private void _00A0(IAsyncStateMachine P_0)
		{
			_2004_205A.SetStateMachine(P_0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine P_0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0(P_0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	private struct _205E : IAsyncStateMachine
	{
		public int _2004_205F;

		public AsyncTaskMethodBuilder _2004_2060;

		public AutomaticHunting _2004_2061;

		public CancellationTokenSource _2004_2062;

		public Vector3 _2004_2063;

		private _204D _2004_2064;

		public CancellationToken _2004_206A;

		public bool _2004_206B;

		private bool _2004_206C;

		private int _2004_206D;

		private TaskAwaiter<bool> _2004_206E;

		private TaskAwaiter _2004_206F;

		private TaskAwaiter<Vector3?> _2004_3000;

		private TaskAwaiter<string> _2005_00A0;

		private void _00A0()
		{
			int num = _2004_205F;
			AutomaticHunting automaticHunting = _2004_2061;
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
					_2004_2064 = new _204D();
					_2004_2064._2003_2053 = _2004_2061;
					automaticHunting._00A0(AutomaticHunting._2003._2002_2022, _2004_2062);
					_2004_2064._2003_2054 = _2004_2063 + automaticHunting._00A0(_2004_2063);
					_2004_206C = false;
					_2004_206D = 1;
					goto IL_01bc;
				case 0:
					awaiter = _2004_206E;
					_2004_206E = default;
					num = (_2004_205F = -1);
					goto IL_0112;
				case 1:
					awaiter4 = _2004_206F;
					_2004_206F = default;
					num = (_2004_205F = -1);
					goto IL_01a3;
				case 2:
					awaiter4 = _2004_206F;
					_2004_206F = default;
					num = (_2004_205F = -1);
					goto IL_0258;
				case 3:
					awaiter3 = _2004_3000;
					_2004_3000 = default;
					num = (_2004_205F = -1);
					goto IL_02d5;
				case 4:
					awaiter2 = _2005_00A0;
					_2005_00A0 = default;
					num = (_2004_205F = -1);
					goto IL_037d;
				case 5:
					{
						awaiter = _2004_206E;
						_2004_206E = default;
						num = (_2004_205F = -1);
						goto IL_0410;
					}
					IL_01bc:
					if (_2004_206D <= 2)
					{
						awaiter = Framework.RunOnFrameworkThread(() =>
						{
							Vector3 valueOrDefault = _2004_2064._2003_2053._1680(_2004_2064._2003_2054).GetValueOrDefault(_2004_2064._2003_2054);
							ICallGateSubscriber<bool>? callGateSubscriber = _2004_2064._2003_2053.m__2012;
							return callGateSubscriber != null && callGateSubscriber.InvokeFunc() && (_2004_2064._2003_2053.m__2013?.InvokeFunc(valueOrDefault, arg2: true) ?? false);
						}).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_2004_205F = 0);
							_2004_206E = awaiter;
							_2004_2060.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_0112;
					}
					goto IL_01c8;
					IL_01aa:
					_2004_206D++;
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
					if (!_2004_206C)
					{
						automaticHunting.PrintEcho("A怪导航失败: vnavmesh未就绪或无法规划路径");
						break;
					}
					automaticHunting.PrintEcho("导航已开始(前往A怪落点)");
					_2004_206D = 0;
					goto IL_0452;
					IL_0258:
					awaiter4.GetResult();
					awaiter3 = Framework.RunOnFrameworkThread((Func<Vector3?>)GetLocalPlayerPosition).GetAwaiter();
					if (!awaiter3.IsCompleted)
					{
						num = (_2004_205F = 3);
						_2004_3000 = awaiter3;
						_2004_2060.AwaitUnsafeOnCompleted(ref awaiter3, ref this);
						return;
					}
					goto IL_02d5;
					IL_0112:
					result = awaiter.GetResult();
					_2004_206C = result;
					if (!_2004_206C)
					{
						if (_2004_206D < 2)
						{
							automaticHunting.PrintEcho("A怪寻路失败，2秒后重新寻路...");
							awaiter4 = Task.Delay(TimeSpan.FromSeconds(2L), _2004_206A).GetAwaiter();
							if (!awaiter4.IsCompleted)
							{
								num = (_2004_205F = 1);
								_2004_206F = awaiter4;
								_2004_2060.AwaitUnsafeOnCompleted(ref awaiter4, ref this);
								return;
							}
							goto IL_01a3;
						}
						goto IL_01aa;
					}
					goto IL_01c8;
					IL_0452:
					if (_2004_206D < 240)
					{
						awaiter4 = Task.Delay(500, _2004_206A).GetAwaiter();
						if (!awaiter4.IsCompleted)
						{
							num = (_2004_205F = 2);
							_2004_206F = awaiter4;
							_2004_2060.AwaitUnsafeOnCompleted(ref awaiter4, ref this);
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
					if (_2004_206B)
					{
						awaiter = automaticHunting._1680(_2004_206A).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_2004_205F = 5);
							_2004_206E = awaiter;
							_2004_2060.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_0410;
					}
					automaticHunting.PrintEcho("悬停在A怪上方，保持坐骑");
					break;
					IL_02d5:
					result3 = awaiter3.GetResult();
					if (result3.HasValue && !(AutomaticHunting._00A0(result3.Value, _2004_2064._2003_2054) >= 3f))
					{
						automaticHunting._2004("/vnav stop");
						awaiter2 = Framework.RunOnFrameworkThread((Func<string>)automaticHunting._1680).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (_2004_205F = 4);
							_2005_00A0 = awaiter2;
							_2004_2060.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_037d;
					}
					_2004_206D++;
					goto IL_0452;
					IL_01a3:
					awaiter4.GetResult();
					goto IL_01aa;
				}
			}
			catch (Exception exception)
			{
				_2004_205F = -2;
				_2004_2064 = null;
				_2004_2060.SetException(exception);
				return;
			}
			_2004_205F = -2;
			_2004_2064 = null;
			_2004_2060.SetResult();
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0();
		}

		[DebuggerHidden]
		private void _00A0(IAsyncStateMachine P_0)
		{
			_2004_2060.SetStateMachine(P_0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine P_0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0(P_0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	private struct _205F : IAsyncStateMachine
	{
		public int _2005_1680;

		public AsyncTaskMethodBuilder _2005_2000;

		public AutomaticHunting _2005_2001;

		public _00A0 _2005_2002;

		public CancellationTokenSource _2005_2003;

		private _2032 _2005_2004;

		public CancellationToken _2005_2005;

		private bool _2005_2006;

		private int _2005_2007;

		private bool _2005_2008;

		private bool _2005_2009;

		private bool _2005_200A;

		private int _2005_200B;

		private int _2005_2010;

		private TaskAwaiter<bool> _2005_2011;

		private TaskAwaiter _2005_2012;

		private float _2005_2013;

		private TaskAwaiter<Vector3?> _2005_2014;

		private TaskAwaiter<_1680?> _2005_2015;

		private TaskAwaiter<_2000?> _2005_2022;

		private TaskAwaiter<string> _2005_2024;

		private void _00A0()
		{
			int num = _2005_1680;
			AutomaticHunting automaticHunting = _2005_2001;
			try
			{
				TaskAwaiter<Vector3?> awaiter6;
				TaskAwaiter<_1680?> awaiter5;
				TaskAwaiter<_2000?> awaiter4;
				TaskAwaiter awaiter3;
				TaskAwaiter<string> awaiter2;
				TaskAwaiter<bool> awaiter;
				string result;
				_1680? result2;
				bool result3;
				_2000? result4;
				Vector3? result5;
				switch (num)
				{
				default:
					_2005_2004 = new _2032();
					_2005_2004._2003_2024 = _2005_2001;
					_2005_2004._2003_2027 = _2005_2002;
					_2005_2006 = automaticHunting.Config.LandingMode;
					_2005_2004._2003_2025 = _2005_2004._2003_2027.Position;
					_2005_2007 = 0;
					goto IL_09e2;
				case 0:
					awaiter = _2005_2011;
					_2005_2011 = default;
					num = (_2005_1680 = -1);
					goto IL_0176;
				case 1:
					awaiter = _2005_2011;
					_2005_2011 = default;
					num = (_2005_1680 = -1);
					goto IL_01e4;
				case 2:
					awaiter = _2005_2011;
					_2005_2011 = default;
					num = (_2005_1680 = -1);
					goto IL_0285;
				case 3:
					awaiter3 = _2005_2012;
					_2005_2012 = default;
					num = (_2005_1680 = -1);
					goto IL_02fa;
				case 4:
					awaiter3 = _2005_2012;
					_2005_2012 = default;
					num = (_2005_1680 = -1);
					goto IL_03a0;
				case 5:
					awaiter3 = _2005_2012;
					_2005_2012 = default;
					num = (_2005_1680 = -1);
					goto IL_0472;
				case 6:
					awaiter6 = _2005_2014;
					_2005_2014 = default;
					num = (_2005_1680 = -1);
					goto IL_04ef;
				case 7:
					awaiter5 = _2005_2015;
					_2005_2015 = default;
					num = (_2005_1680 = -1);
					goto IL_0609;
				case 8:
					awaiter4 = _2005_2022;
					_2005_2022 = default;
					num = (_2005_1680 = -1);
					goto IL_0704;
				case 9:
					awaiter3 = _2005_2012;
					_2005_2012 = default;
					num = (_2005_1680 = -1);
					goto IL_07fd;
				case 10:
					awaiter2 = _2005_2024;
					_2005_2024 = default;
					num = (_2005_1680 = -1);
					goto IL_08bf;
				case 11:
					{
						awaiter = _2005_2011;
						_2005_2011 = default;
						num = (_2005_1680 = -1);
						goto IL_0953;
					}
					IL_09e2:
					if (_2005_2007 < 2)
					{
						_2005_2008 = _2005_2007 == 1;
						automaticHunting._00A0(_2005_2008 ? AutomaticHunting._2003._2002_2022 : AutomaticHunting._2003._2002_2015, _2005_2003);
						_2005_2009 = false;
						_2005_2010 = 1;
						goto IL_03b9;
					}
					automaticHunting._2004("/vnav stop");
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
					if (_2005_2006)
					{
						awaiter = automaticHunting._1680(_2005_2005).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_2005_1680 = 11);
							_2005_2011 = awaiter;
							_2005_2000.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_0953;
					}
					automaticHunting.PrintEcho("悬停在A怪上方，保持坐骑");
					break;
					IL_02fa:
					awaiter3.GetResult();
					automaticHunting._2004("/vnav flyflag");
					automaticHunting.PrintEcho("开始导航: /vnav flyflag");
					_2005_2009 = true;
					goto IL_031e;
					IL_031e:
					if (!_2005_2009)
					{
						if (_2005_2010 < 2)
						{
							automaticHunting.PrintEcho("寻路失败，2秒后重新寻路...");
							awaiter3 = Task.Delay(TimeSpan.FromSeconds(2L), _2005_2005).GetAwaiter();
							if (!awaiter3.IsCompleted)
							{
								num = (_2005_1680 = 4);
								_2005_2012 = awaiter3;
								_2005_2000.AwaitUnsafeOnCompleted(ref awaiter3, ref this);
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
						num = (_2005_1680 = 6);
						_2005_2014 = awaiter6;
						_2005_2000.AwaitUnsafeOnCompleted(ref awaiter6, ref this);
						return;
					}
					goto IL_04ef;
					IL_03b9:
					if (_2005_2010 <= 2)
					{
						if (_2005_2008)
						{
							awaiter = Framework.RunOnFrameworkThread(() =>
							{
								Vector3 valueOrDefault = _2005_2004._2003_2024._1680(_2005_2004._2003_2025).GetValueOrDefault(_2005_2004._2003_2025);
								ICallGateSubscriber<bool>? callGateSubscriber = _2005_2004._2003_2024.m__2012;
								return callGateSubscriber != null && callGateSubscriber.InvokeFunc() && (_2005_2004._2003_2024.m__2013?.InvokeFunc(valueOrDefault, arg2: true) ?? false);
							}).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (_2005_1680 = 0);
								_2005_2011 = awaiter;
								_2005_2000.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
							goto IL_0176;
						}
						awaiter = automaticHunting._2001(_2005_2005).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_2005_1680 = 1);
							_2005_2011 = awaiter;
							_2005_2000.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_01e4;
					}
					goto IL_03c5;
					IL_0609:
					result2 = awaiter5.GetResult();
					if (result2.HasValue)
					{
						automaticHunting._2004("/vnav stop");
						_2005_2004._2003_2025 = result2.Value.Position + automaticHunting._00A0(result2.Value.Position);
						_2005_200A = true;
						automaticHunting.PrintEcho("发现A怪: " + result2.Value.Name + "，切换到落点(偏移后)");
						goto IL_09a8;
					}
					if (automaticHunting.Config.SEnabled)
					{
						awaiter4 = Framework.RunOnFrameworkThread((Func<_2000?>)automaticHunting._2048).GetAwaiter();
						if (!awaiter4.IsCompleted)
						{
							num = (_2005_1680 = 8);
							_2005_2022 = awaiter4;
							_2005_2000.AwaitUnsafeOnCompleted(ref awaiter4, ref this);
							return;
						}
						goto IL_0704;
					}
					goto IL_0809;
					IL_07fd:
					awaiter3.GetResult();
					break;
					IL_03a7:
					_2005_2010++;
					goto IL_03b9;
					IL_03a0:
					awaiter3.GetResult();
					goto IL_03a7;
					IL_03c5:
					if (!_2005_2009)
					{
						automaticHunting.PrintEcho("导航失败: vnavmesh未就绪或无法规划路径");
						break;
					}
					automaticHunting.PrintEcho(_2005_2008 ? "导航已开始(前往A怪真实坐标)" : "导航已开始(前往标点)");
					_2005_200A = false;
					_2005_200B = 0;
					_2005_2010 = 0;
					goto IL_0998;
					IL_0176:
					result3 = awaiter.GetResult();
					_2005_2009 = result3;
					goto IL_031e;
					IL_0704:
					result4 = awaiter4.GetResult();
					if (result4.HasValue)
					{
						automaticHunting._2004("/vnav stop");
						automaticHunting.PrintEcho($"未发现A怪，发现S怪: {result4.Value.Name} 血量{result4.Value.HpPercent:0.0}%，开始悬停监控");
						awaiter3 = automaticHunting._00A0(result4.Value, _2005_2003, _2005_2005).GetAwaiter();
						if (!awaiter3.IsCompleted)
						{
							num = (_2005_1680 = 9);
							_2005_2012 = awaiter3;
							_2005_2000.AwaitUnsafeOnCompleted(ref awaiter3, ref this);
							return;
						}
						goto IL_07fd;
					}
					goto IL_0809;
					IL_04ef:
					result5 = awaiter6.GetResult();
					if (result5.HasValue)
					{
						_2005_2013 = AutomaticHunting._00A0(result5.Value, _2005_2004._2003_2025);
						if (!_2005_2008 && _2005_2010 - _2005_200B >= 60)
						{
							_2005_200B = _2005_2010;
							automaticHunting.PrintEcho($"飞向标点中(距标点{_2005_2013:0.0}m)");
						}
						if (!_2005_2008 && _2005_2013 < 100f)
						{
							awaiter5 = Framework.RunOnFrameworkThread((Func<_1680?>)automaticHunting._00A0).GetAwaiter();
							if (!awaiter5.IsCompleted)
							{
								num = (_2005_1680 = 7);
								_2005_2015 = awaiter5;
								_2005_2000.AwaitUnsafeOnCompleted(ref awaiter5, ref this);
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
					awaiter = Framework.RunOnFrameworkThread(() => !_2005_2004._2003_2024.Config.SetFlagOnNavigation || _1680(_2005_2004._2003_2027.TerritoryType, 0u, _2005_2004._2003_2027.Position)).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (_2005_1680 = 2);
						_2005_2011 = awaiter;
						_2005_2000.AwaitUnsafeOnCompleted(ref awaiter, ref this);
						return;
					}
					goto IL_0285;
					IL_09a8:
					if (!_2005_2008 && !_2005_200A)
					{
						automaticHunting._2004("/vnav stop");
						automaticHunting.PrintEcho("120秒未接近标点，导航结束");
						break;
					}
					_2005_2007++;
					goto IL_09e2;
					IL_0809:
					if (_2005_2013 < 50f)
					{
						automaticHunting._2004("/vnav stop");
						automaticHunting.PrintEcho("到达标点附近未发现A怪");
						break;
					}
					goto IL_0831;
					IL_0831:
					if (_2005_2008 && _2005_2013 < 3f)
					{
						automaticHunting._2004("/vnav stop");
						awaiter2 = Framework.RunOnFrameworkThread((Func<string>)automaticHunting._1680).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (_2005_1680 = 10);
							_2005_2024 = awaiter2;
							_2005_2000.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_08bf;
					}
					goto IL_0986;
					IL_0986:
					_2005_2010++;
					goto IL_0998;
					IL_0998:
					if (_2005_2010 < 240)
					{
						awaiter3 = Task.Delay(500, _2005_2005).GetAwaiter();
						if (!awaiter3.IsCompleted)
						{
							num = (_2005_1680 = 5);
							_2005_2012 = awaiter3;
							_2005_2000.AwaitUnsafeOnCompleted(ref awaiter3, ref this);
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
					awaiter3 = Task.Delay(150, _2005_2005).GetAwaiter();
					if (!awaiter3.IsCompleted)
					{
						num = (_2005_1680 = 3);
						_2005_2012 = awaiter3;
						_2005_2000.AwaitUnsafeOnCompleted(ref awaiter3, ref this);
						return;
					}
					goto IL_02fa;
				}
			}
			catch (Exception exception)
			{
				_2005_1680 = -2;
				_2005_2004 = null;
				_2005_2000.SetException(exception);
				return;
			}
			_2005_1680 = -2;
			_2005_2004 = null;
			_2005_2000.SetResult();
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0();
		}

		[DebuggerHidden]
		private void _00A0(IAsyncStateMachine P_0)
		{
			_2005_2000.SetStateMachine(P_0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine P_0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0(P_0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	private struct _2060 : IAsyncStateMachine
	{
		public int _2005_2025;

		public AsyncTaskMethodBuilder<bool> _2005_2027;

		public AutomaticHunting _2005_2028;

		public _2000 _2005_2029;

		private _203E _2005_202A;

		public CancellationToken _2005_202B;

		private int _2005_202C;

		private Vector3 _2005_202D;

		private DateTime _2005_202E;

		private DateTime _2005_202F;

		private DateTime _2005_2032;

		private float _2005_2035;

		private TaskAwaiter<Vector3?> _2005_2033;

		private TaskAwaiter _2005_2036;

		private float _2005_203E;

		private TaskAwaiter<(Vector3? player, Vector3? mob)> _2005_2047;

		private void _00A0()
		{
			int num = _2005_2025;
			AutomaticHunting automaticHunting = _2005_2028;
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
					_2005_202A = new _203E();
					_2005_202A._2003_2035 = _2005_2028;
					_2005_202A._2003_2033 = _2005_2029.Position;
					_2005_202C = 1;
					goto IL_051d;
				case 0:
					awaiter = _2005_2033;
					_2005_2033 = default;
					num = (_2005_2025 = -1);
					goto IL_00c9;
				case 1:
					awaiter3 = _2005_2036;
					_2005_2036 = default;
					num = (_2005_2025 = -1);
					goto IL_0157;
				case 2:
					awaiter3 = _2005_2036;
					_2005_2036 = default;
					num = (_2005_2025 = -1);
					goto IL_0203;
				case 3:
					awaiter2 = _2005_2047;
					_2005_2047 = default;
					num = (_2005_2025 = -1);
					goto IL_0295;
				case 4:
					{
						awaiter = _2005_2033;
						_2005_2033 = default;
						num = (_2005_2025 = -1);
						goto IL_043c;
					}
					IL_051d:
					if (_2005_202C <= 2)
					{
						awaiter = automaticHunting._00A0(_2005_202A._2003_2033, true).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_2005_2025 = 0);
							_2005_2033 = awaiter;
							_2005_2027.AwaitUnsafeOnCompleted(ref awaiter, ref this);
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
					_2005_202C++;
					goto IL_051d;
					IL_04d9:
					if ((DateTime.UtcNow - _2005_202E).TotalSeconds < 60.0)
					{
						awaiter3 = Task.Delay(500, _2005_202B).GetAwaiter();
						if (!awaiter3.IsCompleted)
						{
							num = (_2005_2025 = 2);
							_2005_2036 = awaiter3;
							_2005_2027.AwaitUnsafeOnCompleted(ref awaiter3, ref this);
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
						if (_2005_202C < 2)
						{
							automaticHunting.PrintEcho("寻路开怪: 寻路失败，2秒后重试...");
							awaiter3 = Task.Delay(TimeSpan.FromSeconds(2L), _2005_202B).GetAwaiter();
							if (!awaiter3.IsCompleted)
							{
								num = (_2005_2025 = 1);
								_2005_2036 = awaiter3;
								_2005_2027.AwaitUnsafeOnCompleted(ref awaiter3, ref this);
								return;
							}
							goto IL_0157;
						}
						goto IL_050b;
					}
					_2005_202D = result2.Value;
					_2005_202E = DateTime.UtcNow;
					_2005_202F = _2005_202E;
					_2005_2032 = DateTime.MinValue;
					_2005_2035 = 0f;
					goto IL_04d9;
					IL_0203:
					awaiter3.GetResult();
					awaiter2 = Framework.RunOnFrameworkThread(() =>
					{
						Vector3? localPlayerPosition = GetLocalPlayerPosition();
						Vector3? item = (_2005_202A._2003_2035._00A0(_2005_202A._2003_2033, false, out var value) ? new Vector3?(value) : ((Vector3?)null));
						return (player: localPlayerPosition, mob: item);
					}).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						num = (_2005_2025 = 3);
						_2005_2047 = awaiter2;
						_2005_2027.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
						return;
					}
					goto IL_0295;
					IL_0295:
					result3 = awaiter2.GetResult();
					if (result3.Item1.HasValue)
					{
						if (!result3.Item2.HasValue)
						{
							_2005_2035 += 0.5f;
							if (_2005_2035 >= 5f)
							{
								automaticHunting.PrintEcho($"寻路开怪: S怪已消失{_2005_2035:0}秒，中止");
								result = false;
								break;
							}
						}
						else
						{
							_2005_2035 = 0f;
							_2005_202A._2003_2033 = result3.Item2.Value;
							Vector3 vector = result3.Item2.Value + automaticHunting._00A0(result3.Item2.Value);
							if (AutomaticHunting._00A0(result3.Item1.Value, vector) <= 3f)
							{
								result = true;
								break;
							}
							_2005_203E = AutomaticHunting._00A0(vector, _2005_202D);
							if (!(_2005_203E <= 4f) && !((DateTime.UtcNow - _2005_202F).TotalSeconds < 1.5))
							{
								awaiter = automaticHunting._00A0(_2005_202A._2003_2033, true).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									num = (_2005_2025 = 4);
									_2005_2033 = awaiter;
									_2005_2027.AwaitUnsafeOnCompleted(ref awaiter, ref this);
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
						_2005_202D = result4.Value;
						_2005_202F = DateTime.UtcNow;
						if (!((DateTime.UtcNow - _2005_2032).TotalSeconds < 3.0))
						{
							_2005_2032 = DateTime.UtcNow;
							automaticHunting.PrintEcho($"寻路开怪: 目标已移动 {_2005_203E:0.0}m，重新寻路");
						}
					}
					goto IL_04d9;
				}
			}
			catch (Exception exception)
			{
				_2005_2025 = -2;
				_2005_202A = null;
				_2005_2027.SetException(exception);
				return;
			}
			_2005_2025 = -2;
			_2005_202A = null;
			_2005_2027.SetResult(result);
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0();
		}

		[DebuggerHidden]
		private void _00A0(IAsyncStateMachine P_0)
		{
			_2005_2027.SetStateMachine(P_0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine P_0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0(P_0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	private struct _2061 : IAsyncStateMachine
	{
		public int _2005_2048;

		public AsyncTaskMethodBuilder<bool> _2005_2049;

		public AutomaticHunting _2005_204A;

		public int _2005_204B;

		public TimeSpan _2005_204C;

		private DateTime _2005_204D;

		private TaskAwaiter _2005_204E;

		private TaskAwaiter<int> _2005_204F;

		private void _00A0()
		{
			int num = _2005_2048;
			AutomaticHunting CS_0024_003C_003E8__locals1 = _2005_204A;
			bool result;
			try
			{
				TaskAwaiter<int> awaiter;
				if (num != 0)
				{
					if (num != 1)
					{
						_2005_204D = DateTime.UtcNow;
						goto IL_0101;
					}
					awaiter = _2005_204F;
					_2005_204F = default;
					num = (_2005_2048 = -1);
					goto IL_00ee;
				}
				TaskAwaiter awaiter2 = _2005_204E;
				_2005_204E = default;
				num = (_2005_2048 = -1);
				goto IL_0080;
				IL_0101:
				if (DateTime.UtcNow - _2005_204D < _2005_204C)
				{
					awaiter2 = Task.Delay(500).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						num = (_2005_2048 = 0);
						_2005_204E = awaiter2;
						_2005_2049.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
						return;
					}
					goto IL_0080;
				}
				result = false;
				goto end_IL_000e;
				IL_00ee:
				if (awaiter.GetResult() != _2005_204B)
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
					num = (_2005_2048 = 1);
					_2005_204F = awaiter;
					_2005_2049.AwaitUnsafeOnCompleted(ref awaiter, ref this);
					return;
				}
				goto IL_00ee;
				end_IL_000e:;
			}
			catch (Exception exception)
			{
				_2005_2048 = -2;
				_2005_2049.SetException(exception);
				return;
			}
			_2005_2048 = -2;
			_2005_2049.SetResult(result);
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0();
		}

		[DebuggerHidden]
		private void _00A0(IAsyncStateMachine P_0)
		{
			_2005_2049.SetStateMachine(P_0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine P_0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0(P_0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	private struct _2062 : IAsyncStateMachine
	{
		public int _2005_2050;

		public AsyncTaskMethodBuilder _2005_2051;

		public uint _2005_2052;

		public Vector3 _2005_2053;

		public AutomaticHunting _2005_2054;

		public string _2005_2055;

		private CancellationTokenSource _2005_2056;

		private CancellationToken _2005_2057;

		private _00A0 _2005_2058;

		private TaskAwaiter<_2005> _2005_2059;

		private TaskAwaiter<bool> _2005_205A;

		private TaskAwaiter _2005_205B;

		private void _00A0()
		{
			int num = _2005_2050;
			AutomaticHunting automaticHunting = _2005_2054;
			try
			{
				if ((uint)num > 8u)
				{
					_2005_2056 = new CancellationTokenSource();
				}
				try
				{
					TaskAwaiter<_2005> awaiter3;
					TaskAwaiter<bool> awaiter2;
					TaskAwaiter awaiter;
					_2005 result;
					switch (num)
					{
					default:
						_2005_2057 = _2005_2056.Token;
						_2005_2058 = new _00A0(_2005_2052, _2005_2053);
						awaiter3 = automaticHunting._00A0(_2005_2058, _2005_2057).GetAwaiter();
						if (!awaiter3.IsCompleted)
						{
							num = (_2005_2050 = 0);
							_2005_2059 = awaiter3;
							_2005_2051.AwaitUnsafeOnCompleted(ref awaiter3, ref this);
							return;
						}
						goto IL_00d0;
					case 0:
						awaiter3 = _2005_2059;
						_2005_2059 = default;
						num = (_2005_2050 = -1);
						goto IL_00d0;
					case 1:
						awaiter2 = _2005_205A;
						_2005_205A = default;
						num = (_2005_2050 = -1);
						goto IL_0155;
					case 2:
						awaiter = _2005_205B;
						_2005_205B = default;
						num = (_2005_2050 = -1);
						goto IL_01c2;
					case 3:
						awaiter = _2005_205B;
						_2005_205B = default;
						num = (_2005_2050 = -1);
						goto IL_0231;
					case 4:
						awaiter = _2005_205B;
						_2005_205B = default;
						num = (_2005_2050 = -1);
						goto IL_02bb;
					case 5:
						awaiter = _2005_205B;
						_2005_205B = default;
						num = (_2005_2050 = -1);
						goto IL_031d;
					case 6:
						awaiter2 = _2005_205A;
						_2005_205A = default;
						num = (_2005_2050 = -1);
						goto IL_038e;
					case 7:
						awaiter = _2005_205B;
						_2005_205B = default;
						num = (_2005_2050 = -1);
						goto IL_0408;
					case 8:
						{
							awaiter = _2005_205B;
							_2005_205B = default;
							num = (_2005_2050 = -1);
							break;
						}
						IL_0231:
						awaiter.GetResult();
						if (!string.IsNullOrWhiteSpace(_2005_2055))
						{
							automaticHunting.PrintEcho("跨服目标切线: " + _2005_2055);
							awaiter = automaticHunting._2002(_2005_2055).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (_2005_2050 = 4);
								_2005_205B = awaiter;
								_2005_2051.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
							goto IL_02bb;
						}
						goto IL_0324;
						IL_00d0:
						result = awaiter3.GetResult();
						if (result != AutomaticHunting._2005._2002_202C)
						{
							if (result == AutomaticHunting._2005._2002_202B)
							{
								automaticHunting._00A0(AutomaticHunting._2003._2002_2013, _2005_2056);
								awaiter2 = automaticHunting._2000(_2005_2052, _2005_2057).GetAwaiter();
								if (!awaiter2.IsCompleted)
								{
									num = (_2005_2050 = 1);
									_2005_205A = awaiter2;
									_2005_2051.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
									return;
								}
								goto IL_0155;
							}
							goto IL_0163;
						}
						goto end_IL_001e;
						IL_0324:
						automaticHunting._00A0(AutomaticHunting._2003._2002_2014, _2005_2056);
						awaiter2 = automaticHunting._2002(_2005_2057).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (_2005_2050 = 6);
							_2005_205A = awaiter2;
							_2005_2051.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_038e;
						IL_0408:
						awaiter.GetResult();
						awaiter = automaticHunting._00A0(_2005_2058, _2005_2057, _2005_2056).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_2005_2050 = 8);
							_2005_205B = awaiter;
							_2005_2051.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						break;
						IL_02bb:
						awaiter.GetResult();
						awaiter = Task.Delay(3000).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_2005_2050 = 5);
							_2005_205B = awaiter;
							_2005_2051.AwaitUnsafeOnCompleted(ref awaiter, ref this);
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
						awaiter = automaticHunting._00A0(TimeSpan.FromSeconds(45L)).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_2005_2050 = 2);
							_2005_205B = awaiter;
							_2005_2051.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_01c2;
						IL_038e:
						if (awaiter2.GetResult())
						{
							awaiter = Task.Delay(500, _2005_2057).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (_2005_2050 = 7);
								_2005_205B = awaiter;
								_2005_2051.AwaitUnsafeOnCompleted(ref awaiter, ref this);
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
							num = (_2005_2050 = 3);
							_2005_205B = awaiter;
							_2005_2051.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_0231;
						IL_031d:
						awaiter.GetResult();
						goto IL_0324;
					}
					awaiter.GetResult();
					_2005_2057 = default;
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
						_2005_2056.Dispose();
					}
				}
			}
			catch (Exception exception2)
			{
				_2005_2050 = -2;
				_2005_2056 = null;
				_2005_2051.SetException(exception2);
				return;
			}
			_2005_2050 = -2;
			_2005_2056 = null;
			_2005_2051.SetResult();
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0();
		}

		[DebuggerHidden]
		private void _00A0(IAsyncStateMachine P_0)
		{
			_2005_2051.SetStateMachine(P_0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine P_0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0(P_0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	private struct _2063 : IAsyncStateMachine
	{
		public int _2005_205C;

		public AsyncTaskMethodBuilder<_2005> _2005_205D;

		public AutomaticHunting _2005_205E;

		public _00A0 _2005_205F;

		public CancellationToken _2005_2060;

		private (_2005 Preparation, uint AetheryteId, bool SameTerritory) _2005_2061;

		private TaskAwaiter<(_2005 Preparation, uint AetheryteId, bool SameTerritory)> _2005_2062;

		private TaskAwaiter<bool> _2005_2063;

		private void _00A0()
		{
			int num = _2005_205C;
			AutomaticHunting automaticHunting = _2005_205E;
			_2005 result2;
			try
			{
				TaskAwaiter<bool> awaiter;
				TaskAwaiter<(_2005, uint, bool)> awaiter2;
				if (num != 0)
				{
					if (num == 1)
					{
						awaiter = _2005_2063;
						_2005_2063 = default;
						num = (_2005_205C = -1);
						goto IL_0135;
					}
					AutomaticHunting automaticHunting2 = _2005_205E;
					_00A0 obj = _2005_205F;
					awaiter2 = Framework.RunOnFrameworkThread(() => (Preparation: automaticHunting2._00A0(obj, out var item), AetheryteId: item, SameTerritory: _2012() == obj.TerritoryType)).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						num = (_2005_205C = 0);
						_2005_2062 = awaiter2;
						_2005_205D.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
						return;
					}
				}
				else
				{
					awaiter2 = _2005_2062;
					_2005_2062 = default;
					num = (_2005_205C = -1);
				}
				(_2005, uint, bool) result = awaiter2.GetResult();
				_2005_2061 = result;
				if (_2005_2061.Preparation == AutomaticHunting._2005._2002_202B)
				{
					awaiter = automaticHunting._00A0(_2005_2061.AetheryteId, _2005_2060).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (_2005_205C = 1);
						_2005_2063 = awaiter;
						_2005_205D.AwaitUnsafeOnCompleted(ref awaiter, ref this);
						return;
					}
					goto IL_0135;
				}
				result2 = _2005_2061.Preparation;
				goto end_IL_000e;
				IL_0135:
				if (awaiter.GetResult())
				{
					result2 = AutomaticHunting._2005._2002_202B;
				}
				else if (_2005_2061.SameTerritory)
				{
					automaticHunting.PrintEcho("无法传送，改用直接导航");
					result2 = AutomaticHunting._2005._2002_202A;
				}
				else
				{
					automaticHunting.PrintEcho("跨图传送未开始，导航已取消");
					result2 = AutomaticHunting._2005._2002_202C;
				}
				end_IL_000e:;
			}
			catch (Exception exception)
			{
				_2005_205C = -2;
				_2005_205D.SetException(exception);
				return;
			}
			_2005_205C = -2;
			_2005_205D.SetResult(result2);
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0();
		}

		[DebuggerHidden]
		private void _00A0(IAsyncStateMachine P_0)
		{
			_2005_205D.SetStateMachine(P_0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine P_0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0(P_0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	private struct _2064 : IAsyncStateMachine
	{
		public int _2005_2064;

		public AsyncTaskMethodBuilder _2005_206A;

		public uint _2005_206B;

		public AutomaticHunting _2005_206C;

		public string _2005_206D;

		public CancellationToken _2005_206E;

		public string _2005_206F;

		private uint _2005_3000;

		private DateTime _2006_00A0;

		private DateTime _2006_1680;

		private DateTime _2006_2000;

		private bool _2006_2001;

		private bool _2006_2002;

		private bool _2006_2003;

		private int _2006_2004;

		private int _2006_2005;

		private TaskAwaiter<uint> _2006_2006;

		private TaskAwaiter _2006_2007;

		private TaskAwaiter<string> _2006_2008;

		private bool _2006_2009;

		private bool _2006_200A;

		private TaskAwaiter<(bool, bool)> _2006_200B;

		private void _00A0()
		{
			int num = _2005_2064;
			AutomaticHunting automaticHunting = _2005_206C;
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
						if (_2005_206B != 0)
						{
							_2005_3000 = 0u;
							_2006_2005 = 0;
							goto IL_016d;
						}
						automaticHunting.PrintEcho("无法识别目标服务器: " + _2005_206D + "，跨服中止");
						goto end_IL_0014;
					case 0:
						awaiter3 = _2006_2006;
						_2006_2006 = default;
						num = (_2005_2064 = -1);
						goto IL_00d9;
					case 1:
						awaiter = _2006_2007;
						_2006_2007 = default;
						num = (_2005_2064 = -1);
						goto IL_0154;
					case 2:
						awaiter2 = _2006_2008;
						_2006_2008 = default;
						num = (_2005_2064 = -1);
						goto IL_01fc;
					case 3:
						awaiter = _2006_2007;
						_2006_2007 = default;
						num = (_2005_2064 = -1);
						goto IL_029b;
					case 4:
						awaiter = _2006_2007;
						_2006_2007 = default;
						num = (_2005_2064 = -1);
						goto IL_031a;
					case 5:
						awaiter = _2006_2007;
						_2006_2007 = default;
						num = (_2005_2064 = -1);
						goto IL_03cf;
					case 6:
						awaiter4 = _2006_200B;
						_2006_200B = default;
						num = (_2005_2064 = -1);
						goto IL_0450;
					case 7:
						awaiter3 = _2006_2006;
						_2006_2006 = default;
						num = (_2005_2064 = -1);
						goto IL_04ff;
					case 8:
						awaiter2 = _2006_2008;
						_2006_2008 = default;
						num = (_2005_2064 = -1);
						goto IL_0584;
					case 9:
						awaiter = _2006_2007;
						_2006_2007 = default;
						num = (_2005_2064 = -1);
						goto IL_0648;
					case 10:
						awaiter = _2006_2007;
						_2006_2007 = default;
						num = (_2005_2064 = -1);
						goto IL_07e2;
					case 11:
						{
							awaiter = _2006_2007;
							_2006_2007 = default;
							num = (_2005_2064 = -1);
							break;
						}
						IL_029b:
						awaiter.GetResult();
						goto end_IL_0014;
						IL_0450:
						(_2006_2009, _2006_200A) = awaiter4.GetResult();
						if (_2006_2009 && !_2006_2002)
						{
							_2006_2002 = true;
							automaticHunting.PrintEcho("检测到跨服状态92(ReadyingVisitOtherWorld)：跨服指令已生效");
						}
						if (_2006_2009 | _2006_200A)
						{
							_2006_2003 = true;
						}
						awaiter3 = automaticHunting._200B().GetAwaiter();
						if (!awaiter3.IsCompleted)
						{
							num = (_2005_2064 = 7);
							_2006_2006 = awaiter3;
							_2005_206A.AwaitUnsafeOnCompleted(ref awaiter3, ref this);
							return;
						}
						goto IL_04ff;
						IL_016d:
						if (_2006_2005 < 3)
						{
							awaiter3 = automaticHunting._200B().GetAwaiter();
							if (!awaiter3.IsCompleted)
							{
								num = (_2005_2064 = 0);
								_2006_2006 = awaiter3;
								_2005_206A.AwaitUnsafeOnCompleted(ref awaiter3, ref this);
								return;
							}
							goto IL_00d9;
						}
						goto IL_0179;
						IL_0598:
						if (flag)
						{
							_2006_2001 = true;
							goto IL_0768;
						}
						if (!_2006_2003 && DateTime.UtcNow - _2006_1680 > TimeSpan.FromSeconds(5L) && !_2006_2009 && !_2006_200A)
						{
							awaiter = automaticHunting._2002(_2005_206F).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (_2005_2064 = 9);
								_2006_2007 = awaiter;
								_2005_206A.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
							goto IL_0648;
						}
						goto IL_06c8;
						IL_031a:
						awaiter.GetResult();
						_2006_00A0 = DateTime.UtcNow;
						_2006_1680 = DateTime.UtcNow;
						_2006_2000 = DateTime.UtcNow;
						_2006_2001 = false;
						_2006_2002 = false;
						_2006_2003 = false;
						_2006_2004 = 0;
						goto IL_0746;
						IL_00d9:
						result = awaiter3.GetResult();
						_2005_3000 = result;
						if (_2005_3000 == 0)
						{
							awaiter = Task.Delay(1000, _2005_206E).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (_2005_2064 = 1);
								_2006_2007 = awaiter;
								_2005_206A.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
							goto IL_0154;
						}
						goto IL_0179;
						IL_0746:
						if (DateTime.UtcNow - _2006_00A0 < TimeSpan.FromMinutes(10L))
						{
							_2005_206E.ThrowIfCancellationRequested();
							awaiter = Task.Delay(200, _2005_206E).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (_2005_2064 = 5);
								_2006_2007 = awaiter;
								_2005_206A.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
							goto IL_03cf;
						}
						goto IL_0768;
						IL_0648:
						awaiter.GetResult();
						_2006_1680 = DateTime.UtcNow;
						_2006_2004++;
						if (_2006_2004 == 1 || _2006_2004 % 6 == 0)
						{
							automaticHunting.PrintEcho($"跨服指令未受理，重发第{_2006_2004}次: {_2005_206F}");
						}
						goto IL_06c8;
						IL_0154:
						awaiter.GetResult();
						_2006_2005++;
						goto IL_016d;
						IL_0179:
						if (_2005_3000 != 0)
						{
							flag = _2005_3000 == _2005_206B;
							if (!flag)
							{
								awaiter2 = automaticHunting._2010().GetAwaiter();
								if (!awaiter2.IsCompleted)
								{
									num = (_2005_2064 = 2);
									_2006_2008 = awaiter2;
									_2005_206A.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
									return;
								}
								goto IL_01fc;
							}
							goto IL_0210;
						}
						automaticHunting.PrintEcho("无法读取当前服务器，跨服中止");
						goto end_IL_0014;
						IL_06c8:
						if (DateTime.UtcNow - _2006_2000 > TimeSpan.FromSeconds(60L))
						{
							_2006_2000 = DateTime.UtcNow;
							int value = (int)(DateTime.UtcNow - _2006_00A0).TotalSeconds;
							automaticHunting.PrintEcho($"跨服等待中: 已等待{value}/600秒");
						}
						goto IL_0746;
						IL_03cf:
						awaiter.GetResult();
						awaiter4 = Framework.RunOnFrameworkThread(() => (Condition[ConditionFlag.ReadyingVisitOtherWorld], Condition[ConditionFlag.BetweenAreas] || Condition[ConditionFlag.BetweenAreas51])).GetAwaiter();
						if (!awaiter4.IsCompleted)
						{
							num = (_2005_2064 = 6);
							_2006_200B = awaiter4;
							_2005_206A.AwaitUnsafeOnCompleted(ref awaiter4, ref this);
							return;
						}
						goto IL_0450;
						IL_0584:
						flag = awaiter2.GetResult() == _2005_206D;
						goto IL_0598;
						IL_07e2:
						awaiter.GetResult();
						automaticHunting.PrintEcho("跨服完成，已到达服务器[" + _2005_206D + "]");
						automaticHunting._2001(_2005_206D);
						awaiter = automaticHunting._00A0(_2005_206E).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_2005_2064 = 11);
							_2006_2007 = awaiter;
							_2005_206A.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						break;
						IL_01fc:
						flag = awaiter2.GetResult() == _2005_206D;
						goto IL_0210;
						IL_0210:
						if (flag)
						{
							automaticHunting.PrintEcho("已在服务器[" + _2005_206D + "]，跳过跨服");
							automaticHunting._2001(_2005_206D);
							awaiter = automaticHunting._00A0(_2005_206E).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (_2005_2064 = 3);
								_2006_2007 = awaiter;
								_2005_206A.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
							goto IL_029b;
						}
						automaticHunting.PrintEcho("执行跨服: " + _2005_206F);
						awaiter = automaticHunting._2002(_2005_206F).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_2005_2064 = 4);
							_2006_2007 = awaiter;
							_2005_206A.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_031a;
						IL_04ff:
						result2 = awaiter3.GetResult();
						if (result2 != 0 && result2 == _2005_206B)
						{
							_2006_2001 = true;
							goto IL_0768;
						}
						flag = result2 != 0;
						if (flag)
						{
							awaiter2 = automaticHunting._2010().GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								num = (_2005_2064 = 8);
								_2006_2008 = awaiter2;
								_2005_206A.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
								return;
							}
							goto IL_0584;
						}
						goto IL_0598;
						IL_0768:
						if (_2006_2001)
						{
							awaiter = Task.Delay(5000, _2005_206E).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (_2005_2064 = 10);
								_2006_2007 = awaiter;
								_2005_206A.AwaitUnsafeOnCompleted(ref awaiter, ref this);
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
						object obj = automaticHunting.m__2052;
						bool flag = false;
						try
						{
							Monitor.Enter(obj, ref flag);
							automaticHunting.m__2055 = false;
							automaticHunting.m__2056 = DateTime.MinValue;
							automaticHunting.m__2057 = 0u;
							automaticHunting.m__2058 = default;
							automaticHunting.m__2059 = null;
							automaticHunting.m__205A = 0u;
							automaticHunting.m__205B = 0u;
							automaticHunting.m__205C = default;
							automaticHunting.m__2054?.Dispose();
							automaticHunting.m__2054 = null;
							automaticHunting.m__2053 = null;
						}
						finally
						{
							if (num < 0 && flag)
							{
								Monitor.Exit(obj);
							}
						}
					}
				}
			}
			catch (Exception exception)
			{
				_2005_2064 = -2;
				_2005_206A.SetException(exception);
				return;
			}
			_2005_2064 = -2;
			_2005_206A.SetResult();
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0();
		}

		[DebuggerHidden]
		private void _00A0(IAsyncStateMachine P_0)
		{
			_2005_206A.SetStateMachine(P_0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine P_0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0(P_0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	private struct _206A : IAsyncStateMachine
	{
		public int _2006_2010;

		public AsyncTaskMethodBuilder _2006_2011;

		public CancellationTokenSource _2006_2012;

		public uint _2006_2013;

		public Vector3 _2006_2014;

		public AutomaticHunting _2006_2015;

		private CancellationToken _2006_2022;

		private TaskAwaiter<_2005> _2006_2024;

		private TaskAwaiter<bool> _2006_2025;

		private TaskAwaiter _2006_2027;

		private void _00A0()
		{
			int num = _2006_2010;
			AutomaticHunting automaticHunting = _2006_2015;
			try
			{
				_ = 5;
				try
				{
					TaskAwaiter<_2005> awaiter3;
					TaskAwaiter<bool> awaiter2;
					TaskAwaiter awaiter;
					_2005 result;
					switch (num)
					{
					default:
					{
						_2006_2022 = _2006_2012.Token;
						_00A0 obj = new _00A0(_2006_2013, _2006_2014);
						awaiter3 = automaticHunting._00A0(obj, _2006_2022).GetAwaiter();
						if (!awaiter3.IsCompleted)
						{
							num = (_2006_2010 = 0);
							_2006_2024 = awaiter3;
							_2006_2011.AwaitUnsafeOnCompleted(ref awaiter3, ref this);
							return;
						}
						goto IL_00b3;
					}
					case 0:
						awaiter3 = _2006_2024;
						_2006_2024 = default;
						num = (_2006_2010 = -1);
						goto IL_00b3;
					case 1:
						awaiter2 = _2006_2025;
						_2006_2025 = default;
						num = (_2006_2010 = -1);
						goto IL_0138;
					case 2:
						awaiter2 = _2006_2025;
						_2006_2025 = default;
						num = (_2006_2010 = -1);
						goto IL_01b0;
					case 3:
						awaiter = _2006_2027;
						_2006_2027 = default;
						num = (_2006_2010 = -1);
						goto IL_0227;
					case 4:
						awaiter2 = _2006_2025;
						_2006_2025 = default;
						num = (_2006_2010 = -1);
						goto IL_028b;
					case 5:
						{
							awaiter = _2006_2027;
							_2006_2027 = default;
							num = (_2006_2010 = -1);
							break;
						}
						IL_0146:
						automaticHunting._00A0(AutomaticHunting._2003._2002_2014, _2006_2012);
						awaiter2 = automaticHunting._2002(_2006_2022).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (_2006_2010 = 2);
							_2006_2025 = awaiter2;
							_2006_2011.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_01b0;
						IL_00b3:
						result = awaiter3.GetResult();
						if (result != AutomaticHunting._2005._2002_202C)
						{
							if (result == AutomaticHunting._2005._2002_202B)
							{
								automaticHunting._00A0(AutomaticHunting._2003._2002_2013, _2006_2012);
								awaiter2 = automaticHunting._2000(_2006_2013, _2006_2022).GetAwaiter();
								if (!awaiter2.IsCompleted)
								{
									num = (_2006_2010 = 1);
									_2006_2025 = awaiter2;
									_2006_2011.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
									return;
								}
								goto IL_0138;
							}
							goto IL_0146;
						}
						goto end_IL_0013;
						IL_0227:
						awaiter.GetResult();
						awaiter2 = automaticHunting._2001(_2006_2022).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (_2006_2010 = 4);
							_2006_2025 = awaiter2;
							_2006_2011.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_028b;
						IL_01b0:
						if (awaiter2.GetResult())
						{
							awaiter = Task.Delay(100, _2006_2022).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (_2006_2010 = 3);
								_2006_2027 = awaiter;
								_2006_2011.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
							goto IL_0227;
						}
						automaticHunting.PrintEcho("召唤坐骑失败，导航已取消");
						goto end_IL_0013;
						IL_028b:
						if (awaiter2.GetResult())
						{
							automaticHunting._00A0(AutomaticHunting._2003._2002_2015, _2006_2012);
							automaticHunting.PrintEcho("开始导航: /vnav flyflag");
							automaticHunting._2004("/vnav flyflag");
							awaiter = automaticHunting._00A0(_2006_2014, _2006_2012, _2006_2022).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (_2006_2010 = 5);
								_2006_2027 = awaiter;
								_2006_2011.AwaitUnsafeOnCompleted(ref awaiter, ref this);
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
					_2006_2022 = default;
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
						object obj2 = automaticHunting.m__204A;
						bool lockTaken = false;
						try
						{
							Monitor.Enter(obj2, ref lockTaken);
							if (automaticHunting.m__204B == _2006_2012)
							{
								automaticHunting.m__204B = null;
								automaticHunting.m__204C = null;
								automaticHunting.m__204D = AutomaticHunting._2003._2002_2012;
							}
						}
						finally
						{
							if (num < 0 && lockTaken)
							{
								Monitor.Exit(obj2);
							}
						}
						_2006_2012.Dispose();
					}
				}
			}
			catch (Exception exception)
			{
				_2006_2010 = -2;
				_2006_2011.SetException(exception);
				return;
			}
			_2006_2010 = -2;
			_2006_2011.SetResult();
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0();
		}

		[DebuggerHidden]
		private void _00A0(IAsyncStateMachine P_0)
		{
			_2006_2011.SetStateMachine(P_0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine P_0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0(P_0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	private struct _206B : IAsyncStateMachine
	{
		public int _2006_2028;

		public AsyncTaskMethodBuilder _2006_2029;

		public CancellationTokenSource _2006_202A;

		public AutomaticHunting _2006_202B;

		public _00A0 _2006_202C;

		private CancellationToken _2006_202D;

		private TaskAwaiter<_2005> _2006_202E;

		private TaskAwaiter<bool> _2006_202F;

		private TaskAwaiter _2006_2032;

		private void _00A0()
		{
			int num = _2006_2028;
			AutomaticHunting automaticHunting = _2006_202B;
			try
			{
				_ = 4;
				try
				{
					TaskAwaiter<_2005> awaiter3;
					TaskAwaiter<bool> awaiter2;
					TaskAwaiter awaiter;
					_2005 result;
					switch (num)
					{
					default:
						_2006_202D = _2006_202A.Token;
						awaiter3 = automaticHunting._00A0(_2006_202C, _2006_202D).GetAwaiter();
						if (!awaiter3.IsCompleted)
						{
							num = (_2006_2028 = 0);
							_2006_202E = awaiter3;
							_2006_2029.AwaitUnsafeOnCompleted(ref awaiter3, ref this);
							return;
						}
						goto IL_009e;
					case 0:
						awaiter3 = _2006_202E;
						_2006_202E = default;
						num = (_2006_2028 = -1);
						goto IL_009e;
					case 1:
						awaiter2 = _2006_202F;
						_2006_202F = default;
						num = (_2006_2028 = -1);
						goto IL_012b;
					case 2:
						awaiter2 = _2006_202F;
						_2006_202F = default;
						num = (_2006_2028 = -1);
						goto IL_01ae;
					case 3:
						awaiter = _2006_2032;
						_2006_2032 = default;
						num = (_2006_2028 = -1);
						goto IL_0225;
					case 4:
						{
							awaiter = _2006_2032;
							_2006_2032 = default;
							num = (_2006_2028 = -1);
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
						if (result != AutomaticHunting._2005._2002_202C)
						{
							if (result == AutomaticHunting._2005._2002_202B)
							{
								automaticHunting._00A0(AutomaticHunting._2003._2002_2013, _2006_202A);
								awaiter2 = automaticHunting._2000(_2006_202C.TerritoryType, _2006_202D).GetAwaiter();
								if (!awaiter2.IsCompleted)
								{
									num = (_2006_2028 = 1);
									_2006_202F = awaiter2;
									_2006_2029.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
									return;
								}
								goto IL_012b;
							}
							goto IL_0139;
						}
						goto end_IL_0013;
						IL_0139:
						_2006_202D.ThrowIfCancellationRequested();
						automaticHunting._00A0(AutomaticHunting._2003._2002_2014, _2006_202A);
						awaiter2 = automaticHunting._2002(_2006_202D).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (_2006_2028 = 2);
							_2006_202F = awaiter2;
							_2006_2029.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_01ae;
						IL_0225:
						awaiter.GetResult();
						awaiter = automaticHunting._00A0(_2006_202C, _2006_202D, _2006_202A).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_2006_2028 = 4);
							_2006_2032 = awaiter;
							_2006_2029.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						break;
						IL_01ae:
						if (awaiter2.GetResult())
						{
							awaiter = Task.Delay(100, _2006_202D).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (_2006_2028 = 3);
								_2006_2032 = awaiter;
								_2006_2029.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
							goto IL_0225;
						}
						automaticHunting.PrintEcho("召唤坐骑失败，导航已取消");
						goto end_IL_0013;
					}
					awaiter.GetResult();
					_2006_202D = default;
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
						object obj = automaticHunting.m__204A;
						bool lockTaken = false;
						try
						{
							Monitor.Enter(obj, ref lockTaken);
							if (automaticHunting.m__204B == _2006_202A)
							{
								automaticHunting.m__204B = null;
								automaticHunting.m__204C = null;
								automaticHunting.m__204D = AutomaticHunting._2003._2002_2012;
							}
						}
						finally
						{
							if (num < 0 && lockTaken)
							{
								Monitor.Exit(obj);
							}
						}
						_2006_202A.Dispose();
					}
				}
			}
			catch (Exception exception)
			{
				_2006_2028 = -2;
				_2006_2029.SetException(exception);
				return;
			}
			_2006_2028 = -2;
			_2006_2029.SetResult();
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0();
		}

		[DebuggerHidden]
		private void _00A0(IAsyncStateMachine P_0)
		{
			_2006_2029.SetStateMachine(P_0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine P_0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0(P_0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	private struct _206C : IAsyncStateMachine
	{
		public int _2006_2035;

		public AsyncTaskMethodBuilder _2006_2033;

		public AutomaticHunting _2006_2036;

		public CancellationTokenSource _2006_203E;

		public _2000 _2006_2047;

		private _2035 _2006_2048;

		public CancellationToken _2006_2049;

		private _2033 _2006_204A;

		private bool _2006_204B;

		private int _2006_204C;

		private TaskAwaiter<bool> _2006_204D;

		private TaskAwaiter _2006_204E;

		private float _2006_204F;

		private TaskAwaiter<Vector3?> _2006_2050;

		private TaskAwaiter<_2000?> _2006_2051;

		private float _2006_2052;

		private float _2006_2053;

		private float _2006_2054;

		private float _2006_2055;

		private bool _2006_2056;

		private DateTime _2006_2057;

		private DateTime _2006_2058;

		private TaskAwaiter<string> _2006_2059;

		private (int kind, float hp, Vector3 pos, Vector3? player, bool isS) _2006_205A;

		private TaskAwaiter<(int kind, float hp, Vector3 pos, Vector3? player, bool isS)> _2006_205B;

		private float _2006_205C;

		private void _00A0()
		{
			int num = _2006_2035;
			AutomaticHunting CS_0024_003C_003E8__locals41 = _2006_2036;
			try
			{
				if ((uint)num > 10u)
				{
					CS_0024_003C_003E8__locals41.m__2028 = true;
				}
				try
				{
					TaskAwaiter<Vector3?> awaiter6;
					TaskAwaiter<_2000?> awaiter5;
					TaskAwaiter<string> awaiter4;
					TaskAwaiter<(int, float, Vector3, Vector3?, bool)> awaiter3;
					TaskAwaiter<bool> awaiter2;
					TaskAwaiter awaiter;
					bool result;
					Vector3? result2;
					bool result3;
					_2000? result4;
					string result5;
					(int, float, Vector3, Vector3?, bool) result6;
					SMonitorStatus sMonitor2;
					float item;
					SMonitorStatus sMonitor3;
					switch (num)
					{
					default:
						_2006_2048 = new _2035();
						_2006_2048._2003_202B = CS_0024_003C_003E8__locals41;
						CS_0024_003C_003E8__locals41._00A0(AutomaticHunting._2003._2002_2024, _2006_203E);
						CS_0024_003C_003E8__locals41.BeginSMonitor(_2006_2047.Name, "前往S怪", _2006_2047.HpPercent, CS_0024_003C_003E8__locals41.Config.SHpThreshold);
						_2006_2048._2003_202A = new Vector3(_2006_2047.Position.X, _2006_2047.Position.Y + CS_0024_003C_003E8__locals41.Config.SHoverHeight, _2006_2047.Position.Z);
						_2006_204B = false;
						_2006_204C = 1;
						goto IL_0230;
					case 0:
						awaiter2 = _2006_204D;
						_2006_204D = default;
						num = (_2006_2035 = -1);
						goto IL_0186;
					case 1:
						awaiter = _2006_204E;
						_2006_204E = default;
						num = (_2006_2035 = -1);
						goto IL_0217;
					case 2:
						awaiter = _2006_204E;
						_2006_204E = default;
						num = (_2006_2035 = -1);
						goto IL_032e;
					case 3:
						awaiter6 = _2006_2050;
						_2006_2050 = default;
						num = (_2006_2035 = -1);
						goto IL_03ab;
					case 4:
						awaiter5 = _2006_2051;
						_2006_2051 = default;
						num = (_2006_2035 = -1);
						goto IL_0444;
					case 5:
						awaiter = _2006_204E;
						_2006_204E = default;
						num = (_2006_2035 = -1);
						goto IL_054c;
					case 6:
						awaiter4 = _2006_2059;
						_2006_2059 = default;
						num = (_2006_2035 = -1);
						goto IL_05f0;
					case 7:
						awaiter = _2006_204E;
						_2006_204E = default;
						num = (_2006_2035 = -1);
						goto IL_076a;
					case 8:
						awaiter3 = _2006_205B;
						_2006_205B = default;
						num = (_2006_2035 = -1);
						goto IL_07ea;
					case 9:
						awaiter2 = _2006_204D;
						_2006_204D = default;
						num = (_2006_2035 = -1);
						goto IL_09d7;
					case 10:
						{
							awaiter = _2006_204E;
							_2006_204E = default;
							num = (_2006_2035 = -1);
							goto IL_0b6f;
						}
						IL_0230:
						if (_2006_204C <= 2)
						{
							awaiter2 = Framework.RunOnFrameworkThread(() =>
							{
								Vector3? vector2 = _2006_2048._2003_202B._1680(_2006_2048._2003_202A);
								Vector3 arg = (vector2.HasValue ? new Vector3(vector2.Value.X, _2006_2048._2003_202A.Y, vector2.Value.Z) : _2006_2048._2003_202A);
								ICallGateSubscriber<bool>? callGateSubscriber = _2006_2048._2003_202B.m__2012;
								return callGateSubscriber != null && callGateSubscriber.InvokeFunc() && (_2006_2048._2003_202B.m__2013?.InvokeFunc(arg, arg2: true) ?? false);
							}).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								num = (_2006_2035 = 0);
								_2006_204D = awaiter2;
								_2006_2033.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
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
							num = (_2006_2035 = 3);
							_2006_2050 = awaiter6;
							_2006_2033.AwaitUnsafeOnCompleted(ref awaiter6, ref this);
							return;
						}
						goto IL_03ab;
						IL_076a:
						awaiter.GetResult();
						_2006_2054 += 0.5f;
						awaiter3 = Framework.RunOnFrameworkThread(() =>
						{
							Vector3? localPlayerPosition = GetLocalPlayerPosition();
							if (TargetManager.Target is IBattleChara { MaxHp: >0u, CurrentHp: >0u } battleChara)
							{
								return (kind: 1, hp: (float)battleChara.CurrentHp / (float)battleChara.MaxHp * 100f, pos: battleChara.Position, player: localPlayerPosition, isS: CS_0024_003C_003E8__locals41._2006(battleChara.Name.TextValue));
							}
							_2000? obj2 = CS_0024_003C_003E8__locals41._2048();
							if (obj2.HasValue)
							{
								if (obj2.Value.HpPercent <= 0f)
								{
									return (kind: 2, hp: 0f, pos: obj2.Value.Position, player: localPlayerPosition, isS: true);
								}
								CS_0024_003C_003E8__locals41._00A0(obj2.Value);
								return (kind: 3, hp: obj2.Value.HpPercent, pos: obj2.Value.Position, player: localPlayerPosition, isS: true);
							}
							return (kind: 0, hp: -1f, pos: Vector3.Zero, player: localPlayerPosition, isS: false);
						}).GetAwaiter();
						if (!awaiter3.IsCompleted)
						{
							num = (_2006_2035 = 8);
							_2006_205B = awaiter3;
							_2006_2033.AwaitUnsafeOnCompleted(ref awaiter3, ref this);
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
						_2006_204B = result;
						if (!_2006_204B)
						{
							if (_2006_204C < 2)
							{
								CS_0024_003C_003E8__locals41.PrintEcho("S怪寻路失败，2秒后重新寻路...");
								awaiter = Task.Delay(TimeSpan.FromSeconds(2L), _2006_2049).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									num = (_2006_2035 = 1);
									_2006_204E = awaiter;
									_2006_2033.AwaitUnsafeOnCompleted(ref awaiter, ref this);
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
							_2006_204F = AutomaticHunting._00A0(result2.Value, _2006_2047.Position);
							awaiter5 = Framework.RunOnFrameworkThread((Func<_2000?>)CS_0024_003C_003E8__locals41._2048).GetAwaiter();
							if (!awaiter5.IsCompleted)
							{
								num = (_2006_2035 = 4);
								_2006_2051 = awaiter5;
								_2006_2033.AwaitUnsafeOnCompleted(ref awaiter5, ref this);
								return;
							}
							goto IL_0444;
						}
						goto IL_0c80;
						IL_09d7:
						result3 = awaiter2.GetResult();
						_2006_2057 = DateTime.UtcNow;
						if (result3 && (DateTime.UtcNow - _2006_2058).TotalSeconds >= 10.0)
						{
							_2006_2058 = DateTime.UtcNow;
							CS_0024_003C_003E8__locals41.PrintEcho($"S怪已移动，距离 {_2006_205C:0.0}m，重新悬停跟随");
						}
						goto IL_0a5b;
						IL_0c80:
						_2006_204C++;
						goto IL_0c92;
						IL_0217:
						awaiter.GetResult();
						goto IL_021e;
						IL_021e:
						_2006_204C++;
						goto IL_0230;
						IL_023c:
						if (!_2006_204B)
						{
							CS_0024_003C_003E8__locals41.PrintEcho("S怪导航失败: vnavmesh未就绪或无法规划路径");
							CS_0024_003C_003E8__locals41.FinishSMonitor("寻路失败");
							break;
						}
						CS_0024_003C_003E8__locals41.PrintEcho($"S怪导航已开始(悬停高度{CS_0024_003C_003E8__locals41.Config.SHoverHeight:0}m)，悬停期间监控血量...");
						_2006_204C = 0;
						goto IL_0c92;
						IL_0444:
						result4 = awaiter5.GetResult();
						_2006_204A._2003_202D = result4;
						if (_2006_204A._2003_202D.HasValue)
						{
							SMonitorStatus sMonitor = CS_0024_003C_003E8__locals41.SMonitor;
							if (sMonitor != null)
							{
								sMonitor.HpPercent = _2006_204A._2003_202D.Value.HpPercent;
							}
							if (_2006_204A._2003_202D.Value.HpPercent <= CS_0024_003C_003E8__locals41.Config.SHpThreshold)
							{
								awaiter = CS_0024_003C_003E8__locals41._00A0(_2006_204A._2003_202D.Value, _2006_204A._2003_202D.Value.HpPercent, _2006_2049).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									num = (_2006_2035 = 5);
									_2006_204E = awaiter;
									_2006_2033.AwaitUnsafeOnCompleted(ref awaiter, ref this);
									return;
								}
								goto IL_054c;
							}
							if (_2006_204F <= CS_0024_003C_003E8__locals41.Config.SHoverDistance)
							{
								CS_0024_003C_003E8__locals41._2004("/vnav stop");
								awaiter4 = Framework.RunOnFrameworkThread(() => _2006_204A._2003_202E._2003_202B._00A0(_2006_204A._2003_202D.Value)).GetAwaiter();
								if (!awaiter4.IsCompleted)
								{
									num = (_2006_2035 = 6);
									_2006_2059 = awaiter4;
									_2006_2033.AwaitUnsafeOnCompleted(ref awaiter4, ref this);
									return;
								}
								goto IL_05f0;
							}
							_2006_204A = null;
						}
						goto IL_0c80;
						IL_05f0:
						result5 = awaiter4.GetResult();
						CS_0024_003C_003E8__locals41.PrintEcho($"已悬停到S怪附近({_2006_204F:0.0}m)，已选中S怪:{result5 ?? _2006_204A._2003_202D.Value.Name}，当前血量:{_2006_204A._2003_202D.Value.HpPercent:0.0}%，等待血量降至{CS_0024_003C_003E8__locals41.Config.SHpThreshold:0}%");
						_2006_2052 = -1f;
						_2006_2053 = 0f;
						_2006_2054 = 0f;
						_2006_2055 = 0f;
						_2006_2056 = false;
						_2006_2057 = DateTime.MinValue;
						_2006_2058 = DateTime.MinValue;
						goto IL_0709;
						IL_0c92:
						if (_2006_204C < 120)
						{
							_2006_204A = new _2033();
							_2006_204A._2003_202E = _2006_2048;
							awaiter = Task.Delay(500, _2006_2049).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (_2006_2035 = 2);
								_2006_204E = awaiter;
								_2006_2033.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
							goto IL_032e;
						}
						CS_0024_003C_003E8__locals41.PrintEcho("S怪悬停监控超时");
						CS_0024_003C_003E8__locals41.FinishSMonitor("悬停导航超时");
						_2006_2048 = null;
						break;
						IL_07ea:
						result6 = awaiter3.GetResult();
						_2006_205A = result6;
						if (_2006_205A.kind == 2)
						{
							CS_0024_003C_003E8__locals41.PrintEcho("S怪已死亡，悬停监控结束");
							CS_0024_003C_003E8__locals41.FinishSMonitor("S怪已死亡");
							break;
						}
						if (_2006_205A.kind == 1 || _2006_205A.kind == 3)
						{
							_2006_2055 = 0f;
							if (_2006_2056 && _2006_205A.kind == 3)
							{
								CS_0024_003C_003E8__locals41.PrintEcho("目标已重新选中: " + _2006_2047.Name);
							}
							_2006_2056 = false;
							_2006_205C = ((_2006_205A.player.HasValue && _2006_205A.isS) ? AutomaticHunting._00A0(_2006_205A.player.Value, _2006_205A.pos) : 0f);
							if (_2006_205C > CS_0024_003C_003E8__locals41.Config.SHoverDistance + 5f && (DateTime.UtcNow - _2006_2057).TotalSeconds >= 3.0)
							{
								_2033 obj = _2006_204A;
								Vector3 vector = new Vector3(_2006_205A.pos.X, _2006_205A.pos.Y + CS_0024_003C_003E8__locals41.Config.SHoverHeight, _2006_205A.pos.Z);
								awaiter2 = Framework.RunOnFrameworkThread(() =>
								{
									Vector3? vector2 = obj._2003_202E._2003_202B._1680(vector);
									Vector3 arg = (vector2.HasValue ? new Vector3(vector2.Value.X, vector.Y, vector2.Value.Z) : vector);
									ICallGateSubscriber<bool>? callGateSubscriber = obj._2003_202E._2003_202B.m__2012;
									return callGateSubscriber != null && callGateSubscriber.InvokeFunc() && (obj._2003_202E._2003_202B.m__2013?.InvokeFunc(arg, arg2: true) ?? false);
								}).GetAwaiter();
								if (!awaiter2.IsCompleted)
								{
									num = (_2006_2035 = 9);
									_2006_204D = awaiter2;
									_2006_2033.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
									return;
								}
								goto IL_09d7;
							}
							goto IL_0a5b;
						}
						_2006_2055 += 0.5f;
						_2006_2056 = true;
						sMonitor2 = CS_0024_003C_003E8__locals41.SMonitor;
						if (sMonitor2 != null)
						{
							sMonitor2.Phase = "目标丢失";
							sMonitor2.TargetLost = true;
							sMonitor2.LostSeconds = _2006_2055;
						}
						if (_2006_2055 >= 30f)
						{
							CS_0024_003C_003E8__locals41.PrintEcho("目标丢失30秒，悬停监控结束");
							CS_0024_003C_003E8__locals41.FinishSMonitor("目标丢失30秒");
							break;
						}
						goto IL_0709;
						IL_0a5b:
						item = _2006_205A.hp;
						sMonitor3 = CS_0024_003C_003E8__locals41.SMonitor;
						if (sMonitor3 != null)
						{
							sMonitor3.Phase = "悬停监控";
							sMonitor3.HpPercent = item;
							sMonitor3.WaitedSeconds = _2006_2054;
							sMonitor3.TargetLost = false;
						}
						if (Math.Abs(item - _2006_2052) >= 5f)
						{
							_2006_2052 = item;
							CS_0024_003C_003E8__locals41.PrintEcho($"悬停监控中: S怪血量 {item:0.0}%");
						}
						if (item <= CS_0024_003C_003E8__locals41.Config.SHpThreshold)
						{
							awaiter = CS_0024_003C_003E8__locals41._00A0(_2006_2047, item, _2006_2049).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (_2006_2035 = 10);
								_2006_204E = awaiter;
								_2006_2033.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
							goto IL_0b6f;
						}
						if (_2006_2054 - _2006_2053 >= 60f)
						{
							_2006_2053 = _2006_2054;
							CS_0024_003C_003E8__locals41.PrintEcho($"悬停监控中: 已等待{_2006_2054:0}秒, S怪血量 {item:0.0}%");
						}
						goto IL_0709;
						IL_0709:
						awaiter = Task.Delay(500, _2006_2049).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_2006_2035 = 7);
							_2006_204E = awaiter;
							_2006_2033.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_076a;
					}
				}
				finally
				{
					if (num < 0)
					{
						CS_0024_003C_003E8__locals41.m__2028 = false;
					}
				}
			}
			catch (Exception exception)
			{
				_2006_2035 = -2;
				_2006_2033.SetException(exception);
				return;
			}
			_2006_2035 = -2;
			_2006_2033.SetResult();
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0();
		}

		[DebuggerHidden]
		private void _00A0(IAsyncStateMachine P_0)
		{
			_2006_2033.SetStateMachine(P_0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine P_0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0(P_0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	private struct _206D : IAsyncStateMachine
	{
		public int _2006_205D;

		public AsyncTaskMethodBuilder<bool> _2006_205E;

		public AutomaticHunting _2006_205F;

		public CancellationToken _2006_2060;

		public uint _2006_2061;

		private _2024 _2006_2062;

		private DateTime _2006_2063;

		private DateTime _2006_2064;

		private TaskAwaiter _2006_206A;

		private TaskAwaiter<(bool Between, bool Foreign)> _2006_206B;

		private void _00A0()
		{
			int num = _2006_205D;
			AutomaticHunting automaticHunting = _2006_205F;
			bool result;
			try
			{
				TaskAwaiter<(bool, bool)> awaiter2;
				TaskAwaiter awaiter;
				(bool, bool) result2;
				switch (num)
				{
				default:
					_2006_2062 = new _2024();
					_2006_2062._2002_206B = _2006_205F;
					_2006_2062._2002_206C = _2006_2060;
					_2006_2062._2003_2000 = _2006_2061;
					awaiter = Framework.RunOnFrameworkThread(() =>
					{
						_2006_2062._2002_206B._2004("/vnav stop");
					}).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (_2006_205D = 0);
						_2006_206A = awaiter;
						_2006_205E.AwaitUnsafeOnCompleted(ref awaiter, ref this);
						return;
					}
					goto IL_00cb;
				case 0:
					awaiter = _2006_206A;
					_2006_206A = default;
					num = (_2006_205D = -1);
					goto IL_00cb;
				case 1:
					awaiter2 = _2006_206B;
					_2006_206B = default;
					num = (_2006_205D = -1);
					goto IL_01f3;
				case 2:
					awaiter = _2006_206A;
					_2006_206A = default;
					num = (_2006_205D = -1);
					goto IL_0275;
				case 3:
					{
						awaiter = _2006_206A;
						_2006_206A = default;
						num = (_2006_205D = -1);
						goto IL_036f;
					}
					IL_0275:
					awaiter.GetResult();
					goto IL_0376;
					IL_00cb:
					awaiter.GetResult();
					_2006_2063 = DateTime.UtcNow;
					_2006_2062._2003_00A0 = _2006_2063;
					_2006_2064 = _2006_2063.AddSeconds(10.0);
					_2006_2062._2002_3000 = null;
					_2006_2062._2003_1680 = 0;
					_2006_2062._2002_206D = 0;
					_2006_2062._2002_206E = null;
					_2006_2062._2002_206F = null;
					goto IL_0376;
					IL_0376:
					if (DateTime.UtcNow - _2006_2063 < TimeSpan.FromSeconds(60L))
					{
						_2006_2062._2002_206C.ThrowIfCancellationRequested();
						awaiter2 = Framework.RunOnFrameworkThread(() =>
						{
							_2006_2062._2002_206C.ThrowIfCancellationRequested();
							if (Condition[ConditionFlag.BetweenAreas] || Condition[ConditionFlag.BetweenAreas51])
							{
								return (Between: _2006_2062._2002_206D > 0, Foreign: _2006_2062._2002_206D == 0);
							}
							if (_2014())
							{
								DateTime valueOrDefault = _2006_2062._2002_206E.GetValueOrDefault();
								if (!_2006_2062._2002_206E.HasValue)
								{
									valueOrDefault = DateTime.UtcNow;
									_2006_2062._2002_206E = valueOrDefault;
								}
								_2006_2062._2002_206F = null;
								return (Between: false, Foreign: false);
							}
							if (_2006_2062._2002_206E.HasValue)
							{
								DateTime valueOrDefault = _2006_2062._2002_206F.GetValueOrDefault();
								if (!_2006_2062._2002_206F.HasValue)
								{
									valueOrDefault = DateTime.UtcNow;
									_2006_2062._2002_206F = valueOrDefault;
								}
								if (DateTime.UtcNow - _2006_2062._2002_206F.Value < TimeSpan.FromSeconds(2L))
								{
									return (Between: false, Foreign: false);
								}
							}
							Vector3? localPlayerPosition = GetLocalPlayerPosition();
							bool num2 = localPlayerPosition.HasValue && _2006_2062._2002_3000.HasValue && Vector3.DistanceSquared(localPlayerPosition.Value, _2006_2062._2002_3000.Value) < 0.0001f;
							_2006_2062._2002_3000 = localPlayerPosition;
							if (!num2 || _2024() || _2014() || Condition[ConditionFlag.MountOrOrnamentTransition] || DateTime.UtcNow < _2006_2062._2003_00A0 || _2006_2062._2002_206B._2015() || !_2013())
							{
								return (Between: false, Foreign: false);
							}
							_2006_2062._2003_1680++;
							bool flag = _2006_2062._2002_206B._2001(_2006_2062._2003_2000);
							if (flag)
							{
								_2006_2062._2002_206D++;
							}
							_2006_2062._2003_00A0 = DateTime.UtcNow.AddSeconds((!flag) ? 1 : 2);
							return (Between: false, Foreign: false);
						}).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (_2006_205D = 1);
							_2006_206B = awaiter2;
							_2006_205E.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_01f3;
					}
					automaticHunting.PrintEcho($"传送失败: 60秒内未进入过图(id={_2006_2062._2003_2000})");
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
						awaiter = Task.Delay(100, _2006_2062._2002_206C).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_2006_205D = 2);
							_2006_206A = awaiter;
							_2006_205E.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_0275;
					}
					if (DateTime.UtcNow >= _2006_2064)
					{
						automaticHunting.PrintEcho($"等待传送开始(id={_2006_2062._2003_2000}，已尝试{_2006_2062._2003_1680}次)...");
						_2006_2064 = DateTime.UtcNow.AddSeconds(10.0);
					}
					awaiter = Task.Delay(100, _2006_2062._2002_206C).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (_2006_205D = 3);
						_2006_206A = awaiter;
						_2006_205E.AwaitUnsafeOnCompleted(ref awaiter, ref this);
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
				_2006_205D = -2;
				_2006_2062 = null;
				_2006_205E.SetException(exception);
				return;
			}
			_2006_205D = -2;
			_2006_2062 = null;
			_2006_205E.SetResult(result);
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0();
		}

		[DebuggerHidden]
		private void _00A0(IAsyncStateMachine P_0)
		{
			_2006_205E.SetStateMachine(P_0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine P_0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0(P_0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	private struct _206E : IAsyncStateMachine
	{
		public int _2006_206C;

		public AsyncTaskMethodBuilder<bool> _2006_206D;

		public CancellationToken _2006_206E;

		public AutomaticHunting _2006_206F;

		private bool _2006_3000;

		private TaskAwaiter<bool> _2007_00A0;

		private int _2007_1680;

		private TaskAwaiter _2007_2000;

		private DateTime _2007_2001;

		private int _2007_2002;

		private void _00A0()
		{
			int num = _2006_206C;
			AutomaticHunting CS_0024_003C_003E8__locals7 = _2006_206F;
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
						num = (_2006_206C = 0);
						_2007_00A0 = awaiter2;
						_2006_206D.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
						return;
					}
					goto IL_00bf;
				case 0:
					awaiter2 = _2007_00A0;
					_2007_00A0 = default;
					num = (_2006_206C = -1);
					goto IL_00bf;
				case 1:
					awaiter2 = _2007_00A0;
					_2007_00A0 = default;
					num = (_2006_206C = -1);
					goto IL_0159;
				case 2:
					awaiter = _2007_2000;
					_2007_2000 = default;
					num = (_2006_206C = -1);
					goto IL_01cd;
				case 3:
					awaiter = _2007_2000;
					_2007_2000 = default;
					num = (_2006_206C = -1);
					goto IL_025c;
				case 4:
					awaiter2 = _2007_00A0;
					_2007_00A0 = default;
					num = (_2006_206C = -1);
					goto IL_02c7;
				case 5:
					awaiter = _2007_2000;
					_2007_2000 = default;
					num = (_2006_206C = -1);
					goto IL_034f;
				case 6:
					awaiter2 = _2007_00A0;
					_2007_00A0 = default;
					num = (_2006_206C = -1);
					goto IL_03ba;
				case 7:
					awaiter2 = _2007_00A0;
					_2007_00A0 = default;
					num = (_2006_206C = -1);
					goto IL_045f;
				case 8:
					awaiter = _2007_2000;
					_2007_2000 = default;
					num = (_2006_206C = -1);
					goto IL_04c9;
				case 9:
					awaiter2 = _2007_00A0;
					_2007_00A0 = default;
					num = (_2006_206C = -1);
					goto IL_0535;
				case 10:
					awaiter2 = _2007_00A0;
					_2007_00A0 = default;
					num = (_2006_206C = -1);
					goto IL_05c5;
				case 11:
					awaiter = _2007_2000;
					_2007_2000 = default;
					num = (_2006_206C = -1);
					goto IL_0634;
				case 12:
					{
						awaiter = _2007_2000;
						_2007_2000 = default;
						num = (_2006_206C = -1);
						goto IL_06c2;
					}
					IL_03c3:
					if (DateTime.UtcNow - _2007_2001 < TimeSpan.FromSeconds(5L))
					{
						awaiter = Task.Delay(200, _2006_206E).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_2006_206C = 5);
							_2007_2000 = awaiter;
							_2006_206D.AwaitUnsafeOnCompleted(ref awaiter, ref this);
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
					awaiter2 = Framework.RunOnFrameworkThread((Func<bool>)CS_0024_003C_003E8__locals7._2032).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						num = (_2006_206C = 9);
						_2007_00A0 = awaiter2;
						_2006_206D.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
						return;
					}
					goto IL_0535;
					IL_00bf:
					if (awaiter2.GetResult())
					{
						result = true;
						break;
					}
					_2006_3000 = false;
					_2007_1680 = 0;
					goto IL_01e6;
					IL_0634:
					awaiter.GetResult();
					_2007_2002++;
					goto IL_064d;
					IL_034f:
					awaiter.GetResult();
					awaiter2 = Framework.RunOnFrameworkThread((Func<bool>)CS_0024_003C_003E8__locals7._2035).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						num = (_2006_206C = 6);
						_2007_00A0 = awaiter2;
						_2006_206D.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
						return;
					}
					goto IL_03ba;
					IL_01e6:
					if (_2007_1680 < 30)
					{
						awaiter2 = Framework.RunOnFrameworkThread(() => _2024()).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (_2006_206C = 1);
							_2007_00A0 = awaiter2;
							_2006_206D.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_0159;
					}
					goto IL_01f3;
					IL_0535:
					if (awaiter2.GetResult())
					{
						_2007_2002 = 0;
						goto IL_064d;
					}
					goto IL_065a;
					IL_03e4:
					CS_0024_003C_003E8__locals7.PrintEcho("召唤坐骑...");
					_2007_1680 = 1;
					goto IL_06db;
					IL_03ba:
					if (!awaiter2.GetResult())
					{
						goto IL_03c3;
					}
					goto IL_03e4;
					IL_06db:
					if (_2007_1680 <= 5)
					{
						awaiter2 = Framework.RunOnFrameworkThread(() => (CS_0024_003C_003E8__locals7.Config.SelectedMountId != 0) ? _2003(CS_0024_003C_003E8__locals7.Config.SelectedMountId) : _2047()).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (_2006_206C = 7);
							_2007_00A0 = awaiter2;
							_2006_206D.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_045f;
					}
					result = false;
					break;
					IL_0159:
					if (awaiter2.GetResult())
					{
						_2006_3000 = true;
						awaiter = Task.Delay(500, _2006_206E).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_2006_206C = 2);
							_2007_2000 = awaiter;
							_2006_206D.AwaitUnsafeOnCompleted(ref awaiter, ref this);
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
					awaiter = Task.Delay(100, _2006_206E).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (_2006_206C = 11);
						_2007_2000 = awaiter;
						_2006_206D.AwaitUnsafeOnCompleted(ref awaiter, ref this);
						return;
					}
					goto IL_0634;
					IL_064d:
					if (_2007_2002 < 30)
					{
						awaiter2 = Framework.RunOnFrameworkThread(() => Condition[ConditionFlag.Mounted]).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (_2006_206C = 10);
							_2007_00A0 = awaiter2;
							_2006_206D.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_05c5;
					}
					goto IL_065a;
					IL_01cd:
					awaiter.GetResult();
					_2007_1680++;
					goto IL_01e6;
					IL_01f3:
					if (_2006_3000)
					{
						awaiter = Task.Delay(1000, _2006_206E).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_2006_206C = 3);
							_2007_2000 = awaiter;
							_2006_206D.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_025c;
					}
					goto IL_0263;
					IL_065a:
					if (_2007_1680 < 5)
					{
						awaiter = Task.Delay(500, _2006_206E).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_2006_206C = 12);
							_2007_2000 = awaiter;
							_2006_206D.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_06c2;
					}
					goto IL_06c9;
					IL_045f:
					if (awaiter2.GetResult())
					{
						awaiter = Task.Delay(100, _2006_206E).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_2006_206C = 8);
							_2007_2000 = awaiter;
							_2006_206D.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_04c9;
					}
					goto IL_065a;
					IL_025c:
					awaiter.GetResult();
					goto IL_0263;
					IL_0263:
					awaiter2 = Framework.RunOnFrameworkThread((Func<bool>)CS_0024_003C_003E8__locals7._2035).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						num = (_2006_206C = 4);
						_2007_00A0 = awaiter2;
						_2006_206D.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
						return;
					}
					goto IL_02c7;
					IL_06c9:
					_2007_1680++;
					goto IL_06db;
					IL_02c7:
					if (!awaiter2.GetResult())
					{
						CS_0024_003C_003E8__locals7.PrintEcho("等待区域加载/动作就绪...");
						_2007_2001 = DateTime.UtcNow;
						goto IL_03c3;
					}
					goto IL_03e4;
				}
			}
			catch (Exception exception)
			{
				_2006_206C = -2;
				_2006_206D.SetException(exception);
				return;
			}
			_2006_206C = -2;
			_2006_206D.SetResult(result);
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0();
		}

		[DebuggerHidden]
		private void _00A0(IAsyncStateMachine P_0)
		{
			_2006_206D.SetStateMachine(P_0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine P_0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0(P_0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	private struct _206F : IAsyncStateMachine
	{
		public int _2007_2003;

		public AsyncTaskMethodBuilder _2007_2004;

		public AutomaticHunting _2007_2005;

		private TaskAwaiter<bool> _2007_2006;

		private void _00A0()
		{
			int num = _2007_2003;
			AutomaticHunting automaticHunting = _2007_2005;
			try
			{
				try
				{
					TaskAwaiter<bool> awaiter;
					if (num != 0)
					{
						awaiter = automaticHunting._2002(CancellationToken.None).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_2007_2003 = 0);
							_2007_2006 = awaiter;
							_2007_2004.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = _2007_2006;
						_2007_2006 = default;
						num = (_2007_2003 = -1);
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
				_2007_2003 = -2;
				_2007_2004.SetException(exception2);
				return;
			}
			_2007_2003 = -2;
			_2007_2004.SetResult();
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0();
		}

		[DebuggerHidden]
		private void _00A0(IAsyncStateMachine P_0)
		{
			_2007_2004.SetStateMachine(P_0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine P_0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0(P_0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	private struct _3000 : IAsyncStateMachine
	{
		public int _2007_2007;

		public AsyncTaskMethodBuilder _2007_2008;

		public AutomaticHunting _2007_2009;

		public int _2007_200A;

		private DateTime _2007_200B;

		private TaskAwaiter _2007_2010;

		private TaskAwaiter<bool> _2007_2011;

		private int _2007_2012;

		private void _00A0()
		{
			int num = _2007_2007;
			AutomaticHunting automaticHunting = _2007_2009;
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
						_2007_200B = DateTime.UtcNow;
						goto IL_013a;
					case 0:
						awaiter = _2007_2010;
						_2007_2010 = default;
						num = (_2007_2007 = -1);
						goto IL_0095;
					case 1:
						awaiter2 = _2007_2011;
						_2007_2011 = default;
						num = (_2007_2007 = -1);
						goto IL_0113;
					case 2:
						awaiter = _2007_2010;
						_2007_2010 = default;
						num = (_2007_2007 = -1);
						goto IL_01c1;
					case 3:
						awaiter2 = _2007_2011;
						_2007_2011 = default;
						num = (_2007_2007 = -1);
						goto IL_024b;
					case 4:
						{
							awaiter = _2007_2010;
							_2007_2010 = default;
							num = (_2007_2007 = -1);
							goto IL_02ac;
						}
						IL_013a:
						if (DateTime.UtcNow - _2007_200B < TimeSpan.FromSeconds(45L))
						{
							awaiter = Task.Delay(500).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (_2007_2007 = 0);
								_2007_2010 = awaiter;
								_2007_2008.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
							goto IL_0095;
						}
						goto IL_015c;
						IL_02ac:
						awaiter.GetResult();
						_2007_2012++;
						goto IL_02c5;
						IL_015c:
						awaiter = Task.Delay((int)(automaticHunting.Config.WaitAfterTeleport * 1000f)).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_2007_2007 = 2);
							_2007_2010 = awaiter;
							_2007_2008.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_01c1;
						IL_0095:
						awaiter.GetResult();
						awaiter2 = Framework.RunOnFrameworkThread(() => Condition[ConditionFlag.BetweenAreas] || Condition[ConditionFlag.BetweenAreas51]).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (_2007_2007 = 1);
							_2007_2011 = awaiter2;
							_2007_2008.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_0113;
						IL_01c1:
						awaiter.GetResult();
						_2007_2012 = 0;
						goto IL_02c5;
						IL_024b:
						if (!awaiter2.GetResult())
						{
							break;
						}
						awaiter = Task.Delay(500).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_2007_2007 = 4);
							_2007_2010 = awaiter;
							_2007_2008.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_02ac;
						IL_02c5:
						if (_2007_2012 >= 30)
						{
							break;
						}
						awaiter2 = Framework.RunOnFrameworkThread(() => _2024()).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (_2007_2007 = 3);
							_2007_2011 = awaiter2;
							_2007_2008.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_024b;
						IL_0113:
						if (awaiter2.GetResult() || !(DateTime.UtcNow - _2007_200B > TimeSpan.FromSeconds(2L)))
						{
							goto IL_013a;
						}
						goto IL_015c;
					}
					automaticHunting.PrintEcho("进入多线地图: 区域已稳定，执行切线");
					automaticHunting.SwitchToInstance(_2007_200A);
				}
				catch (Exception exception)
				{
					Log.Warning(exception, "AutomaticHunting: 等待稳定后切线失败");
				}
			}
			catch (Exception exception2)
			{
				_2007_2007 = -2;
				_2007_2008.SetException(exception2);
				return;
			}
			_2007_2007 = -2;
			_2007_2008.SetResult();
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0();
		}

		[DebuggerHidden]
		private void _00A0(IAsyncStateMachine P_0)
		{
			_2007_2008.SetStateMachine(P_0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine P_0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0(P_0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	private struct _1680_00A0 : IAsyncStateMachine
	{
		public int _2007_2013;

		public AsyncTaskMethodBuilder _2007_2014;

		private _2010 _2007_2015;

		public AutomaticHunting _2007_2022;

		private List<(uint Id, Vector3 Position, string Name)> _2007_2024;

		private TaskAwaiter<List<(uint Id, Vector3 Position, string Name)>> _2007_2025;

		private TaskAwaiter<Vector3?> _2007_2027;

		private TaskAwaiter<bool> _2007_2028;

		private void _00A0()
		{
			int num = _2007_2013;
			AutomaticHunting automaticHunting = _2007_2022;
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
						_2007_2015 = new _2010();
						_2007_2015._2002_205A = _2012();
						awaiter3 = Framework.RunOnFrameworkThread(() => _2002(_2007_2015._2002_205A)).GetAwaiter();
						if (!awaiter3.IsCompleted)
						{
							num = (_2007_2013 = 0);
							_2007_2025 = awaiter3;
							_2007_2014.AwaitUnsafeOnCompleted(ref awaiter3, ref this);
							return;
						}
						goto IL_00b0;
					case 0:
						awaiter3 = _2007_2025;
						_2007_2025 = default;
						num = (_2007_2013 = -1);
						goto IL_00b0;
					case 1:
						awaiter2 = _2007_2027;
						_2007_2027 = default;
						num = (_2007_2013 = -1);
						goto IL_0147;
					case 2:
						awaiter = _2007_2028;
						_2007_2028 = default;
						num = (_2007_2013 = -1);
						goto IL_0235;
					case 3:
						{
							awaiter = _2007_2028;
							_2007_2028 = default;
							num = (_2007_2013 = -1);
							break;
						}
						IL_0147:
						result = awaiter2.GetResult();
						_2007_2015._2002_205B = result;
						if (_2007_2015._2002_205B.HasValue)
						{
							(uint, Vector3, string) tuple = _2007_2024.MinBy(((uint Id, Vector3 Position, string Name) a) => Vector3.DistanceSquared(a.Position, _2007_2015._2002_205B.Value));
							if (!(Vector3.Distance(tuple.Item2, _2007_2015._2002_205B.Value) <= 60f))
							{
								automaticHunting.PrintEcho("切线未生效，传送到最近以太之光(" + tuple.Item3 + ")后重试");
								awaiter = automaticHunting._00A0(tuple.Item1, CancellationToken.None).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									num = (_2007_2013 = 2);
									_2007_2028 = awaiter;
									_2007_2014.AwaitUnsafeOnCompleted(ref awaiter, ref this);
									return;
								}
								goto IL_0235;
							}
						}
						goto end_IL_0013;
						IL_00b0:
						result2 = awaiter3.GetResult();
						_2007_2024 = result2;
						if (_2007_2024.Count != 0)
						{
							awaiter2 = Framework.RunOnFrameworkThread((Func<Vector3?>)GetLocalPlayerPosition).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								num = (_2007_2013 = 1);
								_2007_2027 = awaiter2;
								_2007_2014.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
								return;
							}
							goto IL_0147;
						}
						goto end_IL_0013;
						IL_0235:
						if (awaiter.GetResult())
						{
							awaiter = automaticHunting._2000(_2007_2015._2002_205A, CancellationToken.None).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (_2007_2013 = 3);
								_2007_2028 = awaiter;
								_2007_2014.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
							break;
						}
						goto end_IL_0013;
					}
					awaiter.GetResult();
					_2007_2015 = null;
					_2007_2024 = null;
					end_IL_0013:;
				}
				catch (Exception exception)
				{
					Log.Warning(exception, "AutomaticHunting: 切线重试传送到以太之光失败");
				}
			}
			catch (Exception exception2)
			{
				_2007_2013 = -2;
				_2007_2014.SetException(exception2);
				return;
			}
			_2007_2013 = -2;
			_2007_2014.SetResult();
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0();
		}

		[DebuggerHidden]
		private void _00A0(IAsyncStateMachine P_0)
		{
			_2007_2014.SetStateMachine(P_0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine P_0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0(P_0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	private struct _1680_1680 : IAsyncStateMachine
	{
		public int _2007_2029;

		public AsyncTaskMethodBuilder<bool> _2007_202A;

		public AutomaticHunting _2007_202B;

		public CancellationToken _2007_202C;

		public uint _2007_202D;

		private TaskAwaiter _2007_202E;

		private TaskAwaiter<bool> _2007_202F;

		private void _00A0()
		{
			int num = _2007_2029;
			AutomaticHunting automaticHunting = _2007_202B;
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
					awaiter = automaticHunting.m__2061.WaitAsync(_2007_202C).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (_2007_2029 = 0);
						_2007_202E = awaiter;
						_2007_202A.AwaitUnsafeOnCompleted(ref awaiter, ref this);
						return;
					}
				}
				else
				{
					awaiter = _2007_202E;
					_2007_202E = default;
					num = (_2007_2029 = -1);
				}
				awaiter.GetResult();
				goto IL_007c;
				IL_007c:
				try
				{
					TaskAwaiter<bool> awaiter2;
					if (num != 1)
					{
						awaiter2 = automaticHunting._1680(_2007_202D, _2007_202C).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (_2007_2029 = 1);
							_2007_202F = awaiter2;
							_2007_202A.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
					}
					else
					{
						awaiter2 = _2007_202F;
						_2007_202F = default;
						num = (_2007_2029 = -1);
					}
					result = awaiter2.GetResult();
				}
				finally
				{
					if (num < 0)
					{
						automaticHunting.m__2061.Release();
					}
				}
			}
			catch (Exception exception)
			{
				_2007_2029 = -2;
				_2007_202A.SetException(exception);
				return;
			}
			_2007_2029 = -2;
			_2007_202A.SetResult(result);
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0();
		}

		[DebuggerHidden]
		private void _00A0(IAsyncStateMachine P_0)
		{
			_2007_202A.SetStateMachine(P_0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine P_0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0(P_0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	private struct _1680_2000 : IAsyncStateMachine
	{
		public int _2007_2032;

		public AsyncTaskMethodBuilder _2007_2035;

		public TimeSpan _2007_2033;

		private DateTime _2007_2036;

		private TaskAwaiter<bool> _2007_203E;

		private TaskAwaiter _2007_2047;

		private void _00A0()
		{
			int num = _2007_2032;
			try
			{
				TaskAwaiter awaiter;
				if (num != 0)
				{
					if (num != 1)
					{
						_2007_2036 = DateTime.UtcNow;
						goto IL_0102;
					}
					awaiter = _2007_2047;
					_2007_2047 = default;
					num = (_2007_2032 = -1);
					goto IL_00fb;
				}
				TaskAwaiter<bool> awaiter2 = _2007_203E;
				_2007_203E = default;
				num = (_2007_2032 = -1);
				goto IL_0098;
				IL_0102:
				if (DateTime.UtcNow - _2007_2036 < _2007_2033)
				{
					awaiter2 = Framework.RunOnFrameworkThread(() => _2024()).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						num = (_2007_2032 = 0);
						_2007_203E = awaiter2;
						_2007_2035.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
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
						num = (_2007_2032 = 1);
						_2007_2047 = awaiter;
						_2007_2035.AwaitUnsafeOnCompleted(ref awaiter, ref this);
						return;
					}
					goto IL_00fb;
				}
				end_IL_0007:;
			}
			catch (Exception exception)
			{
				_2007_2032 = -2;
				_2007_2035.SetException(exception);
				return;
			}
			_2007_2032 = -2;
			_2007_2035.SetResult();
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0();
		}

		[DebuggerHidden]
		private void _00A0(IAsyncStateMachine P_0)
		{
			_2007_2035.SetStateMachine(P_0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine P_0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0(P_0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	private struct _1680_2001 : IAsyncStateMachine
	{
		public int _2007_2048;

		public AsyncTaskMethodBuilder<bool> _2007_2049;

		public uint _2007_204A;

		public AutomaticHunting _2007_204B;

		public CancellationToken _2007_204C;

		private _202F _2007_204D;

		private DateTime _2007_204E;

		private TaskAwaiter _2007_204F;

		private TaskAwaiter<bool> _2007_2050;

		private int _2007_2051;

		private void _00A0()
		{
			int num = _2007_2048;
			AutomaticHunting automaticHunting = _2007_204B;
			bool result;
			try
			{
				TaskAwaiter<bool> awaiter2;
				TaskAwaiter awaiter;
				bool flag;
				switch (num)
				{
				default:
					_2007_204D = new _202F();
					_2007_204D._2003_2014 = _2007_204A;
					automaticHunting.PrintEcho("等待传送完成...");
					_2007_204E = DateTime.UtcNow;
					goto IL_03ed;
				case 0:
					awaiter = _2007_204F;
					_2007_204F = default;
					num = (_2007_2048 = -1);
					goto IL_00c1;
				case 1:
					awaiter2 = _2007_2050;
					_2007_2050 = default;
					num = (_2007_2048 = -1);
					goto IL_0142;
				case 2:
					awaiter2 = _2007_2050;
					_2007_2050 = default;
					num = (_2007_2048 = -1);
					goto IL_01e0;
				case 3:
					awaiter = _2007_204F;
					_2007_204F = default;
					num = (_2007_2048 = -1);
					goto IL_02a1;
				case 4:
					awaiter2 = _2007_2050;
					_2007_2050 = default;
					num = (_2007_2048 = -1);
					goto IL_033f;
				case 5:
					{
						awaiter = _2007_204F;
						_2007_204F = default;
						num = (_2007_2048 = -1);
						goto IL_03b8;
					}
					IL_03ed:
					if (DateTime.UtcNow - _2007_204E < TimeSpan.FromSeconds(45L))
					{
						awaiter = Task.Delay(500, _2007_204C).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_2007_2048 = 0);
							_2007_204F = awaiter;
							_2007_2049.AwaitUnsafeOnCompleted(ref awaiter, ref this);
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
					awaiter = Task.Delay(500, _2007_204C).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (_2007_2048 = 5);
						_2007_204F = awaiter;
						_2007_2049.AwaitUnsafeOnCompleted(ref awaiter, ref this);
						return;
					}
					goto IL_03b8;
					IL_02a1:
					awaiter.GetResult();
					_2007_2051 = 0;
					goto IL_03d1;
					IL_03b8:
					awaiter.GetResult();
					_2007_2051++;
					goto IL_03d1;
					IL_00c1:
					awaiter.GetResult();
					awaiter2 = Framework.RunOnFrameworkThread(() => Condition[ConditionFlag.BetweenAreas] || Condition[ConditionFlag.BetweenAreas51]).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						num = (_2007_2048 = 1);
						_2007_2050 = awaiter2;
						_2007_2049.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
						return;
					}
					goto IL_0142;
					IL_01e0:
					flag = awaiter2.GetResult();
					goto IL_01e9;
					IL_03d1:
					if (_2007_2051 < 30)
					{
						awaiter2 = Framework.RunOnFrameworkThread(() => !_2024() && !Condition[ConditionFlag.Casting] && _2012() == _2007_204D._2003_2014 && GetLocalPlayerPosition().HasValue).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (_2007_2048 = 4);
							_2007_2050 = awaiter2;
							_2007_2049.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
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
						awaiter = Task.Delay((int)(automaticHunting.Config.WaitAfterTeleport * 1000f), _2007_204C).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_2007_2048 = 3);
							_2007_204F = awaiter;
							_2007_2049.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_02a1;
					}
					goto IL_03ed;
					IL_0142:
					flag = !awaiter2.GetResult();
					if (flag)
					{
						awaiter2 = Framework.RunOnFrameworkThread(() => _2012() == _2007_204D._2003_2014 && GetLocalPlayerPosition().HasValue).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (_2007_2048 = 2);
							_2007_2050 = awaiter2;
							_2007_2049.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_01e0;
					}
					goto IL_01e9;
				}
			}
			catch (Exception exception)
			{
				_2007_2048 = -2;
				_2007_204D = null;
				_2007_2049.SetException(exception);
				return;
			}
			_2007_2048 = -2;
			_2007_204D = null;
			_2007_2049.SetResult(result);
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0();
		}

		[DebuggerHidden]
		private void _00A0(IAsyncStateMachine P_0)
		{
			_2007_2049.SetStateMachine(P_0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine P_0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0(P_0);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	private struct _1680_2002 : IAsyncStateMachine
	{
		public int _2007_2052;

		public AsyncTaskMethodBuilder<bool> _2007_2053;

		public AutomaticHunting _2007_2054;

		public CancellationToken _2007_2055;

		private int _2007_2056;

		private TaskAwaiter<bool> _2007_2057;

		private TaskAwaiter _2007_2058;

		private void _00A0()
		{
			int num = _2007_2052;
			AutomaticHunting CS_0024_003C_003E8__locals1 = _2007_2054;
			bool result;
			try
			{
				TaskAwaiter awaiter;
				if (num != 0)
				{
					if (num != 1)
					{
						_2007_2056 = 0;
						goto IL_010f;
					}
					awaiter = _2007_2058;
					_2007_2058 = default;
					num = (_2007_2052 = -1);
					goto IL_00f6;
				}
				TaskAwaiter<bool> awaiter2 = _2007_2057;
				_2007_2057 = default;
				num = (_2007_2052 = -1);
				goto IL_0088;
				IL_010f:
				if (_2007_2056 < 60)
				{
					awaiter2 = Framework.RunOnFrameworkThread(() => CS_0024_003C_003E8__locals1.m__2012?.InvokeFunc() ?? false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						num = (_2007_2052 = 0);
						_2007_2057 = awaiter2;
						_2007_2053.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
						return;
					}
					goto IL_0088;
				}
				result = false;
				goto end_IL_000e;
				IL_00f6:
				awaiter.GetResult();
				_2007_2056++;
				goto IL_010f;
				IL_0088:
				if (!awaiter2.GetResult())
				{
					awaiter = Task.Delay(500, _2007_2055).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (_2007_2052 = 1);
						_2007_2058 = awaiter;
						_2007_2053.AwaitUnsafeOnCompleted(ref awaiter, ref this);
						return;
					}
					goto IL_00f6;
				}
				result = true;
				end_IL_000e:;
			}
			catch (Exception exception)
			{
				_2007_2052 = -2;
				_2007_2053.SetException(exception);
				return;
			}
			_2007_2052 = -2;
			_2007_2053.SetResult(result);
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0();
		}

		[DebuggerHidden]
		private void _00A0(IAsyncStateMachine P_0)
		{
			_2007_2053.SetStateMachine(P_0);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine P_0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in  
			this._00A0(P_0);
		}
	}

	public static readonly Vector4 UiColorRun = new Vector4(0.42f, 0.88f, 0.58f, 1f);

	public static readonly Vector4 UiColorNav = new Vector4(0.4f, 0.65f, 1f, 1f);

	public static readonly Vector4 UiColorWait = new Vector4(1f, 0.8f, 0.2f, 1f);

	public static readonly Vector4 UiColorError = new Vector4(0.95f, 0.35f, 0.35f, 1f);

	public static readonly Vector4 UiColorDisabled = new Vector4(0.65f, 0.67f, 0.7f, 1f);

	public static readonly Vector4 UiColorGold = new Vector4(0.86f, 0.71f, 0.31f, 1f);

	[CompilerGenerated]
	private static IPluginLog m__00A0;

	[CompilerGenerated]
	private static ICommandManager m__1680;

	[CompilerGenerated]
	private static IChatGui m__2000;

	[CompilerGenerated]
	private static IClientState m__2001;

	[CompilerGenerated]
	private static IObjectTable m__2002;

	[CompilerGenerated]
	private static IDataManager m__2003;

	[CompilerGenerated]
	private static IDalamudPluginInterface m__2004;

	[CompilerGenerated]
	private static ITargetManager m__2005;

	[CompilerGenerated]
	private static ICondition m__2006;

	[CompilerGenerated]
	private static IFramework m__2007;

	[CompilerGenerated]
	private static IContextMenu m__2008;

	public PluginConfiguration Config;

	[CompilerGenerated]
	private ConfigService m__2009;

	[CompilerGenerated]
	private HudWindow m__200A;

	[CompilerGenerated]
	private SettingsWindow m__200B;

	[CompilerGenerated]
	private OnboardingWindow m__2010;

	[CompilerGenerated]
	private HelpWindow m__2011;

	private ICallGateSubscriber<bool>? m__2012;

	private ICallGateSubscriber<Vector3, bool, bool>? m__2013;

	private ICallGateSubscriber<Vector3, float, float, Vector3?>? m__2014;

	private string m__2015 = string.Empty;

	private uint m__2022;

	private DateTime m__2024 = DateTime.MinValue;

	public Vector3? LastFlagPosition;

	private List<(uint Id, Vector3 Position, string Name)> m__2025 = new List<(uint, Vector3, string)>();

	private _00A0? m__2027;

	private volatile bool m__2028;

	private volatile bool m__2029;

	private bool m__202A;

	private readonly Dictionary<string, _2001> m__202B = new Dictionary<string, _2001>();

	private readonly HashSet<string> m__202C = new HashSet<string>();

	private int m__202D;

	private string? m__202E;

	private int m__202F;

	private string? m__2032;

	private object m__2035 = new object();

	private _00A0? m__2033;

	private string? m__2036;

	private DateTime m__203E = DateTime.MinValue;

	private DateTime m__2047 = DateTime.MinValue;

	private WindowSystem m__2048;

	private HuntUIController m__2049;

	public List<MountInfo> OwnedMounts = new List<MountInfo>();

	public string[] MountNames = Array.Empty<string>();

	public int SelectedMountIndex;

	public string MountSearchText = string.Empty;

	private readonly object m__204A = new object();

	private CancellationTokenSource? m__204B;

	private Task? m__204C;

	private _2003 m__204D;

	private const float m__204E = 50f;

	private bool m__204F;

	public volatile SMonitorStatus? SMonitor;

	private DateTime m__2050 = DateTime.MinValue;

	private static readonly Regex m__2051;

	private object m__2052 = new object();

	private Task? m__2053;

	private CancellationTokenSource? m__2054;

	private bool m__2055;

	private DateTime m__2056 = DateTime.MinValue;

	private uint m__2057;

	private Vector3 m__2058;

	private string? m__2059;

	private uint m__205A;

	private uint m__205B;

	private Vector3 m__205C;

	private (string Server, uint Territory, uint AetheryteId, Vector3 Pos)? m__205D;

	private string? m__205E;

	private DateTime m__205F = DateTime.MinValue;

	private static readonly ConcurrentDictionary<string, List<string>> m__2060;

	private readonly SemaphoreSlim m__2061 = new SemaphoreSlim(1, 1);

	private _2004 m__2062;

	private List<HuntChainStep> m__2063 = new List<HuntChainStep>();

	private int m__2064 = -1;

	private int m__206A;

	private bool m__206B;

	private DateTime m__206C = DateTime.MinValue;

	private DateTime m__206D = DateTime.MinValue;

	private string m__206E = string.Empty;

	private const float m__206F = 100f;

	private const float m__3000 = 3f;

	private const float m__1680_00A0 = 50f;

	private const float m__1680_1680 = 4f;

	private const float m__1680_2000 = 1.5f;

	private const float m__1680_2001 = 5f;

	private const float m__1680_2002 = 3f;

	private const float _1680_2003 = 10f;

	private const float _1680_2004 = 5f;

	private static readonly Dictionary<string, uint> _1680_2005;

	private static readonly object _1680_2006;

	public string Name => "自动狩猎跟车";

	[PluginService]
	internal static IPluginLog Log
	{
		[CompilerGenerated]
		get
		{
			return AutomaticHunting.m__00A0;
		}
		[CompilerGenerated]
		private set
		{
			AutomaticHunting.m__00A0 = pluginLog;
		}
	}

	[PluginService]
	internal static ICommandManager CommandManager
	{
		[CompilerGenerated]
		get
		{
			return AutomaticHunting.m__1680;
		}
		[CompilerGenerated]
		private set
		{
			AutomaticHunting.m__1680 = commandManager;
		}
	}

	[PluginService]
	internal static IChatGui ChatGui
	{
		[CompilerGenerated]
		get
		{
			return AutomaticHunting.m__2000;
		}
		[CompilerGenerated]
		private set
		{
			AutomaticHunting.m__2000 = chatGui;
		}
	}

	[PluginService]
	internal static IClientState ClientState
	{
		[CompilerGenerated]
		get
		{
			return AutomaticHunting.m__2001;
		}
		[CompilerGenerated]
		private set
		{
			AutomaticHunting.m__2001 = clientState;
		}
	}

	[PluginService]
	internal static IObjectTable ObjectTable
	{
		[CompilerGenerated]
		get
		{
			return AutomaticHunting.m__2002;
		}
		[CompilerGenerated]
		private set
		{
			AutomaticHunting.m__2002 = objectTable;
		}
	}

	[PluginService]
	internal static IDataManager DataManager
	{
		[CompilerGenerated]
		get
		{
			return AutomaticHunting.m__2003;
		}
		[CompilerGenerated]
		private set
		{
			AutomaticHunting.m__2003 = dataManager;
		}
	}

	[PluginService]
	internal static IDalamudPluginInterface PluginInterface
	{
		[CompilerGenerated]
		get
		{
			return AutomaticHunting.m__2004;
		}
		[CompilerGenerated]
		private set
		{
			AutomaticHunting.m__2004 = dalamudPluginInterface;
		}
	}

	[PluginService]
	internal static ITargetManager TargetManager
	{
		[CompilerGenerated]
		get
		{
			return AutomaticHunting.m__2005;
		}
		[CompilerGenerated]
		private set
		{
			AutomaticHunting.m__2005 = targetManager;
		}
	}

	[PluginService]
	internal static ICondition Condition
	{
		[CompilerGenerated]
		get
		{
			return AutomaticHunting.m__2006;
		}
		[CompilerGenerated]
		private set
		{
			AutomaticHunting.m__2006 = condition;
		}
	}

	[PluginService]
	internal static IFramework Framework
	{
		[CompilerGenerated]
		get
		{
			return AutomaticHunting.m__2007;
		}
		[CompilerGenerated]
		private set
		{
			AutomaticHunting.m__2007 = framework;
		}
	}

	[PluginService]
	internal static IContextMenu ContextMenu
	{
		[CompilerGenerated]
		get
		{
			return AutomaticHunting.m__2008;
		}
		[CompilerGenerated]
		private set
		{
			AutomaticHunting.m__2008 = contextMenu;
		}
	}

	public ConfigService ConfigService
	{
		[CompilerGenerated]
		get
		{
			return this.m__2009;
		}
		[CompilerGenerated]
		private set
		{
			this.m__2009 = configService;
		}
	}

	public HudWindow HudWindow
	{
		[CompilerGenerated]
		get
		{
			return this.m__200A;
		}
		[CompilerGenerated]
		private set
		{
			this.m__200A = hudWindow;
		}
	}

	public SettingsWindow SettingsWindow
	{
		[CompilerGenerated]
		get
		{
			return this.m__200B;
		}
		[CompilerGenerated]
		private set
		{
			this.m__200B = settingsWindow;
		}
	}

	public OnboardingWindow OnboardingWindow
	{
		[CompilerGenerated]
		get
		{
			return this.m__2010;
		}
		[CompilerGenerated]
		private set
		{
			this.m__2010 = onboardingWindow;
		}
	}

	public HelpWindow HelpWindow
	{
		[CompilerGenerated]
		get
		{
			return this.m__2011;
		}
		[CompilerGenerated]
		private set
		{
			this.m__2011 = helpWindow;
		}
	}

	public bool IsNavigating
	{
		get
		{
			lock (this.m__204A)
			{
				Task task = this.m__204C;
				return task != null && !task.IsCompleted;
			}
		}
	}

	public bool CrossWorldInProgress
	{
		get
		{
			lock (this.m__2052)
			{
				if (this.m__2055 && this.m__2056 != DateTime.MinValue && DateTime.UtcNow - this.m__2056 > TimeSpan.FromSeconds(900L))
				{
					this.m__2055 = false;
					this.m__2056 = DateTime.MinValue;
					this.m__2057 = 0u;
					this.m__2058 = default;
					this.m__2059 = null;
				}
				return this.m__2055;
			}
		}
	}

	public bool IsHuntChainRunning
	{
		get
		{
			_2004 obj = this.m__2062;
			if ((uint)(obj - 1) <= 1u)
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
			if (this.m__2062 == AutomaticHunting._2004._2002_2028 && Config.AutomationEnabled && Config.HuntChain.Enabled && this.m__2064 >= 0)
			{
				return this.m__2064 < this.m__2063.Count;
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
				return this.m__206A > 0;
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
		string text = _1680();
		PrintEcho(string.IsNullOrEmpty(text) ? $"扫描A怪: {100f:0}m内未找到匹配的目标" : ("已选中A怪: " + text));
	}

	private _1680? _00A0()
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
		_1680? result = null;
		float num = 100f;
		foreach (IGameObject item in ObjectTable)
		{
			float num2 = _00A0(localPlayerPosition.Value, item.Position);
			if (!(num2 > num))
			{
				string textValue = item.Name.TextValue;
				if (value.Any((string P_0) => textValue.Contains(P_0)))
				{
					num = num2;
					result = new _1680(textValue, item.Position);
				}
			}
		}
		return result;
	}

	private string? _1680()
	{
		_1680? obj = _00A0();
		if (!obj.HasValue)
		{
			return null;
		}
		IGameObject gameObject = (from P_0 in ObjectTable
			where P_0.Name.TextValue == obj.Value.Name
			orderby _00A0(P_0.Position, obj.Value.Position)
			select P_0).FirstOrDefault();
		if (gameObject == null)
		{
			return null;
		}
		TargetManager.Target = gameObject;
		return obj.Value.Name;
	}

	private void _2000()
	{
		this.m__202B.Clear();
		this.m__202C.Clear();
		this.m__202D = 0;
	}

	private void _2001()
	{
		string currentTerritoryName = GetCurrentTerritoryName();
		if (!string.IsNullOrWhiteSpace(currentTerritoryName) && currentTerritoryName != this.m__202E)
		{
			if (this.m__202E != null)
			{
				_2000();
			}
			this.m__202E = currentTerritoryName;
		}
		if (string.IsNullOrWhiteSpace(currentTerritoryName))
		{
			return;
		}
		int currentInstanceNumber = GetCurrentInstanceNumber();
		if (currentInstanceNumber > 0)
		{
			if (this.m__202F > 0 && currentInstanceNumber != this.m__202F)
			{
				_2000();
				PrintEcho($"检测到分线变化({this.m__202F}→{currentInstanceNumber})，已重置A怪计数");
			}
			this.m__202F = currentInstanceNumber;
		}
		bool flag = Config.ARealmMarks.TryGetValue(currentTerritoryName, out string[] value);
		if (!flag && !Config.DebugCountAInAllMaps)
		{
			return;
		}
		HashSet<string> hashSet = new HashSet<string>();
		foreach (IGameObject item in ObjectTable)
		{
			string textValue = item.Name.TextValue;
			if ((flag && value != null) ? value.Any((string P_0) => textValue.Contains(P_0)) : (item is IBattleChara))
			{
				string text = textValue;
				hashSet.Add(text);
				if (!this.m__202B.TryGetValue(text, out _2001 value2))
				{
					value2 = new _2001
					{
						Name = textValue,
						Position = item.Position
					};
					this.m__202B[text] = value2;
				}
				else
				{
					value2.Position = item.Position;
				}
				if (!value2.HasBeenDamaged && _00A0(item) < 80f)
				{
					value2.HasBeenDamaged = true;
					PrintEcho("A怪已被攻击: " + textValue);
				}
				if (_00A0(item) <= 1f && this.m__202C.Add(textValue))
				{
					this.m__202D++;
					_2029();
					PrintEcho($"已击杀A怪: {textValue} (本图累计:{this.m__202D})");
				}
			}
		}
		if (this.m__202B.Count <= 0)
		{
			return;
		}
		foreach (KeyValuePair<string, _2001> item2 in this.m__202B.Where((KeyValuePair<string, _2001> P_0) => !hashSet.Contains(P_0.Key)).ToList())
		{
			if (item2.Value.HasBeenDamaged && this.m__202C.Add(item2.Value.Name))
			{
				this.m__202D++;
				_2029();
				PrintEcho($"已击杀A怪: {item2.Value.Name} (本图累计:{this.m__202D})");
			}
			this.m__202B.Remove(item2.Key);
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
			Task.Run([AsyncStateMachine(typeof(_2007._00A0))] () =>
			{
				_2007._00A0 stateMachine = default;
				stateMachine._2008_205A = AsyncTaskMethodBuilder.Create();
				stateMachine._2008_2059 = -1;
				stateMachine._2008_205A.Start(ref stateMachine);
				return stateMachine._2008_205A.Task;
			});
		}
		CommandManager.AddHandler("/auht", new CommandInfo(_00A0)
		{
			HelpMessage = "自动狩猎跟车 - /auht help"
		});
		_2004();
		ChatGui.ChatMessage += _00A0;
		Framework.Update += _00A0;
		ClientState.TerritoryChanged += _00A0;
		this.m__2048 = new WindowSystem("AutomaticHunting");
		SettingsWindow = new SettingsWindow(this);
		HudWindow = new HudWindow(this);
		OnboardingWindow = new OnboardingWindow(this);
		HelpWindow = new HelpWindow(this);
		HudWindow.IsOpen = Config.HudVisible;
		this.m__2048.AddWindow(SettingsWindow);
		this.m__2048.AddWindow(HudWindow);
		this.m__2048.AddWindow(OnboardingWindow);
		this.m__2048.AddWindow(HelpWindow);
		PluginInterface.UiBuilder.Draw += this.m__2048.Draw;
		PluginInterface.UiBuilder.OpenConfigUi += _2002;
		PluginInterface.UiBuilder.OpenMainUi += _2003;
		this.m__2049 = new HuntUIController(this);
		this.m__204F = true;
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
		if (this.m__204F)
		{
			ChatGui.ChatMessage -= _00A0;
			Framework.Update -= _00A0;
			ClientState.TerritoryChanged -= _00A0;
			PluginInterface.UiBuilder.Draw -= this.m__2048.Draw;
			PluginInterface.UiBuilder.OpenConfigUi -= _2002;
			PluginInterface.UiBuilder.OpenMainUi -= _2003;
			CommandManager.RemoveHandler("/auht");
			this.m__2049.Dispose();
			StopNavigation();
			ConfigService.Save();
			Theme.PopAll();
			ECommonsMain.Dispose();
			Log.Information("AutomaticHunting: 插件已卸载");
		}
	}

	private void _2002()
	{
		SettingsWindow.IsOpen = true;
	}

	private void _2003()
	{
		HudWindow.IsOpen = true;
	}

	private void _2004()
	{
		try
		{
			this.m__2012 = PluginInterface.GetIpcSubscriber<bool>("vnavmesh.Nav.IsReady");
			this.m__2013 = PluginInterface.GetIpcSubscriber<Vector3, bool, bool>("vnavmesh.SimpleMove.PathfindAndMoveTo");
			this.m__2014 = PluginInterface.GetIpcSubscriber<Vector3, float, float, Vector3?>("vnavmesh.Query.Mesh.NearestPoint");
			Log.Information("AutomaticHunting: vnavmesh IPC initialized");
		}
		catch (Exception exception)
		{
			Log.Warning(exception, "AutomaticHunting: vnavmesh IPC init failed");
		}
	}

	private void _00A0(bool P_0)
	{
		Config.Enabled = P_0;
		ConfigService.Save();
		PrintEcho(Config.Enabled ? "已启用" : "已禁用");
	}

	private void _2005()
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

	private void _00A0(string P_0, string P_1)
	{
		string[] source = P_1.Split(' ', StringSplitOptions.RemoveEmptyEntries);
		switch (source.FirstOrDefault()?.ToLower() ?? string.Empty)
		{
		case "on":
			_00A0(true);
			break;
		case "off":
			_00A0(false);
			break;
		case "leader":
			_1680(P_0, string.Join(" ", source.Skip(1)));
			break;
		case "stop":
			if (_2006())
			{
				StopNavigation();
				PrintEcho("导航已停止");
			}
			break;
		case "overlay":
			_2005();
			break;
		case "status":
			_2008();
			break;
		case "mount":
			if (_2006())
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
			_00A0(source.Skip(1).FirstOrDefault());
			break;
		case "hud":
			_00A0(source.Skip(1).ToArray());
			break;
		default:
			_2007();
			break;
		}
	}

	private void _00A0(string? P_0)
	{
		if (string.IsNullOrWhiteSpace(P_0) || !float.TryParse(P_0, NumberStyles.Float, CultureInfo.InvariantCulture, out var result))
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

	private void _00A0(string[] P_0)
	{
		switch ((P_0.Length != 0) ? P_0[0].ToLowerInvariant() : string.Empty)
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

	private bool _2006()
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

	private void _1680(string P_0, string P_1)
	{
		string text = P_1.Split(' ', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault()?.ToLower() ?? string.Empty;
		if (!(text == "target"))
		{
			if (text == "me")
			{
				SetLeaderToSelf();
			}
			else if (!string.IsNullOrWhiteSpace(P_1))
			{
				string text2 = P_1.Trim();
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

	private void _2007()
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

	private void _2008()
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
			InlineArray5<string> buffer = default;
			global::_00A0._1680._1680<InlineArray5<string>, string>(ref buffer, 0) = directoryName;
			global::_00A0._1680._1680<InlineArray5<string>, string>(ref buffer, 1) = "..";
			global::_00A0._1680._1680<InlineArray5<string>, string>(ref buffer, 2) = "..";
			global::_00A0._1680._1680<InlineArray5<string>, string>(ref buffer, 3) = "..";
			global::_00A0._1680._1680<InlineArray5<string>, string>(ref buffer, 4) = "使用说明.md";
			list.Add(Path.Combine(global::_00A0._1680._00A0<InlineArray5<string>, string>(in buffer, 5)));
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

	private void _00A0(IFramework P_0)
	{
		ConfigService.Tick();
		_2028();
		if (!Config.AutomationEnabled)
		{
			return;
		}
		_2001();
		bool flag = Condition[ConditionFlag.InCombat];
		if (!flag && this.m__2027.HasValue && !IsNavigating)
		{
			_00A0 value = this.m__2027.Value;
			this.m__2027 = null;
			PrintEcho(this.m__2029 ? "S怪监控结束，开始执行延迟的Flag" : "战斗结束，开始执行延迟的导航");
			this.m__2029 = false;
			_1680(value.TerritoryType, value.Position);
		}
		if (!flag && this.m__205D.HasValue && !CrossWorldInProgress)
		{
			(string, uint, uint, Vector3) value2 = this.m__205D.Value;
			this.m__205D = null;
			PrintEcho("战斗结束，开始执行一键跨服");
			StartManualWorldTravel(value2.Item1, value2.Item2, value2.Item3, value2.Item4);
		}
		if (Config.AutoBmrAi && _2022())
		{
			if (flag && !this.m__202A)
			{
				CommandManager.ProcessCommand("/bmrai on");
				PrintEcho("BMR AI 已开启");
			}
			else if (!flag && this.m__202A)
			{
				CommandManager.ProcessCommand("/bmrai off");
				PrintEcho("BMR AI 已关闭");
			}
			this.m__202A = flag;
		}
	}

	private MultiLineMapConfig? _2009()
	{
		uint num = _2012();
		if (num == 0)
		{
			return null;
		}
		return Config.MultiLineMaps.FirstOrDefault((MultiLineMapConfig P_0) => !string.IsNullOrWhiteSpace(P_0.MapId) && uint.TryParse(P_0.MapId.Trim(), out var result) && result == num);
	}

	private void _00A0(uint P_0)
	{
		_1680(P_0);
	}

	private void _1680(uint P_0)
	{
		if (P_0 == 0 || Config.MultiLineMaps.Count == 0)
		{
			return;
		}
		if (!_2000(P_0))
		{
			PrintEcho($"多线检查: 当前区域未配置(terr={P_0}, 配置数={Config.MultiLineMaps.Count})");
			return;
		}
		DateTime utcNow = DateTime.UtcNow;
		if (utcNow - this.m__2047 < TimeSpan.FromSeconds(8L))
		{
			PrintEcho("多线检查: 防抖中，跳过重复切线");
			return;
		}
		this.m__2047 = utcNow;
		_2000();
		int currentInstanceNumber = GetCurrentInstanceNumber();
		if (currentInstanceNumber > 0 && currentInstanceNumber == 1)
		{
			Config.CurrentInstanceNumber = 1;
			_202E();
			PrintEcho($"进入多线地图(terr={P_0}): 已在1线，无需切线");
		}
		else
		{
			PrintEcho($"进入多线地图(terr={P_0}): 等待稳定后切换到1线开始打A怪");
			_00A0(1);
		}
	}

	[AsyncStateMachine(typeof(_3000))]
	private Task _00A0(int P_0)
	{
		_3000 stateMachine = default;
		stateMachine._2007_2008 = AsyncTaskMethodBuilder.Create();
		stateMachine._2007_2009 = this;
		stateMachine._2007_200A = P_0;
		stateMachine._2007_2007 = -1;
		stateMachine._2007_2008.Start(ref stateMachine);
		return stateMachine._2007_2008.Task;
	}

	private bool _2000(uint P_0)
	{
		return Config.MultiLineMaps.Any((MultiLineMapConfig multiLineMapConfig) => !string.IsNullOrWhiteSpace(multiLineMapConfig.MapId) && uint.TryParse(multiLineMapConfig.MapId.Trim(), out var result) && result == P_0);
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

	public void SwitchToInstance(int instanceNumber)
	{
		int num = Math.Clamp(instanceNumber, 1, 10);
		int currentInstanceNumber = GetCurrentInstanceNumber();
		if (currentInstanceNumber > 0 && currentInstanceNumber == num)
		{
			Config.CurrentInstanceNumber = num;
			_202E();
			PrintEcho($"已在 {num} 线，无需切线");
			return;
		}
		Config.CurrentInstanceNumber = num;
		_202E();
		string text = $"/pdr insc {num}";
		Framework.RunOnFrameworkThread(() =>
		{
			try
			{
				CommandManager.ProcessCommand(text);
				Log.Information("AutomaticHunting: 执行切线命令 " + text);
			}
			catch (Exception exception)
			{
				PrintEcho("切线命令执行失败: " + text);
				Log.Warning(exception, "AutomaticHunting: 切线命令失败 " + text);
			}
		});
		PrintEcho("正在切换副本线: " + text);
		_1680(num);
	}

	[AsyncStateMachine(typeof(_2054))]
	private Task _1680(int P_0)
	{
		_2054 stateMachine = default;
		stateMachine._2003_2062 = AsyncTaskMethodBuilder.Create();
		stateMachine._2003_2064 = this;
		stateMachine._2003_2063 = P_0;
		stateMachine._2003_2061 = -1;
		stateMachine._2003_2062.Start(ref stateMachine);
		return stateMachine._2003_2062.Task;
	}

	[AsyncStateMachine(typeof(_2061))]
	private Task<bool> _00A0(int P_0, TimeSpan P_1)
	{
		_2061 stateMachine = default;
		stateMachine._2005_2049 = AsyncTaskMethodBuilder<bool>.Create();
		stateMachine._2005_204A = this;
		stateMachine._2005_204B = P_0;
		stateMachine._2005_204C = P_1;
		stateMachine._2005_2048 = -1;
		stateMachine._2005_2049.Start(ref stateMachine);
		return stateMachine._2005_2049.Task;
	}

	private void _2000(int P_0)
	{
		Config.CurrentInstanceNumber = P_0;
		_202E();
		PrintEcho($"切线完成，已确认在 {P_0} 线");
		_2001(P_0);
	}

	private void _2001(int P_0)
	{
		_00A0 value;
		lock (this.m__2035)
		{
			if (!this.m__2033.HasValue)
			{
				return;
			}
			value = this.m__2033.Value;
			this.m__2033 = null;
			this.m__2036 = null;
			this.m__203E = DateTime.MinValue;
		}
		PrintEcho($"真实线已确认在 {P_0} 线，开始导航到Flag");
		_1680(value.TerritoryType, value.Position);
	}

	[AsyncStateMachine(typeof(_1680_00A0))]
	private Task _200A()
	{
		_1680_00A0 stateMachine = default;
		stateMachine._2007_2014 = AsyncTaskMethodBuilder.Create();
		stateMachine._2007_2022 = this;
		stateMachine._2007_2013 = -1;
		stateMachine._2007_2014.Start(ref stateMachine);
		return stateMachine._2007_2014.Task;
	}

	public bool ShouldSwitchLineOnSameMap(uint territoryType)
	{
		MultiLineMapConfig multiLineMapConfig = _2009();
		if (multiLineMapConfig == null)
		{
			return false;
		}
		return this.m__202D >= multiLineMapConfig.AMobCount;
	}

	public void ResetInstanceKillCount()
	{
		_2000();
	}

	public string GetNavigationStageText()
	{
		lock (this.m__204A)
		{
			return this.m__204D switch
			{
				AutomaticHunting._2003._2002_2013 => "等待传送", 
				AutomaticHunting._2003._2002_2014 => "召唤坐骑", 
				AutomaticHunting._2003._2002_2015 => "前往Flag", 
				AutomaticHunting._2003._2002_2022 => "前往A怪", 
				AutomaticHunting._2003._2002_2024 => "前往S怪", 
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
		_1680? obj = _00A0();
		if (obj.HasValue)
		{
			float value = _00A0(localPlayerPosition.Value, obj.Value.Position);
			return (Label: "★ A怪 · " + obj.Value.Name, Color: UiColorGold, Distance: value);
		}
		if (LastFlagPosition.HasValue)
		{
			float value2 = _00A0(localPlayerPosition.Value, LastFlagPosition.Value);
			return (Label: "▸ Flag 目标", Color: UiColorNav, Distance: value2);
		}
		return (Label: "等待目标", Color: UiColorDisabled, Distance: null);
	}

	public void BeginSMonitor(string mobName, string phase, float hpPercent, float threshold)
	{
		SMonitor = new SMonitorStatus
		{
			MobName = mobName,
			Phase = phase,
			HpPercent = hpPercent,
			Threshold = threshold
		};
	}

	public void FinishSMonitor(string reason)
	{
		_2011 CS_0024_003C_003E8__locals7 = new _2011();
		CS_0024_003C_003E8__locals7._2002_205C = this;
		CS_0024_003C_003E8__locals7._2002_205D = SMonitor;
		if (CS_0024_003C_003E8__locals7._2002_205D != null && !CS_0024_003C_003E8__locals7._2002_205D.Finished)
		{
			CS_0024_003C_003E8__locals7._2002_205D.Finished = true;
			CS_0024_003C_003E8__locals7._2002_205D.Phase = reason;
			Task.Run([AsyncStateMachine(typeof(_2011._00A0))] () =>
			{
				_2011._00A0 stateMachine = default;
				stateMachine._2008_205D = AsyncTaskMethodBuilder.Create();
				stateMachine._2008_205E = CS_0024_003C_003E8__locals7;
				stateMachine._2008_205C = -1;
				stateMachine._2008_205D.Start(ref stateMachine);
				return stateMachine._2008_205D.Task;
			});
		}
	}

	public static Vector4 GetSMonitorColor(SMonitorStatus s)
	{
		if (s.Finished)
		{
			return UiColorDisabled;
		}
		if (s.Phase.Contains("开怪"))
		{
			return UiColorError;
		}
		if (s.TargetLost)
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
		return _00A0(localPlayerPosition.Value, leaderPosition.Value);
	}

	public bool IsBmrLoaded()
	{
		if (IsPluginLoaded("BossModReborn"))
		{
			return true;
		}
		return PluginInterface.InstalledPlugins.Any((IExposedPlugin P_0) => P_0.IsLoaded && (P_0.InternalName.Equals("BossModReborn", StringComparison.OrdinalIgnoreCase) || P_0.InternalName.Equals("BossMod", StringComparison.OrdinalIgnoreCase)));
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
			list.Sort((MountInfo P_0, MountInfo P_1) => string.Compare(P_0.Name, P_1.Name, StringComparison.OrdinalIgnoreCase));
			OwnedMounts = list;
			List<string> list2 = new List<string> { "随机飞行坐骑" };
			list2.AddRange(list.Select((MountInfo P_0) => P_0.Name));
			MountNames = list2.ToArray();
			if (Config.SelectedMountId == 0)
			{
				SelectedMountIndex = 0;
			}
			else
			{
				int num = list.FindIndex((MountInfo P_0) => P_0.RowId == Config.SelectedMountId);
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

	private void _00A0(IHandleableChatMessage P_0)
	{
		try
		{
			_2002(P_0);
			_2003(P_0);
		}
		catch (Exception exception)
		{
			Log.Warning(exception, "AutomaticHunting: chat message handler failed");
		}
	}

	private static string _1680(IHandleableChatMessage P_0)
	{
		string text = P_0.OriginalMessage.ToString();
		if (!string.IsNullOrWhiteSpace(text))
		{
			return text;
		}
		return P_0.Message.TextValue;
	}

	private static string _2000(IHandleableChatMessage P_0)
	{
		StringBuilder stringBuilder = new StringBuilder();
		foreach (Payload payload in P_0.Message.Payloads)
		{
			if (!(payload is MapLinkPayload) && payload is ITextProvider textProvider)
			{
				stringBuilder.Append(textProvider.Text);
			}
		}
		return stringBuilder.ToString();
	}

	private static string? _2001(IHandleableChatMessage P_0)
	{
		foreach (Payload payload in P_0.Sender.Payloads)
		{
			if (payload is PlayerPayload playerPayload && !string.IsNullOrWhiteSpace(playerPayload.PlayerName))
			{
				return playerPayload.PlayerName;
			}
		}
		string text = P_0.OriginalSender.ToString();
		if (!string.IsNullOrWhiteSpace(text))
		{
			return text;
		}
		return null;
	}

	private static bool _00A0(IHandleableChatMessage P_0, out MapLinkPayload P_1)
	{
		foreach (Payload payload in P_0.Message.Payloads)
		{
			if (payload is MapLinkPayload mapLinkPayload)
			{
				P_1 = mapLinkPayload;
				return true;
			}
		}
		P_1 = null;
		return false;
	}

	private void _2002(IHandleableChatMessage P_0)
	{
		string text = _1680(P_0);
		if (string.IsNullOrWhiteSpace(text) || !text.Contains("当前所在副本区为"))
		{
			return;
		}
		string text2 = _1680(text);
		if (!string.IsNullOrWhiteSpace(text2))
		{
			this.m__2032 = text2;
			Config.CurrentInstanceFingerprint = text2;
			PrintEcho($"副本区指纹: '{text2}' 当前目标线:{Config.CurrentInstanceNumber}");
			string text3 = this.m__2036;
			_00A0 obj2;
			if (this.m__2033.HasValue && !string.IsNullOrEmpty(text3) && text3 != text2 && _00A0(out var obj))
			{
				PrintEcho($"副本区已切换到位(指纹变化 '{text3}'→'{text2}')，开始导航到Flag");
				_1680(obj.TerritoryType, obj.Position);
			}
			else if (this.m__2033.HasValue && this.m__203E != DateTime.MinValue && DateTime.UtcNow - this.m__203E > TimeSpan.FromSeconds(15L) && _00A0(out obj2))
			{
				PrintEcho("切换副本线确认超时(15s)，改按当前指纹 '" + text2 + "' 导航到Flag");
				_1680(obj2.TerritoryType, obj2.Position);
			}
		}
	}

	private bool _00A0(out _00A0 P_0)
	{
		lock (this.m__2035)
		{
			if (!this.m__2033.HasValue)
			{
				P_0 = default;
				return false;
			}
			P_0 = this.m__2033.Value;
			this.m__2033 = null;
			this.m__2036 = null;
			this.m__203E = DateTime.MinValue;
			return true;
		}
	}

	private static string? _1680(string P_0)
	{
		int num = P_0.IndexOf('"');
		int num2 = P_0.LastIndexOf('"');
		if (num < 0 || num2 <= num + 1)
		{
			return null;
		}
		int num3 = num + 1;
		string text = P_0.Substring(num3, num2 - num3);
		if (text.Length < 2)
		{
			return null;
		}
		return text[text.Length - 1].ToString();
	}

	private void _2003(IHandleableChatMessage P_0)
	{
		string text = _2001(P_0);
		if (_00A0(P_0, text))
		{
			return;
		}
		if (CrossWorldInProgress)
		{
			if (DateTime.UtcNow - this.m__2050 > TimeSpan.FromSeconds(3L))
			{
				this.m__2050 = DateTime.UtcNow;
				PrintEcho("跨服流程进行中，忽略普通车头指令");
			}
		}
		else
		{
			if (!Config.AutomationEnabled || string.IsNullOrWhiteSpace(Config.LeaderName))
			{
				return;
			}
			if (!_2000(text, Config.LeaderName))
			{
				Log.Debug($"AutomaticHunting: 车头消息发送者不匹配: sender='{text}' (norm='{_2000(text ?? "")}') vs leader='{Config.LeaderName}' (norm='{_2000(Config.LeaderName)}')");
				return;
			}
			if (Config.LeaderYellOnly && P_0.LogKind != XivChatType.Shout)
			{
				Log.Debug($"AutomaticHunting: 忽略非喊话频道车头消息 kind={P_0.LogKind}");
				return;
			}
			uint rowId;
			uint rowId2;
			Vector3 vector;
			string text2;
			if (_00A0(P_0, out MapLinkPayload mapLinkPayload))
			{
				rowId = mapLinkPayload.TerritoryType.RowId;
				rowId2 = mapLinkPayload.Map.RowId;
				vector = new Vector3((float)mapLinkPayload.RawX / 1000f, 0f, (float)mapLinkPayload.RawY / 1000f);
				text2 = $"{rowId}|{rowId2}|{mapLinkPayload.RawX / 1000}|{mapLinkPayload.RawY / 1000}";
			}
			else
			{
				if (!_00A0(P_0, out rowId, out rowId2, out vector))
				{
					return;
				}
				text2 = $"{rowId}|{rowId2}|{(int)vector.X}|{(int)vector.Z}";
			}
			bool flag = DateTime.UtcNow - this.m__2024 < TimeSpan.FromSeconds(60L);
			if ((text2 == this.m__2015) & flag)
			{
				PrintEcho("忽略60秒内重复Flag");
				return;
			}
			this.m__2015 = text2;
			this.m__2024 = DateTime.UtcNow;
			if (flag && LastFlagPosition.HasValue)
			{
				float num = Vector2.Distance(new Vector2(LastFlagPosition.Value.X, LastFlagPosition.Value.Z), new Vector2(vector.X, vector.Z));
				if (num < 50f)
				{
					PrintEcho($"忽略附近重复Flag（距上次{num:0.0}m）");
					return;
				}
			}
			if (Condition[ConditionFlag.InCombat] && LastFlagPosition.HasValue)
			{
				float num2 = Vector2.Distance(new Vector2(LastFlagPosition.Value.X, LastFlagPosition.Value.Z), new Vector2(vector.X, vector.Z));
				if (num2 < 50f)
				{
					PrintEcho($"疑似误触坐标（距当前{num2:0.0}m），不执行");
					return;
				}
			}
			LastFlagPosition = vector;
			this.m__2022 = rowId;
			this.m__2025 = _2002(rowId);
			if (Config.SetFlagOnNavigation && !_1680(rowId, rowId2, vector))
			{
				PrintEcho("设置地图Flag失败");
			}
			_00A0(rowId, rowId2, vector);
		}
	}

	private bool _00A0(IHandleableChatMessage P_0, out uint P_1, out uint P_2, out Vector3 P_3)
	{
		P_1 = 0u;
		P_2 = 0u;
		P_3 = default;
		string text = _1680(P_0);
		if (string.IsNullOrWhiteSpace(text))
		{
			return false;
		}
		Match match = AutomaticHunting.m__2051.Match(text);
		if (!match.Success)
		{
			return false;
		}
		float num = (float)int.Parse(match.Groups[1].Value) + (float)int.Parse(match.Groups[2].Value) / 10f;
		float num2 = (float)int.Parse(match.Groups[3].Value) + (float)int.Parse(match.Groups[4].Value) / 10f;
		Lumina.Excel.Sheets.Map? map = _2025();
		if (!map.HasValue)
		{
			return false;
		}
		Vector2 vector = _00A0(num, num2, map.Value);
		P_1 = ClientState.TerritoryType;
		P_2 = map.Value.RowId;
		P_3 = new Vector3(vector.X, 0f, vector.Y);
		Log.Debug($"AutomaticHunting: 文本坐标解析 ({num:0.0},{num2:0.0}) → 世界 ({vector.X:0.0},{vector.Y:0.0})");
		return true;
	}

	private bool _00A0(IHandleableChatMessage P_0, string? senderName)
	{
		if (!FeatureFlags.CrossWorldLeaderEnabled)
		{
			return false;
		}
		if (!Config.AutomationEnabled || string.IsNullOrWhiteSpace(Config.CrossWorldLeader))
		{
			return false;
		}
		if (string.IsNullOrWhiteSpace(senderName))
		{
			return false;
		}
		if (!string.Equals(_2000(senderName), _2000(Config.CrossWorldLeader), StringComparison.OrdinalIgnoreCase))
		{
			return false;
		}
		string text = _1680(P_0);
		string text2 = _2000(P_0);
		Vector3? vector = null;
		uint num = 0u;
		if (_00A0(P_0, out MapLinkPayload mapLinkPayload))
		{
			num = mapLinkPayload.TerritoryType.RowId;
			vector = new Vector3((float)mapLinkPayload.RawX / 1000f, 0f, (float)mapLinkPayload.RawY / 1000f);
		}
		_00A0(senderName, text, text2, vector, num);
		return true;
	}

	private void _00A0(uint P_0, uint P_1, Vector3 P_2)
	{
		if (P_0 == _2012() && ShouldSwitchLineOnSameMap(P_0))
		{
			ResetInstanceKillCount();
			_00A0(P_0, P_2);
		}
		else
		{
			_1680(P_0, P_2);
		}
	}

	private void _00A0(uint P_0, Vector3 P_1)
	{
		this.m__2036 = Config.CurrentInstanceFingerprint;
		this.m__2033 = new _00A0(P_0, P_1);
		this.m__203E = DateTime.UtcNow;
		SwitchToNextInstance();
		PrintEcho($"{Config.CurrentInstanceNumber}线A怪击杀达标，已切换，等待副本区确认后导航");
	}

	private static bool _2000(string? sender, string P_1)
	{
		if (string.IsNullOrWhiteSpace(sender))
		{
			return false;
		}
		string text = _2000(sender);
		if (text.Length == 0)
		{
			return false;
		}
		string[] array = _2000(P_1).Split(' ', StringSplitOptions.RemoveEmptyEntries);
		foreach (string text2 in array)
		{
			if (text2.Length != 0 && string.Equals(text, text2, StringComparison.OrdinalIgnoreCase))
			{
				return true;
			}
		}
		return false;
	}

	private static string _2000(string P_0)
	{
		if (string.IsNullOrEmpty(P_0))
		{
			return string.Empty;
		}
		int num = P_0.IndexOf('@');
		if (num >= 0)
		{
			P_0 = P_0.Substring(0, num);
		}
		StringBuilder stringBuilder = new StringBuilder(P_0.Length);
		string text = P_0;
		foreach (char c in text)
		{
			if (!char.IsControl(c) && ((c != '\u00a0' && c != '\u3000') || 1 == 0))
			{
				stringBuilder.Append(c);
			}
		}
		return stringBuilder.ToString().Trim().TrimEnd(new char[2] { '：', ':' });
	}

	private void _2001(string P_0)
	{
		lock (this.m__2052)
		{
			this.m__205E = P_0;
			this.m__205F = DateTime.UtcNow;
		}
	}

	private static string _2001(string P_0, string P_1)
	{
		if (string.IsNullOrEmpty(P_0))
		{
			return P_0;
		}
		foreach (string item2 in AutomaticHunting.m__2060.GetOrAdd(P_1, (string text2) =>
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
			if (P_0.Contains(item2))
			{
				P_0 = P_0.Replace(item2, " ");
			}
		}
		return P_0;
	}

	private void _00A0(string P_0, string P_1, string P_2, Vector3? P_3, uint P_4)
	{
		if (!FeatureFlags.CrossWorldLeaderEnabled || !Config.AutomationEnabled || string.IsNullOrWhiteSpace(Config.CrossWorldLeader) || !string.Equals(_2000(P_0), _2000(Config.CrossWorldLeader), StringComparison.OrdinalIgnoreCase))
		{
			return;
		}
		bool flag = false;
		foreach (KeyValuePair<string, string> item in CrossWorldData.ServerWorldTravel)
		{
			if (_2001(P_2, item.Key).Contains(item.Key) && !flag)
			{
				_00A0(item.Key, item.Value, false);
				flag = true;
			}
		}
		foreach (KeyValuePair<string, string> item2 in CrossWorldData.InstanceSwitch)
		{
			if (P_1.Contains(item2.Key))
			{
				lock (this.m__2052)
				{
					this.m__2059 = item2.Value;
				}
				break;
			}
		}
		if (!P_3.HasValue || P_4 == 0)
		{
			return;
		}
		lock (this.m__2052)
		{
			this.m__2057 = P_4;
			this.m__2058 = P_3.Value;
		}
	}

	private void _00A0(string P_0, string P_1, bool P_2 = false)
	{
		lock (this.m__2052)
		{
			if (this.m__2055)
			{
				PrintEcho("跨服流程正在进行中，忽略新的跨服指令");
				return;
			}
			if (!P_2 && string.Equals(P_0, this.m__205E, StringComparison.OrdinalIgnoreCase) && DateTime.UtcNow - this.m__205F < TimeSpan.FromSeconds(60L))
			{
				Log.Debug("AutomaticHunting: 已在服务器[" + P_0 + "]（60秒冷却内），忽略重复跨服指令");
				return;
			}
			this.m__2055 = true;
			this.m__2056 = DateTime.UtcNow;
		}
		StopNavigation();
		ResetInstanceKillCount();
		uint num = _2003(P_0);
		this.m__2054 = new CancellationTokenSource();
		this.m__2053 = Task.Run(() => _00A0(P_0, num, P_1, this.m__2054.Token));
	}

	public void StartManualWorldTravel(string serverName, uint territory, uint aetheryteId, Vector3 aetherytePos, bool fromHuntChain = false)
	{
		if (!fromHuntChain && IsHuntChainRunning)
		{
			_2027();
			PrintEcho("连车: 检测到手动一键跨服，连车已停止");
		}
		if (Condition[ConditionFlag.InCombat])
		{
			this.m__205D = (serverName, territory, aetheryteId, aetherytePos);
			PrintEcho("战斗中，已保存一键跨服，战斗结束后执行");
			return;
		}
		if (CrossWorldInProgress)
		{
			PrintEcho("跨服流程正在进行中，忽略一键跨服");
			return;
		}
		if (!CrossWorldData.ServerWorldTravel.TryGetValue(serverName, out string value))
		{
			PrintEcho("服务器[" + serverName + "]无跨服命令，无法跨服");
			return;
		}
		lock (this.m__2052)
		{
			if (this.m__2055)
			{
				PrintEcho("跨服流程正在进行中，忽略一键跨服");
				return;
			}
			this.m__205A = territory;
			this.m__205B = aetheryteId;
			this.m__205C = aetherytePos;
			_00A0(serverName, value, true);
		}
	}

	public void StopCrossWorld()
	{
		lock (this.m__2052)
		{
			this.m__2054?.Cancel();
			this.m__2054?.Dispose();
			this.m__2054 = null;
			this.m__2055 = false;
			this.m__2056 = DateTime.MinValue;
			this.m__2057 = 0u;
			this.m__2058 = default;
			this.m__2059 = null;
			this.m__2053 = null;
		}
		_2027();
		StopNavigation();
		PrintEcho("已停止跨服流程，恢复普通车头识别");
	}

	[AsyncStateMachine(typeof(_2064))]
	private Task _00A0(string P_0, uint P_1, string P_2, CancellationToken P_3)
	{
		_2064 stateMachine = default;
		stateMachine._2005_206A = AsyncTaskMethodBuilder.Create();
		stateMachine._2005_206C = this;
		stateMachine._2005_206D = P_0;
		stateMachine._2005_206B = P_1;
		stateMachine._2005_206F = P_2;
		stateMachine._2005_206E = P_3;
		stateMachine._2005_2064 = -1;
		stateMachine._2005_206A.Start(ref stateMachine);
		return stateMachine._2005_206A.Task;
	}

	[AsyncStateMachine(typeof(_2057))]
	private Task _00A0(CancellationToken P_0)
	{
		_2057 stateMachine = default;
		stateMachine._2004_200A = AsyncTaskMethodBuilder.Create();
		stateMachine._2004_200B = this;
		stateMachine._2004_2010 = P_0;
		stateMachine._2004_2009 = -1;
		stateMachine._2004_200A.Start(ref stateMachine);
		return stateMachine._2004_200A.Task;
	}

	[AsyncStateMachine(typeof(_205B))]
	private Task _00A0(uint P_0, uint P_1, Vector3 P_2, CancellationToken P_3)
	{
		_205B stateMachine = default;
		stateMachine._2004_2035 = AsyncTaskMethodBuilder.Create();
		stateMachine._2004_203E = this;
		stateMachine._2004_2033 = P_0;
		stateMachine._2004_2047 = P_1;
		stateMachine._2004_2049 = P_2;
		stateMachine._2004_2036 = P_3;
		stateMachine._2004_2032 = -1;
		stateMachine._2004_2035.Start(ref stateMachine);
		return stateMachine._2004_2035.Task;
	}

	[AsyncStateMachine(typeof(_2062))]
	private Task _00A0(uint P_0, Vector3 P_1, string? instanceCommand)
	{
		_2062 stateMachine = default;
		stateMachine._2005_2051 = AsyncTaskMethodBuilder.Create();
		stateMachine._2005_2054 = this;
		stateMachine._2005_2052 = P_0;
		stateMachine._2005_2053 = P_1;
		stateMachine._2005_2055 = instanceCommand;
		stateMachine._2005_2050 = -1;
		stateMachine._2005_2051.Start(ref stateMachine);
		return stateMachine._2005_2051.Task;
	}

	[AsyncStateMachine(typeof(_1680_2000))]
	private Task _00A0(TimeSpan P_0)
	{
		_1680_2000 stateMachine = default;
		stateMachine._2007_2035 = AsyncTaskMethodBuilder.Create();
		stateMachine._2007_2033 = P_0;
		stateMachine._2007_2032 = -1;
		stateMachine._2007_2035.Start(ref stateMachine);
		return stateMachine._2007_2035.Task;
	}

	private Task _2002(string P_0)
	{
		return Framework.RunOnFrameworkThread(() =>
		{
			try
			{
				CommandManager.ProcessCommand(P_0);
			}
			catch (Exception exception)
			{
				Log.Warning(exception, "AutomaticHunting: 执行跨服命令失败 " + P_0);
			}
		});
	}

	[AsyncStateMachine(typeof(_2059))]
	private Task<uint> _200B()
	{
		_2059 stateMachine = default;
		stateMachine._2004_2028 = AsyncTaskMethodBuilder<uint>.Create();
		stateMachine._2004_2027 = -1;
		stateMachine._2004_2028.Start(ref stateMachine);
		return stateMachine._2004_2028.Task;
	}

	[AsyncStateMachine(typeof(_2058))]
	private Task<string> _2010()
	{
		_2058 stateMachine = default;
		stateMachine._2004_2024 = AsyncTaskMethodBuilder<string>.Create();
		stateMachine._2004_2022 = -1;
		stateMachine._2004_2024.Start(ref stateMachine);
		return stateMachine._2004_2024.Task;
	}

	private uint _2003(string P_0)
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
				if (!(item.Name.ExtractText() != P_0))
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
			Log.Warning(exception, "AutomaticHunting: 查询服务器[" + P_0 + "]失败");
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

	private unsafe uint _2011()
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

	private Vector3 _00A0(Vector3 P_0)
	{
		uint num = _2011();
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
		string[] array = _2000(Config.LeaderName).Split(' ', StringSplitOptions.RemoveEmptyEntries);
		foreach (string b in array)
		{
			foreach (IGameObject item in ObjectTable)
			{
				if (item?.Name != null && string.Equals(_2000(item.Name.TextValue), b, StringComparison.OrdinalIgnoreCase))
				{
					return item.Position;
				}
			}
		}
		return null;
	}

	private static uint _2012()
	{
		return ClientState.TerritoryType;
	}

	public static string GetCurrentTerritoryName()
	{
		uint num = _2012();
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

	private Vector3? _1680(Vector3 P_0)
	{
		if (this.m__2014 == null)
		{
			return null;
		}
		try
		{
			return this.m__2014.InvokeFunc(P_0, 120f, 300f) ?? this.m__2014.InvokeFunc(P_0, 180f, 600f) ?? this.m__2014.InvokeFunc(P_0, 260f, 1000f);
		}
		catch (Exception exception)
		{
			Log.Warning(exception, "AutomaticHunting: Failed to snap position to navmesh");
			return null;
		}
	}

	private unsafe bool _2001(uint P_0)
	{
		Telepo* ptr = Telepo.Instance();
		if (ptr != null)
		{
			return ptr->Teleport(P_0, 0);
		}
		return false;
	}

	private unsafe static bool _2013()
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

	private unsafe static bool _2014()
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

	private bool _2015()
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

	[AsyncStateMachine(typeof(_1680_1680))]
	private Task<bool> _00A0(uint P_0, CancellationToken P_1)
	{
		_1680_1680 stateMachine = default;
		stateMachine._2007_202A = AsyncTaskMethodBuilder<bool>.Create();
		stateMachine._2007_202B = this;
		stateMachine._2007_202D = P_0;
		stateMachine._2007_202C = P_1;
		stateMachine._2007_2029 = -1;
		stateMachine._2007_202A.Start(ref stateMachine);
		return stateMachine._2007_202A.Task;
	}

	[AsyncStateMachine(typeof(_206D))]
	private Task<bool> _1680(uint P_0, CancellationToken P_1)
	{
		_206D stateMachine = default;
		stateMachine._2006_205E = AsyncTaskMethodBuilder<bool>.Create();
		stateMachine._2006_205F = this;
		stateMachine._2006_2061 = P_0;
		stateMachine._2006_2060 = P_1;
		stateMachine._2006_205D = -1;
		stateMachine._2006_205E.Start(ref stateMachine);
		return stateMachine._2006_205E.Task;
	}

	private bool _00A0(uint P_0, uint P_1, float P_2, float P_3, out Vector3 P_4)
	{
		P_4 = default;
		if (P_0 == 0)
		{
			return false;
		}
		ExcelSheet<Lumina.Excel.Sheets.Map> excelSheet = DataManager.GetExcelSheet<Lumina.Excel.Sheets.Map>();
		if (excelSheet == null)
		{
			return false;
		}
		if (!excelSheet.TryGetRow(P_1, out var row) || row.SizeFactor <= 0)
		{
			row = excelSheet.FirstOrDefault((Lumina.Excel.Sheets.Map map) => map.TerritoryType.RowId == P_0);
			if (row.RowId == 0 || row.SizeFactor <= 0)
			{
				return false;
			}
		}
		P_4 = new Vector3(50f * P_2 - (float)row.OffsetX - 102400f / (float)(int)row.SizeFactor - 50f, 0f, 50f * P_3 - (float)row.OffsetY - 102400f / (float)(int)row.SizeFactor - 50f);
		return true;
	}

	private unsafe static bool _1680(uint P_0, uint P_1, Vector3 P_2)
	{
		try
		{
			AgentMap* ptr = AgentMap.Instance();
			if (ptr == null)
			{
				return false;
			}
			ptr->SetFlagMapMarker(P_0, P_1, P_2);
			return true;
		}
		catch (Exception exception)
		{
			Log.Warning(exception, "AutomaticHunting: Failed to set map flag");
			return false;
		}
	}

	internal static List<(uint Id, Vector3 Position, string Name)> _2002(uint P_0)
	{
		List<(uint, Vector3, string)> list = new List<(uint, Vector3, string)>();
		foreach (Aetheryte item3 in DataManager.GetExcelSheet<Aetheryte>())
		{
			if (item3.IsAetheryte && item3.Territory.RowId == P_0 && _00A0(item3, out var item))
			{
				string item2 = item3.PlaceName.ValueNullable?.Name.ExtractText() ?? string.Empty;
				list.Add((item3.RowId, item, item2));
			}
		}
		return list;
	}

	private static bool _00A0(Aetheryte P_0, out Vector3 P_1)
	{
		P_1 = default;
		ExcelSheet<Lumina.Excel.Sheets.Map> excelSheet = DataManager.GetExcelSheet<Lumina.Excel.Sheets.Map>();
		if (excelSheet == null)
		{
			return false;
		}
		Lumina.Excel.Sheets.Map map = excelSheet.FirstOrDefault((Lumina.Excel.Sheets.Map map2) => map2.TerritoryType.RowId == P_0.Territory.RowId);
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
				if (item2.DataType == 3 && item2.DataKey.RowId == P_0.RowId)
				{
					P_1 = new Vector3(_00A0(item2.X, (int)map.SizeFactor), 0f, _00A0(item2.Y, (int)map.SizeFactor));
					return true;
				}
			}
		}
		return false;
	}

	private static float _00A0(int P_0, float P_1)
	{
		return ((float)P_0 - 1024f) / (P_1 / 100f);
	}

	public void PrintEcho(string message)
	{
		Log.Information(message);
		if (Config.EchoMessages)
		{
			ChatGui.Print(new XivChatEntry
			{
				Type = XivChatType.Echo,
				Message = new SeStringBuilder().AddUiForeground("[自动狩猎] ", 37).AddText(message).Build()
			});
		}
	}

	public static void PrintEchoAlways(string message)
	{
		Log.Information(message);
		ChatGui.Print(new XivChatEntry
		{
			Type = XivChatType.Echo,
			Message = new SeStringBuilder().AddUiForeground("[自动狩猎] ", 37).AddText(message).Build()
		});
	}

	private static bool _2022()
	{
		uint num = _2012();
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

	private static bool _2024()
	{
		return Condition.Any(ConditionFlag.BetweenAreas, ConditionFlag.BetweenAreas51, ConditionFlag.Occupied, ConditionFlag.Occupied30, ConditionFlag.Occupied33, ConditionFlag.Occupied38, ConditionFlag.Occupied39, ConditionFlag.InCombat);
	}

	public static bool IsPluginLoaded(string internalName)
	{
		return PluginInterface.InstalledPlugins.Any((IExposedPlugin P_0) => P_0.InternalName == internalName && P_0.IsLoaded);
	}

	private Lumina.Excel.Sheets.Map? _2025()
	{
		uint territoryType = ClientState.TerritoryType;
		if (territoryType == 0)
		{
			return null;
		}
		ExcelSheet<Lumina.Excel.Sheets.Map> excelSheet = DataManager.GetExcelSheet<Lumina.Excel.Sheets.Map>();
		if (excelSheet == null)
		{
			return null;
		}
		Lumina.Excel.Sheets.Map value = excelSheet.FirstOrDefault((Lumina.Excel.Sheets.Map P_0) => P_0.TerritoryType.RowId == territoryType);
		if (value.RowId == 0 || value.SizeFactor <= 0)
		{
			return null;
		}
		return value;
	}

	private static Vector2 _00A0(float P_0, float P_1, Lumina.Excel.Sheets.Map P_2)
	{
		float num = (float)(int)P_2.SizeFactor / 100f;
		float x = ((P_0 - 1f) * num / 41f * 2048f - 1024f) / num - (float)P_2.OffsetX;
		float y = ((P_1 - 1f) * num / 41f * 2048f - 1024f) / num - (float)P_2.OffsetY;
		return new Vector2(x, y);
	}

	public void AdjustHuntChainKills(int delta)
	{
		if (CanAdjustHuntChainKills && (delta == 1 || delta == -1))
		{
			this.m__206A = (int)Math.Clamp((long)this.m__206A + (long)delta, 0L, 2147483647L);
		}
	}

	public string GetHuntChainStatusText(bool compact = false)
	{
		string text = (compact ? string.Empty : "连车: ");
		List<HuntChainStep> list = this.m__2063;
		if (this.m__2064 < 0 || this.m__2064 >= list.Count)
		{
			return text + ((this.m__2062 == AutomaticHunting._2004._2002_2029) ? "已完成全部站点" : "未运行");
		}
		string text2 = $"第 {this.m__2064 + 1}/{list.Count} 站";
		int killTarget = list[this.m__2064].KillTarget;
		return this.m__2062 switch
		{
			AutomaticHunting._2004._2002_2027 => text + text2 + " · 前往中", 
			AutomaticHunting._2004._2002_2028 => compact ? $"{text}{text2} · {this.m__206A}/{killTarget}" : $"{text}{text2} · 击杀 {this.m__206A}/{killTarget}", 
			AutomaticHunting._2004._2002_2029 => text + "已完成全部站点", 
			_ => text + "未运行", 
		};
	}

	public bool StartHuntChain(IReadOnlyList<HuntChainStep>? source = null)
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
		IReadOnlyList<HuntChainStep> readOnlyList = source ?? Config.HuntChain.Steps;
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
				if (!_00A0(huntChainStep.Preset, out var _, out var _, out var _))
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
		this.m__2063 = readOnlyList.Select(_00A0).ToList();
		this.m__206E = currentDcName;
		this.m__2064 = 0;
		this.m__206A = 0;
		this.m__206B = false;
		this.m__206C = DateTime.MinValue;
		this.m__206D = DateTime.MinValue;
		this.m__2062 = AutomaticHunting._2004._2002_2027;
		PrintEcho($"连车: 已开始，共 {this.m__2063.Count} 站");
		_202C();
		return true;
	}

	private static HuntChainStep _00A0(HuntChainStep P_0)
	{
		return new HuntChainStep
		{
			Preset = new WorldTravelPreset
			{
				ServerName = P_0.Preset.ServerName,
				ExVersion = P_0.Preset.ExVersion,
				MapRowId = P_0.Preset.MapRowId,
				MapName = P_0.Preset.MapName,
				AetheryteId = P_0.Preset.AetheryteId,
				AetheryteName = P_0.Preset.AetheryteName
			},
			KillTarget = P_0.KillTarget
		};
	}

	public void StopHuntChain(string? reason = null)
	{
		bool flag = this.m__206B;
		_2027();
		if (!string.IsNullOrEmpty(reason))
		{
			PrintEcho(reason);
		}
		if (flag)
		{
			StopCrossWorld();
		}
	}

	private void _2027()
	{
		this.m__2062 = AutomaticHunting._2004._2002_2025;
		this.m__2063.Clear();
		this.m__2064 = -1;
		this.m__206A = 0;
		this.m__206B = false;
		this.m__206C = DateTime.MinValue;
		this.m__206D = DateTime.MinValue;
		this.m__206E = string.Empty;
	}

	private void _2028()
	{
		_202B();
		_2004 obj = this.m__2062;
		if ((obj == AutomaticHunting._2004._2002_2025 || obj == AutomaticHunting._2004._2002_2029) ? true : false)
		{
			return;
		}
		if (!Config.AutomationEnabled)
		{
			StopHuntChain("连车: 自动狩猎已关闭，连车停止");
			return;
		}
		if (!string.IsNullOrEmpty(this.m__206E))
		{
			string currentDcName = WorldTravelData.GetCurrentDcName();
			if (!string.IsNullOrEmpty(currentDcName) && currentDcName != this.m__206E)
			{
				StopHuntChain($"连车: 大区已变化({this.m__206E}→{currentDcName})，连车停止");
				return;
			}
		}
		List<HuntChainStep> list = this.m__2063;
		if (this.m__2064 < 0 || this.m__2064 >= list.Count)
		{
			StopHuntChain("连车: 步骤数据异常，连车停止");
		}
		else if (this.m__2062 == AutomaticHunting._2004._2002_2028)
		{
			_1680(list[this.m__2064]);
		}
		else
		{
			_2000(list[this.m__2064]);
		}
	}

	private void _2029()
	{
		if (this.m__2062 == AutomaticHunting._2004._2002_2028)
		{
			this.m__206A++;
		}
	}

	private void _202A()
	{
		if (Config.HuntChain.CreatePfAfterTravel)
		{
			this.m__206D = DateTime.UtcNow.AddSeconds(1.0);
		}
	}

	private void _202B()
	{
		if (!(this.m__206D == DateTime.MinValue) && !(DateTime.UtcNow < this.m__206D))
		{
			this.m__206D = DateTime.MinValue;
			if (this.m__2062 != AutomaticHunting._2004._2002_2025 && Config.AutomationEnabled && Config.HuntChain.CreatePfAfterTravel)
			{
				PrintEcho("连车: 已到达站点，创建狩猎招募");
				HuntPfController.Create(Config.PfComment);
			}
		}
	}

	private void _1680(HuntChainStep P_0)
	{
		if (this.m__206A >= P_0.KillTarget)
		{
			List<HuntChainStep> list = this.m__2063;
			if (this.m__2064 + 1 >= list.Count)
			{
				this.m__2062 = AutomaticHunting._2004._2002_2029;
				this.m__206B = false;
				PrintEcho("连车: 最后一站击杀达标，全部完成");
				return;
			}
			this.m__2064++;
			PrintEcho($"连车: 第 {this.m__2064} 站达标，前往第 {this.m__2064 + 1} 站");
			_202C();
		}
	}

	private void _2000(HuntChainStep P_0)
	{
		if (!_00A0(P_0.Preset, out var num, out var num2, out var _))
		{
			StopHuntChain($"连车: 第 {this.m__2064 + 1} 站配置已失效，连车停止");
		}
		else if (_00A0(P_0.Preset, num))
		{
			if (!CrossWorldInProgress && !Condition[ConditionFlag.BetweenAreas] && !Condition[ConditionFlag.BetweenAreas51])
			{
				this.m__2062 = AutomaticHunting._2004._2002_2028;
				this.m__206A = 0;
				this.m__206B = false;
				this.m__206C = DateTime.MinValue;
				PrintEcho($"连车: 已到达第 {this.m__2064 + 1} 站 {P_0.Preset.ServerName} {P_0.Preset.MapName}，开始计数");
				_202A();
			}
		}
		else if (this.m__206B)
		{
			if (DateTime.UtcNow > this.m__206C)
			{
				StopHuntChain("连车: 前往下一站超时，连车停止");
			}
		}
		else if (!Condition[ConditionFlag.InCombat] && !CrossWorldInProgress)
		{
			_00A0(P_0.Preset, out num2, out var aetheryteId, out var aetherytePos);
			this.m__206B = true;
			this.m__206C = DateTime.UtcNow.AddSeconds(300.0);
			StartManualWorldTravel(P_0.Preset.ServerName, num, aetheryteId, aetherytePos, fromHuntChain: true);
		}
	}

	private void _202C()
	{
		this.m__206A = 0;
		this.m__206B = false;
		this.m__206C = DateTime.MinValue;
		this.m__2062 = AutomaticHunting._2004._2002_2027;
		List<HuntChainStep> list = this.m__2063;
		if (this.m__2064 < 0 || this.m__2064 >= list.Count)
		{
			StopHuntChain("连车: 步骤数据异常，连车停止");
			return;
		}
		while (this.m__2064 < list.Count && list[this.m__2064].KillTarget == 0)
		{
			PrintEcho($"连车: 第 {this.m__2064 + 1} 站数量为 0，已跳过");
			this.m__2064++;
		}
		if (this.m__2064 >= list.Count)
		{
			this.m__2062 = AutomaticHunting._2004._2002_2029;
			PrintEcho("连车: 已无需要执行的站点，全部完成");
			return;
		}
		HuntChainStep huntChainStep = list[this.m__2064];
		if (_00A0(huntChainStep.Preset, out var num, out var _, out var _) && _00A0(huntChainStep.Preset, num) && !CrossWorldInProgress)
		{
			this.m__2062 = AutomaticHunting._2004._2002_2028;
			PrintEcho($"连车: 第 {this.m__2064 + 1} 站已在目标地图，开始计数");
			_202A();
		}
		else
		{
			PrintEcho($"连车: 前往第 {this.m__2064 + 1} 站 {huntChainStep.Preset.ServerName} {huntChainStep.Preset.MapName} · {huntChainStep.Preset.AetheryteName}");
		}
	}

	private static bool _00A0(WorldTravelPreset P_0, out uint P_1, out uint P_2, out Vector3 P_3)
	{
		P_1 = 0u;
		P_2 = 0u;
		P_3 = default;
		WorldTravelData.MapEntry map = WorldTravelData.GetMaps(P_0.ExVersion).FirstOrDefault((WorldTravelData.MapEntry mapEntry) => mapEntry.RowId == P_0.MapRowId);
		if (map.Territory == 0)
		{
			return false;
		}
		(uint, Vector3, string) tuple = WorldTravelData.GetAetherytes(map).FirstOrDefault(((uint Id, Vector3 Position, string Name) a) => a.Id == P_0.AetheryteId);
		if (tuple.Item1 == 0)
		{
			return false;
		}
		P_1 = map.Territory;
		(P_2, P_3, _) = tuple;
		return true;
	}

	private bool _00A0(WorldTravelPreset P_0, uint P_1)
	{
		if (ClientState.TerritoryType != P_1)
		{
			return false;
		}
		string text = _202D();
		if (text.Length != 0)
		{
			return text == P_0.ServerName;
		}
		return true;
	}

	private string _202D()
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

	private void _1680(uint P_0, Vector3 P_1)
	{
		if (!Config.AutomationEnabled)
		{
			PrintEcho("自动狩猎未启用，无法导航");
			return;
		}
		_00A0 obj = new _00A0(P_0, P_1);
		if (Condition[ConditionFlag.InCombat])
		{
			this.m__2027 = obj;
			this.m__2029 = false;
			PrintEcho("战斗中，已保存最新Flag，战斗结束后执行");
		}
		else if (this.m__2028)
		{
			this.m__2027 = obj;
			this.m__2029 = true;
			PrintEcho("S怪监控中，已保存最新Flag，S怪结束后执行");
		}
		else
		{
			_00A0(obj);
		}
	}

	private void _00A0(_00A0 P_0)
	{
		_202B CS_0024_003C_003E8__locals6 = new _202B();
		CS_0024_003C_003E8__locals6._2003_2008 = this;
		CS_0024_003C_003E8__locals6._2003_2009 = P_0;
		CS_0024_003C_003E8__locals6._2003_200A = new CancellationTokenSource();
		lock (this.m__204A)
		{
			CancellationTokenSource? cancellationTokenSource = this.m__204B;
			CS_0024_003C_003E8__locals6._2003_2007 = this.m__204C;
			this.m__204B = CS_0024_003C_003E8__locals6._2003_200A;
			this.m__204D = AutomaticHunting._2003._2002_2012;
			cancellationTokenSource?.Cancel();
			this.m__204C = Task.Run([AsyncStateMachine(typeof(_202B._00A0))] () =>
			{
				_202B._00A0 stateMachine = default;
				stateMachine._2008_2061 = AsyncTaskMethodBuilder.Create();
				stateMachine._2008_2062 = CS_0024_003C_003E8__locals6;
				stateMachine._2008_2060 = -1;
				stateMachine._2008_2061.Start(ref stateMachine);
				return stateMachine._2008_2061.Task;
			});
		}
	}

	[AsyncStateMachine(typeof(_206B))]
	private Task _00A0(_00A0 P_0, CancellationTokenSource P_1)
	{
		_206B stateMachine = default;
		stateMachine._2006_2029 = AsyncTaskMethodBuilder.Create();
		stateMachine._2006_202B = this;
		stateMachine._2006_202C = P_0;
		stateMachine._2006_202A = P_1;
		stateMachine._2006_2028 = -1;
		stateMachine._2006_2029.Start(ref stateMachine);
		return stateMachine._2006_2029.Task;
	}

	[AsyncStateMachine(typeof(_2063))]
	private Task<_2005> _00A0(_00A0 P_0, CancellationToken P_1)
	{
		_2063 stateMachine = default;
		stateMachine._2005_205D = AsyncTaskMethodBuilder<AutomaticHunting._2005>.Create();
		stateMachine._2005_205E = this;
		stateMachine._2005_205F = P_0;
		stateMachine._2005_2060 = P_1;
		stateMachine._2005_205C = -1;
		stateMachine._2005_205D.Start(ref stateMachine);
		return stateMachine._2005_205D.Task;
	}

	private _2005 _00A0(_00A0 P_0, out uint P_1)
	{
		P_1 = 0u;
		uint num = _2012();
		Vector3? localPlayerPosition = GetLocalPlayerPosition();
		List<(uint, Vector3, string)> list = (this.m__2025 = _2002(P_0.TerritoryType));
		if (list.Count == 0)
		{
			if (num != P_0.TerritoryType)
			{
				PrintEcho("目标区域没有可用水晶，导航已取消");
				return AutomaticHunting._2005._2002_202C;
			}
			return AutomaticHunting._2005._2002_202A;
		}
		(uint, Vector3, string) item = (from x in list.Select(((uint Id, Vector3 Position, string Name) a) => (Aetheryte: a, Corrected: _00A0(a.Id, a.Position, P_0.Position)))
			orderby x.Corrected
			select x).First().Aetheryte;
		item = _00A0(item, list);
		if (num != P_0.TerritoryType)
		{
			(P_1, _, _) = item;
			PrintEcho("传送到 " + item.Item3 + " (距标点最近，修正后)");
			return AutomaticHunting._2005._2002_202B;
		}
		if (!localPlayerPosition.HasValue)
		{
			return AutomaticHunting._2005._2002_202C;
		}
		float num2 = _00A0(localPlayerPosition.Value, P_0.Position);
		float num3 = _00A0(item.Item1, item.Item2, P_0.Position);
		if (num2 - num3 <= Config.TeleportThreshold || string.IsNullOrWhiteSpace(item.Item3))
		{
			return AutomaticHunting._2005._2002_202A;
		}
		(P_1, _, _) = item;
		PrintEcho($"传送更快: 玩家到Flag {num2:0.0}m，水晶到Flag {num3:0.0}m");
		return AutomaticHunting._2005._2002_202B;
	}

	private float _00A0(uint P_0, Vector3 P_1, Vector3 P_2)
	{
		float num = _00A0(P_1, P_2);
		if (P_0 == Config.SpecialSmallAetheryteId)
		{
			return num + 400f;
		}
		return num;
	}

	private (uint Id, Vector3 Position, string Name) _00A0((uint Id, Vector3 Position, string Name) selected, List<(uint Id, Vector3 Position, string Name)> aetherytes)
	{
		if (!Config.BlacklistedAetheryteIds.Contains(selected.Id))
		{
			return selected;
		}
		if (!Config.BlacklistReplacements.TryGetValue(selected.Id, out var value))
		{
			return selected;
		}
		(uint, Vector3, string) result = aetherytes.FirstOrDefault(((uint Id, Vector3 Position, string Name) a) => a.Id == value);
		if (result.Item1 == 0)
		{
			return selected;
		}
		PrintEcho($"水晶{selected.Id}已拉黑，替换为{result.Item3}");
		return result;
	}

	[AsyncStateMachine(typeof(_1680_2001))]
	private Task<bool> _2000(uint P_0, CancellationToken P_1)
	{
		_1680_2001 stateMachine = default;
		stateMachine._2007_2049 = AsyncTaskMethodBuilder<bool>.Create();
		stateMachine._2007_204B = this;
		stateMachine._2007_204A = P_0;
		stateMachine._2007_204C = P_1;
		stateMachine._2007_2048 = -1;
		stateMachine._2007_2049.Start(ref stateMachine);
		return stateMachine._2007_2049.Task;
	}

	[AsyncStateMachine(typeof(_205F))]
	private Task _00A0(_00A0 P_0, CancellationToken P_1, CancellationTokenSource P_2)
	{
		_205F stateMachine = default;
		stateMachine._2005_2000 = AsyncTaskMethodBuilder.Create();
		stateMachine._2005_2001 = this;
		stateMachine._2005_2002 = P_0;
		stateMachine._2005_2005 = P_1;
		stateMachine._2005_2003 = P_2;
		stateMachine._2005_1680 = -1;
		stateMachine._2005_2000.Start(ref stateMachine);
		return stateMachine._2005_2000.Task;
	}

	[AsyncStateMachine(typeof(_206C))]
	private Task _00A0(_2000 P_0, CancellationTokenSource P_1, CancellationToken P_2)
	{
		_206C stateMachine = default;
		stateMachine._2006_2033 = AsyncTaskMethodBuilder.Create();
		stateMachine._2006_2036 = this;
		stateMachine._2006_2047 = P_0;
		stateMachine._2006_203E = P_1;
		stateMachine._2006_2049 = P_2;
		stateMachine._2006_2035 = -1;
		stateMachine._2006_2033.Start(ref stateMachine);
		return stateMachine._2006_2033.Task;
	}

	[AsyncStateMachine(typeof(_2056))]
	private Task _00A0(_2000 P_0, float P_1, CancellationToken P_2)
	{
		_2056 stateMachine = default;
		stateMachine._2004_2002 = AsyncTaskMethodBuilder.Create();
		stateMachine._2004_2003 = this;
		stateMachine._2004_2005 = P_0;
		stateMachine._2004_2004 = P_1;
		stateMachine._2004_2006 = P_2;
		stateMachine._2004_2001 = -1;
		stateMachine._2004_2002.Start(ref stateMachine);
		return stateMachine._2004_2002.Task;
	}

	[AsyncStateMachine(typeof(_2055))]
	private Task<bool> _1680(CancellationToken P_0)
	{
		_2055 stateMachine = default;
		stateMachine._2003_206F = AsyncTaskMethodBuilder<bool>.Create();
		stateMachine._2003_3000 = P_0;
		stateMachine._2003_206E = -1;
		stateMachine._2003_206F.Start(ref stateMachine);
		return stateMachine._2003_206F.Task;
	}

	[AsyncStateMachine(typeof(_2053))]
	private Task _2000(CancellationToken P_0)
	{
		_2053 stateMachine = default;
		stateMachine._2003_205C = AsyncTaskMethodBuilder.Create();
		stateMachine._2003_205D = this;
		stateMachine._2003_205E = P_0;
		stateMachine._2003_205B = -1;
		stateMachine._2003_205C.Start(ref stateMachine);
		return stateMachine._2003_205C.Task;
	}

	[AsyncStateMachine(typeof(_2060))]
	private Task<bool> _00A0(_2000 P_0, CancellationToken P_1)
	{
		_2060 stateMachine = default;
		stateMachine._2005_2027 = AsyncTaskMethodBuilder<bool>.Create();
		stateMachine._2005_2028 = this;
		stateMachine._2005_2029 = P_0;
		stateMachine._2005_202B = P_1;
		stateMachine._2005_2025 = -1;
		stateMachine._2005_2027.Start(ref stateMachine);
		return stateMachine._2005_2027.Task;
	}

	[AsyncStateMachine(typeof(_205A))]
	private Task<Vector3?> _00A0(Vector3 P_0, bool P_1)
	{
		_205A stateMachine = default;
		stateMachine._2004_202B = AsyncTaskMethodBuilder<Vector3?>.Create();
		stateMachine._2004_202C = this;
		stateMachine._2004_202D = P_0;
		stateMachine._2004_202E = P_1;
		stateMachine._2004_202A = -1;
		stateMachine._2004_202B.Start(ref stateMachine);
		return stateMachine._2004_202B.Task;
	}

	private bool _00A0(Vector3 P_0, bool P_1, out Vector3 P_2)
	{
		_2000? obj = _2048();
		if (obj.HasValue)
		{
			P_2 = obj.Value.Position;
			return true;
		}
		if (TargetManager.Target is IBattleChara { CurrentHp: >0u } battleChara && _2006(battleChara.Name.TextValue))
		{
			P_2 = battleChara.Position;
			return true;
		}
		P_2 = P_0;
		return P_1;
	}

	public void StartTargetHpMonitor()
	{
		_202F();
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
		MultiLineMapConfig multiLineMapConfig = _2009();
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
		_202E();
		CommandManager.ProcessCommand($"/pdr insc {num}");
		PrintEcho($"正在切换到 {num} 线 (/pdr insc {num})");
		_1680(num);
	}

	private void _202E()
	{
		PluginInterface.SavePluginConfig(Config);
	}

	[AsyncStateMachine(typeof(_205D))]
	private Task _202F()
	{
		_205D stateMachine = default;
		stateMachine._2004_205A = AsyncTaskMethodBuilder.Create();
		stateMachine._2004_205B = this;
		stateMachine._2004_2059 = -1;
		stateMachine._2004_205A.Start(ref stateMachine);
		return stateMachine._2004_205A.Task;
	}

	public void ExecuteDashSkill()
	{
		string text = Config.SDashCommand?.Trim() ?? string.Empty;
		if (string.IsNullOrWhiteSpace(text))
		{
			return;
		}
		string text2 = null;
		string[] array = new string[3] { "/ac", "/action", "/技能" };
		foreach (string text3 in array)
		{
			if (text.StartsWith(text3, StringComparison.OrdinalIgnoreCase) && (text.Length == text3.Length || char.IsWhiteSpace(text[text3.Length])))
			{
				text2 = text.Substring(text3.Length).Trim().Trim('"');
				break;
			}
		}
		Framework.RunOnFrameworkThread(() =>
		{
			try
			{
				if (text2 == null || !_2005(text2))
				{
					string content = ((!text.StartsWith('/')) ? ("/" + text) : text);
					CommandManager.ProcessCommand(content);
				}
			}
			catch (Exception exception)
			{
				Log.Warning(exception, "AutomaticHunting: 执行突进技能失败 " + text);
			}
		});
	}

	private void _2004(string P_0)
	{
		Framework.RunOnFrameworkThread(() =>
		{
			try
			{
				CommandManager.ProcessCommand(P_0);
			}
			catch (Exception exception)
			{
				Log.Warning(exception, "AutomaticHunting: 执行命令失败 " + P_0);
			}
		});
	}

	private unsafe static bool _2005(string P_0)
	{
		try
		{
			uint value;
			lock (_1680_2006)
			{
				if (!_1680_2005.TryGetValue(P_0, out value))
				{
					ExcelSheet<Lumina.Excel.Sheets.Action> excelSheet = DataManager.GetExcelSheet<Lumina.Excel.Sheets.Action>();
					if (excelSheet == null)
					{
						return false;
					}
					Lumina.Excel.Sheets.Action action = excelSheet.FirstOrDefault((Lumina.Excel.Sheets.Action action2) => action2.Name.ExtractText() == P_0);
					if (action.RowId == 0)
					{
						Log.Warning("AutomaticHunting: 未找到技能 " + P_0);
						return false;
					}
					value = action.RowId;
					_1680_2005[P_0] = value;
				}
			}
			ActionManager* ptr = ActionManager.Instance();
			if (ptr == null)
			{
				return false;
			}
			ulong num = TargetManager.Target?.GameObjectId ?? 0;
			bool flag = ptr->UseAction(ActionType.Action, value, num, 0u, ActionManager.UseActionMode.None, 0u, null);
			Log.Information($"AutomaticHunting: 使用技能 {P_0} (id={value}, target={num}) => {flag}");
			return flag;
		}
		catch (Exception exception)
		{
			Log.Warning(exception, "AutomaticHunting: 查找或使用技能失败 " + P_0);
		}
		return false;
	}

	[AsyncStateMachine(typeof(_1680_2002))]
	private Task<bool> _2001(CancellationToken P_0)
	{
		_1680_2002 stateMachine = default;
		stateMachine._2007_2053 = AsyncTaskMethodBuilder<bool>.Create();
		stateMachine._2007_2054 = this;
		stateMachine._2007_2055 = P_0;
		stateMachine._2007_2052 = -1;
		stateMachine._2007_2053.Start(ref stateMachine);
		return stateMachine._2007_2053.Task;
	}

	[AsyncStateMachine(typeof(_206E))]
	private Task<bool> _2002(CancellationToken P_0)
	{
		_206E stateMachine = default;
		stateMachine._2006_206D = AsyncTaskMethodBuilder<bool>.Create();
		stateMachine._2006_206F = this;
		stateMachine._2006_206E = P_0;
		stateMachine._2006_206C = -1;
		stateMachine._2006_206D.Start(ref stateMachine);
		return stateMachine._2006_206D.Task;
	}

	private unsafe bool _2032()
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

	private unsafe bool _2035()
	{
		if (Condition[ConditionFlag.BetweenAreas] || Condition[ConditionFlag.BetweenAreas51])
		{
			return false;
		}
		if (Condition[ConditionFlag.MountOrOrnamentTransition] || Condition[ConditionFlag.Casting])
		{
			return false;
		}
		if (_2024())
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
		_2033();
	}

	[AsyncStateMachine(typeof(_206F))]
	private Task _2033()
	{
		_206F stateMachine = default;
		stateMachine._2007_2004 = AsyncTaskMethodBuilder.Create();
		stateMachine._2007_2005 = this;
		stateMachine._2007_2003 = -1;
		stateMachine._2007_2004.Start(ref stateMachine);
		return stateMachine._2007_2004.Task;
	}

	public void StopNavigation()
	{
		lock (this.m__204A)
		{
			CancellationTokenSource? cancellationTokenSource = this.m__204B;
			this.m__204D = AutomaticHunting._2003._2002_2012;
			cancellationTokenSource?.Cancel();
		}
		_203E();
		_2036();
	}

	private void _2036()
	{
		FinishSMonitor("已停止");
		LastFlagPosition = null;
		this.m__2022 = 0u;
		this.m__2027 = null;
		this.m__2029 = false;
		this.m__205D = null;
		this.m__2015 = string.Empty;
		this.m__2024 = DateTime.MinValue;
		this.m__2033 = null;
		this.m__2036 = null;
		this.m__203E = DateTime.MinValue;
	}

	public void StartNavigation()
	{
		uint num;
		uint num2;
		Vector3 vector;
		if (!Config.AutomationEnabled)
		{
			PrintEcho("自动狩猎未启用，无法导航");
		}
		else if (!_00A0(out num, out num2, out vector))
		{
			PrintEcho("当前没有 Flag 或 vnav 未就绪");
		}
		else
		{
			_2000(num, vector);
		}
	}

	private unsafe bool _00A0(out uint P_0, out uint P_1, out Vector3 P_2)
	{
		P_0 = 0u;
		P_1 = 0u;
		P_2 = default;
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
		if (!_00A0(territoryId, mapId, mapLinkPayload.XCoord, mapLinkPayload.YCoord, out var vector))
		{
			return false;
		}
		P_0 = territoryId;
		P_1 = mapId;
		P_2 = vector;
		return true;
	}

	private void _2000(uint P_0, Vector3 P_1)
	{
		_204C CS_0024_003C_003E8__locals10 = new _204C();
		CS_0024_003C_003E8__locals10._2003_204F = this;
		CS_0024_003C_003E8__locals10._2003_2050 = P_0;
		CS_0024_003C_003E8__locals10._2003_2051 = P_1;
		LastFlagPosition = CS_0024_003C_003E8__locals10._2003_2051;
		this.m__2022 = CS_0024_003C_003E8__locals10._2003_2050;
		this.m__2025 = _2002(CS_0024_003C_003E8__locals10._2003_2050);
		CS_0024_003C_003E8__locals10._2003_2052 = new CancellationTokenSource();
		lock (this.m__204A)
		{
			CancellationTokenSource? cancellationTokenSource = this.m__204B;
			CS_0024_003C_003E8__locals10._2003_204E = this.m__204C;
			this.m__204B = CS_0024_003C_003E8__locals10._2003_2052;
			this.m__204D = AutomaticHunting._2003._2002_2012;
			cancellationTokenSource?.Cancel();
			this.m__204C = Task.Run([AsyncStateMachine(typeof(_204C._00A0))] () =>
			{
				_204C._00A0 stateMachine = default;
				stateMachine._2008_206A = AsyncTaskMethodBuilder.Create();
				stateMachine._2008_206B = CS_0024_003C_003E8__locals10;
				stateMachine._2008_2064 = -1;
				stateMachine._2008_206A.Start(ref stateMachine);
				return stateMachine._2008_206A.Task;
			});
		}
	}

	[AsyncStateMachine(typeof(_206A))]
	private Task _00A0(uint P_0, Vector3 P_1, CancellationTokenSource P_2)
	{
		_206A stateMachine = default;
		stateMachine._2006_2011 = AsyncTaskMethodBuilder.Create();
		stateMachine._2006_2015 = this;
		stateMachine._2006_2013 = P_0;
		stateMachine._2006_2014 = P_1;
		stateMachine._2006_2012 = P_2;
		stateMachine._2006_2010 = -1;
		stateMachine._2006_2011.Start(ref stateMachine);
		return stateMachine._2006_2011.Task;
	}

	[AsyncStateMachine(typeof(_205C))]
	private Task _00A0(Vector3 P_0, CancellationTokenSource P_1, CancellationToken P_2)
	{
		_205C stateMachine = default;
		stateMachine._2004_204D = AsyncTaskMethodBuilder.Create();
		stateMachine._2004_204E = this;
		stateMachine._2004_2050 = P_0;
		stateMachine._2004_2051 = P_1;
		stateMachine._2004_204F = P_2;
		stateMachine._2004_204C = -1;
		stateMachine._2004_204D.Start(ref stateMachine);
		return stateMachine._2004_204D.Task;
	}

	[AsyncStateMachine(typeof(_205E))]
	private Task _00A0(Vector3 P_0, bool P_1, CancellationTokenSource P_2, CancellationToken P_3)
	{
		_205E stateMachine = default;
		stateMachine._2004_2060 = AsyncTaskMethodBuilder.Create();
		stateMachine._2004_2061 = this;
		stateMachine._2004_2063 = P_0;
		stateMachine._2004_206B = P_1;
		stateMachine._2004_2062 = P_2;
		stateMachine._2004_206A = P_3;
		stateMachine._2004_205F = -1;
		stateMachine._2004_2060.Start(ref stateMachine);
		return stateMachine._2004_2060.Task;
	}

	private Task _203E()
	{
		_2004("/vnav stop");
		return Task.CompletedTask;
	}

	private void _00A0(_2003 P_0, CancellationTokenSource P_1)
	{
		lock (this.m__204A)
		{
			if (this.m__204B == P_1)
			{
				this.m__204D = P_0;
			}
		}
	}

	private static float _00A0(Vector3 P_0, Vector3 P_1)
	{
		return Vector2.Distance(new Vector2(P_0.X, P_0.Z), new Vector2(P_1.X, P_1.Z));
	}

	private unsafe static bool _2003(uint P_0)
	{
		ActionManager* ptr = ActionManager.Instance();
		if (ptr != null)
		{
			return ptr->UseAction(ActionType.Mount, P_0, 3758096384uL, 0u, ActionManager.UseActionMode.None, 0u, null);
		}
		return false;
	}

	private unsafe static bool _2047()
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
		_2000? obj = _2048();
		PrintEcho(obj.HasValue ? $"已找到S怪: {obj.Value.Name} 血量 {obj.Value.HpPercent:0.0}%" : $"扫描S怪: {100f:0}m内未找到匹配的目标");
	}

	private _2000? _2048()
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
		_2000? result = null;
		float num = 100f;
		foreach (IGameObject item in ObjectTable)
		{
			float num2 = _00A0(localPlayerPosition.Value, item.Position);
			if (!(num2 > num))
			{
				string textValue = item.Name.TextValue;
				if (value.Any((string P_0) => textValue.Contains(P_0)))
				{
					float num3 = _00A0(item);
					num = num2;
					result = new _2000(textValue, item.Position, num3);
				}
			}
		}
		return result;
	}

	private bool _2006(string? P_0)
	{
		if (string.IsNullOrWhiteSpace(P_0))
		{
			return false;
		}
		string currentTerritoryName = GetCurrentTerritoryName();
		if (string.IsNullOrWhiteSpace(currentTerritoryName) || !Config.SRealmMarks.TryGetValue(currentTerritoryName, out string[] value))
		{
			return false;
		}
		return value.Any((string value2) => P_0.Contains(value2));
	}

	private string? _00A0(_2000 P_0)
	{
		IGameObject gameObject = (from gameObject2 in ObjectTable
			where gameObject2.Name.TextValue == P_0.Name
			orderby _00A0(gameObject2.Position, P_0.Position)
			select gameObject2).FirstOrDefault();
		if (gameObject == null)
		{
			return null;
		}
		TargetManager.Target = gameObject;
		return gameObject.Name.TextValue;
	}

	private static float _00A0(IGameObject P_0)
	{
		try
		{
			if (P_0 is IBattleChara battleChara)
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

	private static bool _00A0(string P_0, out float P_1)
	{
		if (TargetManager.Target is IBattleChara { CurrentHp: >0u } battleChara)
		{
			float num = battleChara.MaxHp;
			if (num > 0f)
			{
				P_1 = (float)battleChara.CurrentHp / num * 100f;
				return true;
			}
		}
		P_1 = 100f;
		return false;
	}

	static AutomaticHunting()
	{
		AutomaticHunting.m__00A0 = null;
		AutomaticHunting.m__1680 = null;
		AutomaticHunting.m__2000 = null;
		AutomaticHunting.m__2001 = null;
		AutomaticHunting.m__2002 = null;
		AutomaticHunting.m__2003 = null;
		AutomaticHunting.m__2004 = null;
		AutomaticHunting.m__2005 = null;
		AutomaticHunting.m__2006 = null;
		AutomaticHunting.m__2007 = null;
		AutomaticHunting.m__2008 = null;
		AutomaticHunting.m__2051 = new Regex("[(（]?\\s*(\\d{1,2})[.．](\\d)\\s*[,，、]\\s*(\\d{1,2})[.．](\\d)\\s*[)）]?", RegexOptions.Compiled);
		AutomaticHunting.m__2060 = new ConcurrentDictionary<string, List<string>>();
		_1680_2005 = new Dictionary<string, uint>(StringComparer.OrdinalIgnoreCase);
		_1680_2006 = new object();
	}

	[CompilerGenerated]
	private int _2049()
	{
		if (!Condition[ConditionFlag.BetweenAreas] && !Condition[ConditionFlag.BetweenAreas51])
		{
			return GetCurrentInstanceNumber();
		}
		return -1;
	}

	[CompilerGenerated]
	private bool _00A0(MountInfo P_0)
	{
		return P_0.RowId == Config.SelectedMountId;
	}

	[CompilerGenerated]
	private (int kind, float hp, Vector3 pos, Vector3? player, bool isS) _204A()
	{
		Vector3? localPlayerPosition = GetLocalPlayerPosition();
		if (TargetManager.Target is IBattleChara { MaxHp: >0u, CurrentHp: >0u } battleChara)
		{
			return (kind: 1, hp: (float)battleChara.CurrentHp / (float)battleChara.MaxHp * 100f, pos: battleChara.Position, player: localPlayerPosition, isS: _2006(battleChara.Name.TextValue));
		}
		_2000? obj = _2048();
		if (obj.HasValue)
		{
			if (obj.Value.HpPercent <= 0f)
			{
				return (kind: 2, hp: 0f, pos: obj.Value.Position, player: localPlayerPosition, isS: true);
			}
			_00A0(obj.Value);
			return (kind: 3, hp: obj.Value.HpPercent, pos: obj.Value.Position, player: localPlayerPosition, isS: true);
		}
		return (kind: 0, hp: -1f, pos: Vector3.Zero, player: localPlayerPosition, isS: false);
	}

	[CompilerGenerated]
	private bool _204B()
	{
		return this.m__2012?.InvokeFunc() ?? false;
	}

	[CompilerGenerated]
	private bool _204C()
	{
		if (Config.SelectedMountId != 0)
		{
			return _2003(Config.SelectedMountId);
		}
		return _2047();
	}
}
