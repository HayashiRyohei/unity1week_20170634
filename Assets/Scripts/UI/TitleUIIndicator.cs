using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUIIndicator : MonoBehaviour {

	[SerializeField]
	private Button startBtn;

	[SerializeField]
	private Animator burgerAnim;

	void Start () {
		startBtn.onClick.AddListener (() => {
			AudioManager.Instance.PlaySE("se_maoudamashii_system24");
			GameStateManager.Instance.ChangeState(GameStateManager.State.GAME);
		});
		burgerAnim.updateMode = AnimatorUpdateMode.UnscaledTime;
	}	
}
