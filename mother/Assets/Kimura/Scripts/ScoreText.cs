using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreText : MonoBehaviour {

	public Text scoreText;
	private int score = 0;

	private int damage;
	private float mood;
	private float time;

	// Use this for initialization
	void Start () {
		damage 	= SendDataScript.Instance.GetResultDamage();
		mood	= SendDataScript.Instance.GetResultMood();
		time	= SendDataScript.Instance.GetResultTime();

		score = (int)mood + (int)time - damage;
		if (score <= 0) {
			score = 0;
		}
		scoreText.text = score.ToString ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
