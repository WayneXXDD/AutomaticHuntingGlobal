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
			return "請先選擇伺服器";
		}
		if (!arg1)
		{
			return "請先選擇地圖";
		}
		if (!arg2)
		{
			return "請先選擇水晶";
		}
		return null;
	}
}
