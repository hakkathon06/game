using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Selected : MonoBehaviour
{
    private Vector3 defaultPos;
    [SerializeField] TextMeshProUGUI text;

    private void Start()
    {
        defaultPos = transform.position;
    }
    //�@�����ꂽ��
    private void OnMouseDown()
    {
        text.text = "AA";
    }
    //�@�����Ă�����
    private void OnMouseEnter()
    {
    }
    //�@�o�Ă������Ƃ�
    private void OnMouseExit()
    {
    }
    //�@��ɂ��鎞
    private void OnMouseOver()
    {
        Debug.Log("OnMouseOver");
    }
    //�@��������
    private void OnMouseUp()
    {
        Debug.Log("OnMouseUp");
    }
    //�@�I�u�W�F�N�g��Řb������
    private void OnMouseUpAsButton()
    {
        Debug.Log("OnMouseUpAsButton");
    }
}
