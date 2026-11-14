using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace AutomaticHunting.UI;

public static class HelpDocument
{
	public const string ResourceName = "AutomaticHunting.Help.help.json";

	private static readonly object _2000_202E = new object();

	private static List<HelpGroup>? _2000_202F;

	public static IReadOnlyList<HelpGroup> Groups
	{
		get
		{
			lock (_2000_202E)
			{
				if (_2000_202F != null)
				{
					return _2000_202F;
				}
				_2000_202F = _00A0();
				return _2000_202F;
			}
		}
	}

	private static List<HelpGroup> _00A0()
	{
		List<HelpGroup> list = new List<HelpGroup>();
		try
		{
			using Stream stream = typeof(HelpDocument).Assembly.GetManifestResourceStream("AutomaticHunting.Help.help.json");
			if (stream == null)
			{
				AutomaticHunting.Log.Warning("AutomaticHunting: 找不到内嵌帮助资源 AutomaticHunting.Help.help.json");
				return list;
			}
			using JsonDocument jsonDocument = JsonDocument.Parse(stream);
			if (!jsonDocument.RootElement.TryGetProperty("groups", out var value))
			{
				return list;
			}
			foreach (JsonElement item in value.EnumerateArray())
			{
				HelpGroup helpGroup = new HelpGroup
				{
					Id = (item.TryGetProperty("id", out var value2) ? (value2.GetString() ?? string.Empty) : string.Empty),
					Title = (item.TryGetProperty("title", out var value3) ? (value3.GetString() ?? string.Empty) : string.Empty)
				};
				if (item.TryGetProperty("sections", out var value4))
				{
					foreach (JsonElement item2 in value4.EnumerateArray())
					{
						HelpSection helpSection = new HelpSection
						{
							Title = (item2.TryGetProperty("title", out var value5) ? (value5.GetString() ?? string.Empty) : string.Empty)
						};
						if (item2.TryGetProperty("body", out var value6))
						{
							foreach (JsonElement item3 in value6.EnumerateArray())
							{
								if (item3.ValueKind == JsonValueKind.String)
								{
									helpSection.Body.Add(new HelpLine
									{
										Text = (item3.GetString() ?? string.Empty)
									});
								}
								else if (item3.ValueKind == JsonValueKind.Object)
								{
									string text = (item3.TryGetProperty("t", out var value7) ? (value7.GetString() ?? string.Empty) : string.Empty);
									string text2 = (item3.TryGetProperty("u", out var value8) ? (value8.GetString() ?? string.Empty) : string.Empty);
									if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2))
									{
										helpSection.Body.Add(new HelpLine
										{
											Text = text,
											Url = text2
										});
									}
								}
							}
						}
						helpGroup.Sections.Add(helpSection);
					}
				}
				list.Add(helpGroup);
			}
		}
		catch (Exception exception)
		{
			AutomaticHunting.Log.Error(exception, "AutomaticHunting: 内嵌帮助解析失败");
		}
		return list;
	}
}
