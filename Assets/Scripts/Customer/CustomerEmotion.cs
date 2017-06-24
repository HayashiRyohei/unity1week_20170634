using UnityEngine;
using System.Collections;

/// <summary>
/// 客の感情表現
/// </summary>
public class CustomerEmotion : MonoBehaviour {

	[Header("Emotions")]
	[SerializeField]
	private GameObject[] _emotions;

	private int _currentIndex = -1;
	private GameObject _currentEmotion;

	/// <summary>
	/// 感情の表示
	/// </summary>
	/// <param name="index">Index.</param>
	public void ShowEmotion(int index) {
		if(index < 0 || _emotions.Length <= index) return;
		if(_currentIndex == index) return;
		if(_currentEmotion) {
			Destroy(_currentEmotion);
		}
		var emoObj = Instantiate(_emotions[index], new Vector3(0f, 0f, 0f), Quaternion.identity);
		emoObj.transform.SetParent(transform, false);
		_currentIndex = index;
		_currentEmotion = emoObj;
	}
}