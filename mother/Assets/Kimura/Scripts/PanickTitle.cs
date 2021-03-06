﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PanickTitle : MonoBehaviour {

	public Canvas canvas;

	public float delayTime = 0.6f;
	public float delayExitTime = 0.3f;

	// Use this for initialization
	void Start () {
		AudioManager.Instance.PlayBGM(SoundBGMType.BGM001_TITLE);

		InitButton ();
	}

	void InitButton() {
		// キャンバスのボタン関連
		foreach (Transform child in canvas.transform) {
			switch (child.name) {
			case "GameButton":
			case "CreditButton":
			case "ExitButton":
				Button button1 = child.gameObject.GetComponent<Button> ();
				button1.gameObject.SetActive (false);
				break;
			default:
				break;
			}
		}
	}

	public void OnButtonStart() {
		AudioManager.Instance.PlaySE (SoundSEType.SE001_PUSH);
		foreach (Transform child in canvas.transform) {
			switch (child.name) {
			case "GameButton":
			case "CreditButton":
			case "ExitButton":
				Button button1 = child.gameObject.GetComponent<Button> ();
				button1.gameObject.SetActive (true);
				break;
			case "StartButton":
				Button button2 = child.gameObject.GetComponent<Button> ();
				button2.gameObject.SetActive (false);
				break;
			default:
				break;
			}
		}
	}

	public void OnButtonGame() {
		AudioManager.Instance.StopBGM ();
		StartCoroutine (NextScene (delayTime, "GameScene"));
	}

	public void OnButtonCredit() {
		AudioManager.Instance.StopBGM ();
		StartCoroutine (NextScene (delayTime, "CreditScene"));
	}

	public void OnButtonExit() {
		AudioManager.Instance.StopBGM ();
		AudioManager.Instance.PlaySE (SoundSEType.SE001_PUSH);
		Invoke ("CallExit", delayExitTime);
	}

	IEnumerator NextScene(float delay, string nextScene) {

		// FadeIn
		FadeManager.Instance.LoadLevel (nextScene, delay);

		// SEを鳴らす
		AudioManager.Instance.PlaySE (SoundSEType.SE001_PUSH);

		yield return new WaitForSeconds (delay);
		// シーン遷移
		//Application.LoadLevel(nextScene);
	}

	void CallExit() {
		Application.Quit();
	}
}
