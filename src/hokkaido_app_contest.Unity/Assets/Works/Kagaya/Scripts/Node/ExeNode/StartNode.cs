using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class StartNode : ConnectableParentNode {
    private NodeManager nodeManager;

    protected override void Start() {
        base.Start();

        nodeManager = FindObjectsByType<NodeManager>(FindObjectsSortMode.None).First();
        if (nodeManager == null) {
            Debug.LogError("NodeManagerが見つかりませんでした。");
            return;
        }

        nodeManager.AddStartNode(this);
    }

    private void OnDestroy() {
        if (nodeManager == null) {
            Debug.LogError("NodeManagerが見つかりませんでした。");
            return;
        }

        nodeManager.RemoveStartNode(this);
    }

    public override async UniTask Execute(Transform target, CancellationToken ct) {
        foreach (var childNode in childNodeList) {
            // キャンセルされてるなら例外
            ct.ThrowIfCancellationRequested();

            // nullチェック
            if (!childNode) continue;

            // 実行
            await childNode.Execute(target, ct);
        }
    }
}
