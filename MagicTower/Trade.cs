using System;
using System.Windows.Forms;

namespace MagicTower
{
    public partial class Trade : Form
    {
        public Trade()
        {
            InitializeComponent();
            button1.Click += Button1_Click;
            button2.Click += Button1_Click;
            KeyPreview = true;
            KeyDown += Form3_KeyDown;
        }

        public void Init(string str, int cash, int cost)
        {
            enough = cash >= cost;
            if (enough)
            {
                textBox1.Text = str;
                button1.Visible = true;
                button2.Visible = true;
            }
            else
            {
                textBox1.Text = "抱歉，你目前的金币不足！任意键退出。。。";
                button1.Visible = false;
                button2.Visible = false;
            }
        }
        public int GetReturn()
        {
            return ret;
        }
        private void Form3_KeyDown(object sender, KeyEventArgs e)
        {
            if (enough && e.KeyData == Keys.NumPad1)
                Button1_Click(button1, null);
            else
                Button1_Click(button2, null);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (sender == button1)
                ret = 1;
            else
                ret = 0;
            Close();
        }
        int ret = 0;
        bool enough = false;
    }
}
