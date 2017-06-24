using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShotter : MonoBehaviour {

	[SerializeField]
	float power = 1;

	private Vector3 direction = new Vector3(0, -1, 0);
	List<GameObject> foodList = new List<GameObject>();

	[SerializeField]
	Menu menu;

	#region mono
	void Update() {
		if (Input.GetKeyDown(KeyCode.Space)) {
			Shot ();
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			SetOrder (this.menu);
		}
	}
	#endregion

	#region private function
	/// <summary>
	/// 食材を飛ばす
	/// </summary>
	private void Shot() {
		if (foodList.Count > 0) {
			GameObject _obj = (GameObject)Instantiate (foodList [0]);
			_obj.transform.position = this.gameObject.transform.position;
			_obj.GetComponent<Rigidbody> ().AddForce (direction * power, ForceMode.Impulse);
			foodList.RemoveAt (0);
		}
	}
	#endregion

	#region public function
	/// <summary>
	/// メニューをセットする
	/// </summary>
	/// <param name="menu">次のオーダー.</param>
	public void SetOrder(Menu menu) {
		foodList.Clear ();
		foodList = new List<GameObject> (menu.foodList);
	}
	#endregion

}
