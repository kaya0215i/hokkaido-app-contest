using Cysharp.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;

public abstract class ExecuteNode : BaseNode {
    // 下につなげるノードのオフセット座標
    public Vector2 connectNodeOffsetPos;

    private ExecuteNode targetNode;

    // 自分がどの親ノードの子に入っているか
    public ConnectableParentNode myParentNode;

    // 自分がどのノードの下についているか
    public ExecuteNode myConnectTopNode;
    // 自分の下にどのノードがついているか
    public ExecuteNode myConnectBottomNode;

    /// <summary>
    /// 実行メソッド
    /// </summary>
    public abstract UniTask Execute(Transform target, CancellationToken ct);

    protected virtual void Update() {
        if (myConnectTopNode) {
            this.transform.position = (Vector2)myConnectTopNode.transform.position + myConnectTopNode.connectNodeOffsetPos;
        }
    }

    public override void GrabNode() {
        base.GrabNode();

        if (myParentNode) {
            myParentNode.RemoveChildNodeList(this);
            myParentNode = null;

            List<ExecuteNode> exeNodeList = new List<ExecuteNode>();
            SetConnectNodeList(ref exeNodeList);

            foreach (var exeNode in exeNodeList) {
                exeNode.myParentNode = null;
            }
        }

        if (myConnectTopNode) {
            myConnectTopNode.myConnectBottomNode = null;
            myConnectTopNode = null;
        }
    }

    /// <summary>
    /// つながっているノードの中で一番下のノード取得
    /// </summary>
    public ExecuteNode GetLastConnectNode() {
        if (myConnectBottomNode) {
            return myConnectBottomNode.GetLastConnectNode();
        }
        else {
            return this;
        }
    }

    /// <summary>
    /// 下につながっているノードをリストに格納
    /// </summary>
    public void SetConnectNodeList(ref List<ExecuteNode> nodeList) {
        nodeList.Add(this);
        myConnectBottomNode?.SetConnectNodeList(ref nodeList);
    }

    public override void ConnectNode() {
        if (!triggerTargetNode) return;
        targetNode = triggerTargetNode.GetComponent<ExecuteNode>();

        NodeType targetNodeType = targetNode.nodeType;

        switch (targetNodeType) {
            case NodeType.Execute:
                ConnectToExecuteNode();
                break;
            case NodeType.ConnectableParent:
                ConnectToConnectableParentNode();
                break;
        }

        triggerTargetNode = null;
    }

    #region つなげる処理

    /// <summary>
    /// ExecuteNodeにつなげる
    /// </summary>
    private void ConnectToExecuteNode(bool isSerchParentNode = true) {
        // 親ノードがあれば
        if (targetNode.myParentNode && isSerchParentNode) {
            targetNode = targetNode.myParentNode;
            ConnectToConnectableParentNode();
        }
        // 下につながっているノードがあれば
        else if (targetNode.myConnectBottomNode) {
            targetNode = targetNode.GetLastConnectNode();
        }

        myConnectTopNode = targetNode;
        targetNode.myConnectBottomNode = this;
    }
    /// <summary>
    /// ConnectableParentNodeにつなげる
    /// </summary>
    private void ConnectToConnectableParentNode() {
        var connectableParentNode = targetNode.GetComponent<ConnectableParentNode>();
        var lastNode = connectableParentNode.GetLastNode();
        connectableParentNode.AddChildNodeList(this);
        if (lastNode) {
            targetNode = lastNode;
            ConnectToExecuteNode(false);
        }
    }
    #endregion
}
