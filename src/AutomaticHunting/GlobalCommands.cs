using System;
using System.Linq;

namespace AutomaticHunting;

internal static class GlobalCommands
{
    internal static Func<bool> AutomationAllowed = () => false;
    private static bool ownsTravel;
    private static DateTime nextTravelCheck;

    internal static void Process(string command)
    {
        if (!AutomationAllowed() && command != "/vnav stop" && command != "/bmrai off") return;
        if (command.StartsWith(GlobalCommandPolicy.InstancePrefix, StringComparison.OrdinalIgnoreCase))
        {
            if (!GlobalCommandPolicy.TryParseInstance(command, out var instance))
                throw new ArgumentException("國際服分流編號必須為 1 至 9。");
            RequireLifestream();
            if (TravelIsBusy()) return;
            AutomaticHunting.PluginInterface.GetIpcSubscriber<int, object>("Lifestream.ChangeInstance").InvokeAction(instance);
            ownsTravel = true;
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
            ownsTravel = true;
            return;
        }
        AutomaticHunting.CommandManager.ProcessCommand(command);
    }

    internal static void ObserveTravelCompletion()
    {
        if (!ownsTravel || DateTime.UtcNow < nextTravelCheck) return;
        nextTravelCheck = DateTime.UtcNow.AddMilliseconds(500);
        try { if (!TravelIsBusy()) ownsTravel = false; }
        catch { ownsTravel = false; }
    }

    internal static void StopOwnedTravel()
    {
        if (!ownsTravel) return;
        ownsTravel = false;
        try
        {
            if (TravelIsBusy())
                AutomaticHunting.PluginInterface.GetIpcSubscriber<object>("Lifestream.Abort").InvokeAction();
        }
        catch (Exception exception)
        {
            AutomaticHunting.Log.Warning(exception, "停止本插件發起的跨服或切線失敗");
        }
    }

    private static bool TravelIsBusy() =>
        AutomaticHunting.PluginInterface.GetIpcSubscriber<bool>("Lifestream.IsBusy").InvokeFunc();

    private static void RequireLifestream()
    {
        if (!AutomaticHunting.PluginInterface.InstalledPlugins.Any(p => p.IsLoaded && p.InternalName.Equals("Lifestream", StringComparison.OrdinalIgnoreCase)))
            throw new InvalidOperationException("請先安裝並啟用 Lifestream，才能跨服或切換分流。");
    }
}
