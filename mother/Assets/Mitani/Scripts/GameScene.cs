using UnityEngine;
using System.Collections;

public class GameScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		AudioManager.Instance.PlayBGM (SoundBGMType.BGM002_GAME);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
