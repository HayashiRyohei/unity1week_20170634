using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

	[SerializeField]
	private FoodType _type;

	public FoodType type {
		get {
			return _type;
		}
	}

	#region mono
	void OnCollisionEnter(Collision coll) {
		if (coll.gameObject.tag == "Respawn") {
			this.gameObject.tag = "Respawn";
			this.gameObject.transform.SetParent (coll.gameObject.transform.parent);
		}

		if (coll.gameObject.tag == "Finish") {
			Destroy (this.gameObject);
		}
	}
	#endregion
}
