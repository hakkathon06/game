using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HENNSUU : MonoBehaviour
{
    public float speed = 14f;
    [SerializeField] public float CreateItemInterval = 1;
    [SerializeField] public float CreateHurdleInterval = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float speed = 14f * Parameter.Instance().strength;
    }
}
