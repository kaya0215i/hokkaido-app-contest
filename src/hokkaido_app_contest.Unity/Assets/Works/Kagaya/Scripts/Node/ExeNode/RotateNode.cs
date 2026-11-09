using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class RotateNode : ExeCuteNode {
    public Vector3 rotateValue;

    public override UniTask Execute(Transform target, CancellationToken ct) {
        target.Rotate(rotateValue);
        return UniTask.CompletedTask;
    }
}
