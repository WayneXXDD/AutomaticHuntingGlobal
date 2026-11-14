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
	private sealed class RecoveredType_90
	{
		public static readonly RecoveredType_90 field_549 = new RecoveredType_90();

		public static Func<bool> field_550;

		public static Func<bool> field_551;

		public static Func<bool> field_552;

		public static Func<bool> field_553;

		public static Func<bool> field_554;

		public static Func<bool> field_555;

		internal unsafe bool Method_449()
		{
			if (GenericHelpers.TryGetAddonByName<AtkUnitBase>("LookingForGroup", out var _) && EzThrottler.Throttle("AH_PfToggle"))
			{
				Chat.ExecuteCommand("/pfinder");
			}
			return true;
		}

		internal unsafe bool Method_450()
		{
			AtkUnitBase* AddonPtr;
			return !GenericHelpers.TryGetAddonByName<AtkUnitBase>("LookingForGroup", out AddonPtr);
		}

		internal bool Method_451()
		{
			if (EzThrottler.Throttle("AH_PfOpen"))
			{
				Chat.ExecuteCommand("/pfinder");
			}
			return true;
		}

		internal unsafe bool Method_452()
		{
			if (GenericHelpers.TryGetAddonMaster<AddonMaster.LookingForGroupCondition>(out var addonMaster) && GenericHelpers.IsAddonReady(addonMaster.Base))
			{
				addonMaster.Normal();
				return true;
			}
			return false;
		}

		internal unsafe bool Method_453()
		{
			if (GenericHelpers.TryGetAddonMaster<AddonMaster.LookingForGroupCondition>(out var addonMaster) && GenericHelpers.IsAddonReady(addonMaster.Base))
			{
				addonMaster.SelectDutyCategory(11);
				return true;
			}
			return false;
		}

		internal unsafe bool Method_454()
		{
			if (GenericHelpers.TryGetAddonMaster<AddonMaster.LookingForGroupCondition>(out var addonMaster) && GenericHelpers.IsAddonReady(addonMaster.Base) && EzThrottler.Throttle("AH_PfConfirm", 1000))
			{
				return addonMaster.Recruit();
			}
			return false;
		}
	}

	[CompilerGenerated]
	private sealed class RecoveredType_91
	{
		public string field_556;

		public long field_557;

		internal unsafe bool Method_456()
		{
			if (GenericHelpers.TryGetAddonMaster<AddonMaster.LookingForGroup>(out var addonMaster) && GenericHelpers.IsAddonReady(addonMaster.Base) && EzThrottler.Throttle("AH_PfRecruit"))
			{
				Method_445(field_556);
				return addonMaster.RecruitMembersOrDetails();
			}
			return false;
		}

		internal unsafe bool? Method_457()
		{
			IPlayerCharacter localPlayer = AutomaticHunting.ObjectTable.LocalPlayer;
			if (localPlayer != null && localPlayer.OnlineStatus.RowId == 26)
			{
				if (GenericHelpers.TryGetAddonByName<AtkUnitBase>("LookingForGroup", out var _) && EzThrottler.Throttle("AH_PfClose"))
				{
					Chat.ExecuteCommand("/pfinder");
				}
				Method_443("招募已创建");
				return true;
			}
			if (Environment.TickCount64 > field_557)
			{
				Method_443("创建招募超时（未进入招募状态），请查看 Dalamud 日志");
				return null;
			}
			return false;
		}
	}

	private static TaskManager? field_548;

	private static void Method_443(string arg0)
	{
		AutomaticHunting.PrintEchoAlways(arg0);
	}

	public static bool ValidateComment(string? arg0, out string arg1)
	{
		arg1 = string.Empty;
		if (arg0 == null)
		{
			arg0 = string.Empty;
		}
		if (arg0.Length == 0)
		{
			return true;
		}
		if (arg0 != Chat.SanitiseText(arg0))
		{
			arg1 = "招募描述包含非法字符";
			return false;
		}
		if (arg0.Split('\n').Length > 2)
		{
			arg1 = "招募描述不能超过2行";
			return false;
		}
		if (Encoding.UTF8.GetBytes(arg0 + "\0").Length > 192)
		{
			arg1 = "招募描述过长（UTF-8 最多192字节，约60个汉字）";
			return false;
		}
		return true;
	}

	private unsafe static void Method_445(string arg0)
	{
		AgentLookingForGroup* ptr = AgentLookingForGroup.Instance();
		if (ptr == null)
		{
			throw new InvalidOperationException("AgentLookingForGroup 不可用");
		}
		ptr->StoredRecruitmentInfo.CommentString = arg0 + "\0";
	}

	public unsafe static void Create(string? arg0)
	{
		if (!ValidateComment(arg0, out string arg1))
		{
			Method_443(arg1);
			return;
		}
		IPlayerCharacter localPlayer = AutomaticHunting.ObjectTable.LocalPlayer;
		if (localPlayer == null)
		{
			Method_443("现在无法创建招募（角色未就绪）");
			return;
		}
		if (localPlayer.OnlineStatus.RowId == 26)
		{
			Method_443("已在招募中，无需重复创建");
			return;
		}
		if (!QuestManager.IsQuestComplete(67099u) && !QuestManager.IsQuestComplete(67100u) && !QuestManager.IsQuestComplete(67101u))
		{
			Method_443("未解锁狩猎任务，无法创建招募");
			return;
		}
		if (field_548 == null)
		{
			field_548 = new TaskManager();
		}
		field_548.Abort();
		TaskManagerConfiguration configuration = new TaskManagerConfiguration(2000);
		string field_556 = arg0;
		field_548.Enqueue(() =>
		{
			if (GenericHelpers.TryGetAddonByName<AtkUnitBase>("LookingForGroup", out var _) && EzThrottler.Throttle("AH_PfToggle"))
			{
				Chat.ExecuteCommand("/pfinder");
			}
			return true;
		}, configuration);
		field_548.Enqueue(() => !GenericHelpers.TryGetAddonByName<AtkUnitBase>("LookingForGroup", out var _), configuration);
		field_548.Enqueue(() =>
		{
			if (EzThrottler.Throttle("AH_PfOpen"))
			{
				Chat.ExecuteCommand("/pfinder");
			}
			return true;
		}, configuration);
		field_548.Enqueue(() =>
		{
			if (GenericHelpers.TryGetAddonMaster<AddonMaster.LookingForGroup>(out var addonMaster) && GenericHelpers.IsAddonReady(addonMaster.Base) && EzThrottler.Throttle("AH_PfRecruit"))
			{
				Method_445(field_556);
				return addonMaster.RecruitMembersOrDetails();
			}
			return false;
		}, configuration);
		field_548.Enqueue(() =>
		{
			if (GenericHelpers.TryGetAddonMaster<AddonMaster.LookingForGroupCondition>(out var addonMaster) && GenericHelpers.IsAddonReady(addonMaster.Base))
			{
				addonMaster.Normal();
				return true;
			}
			return false;
		}, configuration);
		field_548.Enqueue(() =>
		{
			if (GenericHelpers.TryGetAddonMaster<AddonMaster.LookingForGroupCondition>(out var addonMaster) && GenericHelpers.IsAddonReady(addonMaster.Base))
			{
				addonMaster.SelectDutyCategory(11);
				return true;
			}
			return false;
		}, configuration);
		field_548.Enqueue(() => GenericHelpers.TryGetAddonMaster<AddonMaster.LookingForGroupCondition>(out var addonMaster) && GenericHelpers.IsAddonReady(addonMaster.Base) && EzThrottler.Throttle("AH_PfConfirm", 1000) && addonMaster.Recruit(), configuration);
		long field_557 = Environment.TickCount64 + 8000;
		field_548.Enqueue(() =>
		{
			IPlayerCharacter localPlayer2 = AutomaticHunting.ObjectTable.LocalPlayer;
			if (localPlayer2 != null && localPlayer2.OnlineStatus.RowId == 26)
			{
				if (GenericHelpers.TryGetAddonByName<AtkUnitBase>("LookingForGroup", out var _) && EzThrottler.Throttle("AH_PfClose"))
				{
					Chat.ExecuteCommand("/pfinder");
				}
				Method_443("招募已创建");
				return true;
			}
			if (Environment.TickCount64 > field_557)
			{
				Method_443("创建招募超时（未进入招募状态），请查看 Dalamud 日志");
				return (bool?)null;
			}
			return false;
		}, new TaskManagerConfiguration(15000));
		Method_443("正在创建招募...");
	}
}
