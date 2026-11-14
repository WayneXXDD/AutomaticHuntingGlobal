using System;
using Dalamud.Plugin;

namespace AutomaticHunting.Services;

public sealed class ConfigService
{
	private const long _2001_206D = 5000000L;

	private readonly PluginConfiguration _2001_206E;

	private readonly IDalamudPluginInterface _2001_206F;

	private bool _2001_3000;

	private long _2002_00A0;

	public ConfigService(PluginConfiguration config, IDalamudPluginInterface pluginInterface)
	{
		_2001_206E = config;
		_2001_206F = pluginInterface;
		_2002_00A0 = DateTime.UtcNow.Ticks;
	}

	public void MarkDirty()
	{
		_2001_3000 = true;
	}

	public void Save()
	{
		_2001_3000 = false;
		_2002_00A0 = DateTime.UtcNow.Ticks;
		try
		{
			_2001_206F.SavePluginConfig(_2001_206E);
		}
		catch (Exception exception)
		{
			AutomaticHunting.Log.Error(exception, "AutomaticHunting: 保存配置失败");
		}
	}

	public void Tick()
	{
		if (_2001_3000 && DateTime.UtcNow.Ticks - _2002_00A0 >= 5000000)
		{
			Save();
		}
	}
}
