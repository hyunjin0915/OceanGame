using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId; //���� �������� ����Ʈid
    public int questActionIndex; //����Ʈ ��ȭ���� �ε���
    public GameObject[] questObject; //����Ʈ�� ������Ʈ ����
    Dictionary<int, QuestData> questList;

    void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    void GenerateData()
    {
        questList.Add(10, new QuestData("ù ���� �湮",new int[] {1000} ));
        questList.Add(20, new QuestData("���� �����ϱ�1",new int[] {1000} ));
        questList.Add(30, new QuestData("���� �����ϱ�2",new int[] {5000, 2000, 1000 } ));
        questList.Add(40, new QuestData("���� ����Ʈ Ŭ����!",new int[] {0} ));
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
