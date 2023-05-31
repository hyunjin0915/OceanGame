using System.Collections;
using System.Collections.Generic;

public class QuestData 
{
    public string questName;
    public int[] npcId; //퀘스트와 관련된 npc
    public QuestGoal Qgoal;

    public QuestData(string name, int[] npc) //구조체 생성을 위한 생성자 필요함
    {
        questName = name;
        npcId = npc;
    }
}
