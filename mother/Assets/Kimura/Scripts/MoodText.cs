using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoodText : MonoBehaviour {

	public Text moodText;
	private int mood = 0;

	// Use this for initialization
	void Start () {
		mood = (int)SendDataScript.Instance.resultMood;
		if (mood <= 0) {
			mood = 0;
		}
		moodText.text = mood.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
