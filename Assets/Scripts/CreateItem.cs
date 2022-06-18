using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateItem : MonoBehaviour
{
    public GameObject makeprefab;
    private float timeElapsed;
    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update() {
        timeElapsed += Time.deltaTime;
        HENNSUU hennsuu;
        GameObject obj = GameObject.Find("HENNSUU"); //Playerっていうオブジェクトを探す
        hennsuu = obj.GetComponent<HENNSUU>(); //付いているスクリプトを取得
        float INTERVAL = hennsuu.CreateItemInterval;

        if(timeElapsed >= INTERVAL) {
            float x = Mathf.Floor(Random.Range(1, 5));
            x -= 2.5f;
            x *= 5f;
            Vector3 pos = new Vector3(x, 10.0f, 0);
            Instantiate(makeprefab, pos, Quaternion.identity);

            timeElapsed = 0.0f;
        }
    }
}
