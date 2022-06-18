using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MGHumanTrigger : MonoBehaviour
{
    [SerializeField] private Transform human;
    [SerializeField] private Transform shadow;
    [SerializeField] private Transform cam;
    [SerializeField] private float shadow_tan;
    [SerializeField] private Vector3 human_size;
    [SerializeField] private Sequence sequence;
    [SerializeField] private System.Random rand;
    [SerializeField] private float time;
    [SerializeField] private float dashtime;
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
        human_size = human.localScale;
        // GameObject.FindGameObjectWithTag("human").GetComponent<BoxCollider>().size = human_size;
        shadow = GameObject.FindGameObjectWithTag("shadow").transform;

        cam = Camera.main.gameObject.transform;
        shadow_tan = 0.5f;
        time = 0f;
        dashtime = 0f;
        sidetime = 0f;
        s_default.leftside = -12f;
        s_default.rightside = 12f;
        s_default.dashtime = 2.5f;
        s_default.dashlen = 4f;
        s_default.sidetime = 0.5f;
        s_default.lanewidth = 5f;
        baseheight = 0f;
        action = new int[4] {0, 1, 2, 3};
        item = new string[5]{"hurdle", "item1", "item2", "item3", "item4"};
        rand = new System.Random((int)DateTime.Now.Millisecond);
    }

    void    action_shuffle()
    {
        int i, j, tmp;
        int[] flag = new int[4] {1, 1, 1, 1}; 

        for (i = 4; i > 0; i--)
        {
            j = 0;
            tmp = rand.Next(0, i) + 1;
            while(tmp > 0)
            {
                if (flag[j++] == 1)
                {
                    tmp--;
                }
            }
            flag[--j] = 0;
            action[i - 1] = j;
        }
    }

    void Jump(int flag)
    {
        if (rand.Next(1,1001) <= (int)(Parameter.Instance().intelligence * 1000))
        {
            flag = 0;
        }
        if (flag == 1)
        {
            Debug.Log("random");
            Action_Select(rand.Next(0, 5), 0);
            return ;
        }
        time = 1f + Parameter.Instance().strength;
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
        Parameter.addweight(-0.02f);
        Parameter.addstrength(0.01f);
    }

    void Dash(int flag)
    {
        if (dashtime <= 0)
        {
            if (rand.Next(1,1000) < (int)(Parameter.Instance().intelligence * 1000))
            {
                flag = 0;
            }
            if (flag == 1)
            {
                Debug.Log("random");
                Action_Select(rand.Next(0, 5), 0);
                return ;
            }
            Debug.Log("dash");
            sequence.Join(human.DOMoveY(human.position.y + s_default.dashlen, s_default.dashtime).SetLoops(2,LoopType.Yoyo));
            dashtime = s_default.dashtime * 2;
            Parameter.addweight(-0.02f);
            Parameter.addstrength(0.01f);
        }
    }

    void Right(int flag)
    {
        if (human.position.x < s_default.rightside - s_default.lanewidth && sidetime <= 0)
        {
            if (rand.Next(1,1000) < (int)(Parameter.Instance().intelligence * 1000))
            {
                flag = 0;
            }
            if (flag == 1)
            {
                Debug.Log("random");
                Action_Select(rand.Next(0, 5), 0);
                return ;
            }
            Debug.Log("right");
            sequence.Join(human.DOMoveX(human.position.x + s_default.lanewidth, s_default.sidetime).SetEase(Ease.Linear));
            sidetime = s_default.sidetime;
        }
    }

    void Left(int flag)
    {
        if (human.position.x > s_default.leftside + s_default.lanewidth && sidetime <= 0)
        {
            if (rand.Next(1,1000) < (int)(Parameter.Instance().intelligence * 1000))
            {
                flag = 0;
            }
            if (flag == 1)
            {
                Debug.Log("random");
                Action_Select(rand.Next(0, 5), 0);
                return ;
            }
            Debug.Log("left");
            sequence.Join(human.DOMoveX(human.position.x - s_default.lanewidth, s_default.sidetime).SetEase(Ease.Linear)); 
            sidetime = s_default.sidetime;
        }
    }

    void Action_Select(int n, int flag)
    {
        if (n == 0)
        {
            Jump(flag);
        }
        else if (n == 1)
        {
            Dash(flag);
        }
        else if (n == 2)
        {
            Right(flag);
        }
        else if (n == 3)
        {
            Left(flag);
        }
    }

    void Action()
    {
        sequence = DOTween.Sequence();
        if (Input.GetKey(KeyCode.Space))
        {
            Action_Select(action[0], 1);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Action_Select(action[1], 1);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Action_Select(action[2], 1);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Action_Select(action[3], 1);
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
        size.x = (human_size.x + (Parameter.Instance().weight - 1) / 3) * scale;
        size.y = human_size.y * scale;
        size.z = human_size.z * scale;
        human.localScale = size;
        // scale = (shadow.position - camera.position).magnitude;
        scale = Mathf.Abs(shadow.position.z - cam.position.z);
        if (scale != 0)
        {
            scale = 2 / scale;
        }
        size.x = (human_size.x + (Parameter.Instance().weight - 1) / 3) * scale;
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
            if (time < 2 - Parameter.Instance().weight && Parameter.Instance().weight < 5)
            {
                time = 2 - Parameter.Instance().weight;
                if (time < 0.5f)
                {
                    time = 0.5f;
                }
            }
            Parameter.addweight(0.01f);
        }
        else if (i == 1)
        {
            Parameter.addweight(0.02f);
            Parameter.addstrength(0.01f);
        }
        else if (i == 2)
        {
            Parameter.addweight(0.01f);
            Parameter.addintelligence(0.01f);
        }
        else if (i == 3)
        {
            Parameter.addweight(0.01f);
            Parameter.addsight(0.01f);
        }
        else if (i == 4)
        {
            Parameter.addweight(0.01f);
            Parameter.addintelligence(-0.05f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        int i;
        for (i = 0; i < 4; i++)
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
