using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 客
/// </summary>
public abstract class Customer : MonoBehaviour {

	[SerializeField, Range(3, 10)]
	protected int _foodCnt = 3;

	[Header("UI")]
	[SerializeField]
	private Text _text;

	protected Order _order;

	public int foodCnt {
		get {
			return _foodCnt;
		}
	}

	private void Awake() {
		_order = Order.MakeRandom(_foodCnt);
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
		if(sumEval > 1.2) {
			rank = 3;
		} else if(sumEval > 0.9) {
			rank = 2;
		} else if(sumEval > 0.5) {
			rank = 1;
		}

		return rank;
	}

	/// <summary>
	/// 内容物の評価
	/// </summary>
	/// <returns>The evaluate.</returns>
	/// <param name="foods">Foods.</param>
	protected abstract float ContentsEvaluate(List<FoodType> foods);

	/// <summary>
	/// 見た目の評価
	/// </summary>
	/// <returns>The evaluate.</returns>
	/// <param name="offsets">Offsets.</param>
	protected abstract float VisualEvaluate(List<float> offsets);

	/// <summary>
	/// 特殊な評価
	/// </summary>
	/// <returns>The evaluate.</returns>
	/// <param name="burger">Burger.</param>
	protected abstract float SpecialEvaluate(BurgerData burger);
}