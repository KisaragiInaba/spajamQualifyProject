using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DamageText : MonoBehaviour {

	public Text damageText;
	private int damage = 0;

	// Use this for initialization
	void Start () {
		damage = SendDataScript.Instance.GetResultDamage();
		damage *= -1;
		damageText.text = damage.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
