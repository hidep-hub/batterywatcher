namespace BatteryWatcher.Power;

public readonly record struct BatteryStatus(
    bool HasBattery,
    int Percentage,
    bool IsCharging,
    bool IsOnAcPower,
    TimeSpan? TimeRemaining);
