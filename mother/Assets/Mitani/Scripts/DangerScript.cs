using UnityEngine;
using System.Collections;

public class DangerScript : MonoBehaviour {

    public Sprite[] cutIn;


    private int count;
    private SpriteRenderer renderer;
    


    
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        count--;
        //transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 1.8f, 0), Time.deltaTime * 20);
        
        if (count > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 1.8f, 0), 0.3f);
        }
        else
        {
            transform.Translate(-0.4f, 0, 0);
        }
	}

    public void Init(int num)
    {
        Debug.Log("通った");
        count = 120;
        transform.position = new Vector3(10, 1.8f, 0);
        renderer = gameObject.GetComponent<SpriteRenderer>();
        renderer.sprite = cutIn[num];
    }
}
