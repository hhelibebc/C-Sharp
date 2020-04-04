using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScrollTextTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ExtraInit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            dt = System.DateTime.Now;
            arr[0] = dt.ToLongTimeString();
            arr[1] = rd.Next().ToString();
            arr[2] = "扩展数据帧";
            arr[3] = rd.Next().ToString();
            //text1.AppendLine(arr);
            textBox1.Text += arr[0] + '\t' + arr[1] + '\t' + arr[2] + '\t' + arr[3] + "\r\n";
            textBox1.Select(textBox1.Text.Length, 0);
            textBox1.ScrollToCaret();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (state)
            {
                timer1.Stop();
                button1.Text = "继续";
            }
            else 
            {
                timer1.Start();
                button1.Text = "暂停";
            }
            state = !state;
        }
    }
}
