using System;
using AutomaticHunting.UI.Components;
using Dalamud.Game.ClientState.Objects.SubKinds;
using Dalamud.Game.Gui.ContextMenu;
using Dalamud.Game.Text;
using Dalamud.Game.Text.SeStringHandling;

namespace AutomaticHunting;

public sealed class HuntUIController : IDisposable
{
	private readonly AutomaticHunting field_558;

	private readonly MenuItem field_559;

	public HuntUIController(AutomaticHunting arg0)
	{
		field_558 = arg0;
		field_559 = new MenuItem
		{
			Name = new SeStringBuilder().AddUiForeground("AH 添加为车头", 578).Build(),
			Prefix = SeIconChar.BoxedLetterA,
			PrefixColor = 578,
			OnClicked = Method_461
		};
		AutomaticHunting.ContextMenu.OnMenuOpened += Method_460;
		AutomaticHunting.Log.Debug("右键菜单控制器已初始化");
	}

	private static bool Method_459(MenuTargetDefault arg0)
	{
		if (arg0.TargetObject != null)
		{
			return arg0.TargetObject is IPlayerCharacter;
		}
		if (arg0.TargetContentId != 0L)
		{
			return !string.IsNullOrWhiteSpace(arg0.TargetName);
		}
		return false;
	}

	private void Method_460(IMenuOpenedArgs arg0)
	{
		AutomaticHunting.Log.Debug($"右键菜单打开: addon={arg0.AddonName}, type={arg0.MenuType}, targetType={arg0.Target?.GetType().Name}");
		if (field_558.Config.EnableContextMenu && arg0.MenuType == ContextMenuType.Default && arg0.Target is MenuTargetDefault menuTargetDefault && Method_459(menuTargetDefault))
		{
			arg0.AddMenuItem(field_559);
			AutomaticHunting.Log.Debug("应答右键菜单添加车头选项: addon=" + arg0.AddonName + ", target=" + menuTargetDefault.TargetName);
		}
	}

	private void Method_461(IMenuItemClickedArgs arg0)
	{
		if (arg0.MenuType == ContextMenuType.Default && arg0.Target is MenuTargetDefault menuTargetDefault && Method_459(menuTargetDefault) && !string.IsNullOrWhiteSpace(menuTargetDefault.TargetName))
		{
			string text = menuTargetDefault.TargetName.ToString();
			LeaderRoster.Add(field_558.Config, text);
			field_558.ConfigService.Save();
			AutomaticHunting.Log.Debug("右键菜单设置车头成功: " + text);
			field_558.PrintEcho("车头已设置为: " + text);
		}
	}

	public void Dispose()
	{
		AutomaticHunting.ContextMenu.OnMenuOpened -= Method_460;
	}
}
