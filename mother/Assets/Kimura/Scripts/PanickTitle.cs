using UnityEngine;
using System.Collections;

public class PanickTitle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		AudioManager.Instance.PlayBGM(SoundBGMType.BGM001);

		// クリアフラグ初期化
		ResultBool.Instance.SetSuccessFlag(true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnButtonStart() {
		AudioManager.Instance.StopBGM ();
		AudioManager.Instance.PlaySE (SoundSEType.SE001_JINGLE06);
		Invoke ("NextGameScene", 3.0f);
	}

	public void OnButtonCredit() {
		AudioManager.Instance.StopBGM ();
		AudioManager.Instance.PlaySE (SoundSEType.SE001_JINGLE06);
		Invoke ("CallCredit", 3.0f);
	}

	public void OnButtonExit() {
		AudioManager.Instance.StopBGM ();
		AudioManager.Instance.PlaySE (SoundSEType.SE001_JINGLE06);
		Invoke ("C", 3.0f);
	}

	void NextGameScene() {
		Application.LoadLevel("GameScene");
	}

	void CallCredit() {
		Application.LoadLevel("CreditScene");
	}

	void CallExit() {
		Application.Quit();
	}
}
