using UnityEngine;
using System.Collections;

public class SendDataScript : SingletonMonoBehaviourFast<SendDataScript> {

    public int resultDamage;
    public float resultTime;
    public float resultMood;

	private void Awake() {
		CheckInstance ();
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
