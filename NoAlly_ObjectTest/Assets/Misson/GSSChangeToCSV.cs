using System;
using System.IO;
using System.Text;

public class GSSChangeToCSV
{
    string _folderPath = "Assets/Misson/MissonCSV";
    public void ChangeToCSV(string[] datas)
    {
        StreamWriter sw = new StreamWriter(_folderPath, true, Encoding.GetEncoding("Shift_JIS"));
    }
}
