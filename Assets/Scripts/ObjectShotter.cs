using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShotter : MonoBehaviour {

	[SerializeField]
	float power = 1;
	private Vector3 direction = new Vector3(0, -1, 0);

	/// <summary>
	/// 落とす食べ物
	/// </summary>
	private GameObject shotFood = null;

	[SerializeField]
	private FoodList foodList;

	[SerializeField]
	private Transform burger;

	#region mono
	void Update() {
		if (Input.GetKeyDown(KeyCode.Space)) {
			Shot ();
		}
	}
	void OnTriggerEnter(Collider collider) {
		shotFood = collider.gameObject;
	}
	void OnTriggerExit(Collider collider) {
		shotFood = null;
	}
	#endregion

	#region private function
	/// <summary>
	/// 食材を飛ばす
	/// </summary>
	private void Shot() {
		if (shotFood != null) {
			GameObject _obj = (GameObject)Instantiate (foodList.GetFood(shotFood.name));
			_obj.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
			_obj.transform.localPosition = new Vector3 (shotFood.transform.position.x, this.gameObject.transform.position.y, 0);
			_obj.GetComponent<Rigidbody> ().AddForce (direction * power, ForceMode.Impulse);
			_obj.transform.SetParent (burger);
		}
	}
	#endregion
}
