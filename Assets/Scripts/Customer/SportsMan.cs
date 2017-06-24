using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

/// <summary>
/// スポーツマン
/// 肉好き
/// </summary>
public class SportsMan : Customer {

	/// <summary>
	/// 内容物の評価
	/// </summary>
	/// <returns>The evaluate.</returns>
	/// <param name="foods">Foods.</param>
	protected override float ContentsEvaluate(List<FoodType> foods) {
		return _order.GetProgressRatio(foods);
	}

	/// <summary>
	/// 見た目の評価
	/// </summary>
	/// <returns>The evaluate.</returns>
	/// <param name="offsets">Offsets.</param>
	protected override float VisualEvaluate(List<float> offsets) {
		//なるべく中心軸からブレてないように
		float sum = 0f;
		for(int i = 0; i < offsets.Count; ++i) {
			sum += Mathf.Abs(offsets[i]);
		}
		return sum / offsets.Count;
	}

	/// <summary>
	/// 特殊な評価
	/// </summary>
	/// <returns>The evaluate.</returns>
	/// <param name="burger">Burger.</param>
	protected override float SpecialEvaluate(BurgerData burger) {
		var foods = burger.foods;
		int meatCnt = 0;
		for(int i = 0; i < foods.Count; ++i) {
			if(foods[i] == FoodType.Meat) {
				meatCnt++;
			}
		}
		// Meatが必要以上な場合に加点
		return (meatCnt - _order.foodCnts[(int)FoodType.Meat]) * 0.2f;
	}
}