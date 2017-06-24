using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class FoodList : ScriptableObject {
	[SerializeField]
	private GameObject[] list;

	#region public function
	public GameObject GetFood(string foodName) {
		for (int i = 0; i < list.Length; ++i) {
			if (list[i].name == foodName) {
				return list [i];
			}
		}
		return null;
	}
	#endregion

}
