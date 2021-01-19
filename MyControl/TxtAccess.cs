using System;
using System.IO;
using System.Text;
// C#程序 Unicode编码
// txt文件 GB2312编码，代码页936

namespace MyControl
{
    public class TxtAccess : DataAccess
    {
        public TxtAccess(Direction direction, Encoding target, char depart)
        {
            type = direction;
            ch = depart;
            dest = target;
            strbuf = "";
        }

        public void Open(string str)
        {
            if (File.Exists(str))
            {
                fi = File.Open(str, FileMode.Open, FileAccess.ReadWrite);
                buf = new byte[fi.Length];
                fi.Read(buf, 0, (int)fi.Length);
                fi.Seek(0, SeekOrigin.End);
                if (dest != Encoding.UTF8)
                    buf = Encoding.Convert(dest, Encoding.UTF8, buf);
                char[] arr = new char[buf.Length / 2];
                strbuf = arr.ToString();
                ;
            }
            else if (type == Direction.WRITE)
                fi = File.Create(str);
            cur = strbuf.Length - 1;
        }

        public void Close()
        {
            if (fi != null)
            {
                if (type == Direction.WRITE)
                {
                    char[] arr = strbuf.ToCharArray();
                    buf = new byte[arr.Length * 2];
                    Buffer.BlockCopy(arr, 0, buf, 0, buf.Length);
                    buf = Encoding.Convert(dest, Encoding.GetEncoding(936), buf);
                    fi.Write(buf, 0, buf.Length);
                }
                fi.Close();
            }
        }

        public string Read()
        {
            if (type == Direction.WRITE || strbuf == null || cur >= strbuf.Length)
                return null;
            int ei = strbuf.IndexOf(ch, ++cur);
            int si = cur;
            if (ei == -1)
            {
                cur = strbuf.Length;
                return null;
            }
            int len = ei - si;
            cur = ei;
            return strbuf.Substring(si, len);
        }

        public void Write(string str)
        {
            strbuf += str + ch;
        }
        public void NextLine()
        {
            if (type == Direction.READ)
            {
                int si = strbuf.IndexOf('\n', ++cur);
                if (si == -1)
                    cur = strbuf.Length;
                else
                    cur = si + 1;
            }
            else
                strbuf += '\n';
        }
        public void NextCell()
        {
            if (type == Direction.READ)
                Read();
            else
                strbuf += ch;
        }

        public void SetString(string str)
        {
            strbuf = str;
            cur = -1;
        }

        Encoding dest;
        string strbuf;
        byte[] buf;
        char ch;
        int cur = -1;
        FileStream fi;
        Direction type;
    }
}
