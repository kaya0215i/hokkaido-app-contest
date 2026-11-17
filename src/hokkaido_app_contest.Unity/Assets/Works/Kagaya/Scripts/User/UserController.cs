using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class UserController : MonoBehaviour {
    private UserInputActions _actions;

    // つかんでいるノード
    private BaseNode grabbingNode;
    // つかんでいるノードとのオフセット
    private Vector2 grabOffset;

    private void Start() {
        // インプットアクションを有効に
        _actions = new UserInputActions();
        _actions.Enable();

        _actions.User.Interact.started += MoveNode;
        _actions.User.Interact.canceled += MoveNode;
    }

    private  void OnDestroy() {
        _actions?.Dispose();
    }

    private void Update() {
        // ノードつかんでいるならマウスの/タッチしている位置に移動
        if(grabbingNode && _actions.User.Interact.IsPressed()) {
            grabbingNode.transform.position = GetPointerPos() + grabOffset;
        }
    }

    /// <summary>
    /// ノードを動かす
    /// </summary>
    private void MoveNode(InputAction.CallbackContext ctxt) {
        if (ctxt.started) {
            Debug.Log("つかむ");

            Vector2 worldPoint = GetPointerPos();
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Camera.main.transform.forward.normalized);
            
            if (hit &&
                hit.collider.CompareTag("Node")) {
                Debug.Log($"GrabNode：{hit.collider.name}");

                // ノードの情報保持
                grabbingNode = hit.collider.GetComponent<BaseNode>(); ;
                grabbingNode.GrabNode();

                grabOffset = (Vector2)grabbingNode.transform.position - worldPoint;

                // 衝突判定のためのRigidbodyを付与
                Rigidbody2D nodeRb = grabbingNode.gameObject.AddComponent<Rigidbody2D>();
                nodeRb.bodyType = RigidbodyType2D.Kinematic;
            }
        }
        else if (ctxt.canceled) {
            Debug.Log("離す");

            // ノードの情報を捨てる
            if (grabbingNode) {
                grabbingNode.ReleseNode();
                Destroy(grabbingNode.gameObject.GetComponent<Rigidbody2D>());

                grabbingNode.ConnectNode();

                grabbingNode = null;
            }
            grabOffset = Vector2.zero;
        }
    }

    /// <summary>
    /// 押しているスクリーン座標取得
    /// </summary>
    private Vector2 GetPointerPos() {
        return Camera.main.ScreenToWorldPoint(_actions.User.Pointer.ReadValue<Vector2>());
    }
}
