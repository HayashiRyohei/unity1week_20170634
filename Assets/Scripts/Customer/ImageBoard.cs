using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// イメージボード
/// </summary>
public class ImageBoard : MonoBehaviour {

	[SerializeField]
	private Transform _burgerParent;
	[SerializeField]
	private Vector3 _interval;
	[SerializeField]
	private Vector3 _angles;
	[SerializeField]
	private FoodList _foodList;
	[SerializeField]
	private CustomerEmotion _emotion;

	/// <summary>
	/// バーガーの設定
	/// </summary>
	/// <param name="foods">Foods.</param>
	public void SetBurger(FoodType[] foods) {
		if (!(_burgerParent && _foodList)) return;
		foreach (Transform trans in _burgerParent) {
			Destroy(trans.gameObject);
		}
		for (int i = 0; i < foods.Length; ++i) {
			Debug.Log(foods[i]);
			var food = Instantiate(_foodList.GetFood(foods[i]), _interval * i, Quaternion.identity);
			food.transform.SetParent(_burgerParent, false);
			food.transform.localEulerAngles = _angles;
			food.GetComponent<Rigidbody>().isKinematic = true;
		}
	}
}