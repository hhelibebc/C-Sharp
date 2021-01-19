using System.IO;
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;

namespace MyControl
{
    public class ExcelAccess : DataAccess
    {
        public ExcelAccess(Direction direction, bool autowrap)
        {
            type = direction;
            app = new Excel.Application();
            AutoWrap = autowrap;
        }
        public void Open(string str)
        {
            if (File.Exists(str))
                workbook = app.Workbooks.Open(str);
            else
            {
                workbook = app.Workbooks.Add();
                name = str;
            }
            Seek(1, 1, 1);
        }
        public void Close()
        {
            if (type == Direction.WRITE)
            {
                if (name != null)
                    workbook.SaveAs(name);
                else
                    workbook.Save();
            }
            app.Quit();
        }
        public string Read()
        {
            string str = null;
            Excel.Range range;
            if (type == Direction.READ)
            {
                if (AutoWrap)
                {
                    int i = 5;
                    while (i-- > 0)
                    {
                        range = worksheet.Cells[row, col++];
                        str = range.Text;
                        if (str != null && str != "")
                            return str;
                        else
                            NextLine();
                    }
                }
                else
                {
                    range = worksheet.Cells[row, col++];
                    str = range.Text;
                }
            }
            return str;
        }
        public void Write(string str)
        {
            if (type == Direction.WRITE)
                worksheet.Cells[row, col++] = str;
        }
        public void Seek(int r, int c, int s)
        {
            ind = s;
            if (ind <= workbook.Worksheets.Count)
            {
                worksheet = workbook.Worksheets[ind];
                row = r;
                col = c;
            }
        }
        public void NextCell()
        {
            col++;
        }
        public void NextLine()
        {
            row++;
            col = 1;
        }
        public void NextSheet()
        {
            if (ind + 1 <= workbook.Worksheets.Count)
            {
                ind++;
                Seek(1, 1, ind);
            }
        }
        public void NewSheet(string name)
        {
            worksheet = workbook.Worksheets.Add(Missing.Value, worksheet, Missing.Value, Missing.Value);
            worksheet.Name = name;
            row = 1;
            col = 1;
            ind = worksheet.Index;
        }
        public int GetSheetCount()
        {
            return workbook.Sheets.Count;
        }

        int row = 1, col = 1, ind = 1;
        string name;
        Excel._Application app;
        Excel._Workbook workbook;
        Excel._Worksheet worksheet;
        public bool AutoWrap;
        public Direction type;
    }
}
