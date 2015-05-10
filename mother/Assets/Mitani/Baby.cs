using UnityEngine;
using System.Collections;

public class Baby : MonoBehaviour
{

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

	private int i = 0;

    private GameObject tage;             //移動先


    //ヒット回数
    private int HitCount;


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
    void Start()
    {
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
    void Update()
    {
        nowTime -= Time.deltaTime;
        nowHp -= Time.deltaTime;

        //時間に比例して時計を回す
        clockLong.rotation = Quaternion.Euler(0, 0, 1080 * nowTime / LimitTime);
        clockShort.localRotation = Quaternion.Euler(0, 0, -90 + 1080 * nowTime / LimitTime / 12);

        //HPに比例してBarの長さを変化させる
        hpBar.localScale = new Vector3(nowHp / maxHp, 0.7f, 1.0f);
        //Moodに比例してBarの長さを変化させる
        moodBar.localScale = new Vector3(nowMood / maxMood, 0.7f, 1.0f);



        //ランダムにターゲットに移動する
        if (Time.frameCount % 180 == 0 || tage == null)
        {
            int pattern = UnityEngine.Random.Range(0, 16);
            string tagename = "target" + pattern;
            tage = GameObject.Find(tagename);
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
        if (nowTime <= 0.0f || nowHp <= 0.0f || nowMood <= 0.0f)
        {

            if (nowHp < 0 && nowTime < 0)
            {
				ResultBool.Instance.SetSuccessFlag (true);
            }
            else
            {
				ResultBool.Instance.SetSuccessFlag (false);
			}
            

			sendScript.SetResultMood(nowMood);
			sendScript.SetResultTime(nowTime);
			sendScript.SetResultDamage(HitCount);
        //    Debug.Log("gameover");
			Application.LoadLevel("ResultScene");
        }
    }

    void OnGUI()
    {
#if UNITY_EDITOR
        GUI.Label(new Rect(0, 80, 100, 20), "HP:" + nowHp);
        GUI.Label(new Rect(0, 100, 100, 20), "Mood:" + nowMood);
        GUI.Label(new Rect(0, 120, 100, 20), "Time:" + nowTime);
#endif
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Item")
        {
            GetComponent<Animator>().SetTrigger("Grap");
            ItemScript item = col.GetComponent<ItemScript>();
            nowHp += item.gainHp;
            nowMood += item.gainMood;
            HitCount += item.gainDamage;
            //dangerScript.Init(6);
            dangerScript.Init(item.itemNum);
			i++;
			sendScript.SetResultDamage (i);
			if (nowMood > maxMood) {
				nowMood = maxMood;
			}

			if (nowHp > maxHp) {
				nowHp = maxHp;
			}


            switch (item.itemNum)
            {
                case 0:
                case 7:
                case 10:
                case 13:
                case 16:
                case 17:
                case 18:
                case 20:
                    AudioManager.Instance.PlaySE(SoundSEType.SE008_AET);
                    break;
                case 1:
                case 9:
                    AudioManager.Instance.PlaySE(SoundSEType.SE007_MISTAKE);
                    break;
                case 2:
                case 14:
                    AudioManager.Instance.PlaySE(SoundSEType.SE004_HIT);
                    break;
                case 3:
                case 12:
                    AudioManager.Instance.PlaySE(SoundSEType.SE003_DOWN);
                    break;
                case 4:
                case 6:
                case 8:
                case 19:
                    AudioManager.Instance.PlaySE(SoundSEType.SE009_PAPER);
                    break;
                case 5:
                case 11:
                    AudioManager.Instance.PlaySE(SoundSEType.SE002_UP);
                    break;
                case 15:
                    AudioManager.Instance.PlaySE(SoundSEType.SE010_SENTAKU);
                    break;
                default:
                    break;
            }


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
