using System.Drawing;
namespace MyControl
{
    partial class Curve
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
            components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        }

        #endregion

        private void ExtraInit()
        {
            SuspendLayout();
            //Init(1000, 800, (float)12.5, (float)412.6, Color.Red);
            ResumeLayout(false);
        }
        public void Init(int w, int h, float min, float max, Color Color)
        {
            this.Font = new Font("宋体", 21F, FontStyle.Regular, GraphicsUnit.Pixel, (byte)134);
            this.Width = w;
            this.Height = h;
            color = Color;
            width = w - 70;
            high = h - 30;
            xmin = 0;
            xmax = width;
            ymax = max;
            ymin = min;

            pen = new Pen(color);
            pts = new Point[2];

            rect = new Rectangle(70, 0, width, high);
            g = this.CreateGraphics();

            limits = new System.Windows.Forms.TextBox[4];
            for (int i = 0; i < 4; i++)
            {
                limits[i] = new System.Windows.Forms.TextBox();
                limits[i].BorderStyle = System.Windows.Forms.BorderStyle.None;
                limits[i].ReadOnly = true;
                limits[i].Font = Font;
                limits[i].Width = 70;
                limits[i].Height = 30;
            }
            limits[0].Text = ymin.ToString();
            limits[0].Left = 0;
            limits[0].Top = rect.Bottom - 27;

            limits[1].Text = ymax.ToString();
            limits[1].Left = 0;
            limits[1].Top = 0;

            limits[2].Text = xmin.ToString();
            limits[2].Left = rect.Left;
            limits[2].Top = rect.Bottom;

            limits[3].Text = xmax.ToString();
            limits[3].Left = width;
            limits[3].Top = rect.Bottom;

            Controls.Add(limits[0]);
            Controls.Add(limits[1]);
            Controls.Add(limits[2]);
            Controls.Add(limits[3]);
        }
        public void AddValue(float v)
        {
            if (v < ymin)
                v = ymin;
            else if (v > ymax)
                v = ymax;
            if (cnt <= width)
            {
                cnt++;
                pts[1].X = cnt + 70;
                pts[1].Y = rect.Bottom - (int)((v - ymin) / (ymax - ymin) * high);
                if (cnt >= 2)
                {
                    Curve_Paint(null, null);
                }
                pts[0] = pts[1];
            }
            else
            {
                xmin = xmax;
                xmax += width;
                limits[2].Text = xmin.ToString();
                limits[3].Text = xmax.ToString();
                cnt = 0;
                this.Invalidate(rect);
            }
        }
        private void Curve_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if(cnt == 2)
                g.DrawRectangle(new Pen(Color.Black,2), rect);
            g.DrawLine(pen, pts[0], pts[1]);
        }
        int width, high, xmin, xmax, cnt;
        float ymin, ymax;
        Pen pen;
        Graphics g;
        Point[] pts;
        Color color;
        Rectangle rect;
        System.Windows.Forms.TextBox[] limits;
    }
}
