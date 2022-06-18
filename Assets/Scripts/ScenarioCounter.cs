using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenarioCounter : MonoBehaviour
{
    public GameObject Tatie;
    public GameObject TatiePrefab;
    public GameObject Heroin;
    public GameObject YujinA;
    public GameObject YujinB;
    public GameObject AiAi;

    public GameObject SoundM;

    public TextWriter textWriter;

    private static ScenarioCounter instance;
    public static ScenarioCounter Instance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    [SerializeField]
    private int count = 0;
    [SerializeField]
    private int nowday = 0;
    public void incScenario()
    {
        count++;
    }
    public int getCount()
    {
        return count;
    }
    public int getDay()
    {
        return nowday;
    }
    public void incDays()
    {
        nowday++;
    }

    public void miniGameStart()
    {
        SceneManager.LoadScene("minigame");
    }

    public void endGame()
    {
        SceneManager.LoadScene("N");
        count = 0;
        nowday = 0;
    }

    private void Update()
    {

        if (nowday == 5)
        {
            SceneManager.LoadScene("end");
            if (textWriter == null)
            {
                textWriter = GameObject.Find("TextController").GetComponent<TextWriter>();
            }
            if (textWriter != null)
            {
                if (true)
                {
                    textWriter.path = "Assets/Texts/end1.txt";
                }
                nowday++;
            }
        }

        if (count == 1)
        {
            if (Tatie == null)
            {
                Tatie = Instantiate(TatiePrefab);
                Tatie.GetComponent<Canvas>().worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
                Heroin = Tatie.transform.GetChild(0).gameObject;
                YujinA = Tatie.transform.GetChild(1).gameObject;
                YujinB = Tatie.transform.GetChild(2).gameObject;
                AiAi = Tatie.transform.GetChild(3).gameObject;
                YujinA.SetActive(false);
                YujinB.SetActive(false);
                AiAi.SetActive(false);
            }
        }

        if (count == 2)
        {
            Heroin.SetActive(false);
        }

        if (count == 3)
        {
            Heroin.SetActive(true);
        }

        if (count == 4)
        {
            Heroin.SetActive(false);
        }

        if (count == 5)
        {
            YujinA.SetActive(true);
        }

        if (count == 6)
        {
            YujinB.SetActive(true);
        }

        if (count == 7)
        {
            Heroin.SetActive(false);
            YujinA.SetActive(false);
            YujinB.SetActive(false);
        }

        if (count == 8)
        {
            YujinA.SetActive(true);
            YujinB.SetActive(true);
        }

        if (count == 9)
        {
            AiAi.SetActive(true);
        }

        if (count == 10)
        {
            Heroin = null;
            YujinA = null;
            YujinB = null;
            AiAi = null;
            SceneManager.LoadScene("pre_minigame");
            count++;
        }
    }
}
