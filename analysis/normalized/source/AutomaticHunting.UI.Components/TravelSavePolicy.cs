namespace AutomaticHunting.UI.Components;

public static class TravelSavePolicy
{
	public static bool CanSave(bool arg0, bool arg1, bool arg2)
	{
		return arg0 & arg1 & arg2;
	}

	public static string? DisabledReason(bool arg0, bool arg1, bool arg2)
	{
		if (!arg0)
		{
			return "请先选择服务器";
		}
		if (!arg1)
		{
			return "请先选择地图";
		}
		if (!arg2)
		{
			return "请先选择水晶";
		}
		return null;
	}
}
