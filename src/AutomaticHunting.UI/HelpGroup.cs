using System.Collections.Generic;

namespace AutomaticHunting.UI;

public sealed class HelpGroup
{
	public string Id = string.Empty;

	public string Title = string.Empty;

	public List<HelpSection> Sections = new List<HelpSection>();
}
