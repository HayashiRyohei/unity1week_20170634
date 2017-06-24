using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Food : MonoBehaviour {

	Action collisionDel = null;

	#region mono
	void OnCollisionEnter(Collision coll) {
		if (collisionDel != null) {
			collisionDel ();
		}
	}
	#endregion

	#region public function
	public void Init(Action collDel) {
		collisionDel = collDel;
	}
	#endregion

}
