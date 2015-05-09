using UnityEngine;
using System.Collections;

public class CreditScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		AudioManager.Instance.PlayBGM(SoundBGMType.BGM005);

		obstacleDestroy ("ResultBool");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnButtonTitle() {
		AudioManager.Instance.StopBGM ();
		AudioManager.Instance.PlaySE (SoundSEType.SE001_JINGLE06);
		Invoke ("ReturnTitle", 3.0f);
	}

	void ReturnTitle() {
		Application.LoadLevel("TitleScene");
	}

	void obstacleDestroy(string tagName) {
		GameObject[] obstacles = GameObject.FindGameObjectsWithTag(tagName);
		foreach (GameObject obs in obstacles) {
			Destroy (obs);
		}
	}
}
