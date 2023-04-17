using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEvent : MonoBehaviour
{
    [SerializeField] DialogueEvent dialogue;
    
   /* public Dialogue[] GetDialogue(int npcId)
    {
        // 몇번째줄을 가져올지 작성
        dialogue.dialogues = DatabaseManager.Instance.GetDialogue(npcId);

        return dialogue.dialogues;
    }*/
}