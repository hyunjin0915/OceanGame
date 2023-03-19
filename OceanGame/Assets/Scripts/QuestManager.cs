using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId; //지금 진행중인 퀘스트id
    public int questActionIndex; //퀘스트 대화순서 인덱스
    public GameObject[] questObject; //퀘스트용 오브젝트 저장
    Dictionary<int, QuestData> questList;

    void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    void GenerateData()
    {
        questList.Add(10, new QuestData("첫 마을 방문",new int[] {1000} ));
        questList.Add(20, new QuestData("물약 전달하기1",new int[] {1000} ));
        questList.Add(30, new QuestData("물약 전달하기2",new int[] {5000, 2000, 1000 } ));
        questList.Add(40, new QuestData("물약 퀘스트 클리어!",new int[] {0} ));
    }

    public int GetQuestTalkIndex(int id)
    {
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
    }

    void ControlObject() //퀘스트 오브젝트 관리용 함수
    {
        switch(questId)
        {
            case 10:
                break;
            case 20:
                if (questActionIndex == 1)
                    questObject[0].SetActive(true);
                break;
            case 30:
                if (questActionIndex ==1)
                    questObject[0].SetActive(false);
                break;
        }
    }
}
