using UnityEngine;

public abstract class CalculationNode : BaseNode {
    public enum InputSlot {
        Left,
        Right,
    }

    protected string inputStringLeft;
    protected string inputStringRight;

    /// <summary>
    /// 計算
    /// </summary>
    public abstract object Calc();

    /// <summary>
    /// 入れ子の計算式を文字列で返す
    /// </summary>
    public abstract string NestedCalc();

    /// <summary>
    /// Nodeセット
    /// </summary>
    public abstract void AttachNode(object node, InputSlot inputSlot);

    /// <summary>
    /// 計算式を文字列で取得
    /// </summary>
    protected abstract string GetCalcString();
}
