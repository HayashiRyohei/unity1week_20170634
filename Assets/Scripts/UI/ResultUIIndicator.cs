using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultUIIndicator : MonoBehaviour {

	[SerializeField]
	private Button endBtn;

	[SerializeField]
	private GameObject[] foods;
	[SerializeField]
	private GameObject buns_bottom;
	[SerializeField]
	private GameObject buns_top;

	[SerializeField]
	private ResultDataObject resultData;

	#region mono
	void Start () {
		endBtn.gameObject.SetActive (false);
		endBtn.onClick.AddListener (() => {
//			GameStateManager.Instance.ChangeState (GameStateManager.State.START);
			AudioManager.Instance.PlaySE("se_maoudamashii_system24");
			SceneManager.LoadScene(0);
		});
			
		StartCoroutine (FoodDrop ());
	}
	#endregion

	#region private function
	private IEnumerator FoodDrop() {
		var _wait = new WaitForSeconds (0.5f);
		int _count = 0;
		Time.timeScale = 1;
		GameObject _obj = (GameObject) Instantiate(buns_bottom);
		_obj.layer = LayerMask.NameToLayer("TransparentFX");
		_obj.transform.localPosition = new Vector3 (0, 1010, 0);
		_obj.transform.localScale = new Vector3 (3, 3, 3);
		yield return _wait;
		while (_count < resultData.resultList.Count) {
			_obj = (GameObject) Instantiate(foods[resultData.resultList[_count]]);
			_obj.layer = LayerMask.NameToLayer("TransparentFX");
			_obj.transform.localPosition = new Vector3 (0, 1010, 0);
			_obj.transform.localScale = new Vector3 (3, 3, 3);
			_count++;
			yield return _wait;
		}
		yield return _wait;
		_obj = (GameObject) Instantiate(buns_top);
		_obj.layer = LayerMask.NameToLayer("TransparentFX");
		_obj.transform.localPosition = new Vector3 (0, 1010, 0);
		_obj.transform.localScale = new Vector3 (3, 3, 3);

		yield return _wait;
		yield return _wait;
		endBtn.gameObject.SetActive (true);
	}
	#endregion

	// 満足度は4段階(0 ~ 3)
}
