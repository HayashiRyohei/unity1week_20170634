using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 実際のハンバーガーのデータ
/// </summary>
public class BurgerData {

	private List<FoodType> _foods;
	private List<float> _offsets;

	public List<FoodType> foods {
		get {
			return _foods;
		}
	}
	public List<float> offsets {
		get {
			return _offsets;
		}
	}

	public BurgerData() {
		_foods = new List<FoodType>();
		_offsets = new List<float>();
	}

	/// <summary>
	/// 指定したインデックスの食材の種類とズレを取得する
	/// </summary>
	/// <returns>The <see cref="System.Boolean"/>.</returns>
	/// <param name="index">Index.</param>
	/// <param name="type">Type.</param>
	/// <param name="offset">Offset.</param>
	public bool GetAt(int index, out FoodType type, out float offset) {
		type = FoodType.None;
		offset = 0f;
		if (index < 0 || _foods.Count <= index) return false;

		type = _foods[index];
		offset = _offsets[index];
		return true;
	}

	/// <summary>
	/// 材料とずれを追加
	/// </summary>
	/// <param name="type">Type.</param>
	/// <param name="offset">Offset.</param>
	public void Add(FoodType type, float offset) {
		_foods.Add (type);
		_offsets.Add (offset);
	}
}