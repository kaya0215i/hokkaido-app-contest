using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using NCalc;
using System;

public class CalculationNode : BaseNode {
    public enum CalcType {
        add,
        subtract,
        multiply,
        divide,
    }

    [SerializeField] private CalcType calcType;

    public enum InputSlot {
        Left,
        Right,
    }

    /// <summary>
    /// 入力値_左
    /// </summary>
    [SerializeField] protected float inputLeft;
    /// <summary>
    /// 入力値_右
    /// </summary>
    [SerializeField] protected float inputRight;

    /// <summary>
    /// CalcNode_左
    /// </summary>
    [SerializeField] protected CalculationNode inputCalcNodeLeft;
    /// <summary>
    /// CalcNode_右
    /// </summary>
    [SerializeField] protected CalculationNode inputCalcNodeRight;

    [SerializeField] private bool useParentheses = false;

    protected string inputStringLeft;
    protected string inputStringRight;

    public float Calc() {
        inputStringLeft = inputLeft.ToString();
        inputStringRight = inputRight.ToString();

        // inputCalcNodeLeft にインスタンスがあればそのCalcChildメソッドを実行
        if (inputCalcNodeLeft) {
            inputStringLeft = inputCalcNodeLeft.CalcChild();
        }

        // inputCalcNodeRight にインスタンスがあればそのCalcChildメソッドを実行
        if (inputCalcNodeRight) {
            inputStringRight = inputCalcNodeRight.CalcChild();
        }

        // 文字列内の式を計算
        string calcString = GetCalcString();
        var expr = new NCalc.Expression(calcString);
        float result = Convert.ToSingle(expr.Evaluate());

        string logTxt = $"{calcString}\n" +
            $"{result}";

        Debug.Log(logTxt);

        return result;
    }

    public string CalcChild() {
        inputStringLeft = inputLeft.ToString();
        inputStringRight = inputRight.ToString();

        // inputCalcNodeLeft にインスタンスがあればそのCalcChildメソッドを実行
        if (inputCalcNodeLeft) {
            inputStringLeft = inputCalcNodeLeft.CalcChild();
        }

        // inputCalcNodeRight にインスタンスがあればそのCalcChildメソッドを実行
        if (inputCalcNodeRight) {
            inputStringRight = inputCalcNodeRight.CalcChild();
        }

        // 計算式を文字列で返す
        return GetCalcString();
    }

    public void AttachCalcNode(CalculationNode calcNode, InputSlot inputSlot) {
        switch (inputSlot) {
            case InputSlot.Left:
                inputCalcNodeLeft = calcNode;
                inputLeft = 0;
                break;

            case InputSlot.Right:
                inputCalcNodeRight = calcNode;
                inputRight = 0;
                break;
        }
    }

    /// <summary>
    /// 計算式を文字列で取得
    /// </summary>
    private string GetCalcString() {
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

    public override UniTask Execute(Transform target, CancellationToken ct) {
        return UniTask.CompletedTask;
    }
}
