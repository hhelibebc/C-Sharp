using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TableTest
{
    public class Common:System.Windows.Forms.Form
    {
        public delegate void delegate1();
        protected override void  OnClosed(EventArgs e)
        {
            fun1();
            this.Hide();
            //base.OnClosed(e);
        }
        public delegate1 fun1;
    }
}
