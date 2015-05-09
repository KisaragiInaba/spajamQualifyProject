using UnityEngine;
using System.Collections;

public class ResultScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (ResultBool.Instance.IsSuccess()) {
			AudioManager.Instance.PlayBGM (SoundBGMType.BGM005);
		} else {
			AudioManager.Instance.PlayBGM (SoundBGMType.BGM006);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnButtonBackTitle() {
		Application.LoadLevel("TitleScene");
	}

	public void OnButtonRetry() {
		Application.LoadLevel("gameScene");
	}

}
