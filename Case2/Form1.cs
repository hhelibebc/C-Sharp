using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sunisoft.IrisSkin;
using System.IO;
using System.Runtime.InteropServices;

namespace TableTest
{

    public partial class Form1 : Form
    {
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(
            string lpAppName,
            string lpKeyName,
            string lpDefault,
            StringBuilder lpReturnedString,
            int nSize,
            string lpFileName);
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(
            string mpAppName,
            string mpKeyName,
            string mpDefault,
            string mpFileName);
        public Form1()
        {
            SkinInit();
            InitializeComponent();
            ExtraInit();
        }

        void CloseSubWindow() 
        {
            if (--subCnt <= 0)
                this.Show();
        }
        void ExtraInit()
        {
            this.SuspendLayout();
            dir = new DirectoryInfo(curPath);
            files = dir.GetFiles();
            foreach (FileInfo f in files)
                comboBox1.Items.Add(f.Name);
            comboBox1.Text = values[0];
            textBox1.Text = values[1];
            if (values[2] == "true")
                checkBox1.Checked = true;
            if (values[3] == "true")
                checkBox2.Checked = true;
            button1.Click += new EventHandler(button1_Click);
            this.ResumeLayout(false);
        }
        string ContentReader(string area, string key, string def)
        {
            StringBuilder stringBuilder = new StringBuilder(1024);         //定义一个最大长度为1024的可变字符串
            GetPrivateProfileString(area, key, def, stringBuilder, 1024, str);       //读取INI文件
            return stringBuilder.ToString();                //返回INI文件的内容
        }
        void SkinInit()
        {
            if (File.Exists(str))                       //判断是否存在该INI文件
            {
                for (int i = 0; i < 4; i++)
                    values[i] = ContentReader(area, keys[i], "");
            }
            else 
            {
                FileStream fi = File.Create(str);
                fi.Close();
            }
            if (values[0] == "")
                values[0] = "MacOS.ssk";
            se = new SkinEngine((Component)this);
            se.SkinFile = curPath + values[0];
        }
        void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "admin" && textBox2.Text == "11235813")
            {
                if (checkBox1.Checked && !form2.Visible) 
                {
                    subCnt++;
                    form2.fun1 = CloseSubWindow;
                    form2.Show();
                }
                if (checkBox2.Checked && !form3.Visible)
                {
                    subCnt++;
                    form3.fun1 = CloseSubWindow;
                    form3.Show();
                }
                if (subCnt >= 0)
                {
                    this.Hide();
                    se.SkinFile = curPath + comboBox1.Text;
                }
                else
                    MessageBox.Show("至少打开一个子窗口");
            }
            else
                MessageBox.Show("用户名或密码错误");
            if (File.Exists(str))                      //判断是否存在INI文件
            {
                values[0] = comboBox1.Text;
                values[1] = textBox1.Text;
                if (checkBox1.Checked)
                    values[2] = "true";
                else
                    values[2] = "false";
                if (checkBox2.Checked)
                    values[3] = "true";
                else
                    values[3] = "false";
                for(int i=0;i<4;i++)
                    WritePrivateProfileString(area, keys[i], values[i], str);
            }
        }
        OpenFileDialog fd = new OpenFileDialog();
        DirectoryInfo dir;
        SkinEngine se;
        FileInfo[] files;
        string[] keys = { "皮肤", "用户名", "窗口1", "窗口2" };
        string[] values = { "", "", "", "" };
        string str = ".\\config.ini";
        string area = "参数";
        string curPath = ".\\Skins\\";
        int subCnt = 0;
        Form2 form2 = new Form2();
        Form3 form3 = new Form3();
    }
}
