using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : Singleton<QuestManager>
{
    public int questId; //지금 진행중인 퀘스트id
    public int questActionIndex=0; //퀘스트 대화순서 인덱스
    public GameObject[] questObject; //퀘스트용 오브젝트 저장
    public Dictionary<int, QuestData> questList;

    public GameObject questWindow; //퀘스트 UI창
    public TextMeshProUGUI questTitleText; //UI창에 퀘스트 정보 글씨
    public float questUITime = 4.0f;

    public bool isShellCollected = false;
    public bool isFishRodCollected = false;

    public override void Awake()
    {
        base.Awake();
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    void GenerateData()
    {
        questList.Add(10, new QuestData("마을대화~시하브집대화",new int[] {1000,10000} ));
        questList.Add(20, new QuestData("시하브의 방을 둘러보자",new int[] {3000} ));
        questList.Add(30, new QuestData("마을을 돌아다니자",new int[] {5000} ));
        questList.Add(40, new QuestData("낚시대를 찾자",new int[] {200 } ));
        questList.Add(50, new QuestData("낚시대를 이용하여 분수대에 빠진 열쇠를 꺼내자",new int[] {200} ));
        questList.Add(60, new QuestData("마을을 돌아다니자",new int[] {1000 } ));
    }

    public int GetQuestTalkIndex()
    {
        return questId + questActionIndex;
        //퀘스트 id + 퀘스트 대화 순서 = 퀘스트 대화id
    }
    public string CheckQuestObjs(int id)
    {
        switch (id)
        {
            case 100:
                isShellCollected = true;
                return "소라고동";
            case 200:
                isFishRodCollected = true;
                return "낚싯대";
        }
        return "null";
    }
    public string CheckQuest(int id)
    {
        if (id == questList[questId].npcId[questActionIndex]) //현재 퀘스트에 해당하는 npc일 경우에만 대화순서 넘어가도록
            questActionIndex++; //대화 진행을 위해 퀘스트 대화순서 올리는 함수

       // ControlObject(); //퀘스트 오브젝트 관리용 함수
        //케이스 맞는 퀘스트 실제진행 함수 실행

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
        ControlObject();

        StartCoroutine(OpenQuestWindow());
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
                   // questObject[0].SetActive(true);
                break;
            case 40:
                if (isFishRodCollected)
                    NextQuest();
                break;
        }
    }



    public IEnumerator OpenQuestWindow()
    {
        Debug.Log("퀘스트ui창열기");
        questWindow.SetActive(true);
        questTitleText.text = questList[questId].questName;
        yield return new WaitForSeconds(questUITime);
        questWindow.SetActive(false);
    }

    public bool ShellCollect()
    {
        isShellCollected = true;
        return isShellCollected;
    }
}
