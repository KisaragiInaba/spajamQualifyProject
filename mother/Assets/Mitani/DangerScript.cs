using UnityEngine;
using System.Collections;

public class DangerScript : MonoBehaviour {

    private int count;
    private SpriteRenderer renderer;
    private GameObject cutInObj;

    public Sprite[] cutIn;
	// Use this for initialization
	void Start () {
        cutInObj = GameObject.Find("CutIn");
        renderer = cutInObj.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        count--;
        if (count > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 1.8f, 0), Time.deltaTime * 1);
        }
        else
        {
            transform.Translate(-0.2f, 0, 0);
        }
	}

    public void Init(int num)
    {
        count = 180;
        renderer.sprite = cutIn[num];
        transform.position = new Vector3(10, 1.8f, 0);
    }
}
