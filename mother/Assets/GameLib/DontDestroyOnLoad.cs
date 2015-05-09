/*
*   @author Kyuzen
*/
using UnityEngine;
using System.Collections;

public class DontDestroyOnLoad : MonoBehaviour {

	[SerializeField]
	bool dontDestroyOnLoad;

	void Awake() {
        if (dontDestroyOnLoad)
        {
			DontDestroyOnLoad(this.gameObject);
		}	
	}
}
