using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

/// <summary>
/// 女子高生。JK
/// 欲しいものと反対のものが欲しいツンデレ
/// 流行り物が好きなので見た目も普通のとは違うものが好き
/// </summary>
public class Joshikosei : Customer {

	private float _tCEval, _tVEval;

	/// <summary>
	/// 内容物の評価(0-1で評価)
	/// </summary>
	/// <returns>The evaluate.</returns>
	/// <param name="foods">Foods.</param>
	protected override float ContentsEvaluate(List<FoodType> foods) {
		// 一致していないほど評価が高い
		_tCEval = 1f - _order.GetProgressRatioIgnoreBuns(foods);
		return _tCEval;
	}

	/// <summary>
	/// 見た目の評価(0-1で評価)
	/// </summary>
	/// <returns>The evaluate.</returns>
	/// <param name="offsets">Offsets.</param>
	protected override float VisualEvaluate(List<float> offsets) {
		_tVEval = 1f - BasicVisualEvaluate(offsets);
		return _tVEval;
	}


	/// <summary>
	/// 特殊な評価(0-1で評価)
	/// </summary>
	/// <returns>The evaluate.</returns>
	/// <param name="burger">Burger.</param>
	protected override float SpecialEvaluate(BurgerData burger) {
		return (_tCEval + _tVEval) * 0.5f;
	}
}