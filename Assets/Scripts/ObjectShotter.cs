using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShotter : MonoBehaviour {

	[SerializeField]
	float power = 1;
	[SerializeField]
	Vector3 direction = new Vector3(0, 0, 0);
	[SerializeField]
	GameObject obj;
	List<GameObject> foodList = new List<GameObject>();

	[SerializeField]
	Menu menu;

	#region mono
	void Update() {
		if (Input.GetKeyDown(KeyCode.Return)) {
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
			_obj.GetComponent<Food> ().Init (CollisionEnter);
			_obj.GetComponent<Rigidbody> ().AddForce (direction * power, ForceMode.Impulse);
			foodList.RemoveAt (0);
		}
	}

	/// <summary>
	/// 飛ばした食材がぶつかったときに呼ばれるコールバック
	/// </summary>
	private void CollisionEnter() {
		Debug.Log ("test");
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
