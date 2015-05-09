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
		Application.LoadLevel("ResultScene");
	}

	public void OnButtonCredit() {
		Application.LoadLevel("CreditScene");
	}

	public void OnButtonExit() {
		Application.Quit();
	}
}
