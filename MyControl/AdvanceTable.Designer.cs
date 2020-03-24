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
    partial class AdvanceTable:Table
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
            ResumeLayout(false);
        }
        public void Init(int r, int c, int width, ORDER_TYPE t)
        {
            type = t;
            Init(r, c, width / c, TABLE_T.BORDER);
        }
        public void Next(int step)
        {
            int dim = 0, offset = 0;
            switch (type)
            {
                case ORDER_TYPE.SETS | ORDER_TYPE.ROW: dim = 1; offset = 1; break;
                case ORDER_TYPE.SETS | ORDER_TYPE.COL: dim = 0; offset = 1; break;
                case ORDER_TYPE.VARS | ORDER_TYPE.ROW: dim = 1; offset = 0; break;
                case ORDER_TYPE.VARS | ORDER_TYPE.COL: dim = 0; offset = 0; break;
            }
            while (step > 0)
            {
                cur[dim]++;
                if (cur[dim] >= rc[dim])
                {
                    cur[dim] = offset;
                    cur[1 - dim] += 2 - offset;
                }
                step--;
            }
        }
        public void SetShadow(PROP_TYPE arg)
        {
            int i, j;
            switch (arg)
            {
                case PROP_TYPE.NULL:
                    SetProperty(0, 0, SET_T.ALL | SET_T.BCOLOR, colors[0]);
                    break;
                case PROP_TYPE.ROW:
                    for (i = 1; i < rc[0]; i += 2)
                        SetProperty(i, 0, SET_T.ROW | SET_T.BCOLOR, colors[1]);
                    break;
                case PROP_TYPE.COL:
                    for (i = 1; i < rc[1]; i += 2)
                        SetProperty(0, i, SET_T.COL | SET_T.BCOLOR, colors[1]);
                    break;
                case PROP_TYPE.BOTH:
                    int si = 0;
                    for (i = 0; i < rc[0]; i++)
                    {
                        for (j = 0; j < rc[1]; j++)
                        {
                            SetProperty(i, j, SET_T.ONE | SET_T.BCOLOR, colors[si]);
                            si = 1 - si;
                        }
                        si = 1 - si;
                    }
                    break;
            }
        }
        public void SetHeader(PROP_TYPE arg, string[] arr)
        {
            int i, j = 0;
            if ((type & ORDER_TYPE.TYPE_MASK) == ORDER_TYPE.SETS)
            {
                if ((arg & PROP_TYPE.BOTH) == PROP_TYPE.COL)
                {
                    for (i = 1; i < rc[0]; i++)
                        SetString(i, 0, arr[j++]);
                }
                else
                {
                    for (i = 1; i < rc[0]; i++)
                        SetString(i, 0, i.ToString());
                }
                if ((arg & PROP_TYPE.BOTH) == PROP_TYPE.ROW)
                {
                    for (i = 1; i < rc[1]; i++)
                        SetString(0, i, arr[j++]);
                }
                else
                {
                    for (i = 1; i < rc[1]; i++)
                        SetString(0, i, i.ToString());
                }
            }
            else
            {
                SetCurPos(0, 0);
                foreach (string str in arr)
                    SetValue(str);
            }
        }
        public void SetCurPos(int r, int c)
        {
            cur[0] = r;
            cur[1] = c;
        }
        public void SetValue(object arg)
        {
            SetString(cur[0], cur[1], arg.ToString());
            Next(1);
        }
        public void SetProperty(SET_T t, object arg)
        {
            SetProperty(cur[0], cur[1], t, arg);
        }
        public void SetColWidth(int c, int width, EXPAND_TYPE t)
        {
            int v, i, j;
            int old_width = (int)GetProperty(0, c, SET_T.WIDTH);
            SetProperty(0, c, SET_T.COL | SET_T.WIDTH, width);
            c++;
            if (c >= rc[1] || t == EXPAND_TYPE.PUSH)
                this.Width += width - old_width;
            if (t == EXPAND_TYPE.PUSH)
            {
                for (i = c; i < rc[1]; i++)
                {
                    v = (int)GetProperty(0, i, SET_T.LEFT);
                    SetProperty(0, i, SET_T.COL | SET_T.LEFT, v + (width - old_width));
                }
            }
            else
            {
                v = (int)GetProperty(0, c, SET_T.WIDTH);
                SetProperty(0, c, SET_T.COL | SET_T.WIDTH, v - (width - old_width));
                v = (int)GetProperty(0, c, SET_T.LEFT);
                SetProperty(0, c, SET_T.COL | SET_T.LEFT, v + (width - old_width));
            }
        }

        public enum PROP_TYPE { NULL, ROW, COL, BOTH };
        public enum ORDER_TYPE { ROW, COL, ORDER_MASK = 1, SETS = 2, VARS = 4, TYPE_MASK = 0xFE };
        public enum EXPAND_TYPE { PUSH, SEIZE };
        private ORDER_TYPE type;
        private int[] cur = { 0, 0 };
        private Color[] colors = { Color.White, Color.Cyan };
    }
}
