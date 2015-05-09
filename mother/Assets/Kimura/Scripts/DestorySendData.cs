using UnityEngine;
using System.Collections;

public class DestorySendData : MonoBehaviour {

	// Use this for initialization
	void Start () {
		obstacleDestroy ("SendData");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void obstacleDestroy(string tagName) {
		GameObject[] obstacles = GameObject.FindGameObjectsWithTag(tagName);
		foreach (GameObject obs in obstacles) {
			Destroy (obs);
		}
	}
}
