using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MGHumanTrigger : MonoBehaviour
{
    private Transform human;
    private Transform shadow;
    private Transform cam;
    [SerializeField]  private float shadow_tan;
    private Vector3 human_size;
    private Sequence sequence;
    private float time;
    private float dashtime;
    private float sidetime;
    private float baseheight;
    private int [] action;
    private string[] item;
    public struct S_default
    {
        public float leftside;
        public float rightside;
        public float dashtime;
        public float dashlen;
        public float sidetime;
        public float lanewidth;
    }
    private S_default s_default;

    // Start is called before the first frame update
    void Start()
    {
        human = GameObject.FindGameObjectWithTag("human").transform;
        human_size = GameObject.FindGameObjectWithTag("human").GetComponent<BoxCollider>().size;
        shadow = GameObject.FindGameObjectWithTag("shadow").transform;
        cam = Camera.main.gameObject.transform;
        shadow_tan = 0.5f;
        time = 0f;
        dashtime = 0f;
        sidetime = 0f;
        s_default.leftside = -4f;
        s_default.rightside = 4f;
        s_default.dashtime = 2.5f;
        s_default.dashlen = 4f;
        s_default.sidetime = 0.5f;
        s_default.lanewidth = 2f;
        baseheight = 0f;
        action = new int[4] {0, 1, 2, 3};
        item = new string[4]{"hurdle", "item1", "item2", "item3"};
    }

    float derivative()
    {
        return (0.01f);
    }

    void Jump()
    {
        time = 1f + Parameter.param.strength;
        if (dashtime > 0)
        {
            if (time > dashtime)
            {
                time = dashtime;
            }
            if (time > dashtime - s_default.dashtime && dashtime > s_default.dashtime)
            {
                time = dashtime - s_default.dashtime;
            }
        }
        if (sidetime > 0 && time > sidetime)
        {
            time = sidetime;
        }
        Debug.Log("jump");
        sequence.Join(human.DOMoveZ(-1f, time / 2).SetLoops(2,LoopType.Yoyo));
        Parameter.param.strength += derivative();
    }

    void Dash()
    {
        if (dashtime <= 0)
        {
            Debug.Log("dash");
            sequence.Join(human.DOMoveY(human.position.y + s_default.dashlen, s_default.dashtime).SetLoops(2,LoopType.Yoyo));
            dashtime = s_default.dashtime * 2;
        }
    }

    void Right()
    {
        if (human.position.x < s_default.rightside - s_default.lanewidth && sidetime <= 0)
        {
            Debug.Log("right");
            sequence.Join(human.DOMoveX(human.position.x + s_default.lanewidth, s_default.sidetime).SetEase(Ease.Linear));
            sidetime = s_default.sidetime;
        }
    }

    void Left()
    {
        if (human.position.x > s_default.leftside + s_default.lanewidth && sidetime <= 0)
        {
            Debug.Log("left");
            sequence.Join(human.DOMoveX(human.position.x - s_default.lanewidth, s_default.sidetime).SetEase(Ease.Linear)); 
            sidetime = s_default.sidetime;
        }
    }

    void Action_Select(int n)
    {
        if (n == 0)
        {
            Jump();
        }
        else if (n == 1)
        {
            Dash();
        }
        else if (n == 2)
        {
            Right();
        }
        else if (n == 3)
        {
            Left();
        }
    }

    void Action()
    {
        sequence = DOTween.Sequence();
        if (Input.GetKey(KeyCode.Space))
        {
            Action_Select(action[0]);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Action_Select(action[1]);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Action_Select(action[2]);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Action_Select(action[3]);
        }

    }

    void Fix_position()
    {
        Vector3 pos;
        pos.x = human.position.x;
        pos.y = human.position.y + (human.position.z - baseheight) * shadow_tan - human.localScale.y;
        pos.z = baseheight;
        shadow.position = pos; 
    }

    void Fix_size()
    {
        float scale;
        Vector3 size;

        // scale = (human.position - cam.position).magnitude;
        scale = Mathf.Abs(human.position.z - cam.position.z);
        if (scale != 0)
        {
            scale = 2 / scale;
        }
        size.x = human_size.x * scale;
        size.y = human_size.y * scale;
        size.z = human_size.z * scale;
        human.localScale = size;
        // scale = (shadow.position - camera.position).magnitude;
        scale = Mathf.Abs(shadow.position.z - cam.position.z);
        if (scale != 0)
        {
            scale = 2 / scale;
        }
        size.x = human_size.x * scale;
        size.y = human_size.z * shadow_tan * scale;
        size.z = 0.02f;
        shadow.localScale = size;
    }

    void Update()
    {
        if (time <= 0)
        {
            Action();
        }
        else
        {
            time -= Time.deltaTime;
        }
        if (dashtime > 0)
        {
            dashtime -= Time.deltaTime;
        }
        if (sidetime > 0)
        {
            sidetime -= Time.deltaTime;
        }
        Fix_position();
        Fix_size();
    }

    void GetItem(int i)
    {
        if (i == 0)
        {
            time = 2;
        }
        else if (i == 1)
        {

        }
        else if (i == 2)
        {

        }
        else if (i == 3)
        {

        }
    }

    void OnTriggerEnter(Collider other)
    {
        int i;
        for (i = 0; i < 3; i++)
        {
            if (string.Compare(other.gameObject.tag, item[i]) == 0)
            {
                Debug.Log(item[i]);
                GetItem(i);
                other.gameObject.SetActive(false);
            }
        }
    }
}
