using System;
using System.Numerics;

namespace AutomaticHunting;

public readonly record struct NavigationRecoveryDestination(uint TerritoryType, Vector3 Position, bool ManualFlag, bool IsMapFlag = true);
public readonly record struct NavigationRecoveryStep(bool StopOldPath, NavigationRecoveryDestination? Restart);

public sealed class NavigationTravelRecovery
{
    public const double StableSeconds = 1;
    private NavigationRecoveryDestination? destination;
    private bool interrupted;
    private bool oldPathDrained;
    private Vector3? previousPosition;
    private DateTime previousSample;
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
        if (destination.HasValue && !interrupted && territory == DestinationTerritory)
            destination = destination.Value with { Position = position, IsMapFlag = false };
    }

    public void Clear()
    {
        destination = null;
        interrupted = false;
        oldPathDrained = false;
        previousPosition = null;
        readySince = null;
    }

    public NavigationRecoveryStep Observe(uint currentTerritory, bool betweenAreas, bool hasPlayer,
        bool readyToRestart, bool oldPathfindInProgress, DateTime now, bool oldWorkerRunning = false, Vector3? playerPosition = null)
    {
        if (!destination.HasValue) return default;
        var saved = destination.Value;
        // Also catch a same-zone relocation when no loading frame was observed.
        double seconds = (now - previousSample).TotalSeconds;
        bool jumped = previousPosition.HasValue && playerPosition.HasValue && seconds >= 0 && seconds <= 1 &&
            Vector3.DistanceSquared(previousPosition.Value, playerPosition.Value) >= 80f * 80f;
        previousPosition = playerPosition;
        previousSample = now;
        bool travelling = betweenAreas || !hasPlayer || currentTerritory != saved.TerritoryType || jumped;
        if (!interrupted)
        {
            if (!travelling) return default;
            interrupted = true;
        }
        // Clear through completion of the old query, then stop touching unrelated routes.
        bool stopOldPath = !oldPathDrained;
        if (!oldWorkerRunning && !oldPathfindInProgress) oldPathDrained = true;
        if (travelling || !readyToRestart || oldWorkerRunning || oldPathfindInProgress)
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
