namespace AutomaticHunting.UI.Components;

public static class TravelSavePolicy
{
	public static bool CanSave(bool hasServer, bool hasMap, bool hasAetheryte)
	{
		return hasServer & hasMap & hasAetheryte;
	}

	public static string? DisabledReason(bool hasServer, bool hasMap, bool hasAetheryte)
	{
		if (!hasServer)
		{
			return "请先选择服务器";
		}
		if (!hasMap)
		{
			return "请先选择地图";
		}
		if (!hasAetheryte)
		{
			return "请先选择水晶";
		}
		return null;
	}
}
