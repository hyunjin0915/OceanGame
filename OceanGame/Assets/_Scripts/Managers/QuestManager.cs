using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : Singleton<QuestManager>
{
    public int questId; //���� �������� ����Ʈid
    public int questActionIndex; //����Ʈ ��ȭ���� �ε���
    public GameObject[] questObject; //����Ʈ�� ������Ʈ ����
    Dictionary<int, QuestData> questList;

    public override void Awake()
    {
        base.Awake();
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    void GenerateData()
    {
        questList.Add(10, new QuestData("������ȭ~���Ϻ�����ȭ",new int[] {1000,10000} ));
        questList.Add(20, new QuestData("Ÿ���ƴ�ȭ~�������Ҷ������",new int[] {2000,4000} ));
        questList.Add(30, new QuestData("������ �տ��� ���Ϻ���� ��ȭ",new int[] {1000 } ));
    }

    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
        //����Ʈ id + ����Ʈ ��ȭ ���� = ����Ʈ ��ȭid
    }

    public string CheckQuest(int id)
    {
        if (id == questList[questId].npcId[questActionIndex]) //���� ����Ʈ�� �ش��ϴ� npc�� ��쿡�� ��ȭ���� �Ѿ����
            questActionIndex++; //��ȭ ������ ���� ����Ʈ ��ȭ���� �ø��� �Լ�

        ControlObject(); //����Ʈ ������Ʈ ������ �Լ�

        if (questActionIndex == questList[questId].npcId.Length) //����Ʈ ��ȭ������ ���� �������� �� ���� ����Ʈ��
            NextQuest();

        return questList[questId].questName;
    }
    public string CheckQuest()
    {
        return questList[questId].questName;
    }

    void NextQuest() //��������Ʈ�� �Ѿ��
    {
        questId += 10;
        questActionIndex = 0;
    }

    void ControlObject() //����Ʈ ������Ʈ ������ �Լ�
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
