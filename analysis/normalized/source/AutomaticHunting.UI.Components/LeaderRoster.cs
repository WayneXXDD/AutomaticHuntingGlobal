using System;
using System.Collections.Generic;

namespace AutomaticHunting.UI.Components;

public static class LeaderRoster
{
	public const int MaxVisibleRows = 10;

	public static string Normalize(string? arg0)
	{
		return (arg0 ?? string.Empty).Trim();
	}

	public static bool EnsureMigrated(PluginConfiguration arg0)
	{
		if (arg0 == null)
		{
			return false;
		}
		if (arg0.LeaderEntries == null)
		{
			arg0.LeaderEntries = new List<LeaderEntry>();
		}
		if (arg0.LeaderEntries.Count > 0)
		{
			SyncLeaderName(arg0);
			return false;
		}
		string text = Normalize(arg0.LeaderName);
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
				arg0.LeaderEntries.Add(new LeaderEntry
				{
					Name = text2,
					Enabled = true
				});
			}
		}
		SyncLeaderName(arg0);
		return arg0.LeaderEntries.Count > 0;
	}

	public static List<string> Active(PluginConfiguration arg0)
	{
		List<string> list = new List<string>();
		if (arg0?.LeaderEntries == null)
		{
			return list;
		}
		foreach (LeaderEntry leaderEntry in arg0.LeaderEntries)
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

	public static bool Add(PluginConfiguration arg0, string? arg1)
	{
		if (arg0 == null)
		{
			return false;
		}
		if (arg0.LeaderEntries == null)
		{
			arg0.LeaderEntries = new List<LeaderEntry>();
		}
		string text = Normalize(arg1);
		if (text.Length == 0)
		{
			return false;
		}
		int num = IndexOf(arg0, text);
		if (num >= 0)
		{
			if (!arg0.LeaderEntries[num].Enabled)
			{
				arg0.LeaderEntries[num].Enabled = true;
				SyncLeaderName(arg0);
			}
			return false;
		}
		arg0.LeaderEntries.Add(new LeaderEntry
		{
			Name = text,
			Enabled = true
		});
		SyncLeaderName(arg0);
		return true;
	}

	public static bool Toggle(PluginConfiguration arg0, int arg1)
	{
		if (arg0?.LeaderEntries == null || arg1 < 0 || arg1 >= arg0.LeaderEntries.Count)
		{
			return false;
		}
		arg0.LeaderEntries[arg1].Enabled = !arg0.LeaderEntries[arg1].Enabled;
		SyncLeaderName(arg0);
		return true;
	}

	public static bool SetEnabled(PluginConfiguration arg0, int arg1, bool arg2)
	{
		if (arg0?.LeaderEntries == null || arg1 < 0 || arg1 >= arg0.LeaderEntries.Count)
		{
			return false;
		}
		if (arg0.LeaderEntries[arg1].Enabled == arg2)
		{
			return false;
		}
		arg0.LeaderEntries[arg1].Enabled = arg2;
		SyncLeaderName(arg0);
		return true;
	}

	public static bool RemoveAt(PluginConfiguration arg0, int arg1)
	{
		if (arg0?.LeaderEntries == null || arg1 < 0 || arg1 >= arg0.LeaderEntries.Count)
		{
			return false;
		}
		arg0.LeaderEntries.RemoveAt(arg1);
		SyncLeaderName(arg0);
		return true;
	}

	public static bool ClearChecks(PluginConfiguration arg0)
	{
		if (arg0?.LeaderEntries == null || arg0.LeaderEntries.Count == 0)
		{
			return false;
		}
		bool result = false;
		foreach (LeaderEntry leaderEntry in arg0.LeaderEntries)
		{
			if (leaderEntry.Enabled)
			{
				leaderEntry.Enabled = false;
				result = true;
			}
		}
		SyncLeaderName(arg0);
		return result;
	}

	public static void ClearAll(PluginConfiguration arg0)
	{
		arg0?.LeaderEntries?.Clear();
		if (arg0 != null)
		{
			SyncLeaderName(arg0);
		}
	}

	public static void SyncLeaderName(PluginConfiguration arg0)
	{
		if (arg0 != null)
		{
			arg0.LeaderName = string.Join(" ", Active(arg0));
		}
	}

	public static int IndexOf(PluginConfiguration arg0, string? arg1)
	{
		string text = Normalize(arg1);
		if (arg0?.LeaderEntries == null || text.Length == 0)
		{
			return -1;
		}
		for (int i = 0; i < arg0.LeaderEntries.Count; i++)
		{
			if (string.Equals(Normalize(arg0.LeaderEntries[i].Name), text, StringComparison.OrdinalIgnoreCase))
			{
				return i;
			}
		}
		return -1;
	}

	public static (int ActiveCount, int Total) Counts(PluginConfiguration arg0)
	{
		if (arg0?.LeaderEntries == null || arg0.LeaderEntries.Count == 0)
		{
			return (ActiveCount: 0, Total: 0);
		}
		int num = 0;
		foreach (LeaderEntry leaderEntry in arg0.LeaderEntries)
		{
			if (leaderEntry.Enabled && Normalize(leaderEntry.Name).Length > 0)
			{
				num++;
			}
		}
		return (ActiveCount: num, Total: arg0.LeaderEntries.Count);
	}
}
