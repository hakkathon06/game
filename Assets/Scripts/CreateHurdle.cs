using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateHurdle : MonoBehaviour
{
    public GameObject makeprefab;
    // Start is called before the first frame update
    private float timeElapsed;
    void Start()
    {

    }

    // Update is called once per frame
    void Update() {
        timeElapsed += Time.deltaTime;
        HENNSUU hennsuu;
        GameObject obj = GameObject.Find("HENNSUU"); //Playerっていうオブジェクトを探す
        hennsuu = obj.GetComponent<HENNSUU>(); //付いているスクリプトを取得
        float INTERVAL = hennsuu.CreateHurdleInterval;

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
