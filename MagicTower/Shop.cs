using System;
using System.Windows.Forms;

namespace MagicTower
{
    public partial class Shop : Form
    {
        public Shop()
        {
            InitializeComponent();
            button1.Click += Button1_Click;
            button2.Click += Button1_Click;
            button3.Click += Button1_Click;
            button4.Click += Button1_Click;
            KeyPreview = true;
            KeyDown += Form2_KeyDown;
        }


        public void Init(int cash, int cost, int life, int attack, int defense)
        {
            ret = 0;
            enough = cash >= cost;
            if (enough)
            {
                textBox1.Text = "本次需花费 " + cost.ToString() + " 金币为你提升如下属性：";
                button1.Text = "提升生命值：" + life.ToString();
                button2.Text = "提升攻击力：" + attack.ToString();
                button3.Text = "提升防御力：" + defense.ToString();
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
            }
            else
            {
                textBox1.Text = "本次需花费 " + cost.ToString() + " 金币!你目前的金币还不够,任意键退出。";
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
            }
        }
        public int GetReturn()
        {
            return ret;
        }
        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (enough)
            {
                switch (e.KeyData)
                {
                    case Keys.NumPad1: Button1_Click(button1, null); break;
                    case Keys.NumPad2: Button1_Click(button2, null); break;
                    case Keys.NumPad3: Button1_Click(button3, null); break;
                    default: Button1_Click(button4, null); break;
                }
            }
            else
                Button1_Click(button4, null);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (sender == button1)
                ret = 1;
            else if (sender == button2)
                ret = 2;
            else if (sender == button3)
                ret = 3;
            else
                ret = 0;
            Close();
        }

        bool enough = false;
        int ret = 0;
    }
}
