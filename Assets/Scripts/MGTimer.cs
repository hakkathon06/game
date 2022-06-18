using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MGTimer : MonoBehaviour
{
    [SerializeField] private Text gamecount;
    [SerializeField] private Text startcount;
    [SerializeField] private GameObject panel;
    private int seconds;
    private float totalTime;
    public static int startflag;
    // Start is called before the first frame update
    void Start()
    {
        startflag = 1;
        totalTime = 0f;
        gamecount.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        seconds = (int)totalTime;
        totalTime += Time.deltaTime;
        if (startflag == 1)
        {
            if (totalTime > 4f)
            {
                startflag = 2;
                totalTime = 0;
                panel.gameObject.SetActive(false);
                startcount.gameObject.SetActive(false);
                gamecount.gameObject.SetActive(true);
            }
            seconds = 3 - seconds;
            startcount.text = seconds.ToString();
        }
        else if (startflag == 2) 
        {
            seconds = 40 - seconds;
            if (totalTime > 40f)
            {
                startflag = 3;
                totalTime = 0;
                panel.gameObject.SetActive(true);
                startcount.gameObject.SetActive(true);
                gamecount.gameObject.SetActive(false);
                startcount.text = "Time Up!";
            }
            gamecount.text = seconds.ToString();
        }
        else
        {
            if (totalTime > 3f)
            {
                SceneManager.LoadScene("pre_minigame");
            }
        }
    }
}
