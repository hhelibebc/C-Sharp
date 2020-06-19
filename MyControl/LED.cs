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
    public partial class LED : UserControl
    {
        public LED()
        {
            InitializeComponent();
        }
        public void Init(int dia,Color fc,Color tc)
        {
            SuspendLayout();
            直径 = dia;
            FColor = fc;
            TColor = tc;
            bit = new Bitmap(直径, 直径);
            this.Width = 直径;
            this.Height = 直径;
            pic.Size = this.Size;
            pic.Paint += new PaintEventHandler(pic_Paint);
            ResumeLayout(false);
        }
        void pic_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = Graphics.FromImage(bit);
            if (State)
                g.FillEllipse(new SolidBrush(TColor), new Rectangle(0, 0, 直径, 直径));
            else
                g.FillEllipse(new SolidBrush(FColor), new Rectangle(0, 0, 直径, 直径));
            e.Graphics.DrawImage(bit, new Point(0, 0));
            g.Dispose();
        }
        public bool state 
        {
            get { return State; }
            set 
            {
                State = value;
                pic.Invalidate();
            }
        }
        Color FColor = SystemColors.Control;
        Color TColor = Color.Green;
        int 直径;
        bool State;
        Bitmap bit;
        public PictureBox pic;
    }
}
