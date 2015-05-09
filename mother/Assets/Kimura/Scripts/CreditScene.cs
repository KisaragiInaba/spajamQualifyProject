using UnityEngine;
using System.Collections;

public class CreditScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		AudioManager.Instance.StartBGM ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnButtonTitle() {
		Application.LoadLevel("TitleScene");
	}

}
