using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class ModeSelectNode : ExecuteNode
{

    public override UniTask Execute(Transform target, CancellationToken ct)
    {
        string tag = target.gameObject.tag;
        if(tag == "normal")
        {
            
        }
        return UniTask.CompletedTask;
    }

}
