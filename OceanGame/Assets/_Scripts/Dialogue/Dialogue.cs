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
    [Tooltip("초상화이미지")]
    public GameObject portraitImg;
    [Tooltip("대화ID")]
    public int talkId;
   // [Tooltip("npc Id")]
   // public int NpcId;
    //public int questId;

    [HideInInspector]
    public string[] spriteName;


}
[System.Serializable]
public class DialogueEvent
{
    public string eventId; //이게 어떤 이벤트인지 name으로 명시

   // public Vector2 line; //X~Y까지의 대사 추출해서 꺼내오는 역할
    public Dialogue[] dialogues;
}

