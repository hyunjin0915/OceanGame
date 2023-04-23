using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : Singleton<GameManager>
{
    //public TalkManager talkManager;
   // public QuestManager questManager;
   // [SerializeField]
    public Image portraitImg;
  //  [SerializeField]
    public TextMeshProUGUI talkText; //대화창 텍스트
  //  [SerializeField]
    public GameObject talkPanel; //대화창
    [SerializeField]
    private float textSpeed; //대화글 써지는 속도

    private GameObject scanObject; //앞에있는 물체 판별
    public bool isAction;
    public int talkIndex; //몇번째 문장 가져올지 결정
    public bool isnowTalking; //말하고있는데 스페이스바 눌러서 다음 문자열로 넘어가버리지 않게

   
    public GameObject player; //플레이어

    public string sceneName; //씬 이름
    public string mapName; //맵 이름

    public string curCharacterName; //현재 캐릭터 이름
    public int curLv; //현재 레벨
    public int curHp; //현재 체력
    public float playTime = 0;



    private void Start()
    {
        Debug.Log(QuestManager.Instance.CheckQuest());
        talkText.text = string.Empty;

        //게임 시작할때 로딩한 것을 불러옴 여기 수정 필요할듯 로딩버튼을 시작화면에 만들거면 수정필요

        //GameLoad();

    }

    public override void Awake()
    {

        base.Awake();

    }
    void Update()
    {


        //ESC키를 눌렀을 때 메뉴창이 나오도록 함
        if (Input.GetButtonDown("Cancel"))
        {
            UIManager.Instance.SetUIOn(UIManager.Instance.menuSet);

            //게임 속도를 0배속으로 전환한다.
            Time.timeScale = 0f;

        }

    }



    public void Action(GameObject scanObj)
    {
        Debug.Log("Action 말걸기");
        scanObject = scanObj; //넘겨받은 스캔된 오브젝트의
        ObjData objData = scanObject.GetComponent<ObjData>(); //정보를 가져와서
        Talk(objData.id, objData.isNpc); //Talk함수 호출하고
       
        talkPanel.SetActive(isAction); //panel 활성화/비활성화

    }

    void Talk(int id, bool isNpc)
    {
        int questTalkIndex = QuestManager.Instance.GetQuestTalkIndex(id);
        string talkData = TalkManager.Instance.GetTalk(id+ questTalkIndex, talkIndex); //해당하는 대화내용 가져와서 

        if (talkData == null) //대화끝나면
        {
            isAction = false; //창없애고
            talkIndex = 0; //인덱스초기화한 다음
            Debug.Log(QuestManager.Instance.CheckQuest(id)); //대화가 끝나면 퀘스트의 다음 대화로
            return; //함수 종료
        }
        if (isNpc)
        {
            talkText.text = string.Empty; //텍스트 비우고

            string realTalkData = talkData.Split(':')[0];
            StartCoroutine(TypeLine(realTalkData)); //대화창입력 코루틴 실행

            portraitImg.color = new Color(1, 1, 1, 1);
            portraitImg.sprite = TalkManager.Instance.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
        }
        else
        {
            talkText.text = string.Empty;
            StartCoroutine(TypeLine(talkData));

            portraitImg.color = new Color(1, 1, 1, 0);

        }

        isAction = true;
        talkIndex++; //다음 문장으로
    }

    IEnumerator TypeLine(string talking) //한글자씩 써지는 효과
    {
        foreach (char c in talking.ToCharArray())
        {
            isnowTalking = true;
            talkText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        isnowTalking = false;
    }

    //게임종료 함수
    public void GameExit()
    {
        Application.Quit();
    }



    //기존에 있던 세이브, 로드 기능을 세이브매니저에 옮겼음!
}
