namespace ImagingSortCharlie.Forms
{
  partial class SettingArea
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingArea));
      this.label4 = new System.Windows.Forms.Label();
      this.lb_name = new System.Windows.Forms.Label();
      this.tb_name = new System.Windows.Forms.TextBox();
      this.lb_rely = new System.Windows.Forms.Label();
      this.cb_teeth = new System.Windows.Forms.CheckBox();
      this.cb_locating = new System.Windows.Forms.CheckBox();
      this.lb_black = new System.Windows.Forms.Label();
      this.lb_white = new System.Windows.Forms.Label();
      this.lb_color = new System.Windows.Forms.Label();
      this.label15 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.cb_area = new System.Windows.Forms.CheckBox();
      this.label2 = new System.Windows.Forms.Label();
      this.label8 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.tb_min_area = new ImagingSortCharlie.CalcTextBox();
      this.tb_max_area = new ImagingSortCharlie.CalcTextBox();
      this.rbBlackHead = new ImagingSortCharlie.FramedPicture(this.components);
      this.rbWhiteHead = new ImagingSortCharlie.FramedPicture(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.rbBlackHead)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.rbWhiteHead)).BeginInit();
      this.SuspendLayout();
      // 
      // label4
      // 
      this.label4.BackColor = System.Drawing.Color.SteelBlue;
      resources.ApplyResources(this.label4, "label4");
      this.label4.Name = "label4";
      // 
      // lb_name
      // 
      resources.ApplyResources(this.lb_name, "lb_name");
      this.lb_name.BackColor = System.Drawing.Color.Transparent;
      this.lb_name.ForeColor = System.Drawing.SystemColors.ControlText;
      this.lb_name.Name = "lb_name";
      // 
      // tb_name
      // 
      resources.ApplyResources(this.tb_name, "tb_name");
      this.tb_name.Name = "tb_name";
      // 
      // lb_rely
      // 
      resources.ApplyResources(this.lb_rely, "lb_rely");
      this.lb_rely.BackColor = System.Drawing.Color.Transparent;
      this.lb_rely.ForeColor = System.Drawing.SystemColors.ControlText;
      this.lb_rely.Name = "lb_rely";
      // 
      // cb_teeth
      // 
      this.cb_teeth.BackColor = System.Drawing.Color.Transparent;
      this.cb_teeth.ForeColor = System.Drawing.SystemColors.ControlText;
      resources.ApplyResources(this.cb_teeth, "cb_teeth");
      this.cb_teeth.Name = "cb_teeth";
      this.cb_teeth.UseVisualStyleBackColor = false;
      this.cb_teeth.CheckedChanged += new System.EventHandler(this.cb_teeth_CheckedChanged);
      // 
      // cb_locating
      // 
      this.cb_locating.BackColor = System.Drawing.Color.Transparent;
      this.cb_locating.ForeColor = System.Drawing.SystemColors.ControlText;
      resources.ApplyResources(this.cb_locating, "cb_locating");
      this.cb_locating.Name = "cb_locating";
      this.cb_locating.UseVisualStyleBackColor = false;
      this.cb_locating.CheckedChanged += new System.EventHandler(this.cb_locating_CheckedChanged);
      // 
      // lb_black
      // 
      resources.ApplyResources(this.lb_black, "lb_black");
      this.lb_black.ForeColor = System.Drawing.SystemColors.ControlText;
      this.lb_black.Name = "lb_black";
      // 
      // lb_white
      // 
      resources.ApplyResources(this.lb_white, "lb_white");
      this.lb_white.ForeColor = System.Drawing.SystemColors.ControlText;
      this.lb_white.Name = "lb_white";
      // 
      // lb_color
      // 
      resources.ApplyResources(this.lb_color, "lb_color");
      this.lb_color.ForeColor = System.Drawing.SystemColors.ControlText;
      this.lb_color.Name = "lb_color";
      // 
      // label15
      // 
      resources.ApplyResources(this.label15, "label15");
      this.label15.ForeColor = System.Drawing.SystemColors.ControlText;
      this.label15.Name = "label15";
      // 
      // label1
      // 
      resources.ApplyResources(this.label1, "label1");
      this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
      this.label1.Name = "label1";
      // 
      // cb_area
      // 
      this.cb_area.BackColor = System.Drawing.Color.Transparent;
      this.cb_area.Checked = true;
      this.cb_area.CheckState = System.Windows.Forms.CheckState.Checked;
      this.cb_area.ForeColor = System.Drawing.SystemColors.ControlText;
      resources.ApplyResources(this.cb_area, "cb_area");
      this.cb_area.Name = "cb_area";
      this.cb_area.UseVisualStyleBackColor = false;
      // 
      // label2
      // 
      resources.ApplyResources(this.label2, "label2");
      this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
      this.label2.Name = "label2";
      // 
      // label8
      // 
      this.label8.BackColor = System.Drawing.Color.SteelBlue;
      resources.ApplyResources(this.label8, "label8");
      this.label8.Name = "label8";
      // 
      // label3
      // 
      this.label3.BackColor = System.Drawing.Color.SteelBlue;
      resources.ApplyResources(this.label3, "label3");
      this.label3.Name = "label3";
      // 
      // tb_min_area
      // 
      this.tb_min_area.AllowPromptAsInput = false;
      this.tb_min_area.ClearOnActive = true;
      this.tb_min_area.HidePromptOnLeave = true;
      resources.ApplyResources(this.tb_min_area, "tb_min_area");
      this.tb_min_area.Name = "tb_min_area";
      // 
      // tb_max_area
      // 
      this.tb_max_area.AllowPromptAsInput = false;
      this.tb_max_area.ClearOnActive = true;
      this.tb_max_area.HidePromptOnLeave = true;
      resources.ApplyResources(this.tb_max_area, "tb_max_area");
      this.tb_max_area.Name = "tb_max_area";
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
      // SettingArea
      // 
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
      this.BackColor = System.Drawing.Color.Transparent;
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label8);
      this.Controls.Add(this.tb_min_area);
      this.Controls.Add(this.label15);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.cb_area);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.tb_max_area);
      this.Controls.Add(this.lb_black);
      this.Controls.Add(this.lb_white);
      this.Controls.Add(this.lb_color);
      this.Controls.Add(this.rbBlackHead);
      this.Controls.Add(this.rbWhiteHead);
      this.Controls.Add(this.cb_locating);
      this.Controls.Add(this.cb_teeth);
      this.Controls.Add(this.lb_rely);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.lb_name);
      this.Controls.Add(this.tb_name);
      this.Name = "SettingArea";
      resources.ApplyResources(this, "$this");
      this.Load += new System.EventHandler(this.SettingArea_Load);
      ((System.ComponentModel.ISupportInitialize)(this.rbBlackHead)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.rbWhiteHead)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label lb_name;
    private System.Windows.Forms.TextBox tb_name;
    private System.Windows.Forms.Label lb_rely;
    private System.Windows.Forms.CheckBox cb_teeth;
    private System.Windows.Forms.CheckBox cb_locating;
    private System.Windows.Forms.Label lb_black;
    private System.Windows.Forms.Label lb_white;
    private System.Windows.Forms.Label lb_color;
    private FramedPicture rbBlackHead;
    private FramedPicture rbWhiteHead;
    private CalcTextBox tb_min_area;
    private System.Windows.Forms.Label label15;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.CheckBox cb_area;
    private System.Windows.Forms.Label label2;
    private CalcTextBox tb_max_area;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Label label3;
  }
}
