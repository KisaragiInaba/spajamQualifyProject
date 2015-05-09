using UnityEngine;
using System.Collections;

public class BabyMove : MonoBehaviour {

    public float startHp;
    public float maxHp;

    public float startMood;
    public float maxMood;

    public float speed = 0.1f;

    private int count;
    private Transform tage;

    


    private int patterned;      //
    private Transform positioned;        //動いてたらアニメーション

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        count++;
       

        if (count % 180 == 0)
        {
            int pattern = UnityEngine.Random.Range(0,3);
            switch(pattern){
                case 0:
                    tage = GameObject.Find("target0").transform;
                    break;
                case 1:
                    tage = GameObject.Find("target1").transform;
                    break;
                case 2:
                    tage = GameObject.Find("target2").transform;
                    break;
                case 3:
                    tage = GameObject.Find("target3").transform;
                    break;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, tage.transform.position, Time.deltaTime * speed);
	}
}
