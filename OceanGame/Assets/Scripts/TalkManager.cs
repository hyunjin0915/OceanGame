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
        talkData.Add(1000, new string[] { "������ �����?", "�ȳ�" });

        //�繰
        talkData.Add(100, new string[] { "Ư���� �� ����δ�." });

        //ù��° �Ű������� objData�� id �ֱ�
    }

    public string GetTalk(int id, int talkIndex) //talkindex�� ���°������ ������ ������ ����
    {
        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }
}