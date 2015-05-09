using UnityEngine;
using System.Collections;

public class SendDataScript : SingletonMonoBehaviourFast<SendDataScript> {


	public int resultDamage;
	public float resultTime;
	public float resultMood;

	public void Awake ()
	{
		CheckInstance ();
	}

	public int ReturnDamage() {
		return resultDamage;
	}

	public float ReturnTime() {
		return resultTime;
	}

	public float ReturnMood() {
		return resultMood;
	}
}
