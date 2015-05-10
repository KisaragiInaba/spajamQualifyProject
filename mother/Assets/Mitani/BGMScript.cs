using UnityEngine;
using System.Collections;

public class BGMScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        AudioManager.Instance.PlayBGM(SoundBGMType.BGM002_GAME);
		// クリアフラグ初期化
		ResultBool.Instance.SetSuccessFlag(true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
