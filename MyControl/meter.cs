using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyControl
{
    public partial class meter : UserControl
    {
        public meter()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
            InitializeComponent();
            //Init(150, -500, 500, "A");
        }
        public void Init(int dia, double min, double max, string unit)
        {
            this.SuspendLayout();
            this.Width = dia;
            this.Height = dia;
            pic.Width = dia;
            pic.Height = dia;

            直径 = dia;
            MinV = min;
            MaxV = max;
            中心 = 直径 / 2;
            指针长度 = 中心 - 15;
            单位 = unit;

            strFormat.Alignment = StringAlignment.Center;
            pic.BackColor = Color.Transparent;
            pic.Paint += new PaintEventHandler(pic_Paint);
            DrawBackImg();
            Value = (MinV + MaxV) / 2;

            this.ResumeLayout(false);
        }

        // 绘制背景 用来总体控制背景的绘制
        private void DrawBackImg()
        {
            Bitmap bit = new Bitmap(this.Width, this.Height);
            Graphics gp = Graphics.FromImage(bit);
            gp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #region 在这里可以扩展需要绘制的背景项目
            //外框
            drawFrame(gp);
            // 画刻度
            DrawRuling(gp);
            //画点
            drawPoint(gp);
            //绘制单位
            DrawUnitStr(gp);
            #endregion
            //当绘制完成后，直接直接设置为背景
            this.BackgroundImage = bit;
        }
        //绘制单位
        private void DrawUnitStr(Graphics gp)
        {
            gp.DrawString(单位, new Font("宋体", 10), new SolidBrush(边框颜色), new PointF(中心, (float)(中心 - 中心 * 0.3)), strFormat);
        }
        // 画外框
        private void drawFrame(Graphics gp)
        {
            Pen pen = new Pen(边框颜色, 2);
            Rectangle rec = new Rectangle(5, 5, 直径 - 10, 直径 - 10);
            gp.DrawEllipse(pen, rec);
        }
        // 画刻度  此次较为复杂，主要是在绘制刻度值时需要处理
        private void DrawRuling(Graphics gp)
        {
            //刻度
            int cerX = 中心;
            int cerY = 中心;

            //这里需要注意，因外在上面的图中标识了rad=0的位置，而我们的仪表时270度的，0点在135度处，

            //为了符合该效果所以起始位置设为135度。
            float start = 135;
            float sweepShot = 0;
            int dx = 0;
            int dy = 0;
            int soildLenght = 8;
            Pen linePen = new Pen(边框颜色, 1);
            float span = (float)((MaxV - MinV) / 30);
            float sp = (float)MinV;
            //用于右边数字右对齐
            StringFormat stf = new StringFormat();
            stf.Alignment = StringAlignment.Far;

            StringFormat stfMid = new StringFormat();
            stfMid.Alignment = StringAlignment.Center;
            stfMid.LineAlignment = StringAlignment.Center;
            for (int i = 0; i <= 30; i++)
            {
                //注意此处，C#提供的三角函数计算中使用的弧度值，而此处获取的是角度值，需要转化

                double rad = (sweepShot + start) * Math.PI / 180;
                float radius = 中心 - 5;
                int px = (int)(cerX + radius * Math.Cos(rad));
                int py = (int)(cerY + radius * Math.Sin(rad));
                if (sweepShot % 15 == 0)
                {
                    linePen.Width = 2;

                    //计算刻度中的粗线
                    dx = (int)(cerX + (radius - soildLenght) * Math.Cos(rad));
                    dy = (int)(cerY + (radius - soildLenght) * Math.Sin(rad));

                    //绘制刻度值，注意字串对其方式
                    string str = sp.ToString("f0");
                    if (sweepShot <= 45)
                    {
                        gp.DrawString(str, new Font("宋体", 9), new SolidBrush(边框颜色), new PointF(dx, dy - 5));
                    }
                    else if (sweepShot > 45 && sweepShot < 135)
                    {
                        gp.DrawString(str, new Font("宋体", 9), new SolidBrush(边框颜色), new PointF(dx, dy));
                    }
                    else if (sweepShot == 135)
                    {
                        gp.DrawString(str, new Font("宋体", 9), new SolidBrush(边框颜色), new PointF(dx, dy + 10), stfMid);
                    }
                    else if (sweepShot > 135 && sweepShot < 225)
                    {
                        gp.DrawString(str, new Font("宋体", 9), new SolidBrush(边框颜色), new PointF(dx, dy), stf);
                    }
                    else if (sweepShot >= 225)
                    {
                        gp.DrawString(str, new Font("宋体", 9), new SolidBrush(边框颜色), new PointF(dx, dy - 5), stf);
                    }

                }
                else
                {

                    //计算刻度中细线

                    linePen.Width = 1;
                    dx = (int)(cerX + (radius - soildLenght + 2) * Math.Cos(rad));
                    dy = (int)(cerY + (radius - soildLenght + 2) * Math.Sin(rad));
                }

                //绘制刻度线
                gp.DrawLine(linePen, new Point(px, py), new Point(dx, dy));
                sp += span;
                sweepShot += 9;
            }
        }
        //画中间的点
        private void drawPoint(Graphics gp)
        {
            Pen p = new Pen(边框颜色);
            int tmpWidth = 6;
            int px = 中心 - tmpWidth;

            gp.DrawEllipse(p, new Rectangle(px, px, 2 * tmpWidth, 2 * tmpWidth));

            //在画点时，我使用了指针的颜色，这样看起来，更真实一点
            gp.FillEllipse(new SolidBrush(指针颜色), new Rectangle(px + 2, px + 2, 2 * tmpWidth - 4, 2 * tmpWidth - 4));
        }
        //为了方式绘制指针时产生的闪烁，PictureBox添加该事件方法
        private void pic_Paint(object sender, PaintEventArgs e)
        {
            DrawForeImg(e.Graphics);
        }
        //使用方法
        public double ChangeValue
        {
            get { return Value; }
            set
            {
                if (value <= MaxV && value >= MinV)
                    Value = value;
                else if(value > MaxV)
                {
                    MaxV = value;
                    Value = value;
                }
                else if (value < MinV)
                {
                    MinV = value;
                    Value = value;
                }
                pic.Invalidate();
            }
        }
        //指针的具体画法
        private void DrawForeImg(Graphics gp)
        {
            Bitmap bit = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(bit);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //画针
            DrawPin(g);
            DrawString(g);

            //注意此处的绘制方式，这样可以有效减少界面的闪烁问题。
            gp.DrawImage(bit, new Point(0, 0));
            g.Dispose();

        }
        //画针
        private void DrawPin(Graphics g)
        {
            float start = 135;
            float sweepShot = (float)((Value - MinV) / (MaxV - MinV) * 270);

            Pen linePen = new Pen(指针颜色, 1);
            Pen NxPen = new Pen(指针颜色, 2);
            double rad = (sweepShot + start) * Math.PI / 180;
            float radius = 中心 - 5;
            int dx = (int)(中心 + (指针长度) * Math.Cos(rad));
            int dy = (int)(中心 + (指针长度) * Math.Sin(rad));

            int px = (int)(中心 + (指针长度 * 0.4) * Math.Cos(rad));
            int py = (int)(中心 + (指针长度 * 0.4) * Math.Sin(rad));

            g.DrawLine(linePen, new Point(中心, 中心), new Point(dx, dy));
            g.DrawLine(NxPen, new Point(中心, 中心), new Point(px, py));
        }
        //绘制在仪表下面的值
        private void DrawString(Graphics g)
        {
            string str = Value.ToString("F2");
            g.DrawString(str, new Font("宋体", 9), new SolidBrush(指针颜色), new PointF(中心, (float)(中心 + 中心 * 0.4)), strFormat);
        }

        public double Value;
        public double MaxV;
        public double MinV;
        int 直径;
        int 中心;
        int 指针长度;
        string 单位;
        public Color 指针颜色 = Color.Red;
        public Color 边框颜色 = Color.Gray;
        StringFormat strFormat = new StringFormat();
        private System.Windows.Forms.PictureBox pic;
    }
}
