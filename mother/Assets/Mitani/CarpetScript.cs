using UnityEngine;
using System.Collections;

public class CarpetScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(0, -0.01f, 0);
        if (transform.position.y < -5.8)
        {
            transform.Translate(0, 12.8f, 0);
        }
	}
}
