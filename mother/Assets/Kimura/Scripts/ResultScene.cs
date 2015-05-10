using UnityEngine;
using System.Collections;

public class ResultScene : MonoBehaviour {

	public float delayTime = 0.6f;

	// Use this for initialization
	void Start () {
		if (ResultBool.Instance.IsSuccess()) {
			AudioManager.Instance.PlayBGM (SoundBGMType.BGM005_RESULT);
		} else {
			AudioManager.Instance.PlayBGM (SoundBGMType.BGM006_RESULT);
		}

		// boolオブジェクトの削除
		obstacleDestroy ("ResultBool");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnButtonBackTitle() {
		AudioManager.Instance.StopBGM ();
		StartCoroutine (NextScene (delayTime, "TitleScene"));
	}

	public void OnButtonRetry() {
		AudioManager.Instance.StopBGM ();
		StartCoroutine (NextScene (delayTime, "GameScene"));
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

	void obstacleDestroy(string tagName) {
		GameObject[] obstacles = GameObject.FindGameObjectsWithTag(tagName);
		foreach (GameObject obs in obstacles) {
			Destroy (obs);
		}
	}
}
