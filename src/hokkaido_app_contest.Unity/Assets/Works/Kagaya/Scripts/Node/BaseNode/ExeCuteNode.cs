using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public abstract class ExecuteNode : BaseNode {
    /// <summary>
    /// 実行メソッド
    /// </summary>
    public abstract UniTask Execute(Transform target, CancellationToken ct);
}
