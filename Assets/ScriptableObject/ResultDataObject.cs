using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultDataObject : ScriptableObject {

	public List<int> resultList = new List<int>();

	public void Init() {
		resultList.Clear ();
	}
}
