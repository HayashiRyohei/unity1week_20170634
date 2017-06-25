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
	[SerializeField]
	private GameObject gameUI;
	private ObjectShooter objectShooter;

	#region mono
	void Awake () {
		if (instance == null) {
			instance = this;
		} else {
			Destroy (this.gameObject);
		}
	}
	void Start() {
		AudioManager.Instance.PlayBGM ("game_maoudamashii_5_town16");
		titleUI = (GameObject)Instantiate (titleUIPrefub);
		resultUI = (GameObject)Instantiate (resultUIPrefub);
		resultUI.SetActive (false);
		gameUI.SetActive (false);

		objectShooter = GameObject.Find ("launcher").GetComponent<ObjectShooter> ();

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
			Camera.main.GetComponent<Gauss> ().Blur ();
			resultUI.SetActive (false);
			gameUI.SetActive (false);
			titleUI.SetActive (true);
			objectShooter.controllable = false;

			yield return _game;
			Camera.main.GetComponent<Gauss> ().UnBlur ();
			titleUI.SetActive (false);
			resultUI.SetActive (false);
			gameUI.SetActive (true);
			objectShooter.controllable = true;

			yield return _result;
			AudioManager.Instance.PlayBGM ("game_maoudamashii_9_jingle05");
			Camera.main.GetComponent<Gauss> ().Blur ();
			titleUI.SetActive (false);
			gameUI.SetActive (false);
			resultUI.SetActive (true);
			objectShooter.controllable = false;
		}
	}
}