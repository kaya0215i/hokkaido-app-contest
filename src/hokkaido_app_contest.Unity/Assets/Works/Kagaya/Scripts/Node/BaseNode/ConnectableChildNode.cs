using System.Collections.Generic;
using UnityEngine;

public abstract class ConnectableChildNode : ExeCuteNode {
    /// <summary>
    /// 子ノードリスト
    /// </summary>
    [SerializeField] protected List<ExeCuteNode> childNodeList = new List<ExeCuteNode>();
}
