using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using NCalc;
using System;

public class ArithmeticOperatorNode : CalculationNode {
    public enum CalcType {
        add, // +
        subtract, // -
        multiply, // *
        divide, // /
    }

    [SerializeField] private CalcType calcType;

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

    /// <summary>
    /// ()つけるか
    /// </summary>
    [SerializeField] private bool useParentheses = false;

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
        float result = Convert.ToSingle(expr.Evaluate());

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
        switch (calcType) {
            case CalcType.add:
                calcString = $"{inputStringLeft} + {inputStringRight}";
                break;

            case CalcType.subtract:
                calcString = $"{inputStringLeft} - {inputStringRight}";
                break;

            case CalcType.multiply:
                calcString = $"{inputStringLeft} * {inputStringRight}";
                break;

            case CalcType.divide:
                calcString =  $"{inputStringLeft} / {inputStringRight}";
                break;
        }

        if (useParentheses) {
            calcString = $"({calcString})";
        }
        return calcString;
    }
}
