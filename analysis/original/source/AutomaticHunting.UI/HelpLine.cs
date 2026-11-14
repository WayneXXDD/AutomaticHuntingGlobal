namespace AutomaticHunting.UI;

public sealed class HelpLine
{
	public string Text = string.Empty;

	public string? Url;

	public bool IsLink => !string.IsNullOrEmpty(Url);
}
