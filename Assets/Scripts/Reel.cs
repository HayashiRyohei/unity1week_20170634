﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reel : MonoBehaviour {
	
	[SerializeField]
	private Transform[] foods;
	private Vector3[] initPos;
	[SerializeField]
	private Vector3 startPos = new Vector3 (-3, 0, 0);
	[SerializeField]
	private Vector3 targetPos = new Vector3 (3, 0, 0);
	[SerializeField, Header("流れる速さ")]
	private float waitTime = 1;
	[SerializeField, Header("次の食材が流れるまでの時間")]
	private float interval = 0.5f;

	void Start() {
		StartCoroutine (AllMove ());
	}

	IEnumerator AllMove() {
		var wait = new WaitForSeconds (interval);
		while (true) {
			for (int i = 0; i < foods.Length; ++i) {
				StartCoroutine (Move (foods [i]));
				yield return wait;
			}
		}
	}

	IEnumerator Move(Transform tran) {
		float _time = 0;
		while (_time < waitTime) {
			_time += Time.deltaTime;
			tran.localPosition = Vector3.Lerp (startPos, targetPos, _time / waitTime);
			yield return null;
		}
	}


}
