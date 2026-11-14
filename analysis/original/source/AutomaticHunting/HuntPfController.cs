using System;
using System.Runtime.CompilerServices;
using System.Text;
using Dalamud.Game.ClientState.Objects.SubKinds;
using ECommons;
using ECommons.Automation;
using ECommons.Automation.NeoTaskManager;
using ECommons.Throttlers;
using ECommons.UIHelpers.AddonMasterImplementations;
using FFXIVClientStructs.FFXIV.Client.Game;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using FFXIVClientStructs.FFXIV.Component.GUI;

namespace AutomaticHunting;

public static class HuntPfController
{
	[Serializable]
	[CompilerGenerated]
	private sealed class _00A0
	{
		public static readonly _00A0 _2007_2059 = new _00A0();

		public static Func<bool> _2007_205A;

		public static Func<bool> _2007_205B;

		public static Func<bool> _2007_205C;

		public static Func<bool> _2007_205D;

		public static Func<bool> _2007_205E;

		public static Func<bool> _2007_205F;

		internal unsafe bool _00A0()
		{
			if (GenericHelpers.TryGetAddonByName<AtkUnitBase>("LookingForGroup", out var _) && EzThrottler.Throttle("AH_PfToggle"))
			{
				Chat.ExecuteCommand("/pfinder");
			}
			return true;
		}

		internal unsafe bool _1680()
		{
			AtkUnitBase* AddonPtr;
			return !GenericHelpers.TryGetAddonByName<AtkUnitBase>("LookingForGroup", out AddonPtr);
		}

		internal bool _2000()
		{
			if (EzThrottler.Throttle("AH_PfOpen"))
			{
				Chat.ExecuteCommand("/pfinder");
			}
			return true;
		}

		internal unsafe bool _2001()
		{
			if (GenericHelpers.TryGetAddonMaster<AddonMaster.LookingForGroupCondition>(out var addonMaster) && GenericHelpers.IsAddonReady(addonMaster.Base))
			{
				addonMaster.Normal();
				return true;
			}
			return false;
		}

		internal unsafe bool _2002()
		{
			if (GenericHelpers.TryGetAddonMaster<AddonMaster.LookingForGroupCondition>(out var addonMaster) && GenericHelpers.IsAddonReady(addonMaster.Base))
			{
				addonMaster.SelectDutyCategory(11);
				return true;
			}
			return false;
		}

		internal unsafe bool _2003()
		{
			if (GenericHelpers.TryGetAddonMaster<AddonMaster.LookingForGroupCondition>(out var addonMaster) && GenericHelpers.IsAddonReady(addonMaster.Base) && EzThrottler.Throttle("AH_PfConfirm", 1000))
			{
				return addonMaster.Recruit();
			}
			return false;
		}
	}

	[CompilerGenerated]
	private sealed class _1680
	{
		public string _2007_2060;

		public long _2007_2061;

		internal unsafe bool _00A0()
		{
			if (GenericHelpers.TryGetAddonMaster<AddonMaster.LookingForGroup>(out var addonMaster) && GenericHelpers.IsAddonReady(addonMaster.Base) && EzThrottler.Throttle("AH_PfRecruit"))
			{
				HuntPfController._1680(_2007_2060);
				return addonMaster.RecruitMembersOrDetails();
			}
			return false;
		}

		internal unsafe bool? _1680()
		{
			IPlayerCharacter localPlayer = AutomaticHunting.ObjectTable.LocalPlayer;
			if (localPlayer != null && localPlayer.OnlineStatus.RowId == 26)
			{
				if (GenericHelpers.TryGetAddonByName<AtkUnitBase>("LookingForGroup", out var _) && EzThrottler.Throttle("AH_PfClose"))
				{
					Chat.ExecuteCommand("/pfinder");
				}
				HuntPfController._00A0("招募已创建");
				return true;
			}
			if (Environment.TickCount64 > _2007_2061)
			{
				HuntPfController._00A0("创建招募超时（未进入招募状态），请查看 Dalamud 日志");
				return null;
			}
			return false;
		}
	}

	private static TaskManager? _1680_200A;

	private static void _00A0(string P_0)
	{
		AutomaticHunting.PrintEchoAlways(P_0);
	}

	public static bool ValidateComment(string? s, out string error)
	{
		error = string.Empty;
		if (s == null)
		{
			s = string.Empty;
		}
		if (s.Length == 0)
		{
			return true;
		}
		if (s != Chat.SanitiseText(s))
		{
			error = "招募描述包含非法字符";
			return false;
		}
		if (s.Split('\n').Length > 2)
		{
			error = "招募描述不能超过2行";
			return false;
		}
		if (Encoding.UTF8.GetBytes(s + "\0").Length > 192)
		{
			error = "招募描述过长（UTF-8 最多192字节，约60个汉字）";
			return false;
		}
		return true;
	}

	private unsafe static void _1680(string P_0)
	{
		AgentLookingForGroup* ptr = AgentLookingForGroup.Instance();
		if (ptr == null)
		{
			throw new InvalidOperationException("AgentLookingForGroup 不可用");
		}
		ptr->StoredRecruitmentInfo.CommentString = P_0 + "\0";
	}

	public unsafe static void Create(string? comment)
	{
		if (!ValidateComment(comment, out string error))
		{
			_00A0(error);
			return;
		}
		IPlayerCharacter localPlayer = AutomaticHunting.ObjectTable.LocalPlayer;
		if (localPlayer == null)
		{
			_00A0("现在无法创建招募（角色未就绪）");
			return;
		}
		if (localPlayer.OnlineStatus.RowId == 26)
		{
			_00A0("已在招募中，无需重复创建");
			return;
		}
		if (!QuestManager.IsQuestComplete(67099u) && !QuestManager.IsQuestComplete(67100u) && !QuestManager.IsQuestComplete(67101u))
		{
			_00A0("未解锁狩猎任务，无法创建招募");
			return;
		}
		if (_1680_200A == null)
		{
			_1680_200A = new TaskManager();
		}
		_1680_200A.Abort();
		TaskManagerConfiguration configuration = new TaskManagerConfiguration(2000);
		string text = comment;
		_1680_200A.Enqueue(() =>
		{
			if (GenericHelpers.TryGetAddonByName<AtkUnitBase>("LookingForGroup", out var _) && EzThrottler.Throttle("AH_PfToggle"))
			{
				Chat.ExecuteCommand("/pfinder");
			}
			return true;
		}, configuration);
		_1680_200A.Enqueue(() => !GenericHelpers.TryGetAddonByName<AtkUnitBase>("LookingForGroup", out var _), configuration);
		_1680_200A.Enqueue(() =>
		{
			if (EzThrottler.Throttle("AH_PfOpen"))
			{
				Chat.ExecuteCommand("/pfinder");
			}
			return true;
		}, configuration);
		_1680_200A.Enqueue(() =>
		{
			if (GenericHelpers.TryGetAddonMaster<AddonMaster.LookingForGroup>(out var addonMaster) && GenericHelpers.IsAddonReady(addonMaster.Base) && EzThrottler.Throttle("AH_PfRecruit"))
			{
				_1680(text);
				return addonMaster.RecruitMembersOrDetails();
			}
			return false;
		}, configuration);
		_1680_200A.Enqueue(() =>
		{
			if (GenericHelpers.TryGetAddonMaster<AddonMaster.LookingForGroupCondition>(out var addonMaster) && GenericHelpers.IsAddonReady(addonMaster.Base))
			{
				addonMaster.Normal();
				return true;
			}
			return false;
		}, configuration);
		_1680_200A.Enqueue(() =>
		{
			if (GenericHelpers.TryGetAddonMaster<AddonMaster.LookingForGroupCondition>(out var addonMaster) && GenericHelpers.IsAddonReady(addonMaster.Base))
			{
				addonMaster.SelectDutyCategory(11);
				return true;
			}
			return false;
		}, configuration);
		_1680_200A.Enqueue(() => GenericHelpers.TryGetAddonMaster<AddonMaster.LookingForGroupCondition>(out var addonMaster) && GenericHelpers.IsAddonReady(addonMaster.Base) && EzThrottler.Throttle("AH_PfConfirm", 1000) && addonMaster.Recruit(), configuration);
		long num = Environment.TickCount64 + 8000;
		_1680_200A.Enqueue(() =>
		{
			IPlayerCharacter localPlayer2 = AutomaticHunting.ObjectTable.LocalPlayer;
			if (localPlayer2 != null && localPlayer2.OnlineStatus.RowId == 26)
			{
				if (GenericHelpers.TryGetAddonByName<AtkUnitBase>("LookingForGroup", out var _) && EzThrottler.Throttle("AH_PfClose"))
				{
					Chat.ExecuteCommand("/pfinder");
				}
				_00A0("招募已创建");
				return true;
			}
			if (Environment.TickCount64 > num)
			{
				_00A0("创建招募超时（未进入招募状态），请查看 Dalamud 日志");
				return (bool?)null;
			}
			return false;
		}, new TaskManagerConfiguration(15000));
		_00A0("正在创建招募...");
	}
}
