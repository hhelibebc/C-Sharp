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

        private void ExtraInit()
        {
            if (rc[0] * rc[1] != 0)
            {
                int i, j;
                SuspendLayout();
                this.Size = new Size(w * rc[1], h * rc[0]);
                tbs = new TextBox[rc[0], rc[1]];
                for (i = 0; i < rc[0]; i++)
                {
                    for (j = 0; j < rc[1]; j++)
                    {
                        tbs[i, j] = new TextBox();
                        tbs[i, j].Font = this.Font;
                        if (style == TABLE_T.INPUT)
                            tbs[i, j].ReadOnly = false;
                        else
                            tbs[i, j].ReadOnly = true;
                        if (style == TABLE_T.NULL)
                            tbs[i, j].BorderStyle = BorderStyle.None;
                        else
                            tbs[i, j].BorderStyle = BorderStyle.FixedSingle;
                        tbs[i, j].Location = new Point(j * w, i * h);
                        tbs[i, j].Size = new Size(w, h);
                        Controls.Add(tbs[i, j]);
                    }
                }
                ResumeLayout(false);
            }
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
                    tbs[r, c].BackColor = (Color)arg;
                    break;
                case SET_T.BORDER: 
                    tbs[r,c].BorderStyle = (BorderStyle)arg;
                    break;
                case SET_T.LEFT:
                    tbs[r, c].Left = (int)arg;
                    break;
                case SET_T.WIDTH:
                    tbs[r, c].Width = (int)arg;
                    break;
            }
        }
        public void Init(int r, int c, int weight, TABLE_T st)
        {
            if (r * c != 0 && (r > 3 || c > 3))
            {
                w = weight;
                rc[0] = r;
                rc[1] = c;
                style = st;
                Controls.Clear();
                ExtraInit();
            }
            else
                MessageBox.Show("行、列数需均不为0，且不全小于4");
        }
        public string GetString(int r, int c)
        {
            if (!InvalidRC(r, c))
                return null;
            return tbs[r, c].Text;
        }
        public void SetString(int r, int c, string str)
        {
            if (!InvalidRC(r, c))
                return;
            tbs[r, c].Text = str;
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
        public object GetProperty(int r, int c, SET_T t)
        {
            if (InvalidRC(r, c))
            {
                switch (t)
                {
                    case SET_T.BCOLOR: return tbs[r, c].BackColor;
                    case SET_T.BORDER: return tbs[r, c].BorderStyle;
                    case SET_T.LEFT: return tbs[r, c].Left;
                    case SET_T.WIDTH: return tbs[r, c].Width;
                }
            }
            return null;
        }

        public enum TABLE_T { NULL, BORDER, INPUT = 3 };
        public enum SET_T
        {
            ONE, ROW, COL, ALL, RANGE_MASK = 3, BCOLOR = 4, FCOLOR = 8,
            LEFT = 0x10, TOP = 0x20, LOCATION = 0x30, WIDTH = 0x40, 
            HIGH = 0x80, SIZE = 0xC0, BORDER = 0x100, PROP_MASK = 0xFFFC
        };
        public int[] rc { get; private set; }
        private TABLE_T style;
        private int w = 75, h = 26;
        private TextBox[,] tbs;
    } 
}
