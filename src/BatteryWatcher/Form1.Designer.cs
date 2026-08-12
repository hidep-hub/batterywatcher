namespace BatteryWatcher;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        this.labelBatteryStatus = new System.Windows.Forms.Label();
        this.SuspendLayout();
        //
        // labelBatteryStatus
        //
        this.labelBatteryStatus.AutoSize = true;
        this.labelBatteryStatus.Font = new System.Drawing.Font("Yu Gothic UI", 14F);
        this.labelBatteryStatus.Location = new System.Drawing.Point(30, 30);
        this.labelBatteryStatus.Name = "labelBatteryStatus";
        this.labelBatteryStatus.Size = new System.Drawing.Size(150, 26);
        this.labelBatteryStatus.TabIndex = 0;
        this.labelBatteryStatus.Text = "取得中...";
        //
        // Form1
        //
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(400, 150);
        this.Controls.Add(this.labelBatteryStatus);
        this.Text = "BatteryWatcher (動作確認用)";
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private System.Windows.Forms.Label labelBatteryStatus;

    #endregion
}
