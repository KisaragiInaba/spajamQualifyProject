using UnityEngine;
using System.Collections;

public class ItemScript : MonoBehaviour
{

    //sutatus
    public float gainHp = 0;
    public float gainMood = 0;
    public int gainDamage = 0;

    public int itemNum;
    //Catch
    private bool catchFlag = false;
    private Camera camera;

    private float speed;
    // Use this for initialization
    void Start()
    {
        GameObject cameraObj = GameObject.Find("Main Camera");
        camera = cameraObj.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, -1 * speed, 0);
    }

    void OnMouseDown()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        transform.position = camera.ScreenToWorldPoint(mousePos);
        catchFlag = true;
    }

    void OnMouseDrag()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        transform.position = camera.ScreenToWorldPoint(mousePos);
        //gameObject.rigidbody2D.velocity = (mousePos - mousePosed) / Time.deltaTime;
    }

    public void SetSpeed(float setspeed)
    {
        speed = setspeed;
    }
}
