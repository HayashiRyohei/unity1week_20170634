using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// 注文
/// </summary>
[Serializable]
public class Order {

	private List<FoodType> _contents;
	private int[] _foodCnts;

	public List<FoodType> contents {
		get {
			return _contents;
		}
	}
	public int[] foodCnts {
		get {
			return _foodCnts;
		}
	}

	private Order(FoodType[] foods) {
		_contents = new List<FoodType>(foods);
		_foodCnts = new int[(int)FoodType.Length];
		for(int i = 0; i < _contents.Count; ++i) {
			_foodCnts[(int)_contents[i]]++;
		}
	}

	/// <summary>
	/// 指定した数のランダムな食材からなるオーダーを作成する
	/// </summary>
	/// <returns>The random.</returns>
	/// <param name="num">Number.</param>
	public static Order MakeRandom(int num) {
		var foods = new FoodType[num];
		for(int i = 0; i < foods.Length; ++i) {
			foods[i] = (FoodType)UnityEngine.Random.Range(0, (int)FoodType.Length);
		}
		return new Order(foods);
	}

	/// <summary>
	/// 指定した食材リストとオーダーへの達成率を返す(順不同)
	/// </summary>
	/// <returns>The match ratio.</returns>
	/// <param name="foods">Foods.</param>
	public float GetProgressRatio(List<FoodType> foods) {
		var reallyCnts = new int[_foodCnts.Length];
		int i;
		for(i = 0; i < foods.Count; ++i) {
			reallyCnts[(int)foods[i]]++;
		}
		float eval = 0;
		for(i = 0; i < reallyCnts.Length; ++i) {
			if(_foodCnts[i] > 0) {
				if(reallyCnts[i] >= (_foodCnts[i])) {
					eval += _foodCnts[i];
				} else {
					eval += reallyCnts[i];
				}
			}
		}
		return eval / _contents.Count;
	}
}