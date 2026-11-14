namespace AutomaticHunting.UI.Pages;

public abstract class SettingsPage
{
	protected readonly AutomaticHunting Plugin;

	protected PluginConfiguration Config => Plugin.Config;

	protected SettingsPage(AutomaticHunting arg0)
	{
		Plugin = arg0;
	}

	protected void Save()
	{
		Plugin.ConfigService.MarkDirty();
	}

	protected static string FormatPreset(WorldTravelPreset arg0)
	{
		return $"{arg0.ServerName}  {arg0.ExVersion + 2}.0  {arg0.MapName} · {arg0.AetheryteName}";
	}

	protected static WorldTravelPreset ClonePreset(WorldTravelPreset arg0)
	{
		return new WorldTravelPreset
		{
			ServerName = arg0.ServerName,
			ExVersion = arg0.ExVersion,
			MapRowId = arg0.MapRowId,
			MapName = arg0.MapName,
			AetheryteId = arg0.AetheryteId,
			AetheryteName = arg0.AetheryteName
		};
	}

	protected static HuntChainStep CloneStep(HuntChainStep arg0)
	{
		return new HuntChainStep
		{
			Preset = ClonePreset(arg0.Preset),
			KillTarget = arg0.KillTarget
		};
	}

	public abstract void Draw();
}
