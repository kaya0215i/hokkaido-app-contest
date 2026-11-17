using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ConnectableParentNode : ExecuteNode {
    // 子につなげるノードのオフセット座標
    public Vector2 childNodeOffsetPos;

    /// <summary>
    /// 子ノードリスト
    /// </summary>
    [SerializeField] protected List<ExecuteNode> childNodeList = new List<ExecuteNode>();

    // 下につなげられるか
    public bool isConnectableUnder = false;

    protected override void Update() {
        base.Update();

        if (childNodeList.FirstOrDefault()) {
            childNodeList.First().transform.position = (Vector2)this.transform.position + childNodeOffsetPos;
        }
    }



    /// <summary>
    /// つながっている一番下のノード取得
    /// </summary>
    public ExecuteNode GetLastNode() {
        return childNodeList.LastOrDefault();
    }

    /// <summary>
    /// Nodeセット
    /// </summary>
    public void AddChildNodeList(ExecuteNode setExeNode) {
        List<ExecuteNode> exeNodeList = new List<ExecuteNode>();
        setExeNode.SetConnectNodeList(ref exeNodeList);

        foreach (var exeNode in exeNodeList) {
            childNodeList.Add(exeNode);
            exeNode.myParentNode = this;
        }
    }

    /// <summary>
    /// Node削除
    /// </summary>
    public void RemoveChildNodeList(ExecuteNode rmExeNode) {
        if (!childNodeList.Contains(rmExeNode)) return;

        int index = childNodeList.IndexOf(rmExeNode);
        
        childNodeList.RemoveRange(index, childNodeList.Count - index);
    }
}
