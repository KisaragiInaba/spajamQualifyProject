using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PanickTitle : MonoBehaviour {

	public float delayTime = 0.6f;

	// デバッグ用
	// デバッグ時はprivate を publicにする
	private string nextScene1 = "gameScene";

	// Use this for initialization
	void Start () {
		AudioManager.Instance.PlayBGM(SoundBGMType.BGM001_TITLE);

		// クリアフラグ初期化
		ResultBool.Instance.SetSuccessFlag(true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnButtonStart() {
		AudioManager.Instance.StopBGM ();
		StartCoroutine (NextScene (delayTime, nextScene1));
	}

	public void OnButtonCredit() {
		AudioManager.Instance.StopBGM ();
		StartCoroutine (NextScene (delayTime, "creditScene"));
	}

	public void OnButtonExit() {
		AudioManager.Instance.StopBGM ();
		AudioManager.Instance.PlaySE (SoundSEType.SE001_PUSH);
		Invoke ("CallExit", delayTime);
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
