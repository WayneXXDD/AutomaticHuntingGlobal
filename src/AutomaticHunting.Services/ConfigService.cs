using System;
using Dalamud.Plugin;

namespace AutomaticHunting.Services;

public sealed class ConfigService
{
	private const long field_1188 = 5000000L;

	private readonly PluginConfiguration field_1189;

	private readonly IDalamudPluginInterface field_1190;

	private bool field_1191;

	private long field_1192;

	public ConfigService(PluginConfiguration arg0, IDalamudPluginInterface arg1)
	{
		field_1189 = arg0;
		field_1190 = arg1;
		field_1192 = DateTime.UtcNow.Ticks;
	}

	public void MarkDirty()
	{
		field_1191 = true;
	}

	public void Save()
	{
		field_1191 = false;
		field_1192 = DateTime.UtcNow.Ticks;
		try
		{
			field_1190.SavePluginConfig(field_1189);
		}
		catch (Exception exception)
		{
			AutomaticHunting.Log.Error(exception, "自動狩獵跟車：儲存設定失敗");
		}
	}

	public void Tick()
	{
		if (field_1191 && DateTime.UtcNow.Ticks - field_1192 >= 5000000)
		{
			Save();
		}
	}
}
