using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class FoodList : ScriptableObject {
	[SerializeField]
	private Food[] foods;

	#region public function
	public Food GetFood(string foodName) {
		for (int i = 0; i < foods.Length; ++i) {
			if (foods[i].name == foodName) {
				return foods[i];
			}
		}
		return null;
	}

	public Food GetFood(FoodType type) {
		for (int i = 0; i < foods.Length; ++i) {
			if (foods[i].type == type) {
				return foods[i];
			}
		}
		return null;
	}
	#endregion

}
