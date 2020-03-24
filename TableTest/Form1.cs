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
        void ExtraInit()
        {
            tabControl1.SuspendLayout();
            tabPage2.SuspendLayout();

            table1 = new MyControl.AdvanceTable();
            table1.Location = new Point(20, 20);
            table1.Init(9, 15, 1100, MyControl.AdvanceTable.ORDER_TYPE.SETS | MyControl.AdvanceTable.ORDER_TYPE.ROW);
            table1.SetHeader(MyControl.AdvanceTable.PROP_TYPE.COL, table1_header);
            table1.SetShadow(MyControl.AdvanceTable.PROP_TYPE.ROW);

            table2 = new MyControl.AdvanceTable();
            table2.Location = new Point(20, 300);
            table2.Init(11, 14, 1100, MyControl.AdvanceTable.ORDER_TYPE.VARS | MyControl.AdvanceTable.ORDER_TYPE.COL);
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 14; j += 2)
                {
                    str.Remove(0, str.Length);
                    str.AppendFormat("var{0}{1}", i, j);
                    table2.SetString(i, j, str.ToString());
                }
            }
                table2.SetShadow(MyControl.AdvanceTable.PROP_TYPE.COL);
            table2.SetColWidth(0, 100, MyControl.AdvanceTable.EXPAND_TYPE.SEIZE);
            table2.SetColWidth(2, 100, MyControl.AdvanceTable.EXPAND_TYPE.SEIZE);
            table2.SetColWidth(4, 100, MyControl.AdvanceTable.EXPAND_TYPE.SEIZE);
            table2.SetColWidth(6, 100, MyControl.AdvanceTable.EXPAND_TYPE.SEIZE);
            table2.SetColWidth(8, 100, MyControl.AdvanceTable.EXPAND_TYPE.SEIZE);
            table2.SetColWidth(10, 100, MyControl.AdvanceTable.EXPAND_TYPE.SEIZE);
            table2.SetColWidth(12, 100, MyControl.AdvanceTable.EXPAND_TYPE.SEIZE);

            tabPage2.Controls.Add(table1);
            tabPage2.Controls.Add(table2);
            tabPage2.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
        }
        private MyControl.AdvanceTable table1, table2;
        private string[] table1_header = { "row1", "row2", "row3", "row4", "row5", "row6", "row7", "row8" };
    }
}
