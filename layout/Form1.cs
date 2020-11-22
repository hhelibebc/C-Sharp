using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace layout
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Paint += Form1_Paint;
            Resize += Form1_Resize;
            MouseClick += Form1_MouseClick;

            pen = new Pen(Color.Black);
            MyLayout.Panel.output = new List<MyLayout.Panel.PARAM>();

            Basic();
            //Test1();
            //Test2();
            //Test3();
            //Test4();
            //Test5();
            //Test6();
        }
        void Test1()
        {
            main = new MyLayout.HBox();
            main.SetParam(null, 5, 5);

            main.AddPanel(5, 5);
            main.AddEmpty(5, 5);
            main.AddPanel(5, 5);
            main.AddVBox(5, 5);
            main.AddGrid(5, 5);
            main.SetHorizontal(new short[] { 100, -1, -2, -1, 100 });
        }
        void Test2()
        {
            main = new MyLayout.VBox();
            main.SetParam(null, 5, 5);

            main.AddPanel(5, 5);
            main.AddEmpty(5, 5);
            main.AddPanel(5, 5);
            main.AddVBox(5, 5);
            main.AddGrid(5, 5);
            main.SetVertical(new short[] { 100, -1, -2, -1, 100 });
        }
        void Test3()
        {

            main = new MyLayout.Grid();
            main.SetParam(null, 5, 5);

            main.AddPanel(5, 5);
            main.AddPanel(5, 5);
            main.AddEmpty(5, 5);
            main.AddPanel(5, 5);
            main.AddPanel(5, 5);

            main.AddPanel(5, 5);
            main.AddPanel(5, 5);
            main.AddEmpty(5, 5);
            main.AddPanel(5, 5);
            main.AddPanel(5, 5);

            main.AddEmpty(5, 5);
            main.AddEmpty(5, 5);
            main.AddEmpty(5, 5);
            main.AddEmpty(5, 5);
            main.AddEmpty(5, 5);

            main.AddPanel(5, 5);
            main.AddPanel(5, 5);
            main.AddEmpty(5, 5);
            main.AddPanel(5, 5);
            main.AddPanel(5, 5);

            main.AddPanel(5, 5);
            main.AddPanel(5, 5);
            main.AddEmpty(5, 5);
            main.AddPanel(5, 5);
            main.AddPanel(5, 5);
            main.SetHorizontal(new short[] { 100, -1, -2, -1, 100 });
            main.SetVertical(new short[] { 100, -1, -2, -1, 100 });
        }
        void Test4()
        {
            main = new MyLayout.VBox();
            main.SetParam(null, 10, 10);

            main.AddHBox(5, 5);
            main.AddPanel(5, 5);
            main.AddGrid(5, 3);
            main.SetVertical(new short[] { 40, 120, -1 });

            MyLayout.Panel p = main.children[0];
            p.AddPanel(5, 5);
            p.AddEmpty(5, 5);
            p.AddPanel(5, 5);
            p.SetHorizontal(new short[] { 100, -1, 100 });

            MyLayout.Panel p2 = main.children[2];
            p2.AddPanel(0, 0, 24);
            p2.SetHorizontal(MyLayout.Panel.GetDefault(4));
            p2.SetVertical(MyLayout.Panel.GetDefault(6));
        }
        void Test5()
        {
            main = new MyLayout.VBox();
            main.SetParam(null, 5, 5);
            main.AddHBox(3, 3);
            main.AddHBox(3, 3);
            main.AddPanel(3, 3);
            main.AddHBox(3, 3);
            main.AddHBox(3, 3);
            main.AddHBox(3, 3);
            main.AddGrid(3, 3);
            main.SetVertical(new short[] { 40, 50, 120, -1, -1, 40, -7 });

            MyLayout.Panel p = main.children[0];
            p.AddPanel(0, 0);
            p.AddEmpty(0, 0);
            p.AddPanel(0, 0);
            p.AddPanel(0, 0);
            p.AddPanel(0, 0);
            p.SetHorizontal(new short[] { 100, -1, 100, 100, 100 });

            p = main.children[1];
            p.AddPanel(0, 0);
            p.AddPanel(0, 0);
            p.AddEmpty(0, 0);
            p.AddPanel(0, 0);
            p.SetHorizontal(new short[] { 50, 120, -1, 50 });

            p = main.children[3];
            p.AddPanel(0, 0);
            p.AddPanel(0, 0);
            p.AddEmpty(0, 0);
            p.SetHorizontal(new short[] { -1, -1, -4});

            p = main.children[4];
            p.AddPanel(0, 0);
            p.AddPanel(0, 0);
            p.AddPanel(0, 0);
            p.AddPanel(0, 0);
            p.AddPanel(0, 0);
            p.AddPanel(0, 0);
            p.SetHorizontal(MyLayout.Panel.GetDefault(6));

            p = main.children[5];
            p.AddPanel(0, 0);
            p.AddPanel(0, 0);
            p.AddEmpty(0, 0);
            p.SetHorizontal(new short[] { 140, 120, -1 });

            p = main.children[6];
            p.AddPanel(0, 0, 35);
            p.SetHorizontal(MyLayout.Panel.GetDefault(5));
            p.SetVertical(MyLayout.Panel.GetDefault(7));
        }
        void Test6()
        {
            main = new MyLayout.HBox();
            main.SetParam(null, 30, 10);

            main.AddPanel(5, 5);
            main.AddPanel(5, 5);
            main.AddPanel(5, 5);
            main.AddPanel(5, 5);
            main.AddPanel(5, 5);
            main.SetHorizontal(new short[] { 100, -1, -2, -1, 100 });
        }
        void Basic()
        {
            SuspendLayout();
            Controls.AddRange(MyLayout.Panel.GetTextBoxes(1));
            Controls.AddRange(MyLayout.Panel.GetButtons(24));
            for(int i = 0; i < 24; i++)
            {
                Controls[i + 1].Text = names[i];
                Controls[i + 1].Click += ButtonClick;
            }
            ResumeLayout(false);

            main = new MyLayout.VBox();
            main.rect.X = 10;
            main.rect.Y = 10;
            main.SetParam(null, 10, 5);

            main.AddPanel(0, 0);
            main.AddGrid(0, 3);
            main.SetVertical(new short[] {120, -1 });
            main.SetID(0, 0);

            MyLayout.Panel p3 = main.children[1];
            p3.AddPanel(3, 3, 24);
            p3.SetHorizontal(MyLayout.Panel.GetDefault(4));
            p3.SetVertical(MyLayout.Panel.GetDefault(6));
            for (int i = 0; i < 24; i++)
                p3.SetID((byte)i, (short)(i + 1));

            Form1_Resize(null, null);
            Initialized();
        }
        
        void Initialized()
        {
            result = 0;
            num = 0;
            symbol = 0;
            state = STATE.INITIAL;
            tmp = "0";
            isPos = true;
            SetString();
        }
        void SetString()
        {
            Controls[0].Text = tmp;
        }
        void GetValue() 
        {
            double.TryParse(tmp, out num);
        }
        void InputN(int n)
        {
            if(tmp != "0" && state != STATE.SYM && state != STATE.FINAL)
                tmp += n.ToString();
            else
                tmp = n.ToString();
            SetString();
            switch (state)
            {
                case STATE.INITIAL:
                case STATE.SYM:
                case STATE.FINAL:
                    state = STATE.NUM;
                    break;
                case STATE.FLOAT_N:
                    state = STATE.FLOAT_F;
                    break;
                default:break;
            }
        }
        void InputSymbol(int n)
        {
            GetValue();
            if (n != 0)
            {
                result = num;
                isPos = true;
            }
            switch (symbol)
            {
                case 1: result += num; break;
                case 2: result -= num; break;
                case 3: result *= num; break;
                case 4: result /= num; break;
                default:break;
            }

            state = STATE.SYM;
            symbol = (byte)n;
        }
        void InputOpt(int n)
        {
            GetValue();
            if (num == 0 && n == 2)
                tmp = errors[0];
            else if (num < 0 && n == 4)
                tmp = errors[1];
            else
            {
                switch (n)
                {
                    case 1: result = num / 100; break;
                    case 2: result = 1 / num; break;
                    case 3: result = num * num; break;
                    case 4: result = Math.Sqrt(num); break;
                    default: break;
                }
                tmp = result.ToString();
            }
            SetString();
            state = STATE.FINAL;
        }
        private void ButtonClick(object sender, EventArgs e)
        {
            int idx = Controls.IndexOf((Control)sender);
            switch (idx)
            {
                case 9:  InputN(7); break;
                case 10: InputN(8); break;
                case 11: InputN(9); break;
                case 13: InputN(4); break;
                case 14: InputN(5); break;
                case 15: InputN(6); break;
                case 17: InputN(1); break;
                case 18: InputN(2); break;
                case 19: InputN(3); break;
                case 22: InputN(0); break;

                case 1: InputOpt(1); break;
                case 5: InputOpt(2); break;
                case 6: InputOpt(3); break;
                case 7: InputOpt(4); break;

                case 8:
                case 12:
                case 16:
                case 20:
                    byte new_sym = (byte)((24 - idx) / 4);
                    if (symbol != 0)
                    {
                        InputSymbol(0);
                        symbol = new_sym;
                        tmp = result.ToString();
                        SetString();
                    }
                    else
                        InputSymbol(new_sym);
                    break;

                case 2:           // clear error
                    if (state == STATE.FINAL)
                        Initialized();
                    else
                    {
                        tmp = "0";
                        num = 0;
                        isPos = true;
                        SetString();
                        state = STATE.SYM;
                    }
                    break;
                case 3:           // clear all
                    if (state != STATE.INITIAL)
                        Initialized();
                    break;
                case 4:           // backspace
                    if (state != STATE.SYM && state != STATE.FINAL)
                    {
                        if (tmp.Length > 1)
                        {
                            if (tmp[tmp.Length - 1] == '.')
                                state = STATE.NUM;
                            tmp = tmp.Substring(0, tmp.Length - 1);
                        }
                        else if (tmp.Length == 1)
                            tmp = "0";
                        SetString();
                    }
                    break;
                case 21:          // symbol
                    if(tmp != "0")
                    {
                        if (isPos)
                            tmp = "-" + tmp;
                        else
                            tmp = tmp.Substring(1);
                        isPos = !isPos;
                        SetString();
                    }
                    break;
                case 23:          // point
                    if(state != STATE.FLOAT_N && state != STATE.FLOAT_F)
                    {
                        if(state ==  STATE.SYM || state == STATE.FINAL)
                            tmp = "0.";
                        else
                            tmp = tmp + ".";
                        state = STATE.FLOAT_N;
                        SetString();
                    }
                    break;
                case 24:          // equal
                    if(state != STATE.INITIAL && state != STATE.FINAL)
                    {
                        if (tmp == "0" && symbol == 4)
                            tmp = errors[0];
                        else
                        {
                            InputSymbol(0);
                            tmp = result.ToString();
                        }
                        symbol = 0;
                        state = STATE.FINAL;
                        SetString();
                    }
                    break;
                default:
                    break;
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            //rect.X = e.X;
            //rect.Y = e.Y;
            //rect.Width = 100;
            //rect.Height = 30;
            //Form1_Paint(null, null);
        }

        void Adjust()
        {
            foreach(MyLayout.Panel.PARAM x in MyLayout.Panel.output)
            {
                Controls[x.id].Location = x.rect.Location;
                Controls[x.id].Size = x.rect.Size;
            }
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            g = this.CreateGraphics();
            main.rect.Width = Width - 20;
            main.rect.Height = Height - 50;
            main.Update();
            MyLayout.Panel.output.Clear();
            main.Print();
            Adjust();
            //Invalidate(true);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //if (g != null)
            //{
            //    foreach (MyLayout.Panel.PARAM x in MyLayout.Panel.output)
            //        g.DrawRectangle(pen, x.rect);
            //}
        }

        Pen pen;
        Graphics g;
        MyLayout.Panel main;
        STATE state;
        double result = 0, num = 0;
        byte symbol = 0;
        bool isPos;
        string tmp;
        string[] names =
        {
            "%", "CE", "C", "退格",
            "倒数", "平方", "平方根", "÷",
            "7", "8", "9", "×",
            "4", "5", "6", "-",
            "1", "2", "3", "+",
            "±", "0", "·", "=",
        };
        string[] errors =
        {
            "Can't divide by zero!",
            "Can't get a root to a negative!"
        };
        enum STATE
        {
            INITIAL,
            FLOAT_N,
            FLOAT_F,
            NUM,
            SYM,
            FINAL
        };
    }
}
