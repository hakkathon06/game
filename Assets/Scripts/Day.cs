using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Day : MonoBehaviour
{
    public ScenarioCounter sc;
    public TextMeshProUGUI txt;

    // Start is called before the first frame update
    void Start()
    {
        sc = GameObject.Find("ScenarioCounter").GetComponent<ScenarioCounter>();
    }

    // Update is called once per frame
    void Update()
    {
        txt.text = sc.getDay() + "“ú–Ú";
    }
}
