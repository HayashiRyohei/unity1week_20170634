using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectShooter : MonoBehaviour {

	public class ShotEvent : UnityEvent<float, FoodType> { }

	[SerializeField]
	float power = 1;
	private Vector3 direction = new Vector3(0, -1, 0);

	/*
	/// <summary>
	/// 落とす食べ物
	/// </summary>
	private GameObject shotFood = null;
	*/

	[SerializeField]
	private FoodList foodList;

	[SerializeField]
	private Transform burger;

	[SerializeField]
	private Reel reel;

	private ShotEvent _onShot;

	public ShotEvent onShot {
		get {
			return _onShot;
		}
	}

	#region mono

	void Awake() {
		_onShot = new ShotEvent();
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Space)) {
			Shot ();
		}
	}
	/*
	void OnTriggerEnter(Collider collider) {
		shotFood = collider.gameObject;
	}
	void OnTriggerExit(Collider collider) {
		shotFood = null;
	}
	*/
	#endregion

	#region private function
	/// <summary>
	/// 食材を飛ばす
	/// </summary>
	private void Shot() {
		/*
		if (shotFood != null) {
			GameObject _obj = (GameObject)Instantiate (foodList.GetFood(shotFood.name));
			_obj.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
			_obj.transform.localPosition = new Vector3 (shotFood.transform.position.x, this.gameObject.transform.position.y, 0);
			_obj.GetComponent<Rigidbody> ().AddForce (direction * power, ForceMode.Impulse);
			_obj.transform.SetParent (burger);
		}
		*/
		if (reel) {
			var food = reel.GetNearestFood ();
			float offset = food.position.x;
			Food foodObj = Instantiate(foodList.GetFood(food.name));
			foodObj.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
			foodObj.transform.localPosition = new Vector3 (offset, this.gameObject.transform.position.y, 0);
			foodObj.GetComponent<Rigidbody> ().AddForce (direction * power, ForceMode.Impulse);
			foodObj.transform.SetParent (burger);

			// イベント
			_onShot.Invoke(offset, foodObj.type);
		}
	}
	#endregion
}
