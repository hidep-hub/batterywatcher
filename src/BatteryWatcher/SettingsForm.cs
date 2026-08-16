using BatteryWatcher.Power;
using BatteryWatcher.Startup;

namespace BatteryWatcher;

public partial class SettingsForm : Form
{
    private readonly BatteryDisplaySettings _settings;

    public SettingsForm(BatteryDisplaySettings settings)
    {
        _settings = settings;
        InitializeComponent();

        buttonAcColor.Click += (_, _) => PickColor(buttonAcColor);
        buttonGreenColor.Click += (_, _) => PickColor(buttonGreenColor);
        buttonYellowColor.Click += (_, _) => PickColor(buttonYellowColor);
        buttonRedColor.Click += (_, _) => PickColor(buttonRedColor);
        buttonBlinkColorA.Click += (_, _) => PickColor(buttonBlinkColorA);
        buttonBlinkColorB.Click += (_, _) => PickColor(buttonBlinkColorB);

        buttonOk.Click += (_, _) => ApplyToSettings();

        LoadFromSettings();
    }

    private void LoadFromSettings()
    {
        numericGreenMin.Value = _settings.GreenMinPercentage;
        numericYellowMin.Value = _settings.YellowMinPercentage;
        numericRedMin.Value = _settings.RedMinPercentage;

        buttonAcColor.BackColor = _settings.AcPowerColor;
        buttonGreenColor.BackColor = _settings.GreenColor;
        buttonYellowColor.BackColor = _settings.YellowColor;
        buttonRedColor.BackColor = _settings.RedColor;
        buttonBlinkColorA.BackColor = _settings.BlinkColorA;
        buttonBlinkColorB.BackColor = _settings.BlinkColorB;

        checkBoxBlinkEnabled.Checked = _settings.BlinkEnabled;
        numericBlinkInterval.Value = _settings.BlinkIntervalMilliseconds;

        checkBoxStartWithWindows.Checked = StartupManager.IsEnabled();
    }

    private void PickColor(Button target)
    {
        using var dialog = new ColorDialog { Color = target.BackColor };
        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            target.BackColor = dialog.Color;
        }
    }

    private void ApplyToSettings()
    {
        _settings.GreenMinPercentage = (int)numericGreenMin.Value;
        _settings.YellowMinPercentage = (int)numericYellowMin.Value;
        _settings.RedMinPercentage = (int)numericRedMin.Value;

        _settings.AcPowerColor = buttonAcColor.BackColor;
        _settings.GreenColor = buttonGreenColor.BackColor;
        _settings.YellowColor = buttonYellowColor.BackColor;
        _settings.RedColor = buttonRedColor.BackColor;
        _settings.BlinkColorA = buttonBlinkColorA.BackColor;
        _settings.BlinkColorB = buttonBlinkColorB.BackColor;

        _settings.BlinkEnabled = checkBoxBlinkEnabled.Checked;
        _settings.BlinkIntervalMilliseconds = (int)numericBlinkInterval.Value;

        StartupManager.SetEnabled(checkBoxStartWithWindows.Checked);
    }
}
