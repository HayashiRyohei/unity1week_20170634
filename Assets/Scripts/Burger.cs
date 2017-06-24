using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burger : MonoBehaviour {

	#region mono
	void Update() {
		// 出荷
		if (Input.GetKeyDown (KeyCode.Return)) {
			OfferOfBurger ();
		}
	}
	#endregion

	#region public function
	public void OfferOfBurger() {
		// 子オブジェクの状態を把握して提供する

		// 子オブジェクトを破棄する
		foreach ( Transform n in this.gameObject.transform ) {
			GameObject.Destroy(n.gameObject);
		}	
	}
	#endregion

}
