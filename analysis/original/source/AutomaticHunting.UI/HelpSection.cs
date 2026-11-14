using System.Collections.Generic;

namespace AutomaticHunting.UI;

public sealed class HelpSection
{
	public string Title = string.Empty;

	public List<HelpLine> Body = new List<HelpLine>();
}
