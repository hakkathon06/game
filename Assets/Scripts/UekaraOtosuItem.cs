using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UekaraOtosuItem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // 落下速度をここで定義
        //下方向にスクロール
        HENNSUU hennsuu;
        GameObject obj = GameObject.Find("HENNSUU"); //Playerっていうオブジェクトを探す
        hennsuu = obj.GetComponent<HENNSUU>(); //付いているスクリプトを取得
        float speed = hennsuu.speed*0.5f;
        transform.position -= new Vector3(0, Time.deltaTime * speed);

        //Yが-26まで来れば、オブジェクトを消そう
        if (transform.position.y <= -26f)
        {
            Destroy(gameObject);
        }
    }
}
