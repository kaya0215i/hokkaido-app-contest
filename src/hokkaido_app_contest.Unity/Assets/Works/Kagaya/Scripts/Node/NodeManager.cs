using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class NodeManager : MonoBehaviour {
    [SerializeField] private Transform target;
    [SerializeField] private Transform nodes;

    private bool isStarted = false;

    // スタートノードリスト
    [SerializeField] private List<StartNode> startNodes = new List<StartNode>();
    private CancellationTokenSource cts;

    /// <summary>
    /// statNodeリストに追加
    /// </summary>
    public void AddStartNode(StartNode startNode) {
        startNodes.Add(startNode);
    }

    /// <summary>
    /// startNodeリストから削除
    /// </summary>
    public void RemoveStartNode(StartNode startNode) {
        startNodes.Remove(startNode);
    }

    /// <summary>
    /// プログラムを開始
    /// </summary>
    public async void StartProgram() {
        if (!target) return;
        if (isStarted) return;

        Debug.Log("Start");

        isStarted = true;

        // cts発行
        cts = new CancellationTokenSource();

        var tasks = new List<UniTask>();

        foreach (var startNode in startNodes) {
            // nullチェック
            if (!startNode) continue;

            // 実行してリストに格納
            tasks.Add(
                startNode.Execute(target, cts.Token)
            );
        }

        // 待機
        try {
            await UniTask.WhenAll(tasks);
        }
        catch (OperationCanceledException e) {
            Debug.Log("Stop");
        }

        isStarted = false;
    }

    /// <summary>
    /// プログラムを止める
    /// </summary>
    public void StopProgram() {
        // キャンセル
        cts.Cancel();
        target.DOKill();

        isStarted = false;
    }
}
