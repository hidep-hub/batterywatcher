using BatteryWatcher.Power;
using TrayIcon = BatteryWatcher.Icon.BatteryIconRenderer;

namespace BatteryWatcher;

public partial class Form1 : Form
{
    private readonly BatteryDisplaySettings _displaySettings = new();
    private readonly BatteryMonitor _batteryMonitor = new();
    private readonly NotifyIcon _notifyIcon = new();
    private readonly ContextMenuStrip _trayMenu = new();
    private readonly System.Windows.Forms.Timer _blinkTimer = new();
    private System.Drawing.Icon? _currentTrayIcon;
    private BatteryStatus _lastStatus;
    private bool _blinkPhase;

    public Form1()
    {
        InitializeComponent();

        _trayMenu.Items.Add("設定", null, (_, _) => OpenSettings());
        _trayMenu.Items.Add(new ToolStripSeparator());
        _trayMenu.Items.Add("終了", null, (_, _) => Close());
        _notifyIcon.ContextMenuStrip = _trayMenu;
        _notifyIcon.Visible = true;

        _blinkTimer.Interval = _displaySettings.BlinkIntervalMilliseconds;
        _blinkTimer.Tick += (_, _) =>
        {
            _blinkPhase = !_blinkPhase;
            RenderIcon(BatteryDisplayResolver.Resolve(_lastStatus, _displaySettings), _blinkPhase);
        };

        _batteryMonitor.StatusChanged += (_, status) => UpdateStatus(status);
        UpdateStatus(_batteryMonitor.Current);

        FormClosed += (_, _) =>
        {
            _batteryMonitor.Dispose();
            _blinkTimer.Dispose();
            _notifyIcon.Dispose();
            _trayMenu.Dispose();
            _currentTrayIcon?.Dispose();
        };
    }

    private void OpenSettings()
    {
        using var settingsForm = new SettingsForm(_displaySettings);
        if (settingsForm.ShowDialog(this) == DialogResult.OK)
        {
            _blinkTimer.Interval = _displaySettings.BlinkIntervalMilliseconds;
            UpdateStatus(_lastStatus);
        }
    }

    private void UpdateStatus(BatteryStatus status)
    {
        _lastStatus = status;

        if (!status.HasBattery)
        {
            _blinkTimer.Stop();
            labelBatteryStatus.Text = "バッテリーなし（AC電源のみ）";
            _notifyIcon.Text = "バッテリーなし";
            SetTrayIcon(TrayIcon.Render("--", Color.Gray));
            return;
        }

        var power = status.IsOnAcPower ? "AC接続" : "バッテリー駆動";
        var charging = status.IsCharging ? "充電中" : "非充電";
        var remaining = status.TimeRemaining is { } t ? $" 残り{t:hh\\:mm}" : "";

        labelBatteryStatus.Text = $"{status.Percentage}% ({power} / {charging}){remaining}";
        _notifyIcon.Text = $"{status.Percentage}% - {power} / {charging}";

        var display = BatteryDisplayResolver.Resolve(status, _displaySettings);
        if (display.IsBlinking)
        {
            if (!_blinkTimer.Enabled)
            {
                _blinkPhase = false;
                _blinkTimer.Start();
            }
        }
        else
        {
            _blinkTimer.Stop();
        }

        RenderIcon(display, _blinkPhase && display.IsBlinking);
    }

    private void RenderIcon(BatteryDisplayInfo display, bool useAltColor)
    {
        var color = useAltColor ? display.BlinkAltColor : display.TextColor;
        var icon = display.Kind == BatteryDisplayKind.PowerConnected
            ? TrayIcon.RenderPowerPlug(color)
            : TrayIcon.Render(display.Text, color);
        SetTrayIcon(icon);
    }

    private void SetTrayIcon(System.Drawing.Icon newIcon)
    {
        _notifyIcon.Icon = newIcon;
        _currentTrayIcon?.Dispose();
        _currentTrayIcon = newIcon;
    }
}
