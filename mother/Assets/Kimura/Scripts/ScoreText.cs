using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreText : MonoBehaviour {

	public Text scoreText;
	private int score = 0;

	private int damage 	= SendDataScript.Instance.resultDamage;
	private float mood	= SendDataScript.Instance.resultMood;
	private float time	= SendDataScript.Instance.resultTime;

	// Use this for initialization
	void Start () {
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
