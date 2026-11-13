using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class DelayNode : ExecuteNode {
    public float delayTime = 0;

    public override async UniTask Execute(Transform target, CancellationToken ct) {
        await UniTask.Delay(TimeSpan.FromSeconds(delayTime), cancellationToken: ct);
    }
}
