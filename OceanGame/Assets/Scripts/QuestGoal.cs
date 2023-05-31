using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal 
{
    public GoalType goalType;
    public bool isReached;
    
    public void FindGameObj()
    {
        
        if (goalType == GoalType.Find)
        {
            isReached = true;
        }
    }

    public void DeliverGameObj()
    {
        if (goalType == GoalType.Deliver)
            isReached = true;
    }

    public void ClearMiniGame()
    {
        if (goalType == GoalType.MiniGame)
            isReached = true;
    }
        
}

public enum GoalType
{
    Find,
    Deliver,
    MiniGame
}

