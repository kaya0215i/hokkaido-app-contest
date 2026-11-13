using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class UserController : MonoBehaviour {
    private UserInputActions _actions;

    // つかんでいるノード
    private Transform grabbingNodeTransform;
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
        if(grabbingNodeTransform && _actions.User.Interact.IsPressed()) {
            grabbingNodeTransform.transform.position = GetPointerPos() + grabOffset;
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
                Debug.Log(hit.collider.name);
                grabbingNodeTransform = hit.collider.transform;
                grabOffset = (Vector2)grabbingNodeTransform.position - worldPoint;
            }
        }
        else if (ctxt.canceled) {
            Debug.Log("離す");

            grabbingNodeTransform = null;
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
