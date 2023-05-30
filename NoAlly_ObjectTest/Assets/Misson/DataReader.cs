using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;　　　//　<=　☆　宣言を追加します

/// <summary>
/// 読み込むシートのデータ群。シートの情報を管理します
/// </summary>
[System.Serializable]
public class SheetData
{
    public MissonDataPath SheetName;
    public List<string[]> DatasList = new List<string[]>();
}

/// <summary>
/// スプレッドシートのデータを取得してシート別に解析します
/// 解析が終了したら、OnLoadEnd に登録したメソッドを呼び出します
/// 解析結果は SheetData クラスの DatasList 変数からシートごとに取得
/// </summary>
public class DataReader : MonoBehaviour
{
    [SerializeField] string _gssID = "1cgSvD5aqdSlMYCYJ0MoecTIybA_ZRWfOPZ98ppZJ0h4/edit#gid=0"; //グーグルスプレッドシートにアクセスするためのID

    [SerializeField, Header("スプレッドシートを読み込み後にコールバックする関数")]
    UnityEvent _onLoadEnd;　　　// この変数にインスペクターからメソッドを登録しておくと、スプレッドシートを読み込み後にコールバックする

    [SerializeField, Header("読み込みたいシート名を選択")]
    SheetData[] _sheetDatas;

    public SheetData[] Sheet => _sheetDatas;
    public async UniTask Reload() => await GetFromWebAsync();

    public async UniTask GetFromWebAsync()
    {   //メソッドの戻り値、キーワードの追加、メソッド名を非同期処理のメソッドであることがわかる名称に変更します。


        // CancellationToken の作成
        var token = this.GetCancellationTokenOnDestroy();  //　<=　☆②　処理を追加します　

        // 複数のシートの読み込み
        for (int i = 0; i < _sheetDatas.Length; i++)
        {

            // シート名だけ毎回読み込み先を変更する
            string url = "https://docs.google.com/spreadsheets/d/" + _gssID + "/gviz/tq?tqx=out:csv&sheet=" + _sheetDatas[i].SheetName.ToString();
            Debug.Log(url);
            // Web の GoogleSpreadSheet を取得
            UnityWebRequest request = UnityWebRequest.Get("https://script.googleusercontent.com/macros/echo?user_content_key=eZd8qj64xdQEhwdKnRxsFWPEptWv0V_7UopZs73_doX5l_S1YNdpmQ_uc1GxpKOFNfHTilZXRFasiuzmAa4HxKaRHhIArbtgm5_BxDlH2jW0nuo2oDemN9CCS2h10ox_1xSncGQajx_ryfhECjZEnNYgiOI-sVU5uCDgGWcftGfVSSt2KKrLYFNlYfUJJy1HfJReQy1ZqrryuR7bYuv6629Oq-nIc_5UPtz3rmbcAkVFmNp2exz8OQ&lib=MfTJfxUnn6Elzklcl7ja6dfmVBeyHE92y");

            // 非同期処理の処理に加えて CancellationToken の設定を行い、非同期処理をキャンセルした場合には処理が停止するようにセットする
            await request.SendWebRequest().WithCancellation(token);　　//　<=　☆③　処理を書き換えます

            //Debug.Log(request.downloadHandler.text);

            // エラーが発生しているか確認
            bool protocol_error = request.result == UnityWebRequest.Result.ProtocolError ? true : false;
            bool connection_error = request.result == UnityWebRequest.Result.ConnectionError ? true : false;

            // エラーがある場合
            if (protocol_error || connection_error)
            {

                // エラー表示を行い、処理を終了する
                Debug.LogError(request.error);
                return;               //　<=　☆④  処理を書き換えます
            }
            // GSS の各シートごとのデータを List<string[]> の形で取得
            Debug.Log(request.downloadHandler.text);
            _sheetDatas[i].DatasList = ConvertToArrayListFromCSV(request.downloadHandler.text);
        }
        // GSSLoader のメソッドを登録しておいて実行する
        _onLoadEnd.Invoke();
    }

    /// <summary>
    /// 取得した GoogleSpreadSheet(GSS) の CSV ファイルの情報を ArrayList 形式に変換
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    List<string[]> ConvertToArrayListFromCSV(string text)
    {
        StringReader reader = new StringReader(text);
        reader.ReadLine();  // 1行目はヘッダー情報なので、読み込んで何もしないで読み飛ばす

        List<string[]> rows = new List<string[]>();

        while (reader.Peek() >= 0)
        {  // Peek メソッドを使うと戻り値の値によりファイルの末尾まで達しているか確認できる。末尾になると -1 が戻るので、そうなるまで繰り返す
            string[] elements = reader.ReadLine().Split(',');  //データの読み込みと分割 

            for (int i = 0; i < elements.Length; i++)
            {　　// 1文字ずつ取り出す
                if (elements[i] == "\"\"")
                {
                    continue;                       // 取り出した文字が空白である場合は除去
                }
                elements[i] = elements[i].TrimStart('"').TrimEnd('"');  // 文字の最初と最後にある "" を削除する
                //Debug.Log(elements[i]);
            }
            rows.Add(elements);
        }
        return rows;
    }
}
