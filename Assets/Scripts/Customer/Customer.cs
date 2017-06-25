using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 客
/// </summary>
public abstract class Customer : MonoBehaviour {

	[SerializeField]
	protected FoodType[] _orderfoods;
	[SerializeField, Range(3, 10)]
	protected int _randomFoodCnt = 3;

	[Header("Evaluate Parameter")]
	[SerializeField, Range(0f, 3f)]
	protected float _maxOffset = 0.3f;	//許容できるズレの最大幅

	protected Order _order;

	public int orderFoodCnt {
		get {
			return _orderfoods.Length;
		}
	}
	public int randomFoodCnt {
		get {
			return _randomFoodCnt;
		}
	}
	public Order order {
		get {
			return _order;
		}
	}

	private void Awake() {
		if(_orderfoods.Length > 0) {
			_order = Order.MakeFromArray(_orderfoods, true);
		} else {
			_order = Order.MakeRandom(_randomFoodCnt);
		}
	}

	/// <summary>
	/// 内容物の評価(0-1で評価)
	/// </summary>
	/// <returns>The evaluate.</returns>
	/// <param name="foods">Foods.</param>
	protected abstract float ContentsEvaluate(List<FoodType> foods);

	/// <summary>
	/// 見た目の評価(0-1で評価)
	/// </summary>
	/// <returns>The evaluate.</returns>
	/// <param name="offsets">Offsets.</param>
	protected abstract float VisualEvaluate(List<float> offsets);

	/// <summary>
	/// 特殊な評価(0-1で評価)
	/// </summary>
	/// <returns>The evaluate.</returns>
	/// <param name="burger">Burger.</param>
	protected abstract float SpecialEvaluate(BurgerData burger);

	/// <summary>
	/// 標準的なずれの評価。(ベースからズレの平均値)
	/// </summary>
	/// <returns>The visual evaluate.</returns>
	/// <param name="offsets">Offsets.</param>
	protected float BasicVisualEvaluate(List<float> offsets) {
		float sum = 0f;
		float baseOffset = offsets[0];
		for (int i = 1; i < offsets.Count; ++i) {
			sum += Mathf.Min(Mathf.Abs(baseOffset - offsets[i]), _maxOffset);
		}
		return ((_maxOffset + 0.01f) - (sum / offsets.Count)) / _maxOffset;
	}

	/// <summary>
	/// ハンバーガーの評価を行い、ランクを返す
	/// </summary>
	/// <returns>The evaluate.</returns>
	/// <param name="burger">Burger.</param>
	public int Evaluate(BurgerData burger) {
		float cEval = ContentsEvaluate(burger.foods);
		float vEval = VisualEvaluate(burger.offsets);
		float sEval = SpecialEvaluate(burger);
		float sumEval = cEval + vEval + sEval;
		Debug.Log(string.Format("cEval: {0}, vEval: {1}, sEval: {2}, sumEval: {3}", cEval, vEval, sEval, sumEval));

		//とりあえず決め打ちで
		int rank = 0;
		if (sumEval > 2.5) {
			rank = 3;
		} else if (sumEval > 1.8) {
			rank = 2;
		} else if (sumEval > 1.2) {
			rank = 1;
		}

		return rank;
	}
}