using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    [SerializeField]
    private TextMeshProUGUI talkText;
    [SerializeField]
    private GameObject talkPanel;
    private GameObject scanObject;
    public bool isAction;
    public int talkIndex;

    public void Action(GameObject scanObj)
    { 
        scanObject = scanObj; //넘겨받은 스캔된 오브젝트의
        ObjData objData = scanObject.GetComponent<ObjData>(); //정보를 가져와서
        Talk(objData.id, objData.isNpc); //Talk함수 호출하고
       
        talkPanel.SetActive(isAction); //panel 활성화/비활성화
    }

    void Talk(int id, bool isNpc)
    {
        string talkData = talkManager.GetTalk(id, talkIndex); //해당하는 대화내용 가져와서 

        if (talkData == null) //대화끝나면
        {
            isAction = false; //창없애고
            talkIndex = 0; //인덱스초기화한 다음
            return; //함수 종료
        }
        if (isNpc)
        {
            talkText.text = talkData;
        }
        else
        {
            talkText.text = talkData;
        }

        isAction = true;
        talkIndex++; //다음 문장으로
    }
}
