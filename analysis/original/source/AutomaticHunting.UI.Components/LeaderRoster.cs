using System;
using System.Collections.Generic;

namespace AutomaticHunting.UI.Components;

public static class LeaderRoster
{
	public const int MaxVisibleRows = 10;

	public static string Normalize(string? name)
	{
		return (name ?? string.Empty).Trim();
	}

	public static bool EnsureMigrated(PluginConfiguration config)
	{
		if (config == null)
		{
			return false;
		}
		if (config.LeaderEntries == null)
		{
			config.LeaderEntries = new List<LeaderEntry>();
		}
		if (config.LeaderEntries.Count > 0)
		{
			SyncLeaderName(config);
			return false;
		}
		string text = Normalize(config.LeaderName);
		if (text.Length == 0)
		{
			return false;
		}
		string[] array = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
		for (int i = 0; i < array.Length; i++)
		{
			string text2 = Normalize(array[i]);
			if (text2.Length != 0)
			{
				config.LeaderEntries.Add(new LeaderEntry
				{
					Name = text2,
					Enabled = true
				});
			}
		}
		SyncLeaderName(config);
		return config.LeaderEntries.Count > 0;
	}

	public static List<string> Active(PluginConfiguration config)
	{
		List<string> list = new List<string>();
		if (config?.LeaderEntries == null)
		{
			return list;
		}
		foreach (LeaderEntry leaderEntry in config.LeaderEntries)
		{
			if (leaderEntry != null && leaderEntry.Enabled)
			{
				string text = Normalize(leaderEntry.Name);
				if (text.Length > 0)
				{
					list.Add(text);
				}
			}
		}
		return list;
	}

	public static bool Add(PluginConfiguration config, string? rawName)
	{
		if (config == null)
		{
			return false;
		}
		if (config.LeaderEntries == null)
		{
			config.LeaderEntries = new List<LeaderEntry>();
		}
		string text = Normalize(rawName);
		if (text.Length == 0)
		{
			return false;
		}
		int num = IndexOf(config, text);
		if (num >= 0)
		{
			if (!config.LeaderEntries[num].Enabled)
			{
				config.LeaderEntries[num].Enabled = true;
				SyncLeaderName(config);
			}
			return false;
		}
		config.LeaderEntries.Add(new LeaderEntry
		{
			Name = text,
			Enabled = true
		});
		SyncLeaderName(config);
		return true;
	}

	public static bool Toggle(PluginConfiguration config, int index)
	{
		if (config?.LeaderEntries == null || index < 0 || index >= config.LeaderEntries.Count)
		{
			return false;
		}
		config.LeaderEntries[index].Enabled = !config.LeaderEntries[index].Enabled;
		SyncLeaderName(config);
		return true;
	}

	public static bool SetEnabled(PluginConfiguration config, int index, bool enabled)
	{
		if (config?.LeaderEntries == null || index < 0 || index >= config.LeaderEntries.Count)
		{
			return false;
		}
		if (config.LeaderEntries[index].Enabled == enabled)
		{
			return false;
		}
		config.LeaderEntries[index].Enabled = enabled;
		SyncLeaderName(config);
		return true;
	}

	public static bool RemoveAt(PluginConfiguration config, int index)
	{
		if (config?.LeaderEntries == null || index < 0 || index >= config.LeaderEntries.Count)
		{
			return false;
		}
		config.LeaderEntries.RemoveAt(index);
		SyncLeaderName(config);
		return true;
	}

	public static bool ClearChecks(PluginConfiguration config)
	{
		if (config?.LeaderEntries == null || config.LeaderEntries.Count == 0)
		{
			return false;
		}
		bool result = false;
		foreach (LeaderEntry leaderEntry in config.LeaderEntries)
		{
			if (leaderEntry.Enabled)
			{
				leaderEntry.Enabled = false;
				result = true;
			}
		}
		SyncLeaderName(config);
		return result;
	}

	public static void ClearAll(PluginConfiguration config)
	{
		config?.LeaderEntries?.Clear();
		if (config != null)
		{
			SyncLeaderName(config);
		}
	}

	public static void SyncLeaderName(PluginConfiguration config)
	{
		if (config != null)
		{
			config.LeaderName = string.Join(" ", Active(config));
		}
	}

	public static int IndexOf(PluginConfiguration config, string? rawName)
	{
		string text = Normalize(rawName);
		if (config?.LeaderEntries == null || text.Length == 0)
		{
			return -1;
		}
		for (int i = 0; i < config.LeaderEntries.Count; i++)
		{
			if (string.Equals(Normalize(config.LeaderEntries[i].Name), text, StringComparison.OrdinalIgnoreCase))
			{
				return i;
			}
		}
		return -1;
	}

	public static (int ActiveCount, int Total) Counts(PluginConfiguration config)
	{
		if (config?.LeaderEntries == null || config.LeaderEntries.Count == 0)
		{
			return (ActiveCount: 0, Total: 0);
		}
		int num = 0;
		foreach (LeaderEntry leaderEntry in config.LeaderEntries)
		{
			if (leaderEntry.Enabled && Normalize(leaderEntry.Name).Length > 0)
			{
				num++;
			}
		}
		return (ActiveCount: num, Total: config.LeaderEntries.Count);
	}
}
