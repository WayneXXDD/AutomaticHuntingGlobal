using System;
using AutomaticHunting.UI.Components;
using Dalamud.Game.ClientState.Objects.SubKinds;
using Dalamud.Game.Gui.ContextMenu;
using Dalamud.Game.Text;
using Dalamud.Game.Text.SeStringHandling;

namespace AutomaticHunting;

public sealed class HuntUIController : IDisposable
{
	private readonly AutomaticHunting _1680_200B;

	private readonly MenuItem _1680_2010;

	public HuntUIController(AutomaticHunting plugin)
	{
		_1680_200B = plugin;
		_1680_2010 = new MenuItem
		{
			Name = new SeStringBuilder().AddUiForeground("AH 添加为车头", 578).Build(),
			Prefix = SeIconChar.BoxedLetterA,
			PrefixColor = 578,
			OnClicked = _00A0
		};
		AutomaticHunting.ContextMenu.OnMenuOpened += _00A0;
		AutomaticHunting.Log.Debug("右键菜单控制器已初始化");
	}

	private static bool _00A0(MenuTargetDefault P_0)
	{
		if (P_0.TargetObject != null)
		{
			return P_0.TargetObject is IPlayerCharacter;
		}
		if (P_0.TargetContentId != 0L)
		{
			return !string.IsNullOrWhiteSpace(P_0.TargetName);
		}
		return false;
	}

	private void _00A0(IMenuOpenedArgs P_0)
	{
		AutomaticHunting.Log.Debug($"右键菜单打开: addon={P_0.AddonName}, type={P_0.MenuType}, targetType={P_0.Target?.GetType().Name}");
		if (_1680_200B.Config.EnableContextMenu && P_0.MenuType == ContextMenuType.Default && P_0.Target is MenuTargetDefault menuTargetDefault && _00A0(menuTargetDefault))
		{
			P_0.AddMenuItem(_1680_2010);
			AutomaticHunting.Log.Debug("应答右键菜单添加车头选项: addon=" + P_0.AddonName + ", target=" + menuTargetDefault.TargetName);
		}
	}

	private void _00A0(IMenuItemClickedArgs P_0)
	{
		if (P_0.MenuType == ContextMenuType.Default && P_0.Target is MenuTargetDefault menuTargetDefault && _00A0(menuTargetDefault) && !string.IsNullOrWhiteSpace(menuTargetDefault.TargetName))
		{
			string text = menuTargetDefault.TargetName.ToString();
			LeaderRoster.Add(_1680_200B.Config, text);
			_1680_200B.ConfigService.Save();
			AutomaticHunting.Log.Debug("右键菜单设置车头成功: " + text);
			_1680_200B.PrintEcho("车头已设置为: " + text);
		}
	}

	public void Dispose()
	{
		AutomaticHunting.ContextMenu.OnMenuOpened -= _00A0;
	}
}
