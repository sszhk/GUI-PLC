namespace ImagingSortCharlie.Forms
{
  partial class SettingStarving
  {
    /// <summary> 
    /// 必需的设计器变量。
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// 清理所有正在使用的资源。
    /// </summary>
    /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region 组件设计器生成的代码

    /// <summary> 
    /// 设计器支持所需的方法 - 不要
    /// 使用代码编辑器修改此方法的内容。
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingStarving));
      this.cbStarving = new System.Windows.Forms.CheckBox();
      this.label8 = new System.Windows.Forms.Label();
      this.lbName = new System.Windows.Forms.Label();
      this.tbName = new System.Windows.Forms.TextBox();
      this.label10 = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this.label11 = new System.Windows.Forms.Label();
      this.rbBlackHead = new ImagingSortCharlie.FramedPicture(this.components);
      this.rbWhiteHead = new ImagingSortCharlie.FramedPicture(this.components);
      this.tb_min_starving = new ImagingSortCharlie.CalcTextBox();
      this.label15 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.tb_max_starving = new ImagingSortCharlie.CalcTextBox();
      this.label1 = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.rbBlackHead)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.rbWhiteHead)).BeginInit();
      this.SuspendLayout();
      // 
      // cbStarving
      // 
      this.cbStarving.BackColor = System.Drawing.Color.Transparent;
      this.cbStarving.Checked = true;
      this.cbStarving.CheckState = System.Windows.Forms.CheckState.Checked;
      this.cbStarving.ForeColor = System.Drawing.SystemColors.ControlText;
      resources.ApplyResources(this.cbStarving, "cbStarving");
      this.cbStarving.Name = "cbStarving";
      this.cbStarving.UseVisualStyleBackColor = false;
      // 
      // label8
      // 
      this.label8.BackColor = System.Drawing.Color.SteelBlue;
      resources.ApplyResources(this.label8, "label8");
      this.label8.Name = "label8";
      // 
      // lbName
      // 
      resources.ApplyResources(this.lbName, "lbName");
      this.lbName.BackColor = System.Drawing.Color.Transparent;
      this.lbName.ForeColor = System.Drawing.SystemColors.ControlText;
      this.lbName.Name = "lbName";
      // 
      // tbName
      // 
      resources.ApplyResources(this.tbName, "tbName");
      this.tbName.Name = "tbName";
      // 
      // label10
      // 
      resources.ApplyResources(this.label10, "label10");
      this.label10.ForeColor = System.Drawing.SystemColors.ControlText;
      this.label10.Name = "label10";
      // 
      // label7
      // 
      resources.ApplyResources(this.label7, "label7");
      this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
      this.label7.Name = "label7";
      // 
      // label11
      // 
      resources.ApplyResources(this.label11, "label11");
      this.label11.ForeColor = System.Drawing.SystemColors.ControlText;
      this.label11.Name = "label11";
      // 
      // rbBlackHead
      // 
      this.rbBlackHead.BackColor = System.Drawing.Color.Black;
      this.rbBlackHead.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.rbBlackHead.Checked = false;
      this.rbBlackHead.Cursor = System.Windows.Forms.Cursors.Hand;
      this.rbBlackHead.FrameColor = System.Drawing.Color.DodgerBlue;
      this.rbBlackHead.Group = 2;
      resources.ApplyResources(this.rbBlackHead, "rbBlackHead");
      this.rbBlackHead.Name = "rbBlackHead";
      this.rbBlackHead.NormalColor = System.Drawing.Color.Black;
      this.rbBlackHead.OverColor = System.Drawing.Color.Red;
      this.rbBlackHead.TabStop = false;
      this.rbBlackHead.Click += new System.EventHandler(this.rbWhiteHead_Click);
      // 
      // rbWhiteHead
      // 
      this.rbWhiteHead.BackColor = System.Drawing.Color.White;
      this.rbWhiteHead.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.rbWhiteHead.Checked = true;
      this.rbWhiteHead.Cursor = System.Windows.Forms.Cursors.Hand;
      this.rbWhiteHead.FrameColor = System.Drawing.Color.DodgerBlue;
      this.rbWhiteHead.Group = 2;
      resources.ApplyResources(this.rbWhiteHead, "rbWhiteHead");
      this.rbWhiteHead.Name = "rbWhiteHead";
      this.rbWhiteHead.NormalColor = System.Drawing.Color.White;
      this.rbWhiteHead.OverColor = System.Drawing.Color.Red;
      this.rbWhiteHead.TabStop = false;
      this.rbWhiteHead.Click += new System.EventHandler(this.rbWhiteHead_Click);
      // 
      // tb_min_starving
      // 
      this.tb_min_starving.AllowPromptAsInput = false;
      this.tb_min_starving.ClearOnActive = true;
      this.tb_min_starving.HidePromptOnLeave = true;
      resources.ApplyResources(this.tb_min_starving, "tb_min_starving");
      this.tb_min_starving.Name = "tb_min_starving";
      // 
      // label15
      // 
      resources.ApplyResources(this.label15, "label15");
      this.label15.ForeColor = System.Drawing.SystemColors.ControlText;
      this.label15.Name = "label15";
      // 
      // label2
      // 
      resources.ApplyResources(this.label2, "label2");
      this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
      this.label2.Name = "label2";
      // 
      // tb_max_starving
      // 
      this.tb_max_starving.AllowPromptAsInput = false;
      this.tb_max_starving.ClearOnActive = true;
      this.tb_max_starving.HidePromptOnLeave = true;
      resources.ApplyResources(this.tb_max_starving, "tb_max_starving");
      this.tb_max_starving.Name = "tb_max_starving";
      // 
      // label1
      // 
      resources.ApplyResources(this.label1, "label1");
      this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
      this.label1.Name = "label1";
      // 
      // SettingStarving
      // 
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
      this.BackColor = System.Drawing.Color.Transparent;
      this.Controls.Add(this.label1);
      this.Controls.Add(this.tb_max_starving);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label15);
      this.Controls.Add(this.label10);
      this.Controls.Add(this.label7);
      this.Controls.Add(this.label11);
      this.Controls.Add(this.rbBlackHead);
      this.Controls.Add(this.rbWhiteHead);
      this.Controls.Add(this.label8);
      this.Controls.Add(this.lbName);
      this.Controls.Add(this.tbName);
      this.Controls.Add(this.tb_min_starving);
      this.Controls.Add(this.cbStarving);
      this.Name = "SettingStarving";
      resources.ApplyResources(this, "$this");
      this.Load += new System.EventHandler(this.SettingStarving_Load);
      ((System.ComponentModel.ISupportInitialize)(this.rbBlackHead)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.rbWhiteHead)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private CalcTextBox tb_min_starving;
    private System.Windows.Forms.CheckBox cbStarving;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Label lbName;
    private System.Windows.Forms.TextBox tbName;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label label11;
    private FramedPicture rbBlackHead;
    private FramedPicture rbWhiteHead;
    private System.Windows.Forms.Label label15;
    private System.Windows.Forms.Label label2;
    private CalcTextBox tb_max_starving;
    private System.Windows.Forms.Label label1;
  }
}
