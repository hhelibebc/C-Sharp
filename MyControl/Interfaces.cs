namespace MyControl
{
    public enum Direction { READ, WRITE };
    interface DataAccess
    {
        void Open(string str);
        void Close();
        string Read();
        void Write(string str);
        void NextCell();
        void NextLine();
    }
    interface Communicate
    {

    }
    interface Iterator
    {

    }
}
