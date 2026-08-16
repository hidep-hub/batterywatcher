namespace BatteryWatcher;

partial class SettingsForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();

        this.groupBoxThreshold = new System.Windows.Forms.GroupBox();
        this.labelGreenMin = new System.Windows.Forms.Label();
        this.numericGreenMin = new System.Windows.Forms.NumericUpDown();
        this.labelYellowMin = new System.Windows.Forms.Label();
        this.numericYellowMin = new System.Windows.Forms.NumericUpDown();
        this.labelRedMin = new System.Windows.Forms.Label();
        this.numericRedMin = new System.Windows.Forms.NumericUpDown();

        this.groupBoxColor = new System.Windows.Forms.GroupBox();
        this.labelAcColor = new System.Windows.Forms.Label();
        this.buttonAcColor = new System.Windows.Forms.Button();
        this.labelGreenColor = new System.Windows.Forms.Label();
        this.buttonGreenColor = new System.Windows.Forms.Button();
        this.labelYellowColor = new System.Windows.Forms.Label();
        this.buttonYellowColor = new System.Windows.Forms.Button();
        this.labelRedColor = new System.Windows.Forms.Label();
        this.buttonRedColor = new System.Windows.Forms.Button();
        this.labelBlinkColorA = new System.Windows.Forms.Label();
        this.buttonBlinkColorA = new System.Windows.Forms.Button();
        this.labelBlinkColorB = new System.Windows.Forms.Label();
        this.buttonBlinkColorB = new System.Windows.Forms.Button();

        this.groupBoxDisplay = new System.Windows.Forms.GroupBox();
        this.checkBoxBlinkEnabled = new System.Windows.Forms.CheckBox();
        this.labelBlinkInterval = new System.Windows.Forms.Label();
        this.numericBlinkInterval = new System.Windows.Forms.NumericUpDown();

        this.checkBoxStartWithWindows = new System.Windows.Forms.CheckBox();

        this.buttonOk = new System.Windows.Forms.Button();
        this.buttonCancel = new System.Windows.Forms.Button();

        ((System.ComponentModel.ISupportInitialize)(this.numericGreenMin)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.numericYellowMin)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.numericRedMin)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.numericBlinkInterval)).BeginInit();
        this.groupBoxThreshold.SuspendLayout();
        this.groupBoxColor.SuspendLayout();
        this.groupBoxDisplay.SuspendLayout();
        this.SuspendLayout();

        //
        // groupBoxThreshold
        //
        this.groupBoxThreshold.Text = "しきい値";
        this.groupBoxThreshold.Location = new System.Drawing.Point(12, 12);
        this.groupBoxThreshold.Size = new System.Drawing.Size(376, 110);
        this.groupBoxThreshold.Controls.Add(this.labelGreenMin);
        this.groupBoxThreshold.Controls.Add(this.numericGreenMin);
        this.groupBoxThreshold.Controls.Add(this.labelYellowMin);
        this.groupBoxThreshold.Controls.Add(this.numericYellowMin);
        this.groupBoxThreshold.Controls.Add(this.labelRedMin);
        this.groupBoxThreshold.Controls.Add(this.numericRedMin);

        this.labelGreenMin.Text = "緑 最小%:";
        this.labelGreenMin.AutoSize = true;
        this.labelGreenMin.Location = new System.Drawing.Point(10, 27);

        this.numericGreenMin.Location = new System.Drawing.Point(110, 24);
        this.numericGreenMin.Size = new System.Drawing.Size(60, 23);
        this.numericGreenMin.Minimum = 0;
        this.numericGreenMin.Maximum = 100;

        this.labelYellowMin.Text = "黄 最小%:";
        this.labelYellowMin.AutoSize = true;
        this.labelYellowMin.Location = new System.Drawing.Point(10, 57);

        this.numericYellowMin.Location = new System.Drawing.Point(110, 54);
        this.numericYellowMin.Size = new System.Drawing.Size(60, 23);
        this.numericYellowMin.Minimum = 0;
        this.numericYellowMin.Maximum = 100;

        this.labelRedMin.Text = "赤 最小%:";
        this.labelRedMin.AutoSize = true;
        this.labelRedMin.Location = new System.Drawing.Point(10, 87);

        this.numericRedMin.Location = new System.Drawing.Point(110, 84);
        this.numericRedMin.Size = new System.Drawing.Size(60, 23);
        this.numericRedMin.Minimum = 0;
        this.numericRedMin.Maximum = 100;

        //
        // groupBoxColor
        //
        this.groupBoxColor.Text = "配色";
        this.groupBoxColor.Location = new System.Drawing.Point(12, 130);
        this.groupBoxColor.Size = new System.Drawing.Size(376, 235);
        this.groupBoxColor.Controls.Add(this.labelAcColor);
        this.groupBoxColor.Controls.Add(this.buttonAcColor);
        this.groupBoxColor.Controls.Add(this.labelGreenColor);
        this.groupBoxColor.Controls.Add(this.buttonGreenColor);
        this.groupBoxColor.Controls.Add(this.labelYellowColor);
        this.groupBoxColor.Controls.Add(this.buttonYellowColor);
        this.groupBoxColor.Controls.Add(this.labelRedColor);
        this.groupBoxColor.Controls.Add(this.buttonRedColor);
        this.groupBoxColor.Controls.Add(this.labelBlinkColorA);
        this.groupBoxColor.Controls.Add(this.buttonBlinkColorA);
        this.groupBoxColor.Controls.Add(this.labelBlinkColorB);
        this.groupBoxColor.Controls.Add(this.buttonBlinkColorB);

        this.labelAcColor.Text = "電源接続:";
        this.labelAcColor.AutoSize = true;
        this.labelAcColor.Location = new System.Drawing.Point(10, 30);
        this.buttonAcColor.Location = new System.Drawing.Point(110, 24);
        this.buttonAcColor.Size = new System.Drawing.Size(80, 25);
        this.buttonAcColor.UseVisualStyleBackColor = false;

        this.labelGreenColor.Text = "緑:";
        this.labelGreenColor.AutoSize = true;
        this.labelGreenColor.Location = new System.Drawing.Point(10, 65);
        this.buttonGreenColor.Location = new System.Drawing.Point(110, 59);
        this.buttonGreenColor.Size = new System.Drawing.Size(80, 25);
        this.buttonGreenColor.UseVisualStyleBackColor = false;

        this.labelYellowColor.Text = "黄:";
        this.labelYellowColor.AutoSize = true;
        this.labelYellowColor.Location = new System.Drawing.Point(10, 100);
        this.buttonYellowColor.Location = new System.Drawing.Point(110, 94);
        this.buttonYellowColor.Size = new System.Drawing.Size(80, 25);
        this.buttonYellowColor.UseVisualStyleBackColor = false;

        this.labelRedColor.Text = "赤:";
        this.labelRedColor.AutoSize = true;
        this.labelRedColor.Location = new System.Drawing.Point(10, 135);
        this.buttonRedColor.Location = new System.Drawing.Point(110, 129);
        this.buttonRedColor.Size = new System.Drawing.Size(80, 25);
        this.buttonRedColor.UseVisualStyleBackColor = false;

        this.labelBlinkColorA.Text = "点滅色A:";
        this.labelBlinkColorA.AutoSize = true;
        this.labelBlinkColorA.Location = new System.Drawing.Point(10, 170);
        this.buttonBlinkColorA.Location = new System.Drawing.Point(110, 164);
        this.buttonBlinkColorA.Size = new System.Drawing.Size(80, 25);
        this.buttonBlinkColorA.UseVisualStyleBackColor = false;

        this.labelBlinkColorB.Text = "点滅色B:";
        this.labelBlinkColorB.AutoSize = true;
        this.labelBlinkColorB.Location = new System.Drawing.Point(10, 205);
        this.buttonBlinkColorB.Location = new System.Drawing.Point(110, 199);
        this.buttonBlinkColorB.Size = new System.Drawing.Size(80, 25);
        this.buttonBlinkColorB.UseVisualStyleBackColor = false;

        //
        // groupBoxDisplay
        //
        this.groupBoxDisplay.Text = "表示形式";
        this.groupBoxDisplay.Location = new System.Drawing.Point(12, 375);
        this.groupBoxDisplay.Size = new System.Drawing.Size(376, 90);
        this.groupBoxDisplay.Controls.Add(this.checkBoxBlinkEnabled);
        this.groupBoxDisplay.Controls.Add(this.labelBlinkInterval);
        this.groupBoxDisplay.Controls.Add(this.numericBlinkInterval);

        this.checkBoxBlinkEnabled.Text = "点滅を有効にする";
        this.checkBoxBlinkEnabled.AutoSize = true;
        this.checkBoxBlinkEnabled.Location = new System.Drawing.Point(10, 27);

        this.labelBlinkInterval.Text = "点滅間隔(ms):";
        this.labelBlinkInterval.AutoSize = true;
        this.labelBlinkInterval.Location = new System.Drawing.Point(10, 57);
        this.numericBlinkInterval.Location = new System.Drawing.Point(130, 54);
        this.numericBlinkInterval.Size = new System.Drawing.Size(80, 23);
        this.numericBlinkInterval.Minimum = 100;
        this.numericBlinkInterval.Maximum = 5000;
        this.numericBlinkInterval.Increment = 50;

        //
        // checkBoxStartWithWindows
        //
        this.checkBoxStartWithWindows.Text = "Windows起動時に自動的に開始する";
        this.checkBoxStartWithWindows.AutoSize = true;
        this.checkBoxStartWithWindows.Location = new System.Drawing.Point(12, 475);

        //
        // buttonOk / buttonCancel
        //
        this.buttonOk.Text = "OK";
        this.buttonOk.Location = new System.Drawing.Point(216, 510);
        this.buttonOk.Size = new System.Drawing.Size(80, 30);
        this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;

        this.buttonCancel.Text = "キャンセル";
        this.buttonCancel.Location = new System.Drawing.Point(308, 510);
        this.buttonCancel.Size = new System.Drawing.Size(80, 30);
        this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;

        //
        // SettingsForm
        //
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(400, 552);
        this.Controls.Add(this.groupBoxThreshold);
        this.Controls.Add(this.groupBoxColor);
        this.Controls.Add(this.groupBoxDisplay);
        this.Controls.Add(this.checkBoxStartWithWindows);
        this.Controls.Add(this.buttonOk);
        this.Controls.Add(this.buttonCancel);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.AcceptButton = this.buttonOk;
        this.CancelButton = this.buttonCancel;
        this.Text = "設定";

        ((System.ComponentModel.ISupportInitialize)(this.numericGreenMin)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.numericYellowMin)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.numericRedMin)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.numericBlinkInterval)).EndInit();
        this.groupBoxThreshold.ResumeLayout(false);
        this.groupBoxThreshold.PerformLayout();
        this.groupBoxColor.ResumeLayout(false);
        this.groupBoxColor.PerformLayout();
        this.groupBoxDisplay.ResumeLayout(false);
        this.groupBoxDisplay.PerformLayout();
        this.ResumeLayout(false);
    }

    private System.Windows.Forms.GroupBox groupBoxThreshold;
    private System.Windows.Forms.Label labelGreenMin;
    private System.Windows.Forms.NumericUpDown numericGreenMin;
    private System.Windows.Forms.Label labelYellowMin;
    private System.Windows.Forms.NumericUpDown numericYellowMin;
    private System.Windows.Forms.Label labelRedMin;
    private System.Windows.Forms.NumericUpDown numericRedMin;

    private System.Windows.Forms.GroupBox groupBoxColor;
    private System.Windows.Forms.Label labelAcColor;
    private System.Windows.Forms.Button buttonAcColor;
    private System.Windows.Forms.Label labelGreenColor;
    private System.Windows.Forms.Button buttonGreenColor;
    private System.Windows.Forms.Label labelYellowColor;
    private System.Windows.Forms.Button buttonYellowColor;
    private System.Windows.Forms.Label labelRedColor;
    private System.Windows.Forms.Button buttonRedColor;
    private System.Windows.Forms.Label labelBlinkColorA;
    private System.Windows.Forms.Button buttonBlinkColorA;
    private System.Windows.Forms.Label labelBlinkColorB;
    private System.Windows.Forms.Button buttonBlinkColorB;

    private System.Windows.Forms.GroupBox groupBoxDisplay;
    private System.Windows.Forms.CheckBox checkBoxBlinkEnabled;
    private System.Windows.Forms.Label labelBlinkInterval;
    private System.Windows.Forms.NumericUpDown numericBlinkInterval;

    private System.Windows.Forms.CheckBox checkBoxStartWithWindows;

    private System.Windows.Forms.Button buttonOk;
    private System.Windows.Forms.Button buttonCancel;

    #endregion
}
