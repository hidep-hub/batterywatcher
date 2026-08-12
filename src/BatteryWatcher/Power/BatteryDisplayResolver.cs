namespace BatteryWatcher.Power;

public enum BatteryDisplayKind
{
    PowerConnected,
    Percentage,
}

public readonly record struct BatteryDisplayInfo(
    BatteryDisplayKind Kind,
    string Text,
    Color TextColor,
    bool IsBlinking,
    Color BlinkAltColor);

public static class BatteryDisplayResolver
{
    public static BatteryDisplayInfo Resolve(BatteryStatus status, BatteryDisplaySettings settings)
    {
        if (status.IsOnAcPower)
        {
            return new BatteryDisplayInfo(BatteryDisplayKind.PowerConnected, string.Empty, settings.AcPowerColor, false, settings.AcPowerColor);
        }

        var percentage = status.Percentage;
        var text = percentage >= 100 ? "FULL" : $"{percentage}%";

        if (percentage < settings.RedMinPercentage)
        {
            return new BatteryDisplayInfo(BatteryDisplayKind.Percentage, text, settings.BlinkColorA, settings.BlinkEnabled, settings.BlinkColorB);
        }

        var color = percentage >= settings.GreenMinPercentage ? settings.GreenColor
            : percentage >= settings.YellowMinPercentage ? settings.YellowColor
            : settings.RedColor;

        return new BatteryDisplayInfo(BatteryDisplayKind.Percentage, text, color, false, color);
    }
}
