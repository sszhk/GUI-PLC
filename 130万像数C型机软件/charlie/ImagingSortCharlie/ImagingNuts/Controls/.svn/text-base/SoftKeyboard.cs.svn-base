using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Globalization;
using System.Drawing.Drawing2D;
namespace ImagingSortCharlie
{
	/// <summary>
	/// Summary description for frmCalculator.
	/// </summary>
	public class SoftKeyboard : System.Windows.Forms.Form
	{
		private KeyButton btnEight;
    private KeyButton btnNine;
		private KeyButton btnSeven;
		private KeyButton btnFive;
    private KeyButton btnSix;
		private KeyButton btnFour;
		private KeyButton btnDot;
    private KeyButton btnPlusMinus;
		private KeyButton btnZero;
		private KeyButton btnTwo;
    private KeyButton btnThree;
		private KeyButton btnOne;
		private KeyButton btnBack;
		private KeyButton btnEqual;
    private KeyButton btnClear;
    private KeyButton btnExpand;
    private Panel plControl;
    private Panel plDigits;
    private Panel plAlpha;
    private KeyButton btnA;
    private KeyButton btnB;
    private KeyButton btnC;
    private KeyButton btnG;
    private KeyButton btnF;
    private KeyButton btnE;
    private KeyButton btnD;
    private KeyButton btnCaps;
    private KeyButton btnZ;
    private KeyButton btnX;
    private KeyButton btnY;
    private KeyButton btnW;
    private KeyButton btnV;
    private KeyButton btnU;
    private KeyButton btnT;
    private KeyButton btnS;
    private KeyButton btnQ;
    private KeyButton btnR;
    private KeyButton btnP;
    private KeyButton btnO;
    private KeyButton btnN;
    private KeyButton btnM;
    private KeyButton btnL;
    private KeyButton btnJ;
    private KeyButton btnK;
    private KeyButton btnI;
    private KeyButton btnH;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public delegate void ValueChangedEvent(object sender, string value);
		public event ValueChangedEvent ValueCleared=null; 
		public event KeyPressEventHandler NumberKeyPressed=null;
		public delegate void ErrorEvent(object sender, string Message);
    public event EventHandler Canceled = null;
    public event EventHandler Finished = null;

    private Button[] alpha;
		public SoftKeyboard()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this.btnDot.Text=CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
      alpha = new Button[] { 
        btnA, btnB, btnC,btnD,btnE,
        btnF,btnG,btnH, btnI, btnJ,
        btnK, btnL, btnM,btnN,btnO,
        btnP, btnQ, btnR, btnS, btnT,
        btnU, btnV, btnW, btnX, btnY,
        btnZ
      };
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      this.plControl = new System.Windows.Forms.Panel();
      this.btnBack = new ImagingSortCharlie.KeyButton();
      this.btnExpand = new ImagingSortCharlie.KeyButton();
      this.btnClear = new ImagingSortCharlie.KeyButton();
      this.btnEqual = new ImagingSortCharlie.KeyButton();
      this.plDigits = new System.Windows.Forms.Panel();
      this.btnSeven = new ImagingSortCharlie.KeyButton();
      this.btnDot = new ImagingSortCharlie.KeyButton();
      this.btnNine = new ImagingSortCharlie.KeyButton();
      this.btnPlusMinus = new ImagingSortCharlie.KeyButton();
      this.btnEight = new ImagingSortCharlie.KeyButton();
      this.btnZero = new ImagingSortCharlie.KeyButton();
      this.btnFour = new ImagingSortCharlie.KeyButton();
      this.btnTwo = new ImagingSortCharlie.KeyButton();
      this.btnSix = new ImagingSortCharlie.KeyButton();
      this.btnThree = new ImagingSortCharlie.KeyButton();
      this.btnFive = new ImagingSortCharlie.KeyButton();
      this.btnOne = new ImagingSortCharlie.KeyButton();
      this.plAlpha = new System.Windows.Forms.Panel();
      this.btnCaps = new ImagingSortCharlie.KeyButton();
      this.btnZ = new ImagingSortCharlie.KeyButton();
      this.btnX = new ImagingSortCharlie.KeyButton();
      this.btnY = new ImagingSortCharlie.KeyButton();
      this.btnW = new ImagingSortCharlie.KeyButton();
      this.btnV = new ImagingSortCharlie.KeyButton();
      this.btnU = new ImagingSortCharlie.KeyButton();
      this.btnT = new ImagingSortCharlie.KeyButton();
      this.btnS = new ImagingSortCharlie.KeyButton();
      this.btnQ = new ImagingSortCharlie.KeyButton();
      this.btnR = new ImagingSortCharlie.KeyButton();
      this.btnP = new ImagingSortCharlie.KeyButton();
      this.btnO = new ImagingSortCharlie.KeyButton();
      this.btnN = new ImagingSortCharlie.KeyButton();
      this.btnM = new ImagingSortCharlie.KeyButton();
      this.btnL = new ImagingSortCharlie.KeyButton();
      this.btnJ = new ImagingSortCharlie.KeyButton();
      this.btnK = new ImagingSortCharlie.KeyButton();
      this.btnI = new ImagingSortCharlie.KeyButton();
      this.btnH = new ImagingSortCharlie.KeyButton();
      this.btnG = new ImagingSortCharlie.KeyButton();
      this.btnF = new ImagingSortCharlie.KeyButton();
      this.btnE = new ImagingSortCharlie.KeyButton();
      this.btnC = new ImagingSortCharlie.KeyButton();
      this.btnD = new ImagingSortCharlie.KeyButton();
      this.btnB = new ImagingSortCharlie.KeyButton();
      this.btnA = new ImagingSortCharlie.KeyButton();
      this.plControl.SuspendLayout();
      this.plDigits.SuspendLayout();
      this.plAlpha.SuspendLayout();
      this.SuspendLayout();
      // 
      // plControl
      // 
      this.plControl.BackColor = System.Drawing.Color.Transparent;
      this.plControl.Controls.Add(this.btnBack);
      this.plControl.Controls.Add(this.btnExpand);
      this.plControl.Controls.Add(this.btnClear);
      this.plControl.Controls.Add(this.btnEqual);
      this.plControl.Location = new System.Drawing.Point(170, 9);
      this.plControl.Margin = new System.Windows.Forms.Padding(0);
      this.plControl.Name = "plControl";
      this.plControl.Size = new System.Drawing.Size(112, 215);
      this.plControl.TabIndex = 24;
      // 
      // btnBack
      // 
      this.btnBack.BackColor = System.Drawing.Color.DimGray;
      this.btnBack.Checked = false;
      this.btnBack.Curve = 4;
      this.btnBack.ForeColor = System.Drawing.Color.RoyalBlue;
      this.btnBack.Location = new System.Drawing.Point(3, 2);
      this.btnBack.Name = "btnBack";
      this.btnBack.Size = new System.Drawing.Size(104, 48);
      this.btnBack.TabIndex = 22;
      this.btnBack.TabStop = false;
      this.btnBack.Text = "退格";
      this.btnBack.UseVisualStyleBackColor = false;
      this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
      this.btnBack.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmCalculator_KeyDown);
      // 
      // btnExpand
      // 
      this.btnExpand.BackColor = System.Drawing.Color.DimGray;
      this.btnExpand.Checked = false;
      this.btnExpand.Curve = 4;
      this.btnExpand.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnExpand.Location = new System.Drawing.Point(3, 159);
      this.btnExpand.Name = "btnExpand";
      this.btnExpand.Size = new System.Drawing.Size(104, 48);
      this.btnExpand.TabIndex = 23;
      this.btnExpand.TabStop = false;
      this.btnExpand.Text = ">>";
      this.btnExpand.UseVisualStyleBackColor = false;
      this.btnExpand.Click += new System.EventHandler(this.btnExpand_Click);
      // 
      // btnClear
      // 
      this.btnClear.BackColor = System.Drawing.Color.DimGray;
      this.btnClear.Checked = false;
      this.btnClear.Curve = 4;
      this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnClear.ForeColor = System.Drawing.Color.Red;
      this.btnClear.Location = new System.Drawing.Point(3, 54);
      this.btnClear.Name = "btnClear";
      this.btnClear.Size = new System.Drawing.Size(104, 48);
      this.btnClear.TabIndex = 20;
      this.btnClear.TabStop = false;
      this.btnClear.Text = "清除";
      this.btnClear.UseVisualStyleBackColor = false;
      this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
      this.btnClear.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmCalculator_KeyDown);
      // 
      // btnEqual
      // 
      this.btnEqual.BackColor = System.Drawing.Color.DimGray;
      this.btnEqual.Checked = false;
      this.btnEqual.Curve = 4;
      this.btnEqual.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnEqual.ForeColor = System.Drawing.Color.OliveDrab;
      this.btnEqual.Location = new System.Drawing.Point(3, 108);
      this.btnEqual.Name = "btnEqual";
      this.btnEqual.Size = new System.Drawing.Size(104, 47);
      this.btnEqual.TabIndex = 21;
      this.btnEqual.TabStop = false;
      this.btnEqual.Text = "确定";
      this.btnEqual.UseVisualStyleBackColor = false;
      this.btnEqual.Click += new System.EventHandler(this.btnEqual_Click);
      this.btnEqual.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmCalculator_KeyDown);
      // 
      // plDigits
      // 
      this.plDigits.BackColor = System.Drawing.Color.Transparent;
      this.plDigits.Controls.Add(this.btnSeven);
      this.plDigits.Controls.Add(this.btnDot);
      this.plDigits.Controls.Add(this.btnNine);
      this.plDigits.Controls.Add(this.btnPlusMinus);
      this.plDigits.Controls.Add(this.btnEight);
      this.plDigits.Controls.Add(this.btnZero);
      this.plDigits.Controls.Add(this.btnFour);
      this.plDigits.Controls.Add(this.btnTwo);
      this.plDigits.Controls.Add(this.btnSix);
      this.plDigits.Controls.Add(this.btnThree);
      this.plDigits.Controls.Add(this.btnFive);
      this.plDigits.Controls.Add(this.btnOne);
      this.plDigits.Location = new System.Drawing.Point(10, 9);
      this.plDigits.Name = "plDigits";
      this.plDigits.Size = new System.Drawing.Size(160, 215);
      this.plDigits.TabIndex = 25;
      // 
      // btnSeven
      // 
      this.btnSeven.BackColor = System.Drawing.Color.DimGray;
      this.btnSeven.Checked = false;
      this.btnSeven.Curve = 4;
      this.btnSeven.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnSeven.Location = new System.Drawing.Point(2, 2);
      this.btnSeven.Name = "btnSeven";
      this.btnSeven.Size = new System.Drawing.Size(49, 48);
      this.btnSeven.TabIndex = 4;
      this.btnSeven.TabStop = false;
      this.btnSeven.Text = "7";
      this.btnSeven.UseVisualStyleBackColor = false;
      this.btnSeven.Click += new System.EventHandler(this.number_Click);
      this.btnSeven.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmCalculator_KeyDown);
      // 
      // btnDot
      // 
      this.btnDot.BackColor = System.Drawing.Color.DimGray;
      this.btnDot.Checked = false;
      this.btnDot.Curve = 4;
      this.btnDot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnDot.Location = new System.Drawing.Point(106, 159);
      this.btnDot.Name = "btnDot";
      this.btnDot.Size = new System.Drawing.Size(49, 48);
      this.btnDot.TabIndex = 19;
      this.btnDot.TabStop = false;
      this.btnDot.Text = ".";
      this.btnDot.UseVisualStyleBackColor = false;
      this.btnDot.Click += new System.EventHandler(this.btnDot_Click);
      this.btnDot.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmCalculator_KeyDown);
      // 
      // btnNine
      // 
      this.btnNine.BackColor = System.Drawing.Color.DimGray;
      this.btnNine.Checked = false;
      this.btnNine.Curve = 4;
      this.btnNine.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnNine.Location = new System.Drawing.Point(106, 2);
      this.btnNine.Name = "btnNine";
      this.btnNine.Size = new System.Drawing.Size(49, 48);
      this.btnNine.TabIndex = 6;
      this.btnNine.TabStop = false;
      this.btnNine.Text = "9";
      this.btnNine.UseVisualStyleBackColor = false;
      this.btnNine.Click += new System.EventHandler(this.number_Click);
      this.btnNine.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmCalculator_KeyDown);
      // 
      // btnPlusMinus
      // 
      this.btnPlusMinus.BackColor = System.Drawing.Color.DimGray;
      this.btnPlusMinus.Checked = false;
      this.btnPlusMinus.Curve = 4;
      this.btnPlusMinus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnPlusMinus.Location = new System.Drawing.Point(2, 159);
      this.btnPlusMinus.Name = "btnPlusMinus";
      this.btnPlusMinus.Size = new System.Drawing.Size(49, 48);
      this.btnPlusMinus.TabIndex = 18;
      this.btnPlusMinus.TabStop = false;
      this.btnPlusMinus.Text = "_";
      this.btnPlusMinus.UseVisualStyleBackColor = false;
      this.btnPlusMinus.Click += new System.EventHandler(this.letters_Click);
      this.btnPlusMinus.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmCalculator_KeyDown);
      // 
      // btnEight
      // 
      this.btnEight.BackColor = System.Drawing.Color.DimGray;
      this.btnEight.Checked = false;
      this.btnEight.Curve = 4;
      this.btnEight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnEight.Location = new System.Drawing.Point(54, 2);
      this.btnEight.Name = "btnEight";
      this.btnEight.Size = new System.Drawing.Size(49, 48);
      this.btnEight.TabIndex = 7;
      this.btnEight.TabStop = false;
      this.btnEight.Text = "8";
      this.btnEight.UseVisualStyleBackColor = false;
      this.btnEight.Click += new System.EventHandler(this.number_Click);
      this.btnEight.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmCalculator_KeyDown);
      // 
      // btnZero
      // 
      this.btnZero.BackColor = System.Drawing.Color.DimGray;
      this.btnZero.Checked = false;
      this.btnZero.Curve = 4;
      this.btnZero.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnZero.Location = new System.Drawing.Point(54, 159);
      this.btnZero.Name = "btnZero";
      this.btnZero.Size = new System.Drawing.Size(49, 48);
      this.btnZero.TabIndex = 16;
      this.btnZero.TabStop = false;
      this.btnZero.Text = "0";
      this.btnZero.UseVisualStyleBackColor = false;
      this.btnZero.Click += new System.EventHandler(this.number_Click);
      this.btnZero.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmCalculator_KeyDown);
      // 
      // btnFour
      // 
      this.btnFour.BackColor = System.Drawing.Color.DimGray;
      this.btnFour.Checked = false;
      this.btnFour.Curve = 4;
      this.btnFour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnFour.Location = new System.Drawing.Point(2, 54);
      this.btnFour.Name = "btnFour";
      this.btnFour.Size = new System.Drawing.Size(49, 48);
      this.btnFour.TabIndex = 8;
      this.btnFour.TabStop = false;
      this.btnFour.Text = "4";
      this.btnFour.UseVisualStyleBackColor = false;
      this.btnFour.Click += new System.EventHandler(this.number_Click);
      this.btnFour.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmCalculator_KeyDown);
      // 
      // btnTwo
      // 
      this.btnTwo.BackColor = System.Drawing.Color.DimGray;
      this.btnTwo.Checked = false;
      this.btnTwo.Curve = 4;
      this.btnTwo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnTwo.Location = new System.Drawing.Point(54, 107);
      this.btnTwo.Name = "btnTwo";
      this.btnTwo.Size = new System.Drawing.Size(49, 48);
      this.btnTwo.TabIndex = 15;
      this.btnTwo.TabStop = false;
      this.btnTwo.Text = "2";
      this.btnTwo.UseVisualStyleBackColor = false;
      this.btnTwo.Click += new System.EventHandler(this.number_Click);
      this.btnTwo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmCalculator_KeyDown);
      // 
      // btnSix
      // 
      this.btnSix.BackColor = System.Drawing.Color.DimGray;
      this.btnSix.Checked = false;
      this.btnSix.Curve = 4;
      this.btnSix.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnSix.Location = new System.Drawing.Point(106, 54);
      this.btnSix.Name = "btnSix";
      this.btnSix.Size = new System.Drawing.Size(49, 48);
      this.btnSix.TabIndex = 10;
      this.btnSix.TabStop = false;
      this.btnSix.Text = "6";
      this.btnSix.UseVisualStyleBackColor = false;
      this.btnSix.Click += new System.EventHandler(this.number_Click);
      this.btnSix.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmCalculator_KeyDown);
      // 
      // btnThree
      // 
      this.btnThree.BackColor = System.Drawing.Color.DimGray;
      this.btnThree.Checked = false;
      this.btnThree.Curve = 4;
      this.btnThree.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnThree.Location = new System.Drawing.Point(106, 107);
      this.btnThree.Name = "btnThree";
      this.btnThree.Size = new System.Drawing.Size(49, 48);
      this.btnThree.TabIndex = 14;
      this.btnThree.TabStop = false;
      this.btnThree.Text = "3";
      this.btnThree.UseVisualStyleBackColor = false;
      this.btnThree.Click += new System.EventHandler(this.number_Click);
      this.btnThree.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmCalculator_KeyDown);
      // 
      // btnFive
      // 
      this.btnFive.BackColor = System.Drawing.Color.DimGray;
      this.btnFive.Checked = false;
      this.btnFive.Curve = 4;
      this.btnFive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnFive.Location = new System.Drawing.Point(54, 54);
      this.btnFive.Name = "btnFive";
      this.btnFive.Size = new System.Drawing.Size(49, 48);
      this.btnFive.TabIndex = 11;
      this.btnFive.TabStop = false;
      this.btnFive.Text = "5";
      this.btnFive.UseVisualStyleBackColor = false;
      this.btnFive.Click += new System.EventHandler(this.number_Click);
      this.btnFive.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmCalculator_KeyDown);
      // 
      // btnOne
      // 
      this.btnOne.BackColor = System.Drawing.Color.DimGray;
      this.btnOne.Checked = false;
      this.btnOne.Curve = 4;
      this.btnOne.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnOne.Location = new System.Drawing.Point(2, 107);
      this.btnOne.Name = "btnOne";
      this.btnOne.Size = new System.Drawing.Size(49, 48);
      this.btnOne.TabIndex = 12;
      this.btnOne.TabStop = false;
      this.btnOne.Text = "1";
      this.btnOne.UseVisualStyleBackColor = false;
      this.btnOne.Click += new System.EventHandler(this.number_Click);
      this.btnOne.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmCalculator_KeyDown);
      // 
      // plAlpha
      // 
      this.plAlpha.BackColor = System.Drawing.Color.Transparent;
      this.plAlpha.Controls.Add(this.btnCaps);
      this.plAlpha.Controls.Add(this.btnZ);
      this.plAlpha.Controls.Add(this.btnX);
      this.plAlpha.Controls.Add(this.btnY);
      this.plAlpha.Controls.Add(this.btnW);
      this.plAlpha.Controls.Add(this.btnV);
      this.plAlpha.Controls.Add(this.btnU);
      this.plAlpha.Controls.Add(this.btnT);
      this.plAlpha.Controls.Add(this.btnS);
      this.plAlpha.Controls.Add(this.btnQ);
      this.plAlpha.Controls.Add(this.btnR);
      this.plAlpha.Controls.Add(this.btnP);
      this.plAlpha.Controls.Add(this.btnO);
      this.plAlpha.Controls.Add(this.btnN);
      this.plAlpha.Controls.Add(this.btnM);
      this.plAlpha.Controls.Add(this.btnL);
      this.plAlpha.Controls.Add(this.btnJ);
      this.plAlpha.Controls.Add(this.btnK);
      this.plAlpha.Controls.Add(this.btnI);
      this.plAlpha.Controls.Add(this.btnH);
      this.plAlpha.Controls.Add(this.btnG);
      this.plAlpha.Controls.Add(this.btnF);
      this.plAlpha.Controls.Add(this.btnE);
      this.plAlpha.Controls.Add(this.btnC);
      this.plAlpha.Controls.Add(this.btnD);
      this.plAlpha.Controls.Add(this.btnB);
      this.plAlpha.Controls.Add(this.btnA);
      this.plAlpha.Location = new System.Drawing.Point(285, 9);
      this.plAlpha.Name = "plAlpha";
      this.plAlpha.Size = new System.Drawing.Size(370, 215);
      this.plAlpha.TabIndex = 26;
      this.plAlpha.Visible = false;
      // 
      // btnCaps
      // 
      this.btnCaps.BackColor = System.Drawing.Color.DarkGray;
      this.btnCaps.Checked = false;
      this.btnCaps.Curve = 4;
      this.btnCaps.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnCaps.Location = new System.Drawing.Point(262, 159);
      this.btnCaps.Name = "btnCaps";
      this.btnCaps.Size = new System.Drawing.Size(100, 48);
      this.btnCaps.TabIndex = 27;
      this.btnCaps.TabStop = false;
      this.btnCaps.Text = "大小写";
      this.btnCaps.UseVisualStyleBackColor = false;
      this.btnCaps.Click += new System.EventHandler(this.btnCaps_Click);
      // 
      // btnZ
      // 
      this.btnZ.BackColor = System.Drawing.Color.DarkGray;
      this.btnZ.Checked = false;
      this.btnZ.Curve = 4;
      this.btnZ.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnZ.Location = new System.Drawing.Point(210, 159);
      this.btnZ.Name = "btnZ";
      this.btnZ.Size = new System.Drawing.Size(49, 48);
      this.btnZ.TabIndex = 26;
      this.btnZ.TabStop = false;
      this.btnZ.Text = "Z";
      this.btnZ.UseVisualStyleBackColor = false;
      this.btnZ.Click += new System.EventHandler(this.letters_Click);
      // 
      // btnX
      // 
      this.btnX.BackColor = System.Drawing.Color.DarkGray;
      this.btnX.Checked = false;
      this.btnX.Curve = 4;
      this.btnX.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnX.Location = new System.Drawing.Point(106, 159);
      this.btnX.Name = "btnX";
      this.btnX.Size = new System.Drawing.Size(49, 48);
      this.btnX.TabIndex = 28;
      this.btnX.TabStop = false;
      this.btnX.Text = "X";
      this.btnX.UseVisualStyleBackColor = false;
      this.btnX.Click += new System.EventHandler(this.letters_Click);
      // 
      // btnY
      // 
      this.btnY.BackColor = System.Drawing.Color.DarkGray;
      this.btnY.Checked = false;
      this.btnY.Curve = 4;
      this.btnY.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnY.Location = new System.Drawing.Point(158, 159);
      this.btnY.Name = "btnY";
      this.btnY.Size = new System.Drawing.Size(49, 48);
      this.btnY.TabIndex = 23;
      this.btnY.TabStop = false;
      this.btnY.Text = "Y";
      this.btnY.UseVisualStyleBackColor = false;
      this.btnY.Click += new System.EventHandler(this.letters_Click);
      // 
      // btnW
      // 
      this.btnW.BackColor = System.Drawing.Color.DarkGray;
      this.btnW.Checked = false;
      this.btnW.Curve = 4;
      this.btnW.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnW.Location = new System.Drawing.Point(54, 159);
      this.btnW.Name = "btnW";
      this.btnW.Size = new System.Drawing.Size(49, 48);
      this.btnW.TabIndex = 25;
      this.btnW.TabStop = false;
      this.btnW.Text = "W";
      this.btnW.UseVisualStyleBackColor = false;
      this.btnW.Click += new System.EventHandler(this.letters_Click);
      // 
      // btnV
      // 
      this.btnV.BackColor = System.Drawing.Color.DarkGray;
      this.btnV.Checked = false;
      this.btnV.Curve = 4;
      this.btnV.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnV.Location = new System.Drawing.Point(2, 159);
      this.btnV.Name = "btnV";
      this.btnV.Size = new System.Drawing.Size(49, 48);
      this.btnV.TabIndex = 24;
      this.btnV.TabStop = false;
      this.btnV.Text = "V";
      this.btnV.UseVisualStyleBackColor = false;
      this.btnV.Click += new System.EventHandler(this.letters_Click);
      // 
      // btnU
      // 
      this.btnU.BackColor = System.Drawing.Color.DarkGray;
      this.btnU.Checked = false;
      this.btnU.Curve = 4;
      this.btnU.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnU.Location = new System.Drawing.Point(314, 107);
      this.btnU.Name = "btnU";
      this.btnU.Size = new System.Drawing.Size(49, 48);
      this.btnU.TabIndex = 22;
      this.btnU.TabStop = false;
      this.btnU.Text = "U";
      this.btnU.UseVisualStyleBackColor = false;
      this.btnU.Click += new System.EventHandler(this.letters_Click);
      // 
      // btnT
      // 
      this.btnT.BackColor = System.Drawing.Color.DarkGray;
      this.btnT.Checked = false;
      this.btnT.Curve = 4;
      this.btnT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnT.Location = new System.Drawing.Point(262, 107);
      this.btnT.Name = "btnT";
      this.btnT.Size = new System.Drawing.Size(49, 48);
      this.btnT.TabIndex = 20;
      this.btnT.TabStop = false;
      this.btnT.Text = "T";
      this.btnT.UseVisualStyleBackColor = false;
      this.btnT.Click += new System.EventHandler(this.letters_Click);
      // 
      // btnS
      // 
      this.btnS.BackColor = System.Drawing.Color.DarkGray;
      this.btnS.Checked = false;
      this.btnS.Curve = 4;
      this.btnS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnS.Location = new System.Drawing.Point(210, 107);
      this.btnS.Name = "btnS";
      this.btnS.Size = new System.Drawing.Size(49, 48);
      this.btnS.TabIndex = 19;
      this.btnS.TabStop = false;
      this.btnS.Text = "S";
      this.btnS.UseVisualStyleBackColor = false;
      this.btnS.Click += new System.EventHandler(this.letters_Click);
      // 
      // btnQ
      // 
      this.btnQ.BackColor = System.Drawing.Color.DarkGray;
      this.btnQ.Checked = false;
      this.btnQ.Curve = 4;
      this.btnQ.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnQ.Location = new System.Drawing.Point(106, 107);
      this.btnQ.Name = "btnQ";
      this.btnQ.Size = new System.Drawing.Size(49, 48);
      this.btnQ.TabIndex = 21;
      this.btnQ.TabStop = false;
      this.btnQ.Text = "Q";
      this.btnQ.UseVisualStyleBackColor = false;
      this.btnQ.Click += new System.EventHandler(this.letters_Click);
      // 
      // btnR
      // 
      this.btnR.BackColor = System.Drawing.Color.DarkGray;
      this.btnR.Checked = false;
      this.btnR.Curve = 4;
      this.btnR.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnR.Location = new System.Drawing.Point(158, 107);
      this.btnR.Name = "btnR";
      this.btnR.Size = new System.Drawing.Size(49, 48);
      this.btnR.TabIndex = 16;
      this.btnR.TabStop = false;
      this.btnR.Text = "R";
      this.btnR.UseVisualStyleBackColor = false;
      this.btnR.Click += new System.EventHandler(this.letters_Click);
      // 
      // btnP
      // 
      this.btnP.BackColor = System.Drawing.Color.DarkGray;
      this.btnP.Checked = false;
      this.btnP.Curve = 4;
      this.btnP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnP.Location = new System.Drawing.Point(54, 107);
      this.btnP.Name = "btnP";
      this.btnP.Size = new System.Drawing.Size(49, 48);
      this.btnP.TabIndex = 18;
      this.btnP.TabStop = false;
      this.btnP.Text = "P";
      this.btnP.UseVisualStyleBackColor = false;
      this.btnP.Click += new System.EventHandler(this.letters_Click);
      // 
      // btnO
      // 
      this.btnO.BackColor = System.Drawing.Color.DarkGray;
      this.btnO.Checked = false;
      this.btnO.Curve = 4;
      this.btnO.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnO.Location = new System.Drawing.Point(2, 107);
      this.btnO.Name = "btnO";
      this.btnO.Size = new System.Drawing.Size(49, 48);
      this.btnO.TabIndex = 17;
      this.btnO.TabStop = false;
      this.btnO.Text = "O";
      this.btnO.UseVisualStyleBackColor = false;
      this.btnO.Click += new System.EventHandler(this.letters_Click);
      // 
      // btnN
      // 
      this.btnN.BackColor = System.Drawing.Color.DarkGray;
      this.btnN.Checked = false;
      this.btnN.Curve = 4;
      this.btnN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnN.Location = new System.Drawing.Point(314, 54);
      this.btnN.Name = "btnN";
      this.btnN.Size = new System.Drawing.Size(49, 48);
      this.btnN.TabIndex = 15;
      this.btnN.TabStop = false;
      this.btnN.Text = "N";
      this.btnN.UseVisualStyleBackColor = false;
      this.btnN.Click += new System.EventHandler(this.letters_Click);
      // 
      // btnM
      // 
      this.btnM.BackColor = System.Drawing.Color.DarkGray;
      this.btnM.Checked = false;
      this.btnM.Curve = 4;
      this.btnM.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnM.Location = new System.Drawing.Point(262, 54);
      this.btnM.Name = "btnM";
      this.btnM.Size = new System.Drawing.Size(49, 48);
      this.btnM.TabIndex = 13;
      this.btnM.TabStop = false;
      this.btnM.Text = "M";
      this.btnM.UseVisualStyleBackColor = false;
      this.btnM.Click += new System.EventHandler(this.letters_Click);
      // 
      // btnL
      // 
      this.btnL.BackColor = System.Drawing.Color.DarkGray;
      this.btnL.Checked = false;
      this.btnL.Curve = 4;
      this.btnL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnL.Location = new System.Drawing.Point(210, 54);
      this.btnL.Name = "btnL";
      this.btnL.Size = new System.Drawing.Size(49, 48);
      this.btnL.TabIndex = 12;
      this.btnL.TabStop = false;
      this.btnL.Text = "L";
      this.btnL.UseVisualStyleBackColor = false;
      this.btnL.Click += new System.EventHandler(this.letters_Click);
      // 
      // btnJ
      // 
      this.btnJ.BackColor = System.Drawing.Color.DarkGray;
      this.btnJ.Checked = false;
      this.btnJ.Curve = 4;
      this.btnJ.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnJ.Location = new System.Drawing.Point(106, 54);
      this.btnJ.Name = "btnJ";
      this.btnJ.Size = new System.Drawing.Size(49, 48);
      this.btnJ.TabIndex = 14;
      this.btnJ.TabStop = false;
      this.btnJ.Text = "J";
      this.btnJ.UseVisualStyleBackColor = false;
      this.btnJ.Click += new System.EventHandler(this.letters_Click);
      // 
      // btnK
      // 
      this.btnK.BackColor = System.Drawing.Color.DarkGray;
      this.btnK.Checked = false;
      this.btnK.Curve = 4;
      this.btnK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnK.Location = new System.Drawing.Point(158, 54);
      this.btnK.Name = "btnK";
      this.btnK.Size = new System.Drawing.Size(49, 48);
      this.btnK.TabIndex = 9;
      this.btnK.TabStop = false;
      this.btnK.Text = "K";
      this.btnK.UseVisualStyleBackColor = false;
      this.btnK.Click += new System.EventHandler(this.letters_Click);
      // 
      // btnI
      // 
      this.btnI.BackColor = System.Drawing.Color.DarkGray;
      this.btnI.Checked = false;
      this.btnI.Curve = 4;
      this.btnI.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnI.Location = new System.Drawing.Point(54, 54);
      this.btnI.Name = "btnI";
      this.btnI.Size = new System.Drawing.Size(49, 48);
      this.btnI.TabIndex = 11;
      this.btnI.TabStop = false;
      this.btnI.Text = "I";
      this.btnI.UseVisualStyleBackColor = false;
      this.btnI.Click += new System.EventHandler(this.letters_Click);
      // 
      // btnH
      // 
      this.btnH.BackColor = System.Drawing.Color.DarkGray;
      this.btnH.Checked = false;
      this.btnH.Curve = 4;
      this.btnH.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnH.Location = new System.Drawing.Point(2, 54);
      this.btnH.Name = "btnH";
      this.btnH.Size = new System.Drawing.Size(49, 48);
      this.btnH.TabIndex = 10;
      this.btnH.TabStop = false;
      this.btnH.Text = "H";
      this.btnH.UseVisualStyleBackColor = false;
      this.btnH.Click += new System.EventHandler(this.letters_Click);
      // 
      // btnG
      // 
      this.btnG.BackColor = System.Drawing.Color.DarkGray;
      this.btnG.Checked = false;
      this.btnG.Curve = 4;
      this.btnG.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnG.Location = new System.Drawing.Point(314, 2);
      this.btnG.Name = "btnG";
      this.btnG.Size = new System.Drawing.Size(49, 48);
      this.btnG.TabIndex = 8;
      this.btnG.TabStop = false;
      this.btnG.Text = "G";
      this.btnG.UseVisualStyleBackColor = false;
      this.btnG.Click += new System.EventHandler(this.letters_Click);
      // 
      // btnF
      // 
      this.btnF.BackColor = System.Drawing.Color.DarkGray;
      this.btnF.Checked = false;
      this.btnF.Curve = 4;
      this.btnF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnF.Location = new System.Drawing.Point(262, 2);
      this.btnF.Name = "btnF";
      this.btnF.Size = new System.Drawing.Size(49, 48);
      this.btnF.TabIndex = 7;
      this.btnF.TabStop = false;
      this.btnF.Text = "F";
      this.btnF.UseVisualStyleBackColor = false;
      this.btnF.Click += new System.EventHandler(this.letters_Click);
      // 
      // btnE
      // 
      this.btnE.BackColor = System.Drawing.Color.DarkGray;
      this.btnE.Checked = false;
      this.btnE.Curve = 4;
      this.btnE.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnE.Location = new System.Drawing.Point(210, 2);
      this.btnE.Name = "btnE";
      this.btnE.Size = new System.Drawing.Size(49, 48);
      this.btnE.TabIndex = 6;
      this.btnE.TabStop = false;
      this.btnE.Text = "E";
      this.btnE.UseVisualStyleBackColor = false;
      this.btnE.Click += new System.EventHandler(this.letters_Click);
      // 
      // btnC
      // 
      this.btnC.BackColor = System.Drawing.Color.DarkGray;
      this.btnC.Checked = false;
      this.btnC.Curve = 4;
      this.btnC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnC.Location = new System.Drawing.Point(106, 2);
      this.btnC.Name = "btnC";
      this.btnC.Size = new System.Drawing.Size(49, 48);
      this.btnC.TabIndex = 7;
      this.btnC.TabStop = false;
      this.btnC.Text = "C";
      this.btnC.UseVisualStyleBackColor = false;
      this.btnC.Click += new System.EventHandler(this.letters_Click);
      // 
      // btnD
      // 
      this.btnD.BackColor = System.Drawing.Color.DarkGray;
      this.btnD.Checked = false;
      this.btnD.Curve = 4;
      this.btnD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnD.Location = new System.Drawing.Point(158, 2);
      this.btnD.Name = "btnD";
      this.btnD.Size = new System.Drawing.Size(49, 48);
      this.btnD.TabIndex = 5;
      this.btnD.TabStop = false;
      this.btnD.Text = "D";
      this.btnD.UseVisualStyleBackColor = false;
      this.btnD.Click += new System.EventHandler(this.letters_Click);
      // 
      // btnB
      // 
      this.btnB.BackColor = System.Drawing.Color.DarkGray;
      this.btnB.Checked = false;
      this.btnB.Curve = 4;
      this.btnB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnB.Location = new System.Drawing.Point(54, 2);
      this.btnB.Name = "btnB";
      this.btnB.Size = new System.Drawing.Size(49, 48);
      this.btnB.TabIndex = 6;
      this.btnB.TabStop = false;
      this.btnB.Text = "B";
      this.btnB.UseVisualStyleBackColor = false;
      this.btnB.Click += new System.EventHandler(this.letters_Click);
      // 
      // btnA
      // 
      this.btnA.BackColor = System.Drawing.Color.DarkGray;
      this.btnA.Checked = false;
      this.btnA.Curve = 4;
      this.btnA.Location = new System.Drawing.Point(2, 2);
      this.btnA.Name = "btnA";
      this.btnA.Size = new System.Drawing.Size(49, 48);
      this.btnA.TabIndex = 5;
      this.btnA.TabStop = false;
      this.btnA.Text = "A";
      this.btnA.UseVisualStyleBackColor = false;
      this.btnA.Click += new System.EventHandler(this.letters_Click);
      // 
      // SoftKeyboard
      // 
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
      this.BackColor = System.Drawing.Color.DimGray;
      this.ClientSize = new System.Drawing.Size(657, 225);
      this.ControlBox = false;
      this.Controls.Add(this.plAlpha);
      this.Controls.Add(this.plControl);
      this.Controls.Add(this.plDigits);
      this.DoubleBuffered = true;
      this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.KeyPreview = true;
      this.Name = "SoftKeyboard";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
      this.TopMost = true;
      this.Load += new System.EventHandler(this.frmCalculator_Load);
      this.VisibleChanged += new System.EventHandler(this.SoftKeyboard_VisibleChanged);
      this.Leave += new System.EventHandler(this.CheckToHide);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmCalculator_KeyDown);
      this.plControl.ResumeLayout(false);
      this.plDigits.ResumeLayout(false);
      this.plAlpha.ResumeLayout(false);
      this.ResumeLayout(false);

		}
		#endregion
		
		protected override void OnResize(EventArgs e)
		{
			base.OnResize (e);
		}
		private void CheckToHide(object sender, EventArgs e)
		{
      this.Visible = false;
		}

		private void frmCalculator_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyData==Keys.Escape)
			{
        this.Visible = false;
        e.Handled = true;
        if (Canceled != null)
          Canceled(null, null);
        return;
			}
      if ((e.KeyData >= Keys.D0 &&
        e.KeyData <= Keys.D9) )
      {
        OnNumberKeyPressed((char)('0' + (e.KeyData - Keys.D0)));
      }
      if (e.KeyData >= Keys.NumPad0 &&
        e.KeyData <= Keys.NumPad9)
      {
        OnNumberKeyPressed((char)('0' + (e.KeyData - Keys.NumPad0)));
      }
			switch(e.KeyData)
			{
				case Keys.Decimal:
					OnNumberKeyPressed(Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
					break;
				case Keys.Enter:
					btnEqual_Click(this,new EventArgs());
					break;
				case Keys.Back:
					OnNumberKeyPressed(Convert.ToChar(8));
					break;
        default:
          return;
			}
		}

		private void OnNumberKeyPressed(char key)
		{
			if(NumberKeyPressed!=null)
				NumberKeyPressed(this,new KeyPressEventArgs(key));
		}

		private void OperationKeyPressed(string key)
		{
			PerformOperation();
		}
		private void PerformOperation()
		{
      if (Finished != null)
        Finished(null, null);
		}

		private void btnDot_Click(object sender, System.EventArgs e)
		{
			OnNumberKeyPressed(Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
		}

		private void btnClear_Click(object sender, System.EventArgs e)
		{
      this.Text = "";
      if( ValueCleared != null )
        ValueCleared(null, this.Text);
		}
		public void Show(string clickedOperator)
		{
      this.Show();
      btnExpand_Click(null, null);
		}

    private void number_Click(object sender, EventArgs e)
    {
      if (!(sender is Button))
        return;
      Button btn = (Button)sender;
      int num = 0;
      if (!int.TryParse(btn.Text, out num))
        return;
      OnNumberKeyPressed((char)('0' + num));
    }
    private void letters_Click(object sender, EventArgs e)
    {
      if (!(sender is Button))
        return;
      Button btn = (Button)sender;
      char num;
      if (!char.TryParse(btn.Text, out num))
        return;
      OnNumberKeyPressed(num);
    }
		private void btnBack_Click(object sender, System.EventArgs e)
		{
			if(NumberKeyPressed!=null)
				NumberKeyPressed(this,new KeyPressEventArgs(Convert.ToChar(8)));
		}

		private void btnEqual_Click(object sender, System.EventArgs e)
		{
			PerformOperation();
      this.Visible = false;
		}
		//private double calcValue=0;

		private void frmCalculator_Load(object sender, System.EventArgs e)
		{
      UpdateStatus();
		}

// 		public double Value
// 		{
// 			get
// 			{
// 				return calcValue;
// 			}
// 			set
// 			{
// 				if(calcValue!=value&&ValueChanged!=null)
// 					ValueChanged(this,value);
// 				calcValue=value;
// 			}
// 		}
    private bool HasLetters { get { return plAlpha.Visible; } }
    private void UpdateStatus()
    {
      btnExpand.Text = (HasLetters) ? "<<" : ">>";
      if (!HasLetters)
      {
        this.Width = (plDigits.Width + plControl.Width)+15;
        //plControl.Location = new Point(plDigits.Right, plControl.Top);
      }
      else
      {
        this.Width = (plDigits.Width + plControl.Width + plAlpha.Width)+15;
        //plControl.Location = new Point(plAlpha.Right, plControl.Top);
      }
    }
    private void btnExpand_Click(object sender, EventArgs e)
    {
      plAlpha.Visible = !HasLetters;
      UpdateStatus();
      Refresh();
    }
    protected override void OnPaintBackground(PaintEventArgs e)
    {
      //base.OnPaintBackground(e);
      Rectangle rc = this.ClientRectangle;
      e.Graphics.FillRectangle(Brushes.DimGray, rc);
      e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
      GraphicsPath gp = new GraphicsPath();
      gp.AddArc(rc.Left-rc.Width/2, rc.Top - rc.Height, rc.Width* 2, rc.Width* 2, 0, 360);
      Point pt = new Point(rc.Right, rc.Top);
      using(PathGradientBrush br = new PathGradientBrush(gp))
      {
        br.CenterPoint = pt;
        br.CenterColor = Color.WhiteSmoke;
        br.SurroundColors = new Color[] {Color.Black};
        e.Graphics.FillRectangle(br, rc);
      }
    }
    protected override void  OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);

      Rectangle rc = this.ClientRectangle;
      using(Pen p = new Pen(Color.Black, 4))
      {
        e.Graphics.DrawRectangle(p, rc);
      }
    }

    private void btnCaps_Click(object sender, EventArgs e)
    {
      bool lower = char.IsLower(btnA.Text[0]);
      for (int i = 0; i < alpha.Length; i++ )
      {
        if( lower )
        {
          alpha[i].Text = char.ToUpper(alpha[i].Text[0]).ToString();
        }
        else
        {
          alpha[i].Text = char.ToLower(alpha[i].Text[0]).ToString();
        }
      }
    }
    private bool clear_on_active = true;
    public bool ClearOnActive { get { return clear_on_active; } set { clear_on_active = value; } }
    private void SoftKeyboard_VisibleChanged(object sender, EventArgs e)
    {
      if (this.Visible && clear_on_active)
        this.btnClear_Click(null, null);
    }
    

    
	}
}
