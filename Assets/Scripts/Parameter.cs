using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parameter : MonoBehaviour
{
    public static Parameter param;
    public float strength;
    public float weight;
    public float intelligence;
    public float sight;

    public void Start()
    {
        if(param == null)
        {
            param = this;
        }
        strength = 0.1f;
        weight = 1f;
        intelligence = 1f;
        sight = 1f;
    }
}
