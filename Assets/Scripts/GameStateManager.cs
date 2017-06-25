using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour {

	private static GameStateManager instance = null;
	public static GameStateManager Instance {
		get { return instance; }
	}
	/// <summary>
	/// ゲームの状態
	/// </summary>
	public enum State {
		START,
		GAME,
		RESULT,
	}
	/// <summary>
	/// ゲームの状態管理
	/// </summary>
	private State state = State.START;

	[SerializeField]
	private GameObject titleUIPrefub;
	[SerializeField]
	private GameObject resultUIPrefub;

	private GameObject titleUI;
	private GameObject resultUI;

	#region mono
	void Awake () {
		if (instance == null) {
			instance = this;
		} else {
			Destroy (this.gameObject);
		}
	}
	void Start() {
		titleUI = (GameObject)Instantiate (titleUIPrefub);
		resultUI = (GameObject)Instantiate (resultUIPrefub);
		resultUI.SetActive (false);
		StartCoroutine (GameStateControll ());
	}
	void Update() {
		if (Input.GetKeyDown (KeyCode.R)) {
			ChangeState (State.RESULT);
		}
	}
	#endregion

	#region public function
	public State GetState() {
		return state;
	}
	public void ChangeState(State nextState) {
		state = nextState;
	}
	#endregion

	private IEnumerator GameStateControll() {
		var _start = new WaitUntil (() => 
			state == State.START
		);

		var _game = new WaitUntil (() => 
			state == State.GAME
		);

		var _result = new WaitUntil (() => 
			state == State.RESULT
		);
		while (true) {
			yield return _start;
			resultUI.SetActive (false);
			titleUI.SetActive (true);

			yield return _game;
			titleUI.SetActive (false);
			resultUI.SetActive (false);

			yield return _result;
			titleUI.SetActive (false);
			resultUI.SetActive (true);
		}
	}
}