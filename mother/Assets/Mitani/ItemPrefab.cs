﻿using UnityEngine;
using System.Collections;

public class ItemPrefab : MonoBehaviour
{

    public GameObject[] Item;

    public int frequncy = 40;       //出現確率
    public int flamecheck = 30;     //何フレームごとにチェックするか   ※60=1秒


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount % 120 == 0)
        {
            frequncy += 2;
        }
        if (Time.frameCount % flamecheck == 0)
        {
            int rand = UnityEngine.Random.Range(0, 100);
            if (rand < frequncy)
            {
                int randnum = UnityEngine.Random.Range(0, Item.Length);
                int randX = UnityEngine.Random.Range(0, 30);
                Vector3 popPosition = new Vector3(transform.position.x - 1.5f + randX / 10.0f, transform.position.y, transform.position.z);
                GameObject item = Instantiate(Item[randnum], popPosition, transform.rotation) as GameObject;
                item.name = "target" + randnum;

                ItemScript itemScript = item.GetComponent<ItemScript>();
                itemScript.itemNum = randnum;
                int randd = UnityEngine.Random.Range(0, 40);
                itemScript.SetSpeed(0.01f + randd / 3000.0f);
            }
        }
    }
}
