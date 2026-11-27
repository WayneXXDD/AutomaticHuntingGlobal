using System;
using System.Threading;

namespace AutomaticHunting;

// Each explicit start gets a fresh token. Stopping never revives old delayed work.
internal sealed class AutomationSession : IDisposable
{
    private readonly object gate = new();
    private CancellationTokenSource? active;
    private bool disposed;

    public CancellationToken Token
    {
        get { lock (gate) return active?.Token ?? new CancellationToken(canceled: true); }
    }

    public bool Start()
    {
        lock (gate)
        {
            if (disposed) return false;
            active ??= new CancellationTokenSource();
            return true;
        }
    }

    public void Stop()
    {
        CancellationTokenSource? previous;
        lock (gate) { previous = active; active = null; }
        if (previous == null) return;
        try { previous.Cancel(); }
        finally { previous.Dispose(); }
    }

    public void Dispose()
    {
        lock (gate) disposed = true;
        Stop();
    }
}
