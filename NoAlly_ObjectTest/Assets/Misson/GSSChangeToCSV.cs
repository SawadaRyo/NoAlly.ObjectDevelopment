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
            // �t�@�C�����J��//false�F����㏑����csv�t�@�C�����쐬//�G���R�[�f�B���O
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
            Console.WriteLine(e.Message); // ��O���o���ɃG���[���b�Z�[�W��\��
        }
    }
}
