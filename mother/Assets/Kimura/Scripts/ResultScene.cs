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

		// boolオブジェクトの削除
		obstacleDestroy ("ResultBool");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnButtonBackTitle() {
		AudioManager.Instance.StopBGM ();
		AudioManager.Instance.PlaySE (SoundSEType.SE001_JINGLE06);
		Invoke ("ReturnTitle", 3.0f);
	}

	public void OnButtonRetry() {
		AudioManager.Instance.StopBGM ();
		AudioManager.Instance.PlaySE (SoundSEType.SE001_JINGLE06);
		Invoke ("RetryGame", 3.0f);
	}

	void ReturnTitle() {
		Application.LoadLevel("TitleScene");
	}

	void RetryGame() {
		Application.LoadLevel("gameScene");
	}

	void obstacleDestroy(string tagName) {
		GameObject[] obstacles = GameObject.FindGameObjectsWithTag(tagName);
		foreach (GameObject obs in obstacles) {
			Destroy (obs);
		}
	}
}
