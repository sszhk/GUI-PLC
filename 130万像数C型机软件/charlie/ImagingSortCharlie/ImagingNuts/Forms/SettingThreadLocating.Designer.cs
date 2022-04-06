namespace ImagingSortCharlie.Forms
{
  partial class SettingThreadLocating
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingThreadLocating));
      this.lbName = new System.Windows.Forms.Label();
      this.tbName = new System.Windows.Forms.TextBox();
      this.label27 = new System.Windows.Forms.Label();
      this.label10 = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this.lbThreadColor = new System.Windows.Forms.Label();
      this.rbBlackHead = new ImagingSortCharlie.FramedPicture(this.components);
      this.rbWhiteHead = new ImagingSortCharlie.FramedPicture(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.rbBlackHead)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.rbWhiteHead)).BeginInit();
      this.SuspendLayout();
      // 
      // lbName
      // 
      this.lbName.AccessibleDescription = null;
      this.lbName.AccessibleName = null;
      resources.ApplyResources(this.lbName, "lbName");
      this.lbName.BackColor = System.Drawing.Color.Transparent;
      this.lbName.Font = null;
      this.lbName.ForeColor = System.Drawing.SystemColors.ControlText;
      this.lbName.Name = "lbName";
      // 
      // tbName
      // 
      this.tbName.AccessibleDescription = null;
      this.tbName.AccessibleName = null;
      resources.ApplyResources(this.tbName, "tbName");
      this.tbName.BackgroundImage = null;
      this.tbName.Name = "tbName";
      // 
      // label27
      // 
      this.label27.AccessibleDescription = null;
      this.label27.AccessibleName = null;
      resources.ApplyResources(this.label27, "label27");
      this.label27.BackColor = System.Drawing.Color.SteelBlue;
      this.label27.Font = null;
      this.label27.Name = "label27";
      // 
      // label10
      // 
      this.label10.AccessibleDescription = null;
      this.label10.AccessibleName = null;
      resources.ApplyResources(this.label10, "label10");
      this.label10.Font = null;
      this.label10.ForeColor = System.Drawing.SystemColors.ControlText;
      this.label10.Name = "label10";
      // 
      // label7
      // 
      this.label7.AccessibleDescription = null;
      this.label7.AccessibleName = null;
      resources.ApplyResources(this.label7, "label7");
      this.label7.Font = null;
      this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
      this.label7.Name = "label7";
      // 
      // lbThreadColor
      // 
      this.lbThreadColor.AccessibleDescription = null;
      this.lbThreadColor.AccessibleName = null;
      resources.ApplyResources(this.lbThreadColor, "lbThreadColor");
      this.lbThreadColor.Font = null;
      this.lbThreadColor.ForeColor = System.Drawing.SystemColors.ControlText;
      this.lbThreadColor.Name = "lbThreadColor";
      // 
      // rbBlackHead
      // 
      this.rbBlackHead.AccessibleDescription = null;
      this.rbBlackHead.AccessibleName = null;
      resources.ApplyResources(this.rbBlackHead, "rbBlackHead");
      this.rbBlackHead.BackColor = System.Drawing.Color.Black;
      this.rbBlackHead.BackgroundImage = null;
      this.rbBlackHead.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.rbBlackHead.Checked = false;
      this.rbBlackHead.Cursor = System.Windows.Forms.Cursors.Hand;
      this.rbBlackHead.Font = null;
      this.rbBlackHead.FrameColor = System.Drawing.Color.DodgerBlue;
      this.rbBlackHead.Group = 2;
      this.rbBlackHead.ImageLocation = null;
      this.rbBlackHead.Name = "rbBlackHead";
      this.rbBlackHead.NormalColor = System.Drawing.Color.Black;
      this.rbBlackHead.OverColor = System.Drawing.Color.Red;
      this.rbBlackHead.TabStop = false;
      this.rbBlackHead.Click += new System.EventHandler(this.rbWhiteHead_Click);
      // 
      // rbWhiteHead
      // 
      this.rbWhiteHead.AccessibleDescription = null;
      this.rbWhiteHead.AccessibleName = null;
      resources.ApplyResources(this.rbWhiteHead, "rbWhiteHead");
      this.rbWhiteHead.BackColor = System.Drawing.Color.White;
      this.rbWhiteHead.BackgroundImage = null;
      this.rbWhiteHead.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.rbWhiteHead.Checked = true;
      this.rbWhiteHead.Cursor = System.Windows.Forms.Cursors.Hand;
      this.rbWhiteHead.Font = null;
      this.rbWhiteHead.FrameColor = System.Drawing.Color.DodgerBlue;
      this.rbWhiteHead.Group = 2;
      this.rbWhiteHead.ImageLocation = null;
      this.rbWhiteHead.Name = "rbWhiteHead";
      this.rbWhiteHead.NormalColor = System.Drawing.Color.White;
      this.rbWhiteHead.OverColor = System.Drawing.Color.Red;
      this.rbWhiteHead.TabStop = false;
      this.rbWhiteHead.Click += new System.EventHandler(this.rbWhiteHead_Click);
      // 
      // SettingThreadLocating
      // 
      this.AccessibleDescription = null;
      this.AccessibleName = null;
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
      resources.ApplyResources(this, "$this");
      this.BackColor = System.Drawing.Color.Transparent;
      this.BackgroundImage = null;
      this.Controls.Add(this.label10);
      this.Controls.Add(this.label7);
      this.Controls.Add(this.lbThreadColor);
      this.Controls.Add(this.rbBlackHead);
      this.Controls.Add(this.rbWhiteHead);
      this.Controls.Add(this.lbName);
      this.Controls.Add(this.tbName);
      this.Controls.Add(this.label27);
      this.Name = "SettingThreadLocating";
      this.Load += new System.EventHandler(this.SettingThreadLocating_Load);
      ((System.ComponentModel.ISupportInitialize)(this.rbBlackHead)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.rbWhiteHead)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lbName;
    private System.Windows.Forms.TextBox tbName;
    private System.Windows.Forms.Label label27;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label lbThreadColor;
    private FramedPicture rbBlackHead;
    private FramedPicture rbWhiteHead;
  }
}
