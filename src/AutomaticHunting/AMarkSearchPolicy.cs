using System;

namespace AutomaticHunting;

public static class AMarkSearchPolicy
{
    public const float DefaultRadius = 200f;
    public const float MinimumRadius = 50f;
    public const float MaximumRadius = 500f;
    public const float ArrivalDistance = 10f;
    public const double ArrivalGraceSeconds = 5;

    public static float NormalizeRadius(float value) =>
        float.IsFinite(value) ? Math.Clamp(value, MinimumRadius, MaximumRadius) : DefaultRadius;

    public static bool ShouldScan(float distanceToFlag, float configuredRadius) =>
        float.IsFinite(distanceToFlag) && distanceToFlag >= 0 && distanceToFlag <= NormalizeRadius(configuredRadius);
}

public enum AMarkArrivalState { Approaching, BeginWait, Waiting, Expired }

public sealed class AMarkArrivalSearch
{
    private DateTime? arrivedAt;

    public AMarkArrivalState Observe(float distanceToFlag, DateTime now)
    {
        if (!float.IsFinite(distanceToFlag) || distanceToFlag < 0 || distanceToFlag >= AMarkSearchPolicy.ArrivalDistance)
        {
            arrivedAt = null;
            return AMarkArrivalState.Approaching;
        }
        if (!arrivedAt.HasValue)
        {
            arrivedAt = now;
            return AMarkArrivalState.BeginWait;
        }
        return (now - arrivedAt.Value).TotalSeconds >= AMarkSearchPolicy.ArrivalGraceSeconds
            ? AMarkArrivalState.Expired
            : AMarkArrivalState.Waiting;
    }
}
