using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    /*
    private enum Selector
    {
        Start,
        Exit,
        End
    };
    */
    public void PressStart()
    {
        SceneManager.LoadScene("main_text_n");
        SoundManager.Instance().Play("Button");
        SoundManager.Instance().PlayLoop("MainBGM");
    }

    public void PressExit()
    {
        SceneManager.LoadScene("pre_minigame");
        SoundManager.Instance().Play("Button");
        SoundManager.Instance().PlayLoop("MainBGM");
    }

    // [SerializeField] private List<GameObject> Selectors;

    // int tmpId = (int)Selector.Start;

    /*
    void Select(int id)
    {
        for (int i = 0; i < Selectors.Count; i++)
        {
            Selectors[i].SetActive(false);
        }
        Selectors[id].SetActive(true);
    }
    */
    // Start is called before the first frame update
    void Start()
    {
        // Select(tmpId);
        SoundManager.Instance().PlayLoop("TitleBGM");
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            tmpId++;
            tmpId %= (int)Selector.End;
            Select(tmpId);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            tmpId--;
            tmpId += (int)Selector.End;
            tmpId %= (int)Selector.End;
            Select(tmpId);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (tmpId == (int)Selector.Start)
            {
                SceneManager.LoadScene("main_novel");
            }
            else if (tmpId == (int)Selector.End)
            {

            }
        }*/
    }
}
