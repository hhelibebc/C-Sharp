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
        private void HighLight(int r, int c)
        {
            SetProperty(highlight_rc[0], highlight_rc[1], SET_T.BCOLOR | SET_T.ROW, colors[0]);
            SetProperty(highlight_rc[0], highlight_rc[1], SET_T.BCOLOR | SET_T.COL, colors[0]);
            if (r != highlight_rc[0] || c != highlight_rc[1])
            {
                SetProperty(r, c, SET_T.BCOLOR | SET_T.ROW, colors[1]);
                SetProperty(r, c, SET_T.BCOLOR | SET_T.COL, colors[1]);
                highlight_rc[0] = r;
                highlight_rc[1] = c;
            }
        }
        public void Init(int r, int c, int width, ORDER_T t,DispTips fun1)
        {
            if ((t & ORDER_T.TYPE_MASK) == ORDER_T.VARS)
                Init(r, c, width / c, TABLE_T.BORDER, t, null, fun1);
            else
                Init(r, c, width / c, TABLE_T.BORDER, t, HighLight, fun1);
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
        public enum EXPAND_TYPE { PUSH, SEIZE };
        private Color[] colors = { Color.FromArgb(240, 240, 240), Color.Cyan };
        private int[] highlight_rc = { 0, 0 };
    }
}
