using UnityEngine;
using System.Collections;

public class SendDataScript : SingletonMonoBehaviourFast<SendDataScript> {

	private int resultDamage;
	private float resultTime;
	private float resultMood;

	protected override void Awake() {
		CheckInstance ();
	}

	public void SetResultDamage(int damage) {
		resultDamage = damage;
	}

	public void SetResultTime(float time) {
		resultTime = time;
	}

	public void SetResultMood(float mood) {
		resultMood = mood;
	}

	public int GetResultDamage() {
		return resultDamage;
	}

	public float GetResultTime() {
		return resultTime;
	}

	public float GetResultMood() {
		return resultMood;
	}
}
