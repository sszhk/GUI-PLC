namespace ImagingSortCharlie.Forms
{
  partial class FormProgress
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
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
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProgress));
      this.pb = new System.Windows.Forms.ProgressBar();
      this.lbPrompt = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // pb
      // 
      resources.ApplyResources(this.pb, "pb");
      this.pb.MarqueeAnimationSpeed = 10;
      this.pb.Name = "pb";
      this.pb.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
      // 
      // lbPrompt
      // 
      resources.ApplyResources(this.lbPrompt, "lbPrompt");
      this.lbPrompt.BackColor = System.Drawing.Color.Transparent;
      this.lbPrompt.ForeColor = System.Drawing.Color.DimGray;
      this.lbPrompt.Name = "lbPrompt";
      // 
      // FormProgress
      // 
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
      this.BackColor = System.Drawing.Color.White;
      this.BackgroundImage = global::ImagingSortCharlie.Properties.Resources.artful_nuts;
      resources.ApplyResources(this, "$this");
      this.ControlBox = false;
      this.Controls.Add(this.pb);
      this.Controls.Add(this.lbPrompt);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Name = "FormProgress";
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.Load += new System.EventHandler(this.Progress_Load);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ProgressBar pb;
    private System.Windows.Forms.Label lbPrompt;
  }
}