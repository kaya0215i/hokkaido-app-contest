using System.Collections.Generic;
using UnityEngine;

public abstract class ConnectableChildNode : ExecuteNode {
    /// <summary>
    /// 子ノードリスト
    /// </summary>
    [SerializeField] protected List<ExecuteNode> childNodeList = new List<ExecuteNode>();
}
