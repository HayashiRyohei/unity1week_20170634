using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

/// <summary>
/// オタク
/// オーダー通りじゃないと死んじゃう
/// </summary>
public class Geek : Customer {

	[Header("Geek Evaluate")]
	[SerializeField, Range(0f, 3f)]
	private float _matchAdd = 1f;		//完全にオーダーと一致していた場合の加点

	/// <summary>
	/// 内容物の評価
	/// </summary>
	/// <returns>The evaluate.</returns>
	/// <param name="foods">Foods.</param>
	protected override float ContentsEvaluate(List<FoodType> foods) {
		return _order.GetMatchRatio(foods);
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
	/// 特殊な評価
	/// </summary>
	/// <returns>The evaluate.</returns>
	/// <param name="burger">Burger.</param>
	protected override float SpecialEvaluate(BurgerData burger) {
		// 完全に一致していたら無条件で評価値1
		var ratio = _order.GetMatchRatio(burger.foods);
		return ratio > 0.99f ? 1f: 0f;
	}
}