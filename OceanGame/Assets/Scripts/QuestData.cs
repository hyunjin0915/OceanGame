using System.Collections;
using System.Collections.Generic;

public class QuestData 
{
    public string questName;
    public int[] npcId; //����Ʈ�� ���õ� npc

    public QuestData(string name, int[] npc) //����ü ������ ���� ������ �ʿ���
    {
        questName = name;
        npcId = npc;
    }
}
