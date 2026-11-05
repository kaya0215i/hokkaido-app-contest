using Cysharp.Threading.Tasks;
using MagicOnion;
using MagicOnion.Client;
using UnityEditor.PackageManager;
using UnityEngine;
using hokkaido_app_contest.Shared.Interfaces.Services;
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
            await client.ConnectionTest();
        }catch (RpcException e) {
            Debug.LogException(e);
        }
    }
}
