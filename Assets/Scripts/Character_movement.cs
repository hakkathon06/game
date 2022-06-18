using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;  //DOTweenを使うときはこのusingを入れる


public class Character_movement : MonoBehaviour
{
    private int test_time = 0;//条件のために作った変数
    private int character_position=-5;//girlのx座標
    private GameObject girl;

    // Start is called before the first frame update
    void Start()
    {
        girl=GameObject.Find("Girl");
    }

    // Update is called once per frame
    void Update(){
        test_time++;
        //末尾に.SetDelay(3f);と書くと3秒後に移動を開始してくれる
        if(test_time==100){
            //条件を満たした時1.5秒かけて女の子が右に移動する
            character_position=260;
            girl.transform.DOLocalMove(new Vector3(character_position, 86f, 0f), 1.5f);
            Debug.Log("Go right");
        }
        if(test_time==1000){
            //条件を満たした時1.5秒かけて女の子が元の位置（真ん中）に移動する
            character_position=-5;
            girl.transform.DOLocalMove(new Vector3(character_position, 86f, 0f), 1.5f);
            Debug.Log("Go middle");
        }
        if(test_time==2000){
            //条件を満たした時1.5秒かけて女の子が左に移動する
            character_position=-270;
            girl.transform.DOLocalMove(new Vector3(character_position, 86f, 0f), 1.5f);
            Debug.Log("Go left");
        }
        if(test_time==3000){
            //条件を満たした時1秒かけて上にジャンプして元の位置に戻る
            girl.transform.DOLocalJump(new Vector3(character_position, 86f, 0f), jumpPower: 100f, numJumps: 1, duration: 1f);
            Debug.Log("Jump");
        }
        if(test_time==4000){
            //条件を満たした時すぐに消える
            girl.gameObject.SetActive(false);
            Debug.Log("Fade out");
        }
        if(test_time==5000){
            //条件を満たした時すぐ現れる
            girl.gameObject.SetActive(true);
            Debug.Log("Fade in");
        }
    }
}
