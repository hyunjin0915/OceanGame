using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : Singleton<TalkManager>
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> protraitData;
    Dictionary<int, Sprite> PlayerprotraitData;

    public Sprite[] portraitArr; //초상화 스프라이트 저장 배열
    public Sprite[] PlayerportraitArr; //플레이어 초상화 스프라이트 저장 배열

    public override void Awake()
    {
        base.Awake();
        talkData = new Dictionary<int, string[]>();
        protraitData = new Dictionary<int, Sprite> ();
        PlayerprotraitData = new Dictionary<int, Sprite> ();
        GenerateData();
    }
    private void GenerateData()
    {
        //NPC들 기본 대사
        talkData.Add(1000, new string[] { "참 평화로운 마을이다:0" });
        talkData.Add(2000, new string[] { "참 평화로운 마을이다:0" });
        talkData.Add(3000, new string[] { "참 평화로운 마을이다:0" });
        talkData.Add(4000, new string[] { "참 평화로운 마을이다:0" });

        talkData.Add(200 , new string[] { "아야;;","이건 뭐지?:" });

        //Quest Talk
        talkData.Add(1000 + 10, new string[] { "정신이 들었니?:0", "이곳은...:13", "이곳은 아틀란티스. 바다가 허락한 자들에게 내어진 작은 마을이란다. " +
            "우리는 너를 기다리고 있었어.:1","자 어서 일어나렴, 우선 우리 집으로 안내해줄게.:0" });
        talkData.Add(10000 + 10, new string[] { "마시렴, 체력을 회복해주는 아이템이란다:0",
            "체력 회복 아이템을 손에 넣었다!:0","체력 회복 아이템은 인벤토리를 들어가서 클릭하면 먹어질거야:0",
            "이곳의 마을 주민들을 만나보지 않을래? 너가 궁금해 하는 것들이 조금은 해소가 될 거야:0"});

        talkData.Add(3000 + 20, new string[] { "오 안녕, 이제야 왔구나! 환영해:0", "이곳은 우리의 천국 아틀란티스야! 많은 것들이 궁금하지?:0",
            "하지만 그건 차차 알아가자고!:0","우선은 마을 사람들과 함께 인사라도 나눠:0","아 그리고 이건 선물이야! 맛있게 먹어~:0","(체력회복 아이템을 받았다.):0"});
        talkData.Add(2000 + 20, new string[] { "안녕... 아틀란티스는 어때?:0", "이곳 사람들은 참 친절해...:0", "육지는 더 이상 의미가 없으니... :0",
            "이곳을 좋아해줬으면 좋겠다.:0","뭐? 무슨 소ㄹ..:0","자세한 설명은 시첼라를 만나봐. 시첼라가 전부 말해줄거야.... :0",
            "난 이만 타누아를 만나러 가야해서... 아 그리고 이건 선물이야.:0","(소라 고동을 받았다) :0","시첼라는 소라 고동을 좋아해. 이걸 선물로 줘. 그럼 너를 더 반겨줄거야… :0"});

        //20번 대화 끝나고 물약 오브젝트 활성화(인벤토리에 소라고동 추가)
        talkData.Add(4000 + 21, new string[] { "안녕, 아틀란티스가 허락한 마지막 아이야.:0", "(..?):0", "이곳의 주민들은 만나봤니? 다들 너의 맘에 들었으면 좋겠구나...:0",
            "안녕하세요. 이건 선물이에요.:0","소라고동이구나? 고마워, 소중히 간직할게.:0","시첼라, 이곳은 어디인가요? 전 이곳에 어떻게 올 수 있던 거죠?:0","모든 것은 순리에 따라서 흘러가지.:0",
            "우리 같은 인간들이 이곳에 올 수 있던 것도 일종의 순리란다.:0","노아의 방주에 탔던 존재들처럼..인간이라는 종을 유지시키기 위하여..:0","노아의 방주??:0"});
        //21번 대화 끝나고 이미지 보여주는 씬 <- 구체적으로 어떻게?


        talkData.Add(1000 + 30, new string[] { "도서관을 가보고싶니?:0","도서관이요?:0", "그래, 저 도서관은 이곳에 대한 기록들이 존재하지. 왕을 만나는 데엔 시간이 조금 남았으니, 한번 가보는 건 어떠니?:0",
        "난 잠시 일이 있어서 자리를 비워야겠구나.:0"});
        //시하브 이동해서 사라짐, 이 때 도서관 문 setActive해주면 될 듯


        //사물
        talkData.Add(100, new string[] { "(가로등인가..?):0" });

        //초상화 관리...
        //protraitData.Add( 0, portraitArr[0]);
        PlayerprotraitData.Add(0, PlayerportraitArr[0]);
        PlayerprotraitData.Add(13, PlayerportraitArr[0]);

        protraitData.Add(1000 + 0, portraitArr[0]);
        protraitData.Add(1000 + 1, portraitArr[1]);
        protraitData.Add(1000 + 2, portraitArr[2]);
        protraitData.Add(1000 + 3, PlayerportraitArr[0]);

        protraitData.Add(10000 + 0, portraitArr[0]);

        protraitData.Add(2000 + 0, portraitArr[0]);
        protraitData.Add(2000 + 1, portraitArr[1]);
        protraitData.Add(2000 + 2, portraitArr[2]);

        protraitData.Add(3000 + 0, portraitArr[0]);
        protraitData.Add(4000 + 0, portraitArr[0]);
        protraitData.Add(5000 + 0, portraitArr[0]);

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
    public Sprite GetPlayerPortrait(int portraitIndex)
    {
        return PlayerprotraitData[portraitIndex];
    }
}