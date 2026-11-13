using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class IfNode : ConnectableChildNode {
    public TrueOrFalseNode calcNode;

    public override async UniTask Execute(Transform target, CancellationToken ct) {
        // calcNodeのインスタンスがある。かつCalcメソッドの返り値がTrueなら
        if (calcNode && (bool)calcNode.Calc()) {
            foreach (var childNode in childNodeList) {
                // キャンセルされてるなら例外
                ct.ThrowIfCancellationRequested();

                // nullチェック
                if (!childNode) continue;

                // 実行
                await childNode.Execute(target, ct);
            }
        }

        return;
    }
}
