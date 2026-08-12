using Microsoft.Win32;

namespace BatteryWatcher.Power;

public sealed class BatteryMonitor : IDisposable
{
    private static readonly TimeSpan PollingInterval = TimeSpan.FromSeconds(30);

    private readonly System.Windows.Forms.Timer _pollingTimer;

    public event EventHandler<BatteryStatus>? StatusChanged;

    public BatteryStatus Current { get; private set; }

    public BatteryMonitor()
    {
        Current = ReadStatus();

        _pollingTimer = new System.Windows.Forms.Timer { Interval = (int)PollingInterval.TotalMilliseconds };
        _pollingTimer.Tick += (_, _) => Refresh();
        _pollingTimer.Start();

        SystemEvents.PowerModeChanged += OnPowerModeChanged;
    }

    public void Refresh()
    {
        Current = ReadStatus();
        StatusChanged?.Invoke(this, Current);
    }

    private void OnPowerModeChanged(object? sender, PowerModeChangedEventArgs e)
    {
        if (e.Mode == PowerModes.StatusChange)
        {
            Refresh();
        }
    }

    private static BatteryStatus ReadStatus()
    {
        if (!NativeMethods.GetSystemPowerStatus(out var raw))
        {
            return new BatteryStatus(false, 0, false, false, null);
        }

        const byte NoBattery = 0x80;
        const byte Unknown = 0xFF;
        const byte ChargingFlag = 0x08;

        var hasBattery = raw.BatteryFlag != NoBattery && raw.BatteryFlag != Unknown;
        var isCharging = (raw.BatteryFlag & ChargingFlag) != 0;
        var isOnAcPower = raw.ACLineStatus == 1;
        var percentage = raw.BatteryLifePercent == 255 ? 0 : raw.BatteryLifePercent;
        TimeSpan? timeRemaining = raw.BatteryLifeTime == uint.MaxValue
            ? null
            : TimeSpan.FromSeconds(raw.BatteryLifeTime);

        return new BatteryStatus(hasBattery, percentage, isCharging, isOnAcPower, timeRemaining);
    }

    public void Dispose()
    {
        SystemEvents.PowerModeChanged -= OnPowerModeChanged;
        _pollingTimer.Stop();
        _pollingTimer.Dispose();
    }
}
