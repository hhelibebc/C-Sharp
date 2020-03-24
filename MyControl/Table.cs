using System.Windows.Forms;
namespace MyControl
{
    public partial class Table : UserControl
    {
        public Table()
        {
            InitializeComponent();
            rc = new int[2];
            ExtraInit();
        }
    }
}
