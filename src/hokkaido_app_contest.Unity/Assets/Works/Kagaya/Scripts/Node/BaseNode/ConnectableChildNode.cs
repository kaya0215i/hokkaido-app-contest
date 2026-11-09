using System.Collections.Generic;
using UnityEngine;

public abstract class ConnectableChildNode : BaseNode {
    /// <summary>
    /// 子ノードリスト
    /// </summary>
    [SerializeField] protected List<BaseNode> childNodeList = new List<BaseNode>();
}
