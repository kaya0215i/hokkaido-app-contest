using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class MoveNode : ExeCuteNode {
    public Vector3 moveValue;

    public override UniTask Execute(Transform target, CancellationToken ct) {
        target.Translate(moveValue);
        return UniTask.CompletedTask;
    }
}
