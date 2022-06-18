using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parameter : MonoBehaviour
{
    private static Parameter instance;
    public static Parameter Instance()
    {
        return (instance);
    }
    public float strength;
    public float weight;
    public float intelligence;
    public float sight;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        strength = 0.1f;
        weight = 1f;
        intelligence = 1f;
        sight = 1f;
        DontDestroyOnLoad(this.gameObject);
    }

    public static void addweight(float x)
    {
        if (instance.weight + x > 0)
        {
            instance.weight += x;
        }
    }

    public static void addstrength(float x)
    {
        if (instance.strength + x > 0)
        {
            instance.strength += x;
        }
    }

    public static void addintelligence(float x)
    {
        if (instance.intelligence + x > 0)
        {
            instance.intelligence += x;
        }
    }

    public static void addsight(float x)
    {
        if (instance.sight + x > 0)
        {
            instance.sight += x;
        }
    }

}
