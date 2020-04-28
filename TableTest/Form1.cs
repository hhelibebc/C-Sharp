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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ExtraInit();
        }
        private void Tips(string str)
        {
            text1.Text = str;
        }
        void ExtraInit()
        {
            int i;
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();

            curve1 = new MyControl.Curve();
            curve1.Location = new Point(20, 20);
            curve1.Init(tabPage1.Width - 40, tabPage1.Height - 40, 0, 100, Color.Red);
            timer1.Start();
            tabPage1.Controls.Add(curve1);

            text1 = new TextBox();
            text1.Font = this.Font;
            text1.Location = new Point(20, 20);
            text1.Size = new System.Drawing.Size(this.Width, 27);
            text1.ReadOnly = true;

            table1 = new MyControl.AdvanceTable();
            table1.Location = new Point(20, 50);
            table1.Init(9, 15, 1100, MyControl.AdvanceTable.ORDER_T.SETS | MyControl.AdvanceTable.ORDER_T.ROW, Tips);
            for (i = 1; i < 9; i++)
                table1.SetString(i, 0, i.ToString());
            for (i = 1; i < 15; i++)
                table1.SetString(0, i, i.ToString());
            table1.SetCurPos(2, 2);
            // 2.7182818284590452353602874713527
            table1.SetValue(2.7182818284590452353602874713527);
            
            table2 = new MyControl.AdvanceTable();
            table2.Location = new Point(20, 330);
            table2.Init(9, 14, 1100, MyControl.AdvanceTable.ORDER_T.VARS | MyControl.AdvanceTable.ORDER_T.COL, Tips);
            table2.SetCurPos(0, 0);
            for (i = 0; i < 63; i++)
            {
                table2.SetValue(i);
            }
            table2.SetCurPos(3, 3);
            table2.SetValue(3.1415926535897932384626);

            tabPage2.Controls.Add(table1);
            tabPage2.Controls.Add(table2);
            tabPage2.Controls.Add(text1);
            tabPage1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
        }
        private MyControl.AdvanceTable table1, table2;
        private MyControl.Curve curve1;
        private TextBox text1;
        private string[] table1_header = { "row1", "row2", "row3", "row4", "row5", "row6", "row7", "row8" };
        private int x;

        private void timer1_Tick(object sender, EventArgs e)
        {
            //Random rd = new Random();
            x++;
            float v = (float)Math.Sin(x*Math.PI/180) * 50 + 50;
            curve1.AddValue(v);
        }
    }
}
