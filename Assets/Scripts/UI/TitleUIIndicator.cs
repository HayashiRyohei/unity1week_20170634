using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUIIndicator : MonoBehaviour {

	[SerializeField]
	private Button startBtn;

	void Start () {
		startBtn.onClick.AddListener (() => {
			GameStateManager.Instance.ChangeState(GameStateManager.State.GAME);
		});
	}	
}
