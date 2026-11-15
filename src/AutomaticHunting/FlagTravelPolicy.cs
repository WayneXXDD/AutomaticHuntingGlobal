using System.Numerics;

namespace AutomaticHunting;

public enum FlagTravelAction { Automatic, DirectNavigation, WaitForTerritory, Reject }

public static class FlagTravelPolicy
{
    public static FlagTravelAction Decide(bool autoTeleport, uint currentTerritory, uint targetTerritory)
    {
        if (targetTerritory == 0) return FlagTravelAction.Reject;
        if (autoTeleport) return FlagTravelAction.Automatic;
        return currentTerritory == targetTerritory
            ? FlagTravelAction.DirectNavigation
            : FlagTravelAction.WaitForTerritory;
    }
}

public readonly record struct PendingFlagDestination(uint TerritoryType, Vector3 Position);

// Accessed on the framework thread. A new coordinate replaces the old destination.
public sealed class PendingFlagTravel
{
    private PendingFlagDestination? destination;
    public bool IsWaiting => destination.HasValue;
    public void Clear() => destination = null;
    public void Set(uint territory, Vector3 position) =>
        destination = territory == 0 ? null : new PendingFlagDestination(territory, position);

    public PendingFlagDestination? TakeReady(bool autoTeleport, uint currentTerritory, bool playerReady)
    {
        if (!playerReady || !destination.HasValue) return null;
        var pending = destination.Value;
        if (FlagTravelPolicy.Decide(autoTeleport, currentTerritory, pending.TerritoryType) == FlagTravelAction.WaitForTerritory)
            return null;
        Clear();
        return pending;
    }
}
