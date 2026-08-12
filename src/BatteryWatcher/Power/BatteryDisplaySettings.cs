namespace BatteryWatcher.Power;

/// <summary>
/// しきい値・配色・点滅の設定。将来的に設定画面（BW-007）から変更される想定の値を保持する。
/// </summary>
public sealed class BatteryDisplaySettings
{
    /// <summary>これ以上は緑（100%はFULL表示）。</summary>
    public int GreenMinPercentage { get; set; } = 30;

    /// <summary>これ以上・GreenMin未満は黄色。</summary>
    public int YellowMinPercentage { get; set; } = 20;

    /// <summary>これ以上・YellowMin未満は赤。これ未満は点滅。</summary>
    public int RedMinPercentage { get; set; } = 10;

    public Color AcPowerColor { get; set; } = Color.White;
    public Color GreenColor { get; set; } = Color.LimeGreen;
    public Color YellowColor { get; set; } = Color.Gold;
    public Color RedColor { get; set; } = Color.Red;

    public bool BlinkEnabled { get; set; } = true;
    public Color BlinkColorA { get; set; } = Color.Red;
    public Color BlinkColorB { get; set; } = Color.White;
    public int BlinkIntervalMilliseconds { get; set; } = 500;
}
