using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : Singleton<TalkManager>
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> protraitData;
    Dictionary<int, Sprite> PlayerprotraitData;

    public Sprite[] portraitArr; //�ʻ�ȭ ��������Ʈ ���� �迭
    public Sprite[] PlayerportraitArr; //�÷��̾� �ʻ�ȭ ��������Ʈ ���� �迭

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
        //NPC�� �⺻ ���
        talkData.Add(1000, new string[] { "�� ��ȭ�ο� �����̴�:0" });
        talkData.Add(2000, new string[] { "�� ��ȭ�ο� �����̴�:0" });
        talkData.Add(3000, new string[] { "�� ��ȭ�ο� �����̴�:0" });
        talkData.Add(4000, new string[] { "�� ��ȭ�ο� �����̴�:0" });

        talkData.Add(200 , new string[] { "�ƾ�;;","�̰� ����?:" });

        //Quest Talk
        talkData.Add(1000 + 10, new string[] { "������ �����?:0", "�̰���...:13", "�̰��� ��Ʋ��Ƽ��. �ٴٰ� ����� �ڵ鿡�� ������ ���� �����̶���. " +
            "�츮�� �ʸ� ��ٸ��� �־���.:1","�� � �Ͼ��, �켱 �츮 ������ �ȳ����ٰ�.:0" });
        talkData.Add(10000 + 10, new string[] { "���÷�, ü���� ȸ�����ִ� �������̶���:0",
            "ü�� ȸ�� �������� �տ� �־���!:0","ü�� ȸ�� �������� �κ��丮�� ���� Ŭ���ϸ� �Ծ����ž�:0",
            "�̰��� ���� �ֹε��� �������� ������? �ʰ� �ñ��� �ϴ� �͵��� ������ �ؼҰ� �� �ž�:0"});

        talkData.Add(3000 + 20, new string[] { "�� �ȳ�, ������ �Ա���! ȯ����:0", "�̰��� �츮�� õ�� ��Ʋ��Ƽ����! ���� �͵��� �ñ�����?:0",
            "������ �װ� ���� �˾ư��ڰ�!:0","�켱�� ���� ������ �Բ� �λ�� ����:0","�� �׸��� �̰� �����̾�! ���ְ� �Ծ�~:0","(ü��ȸ�� �������� �޾Ҵ�.):0"});
        talkData.Add(2000 + 20, new string[] { "�ȳ�... ��Ʋ��Ƽ���� �?:0", "�̰� ������� �� ģ����...:0", "������ �� �̻� �ǹ̰� ������... :0",
            "�̰��� ������������ ���ڴ�.:0","��? ���� �Ҥ�..:0","�ڼ��� ������ ��ÿ�� ������. ��ÿ�� ���� �����ٰž�.... :0",
            "�� �̸� Ÿ���Ƹ� ������ �����ؼ�... �� �׸��� �̰� �����̾�.:0","(�Ҷ� ���� �޾Ҵ�) :0","��ÿ��� �Ҷ� ���� ������. �̰� ������ ��. �׷� �ʸ� �� �ݰ��ٰžߡ� :0"});

        //20�� ��ȭ ������ ���� ������Ʈ Ȱ��ȭ(�κ��丮�� �Ҷ�� �߰�)
        talkData.Add(4000 + 21, new string[] { "�ȳ�, ��Ʋ��Ƽ���� ����� ������ ���̾�.:0", "(..?):0", "�̰��� �ֹε��� �����ô�? �ٵ� ���� ���� ������� ���ڱ���...:0",
            "�ȳ��ϼ���. �̰� �����̿���.:0","�Ҷ���̱���? ����, ������ �����Ұ�.:0","��ÿ��, �̰��� ����ΰ���? �� �̰��� ��� �� �� �ִ� ����?:0","��� ���� ������ ���� �귯����.:0",
            "�츮 ���� �ΰ����� �̰��� �� �� �ִ� �͵� ������ ��������.:0","����� ���ֿ� ���� �����ó��..�ΰ��̶�� ���� ������Ű�� ���Ͽ�..:0","����� ����??:0"});
        //21�� ��ȭ ������ �̹��� �����ִ� �� <- ��ü������ ���?


        talkData.Add(1000 + 30, new string[] { "�������� ������ʹ�?:0","�������̿�?:0", "�׷�, �� �������� �̰��� ���� ��ϵ��� ��������. ���� ������ ���� �ð��� ���� ��������, �ѹ� ������ �� ���?:0",
        "�� ��� ���� �־ �ڸ��� ����߰ڱ���.:0"});
        //���Ϻ� �̵��ؼ� �����, �� �� ������ �� setActive���ָ� �� ��


        //�繰
        talkData.Add(100, new string[] { "(���ε��ΰ�..?):0" });

        //�ʻ�ȭ ����...
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
    public Sprite GetPlayerPortrait(int portraitIndex)
    {
        return PlayerprotraitData[portraitIndex];
    }
}