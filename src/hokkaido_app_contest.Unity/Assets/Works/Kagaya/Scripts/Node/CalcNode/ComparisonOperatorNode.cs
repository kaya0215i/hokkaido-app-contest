using NCalc;
using System;
using UnityEngine;

/// <summary>
/// 比較演算ノード
/// </summary>
public class ComparisonOperatorNode : TrueOrFalseNode {
    public enum ComparisonOperatorType {
        Equal, // ==
        NotEqual, // !=
        GreaterThan, // >
        GreaterThanOrEqual, // >=
        LessThan, // <
        LessThanOrEqual, // <=
    }

    [SerializeField] private ComparisonOperatorType comparisonOperatorType;

    /// <summary>
    /// 入力値_左
    /// </summary>
    [SerializeField] private float inputLeft;
    /// <summary>
    /// 入力値_右
    /// </summary>
    [SerializeField] private float inputRight;

    /// <summary>
    /// InputCalcNode_左
    /// </summary>
    [SerializeField] private ArithmeticOperatorNode inputCalcNodeLeft;
    /// <summary>
    /// InputCalcNode_右
    /// </summary>
    [SerializeField] private ArithmeticOperatorNode inputCalcNodeRight;

    public override object Calc() {
        inputStringLeft = inputLeft.ToString();
        inputStringRight = inputRight.ToString();

        // inputCalcNodeLeft にインスタンスがあればそのNestedCalcメソッドを実行
        if (inputCalcNodeLeft) {
            inputStringLeft = inputCalcNodeLeft.NestedCalc();
        }

        // inputCalcNodeRight にインスタンスがあればそのNestedCalcメソッドを実行
        if (inputCalcNodeRight) {
            inputStringRight = inputCalcNodeRight.NestedCalc();
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
        inputStringLeft = inputLeft.ToString();
        inputStringRight = inputRight.ToString();

        // inputCalcNodeLeft にインスタンスがあればそのNestedCalcメソッドを実行
        if (inputCalcNodeLeft) {
            inputStringLeft = inputCalcNodeLeft.NestedCalc();
        }

        // inputCalcNodeRight にインスタンスがあればそのNestedCalcメソッドを実行
        if (inputCalcNodeRight) {
            inputStringRight = inputCalcNodeRight.NestedCalc();
        }

        // 計算式を文字列で返す
        return GetCalcString();
    }

    public override void AttachNode(object node, InputSlot inputSlot) {
        switch (inputSlot) {
            case InputSlot.Left:
                inputCalcNodeLeft = (ArithmeticOperatorNode)node;
                inputLeft = 0;
                break;

            case InputSlot.Right:
                inputCalcNodeRight = (ArithmeticOperatorNode)node;
                inputRight = 0;
                break;
        }
    }

    protected override string GetCalcString() {
        string calcString = "";
        switch (comparisonOperatorType) {
            case ComparisonOperatorType.Equal:
                calcString = $"{inputStringLeft} == {inputStringRight}";
                break;
            case ComparisonOperatorType.NotEqual:
                calcString = $"{inputStringLeft} != {inputStringRight}";
                break;
            case ComparisonOperatorType.GreaterThan:
                calcString = $"{inputStringLeft} > {inputStringRight}";
                break;
            case ComparisonOperatorType.GreaterThanOrEqual:
                calcString = $"{inputStringLeft} >= {inputStringRight}";
                break;
            case ComparisonOperatorType.LessThan:
                calcString = $"{inputStringLeft} < {inputStringRight}";
                break;
            case ComparisonOperatorType.LessThanOrEqual:
                calcString = $"{inputStringLeft} <= {inputStringRight}";
                break;
        }
        return calcString;
    }
}
