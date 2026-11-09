using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class ForLoopNode : ConnectableChildNode {
    public int loopNum = 1;

    public override async UniTask Execute(Transform target, CancellationToken ct) {
        for (int i = 0; i < loopNum; i++) {
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
