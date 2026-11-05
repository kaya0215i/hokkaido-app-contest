using UnityEngine;

public class ConnectionTest : MonoBehaviour {
    private async void Start() {
        await UserModel.I.CreateUserModel();

        await UserModel.I.ConnectionTest();
    }
}
