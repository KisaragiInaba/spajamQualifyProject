using UnityEngine;
using System.Collections;

public class PanickTitle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		AudioManager.Instance.PlayBGM (SoundBGMType.BGM001);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnButtonStart() {
		AudioManager.Instance.PlaySE (SoundSEType.SE001_JINGLE06);
		Application.LoadLevel("GameScene");
	}

	public void OnButtonCredit() {
		Application.LoadLevel("CreditScene");
	}

	public void OnButtonExit() {
		Application.Quit();
	}

	public void OnButtonTitle() {
		Application.LoadLevel("TitleScene");
	}
}
