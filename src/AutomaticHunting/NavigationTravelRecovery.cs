using System;
using System.Numerics;

namespace AutomaticHunting;

public readonly record struct NavigationRecoveryDestination(uint TerritoryType, Vector3 Position, bool ManualFlag);
public readonly record struct NavigationRecoveryStep(bool StopOldPath, NavigationRecoveryDestination? Restart);

public sealed class NavigationTravelRecovery
{
    public const double StableSeconds = 1;
    private NavigationRecoveryDestination? destination;
    private bool interrupted;
    private bool oldPathDrained;
    private DateTime? readySince;

    public bool HasDestination => destination.HasValue;
    public uint DestinationTerritory => destination?.TerritoryType ?? 0;
    public bool IsWaiting => interrupted && HasDestination;

    public void Begin(NavigationRecoveryDestination next)
    {
        Clear();
        if (next.TerritoryType != 0) destination = next;
    }

    public void UpdateDestination(uint territory, Vector3 position)
    {
        if (destination.HasValue && !interrupted && territory != 0)
            destination = destination.Value with { TerritoryType = territory, Position = position };
    }

    public void Clear()
    {
        destination = null;
        interrupted = false;
        oldPathDrained = false;
        readySince = null;
    }

    public NavigationRecoveryStep Observe(uint currentTerritory, bool betweenAreas, bool hasPlayer,
        bool readyToRestart, bool oldPathfindInProgress, DateTime now)
    {
        if (!destination.HasValue) return default;
        var saved = destination.Value;
        bool travelling = betweenAreas || !hasPlayer || currentTerritory != saved.TerritoryType;
        if (!interrupted)
        {
            if (!travelling) return default;
            interrupted = true;
        }
        // Clear through completion of the old query, then stop touching unrelated routes.
        bool stopOldPath = !oldPathDrained;
        if (!oldPathfindInProgress) oldPathDrained = true;
        if (travelling || !readyToRestart || oldPathfindInProgress)
        {
            readySince = null;
            return new(stopOldPath, null);
        }
        if (!readySince.HasValue)
        {
            readySince = now;
            return new(stopOldPath, null);
        }
        if ((now - readySince.Value).TotalSeconds < StableSeconds) return new(stopOldPath, null);
        Clear();
        return new(true, saved);
    }
}
