using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DatabaseManager : Singleton<DatabaseManager>
{
    [SerializeField] string csv_FileName;

    public  Dictionary<int, Dialogue> QuestDic = new ();

    public static bool isFinish = false; //전부 파싱되어 저장되었는지 여부 


    public override void  Awake()
    {
        Debug.Log("어웨이크호출");
        base.Awake();
        if (!isFinish)
        {
            gameObject.GetComponent<DialogueParser>().Parse(csv_FileName);
            isFinish = true;
            if (isFinish) Debug.Log("파싱 완");
        }

    }
    public Dialogue GetDialogue(int _talkId)
    {
        if (!QuestDic.ContainsKey(_talkId))
        {
            //해당 퀘스트 진행 순서 대사가 없을 때...
            if (!QuestDic.ContainsKey(_talkId - _talkId % 10))
            {
                //퀘스트 맨 처음 대사마저 없을 때 - 기본대사를 가지고오기
                return GetDialogue(_talkId - _talkId % 100);
            }
            else
            {
                return GetDialogue(_talkId - _talkId % 10);
            }

        }
       
            return QuestDic[_talkId];

    }
   
}