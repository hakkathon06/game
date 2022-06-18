using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using System.IO;
using TMPro;
using UnityEngine.AddressableAssets;
using System;


public class TextWriter : MonoBehaviour
{
    public UIText uitext;
    public UIButton uibutton;
    public string path;
    List<string> lines = new List<string>();
    int nowline = 1;
    // Start is called before the first frame update
    void Start()
    {
        LoadText();
        StartCoroutine("Cotest");
    }

    void LoadText()
    {
        enabled = false;
        Addressables.LoadAssetAsync<TextAsset>(path).Completed += novelData =>
        {
            StringReader reader = new StringReader(novelData.Result.text);
            //Debug.Log(reader.Peak());
            while (reader.Peek() != -1)
            {
                string line = reader.ReadLine();
                lines.Add(line);
                //Debug.Log(lines.Count);
                enabled = true;
            }
        };

    }

    // クリック待ちのコルーチン
    IEnumerator Skip()
    {
        while (uitext.playing) yield return 0;//一フレーム分待つ。yield return null とは何かが違うらしい
        while (!uitext.IsClicked()) yield return 0;
    }

    IEnumerator DoLine()
    {
        //Debug.Log(lines.Count);
        string line = lines[nowline - 1];
        bool match = Regex.IsMatch(line, "@.");
        if (Regex.IsMatch(line, "@c"))
        {
            nowline++;
        }
        else if (Regex.IsMatch(line, "@b"))// 選択肢
        {
            var words = new List<string>(line.Split(","));
            int button_num = Int32.Parse(words[1]);
            int res = -1;
            uibutton.DrawButton(words.GetRange(2, button_num));
            while (true)
            {
                res = uibutton.GetButton();
                if (res == -1) yield return 0;
                else break;
            }
            nowline = Int32.Parse(words[button_num + res + 1]);
        }
        else if (Regex.IsMatch(line, "@l"))
        {
            string x = line.Substring(3);
            nowline = Int32.Parse(x);
        }
        else if (Regex.IsMatch(line, "@p"))
        {
            ScenarioCounter.Instance().incScenario();
            nowline++;
        }
        else if (Regex.IsMatch(line, "@g"))
        {
            ScenarioCounter.Instance().incDays();
            nowline++;
        }
        else if (Regex.IsMatch(line, "@m"))
        {
            ScenarioCounter.Instance().miniGameStart();
            nowline++;
        }
        else
        {
            int comma = line.IndexOf(",");
            if (comma == -1)
            {
                uitext.DrawText(line);
            }
            else
            {
                uitext.DrawText(line.Substring(0, comma), line.Substring(comma + 1));
            }
            yield return StartCoroutine("Skip");
            nowline++;
        }

        if (nowline > lines.Count) nowline -= lines.Count;
    }

    // 文章を表示させるコルーチン
    IEnumerator Cotest()
    {
        while (true)
        {
            if (!enabled) yield return 0;
            else yield return StartCoroutine("DoLine");
        }
    }
    public void OnClick()
    {
        Debug.Log("押された!");
    }


}