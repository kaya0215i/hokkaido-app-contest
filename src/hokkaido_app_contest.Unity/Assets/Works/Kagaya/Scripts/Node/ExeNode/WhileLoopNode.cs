using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class WhileLoopNode : ConnectableChildNode {
    public override async UniTask Execute(Transform target, CancellationToken ct) {
        while (true) {
            foreach (var childNode in childNodeList) {
                // キャンセルされてるなら例外
                ct.ThrowIfCancellationRequested();

                // nullチェック
                if (!childNode) continue;

                // 実行
                await childNode.Execute(target, ct);
            }
            // 次のフレームまで待機
            await UniTask.NextFrame();
        }
    }
}
