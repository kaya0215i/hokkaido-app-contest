using Cysharp.Threading.Tasks;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseNode : MonoBehaviour {
    public enum NodeType {
        None,
        Execute,
        ConnectableParent,
        ArithmeticOperator,
        TrueOrFalse,
        ComparisonOperator,
        ConjunctionOperator,
    }

    public NodeType nodeType = NodeType.None;

    private bool isGrabbed = false;

    // ぶつかった相手のノード
    protected BaseNode triggerTargetNode;

    protected virtual void Start() {
        if (nodeType == NodeType.None) {
            Debug.LogError($"NodeTypeが設定されていません。{this.gameObject}");
        }
    }

    /// <summary>
    /// このノードをつかむ
    /// </summary>
    public virtual void GrabNode() {
        isGrabbed = true;
    }

    /// <summary>
    /// ノードを離す
    /// </summary>
    public void ReleseNode() {
        isGrabbed = false;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        // ノードに当たったら
        // つかまれていたら
        if (!collision.CompareTag("Node") || !isGrabbed) return;
        Debug.Log("衝突");

        // 当たった相手のオブジェクトを保持
        triggerTargetNode = collision.GetComponent<BaseNode>();
    }

    private void OnTriggerExit2D(Collider2D collision) {
        // ノードに当たったら
        // つかまれていたら
        if (!collision.CompareTag("Node") || !isGrabbed || collision.gameObject != triggerTargetNode.gameObject) return;
        triggerTargetNode = null;
    }

    /// <summary>
    /// ノード同士をつなげる
    /// </summary>
    public abstract void ConnectNode();


    /// <summary>
    /// ArithmeticOperatorNodeにつなげる
    /// </summary>
    private void ConnectToArithmeticOperatorNode() {

    }
    /// <summary>
    /// TrueOrFalseNodeにつなげる
    /// </summary>
    private void ConnectToTrueOrFalseNode() {

    }
    /// <summary>
    /// ComparisonOperatorNodeにつなげる
    /// </summary>
    private void ConnectToComparisonOperatorNode() {

    }
    /// <summary>
    /// ConjunctionOperatorNodeにつなげる
    /// </summary>
    private void ConnectToConjunctionOperatorNode() {

    }
}
