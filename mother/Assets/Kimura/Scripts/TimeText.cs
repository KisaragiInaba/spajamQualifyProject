using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeText : MonoBehaviour {

	public Text timeText;
	private int time = 0;

	// Use this for initialization
	void Start () {
		time = (int)SendDataScript.Instance.GetResultTime ();
		if (time <= 0) {
			time = 0;
		}
		timeText.text = time.ToString ();
	}
}
