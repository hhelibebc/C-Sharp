using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;


namespace MyControl
{
    partial class Table
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
            this.SuspendLayout();
            // 
            // Table
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Table";
            this.ResumeLayout(false);

        }
        #endregion

        public delegate void HighLightCross(int r, int c);
        public delegate void DispTips(string str);
        private void ExtraInit()
        {
            SuspendLayout();
            //Init(10, 10, 64, TABLE_T.CONTROLS, 0, null, null);
            //AddLabel("1");
            //AddLabel("2");
            //AddLabel("3");
            //AddLabel("4");
            //AddLabel("5");
            //Next(6);
            //AddTextBox("T1");
            //AddTextBox("T2");
            //AddTextBox("T3");
            //AddTextBox("T4");
            //AddTextBox("T5");
            //Next(6);
            //AddCheckBox("T1");
            //AddCheckBox("T2");
            //AddCheckBox("T3");
            //AddCheckBox("T4");
            //AddCheckBox("T5");
            //Next(3);
            //AddComboBox(new string[] { "1", "2" });
            //AddComboBox(new string[] { "3", "4" });
            //AddComboBox(new string[] { "5", "6" });
            //AddComboBox(new string[] { "7", "8" });
            //AddComboBox(new string[] { "9", "10" });
            ResumeLayout(false);
        }
        private bool InvalidRC(int r, int c)
        {
            return (r >= 0 && r < rc[0] && c >= 0 && c < rc[1]);
        }
        private void SetProp(int r, int c, SET_T t, object arg)
        {
            if (!InvalidRC(r, c))
                return;
            switch (t)
            {
                case SET_T.BCOLOR:
                    Controls[r * rc[1] + c].BackColor = (Color)arg;
                    break;
                case SET_T.LEFT:
                    Controls[r * rc[1] + c].Left = (int)arg;
                    break;
                case SET_T.WIDTH:
                    Controls[r * rc[1] + c].Width = (int)arg;
                    break;
            }
        }
        private void SetCommonProperty(Control src,string str)
        {
            src.Font = this.Font;
            src.Text = str;
            src.Left = cur[1] * w;
            src.Top = cur[0] * h;
            src.Width = w;
            src.Height = h;
            src.Click += new EventHandler(src_Click);
            Next(1);
        }
        private void src_Click(object sender, EventArgs e)
        {
            int ind = Controls.GetChildIndex((Control)sender);
            if (fun1 != null)
                fun1(ind / rc[1], ind % rc[1]);
            if (fun2 != null)
                fun2(Controls[ind].Text);
        }

        public void Init(int r, int c, int weight, TABLE_T st, ORDER_T t,HighLightCross f1,DispTips f2)
        {
            if (r * c != 0 && (r > 3 || c > 3))
            {
                int i;
                w = weight;
                rc[0] = r;
                rc[1] = c;
                style = st;
                type = 0;
                fun1 = f1;
                fun2 = f2;
                Controls.Clear();
                SuspendLayout();

                this.Size = new Size(w * rc[1], h * rc[0]);
                if (style != TABLE_T.CONTROLS)
                {
                    for (i = 0; i < r * c; i++)
                        AddTextBox("");
                    type = t;
                }
                ResumeLayout(false);
            }
            else
                MessageBox.Show("行、列数需均不为0，且不全小于4");
        }
        public void Next(int step)
        {
            switch (type)
            {
                case ORDER_T.SETS | ORDER_T.ROW:
                    cur[1] += step;
                    while (cur[1] >= rc[1]) {
                        cur[1] -= rc[1];
                        cur[0]++;
                    }
                    break;
                case ORDER_T.SETS | ORDER_T.COL:
                    cur[0] += step;
                    while (cur[0] >= rc[0])
                    {
                        cur[0] -= rc[0];
                        cur[1]++;
                    }
                    break;
                case ORDER_T.VARS | ORDER_T.ROW:
                    cur[1] += step * 2;
                    while (cur[1] >= rc[1])
                    {
                        cur[1] -= rc[1];
                        cur[0]++;
                    }
                    break;
                case ORDER_T.VARS | ORDER_T.COL:
                    cur[0] += step;
                    while (cur[0] >= rc[0])
                    {
                        cur[0] -= rc[0];
                        cur[1] += 2;
                    }
                    break;
                default:
                    cur[1] += step;
                    while (cur[1] >= rc[1])
                    {
                        cur[1] -= rc[1];
                        cur[0]++;
                    }
                    break;
            }
        }
        public void SetCurPos(int r, int c)
        {
            cur[0] = r;
            cur[1] = c;
        }
        public Control GetControl(int r, int c)
        {
            if (InvalidRC(r, c))
            {
                return Controls[r * rc[1] + c];
            }
            return null;
        }
        public void AddTextBox(string str)
        {
            TextBox tmp = new TextBox();
            SetCommonProperty(tmp,str);
            if (style == TABLE_T.INPUT)
                tmp.ReadOnly = false;
            else
                tmp.ReadOnly = true;
            if (style == TABLE_T.NULL)
                tmp.BorderStyle = BorderStyle.None;
            else
                tmp.BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(tmp);
        }
        public void AddLabel(string str)
        {
            Label tmp = new Label();
            SetCommonProperty(tmp, str);
            Controls.Add(tmp);
        }
        public void AddCheckBox(string str)
        {
            CheckBox tmp = new CheckBox();
            SetCommonProperty(tmp, str);
            Controls.Add(tmp);
        }
        public void AddComboBox(string[] str)
        {
            ComboBox tmp = new ComboBox();
            SetCommonProperty(tmp, str[0]);
            tmp.Items.AddRange(str);
            Controls.Add(tmp);
        }
        public void SetString(int r, int c, string str)
        {
            if (!InvalidRC(r, c))
                return;
            Controls[r * rc[1] + c].Text = str;
        }
        public void SetProperty(int r, int c, SET_T t, object arg)
        {
            SET_T kind = t & SET_T.PROP_MASK;
            int i, j;
            switch (t & SET_T.RANGE_MASK)
            {
                case SET_T.ONE:
                    SetProp(r, c, kind, arg);
                    break;
                case SET_T.ROW:
                    for (i = 0; i < rc[1]; i++)
                        SetProp(r, i, kind, arg);
                    break;
                case SET_T.COL:
                    for (i = 0; i < rc[0]; i++)
                        SetProp(i, c, kind, arg);
                    break;
                case SET_T.ALL:
                    for (i = 0; i < rc[0]; i++)
                    {
                        for (j = 0; j < rc[1]; j++)
                        {
                            SetProp(i, j, kind, arg);
                        } 
                    }
                    break;
            }
        }
        public string GetString(int r, int c)
        {
            if (!InvalidRC(r, c))
                return null;
            return Controls[r * rc[1] + c].Text;
        }
        public object GetProperty(int r, int c, SET_T t)
        {
            if (InvalidRC(r, c))
            {
                switch (t)
                {
                    case SET_T.BCOLOR: return Controls[r*rc[1]+c].BackColor;
                    case SET_T.LEFT: return Controls[r*rc[1]+c].Left;
                    case SET_T.WIDTH: return Controls[r*rc[1]+c].Width;
                }
            }
            return null;
        }

        public enum TABLE_T { NULL, BORDER, INPUT, CONTROLS };
        public enum ORDER_T { ROW, COL, ORDER_MASK = 1, SETS = 2, VARS = 4, TYPE_MASK = 0xFE };
        public enum SET_T
        {
            ONE, ROW, COL, ALL, RANGE_MASK = 3, BCOLOR = 4, FCOLOR = 8,
            LEFT = 0x10, TOP = 0x20, LOCATION = 0x30, WIDTH = 0x40, 
            HIGH = 0x80, SIZE = 0xC0, PROP_MASK = 0xFFFC
        };
        public int[] rc { get; private set; }
        public ORDER_T type;
        protected int[] cur = { 0, 0 };
        private HighLightCross fun1;
        private DispTips fun2;
        private TABLE_T style;
        private int w = 75, h = 26;
    } 
}
