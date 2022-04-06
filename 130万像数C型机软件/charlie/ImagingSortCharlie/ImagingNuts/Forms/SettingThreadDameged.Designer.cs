namespace ImagingSortCharlie.Forms
{
  partial class SettingThreadDamage
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingThreadDamage));
      this.cbThreadDamaged = new System.Windows.Forms.CheckBox();
      this.lbName = new System.Windows.Forms.Label();
      this.tbName = new System.Windows.Forms.TextBox();
      this.label27 = new System.Windows.Forms.Label();
      this.label32 = new System.Windows.Forms.Label();
      this.tbHeight = new System.Windows.Forms.NumericUpDown();
      this.lbHeight = new System.Windows.Forms.Label();
      this.tbWidth = new System.Windows.Forms.NumericUpDown();
      this.lbWidth = new System.Windows.Forms.Label();
      this.tb_contrast = new System.Windows.Forms.NumericUpDown();
      this.label1 = new System.Windows.Forms.Label();
      this.tbThreadDameged = new ImagingSortCharlie.CalcTextBox();
      this.cb_rotated = new System.Windows.Forms.CheckBox();
      ((System.ComponentModel.ISupportInitialize)(this.tbHeight)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.tbWidth)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.tb_contrast)).BeginInit();
      this.SuspendLayout();
      // 
      // cbThreadDamaged
      // 
      this.cbThreadDamaged.BackColor = System.Drawing.Color.Transparent;
      this.cbThreadDamaged.Checked = true;
      this.cbThreadDamaged.CheckState = System.Windows.Forms.CheckState.Checked;
      this.cbThreadDamaged.ForeColor = System.Drawing.SystemColors.ControlText;
      resources.ApplyResources(this.cbThreadDamaged, "cbThreadDamaged");
      this.cbThreadDamaged.Name = "cbThreadDamaged";
      this.cbThreadDamaged.UseVisualStyleBackColor = false;
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
      // label27
      // 
      this.label27.BackColor = System.Drawing.Color.SteelBlue;
      resources.ApplyResources(this.label27, "label27");
      this.label27.Name = "label27";
      // 
      // label32
      // 
      this.label32.BackColor = System.Drawing.Color.Gainsboro;
      resources.ApplyResources(this.label32, "label32");
      this.label32.Name = "label32";
      // 
      // tbHeight
      // 
      resources.ApplyResources(this.tbHeight, "tbHeight");
      this.tbHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tbHeight.Name = "tbHeight";
      this.tbHeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tbHeight.ValueChanged += new System.EventHandler(this.tbHeight_ValueChanged);
      // 
      // lbHeight
      // 
      resources.ApplyResources(this.lbHeight, "lbHeight");
      this.lbHeight.BackColor = System.Drawing.Color.Transparent;
      this.lbHeight.ForeColor = System.Drawing.SystemColors.ControlText;
      this.lbHeight.Name = "lbHeight";
      // 
      // tbWidth
      // 
      resources.ApplyResources(this.tbWidth, "tbWidth");
      this.tbWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tbWidth.Name = "tbWidth";
      this.tbWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tbWidth.ValueChanged += new System.EventHandler(this.tbWidth_ValueChanged);
      // 
      // lbWidth
      // 
      resources.ApplyResources(this.lbWidth, "lbWidth");
      this.lbWidth.BackColor = System.Drawing.Color.Transparent;
      this.lbWidth.ForeColor = System.Drawing.SystemColors.ControlText;
      this.lbWidth.Name = "lbWidth";
      // 
      // tb_contrast
      // 
      this.tb_contrast.DecimalPlaces = 1;
      this.tb_contrast.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
      resources.ApplyResources(this.tb_contrast, "tb_contrast");
      this.tb_contrast.Maximum = new decimal(new int[] {
            89,
            0,
            0,
            0});
      this.tb_contrast.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tb_contrast.Name = "tb_contrast";
      this.tb_contrast.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tb_contrast.ValueChanged += new System.EventHandler(this.tb_contrast_ValueChanged);
      // 
      // label1
      // 
      resources.ApplyResources(this.label1, "label1");
      this.label1.BackColor = System.Drawing.Color.Transparent;
      this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
      this.label1.Name = "label1";
      // 
      // tbThreadDameged
      // 
      this.tbThreadDameged.AllowPromptAsInput = false;
      this.tbThreadDameged.ClearOnActive = true;
      this.tbThreadDameged.HidePromptOnLeave = true;
      resources.ApplyResources(this.tbThreadDameged, "tbThreadDameged");
      this.tbThreadDameged.Name = "tbThreadDameged";
      // 
      // cb_rotated
      // 
      this.cb_rotated.BackColor = System.Drawing.Color.Transparent;
      this.cb_rotated.Checked = true;
      this.cb_rotated.CheckState = System.Windows.Forms.CheckState.Checked;
      this.cb_rotated.ForeColor = System.Drawing.SystemColors.ControlText;
      resources.ApplyResources(this.cb_rotated, "cb_rotated");
      this.cb_rotated.Name = "cb_rotated";
      this.cb_rotated.UseVisualStyleBackColor = false;
      this.cb_rotated.CheckedChanged += new System.EventHandler(this.cb_rotated_CheckedChanged);
      // 
      // SettingThreadDamage
      // 
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
      this.BackColor = System.Drawing.Color.Transparent;
      this.Controls.Add(this.cb_rotated);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.tb_contrast);
      this.Controls.Add(this.lbWidth);
      this.Controls.Add(this.tbWidth);
      this.Controls.Add(this.lbHeight);
      this.Controls.Add(this.tbHeight);
      this.Controls.Add(this.label32);
      this.Controls.Add(this.lbName);
      this.Controls.Add(this.tbName);
      this.Controls.Add(this.label27);
      this.Controls.Add(this.tbThreadDameged);
      this.Controls.Add(this.cbThreadDamaged);
      this.Name = "SettingThreadDamage";
      resources.ApplyResources(this, "$this");
      this.Load += new System.EventHandler(this.SettingThreadDameged_Load);
      ((System.ComponentModel.ISupportInitialize)(this.tbHeight)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.tbWidth)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.tb_contrast)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private CalcTextBox tbThreadDameged;
    private System.Windows.Forms.CheckBox cbThreadDamaged;
    private System.Windows.Forms.Label lbName;
    private System.Windows.Forms.TextBox tbName;
    private System.Windows.Forms.Label label27;
    private System.Windows.Forms.Label label32;
    private System.Windows.Forms.NumericUpDown tbHeight;
    private System.Windows.Forms.Label lbHeight;
    private System.Windows.Forms.NumericUpDown tbWidth;
    private System.Windows.Forms.Label lbWidth;
    private System.Windows.Forms.NumericUpDown tb_contrast;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.CheckBox cb_rotated;
  }
}
