// Only the persisted roster fields are needed to exercise the production roster.
namespace AutomaticHunting;

public sealed class PluginConfiguration
{
    public string LeaderName { get; set; } = "";
    public List<LeaderEntry> LeaderEntries { get; set; } = new();
}
