using Outgame;
using Cysharp.Threading.Tasks;

public interface INetworkImplement
{
    public delegate void APICallback<T>(T dataClass);

    //async
    UniTask<APIResponceLogin> Login(string uuid);
    UniTask<APIResponceGetCards> GetCards();
    //

    //callback
    void Login(string uuid, APICallback<APIResponceLogin> callback);
    void GetCards(APICallback<APIResponceGetCards> callback);
    void CreateUser(string name, APICallback<APIResponceUserCreate> callback);
    void Gacha(int gachaId, int drawCount, APICallback<APIResponceGachaDraw> callback);
}
