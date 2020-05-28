using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Game2048
{
    partial class Form1
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 475);
            this.Font = new System.Drawing.Font("楷体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private void ExtraInit()
        {
            int total = rc[0] * rc[1], i;
            SuspendLayout();
            
            arr1 = new byte[rc[0], rc[1]];
            inds = new byte[total];
            this.Height = rc[0] * 100 + 110;
            this.Width = rc[1] * 100 + 60;
            L1.Font = Font;
            L1.Text = "得分";
            L1.Location = new Point(20, 20);
            L1.Size = new System.Drawing.Size(120, 30);

            T1.Font = Font;
            T1.Text = "";
            T1.Left = L1.Right;
            T1.Top = L1.Top;
            T1.Size = L1.Size;
            T1.ReadOnly = true;

            B1.Font = Font;
            B1.Text = "重新开始";
            B1.Left = T1.Right;
            B1.Top = T1.Top;
            B1.Size = T1.Size;
            B1.Click += new EventHandler(B1_Click);

            tab1.Font = Font;
            tab1.Left = L1.Left;
            tab1.Top = L1.Bottom;
            tab1.Init(rc[0], rc[1], 100, MyControl.Table.TABLE_T.CONTROLS, 0, null, null);
            for (i = 0; i < total; i++)
                tab1.AddLabel("");
            for (i = 0; i < rc[0]; i++)
                tab1.SetRowHeight(i, 100, MyControl.AdvanceTable.EXPAND_TYPE.PUSH);
            foreach (Control c in tab1.Controls)
            {
                ((Label)c).BorderStyle = BorderStyle.Fixed3D;
                ((Label)c).TextAlign = ContentAlignment.MiddleCenter;
            }
            Controls.Add(L1);
            Controls.Add(T1);
            Controls.Add(B1);
            Controls.Add(tab1);
            ResumeLayout(false);
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (gameState)
            {
                switch (keyData)
                {
                    case Keys.Up: DisposeUp(); break;
                    case Keys.Down: DisposeDown(); break;
                    case Keys.Left: DisposeLeft(); break;
                    case Keys.Right: DisposeRight(); break;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void B1_Click(object sender, EventArgs e)
        {
            Array.Clear(arr1, 0, arr1.Length);
            gameState = true;
            tGrade = 0;
            T1.Text = "0";
            myRefresh();
        }
        private void myRefresh()
        {
            tab1.SetCurPos(0, 0);
            foreach (byte b in arr1)
            {
                if (b == 0)
                    str = "";
                else
                    str = Math.Pow(2, (double)b).ToString();
                bc = arr2[b & 3];
                tab1.SetProperty(MyControl.Table.SET_T.BCOLOR | MyControl.Table.SET_T.ONE, bc);
                tab1.SetValue(str);
            }
        }
        private int getBlankCell()
        {
            int i = 0, j = 0;
            foreach (byte b in arr1)
            {
                if (b == 0)
                    inds[j++] = (byte)i;
                i++;
            }
            return j;
        }
        private void getNewNumber(int cnt)
        {
            while (cnt-- > 0)
            {
                int v1 = getBlankCell();
                if (v1 == 0)
                {
                    gameState = false;
                    T1.Text += " GameOver";
                }
                else
                {
                    T1.Text = tGrade.ToString();
                    v1 = rd.Next(v1);
                    v1 = inds[v1];
                    arr1[v1 / rc[1], v1 % rc[1]] = (byte)(rd.Next(2) + 1);
                    myRefresh();
                }
            }
        }
        private void DisposeUp()
        {
            for (c = 0; c < rc[1]; c++)
            {
                for (v0 = 0, r = v0; r < rc[0]; r++)
                {
                    v1 = arr1[r, c];
                    if (v1 == 0 || r == v0) continue;
                    v2 = arr1[v0, c];
                    arr1[r, c] = 0;
                    if (v1 == v2)
                    {
                        arr1[v0, c]++;
                        tGrade += (int)Math.Pow(2, v1+1);
                        v0++;
                    }
                    else
                    {
                        if (v2 == 0)
                        {
                            arr1[v0, c] = (byte)v1;
                            // v0不变
                        }
                        else
                        {
                            arr1[v0 + 1, c] = (byte)v1;
                            v0++;
                        }
                    }
                }
            }
            getNewNumber(2);
        }
        private void DisposeDown()
        {
            for (c = 0; c < rc[1]; c++)
            {
                for (v0 = rc[0] - 1,r = v0; r >= 0; r--)
                {
                    v1 = arr1[r, c];
                    if (v1 == 0 || r == v0) continue;
                    v2 = arr1[v0, c];
                    arr1[r, c] = 0;
                    if (v1 == v2)
                    {
                        arr1[v0, c]++;
                        tGrade += (int)Math.Pow(2, v1 + 1);
                        v0--;
                    }
                    else
                    {
                        if (v2 == 0)
                        {
                            arr1[v0, c] = (byte)v1;
                            // v0不变
                        }
                        else
                        {
                            arr1[v0 - 1, c] = (byte)v1;
                            v0--;
                        }
                    }
                }
            }
            getNewNumber(2);
        }
        private void DisposeLeft()
        {
            for (r = 0; r < rc[0]; r++)
            {
                for (v0 = 0, c = v0; c < rc[1]; c++)
                {
                    v1 = arr1[r, c];
                    if (v1 == 0 || c == v0) continue;
                    v2 = arr1[r, v0];
                    arr1[r, c] = 0;
                    if (v1 == v2)
                    {
                        arr1[r, v0]++;
                        tGrade += (int)Math.Pow(2, v1 + 1);
                        v0++;
                    }
                    else
                    {
                        if (v2 == 0)
                        {
                            arr1[r, v0] = (byte)v1;
                            // v0不变
                        }
                        else
                        {
                            arr1[r, v0 + 1] = (byte)v1;
                            v0++;
                        }
                    }
                }
            }
            getNewNumber(2);
        }
        private void DisposeRight()
        {
            for (r = 0; r < rc[0]; r++)
            {
                for (v0 = rc[1] - 1, c = v0; c >= 0; c--)
                {
                    v1 = arr1[r, c];
                    if (v1 == 0 || c == v0) continue;
                    v2 = arr1[r, v0];
                    arr1[r, c] = 0;
                    if (v1 == v2)
                    {
                        arr1[r, v0]++;
                        tGrade += (int)Math.Pow(2, v1 + 1);
                        v0--;
                    }
                    else
                    {
                        if (v2 == 0)
                        {
                            arr1[r, v0] = (byte)v1;
                            // v0不变
                        }
                        else
                        {
                            arr1[r, v0 - 1] = (byte)v1;
                            v0--;
                        }
                    }
                }
            }
            getNewNumber(2);
        }

        Label L1 = new Label();
        TextBox T1 = new TextBox();
        Button B1 = new Button();
        MyControl.AdvanceTable tab1 = new MyControl.AdvanceTable();
        byte[,] arr1;
        byte[] inds;
        byte[] rc = { 8, 16 };
        int tGrade, r, c, v0, v1, v2;
        Color[] arr2 = 
        { 
            Color.FromArgb(0xff, 0xff, 0xff), Color.FromArgb(0xff, 0xff, 0x00),
            Color.FromArgb(0xff, 0x00, 0xff), Color.FromArgb(0xff, 0xff, 0x00) 
        };
        string str;
        Color bc = new Color();
        Random rd = new Random();
        bool gameState = false;
    }
}

