using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TableTest
{
    public partial class Form2 : Common
    {
        public Form2()
        {
            InitializeComponent();
            ExtraInit();
        }
        void ExtraInit() 
        {
            SuspendLayout();
            meter1 = new MyControl.meter();
            meter1.边框颜色 = Color.Black;
            meter1.指针颜色 = Color.Blue;
            meter1.Init(300, -300, 500, "N/m");
            Controls.Add(meter1);
            ResumeLayout(false);
            timer1.Start();
        }
        MyControl.meter meter1;

        bool dir = true;
        int value;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (dir)
            {
                value += 10;
                if (value >= meter1.MaxV)
                    dir = false;
            }
            else
            {
                value -= 10;
                if (value <= meter1.MinV)
                    dir = true;
            }
            meter1.ChangeValue = value;
        }
    }
}
