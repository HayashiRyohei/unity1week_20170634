﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectShooter : MonoBehaviour {

	public class ShotEvent : UnityEvent<float, Food> { }

	[SerializeField]
	float power = 1;
	private Vector3 direction = new Vector3(0, -1, 0);
	[SerializeField, Range(0f, 3f)]
	private float maxOffset = 1f;
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
	private bool _controllable = false;

	public ShotEvent onShot {
		get {
			return _onShot;
		}
	}
	public bool controllable {
		get {
			return _controllable;
		}
		set {
			_controllable = value;
		}
	}

	#region mono

	void Awake() {
		_onShot = new ShotEvent();
	}

	void Update() {
		if (_controllable && Input.GetMouseButtonDown(0)) {
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
			float offset = Mathf.Clamp(food.position.x, -maxOffset, maxOffset);
			Food foodObj = Instantiate(foodList.GetFood(food.name));
			foodObj.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
			foodObj.transform.localPosition = new Vector3 (offset, this.gameObject.transform.position.y, 0);
			foodObj.GetComponent<Rigidbody> ().AddForce (direction * power, ForceMode.Impulse);
			foodObj.transform.SetParent (burger);
			AudioManager.Instance.PlaySE ("se_maoudamashii_retro08");
			// イベント
			_onShot.Invoke(offset, foodObj);
		}
	}
	#endregion
}
