using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue
{
    [Tooltip("��� ġ�� ĳ���� �̸�")]
    public string name;
    [Tooltip("��系��")]
    public string[] contexts;
    public GameObject portraitImg;

    [HideInInspector]
    public string[] spriteName;
}
[System.Serializable]
public class DialogueEvent
{
    public string name; //�̰� � �̺�Ʈ���� name���� ���

    public Vector2 line; //X~Y������ ��� �����ؼ� �������� ����
    public Dialogue[] dialogues;
}