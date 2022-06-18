using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TateScroll : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //下方向にスクロール
        HENNSUU hennsuu;
        GameObject obj = GameObject.Find("HENNSUU"); //Playerっていうオブジェクトを探す
        hennsuu = obj.GetComponent<HENNSUU>(); //付いているスクリプトを取得
        float speed = hennsuu.speed;

        transform.position -= new Vector3(0, Time.deltaTime * speed);

        //Yが-29まで来れば、61まで移動する
        if (transform.position.y <= -48f)
        {
            float z = Mathf.FloorToInt(Random.Range(1, 1));
            z *= 0.001f;
            transform.position = new Vector3(0, 96f, 10+z);
        }
    }
}


