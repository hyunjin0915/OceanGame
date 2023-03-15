using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;

    private void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }
    private void GenerateData()
    {
        //npc
        talkData.Add(1000, new string[] { "정신이 들었니?", "안녕" });

        //사물
        talkData.Add(100, new string[] { "특별할 건 없어보인다." });

        //첫번째 매개변수는 objData의 id 넣기
    }

    public string GetTalk(int id, int talkIndex) //talkindex는 몇번째문장을 가져올 것인지 결정
    {
        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }
}