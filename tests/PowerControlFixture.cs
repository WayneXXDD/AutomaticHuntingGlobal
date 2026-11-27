using System;
using System.Collections.Generic;
using System.Linq;

// Minimal IPC/command adapters exercise the production power gate without a game client.
namespace AutomaticHunting
{
    internal static class AutomaticHunting
    {
        public static readonly PowerTestInterface PluginInterface = new();
        public static readonly PowerTestCommands CommandManager = new();
        public static readonly PowerTestLog Log = new();
    }

    internal static class CrossWorldData
    {
        public static readonly Dictionary<string, string> ServerWorldTravel = new() { ["Test World"] = "/ahglobal world Test World" };
    }

    internal sealed class PowerTestInterface
    {
        public bool Busy;
        public bool AcceptWorld = true;
        public int AbortCount;
        public int InstanceCount;
        public int WorldCount;
        public IEnumerable<PowerTestPlugin> InstalledPlugins => new[] { new PowerTestPlugin("Lifestream", true) };
        public PowerTestSubscriber<T> GetIpcSubscriber<T>(string name) => new(this, name);
        public PowerTestSubscriber<T1, T2> GetIpcSubscriber<T1, T2>(string name) => new(this, name);
    }
    internal record PowerTestPlugin(string InternalName, bool IsLoaded);
    internal sealed class PowerTestSubscriber<T>(PowerTestInterface host, string name)
    {
        public T InvokeFunc()
        {
            if (name != "Lifestream.IsBusy") throw new Exception("Unexpected IPC: " + name);
            return (T)(object)host.Busy;
        }
        public void InvokeAction()
        {
            if (name != "Lifestream.Abort") throw new Exception("Unexpected IPC: " + name);
            host.AbortCount++;
            host.Busy = false;
        }
    }
    internal sealed class PowerTestSubscriber<T1, T2>(PowerTestInterface host, string name)
    {
        public void InvokeAction(T1 value)
        {
            if (name != "Lifestream.ChangeInstance") throw new Exception("Unexpected IPC: " + name);
            host.InstanceCount++;
            host.Busy = true;
        }
        public T2 InvokeFunc(T1 value)
        {
            if (name != "Lifestream.ChangeWorld") throw new Exception("Unexpected IPC: " + name);
            host.WorldCount++;
            if (host.AcceptWorld) host.Busy = true;
            return (T2)(object)host.AcceptWorld;
        }
    }
    internal sealed class PowerTestCommands
    {
        public readonly List<string> Commands = new();
        public void ProcessCommand(string command) => Commands.Add(command);
    }
    internal sealed class PowerTestLog
    {
        public void Warning(Exception error, string message) => throw new Exception(message, error);
    }
}

namespace AutomaticHunting.UI.Components
{
    public enum UiVariant { Primary, Danger }
    public static class UiButton
    {
        public static string Label = "";
        public static UiVariant Variant;
        public static bool Click;
        public static bool Draw(string label, UiVariant variant, System.Numerics.Vector2 size, string tooltip)
        {
            Label = label;
            Variant = variant;
            return Click;
        }
    }
}
