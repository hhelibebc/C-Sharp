using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MyLayout
{
    class Panel
    {
        public Panel()
        {
            id = -1;
            type = TYPE.PANEL;
            children = new List<Panel>();
        }
        public void AddPanel(byte p, byte s)
        {
            AddControl(TYPE.PANEL, p, s);
        }
        public void AddPanel(byte p, byte s, byte c)
        {
            for (int i = 0; i < c; i++)
                AddControl(TYPE.PANEL, p, s);
        }
        public void AddHBox(byte p, byte s)
        {
            AddControl(TYPE.HBOX, p, s);
        }
        public void AddVBox(byte p, byte s)
        {
            AddControl(TYPE.VBOX, p, s);
        }
        public void AddGrid(byte p, byte s)
        {
            AddControl(TYPE.GRID, p, s);
        }
        public void AddEmpty(byte p, byte s)
        {
            AddControl(TYPE.EMPTY, p, s);
        }
        public void SetParam(Panel parent, byte p, byte s)
        {
            this.parent = parent;
            padding = p;
            spacing = s;
        }
        public void SetHorizontal(short[] arr) 
        {
            if (type == TYPE.HBOX)
                ((HBox)this).horizontal = arr;
            else if (type == TYPE.GRID)
                ((Grid)this).horizontal = arr;
            else
                MessageBox.Show("不支持设置 horizontal 属性！");
        }
        public void SetVertical(short[] arr)
        {
            if (type == TYPE.VBOX)
                ((VBox)this).vertical = arr;
            else if (type == TYPE.GRID)
                ((Grid)this).vertical = arr;
            else
                MessageBox.Show("不支持设置 vertical 属性！");
        }
        public void SetID(short id)
        {
            this.id = id;
        }
        public void SetID(byte target,short id)
        {
            children[target].id = id;
        }
        public void SetID(byte[] target,short id)
        {
            Panel p = GetControl(target);
            if(p != null)
                p.id = id;
        }
        public void Update()
        {
            if (children == null || children.Count == 0)
                return;
            byte v = (byte)(padding << 1), r = 1, c = 1;
            switch (type)
            {
                //case TYPE.PANEL:
                //    for(int i = 0; i < children.Count; i++)
                //    {
                //        children[i].rect.X = rect.X + padding;
                //        children[i].rect.Y = rect.Y + padding;
                //        children[i].rect.Width = rect.Width - v;
                //        children[i].rect.Height = rect.Height - v;
                //    }
                //    break;
                case TYPE.HBOX:
                    for (int i = 0; i < children.Count; i++)
                    {
                        children[i].rect.Y = rect.Y + padding;
                        children[i].rect.Height = rect.Height - v;
                    }
                    c = (byte)((HBox)this).horizontal.Length;
                    DisposeWidth(1, c);
                    DisposeLeft(1, c);
                    break;
                case TYPE.VBOX:
                    for (int i = 0; i < children.Count; i++)
                    {
                        children[i].rect.X = rect.X + padding;
                        children[i].rect.Width = rect.Width - v;
                    }
                    r = (byte)((VBox)this).vertical.Length;
                    DisposeHeight(r, 1);
                    DisposeTop(r, 1);
                    break;
                case TYPE.GRID:
                    r = (byte)((Grid)this).vertical.Length;
                    c = (byte)((Grid)this).horizontal.Length;
                    DisposeWidth(r, c);
                    DisposeLeft(r, c);
                    DisposeHeight(r, c);
                    DisposeTop(r, c);
                    break;
                default:break;
            }
            for (int i = 0; i < children.Count; i++)
                children[i].Update();
        }
        public void Print()
        {
            if (type == TYPE.PANEL)
            //if(type != TYPE.EMPTY)
                output.Add(new PARAM(rect, id));
            for (int i = 0; i < children.Count; i++)
                children[i].Print();
        }
        static public short[] GetDefault(byte n)
        {
            short[] tmp = new short[n];
            for (int i = 0; i < n; i++)
                tmp[i] = -1;
            return tmp;
        }
        static public TextBox[] GetTextBoxes(byte n)
        {
            TextBox[] tmp = new TextBox[n];
            for (int i = 0; i < n; i++)
            {
                tmp[i] = new TextBox();
                tmp[i].Multiline = true;
                tmp[i].ReadOnly = true;
            }
            return tmp;
        }
        static public Label[] GetLabels(byte n)
        {
            Label[] tmp = new Label[n];
            for (int i = 0; i < n; i++)
            {
                tmp[i] = new Label();
                tmp[i].AutoSize = false;
                tmp[i].BorderStyle = BorderStyle.FixedSingle;
            }
            return tmp;
        }
        static public Button[] GetButtons(byte n)
        {
            Button[] tmp = new Button[n];
            for (int i = 0; i < n; i++)
            {
                tmp[i] = new Button();
                tmp[i].AutoSize = false;
            }
            return tmp;
        }
        void DisposeLeft(byte r,byte c)
        {
            for(int i = 0; i < r; i++)
            {
                children[i * c].rect.X = rect.X + padding;
                for (int j = 1; j < c; j++)
                    children[i * c + j].rect.X = children[i * c + j - 1].rect.Right + spacing;
            }
        }
        void DisposeTop(byte r, byte c)
        {
            for (int j = 0; j < c; j++)
            {
                children[j].rect.Y = rect.Y + padding;
                for (int i = 1; i < r; i++)
                    children[i * c + j].rect.Y = children[(i - 1) * c + j].rect.Bottom + spacing;
            }
        }
        void DisposeWidth(byte r, byte c)
        {
            short[] tmp;
            if (type == TYPE.HBOX)
                tmp = ((HBox)this).horizontal;
            else
                tmp = ((Grid)this).horizontal;
            short pos = 0, neg = 0, remain = 0;
            for(int i = 0; i < c; i++)
            {
                if (tmp[i] >= 0)
                    pos += tmp[i];
                else
                    neg += tmp[i];
            }
            remain = (short)(rect.Width - pos - 2 * padding - (c - 1) * spacing);
            for(int i = 0; i < r; i++)
            {
                for(int j = 0; j < c; j++)
                {
                    if (tmp[j] >= 0)
                        children[i * c + j].rect.Width = tmp[j];
                    else
                        children[i * c + j].rect.Width = remain * tmp[j] / neg;
                }
            }
        }
        void DisposeHeight(byte r, byte c)
        {
            short[] tmp;
            if (type == TYPE.VBOX)
                tmp = ((VBox)this).vertical;
            else
                tmp = ((Grid)this).vertical;
            short pos = 0, neg = 0, remain = 0;
            for (int i = 0; i < r; i++)
            {
                if (tmp[i] >= 0)
                    pos += tmp[i];
                else
                    neg += tmp[i];
            }
            remain = (short)(rect.Height - pos - 2 * padding - (r - 1) * spacing);
            for (int i = 0; i < r; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    if (tmp[i] >= 0)
                        children[i * c + j].rect.Height = tmp[i];
                    else
                        children[i * c + j].rect.Height = remain * tmp[i] / neg;
                }
            }
        }
        void AddControl(TYPE t, byte p, byte s)
        {
            switch (type)
            {
                case TYPE.EMPTY:
                case TYPE.PANEL:
                    MessageBox.Show("当前对象不支持添加子布局器！");
                    return;
                default:break;
            }
            switch (t)
            {
                case TYPE.EMPTY: children.Add(new Empty()); break;
                case TYPE.PANEL: children.Add(new Panel()); break;
                case TYPE.HBOX: children.Add(new HBox()); break;
                case TYPE.VBOX: children.Add(new VBox()); break;
                case TYPE.GRID: children.Add(new Grid()); break;
                default: break;
            }
            children.Last().SetParam(this, p, s);
        }
        Panel GetControl(byte[] target)
        {
            Panel p = this;
            int i;
            for(i = 0; i < target.Length; i++)
            {
                if (target[i] < p.children.Count)
                    p = p.children[target[i]];
                else
                    break;
            }
            if(i == target.Length)
                return p;
            return null;
        }

        public enum TYPE { 
            PANEL,
            HBOX,
            VBOX,
            GRID,
            EMPTY
        };
        public struct PARAM
        {
            public PARAM(Rectangle r, short i)
            {
                rect = r;
                id = i;
            }
            public Rectangle rect;
            public short id;
        };
        public static List<PARAM> output;
        public List<Panel> children;
        public TYPE type;
        public Rectangle rect;

        short id;
        Panel parent;
        byte padding, spacing;
    }
    class HBox : Panel
    {
        public HBox() => type = TYPE.HBOX;

        public short[] horizontal;
    }
    class VBox : Panel
    {
        public VBox() => type = TYPE.VBOX;

        public short[] vertical;
    }
    class Grid : Panel
    {
        public Grid() => type = TYPE.GRID;

        public short[] horizontal;
        public short[] vertical;
    }
    class Empty : Panel
    {
        public Empty() => type = TYPE.EMPTY;
    }
}
