using NCalc;
using UnityEngine;

/// <summary>
/// 論理演算ノード
/// </summary>
public class ConjunctionOperatorNode : TrueOrFalseNode {
    public enum ConjunctionOperatorType {
        And, // &&
        Or, // ||
        Not, // !
    }

    [SerializeField] private ConjunctionOperatorType conjunctionOperatorType;

    /// <summary>
    /// InputCalcNode_左
    /// </summary>
    [SerializeField] private TrueOrFalseNode inputOperatorNodeLeft;
    /// <summary>
    /// InputCalcNode_右
    /// </summary>
    [SerializeField] private TrueOrFalseNode inputOperatorNodeRight;

    public override object Calc() {
        if (conjunctionOperatorType == ConjunctionOperatorType.Not) {
            // 左のスロットがnullだったらFalse
            if (!inputOperatorNodeLeft) {
                return false;
            }
            // 入れ子のNestedCalcメソッドを実行
            inputStringLeft = inputOperatorNodeLeft.NestedCalc();
        }
        else {
            // どちらかがnullだったらFalse
            if (!inputOperatorNodeLeft || !inputOperatorNodeRight) {
                return false;
            }

            // 入れ子のNestedCalcメソッドを実行
            inputStringLeft = inputOperatorNodeLeft.NestedCalc();
            inputStringRight = inputOperatorNodeRight.NestedCalc();
        }

        // 文字列内の式を計算
        string calcString = GetCalcString();
        var expr = new Expression(calcString);
        bool result = (bool)expr.Evaluate();

        string logTxt = $"{calcString}\n" +
            $"{result}";

        Debug.Log(logTxt);

        return result;
    }

    public override string NestedCalc() {
        if (conjunctionOperatorType == ConjunctionOperatorType.Not) {
            // 左のスロットがnullだったらFalse
            if (!inputOperatorNodeLeft) {
                return "false";
            }

            // 入れ子のNestedCalcメソッドを実行
            inputStringLeft = inputOperatorNodeLeft.NestedCalc();
        }
        else {
            // どちらかがnullだったらFalse
            if (!inputOperatorNodeLeft || !inputOperatorNodeRight) {
                return "false";
            }

            // 入れ子のNestedCalcメソッドを実行
            inputStringLeft = inputOperatorNodeLeft.NestedCalc();
            inputStringRight = inputOperatorNodeRight.NestedCalc();
        }

        // 計算式を文字列で返す
        return GetCalcString();
    }

    public override void AttachNode(object node, InputSlot inputSlot) {
        switch (inputSlot) {
            case InputSlot.Left:
                inputOperatorNodeLeft = (TrueOrFalseNode)node;
                break;

            case InputSlot.Right:
                inputOperatorNodeRight = (TrueOrFalseNode)node;
                break;
        }
    }

    protected override string GetCalcString() {
        string calcString = "";
        switch (conjunctionOperatorType) {
            case ConjunctionOperatorType.And:
                calcString = $"{inputStringLeft} && {inputStringRight}";
                break;
            case ConjunctionOperatorType.Or:
                calcString = $"{inputStringLeft} || {inputStringRight}";
                break;
            case ConjunctionOperatorType.Not:
                calcString = $"!({inputStringLeft})";
                break;
        }
        return calcString;
    }
}
