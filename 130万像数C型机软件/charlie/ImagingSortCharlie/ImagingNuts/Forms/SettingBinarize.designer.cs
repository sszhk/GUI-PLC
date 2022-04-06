namespace ImagingSortCharlie.Forms
{
    partial class SettingBinarize
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
          System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingBinarize));
          this.knob = new LBSoft.IndustrialCtrls.Knobs.LBKnob();
          this.panel1 = new System.Windows.Forms.Panel();
          this.rbFullScreen = new System.Windows.Forms.RadioButton();
          this.lbKind = new System.Windows.Forms.Label();
          this.rbPartial = new System.Windows.Forms.RadioButton();
          this.label2 = new System.Windows.Forms.Label();
          this.label1 = new System.Windows.Forms.Label();
          this.cbReverse = new System.Windows.Forms.CheckBox();
          this.tbName = new System.Windows.Forms.TextBox();
          this.lbName = new System.Windows.Forms.Label();
          this.rbManual = new System.Windows.Forms.RadioButton();
          this.tbThres = new System.Windows.Forms.NumericUpDown();
          this.rbInterVar = new System.Windows.Forms.RadioButton();
          this.panel1.SuspendLayout();
          ((System.ComponentModel.ISupportInitialize)(this.tbThres)).BeginInit();
          this.SuspendLayout();
          // 
          // knob
          // 
          this.knob.BackColor = System.Drawing.Color.Transparent;
          this.knob.DrawRatio = 0.805F;
          this.knob.IndicatorColor = System.Drawing.Color.Gold;
          this.knob.IndicatorOffset = 10F;
          this.knob.KnobCenter = ((System.Drawing.PointF)(resources.GetObject("knob.KnobCenter")));
          this.knob.KnobColor = System.Drawing.Color.SteelBlue;
          this.knob.KnobRect = ((System.Drawing.RectangleF)(resources.GetObject("knob.KnobRect")));
          resources.ApplyResources(this.knob, "knob");
          this.knob.MaxValue = 255F;
          this.knob.MinValue = 0F;
          this.knob.Name = "knob";
          this.knob.Renderer = null;
          this.knob.ScaleColor = System.Drawing.Color.White;
          this.knob.StepValue = 1F;
          this.knob.Style = LBSoft.IndustrialCtrls.Knobs.LBKnob.KnobStyle.Circular;
          this.knob.Value = 0F;
          this.knob.KnobChangeValue += new LBSoft.IndustrialCtrls.Knobs.KnobChangeValue(this.knob_KnobChangeValue);
          // 
          // panel1
          // 
          this.panel1.Controls.Add(this.rbFullScreen);
          this.panel1.Controls.Add(this.lbKind);
          this.panel1.Controls.Add(this.rbPartial);
          resources.ApplyResources(this.panel1, "panel1");
          this.panel1.Name = "panel1";
          // 
          // rbFullScreen
          // 
          resources.ApplyResources(this.rbFullScreen, "rbFullScreen");
          this.rbFullScreen.ForeColor = System.Drawing.SystemColors.ControlText;
          this.rbFullScreen.Name = "rbFullScreen";
          this.rbFullScreen.UseVisualStyleBackColor = true;
          this.rbFullScreen.CheckedChanged += new System.EventHandler(this.rbFullScreen_CheckedChanged);
          // 
          // lbKind
          // 
          resources.ApplyResources(this.lbKind, "lbKind");
          this.lbKind.BackColor = System.Drawing.Color.Transparent;
          this.lbKind.ForeColor = System.Drawing.SystemColors.ControlText;
          this.lbKind.Name = "lbKind";
          // 
          // rbPartial
          // 
          resources.ApplyResources(this.rbPartial, "rbPartial");
          this.rbPartial.ForeColor = System.Drawing.SystemColors.ControlText;
          this.rbPartial.Name = "rbPartial";
          this.rbPartial.UseVisualStyleBackColor = true;
          // 
          // label2
          // 
          this.label2.BackColor = System.Drawing.Color.SteelBlue;
          resources.ApplyResources(this.label2, "label2");
          this.label2.Name = "label2";
          // 
          // label1
          // 
          this.label1.BackColor = System.Drawing.Color.SteelBlue;
          resources.ApplyResources(this.label1, "label1");
          this.label1.Name = "label1";
          // 
          // cbReverse
          // 
          resources.ApplyResources(this.cbReverse, "cbReverse");
          this.cbReverse.ForeColor = System.Drawing.SystemColors.ControlText;
          this.cbReverse.Name = "cbReverse";
          this.cbReverse.UseVisualStyleBackColor = true;
          this.cbReverse.CheckedChanged += new System.EventHandler(this.cbReverse_CheckedChanged);
          // 
          // tbName
          // 
          resources.ApplyResources(this.tbName, "tbName");
          this.tbName.Name = "tbName";
          // 
          // lbName
          // 
          resources.ApplyResources(this.lbName, "lbName");
          this.lbName.BackColor = System.Drawing.Color.Transparent;
          this.lbName.ForeColor = System.Drawing.SystemColors.ControlText;
          this.lbName.Name = "lbName";
          // 
          // rbManual
          // 
          resources.ApplyResources(this.rbManual, "rbManual");
          this.rbManual.Checked = true;
          this.rbManual.ForeColor = System.Drawing.SystemColors.ControlText;
          this.rbManual.Name = "rbManual";
          this.rbManual.TabStop = true;
          this.rbManual.UseVisualStyleBackColor = true;
          this.rbManual.CheckedChanged += new System.EventHandler(this.rbManual_CheckedChanged);
          // 
          // tbThres
          // 
          resources.ApplyResources(this.tbThres, "tbThres");
          this.tbThres.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
          this.tbThres.Name = "tbThres";
          this.tbThres.ValueChanged += new System.EventHandler(this.tbThres_ValueChanged);
          // 
          // rbInterVar
          // 
          resources.ApplyResources(this.rbInterVar, "rbInterVar");
          this.rbInterVar.ForeColor = System.Drawing.SystemColors.ControlText;
          this.rbInterVar.Name = "rbInterVar";
          this.rbInterVar.UseVisualStyleBackColor = true;
          this.rbInterVar.CheckedChanged += new System.EventHandler(this.rbInterVar_CheckedChanged);
          // 
          // SettingBinarize
          // 
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
          this.BackColor = System.Drawing.Color.Transparent;
          this.Controls.Add(this.panel1);
          this.Controls.Add(this.label2);
          this.Controls.Add(this.label1);
          this.Controls.Add(this.cbReverse);
          this.Controls.Add(this.tbName);
          this.Controls.Add(this.lbName);
          this.Controls.Add(this.rbManual);
          this.Controls.Add(this.tbThres);
          this.Controls.Add(this.rbInterVar);
          this.Controls.Add(this.knob);
          this.Name = "SettingBinarize";
          resources.ApplyResources(this, "$this");
          this.Load += new System.EventHandler(this.SettingBinarize_Load);
          this.panel1.ResumeLayout(false);
          this.panel1.PerformLayout();
          ((System.ComponentModel.ISupportInitialize)(this.tbThres)).EndInit();
          this.ResumeLayout(false);
          this.PerformLayout();

        }

        #endregion

        private LBSoft.IndustrialCtrls.Knobs.LBKnob knob;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbFullScreen;
        private System.Windows.Forms.Label lbKind;
        private System.Windows.Forms.RadioButton rbPartial;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.CheckBox cbReverse;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.RadioButton rbManual;
        private System.Windows.Forms.NumericUpDown tbThres;
        private System.Windows.Forms.RadioButton rbInterVar;
    }
}
