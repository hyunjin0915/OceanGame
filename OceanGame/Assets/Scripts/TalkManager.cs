using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> protraitData;

    public Sprite[] portraitArr; //�ʻ�ȭ ��������Ʈ ���� �迭

    private void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        protraitData = new Dictionary<int, Sprite> ();
        GenerateData();
    }
    private void GenerateData()
    {
        //Quest Talk
        talkData.Add(1000 + 10, new string[] { "������ �����?:0", "�̰���...:3", "�̰��� ��Ʋ��Ƽ��. �ٴٰ� ����� �ڵ鿡�� ������ ���� �����̶���. " +
            "�츮�� �ʸ� ��ٸ��� �־���.:1","�� � �Ͼ��, �켱 �츮 ������ �ȳ����ٰ�.:0" });

        talkData.Add(1000 + 20, new string[] { "������ ã�Ƽ� ��������:1", "����� �־�:0" });
        talkData.Add(2000 + 20, new string[] { "������ ������ �����ٵ�:1", "���� �ϳ� ������..:2" });
        //20���� ��ȭ ������ ���� ������Ʈ Ȱ��ȭ
        talkData.Add(5000 + 30, new string[] { "�̰� �����ΰ�?", "�ϴ� �ֿ�������" });
        talkData.Add(1000 + 30, new string[] { "���� �����ش޶�� �� ��Ź�� �� ����ְ� �ִ� ����?:2" });

        talkData.Add(2000 + 31, new string[] { "���� �� �ִ°ž�?:1", "����:0" });

        talkData.Add(1000 + 40, new string[] { "�����߾�:2" });


        //�繰
        talkData.Add(100, new string[] { "Ư���� �� ����δ�." });

        protraitData.Add(1000 + 0, portraitArr[0]);
        protraitData.Add(1000 + 1, portraitArr[1]);
        protraitData.Add(1000 + 2, portraitArr[2]);
        protraitData.Add(1000 + 3, portraitArr[3]);

        protraitData.Add(2000 + 0, portraitArr[0]);
        protraitData.Add(2000 + 1, portraitArr[1]);
        protraitData.Add(2000 + 2, portraitArr[2]);

    }

    public string GetTalk(int id, int talkIndex) //talkindex�� ���°������ ������ ������ ����
    {
        if (!talkData.ContainsKey(id))
        {
            //�ش� ����Ʈ ���� ���� ��簡 ���� ��...
            if (!talkData.ContainsKey(id - id % 10))
            {
                //����Ʈ �� ó�� ��縶�� ���� �� - �⺻��縦 ���������
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