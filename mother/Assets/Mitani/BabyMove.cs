using UnityEngine;
using System.Collections;

public class BabyMove : MonoBehaviour {

    public float startHp = 180;
    public float maxHp = 180;

    public float startMood;
    public float maxMood;

    public float LimitTime = 180;

    public float speed = 0.1f;


    //
    //現在のHPとMood
    private float nowHp;
    private float nowMood;
    private float nowTime;

    private GameObject tage;             //移動先


    //時計
    private Transform clockLong;
    private Transform clockShort;


    /*
    private int patterned;      //
    private Transform positioned;        //動いてたらアニメーション
    */


    //デバック用
    private bool check; 

	// Use this for initialization
	void Start () {
        nowHp = startHp;
        nowMood = startMood;
        nowTime = LimitTime;

        clockLong = GameObject.Find("Long").transform;
        clockShort = GameObject.Find("Short").transform;

        //tage = GameObject.Find("target0");
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
        {

        }
        else
        {
        }
        nowTime -= Time.deltaTime;

        //時間に比例して時計を回す
        clockLong.rotation = Quaternion.Euler(0, 0, 1080 * nowTime / LimitTime);
        clockShort.localRotation = Quaternion.Euler(0, 0, -90 + 1080 * nowTime / LimitTime /12 );

        //ランダムにターゲットに移動する
        if (Time.frameCount % 180 == 0 || tage == null)
        {
            int pattern = UnityEngine.Random.Range(0,3);
            switch(pattern){
                case 0:
                    tage = GameObject.Find("target0");
                    break;
                case 1:
                    tage = GameObject.Find("target1");
                    break;
                case 2:
                    tage = GameObject.Find("target2");
                    break;
                case 3:
                    tage = GameObject.Find("target3");
                    break;
            }
        }
        if (tage != null)
        {
            LookAt2D(tage);
            // transform.Rotate(90, 0, 0);
            transform.position = Vector3.MoveTowards(transform.position, tage.transform.position, Time.deltaTime * speed);
        }

        //時間が切れたらシーン遷移
        if (nowTime <= 0)
        {
        //    Application.LoadLevel("ScoreScene");
        }
	}

    void OnGUI()
    {
        GUI.Label(new Rect(100, 0, 100, 20), "HP:" + nowHp);
        GUI.Label(new Rect(100, 20, 100, 20), "Mood:" + nowMood);
        GUI.Label(new Rect(100, 40, 100, 20), "Time:" + nowTime);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Item")
        {
            ItemScript item = col.GetComponent<ItemScript>();
            nowHp += item.gainHp;
            nowMood += item.gainMood;
            nowTime += item.gainTime;
            Destroy(col.gameObject);
            Debug.Log("hit2");
        }
    }


    public void SetHp(float changehp)
    {
        maxHp += changehp;
    }

    public void SetMood(float changemood)
    {
        maxMood += changemood;
    }






    void LookAt2D(GameObject target)
    {
        // 指定オブジェクトと回転さすオブジェクトの位置の差分(ベクトル)
        Vector3 pos = target.transform.position - transform.position;
        // ベクトルのX,Yを使い回転角を求める
        float angle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
        Quaternion rotation = new Quaternion();
        // 回転角は右方向が0度なので-90しています
        rotation.eulerAngles = new Vector3(0, 0, angle - 90);
        // 回転
        transform.rotation = rotation;
    }
}
