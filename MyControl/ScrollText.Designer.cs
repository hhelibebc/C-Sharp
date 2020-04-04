namespace MyControl
{
    partial class ScrollText
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

        private void ExtraInit() {
            //int[] arr = { 10, 15, 10, 40 };
            //Init(20, 5, 800, arr);
        }
        private void CalControls() {
            head.Font = this.Font;
            head.Left = 0;
            head.Top = 0;
            head.Width = this.Width - ScrollWidth;
            head.Height = HeadHeight;

            body.Font = this.Font;
            body.Top = head.Bottom;
            body.Left = 0;
            body.Width = head.Width;
            body.Height = this.Width - ScrollWidth;
            body.ReadOnly = true;
            body.Multiline = true;

            scroll.Top = 0;
            scroll.Left = head.Right;
            scroll.Width = ScrollWidth;
            scroll.Height = this.Height;
            scroll.Minimum = 0;
            scroll.Maximum = 0;
            scroll.ValueChanged += new System.EventHandler(scroll_ValueChanged);
        }
        private void scroll_ValueChanged(object sender, System.EventArgs e)
        {
            DispPart();
        }
        private void DispPart() {
            if (lines < row)
                body.Text = data.ToString();
            else {
                int remain = lines - scroll.Value;
                int si = scroll.Value;
                if (remain < row)
                    si = lines - row;
                body.Text = data.ToString(si * (cnt + 2), row * (cnt + 2));
            }
        }
        public void SetTitle(string str) {
            head.Text = str;
        }
        public void AppendLine(string[] arr) {
            int i, len, offset = 0, limit = 0;
            for (i = 0; i < cnt; i++)
                buf[i] = ' ';
            for (i = 0; i < arr.Length; i++) {
                limit = chars[i];
                len = arr[i].Length;
                if (len < chars[i])
                    arr[i].CopyTo(0, buf, offset, len);
                else
                    arr[i].CopyTo(0, buf, offset, limit);
                offset += limit;
            }
            if (lines > 1000)
            {
                data.Remove(0, data.Length);
                lines = 0;
            }
            data.Append(buf);
            lines++;
            scroll.Maximum = lines;
            scroll.Value = lines;
            DispPart();
        }
        public void Init(int r, int c, int width, int[] items)
        { 
            this.SuspendLayout();
            this.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)134);
            this.Height = 21 * r + HeadHeight;
            this.Width = width + ScrollWidth;
            scroll = new System.Windows.Forms.VScrollBar();
            body = new System.Windows.Forms.TextBox();
            head = new System.Windows.Forms.Label();
            CalControls();
            foreach (int v in items)
                cnt += v;
            chars = items;
            row = r;
            col = c;
            buf = new char[cnt + 2];
            buf[cnt] = '\r';
            buf[cnt + 1] = '\n';
            data = new System.Text.StringBuilder();
            this.Controls.Add(scroll);
            this.Controls.Add(body);
            this.Controls.Add(head);
            this.ResumeLayout(false);
        }
        
        int[] chars;
        char[] buf;
        System.Text.StringBuilder data;
        int cnt = 0;
        int lines = 0;
        int ScrollWidth = 15;
        int HeadHeight = 26;
        int row;
        int col;
        System.Windows.Forms.VScrollBar scroll;
        System.Windows.Forms.TextBox body;
        System.Windows.Forms.Label head;
    }
}
