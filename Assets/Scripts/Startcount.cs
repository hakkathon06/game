using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Startcount : MonoBehaviour
{
    [SerializeField] private Text startcount;
    private int seconds;
    private float totalTime;
    // Start is called before the first frame update
    void Start()
    {
        totalTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        seconds = 3 - (int)totalTime;
        totalTime += Time.deltaTime;
        if (totalTime > 3f)
        {
            SceneManager.LoadScene("end");
        }
        startcount.text = seconds.ToString();
    }
}
