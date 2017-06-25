using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultUIIndicator : MonoBehaviour {

	[SerializeField]
	private Button endBtn;

	void Start () {
		endBtn.onClick.AddListener (() => {
			GameStateManager.Instance.ChangeState(GameStateManager.State.START);
		});
	}	
}
