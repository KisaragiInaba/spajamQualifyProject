using UnityEngine;
using System.Collections;

public class PanickTitle : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnButton1()
	{
		Application.LoadLevel("");
	}

	public void OnButton2()
	{
		Application.Quit ();
	}
}
