using UnityEngine;
using System.Collections;

public class CreditScene : MonoBehaviour {

	public float delayTime = 0.6f;

	// Use this for initialization
	void Start () {
		AudioManager.Instance.PlayBGM(SoundBGMType.BGM005_RESULT);

		obstacleDestroy ("ResultBool");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnButtonTitle() {
		AudioManager.Instance.StopBGM ();
		AudioManager.Instance.PlaySE (SoundSEType.SE001_PUSH);
		FadeManager.Instance.LoadLevel ("TitleScene", delayTime);
	}

	void obstacleDestroy(string tagName) {
		GameObject[] obstacles = GameObject.FindGameObjectsWithTag(tagName);
		foreach (GameObject obs in obstacles) {
			Destroy (obs);
		}
	}
}
