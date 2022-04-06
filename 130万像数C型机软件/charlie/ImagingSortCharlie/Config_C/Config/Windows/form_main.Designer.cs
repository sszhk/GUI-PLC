namespace ConfigApp.Windows
{
  partial class form_main
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

    #region Windows 窗体设计器生成的代码

    /// <summary>
    /// 设计器支持所需的方法 - 不要
    /// 使用代码编辑器修改此方法的内容。
    /// </summary>
    private void InitializeComponent()
    {
      this.btn_save = new System.Windows.Forms.Button();
      this.pl_camera = new System.Windows.Forms.Panel();
      this.pl_setting = new System.Windows.Forms.Panel();
      this.btn_exit = new System.Windows.Forms.Button();
      this.tb_sn = new System.Windows.Forms.TextBox();
      this.lb_sn = new System.Windows.Forms.Label();
      this.btn_get = new System.Windows.Forms.Button();
      this.tb_mn = new System.Windows.Forms.TextBox();
      this.lb_mn = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // btn_save
      // 
      this.btn_save.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.btn_save.Location = new System.Drawing.Point(350, 467);
      this.btn_save.Name = "btn_save";
      this.btn_save.Size = new System.Drawing.Size(100, 40);
      this.btn_save.TabIndex = 65;
      this.btn_save.Text = "保  存";
      this.btn_save.UseVisualStyleBackColor = true;
      this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
      // 
      // pl_camera
      // 
      this.pl_camera.Location = new System.Drawing.Point(0, 0);
      this.pl_camera.Name = "pl_camera";
      this.pl_camera.Size = new System.Drawing.Size(320, 400);
      this.pl_camera.TabIndex = 32;
      // 
      // pl_setting
      // 
      this.pl_setting.BackColor = System.Drawing.Color.Transparent;
      this.pl_setting.Location = new System.Drawing.Point(350, 0);
      this.pl_setting.Name = "pl_setting";
      this.pl_setting.Size = new System.Drawing.Size(320, 460);
      this.pl_setting.TabIndex = 33;
      // 
      // btn_exit
      // 
      this.btn_exit.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.btn_exit.Location = new System.Drawing.Point(570, 467);
      this.btn_exit.Name = "btn_exit";
      this.btn_exit.Size = new System.Drawing.Size(100, 40);
      this.btn_exit.TabIndex = 66;
      this.btn_exit.Text = "退  出";
      this.btn_exit.UseVisualStyleBackColor = true;
      this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
      // 
      // tb_sn
      // 
      this.tb_sn.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.tb_sn.Location = new System.Drawing.Point(56, 481);
      this.tb_sn.MaxLength = 35;
      this.tb_sn.Name = "tb_sn";
      this.tb_sn.Size = new System.Drawing.Size(250, 23);
      this.tb_sn.TabIndex = 68;
      this.tb_sn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      // 
      // lb_sn
      // 
      this.lb_sn.Location = new System.Drawing.Point(6, 481);
      this.lb_sn.Name = "lb_sn";
      this.lb_sn.Size = new System.Drawing.Size(50, 20);
      this.lb_sn.TabIndex = 67;
      this.lb_sn.Text = "注册码";
      this.lb_sn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // btn_get
      // 
      this.btn_get.Location = new System.Drawing.Point(250, 437);
      this.btn_get.Name = "btn_get";
      this.btn_get.Size = new System.Drawing.Size(50, 23);
      this.btn_get.TabIndex = 330;
      this.btn_get.Text = "获取";
      this.btn_get.UseVisualStyleBackColor = true;
      this.btn_get.Click += new System.EventHandler(this.btn_get_Click);
      // 
      // tb_mn
      // 
      this.tb_mn.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold);
      this.tb_mn.Location = new System.Drawing.Point(56, 434);
      this.tb_mn.MaxLength = 35;
      this.tb_mn.Name = "tb_mn";
      this.tb_mn.ReadOnly = true;
      this.tb_mn.Size = new System.Drawing.Size(188, 26);
      this.tb_mn.TabIndex = 331;
      this.tb_mn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      // 
      // lb_mn
      // 
      this.lb_mn.Location = new System.Drawing.Point(6, 434);
      this.lb_mn.Name = "lb_mn";
      this.lb_mn.Size = new System.Drawing.Size(50, 20);
      this.lb_mn.TabIndex = 332;
      this.lb_mn.Text = "机器码";
      this.lb_mn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // form_main
      // 
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
      this.BackColor = System.Drawing.Color.Linen;
      this.ClientSize = new System.Drawing.Size(689, 514);
      this.ControlBox = false;
      this.Controls.Add(this.lb_mn);
      this.Controls.Add(this.tb_mn);
      this.Controls.Add(this.btn_get);
      this.Controls.Add(this.tb_sn);
      this.Controls.Add(this.lb_sn);
      this.Controls.Add(this.btn_exit);
      this.Controls.Add(this.btn_save);
      this.Controls.Add(this.pl_setting);
      this.Controls.Add(this.pl_camera);
      this.Name = "form_main";
      this.Text = "配置选项";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btn_save;
    private System.Windows.Forms.Panel pl_camera;
    private System.Windows.Forms.Panel pl_setting;
    private System.Windows.Forms.Button btn_exit;
    private System.Windows.Forms.TextBox tb_sn;
    private System.Windows.Forms.Label lb_sn;
    private System.Windows.Forms.Button btn_get;
    private System.Windows.Forms.TextBox tb_mn;
    private System.Windows.Forms.Label lb_mn;


  }
}

