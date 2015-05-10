using UnityEngine;
using System.Collections;

public class Baby : MonoBehaviour {

    public float startHp = 180;
    public float maxHp = 180;

    public float startMood = 50;
    public float maxMood = 100;

    public float LimitTime = 180;

    public float speed = 0.1f;

    public float maxPositionY = -1;
    //
    //現在のHPとMood
    private float nowHp;
    private float nowMood;
    private float nowTime;

    private GameObject tage;             //移動先


    //時計
    private Transform clockLong;
    private Transform clockShort;

    //バー2種類
    private Transform hpBar;
    private Transform moodBar;

    //データ渡し用
    private GameObject sendData;
    private SendDataScript sendScript;

    //デンジャー
    private GameObject danger;
    private DangerScript dangerScript;
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

        hpBar = GameObject.Find("HpBar").transform;
        moodBar = GameObject.Find("MoodBar").transform;

        sendData = GameObject.Find("SendData");
        sendScript = sendData.GetComponent<SendDataScript>();

        danger = GameObject.Find("Danger");
        dangerScript = danger.GetComponent<DangerScript>();
        //tage = GameObject.Find("target0");
	}
	
	// Update is called once per frame
	void Update () {
        nowTime -= Time.deltaTime;

        //時間に比例して時計を回す
        clockLong.rotation = Quaternion.Euler(0, 0, 1080 * nowTime / LimitTime);
        clockShort.localRotation = Quaternion.Euler(0, 0, -90 + 1080 * nowTime / LimitTime /12 );

        //HPに比例してBarの長さを変化させる
        hpBar.localScale = new Vector3(nowHp / maxHp , 0.7f, 1.0f);
        //Moodに比例してBarの長さを変化させる
        moodBar.localScale = new Vector3(nowMood / maxMood, 0.7f, 1.0f);


        //ランダムにターゲットに移動する
        if (Time.frameCount % 180 == 0 || tage == null)
        {
            int pattern = UnityEngine.Random.Range(0,16);
            string tagename = "target" + pattern;
            tage = GameObject.Find(tagename);
           /*
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
                case 4:
                    tage = GameObject.Find("target4");
                    break;
                case 5:
                    tage = GameObject.Find("target5");
                    break;
            }*/
        }
        if (tage != null)
        {
            LookAt2D(tage);
            // transform.Rotate(90, 0, 0);
            transform.position = Vector3.MoveTowards(transform.position, tage.transform.position, Time.deltaTime * speed);
            if (transform.position.y > maxPositionY)
            {
                transform.position = new Vector3(transform.position.x, maxPositionY, transform.position.z);
            }
        }

        //時間が切れたらシーン遷移
		if (nowTime <= 0.0f)
        {
			sendScript.SetResultMood(nowMood);
			sendScript.SetResultTime(nowTime);
			Application.LoadLevel("ResultScene");
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
            GetComponent<Animator>().SetTrigger("Grap");
            ItemScript item = col.GetComponent<ItemScript>();
            nowHp += item.gainHp;
            nowMood += item.gainMood;
            nowTime += item.gainTime;
            //dangerScript.Init(6);
            dangerScript.Init(item.itemNum);
			//sendScript.resultDamage++;
            Destroy(col.gameObject);
        }
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
