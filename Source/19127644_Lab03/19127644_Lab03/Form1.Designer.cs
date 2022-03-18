
namespace _19127644_Lab03
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.openGLControl1 = new SharpGL.OpenGLControl();
            this.LineBtn = new System.Windows.Forms.Button();
            this.CircleBtn = new System.Windows.Forms.Button();
            this.RectangleBtn = new System.Windows.Forms.Button();
            this.EllipseBtn = new System.Windows.Forms.Button();
            this.TriangleBtn = new System.Windows.Forms.Button();
            this.PentagonBtn = new System.Windows.Forms.Button();
            this.HexagonBtn = new System.Windows.Forms.Button();
            this.PolygonBtn = new System.Windows.Forms.Button();
            this.ScanlineBtn = new System.Windows.Forms.Button();
            this.FloodFillBtn = new System.Windows.Forms.Button();
            this.ColorBtn = new System.Windows.Forms.Button();
            this.WidthBtn = new System.Windows.Forms.Button();
            this.ClearBtn = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.RotateBtn = new System.Windows.Forms.Button();
            this.LbMode = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ZoomBtn = new System.Windows.Forms.Button();
            this.LbTime = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // openGLControl1
            // 
            this.openGLControl1.DrawFPS = false;
            this.openGLControl1.Location = new System.Drawing.Point(2, 3);
            this.openGLControl1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.openGLControl1.Name = "openGLControl1";
            this.openGLControl1.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.openGLControl1.RenderContextType = SharpGL.RenderContextType.DIBSection;
            this.openGLControl1.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.openGLControl1.Size = new System.Drawing.Size(810, 810);
            this.openGLControl1.TabIndex = 0;
            this.openGLControl1.OpenGLInitialized += new System.EventHandler(this.openGLControl1_OpenGLInitialized);
            this.openGLControl1.OpenGLDraw += new SharpGL.RenderEventHandler(this.openGLControl1_OpenGLDraw);
            this.openGLControl1.Resized += new System.EventHandler(this.openGLControl1_Resized);
            this.openGLControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.openGLControl1_KeyDown);
            this.openGLControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.openGLControl1_MouseDown);
            this.openGLControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.openGLControl1_MouseMove);
            this.openGLControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.openGLControl1_MouseUp);
            // 
            // LineBtn
            // 
            this.LineBtn.Location = new System.Drawing.Point(898, 120);
            this.LineBtn.Name = "LineBtn";
            this.LineBtn.Size = new System.Drawing.Size(221, 54);
            this.LineBtn.TabIndex = 1;
            this.LineBtn.Text = "Line Segment";
            this.LineBtn.UseVisualStyleBackColor = true;
            this.LineBtn.Click += new System.EventHandler(this.LineBtn_Click);
            // 
            // CircleBtn
            // 
            this.CircleBtn.Location = new System.Drawing.Point(898, 180);
            this.CircleBtn.Name = "CircleBtn";
            this.CircleBtn.Size = new System.Drawing.Size(221, 54);
            this.CircleBtn.TabIndex = 2;
            this.CircleBtn.Text = "Circle";
            this.CircleBtn.UseVisualStyleBackColor = true;
            this.CircleBtn.Click += new System.EventHandler(this.CircleBtn_Click);
            // 
            // RectangleBtn
            // 
            this.RectangleBtn.Location = new System.Drawing.Point(898, 240);
            this.RectangleBtn.Name = "RectangleBtn";
            this.RectangleBtn.Size = new System.Drawing.Size(221, 54);
            this.RectangleBtn.TabIndex = 3;
            this.RectangleBtn.Text = "Rectangle";
            this.RectangleBtn.UseVisualStyleBackColor = true;
            this.RectangleBtn.Click += new System.EventHandler(this.RectangleBtn_Click);
            // 
            // EllipseBtn
            // 
            this.EllipseBtn.Location = new System.Drawing.Point(898, 300);
            this.EllipseBtn.Name = "EllipseBtn";
            this.EllipseBtn.Size = new System.Drawing.Size(221, 54);
            this.EllipseBtn.TabIndex = 4;
            this.EllipseBtn.Text = "Ellipse";
            this.EllipseBtn.UseVisualStyleBackColor = true;
            this.EllipseBtn.Click += new System.EventHandler(this.EllipseBtn_Click);
            // 
            // TriangleBtn
            // 
            this.TriangleBtn.Location = new System.Drawing.Point(1216, 120);
            this.TriangleBtn.Name = "TriangleBtn";
            this.TriangleBtn.Size = new System.Drawing.Size(221, 54);
            this.TriangleBtn.TabIndex = 5;
            this.TriangleBtn.Text = "Triangle";
            this.TriangleBtn.UseVisualStyleBackColor = true;
            this.TriangleBtn.Click += new System.EventHandler(this.TriangleBtn_Click);
            // 
            // PentagonBtn
            // 
            this.PentagonBtn.Location = new System.Drawing.Point(1216, 180);
            this.PentagonBtn.Name = "PentagonBtn";
            this.PentagonBtn.Size = new System.Drawing.Size(221, 54);
            this.PentagonBtn.TabIndex = 6;
            this.PentagonBtn.Text = "Pentagon";
            this.PentagonBtn.UseVisualStyleBackColor = true;
            this.PentagonBtn.Click += new System.EventHandler(this.PentagonBtn_Click);
            // 
            // HexagonBtn
            // 
            this.HexagonBtn.Location = new System.Drawing.Point(1216, 240);
            this.HexagonBtn.Name = "HexagonBtn";
            this.HexagonBtn.Size = new System.Drawing.Size(221, 54);
            this.HexagonBtn.TabIndex = 7;
            this.HexagonBtn.Text = "Hexagon";
            this.HexagonBtn.UseVisualStyleBackColor = true;
            this.HexagonBtn.Click += new System.EventHandler(this.HexagonBtn_Click);
            // 
            // PolygonBtn
            // 
            this.PolygonBtn.Location = new System.Drawing.Point(1216, 300);
            this.PolygonBtn.Name = "PolygonBtn";
            this.PolygonBtn.Size = new System.Drawing.Size(221, 54);
            this.PolygonBtn.TabIndex = 8;
            this.PolygonBtn.Text = "Polygon";
            this.PolygonBtn.UseVisualStyleBackColor = true;
            this.PolygonBtn.Click += new System.EventHandler(this.PolygonBtn_Click);
            // 
            // ScanlineBtn
            // 
            this.ScanlineBtn.Location = new System.Drawing.Point(898, 413);
            this.ScanlineBtn.Name = "ScanlineBtn";
            this.ScanlineBtn.Size = new System.Drawing.Size(221, 54);
            this.ScanlineBtn.TabIndex = 9;
            this.ScanlineBtn.Text = "Scanline";
            this.ScanlineBtn.UseVisualStyleBackColor = true;
            this.ScanlineBtn.Click += new System.EventHandler(this.ScanlineBtn_Click);
            // 
            // FloodFillBtn
            // 
            this.FloodFillBtn.Location = new System.Drawing.Point(1216, 413);
            this.FloodFillBtn.Name = "FloodFillBtn";
            this.FloodFillBtn.Size = new System.Drawing.Size(221, 54);
            this.FloodFillBtn.TabIndex = 10;
            this.FloodFillBtn.Text = "Flood Fill";
            this.FloodFillBtn.UseVisualStyleBackColor = true;
            this.FloodFillBtn.Click += new System.EventHandler(this.FloodFillBtn_Click);
            // 
            // ColorBtn
            // 
            this.ColorBtn.Location = new System.Drawing.Point(898, 519);
            this.ColorBtn.Name = "ColorBtn";
            this.ColorBtn.Size = new System.Drawing.Size(221, 54);
            this.ColorBtn.TabIndex = 11;
            this.ColorBtn.Text = "Change Color";
            this.ColorBtn.UseVisualStyleBackColor = true;
            this.ColorBtn.Click += new System.EventHandler(this.ColorBtn_Click);
            // 
            // WidthBtn
            // 
            this.WidthBtn.Location = new System.Drawing.Point(1216, 519);
            this.WidthBtn.Name = "WidthBtn";
            this.WidthBtn.Size = new System.Drawing.Size(221, 54);
            this.WidthBtn.TabIndex = 12;
            this.WidthBtn.Text = "Width:";
            this.WidthBtn.UseVisualStyleBackColor = true;
            this.WidthBtn.Click += new System.EventHandler(this.WidthBtn_Click);
            // 
            // ClearBtn
            // 
            this.ClearBtn.Location = new System.Drawing.Point(898, 688);
            this.ClearBtn.Name = "ClearBtn";
            this.ClearBtn.Size = new System.Drawing.Size(539, 54);
            this.ClearBtn.TabIndex = 13;
            this.ClearBtn.Text = "Clear";
            this.ClearBtn.UseVisualStyleBackColor = true;
            this.ClearBtn.Click += new System.EventHandler(this.ClearBtn_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // RotateBtn
            // 
            this.RotateBtn.Location = new System.Drawing.Point(898, 602);
            this.RotateBtn.Name = "RotateBtn";
            this.RotateBtn.Size = new System.Drawing.Size(221, 54);
            this.RotateBtn.TabIndex = 14;
            this.RotateBtn.Text = "Rotate";
            this.RotateBtn.UseVisualStyleBackColor = true;
            this.RotateBtn.Click += new System.EventHandler(this.RotateBtn_Click);
            // 
            // LbMode
            // 
            this.LbMode.AutoSize = true;
            this.LbMode.Location = new System.Drawing.Point(893, 32);
            this.LbMode.Name = "LbMode";
            this.LbMode.Size = new System.Drawing.Size(72, 25);
            this.LbMode.TabIndex = 15;
            this.LbMode.Text = "Mode:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(893, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(272, 25);
            this.label1.TabIndex = 16;
            this.label1.Text = "Undo: Ctrl+Z | Cancel: ESC";
            // 
            // ZoomBtn
            // 
            this.ZoomBtn.Location = new System.Drawing.Point(1216, 602);
            this.ZoomBtn.Name = "ZoomBtn";
            this.ZoomBtn.Size = new System.Drawing.Size(221, 54);
            this.ZoomBtn.TabIndex = 17;
            this.ZoomBtn.Text = "Zoom";
            this.ZoomBtn.UseVisualStyleBackColor = true;
            this.ZoomBtn.Click += new System.EventHandler(this.ZoomBtn_Click);
            // 
            // LbTime
            // 
            this.LbTime.AutoSize = true;
            this.LbTime.Location = new System.Drawing.Point(1134, 771);
            this.LbTime.Name = "LbTime";
            this.LbTime.Size = new System.Drawing.Size(65, 25);
            this.LbTime.TabIndex = 18;
            this.LbTime.Text = "Time:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1523, 818);
            this.Controls.Add(this.LbTime);
            this.Controls.Add(this.ZoomBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LbMode);
            this.Controls.Add(this.RotateBtn);
            this.Controls.Add(this.ClearBtn);
            this.Controls.Add(this.WidthBtn);
            this.Controls.Add(this.ColorBtn);
            this.Controls.Add(this.FloodFillBtn);
            this.Controls.Add(this.ScanlineBtn);
            this.Controls.Add(this.PolygonBtn);
            this.Controls.Add(this.HexagonBtn);
            this.Controls.Add(this.PentagonBtn);
            this.Controls.Add(this.TriangleBtn);
            this.Controls.Add(this.EllipseBtn);
            this.Controls.Add(this.RectangleBtn);
            this.Controls.Add(this.CircleBtn);
            this.Controls.Add(this.LineBtn);
            this.Controls.Add(this.openGLControl1);
            this.Name = "Form1";
            this.Text = "19127644_Lab03";
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SharpGL.OpenGLControl openGLControl1;
        private System.Windows.Forms.Button LineBtn;
        private System.Windows.Forms.Button CircleBtn;
        private System.Windows.Forms.Button RectangleBtn;
        private System.Windows.Forms.Button EllipseBtn;
        private System.Windows.Forms.Button TriangleBtn;
        private System.Windows.Forms.Button PentagonBtn;
        private System.Windows.Forms.Button HexagonBtn;
        private System.Windows.Forms.Button PolygonBtn;
        private System.Windows.Forms.Button ScanlineBtn;
        private System.Windows.Forms.Button FloodFillBtn;
        private System.Windows.Forms.Button ColorBtn;
        private System.Windows.Forms.Button WidthBtn;
        private System.Windows.Forms.Button ClearBtn;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button RotateBtn;
        private System.Windows.Forms.Label LbMode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ZoomBtn;
        private System.Windows.Forms.Label LbTime;
    }
}

