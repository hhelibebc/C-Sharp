using System.Windows.Forms;

namespace MagicTower
{
    public partial class Talk : Form
    {
        public Talk()
        {
            InitializeComponent();
            KeyPreview = true;
            KeyDown += Talk_KeyDown;
        }

        public void Init(string[] strs)
        {
            cnt = 0;
            content = strs;
            textBox1.Text = content[cnt];
        }
        private void Talk_KeyDown(object sender, KeyEventArgs e)
        {
            if (++cnt < content.Length)
                textBox1.Text = content[cnt];
            else
                Close();
        }

        int cnt = 0;
        string[] content;
    }
}
