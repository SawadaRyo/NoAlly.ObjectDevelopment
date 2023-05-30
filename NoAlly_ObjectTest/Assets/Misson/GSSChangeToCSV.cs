using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class GSSChangeToCSV
{


    public void CreateCSV(string dataPath, string[] datas)
    {
        try
        {
            // ファイルを開く//false：毎回上書きでcsvファイルを作成//エンコーディング
            StreamWriter sw = new StreamWriter(dataPath, true, Encoding.GetEncoding("Shift_JIS"));
            if (datas != null && datas.Length > 0)
            {
                for (int i = 0; i < datas.Length; i++)
                {
                    sw.WriteLine(datas[i]);
                }
            }
            sw.Close();

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message); // 例外検出時にエラーメッセージを表示
        }
    }
}
