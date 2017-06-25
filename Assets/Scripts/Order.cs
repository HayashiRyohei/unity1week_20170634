using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// 注文
/// </summary>
[Serializable]
public class Order {

	private FoodType[] _contents;
	private int[] _foodCnts;

	public FoodType[] contents {
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
		_contents = foods;
		_foodCnts = new int[(int)FoodType.Length];
		for(int i = 0; i < _contents.Length; ++i) {
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
	/// 指定した配列データからオーダーを作成。第二引数は上下にバンズを追加するか
	/// </summary>
	/// <returns>The from array.</returns>
	/// <param name="src">Foods.</param>
	public static Order MakeFromArray(FoodType[] src, bool addBuns) {
		FoodType[] foods;
		if(addBuns) {
			foods = new FoodType[src.Length + 2];
			foods[0] = FoodType.BunsBottom;
			for(int i = 0, j = 1; i < src.Length; ++i, ++j) {
				foods[j] = src[i];
			}
			foods[foods.Length - 1] = FoodType.BunsTop;
		} else {
			foods = new FoodType[src.Length];
			src.CopyTo(foods, 0);
		}
		return new Order(foods);
	}

	/// <summary>
	/// 指定した食材リストとオーダーへの達成率を返す
	/// </summary>
	/// <returns>The match ratio.</returns>
	/// <param name="foods">Foods.</param>
	public float GetProgressRatio(List<FoodType> foods) {
		var conts = new int[_foodCnts.Length];
		int i;
		for(i = 0; i < foods.Count; ++i) {
			conts[(int)foods[i]]++;
		}
		float eval = 0;
		for(i = 0; i < conts.Length; ++i) {
			if(_foodCnts[i] > 0) {
				if(conts[i] >= (_foodCnts[i])) {
					eval += _foodCnts[i];
				} else {
					eval += conts[i];
				}
			}
		}
		return eval / _contents.Length;
	}

	/// <summary>
	/// バンズ以外の食材リストとオーダーへの達成率
	/// </summary>
	/// <returns>The match ratio ignore buns.</returns>
	/// <param name="foods">Foods.</param>
	public float GetProgressRatioIgnoreBuns(List<FoodType> foods) {
		var cnts = new int[_foodCnts.Length];
		int i;
		for (i = 0; i < foods.Count; ++i) {
			cnts[(int)foods[i]]++;
		}
		float eval = 0;
		for (i = 0; i < cnts.Length; ++i) {
			if (i == (int)FoodType.BunsBottom || i == (int)FoodType.BunsTop) continue;
			if (_foodCnts[i] > 0) {
				if (cnts[i] >= (_foodCnts[i])) {
					eval += _foodCnts[i];
				} else {
					eval += cnts[i];
				}
			}
		}
		return eval / _contents.Length - 2;
	}

	/// <summary>
	/// 指定した食材リストとオーダーの一致率を確認する
	/// </summary>
	/// <returns><c>true</c>, if match was checked, <c>false</c> otherwise.</returns>
	/// <param name="foods">Foods.</param>
	public float GetMatchRatio(List<FoodType> foods) {
		int minIndex = Mathf.Min(foods.Count, _contents.Length);
		int match = 0;
		for(int i = 0; i < minIndex; ++i) {
			if(foods[i] == _contents[i]) {
				match++;
			}
		}
		return (float)match / minIndex;
	}
}