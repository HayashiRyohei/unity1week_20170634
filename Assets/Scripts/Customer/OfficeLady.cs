using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

/// <summary>
/// OL
/// 野菜好き
/// </summary>
public class OfficeLady : Customer {

	[Header("OfficeLady Evaluate")]
	[SerializeField, Range(0f, 1f)]
	private float _vegetableAdd = 0.2f;	//オーダー以上のレタスorトマトが入っていた場合の加点

	/// <summary>
	/// 内容物の評価(0-1で評価)
	/// </summary>
	/// <returns>The evaluate.</returns>
	/// <param name="foods">Foods.</param>
	protected override float ContentsEvaluate(List<FoodType> foods) {
		return _order.GetProgressRatio(foods);
	}

	/// <summary>
	/// 見た目の評価(0-1で評価)
	/// </summary>
	/// <returns>The evaluate.</returns>
	/// <param name="offsets">Offsets.</param>
	protected override float VisualEvaluate(List<float> offsets) {
		return BasicVisualEvaluate(offsets);
	}

	/// <summary>
	/// 特殊な評価(0-1で評価)
	/// </summary>
	/// <returns>The evaluate.</returns>
	/// <param name="burger">Burger.</param>
	protected override float SpecialEvaluate(BurgerData burger) {
		var foods = burger.foods;
		int vegeCnt = 0;
		for (int i = 0; i < foods.Count; ++i) {
			if (foods[i] == FoodType.Lettuce || foods[i] == FoodType.Tomate) {
				vegeCnt++;
			}
		}
		// Meatが必要以上な場合に1を越さない範囲で
		return Mathf.Min(1f, (vegeCnt - _order.foodCnts[(int)FoodType.Meat]) * _vegetableAdd);
	}
}