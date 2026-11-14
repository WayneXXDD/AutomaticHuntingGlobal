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
                throw new ArgumentException("國際服分流編號必須為 1 至 9。");
            RequireLifestream();
            AutomaticHunting.PluginInterface.GetIpcSubscriber<int, object>("Lifestream.ChangeInstance").InvokeAction(instance);
            return;
        }
        if (command.StartsWith(GlobalCommandPolicy.WorldPrefix, StringComparison.OrdinalIgnoreCase))
        {
            var world = command[GlobalCommandPolicy.WorldPrefix.Length..].Trim();
            if (!CrossWorldData.ServerWorldTravel.ContainsKey(world))
                throw new ArgumentException("找不到指定的國際服伺服器：" + world);
            RequireLifestream();
            if (!AutomaticHunting.PluginInterface.GetIpcSubscriber<string, bool>("Lifestream.ChangeWorld").InvokeFunc(world))
                throw new InvalidOperationException("Lifestream 目前無法前往伺服器：" + world + ".");
            return;
        }
        AutomaticHunting.CommandManager.ProcessCommand(command);
    }

    private static void RequireLifestream()
    {
        if (!AutomaticHunting.PluginInterface.InstalledPlugins.Any(p => p.IsLoaded && p.InternalName.Equals("Lifestream", StringComparison.OrdinalIgnoreCase)))
            throw new InvalidOperationException("請先安裝並啟用 Lifestream，才能跨服或切換分流。");
    }
}
