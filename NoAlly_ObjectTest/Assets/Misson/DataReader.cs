using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;�@�@�@//�@<=�@���@�錾��ǉ����܂�

/// <summary>
/// �ǂݍ��ރV�[�g�̃f�[�^�Q�B�V�[�g�̏����Ǘ����܂�
/// </summary>
[System.Serializable]
public class SheetData
{
    public MissonDataPath SheetName;
    public List<string[]> DatasList = new List<string[]>();
}

/// <summary>
/// �X�v���b�h�V�[�g�̃f�[�^���擾���ăV�[�g�ʂɉ�͂��܂�
/// ��͂��I��������AOnLoadEnd �ɓo�^�������\�b�h���Ăяo���܂�
/// ��͌��ʂ� SheetData �N���X�� DatasList �ϐ�����V�[�g���ƂɎ擾
/// </summary>
public class DataReader : MonoBehaviour
{
    [SerializeField] string _gssID = "1cgSvD5aqdSlMYCYJ0MoecTIybA_ZRWfOPZ98ppZJ0h4/edit#gid=0"; //�O�[�O���X�v���b�h�V�[�g�ɃA�N�Z�X���邽�߂�ID

    [SerializeField, Header("�X�v���b�h�V�[�g��ǂݍ��݌�ɃR�[���o�b�N����֐�")]
    UnityEvent _onLoadEnd;�@�@�@// ���̕ϐ��ɃC���X�y�N�^�[���烁�\�b�h��o�^���Ă����ƁA�X�v���b�h�V�[�g��ǂݍ��݌�ɃR�[���o�b�N����

    [SerializeField, Header("�ǂݍ��݂����V�[�g����I��")]
    SheetData[] _sheetDatas;

    public SheetData[] Sheet => _sheetDatas;
    public async UniTask Reload() => await GetFromWebAsync();

    public async UniTask GetFromWebAsync()
    {   //���\�b�h�̖߂�l�A�L�[���[�h�̒ǉ��A���\�b�h����񓯊������̃��\�b�h�ł��邱�Ƃ��킩�閼�̂ɕύX���܂��B


        // CancellationToken �̍쐬
        var token = this.GetCancellationTokenOnDestroy();  //�@<=�@���A�@������ǉ����܂��@

        // �����̃V�[�g�̓ǂݍ���
        for (int i = 0; i < _sheetDatas.Length; i++)
        {

            // �V�[�g����������ǂݍ��ݐ��ύX����
            string url = "https://docs.google.com/spreadsheets/d/" + _gssID + "/gviz/tq?tqx=out:csv&sheet=" + _sheetDatas[i].SheetName.ToString();
            Debug.Log(url);
            // Web �� GoogleSpreadSheet ���擾
            UnityWebRequest request = UnityWebRequest.Get("https://script.googleusercontent.com/macros/echo?user_content_key=eZd8qj64xdQEhwdKnRxsFWPEptWv0V_7UopZs73_doX5l_S1YNdpmQ_uc1GxpKOFNfHTilZXRFasiuzmAa4HxKaRHhIArbtgm5_BxDlH2jW0nuo2oDemN9CCS2h10ox_1xSncGQajx_ryfhECjZEnNYgiOI-sVU5uCDgGWcftGfVSSt2KKrLYFNlYfUJJy1HfJReQy1ZqrryuR7bYuv6629Oq-nIc_5UPtz3rmbcAkVFmNp2exz8OQ&lib=MfTJfxUnn6Elzklcl7ja6dfmVBeyHE92y");

            // �񓯊������̏����ɉ����� CancellationToken �̐ݒ���s���A�񓯊��������L�����Z�������ꍇ�ɂ͏�������~����悤�ɃZ�b�g����
            await request.SendWebRequest().WithCancellation(token);�@�@//�@<=�@���B�@���������������܂�

            //Debug.Log(request.downloadHandler.text);

            // �G���[���������Ă��邩�m�F
            bool protocol_error = request.result == UnityWebRequest.Result.ProtocolError ? true : false;
            bool connection_error = request.result == UnityWebRequest.Result.ConnectionError ? true : false;

            // �G���[������ꍇ
            if (protocol_error || connection_error)
            {

                // �G���[�\�����s���A�������I������
                Debug.LogError(request.error);
                return;               //�@<=�@���C  ���������������܂�
            }
            // GSS �̊e�V�[�g���Ƃ̃f�[�^�� List<string[]> �̌`�Ŏ擾
            Debug.Log(request.downloadHandler.text);
            _sheetDatas[i].DatasList = ConvertToArrayListFromCSV(request.downloadHandler.text);
        }
        // GSSLoader �̃��\�b�h��o�^���Ă����Ď��s����
        _onLoadEnd.Invoke();
    }

    /// <summary>
    /// �擾���� GoogleSpreadSheet(GSS) �� CSV �t�@�C���̏��� ArrayList �`���ɕϊ�
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    List<string[]> ConvertToArrayListFromCSV(string text)
    {
        StringReader reader = new StringReader(text);
        reader.ReadLine();  // 1�s�ڂ̓w�b�_�[���Ȃ̂ŁA�ǂݍ���ŉ������Ȃ��œǂݔ�΂�

        List<string[]> rows = new List<string[]>();

        while (reader.Peek() >= 0)
        {  // Peek ���\�b�h���g���Ɩ߂�l�̒l�ɂ��t�@�C���̖����܂ŒB���Ă��邩�m�F�ł���B�����ɂȂ�� -1 ���߂�̂ŁA�����Ȃ�܂ŌJ��Ԃ�
            string[] elements = reader.ReadLine().Split(',');  //�f�[�^�̓ǂݍ��݂ƕ��� 

            for (int i = 0; i < elements.Length; i++)
            {�@�@// 1���������o��
                if (elements[i] == "\"\"")
                {
                    continue;                       // ���o�����������󔒂ł���ꍇ�͏���
                }
                elements[i] = elements[i].TrimStart('"').TrimEnd('"');  // �����̍ŏ��ƍŌ�ɂ��� "" ���폜����
                //Debug.Log(elements[i]);
            }
            rows.Add(elements);
        }
        return rows;
    }
}
