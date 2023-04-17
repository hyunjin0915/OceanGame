using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : Singleton<QuestManager>
{
    public int questId; //지금 진행중인 퀘스트id
    public int questActionIndex=0; //퀘스트 대화순서 인덱스
    public GameObject[] questObject; //퀘스트용 오브젝트 저장
    Dictionary<int, QuestData> questList;

    public override void Awake()
    {
        base.Awake();
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    void GenerateData()
    {
        questList.Add(10, new QuestData("마을대화~시하브집대화",new int[] {1000,10000} ));
        questList.Add(20, new QuestData("타누아대화~레나나소라고동전달",new int[] {3000,2000,4000} ));
        questList.Add(30, new QuestData("도서관 앞에서 시하브와의 대화",new int[] {1000 } ));
    }

    public int GetQuestTalkIndex(int id)
    {
        //return questId ;
        return questId + questActionIndex;
        //퀘스트 id + 퀘스트 대화 순서 = 퀘스트 대화id
    }

    public string CheckQuest(int id)
    {
        if (id == questList[questId].npcId[questActionIndex]) //현재 퀘스트에 해당하는 npc일 경우에만 대화순서 넘어가도록
            questActionIndex++; //대화 진행을 위해 퀘스트 대화순서 올리는 함수

        ControlObject(); //퀘스트 오브젝트 관리용 함수

        if (questActionIndex == questList[questId].npcId.Length) //퀘스트 대화순서가 끝에 도달했을 때 다음 퀘스트로
            NextQuest();
        

        return questList[questId].questName;
    }
    public string CheckQuest()
    {
        return questList[questId].questName;
    }

    void NextQuest() //다음퀘스트로 넘어가기
    {
        questId += 10;
        questActionIndex = 0;
        if (questList.Count * 10 < questId) EndQuest();
    }
    void EndQuest()
    {
        questId = 10;
        //마지막퀘스트까지 완료하면 다시 처음으로 돌아오게 함 추후 수정 필요
    }
    void ControlObject() //퀘스트 오브젝트 관리용 함수
    {
        switch(questId)
        {
            case 10:
                break;
            case 20:
                //if (questActionIndex == 1)
                    questObject[0].SetActive(true);
                break;
            case 30:
                // if (questActionIndex ==1)
                //    questObject[0].SetActive(false);
                break;
        }
    }
}
