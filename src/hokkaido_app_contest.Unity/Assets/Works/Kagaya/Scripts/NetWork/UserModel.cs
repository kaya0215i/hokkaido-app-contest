using Cysharp.Threading.Tasks;
using MagicOnion;
using MagicOnion.Client;
using UnityEditor.PackageManager;
using UnityEngine;
using hokkaido_app_contest.Shared.Interfaces.Services;
using hokkaido_app_contest.Shared.Models.Entities;
using Grpc.Core;

public class UserModel : Singleton<UserModel> {
#if DEBUG
    string serverURL = "http://localhost:5244";
#else
    string serverURL = "";
#endif

    private GrpcChannelx channelx;
    private IUserService client;

    /// <summary>
    /// MagicOnion接続処理
    /// </summary>
    public UniTask CreateUserModel() {
        channelx = GrpcChannelx.ForAddress(serverURL);
        client = MagicOnionClient.Create<IUserService>(channelx);
        return UniTask.CompletedTask;
    }

    /// <summary>
    /// 接続テスト
    /// </summary>
    public async UniTask ConnectionTest() {
        try {
            User[] users = await client.ConnectionTest();

            string txt = string.Empty;
            foreach (var user in users) {
                txt += $"Id：{user.Id}, Name：{user.Name}\n";
            }
            Debug.Log(txt);
        }catch (RpcException e) {
            Debug.LogException(e);
        }
    }
}
