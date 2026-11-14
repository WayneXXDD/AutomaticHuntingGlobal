namespace AutomaticHunting.UI.Pages;

public abstract class SettingsPage
{
	protected readonly AutomaticHunting Plugin;

	protected PluginConfiguration Config => Plugin.Config;

	protected SettingsPage(AutomaticHunting plugin)
	{
		Plugin = plugin;
	}

	protected void Save()
	{
		Plugin.ConfigService.MarkDirty();
	}

	protected static string FormatPreset(WorldTravelPreset preset)
	{
		return $"{preset.ServerName}  {preset.ExVersion + 2}.0  {preset.MapName} · {preset.AetheryteName}";
	}

	protected static WorldTravelPreset ClonePreset(WorldTravelPreset source)
	{
		return new WorldTravelPreset
		{
			ServerName = source.ServerName,
			ExVersion = source.ExVersion,
			MapRowId = source.MapRowId,
			MapName = source.MapName,
			AetheryteId = source.AetheryteId,
			AetheryteName = source.AetheryteName
		};
	}

	protected static HuntChainStep CloneStep(HuntChainStep source)
	{
		return new HuntChainStep
		{
			Preset = ClonePreset(source.Preset),
			KillTarget = source.KillTarget
		};
	}

	public abstract void Draw();
}
