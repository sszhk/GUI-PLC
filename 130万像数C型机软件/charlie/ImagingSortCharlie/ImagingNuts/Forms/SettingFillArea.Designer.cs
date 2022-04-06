namespace ImagingSortCharlie.Forms
{
  partial class SettingFillArea
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingFillArea));
      this.lbName = new System.Windows.Forms.Label();
      this.tbName = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.label10 = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this.lb_cushion_color = new System.Windows.Forms.Label();
      this.rb_black = new ImagingSortCharlie.FramedPicture(this.components);
      this.rb_white = new ImagingSortCharlie.FramedPicture(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.rb_black)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.rb_white)).BeginInit();
      this.SuspendLayout();
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
      // label4
      // 
      this.label4.BackColor = System.Drawing.Color.SteelBlue;
      resources.ApplyResources(this.label4, "label4");
      this.label4.Name = "label4";
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
      // lb_cushion_color
      // 
      resources.ApplyResources(this.lb_cushion_color, "lb_cushion_color");
      this.lb_cushion_color.ForeColor = System.Drawing.SystemColors.ControlText;
      this.lb_cushion_color.Name = "lb_cushion_color";
      // 
      // rb_black
      // 
      this.rb_black.BackColor = System.Drawing.Color.Black;
      this.rb_black.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.rb_black.Checked = false;
      this.rb_black.Cursor = System.Windows.Forms.Cursors.Hand;
      this.rb_black.FrameColor = System.Drawing.Color.DodgerBlue;
      this.rb_black.Group = 2;
      resources.ApplyResources(this.rb_black, "rb_black");
      this.rb_black.Name = "rb_black";
      this.rb_black.NormalColor = System.Drawing.Color.Black;
      this.rb_black.OverColor = System.Drawing.Color.Red;
      this.rb_black.TabStop = false;
      this.rb_black.Click += new System.EventHandler(this.rbWhiteHead_Click);
      // 
      // rb_white
      // 
      this.rb_white.BackColor = System.Drawing.Color.White;
      this.rb_white.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.rb_white.Checked = true;
      this.rb_white.Cursor = System.Windows.Forms.Cursors.Hand;
      this.rb_white.FrameColor = System.Drawing.Color.DodgerBlue;
      this.rb_white.Group = 2;
      resources.ApplyResources(this.rb_white, "rb_white");
      this.rb_white.Name = "rb_white";
      this.rb_white.NormalColor = System.Drawing.Color.White;
      this.rb_white.OverColor = System.Drawing.Color.Red;
      this.rb_white.TabStop = false;
      this.rb_white.Click += new System.EventHandler(this.rbWhiteHead_Click);
      // 
      // SettingFillArea
      // 
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
      this.BackColor = System.Drawing.Color.Transparent;
      this.Controls.Add(this.label10);
      this.Controls.Add(this.label7);
      this.Controls.Add(this.lb_cushion_color);
      this.Controls.Add(this.rb_black);
      this.Controls.Add(this.rb_white);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.lbName);
      this.Controls.Add(this.tbName);
      this.Name = "SettingFillArea";
      resources.ApplyResources(this, "$this");
      this.Load += new System.EventHandler(this.SettingFillArea_Load);
      ((System.ComponentModel.ISupportInitialize)(this.rb_black)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.rb_white)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lbName;
    private System.Windows.Forms.TextBox tbName;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label lb_cushion_color;
    private FramedPicture rb_black;
    private FramedPicture rb_white;
  }
}
