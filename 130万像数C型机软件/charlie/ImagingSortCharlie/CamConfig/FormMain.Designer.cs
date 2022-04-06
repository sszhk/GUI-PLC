namespace CamConfig
{
  partial class FormMain
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
      this.lv = new System.Windows.Forms.ListView();
      this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
      this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
      this.lb_info = new System.Windows.Forms.Label();
      this.btn_refresh = new System.Windows.Forms.Button();
      this.btn_save = new System.Windows.Forms.Button();
      this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
      this.SuspendLayout();
      // 
      // lv
      // 
      this.lv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader1,
            this.columnHeader3});
      this.lv.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lv.Enabled = false;
      this.lv.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.lv.ForeColor = System.Drawing.Color.DimGray;
      this.lv.FullRowSelect = true;
      this.lv.GridLines = true;
      this.lv.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
      this.lv.Location = new System.Drawing.Point(0, 0);
      this.lv.MultiSelect = false;
      this.lv.Name = "lv";
      this.lv.Size = new System.Drawing.Size(381, 313);
      this.lv.TabIndex = 0;
      this.lv.UseCompatibleStateImageBehavior = false;
      this.lv.View = System.Windows.Forms.View.Details;
      this.lv.SelectedIndexChanged += new System.EventHandler(this.lv_SelectedIndexChanged);
      this.lv.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lv_MouseUp);
      this.lv.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lv_MouseMove);
      this.lv.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lv_MouseDown);
      this.lv.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lv_ItemSelectionChanged);
      // 
      // columnHeader2
      // 
      this.columnHeader2.Text = "View";
      this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.columnHeader2.Width = 77;
      // 
      // columnHeader1
      // 
      this.columnHeader1.Text = "CamSerial";
      this.columnHeader1.Width = 92;
      // 
      // lb_info
      // 
      this.lb_info.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lb_info.Location = new System.Drawing.Point(0, 313);
      this.lb_info.Name = "lb_info";
      this.lb_info.Size = new System.Drawing.Size(381, 27);
      this.lb_info.TabIndex = 1;
      this.lb_info.Text = "No cameras found";
      this.lb_info.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // btn_refresh
      // 
      this.btn_refresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btn_refresh.Location = new System.Drawing.Point(230, 317);
      this.btn_refresh.Name = "btn_refresh";
      this.btn_refresh.Size = new System.Drawing.Size(75, 22);
      this.btn_refresh.TabIndex = 2;
      this.btn_refresh.Text = "&Refresh";
      this.btn_refresh.UseVisualStyleBackColor = true;
      this.btn_refresh.Click += new System.EventHandler(this.btn_refresh_Click);
      // 
      // btn_save
      // 
      this.btn_save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btn_save.Location = new System.Drawing.Point(306, 317);
      this.btn_save.Name = "btn_save";
      this.btn_save.Size = new System.Drawing.Size(75, 22);
      this.btn_save.TabIndex = 3;
      this.btn_save.Text = "&Save";
      this.btn_save.UseVisualStyleBackColor = true;
      this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
      // 
      // columnHeader3
      // 
      this.columnHeader3.Text = "Present";
      this.columnHeader3.Width = 150;
      // 
      // FormMain
      // 
      this.AcceptButton = this.btn_refresh;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(381, 340);
      this.Controls.Add(this.btn_save);
      this.Controls.Add(this.btn_refresh);
      this.Controls.Add(this.lv);
      this.Controls.Add(this.lb_info);
      this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Name = "FormMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Camera Configuration";
      this.Load += new System.EventHandler(this.FormMain_Load);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ListView lv;
    private System.Windows.Forms.Label lb_info;
    private System.Windows.Forms.ColumnHeader columnHeader1;
    private System.Windows.Forms.ColumnHeader columnHeader2;
    private System.Windows.Forms.Button btn_refresh;
    private System.Windows.Forms.Button btn_save;
    private System.Windows.Forms.ColumnHeader columnHeader3;
  }
}

