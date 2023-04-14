using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue
{
    [Tooltip("대사 치는 캐릭터 이름")]
    public string name;
    [Tooltip("대사내용")]
    public string[] contexts;
    public GameObject portraitImg;

    [HideInInspector]
    public string[] spriteName;
}
[System.Serializable]
public class DialogueEvent
{
    public string name; //이게 어떤 이벤트인지 name으로 명시

    public Vector2 line; //X~Y까지의 대사 추출해서 꺼내오는 역할
    public Dialogue[] dialogues;
}