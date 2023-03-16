using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    [SerializeField]
    public TextMeshProUGUI talkText; //대화창 텍스트
    [SerializeField]
    private GameObject talkPanel; //대화창
    [SerializeField]
    private float textSpeed; //대화글 써지는 속도

    private GameObject scanObject; //앞에있는 물체 판별
    public bool isAction;
    public int talkIndex; //몇번째 문장 가져올지 결정
    public bool isnowTalking; //말하고있는데 스페이스바 눌러서 다음 문자열로 넘어가버리지 않게

    private void Start()
    {
        talkText.text = string.Empty; 
    }
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
            talkText.text = string.Empty; //텍스트 비우고
            StartCoroutine(TypeLine(id)); //대화창입력 코루틴 실행
        }
        else
        {
            talkText.text = string.Empty;
            StartCoroutine(TypeLine(id));
        }

        isAction = true;
        talkIndex++; //다음 문장으로
    }

    IEnumerator TypeLine(int id) //한글자씩 써지는 효과
    {
        foreach(char c in talkManager.GetTalk(id, talkIndex).ToCharArray())
        {
            isnowTalking = true;
            talkText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        isnowTalking = false;
    }

}
