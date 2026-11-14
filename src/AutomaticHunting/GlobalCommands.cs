using System;
using System.Linq;

namespace AutomaticHunting;

internal static class GlobalCommands
{
    internal static void Process(string command)
    {
        if (command.StartsWith(GlobalCommandPolicy.InstancePrefix, StringComparison.OrdinalIgnoreCase))
        {
            if (!GlobalCommandPolicy.TryParseInstance(command, out var instance))
                throw new ArgumentException("Global instance number must be 1–9.");
            RequireLifestream();
            AutomaticHunting.PluginInterface.GetIpcSubscriber<int, object>("Lifestream.ChangeInstance").InvokeAction(instance);
            return;
        }
        if (command.StartsWith(GlobalCommandPolicy.WorldPrefix, StringComparison.OrdinalIgnoreCase))
        {
            var world = command[GlobalCommandPolicy.WorldPrefix.Length..].Trim();
            if (!CrossWorldData.ServerWorldTravel.ContainsKey(world))
                throw new ArgumentException("Unknown Global world: " + world);
            RequireLifestream();
            if (!AutomaticHunting.PluginInterface.GetIpcSubscriber<string, bool>("Lifestream.ChangeWorld").InvokeFunc(world))
                throw new InvalidOperationException("Lifestream cannot currently visit " + world + ".");
            return;
        }
        AutomaticHunting.CommandManager.ProcessCommand(command);
    }

    private static void RequireLifestream()
    {
        if (!AutomaticHunting.PluginInterface.InstalledPlugins.Any(p => p.IsLoaded && p.InternalName.Equals("Lifestream", StringComparison.OrdinalIgnoreCase)))
            throw new InvalidOperationException("Install and enable Lifestream for world and instance changes.");
    }
}
