using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> protraitData;

    public Sprite[] portraitArr; //초상화 스프라이트 저장 배열

    private void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        protraitData = new Dictionary<int, Sprite> ();
        GenerateData();
    }
    private void GenerateData()
    {
        //Quest Talk
        talkData.Add(1000 + 10, new string[] { "정신이 들었니?:0", "이곳은...:3", "이곳은 아틀란티스. 바다가 허락한 자들에게 내어진 작은 마을이란다. " +
            "우리는 너를 기다리고 있었어.:1","자 어서 일어나렴, 우선 우리 집으로 안내해줄게.:0" });

        talkData.Add(1000 + 20, new string[] { "물약을 찾아서 전달해줘:1", "어디어디에 있어:0" });
        talkData.Add(2000 + 20, new string[] { "물약이 있으면 좋을텐데:1", "누가 하나 줬으면..:2" });
        //20번대 대화 끝나고 물약 오브젝트 활성화
        talkData.Add(5000 + 30, new string[] { "이게 물약인가?", "일단 주워봐야지" });
        talkData.Add(1000 + 30, new string[] { "물약 전달해달라는 내 부탁은 잘 들어주고 있는 거지?:2" });

        talkData.Add(2000 + 31, new string[] { "물약 나 주는거야?:1", "고마워:0" });

        talkData.Add(1000 + 40, new string[] { "수고했어:2" });


        //사물
        talkData.Add(100, new string[] { "특별할 건 없어보인다." });

        protraitData.Add(1000 + 0, portraitArr[0]);
        protraitData.Add(1000 + 1, portraitArr[1]);
        protraitData.Add(1000 + 2, portraitArr[2]);
        protraitData.Add(1000 + 3, portraitArr[3]);

        protraitData.Add(2000 + 0, portraitArr[0]);
        protraitData.Add(2000 + 1, portraitArr[1]);
        protraitData.Add(2000 + 2, portraitArr[2]);

    }

    public string GetTalk(int id, int talkIndex) //talkindex는 몇번째문장을 가져올 것인지 결정
    {
        if (!talkData.ContainsKey(id))
        {
            //해당 퀘스트 진행 순서 대사가 없을 때...
            if (!talkData.ContainsKey(id - id % 10))
            {
                //퀘스트 맨 처음 대사마저 없을 때 - 기본대사를 가지고오기
                return GetTalk(id - id % 100, talkIndex);
            }
            else
            {
                return GetTalk(id - id % 10, talkIndex);
            }

        }
        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];

    }

    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return protraitData[id + portraitIndex];
    }
}