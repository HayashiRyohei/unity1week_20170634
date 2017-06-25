using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AudioManager : MonoBehaviour {

	private static AudioManager instance;
	public static AudioManager Instance {
		get { return instance; }
	}

	[SerializeField]
	private AudioList bgmList;
	[SerializeField]
	private AudioList seList;

	private Dictionary<string, AudioClip> bgmDict = new Dictionary<string, AudioClip>();
	private Dictionary<string, AudioClip> seDict = new Dictionary<string, AudioClip>();

	private AudioSource bgmSource;
	private List<AudioSource> seSources = new List<AudioSource>();

	[SerializeField]
	private int maxSENum = 10;

	private float masterBGMVolume = 1f;
	private float masterSEVolume = 1f;

	#region mono
	void Awake() {
		if (instance == null) {
			instance = this;
		} else {
			Destroy (this.gameObject);
		}
	}
	void Start() {
		InitBGM ();
		InitSE ();
	}
	#endregion

	#region private function
	/// <summary>
	/// 初期化
	/// </summary>
	private void InitBGM() {
		for (int i = 0; i < bgmList.audioList.Length; ++i) {
			bgmDict.Add (bgmList.audioList [i].name, bgmList.audioList [i]);
		}
		this.bgmSource = this.gameObject.AddComponent<AudioSource>();
		this.bgmSource.loop = true;
	}
	private void InitSE() {
		for (int i = 0; i < seList.audioList.Length; ++i) {
			seDict.Add (seList.audioList [i].name, seList.audioList [i]);
		}
	}
	#endregion

	#region public function
	public void PlayBGM(string bgmName) {
		PlayBGM (bgmName, 1f);
	}
	public void PlayBGM(string bgmName, float volume) {
		if (bgmDict.ContainsKey(bgmName)) {
			StopBGM();
			this.bgmSource.clip = bgmDict [bgmName];
			this.bgmSource.volume = volume * masterBGMVolume;
			this.bgmSource.Play ();
		}
	}
	public void StopBGM() {
		this.bgmSource.Stop ();
		this.bgmSource.clip = null;
	}
	public void PlaySE(string seName) {
		PlaySE (seName, 1f);
	}
	public void PlaySE(string seName, float volume) {
		if (seDict.ContainsKey(seName)) {
			AudioSource _source = this.seSources.FirstOrDefault (s => !s.isPlaying);
			if (_source == null) {
				if (this.seSources.Count >= maxSENum) {
					return;
				}
				_source = this.gameObject.AddComponent<AudioSource> ();
				this.seSources.Add (_source);
			}
			_source.clip = this.seDict [seName];
			_source.volume = volume * masterSEVolume;
			_source.Play ();
		}
	}
	#endregion

}
