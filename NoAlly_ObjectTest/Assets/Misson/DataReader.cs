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
    public MissonType SheetName;
    public List<string[]> DatasList = new List<string[]>();
}

/// <summary>
/// スプレッドシートのデータを取得してシート別に解析します
/// 解析が終了したら、OnLoadEnd に登録したメソッドを呼び出します
/// 解析結果は SheetData クラスの DatasList 変数からシートごとに取得
/// </summary>
public class DataReader : MonoBehaviour
{

    public string SheetID = "1cgSvD5aqdSlMYCYJ0MoecTIybA_ZRWfOPZ98ppZJ0h4/edit#gid=0";

    public UnityEvent OnLoadEnd;　　　// この変数にインスペクターからメソッドを登録しておくと、スプレッドシートを読み込み後にコールバックする

    [Header("読み込みたいシート名を選択")]
    public SheetData[] sheetDatas;


    ////*　処理を変更　*////


    //public void Reload() => StartCoroutine(GetFromWeb());   //  処理を書き換えますのでコメントアウトします

    public async UniTask Reload() => await GetFromWebAsync();


    ////*　処理を変更　*////


    ////*　☆①～②の処理を変更・追加　*////


    public async UniTask GetFromWebAsync()
    {   //　<=　☆①　メソッドの戻り値、キーワードの追加、メソッド名を非同期処理のメソッドであることがわかる名称に変更します。


        // CancellationToken の作成
        var token = this.GetCancellationTokenOnDestroy();  //　<=　☆②　処理を追加します　


        ////*　ここまで　*////


        // 複数のシートの読み込み
        for (int i = 0; i < sheetDatas.Length; i++)
        {

            // シート名だけ毎回読み込み先を変更する
            string url = "https://docs.google.com/spreadsheets/d/" + SheetID + "/gviz/tq?tqx=out:csv&sheet=" + sheetDatas[i].SheetName.ToString();

            // Web の GoogleSpreadSheet を取得
            UnityWebRequest request = UnityWebRequest.Get(url);


            ////*　☆③～④の処理を変更します　*////


            // 取得できるまで待機
            //yield return request.SendWebRequest();　　　//　<=　☆③　処理を書き換えますのでコメントします


            // 非同期処理の処理に加えて CancellationToken の設定を行い、非同期処理をキャンセルした場合には処理が停止するようにセットする
            await request.SendWebRequest().WithCancellation(token);　　//　<=　☆③　処理を書き換えます

            Debug.Log(request.downloadHandler.text);

            // エラーが発生しているか確認
            bool protocol_error = request.result == UnityWebRequest.Result.ProtocolError ? true : false;
            bool connection_error = request.result == UnityWebRequest.Result.ConnectionError ? true : false;

            // エラーがある場合
            if (protocol_error || connection_error)
            {

                // エラー表示を行い、処理を終了する
                Debug.LogError(request.error);
                //yield break;　　　　//　<=　☆④  処理を書き換えますのでコメントします

                return;               //　<=　☆④  処理を書き換えます
            }


            ////*　ここまで　*////


            // GSS の各シートごとのデータを List<string[]> の形で取得
            sheetDatas[i].DatasList = ConvertToArrayListFromCSV(request.downloadHandler.text);
        }

        // GSSLoader のメソッドを登録しておいて実行する
        OnLoadEnd.Invoke();
    }

    /// <summary>
    /// 取得した GoogleSpreadSheet(GSS) の CSV ファイルの情報を ArrayList 形式に変換
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    private List<string[]> ConvertToArrayListFromCSV(string text)
    {
        StringReader reader = new StringReader(text);
        reader.ReadLine();  // 1行目はヘッダー情報なので、読み込んで何もしないで読み飛ばす

        List<string[]> rows = new List<string[]>();

        while (reader.Peek() >= 0)
        {  // Peek メソッドを使うと戻り値の値によりファイルの末尾まで達しているか確認できる。末尾になると -1 が戻るので、そうなるまで繰り返す
            string line = reader.ReadLine();        // 一行ずつ読み込み
            string[] elements = line.Split(',');    // 行のセルは,で区切られているので、それを分割して１文字ずつの情報が入った配列にする

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
