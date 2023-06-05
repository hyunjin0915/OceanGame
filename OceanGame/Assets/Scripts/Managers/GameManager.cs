using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : Singleton<GameManager>
{
    public Image PlayerportraitImg;
    public Image NPCportraitImg;
    

    public TextMeshProUGUI talkText; //대화창 텍스트

    public GameObject talkPanel; //대화창
    [SerializeField]
    private float textSpeed; //대화글 써지는 속도
    [SerializeField] float fadeSpeed;
    int contextCount = 0; //대사 카운트
    Dialogue dialogues;


    private GameObject scanObject; //앞에있는 물체 판별
    public bool isAction; //talkPanel 컨드롤 및 말할 때못움직이게
    public int talkIndex; //몇번째 문장 가져올지 결정
    public bool isnowTalking; //말하고있는데 스페이스바 눌러서 다음 문자열로 넘어가버리지 않게

   
    public GameObject player; //플레이어
    public Player playerLogic; //플레이어 로직

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
        scanObject = scanObj; //넘겨받은 스캔된 오브젝트의
        if (scanObj.CompareTag("Gate"))
        {
            scanObj.GetComponent<NewSceneTransition>().SceneTransition();
            return;
        }
        ObjData objData = scanObject.GetComponent<ObjData>(); //정보를 가져와서

        if(!objData.isNpc) //상대가 사물이면
        {
            string objName = QuestManager.Instance.CheckQuestObjs(objData.id); //해당 사물 획득 체크
            QuestManager.Instance.CheckQuest(objData.id);
            Talk(objName + "을(를) 얻었다!",objData.id);

            isAction = true;
            talkPanel.SetActive(isAction); //panel 활성화/비활성화
            
            scanObj.SetActive(false);
            return;
        }
        else
        {
            int questTalkIndex = QuestManager.Instance.GetQuestTalkIndex();
            Dialogue dialoguess = DatabaseManager.Instance.GetDialogue(objData.id + questTalkIndex);
            Talk(dialoguess, objData.id);


            talkPanel.SetActive(isAction); //panel 활성화/비활성화

        }

    }
    void Talk(string p_dialogue, int _id)
    {
        talkText.text = string.Empty;
        StartCoroutine(TypeLine(p_dialogue, _id)); //대화창입력 코루틴 실행
      //  isAction = false;
       // talkPanel.SetActive(isAction);
    }
    void Talk(Dialogue p_dialogue, int _id)
    {
        dialogues = p_dialogue;
        string talkData = string.Empty;

        if (contextCount < p_dialogue.contexts.Length)
        {
            talkText.text = string.Empty;
            talkData = p_dialogue.contexts[contextCount];
            StartCoroutine(TypeLine(talkData)); //대화창입력 코루틴 실행
            contextCount++;
            isAction = true;
        }
        else
        {
            isAction = false;
            contextCount = 0;
            QuestManager.Instance.CheckQuest(_id);
            return;

        }
        
    }

    void ChangeSprite()
    {
        if (dialogues.spriteName[contextCount] != "")
        {
            Debug.Log(dialogues.spriteName[contextCount]);
            StartCoroutine(SpriteChangeCoroutine(dialogues.spriteName[contextCount]));
        }
    }

    IEnumerator TypeLine(string talking) //한글자씩 써지는 효과
    {
        ChangeSprite();
        string t_replace = talking.Replace("'", ","); //csv파일은쉼표구분이라서
        foreach (char c in t_replace.ToCharArray())
        {
            isnowTalking = true;
            talkText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        isnowTalking = false;
    }
    IEnumerator TypeLine(string talking, int _id) //사물에말걸때 초상화x
    {
        Color color = PlayerportraitImg.color;
        color.a = 0.0f;
        PlayerportraitImg.color = color;
        NPCportraitImg.color = color;

        string t_replace = talking.Replace("'", ","); //csv파일은쉼표구분이라서
        foreach (char c in t_replace.ToCharArray())
        {
            isnowTalking = true;
            talkText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        yield return new WaitForSeconds(2.0f);
        isnowTalking = false;
        isAction = false;
        talkPanel.SetActive(isAction);
    }
    public IEnumerator SpriteChangeCoroutine(string p_SpriteName)
    {
        Image thisImage = NPCportraitImg.GetComponent<Image>();
        Color t_color = thisImage.color;

        if (dialogues.talkerId[contextCount]==10) //플레이어가 말하는 경우
        {
           thisImage = PlayerportraitImg.GetComponent<Image>();
        }
        if(dialogues.talkerId[contextCount] %17==0)
        {
            t_color.a = 0;
            thisImage.color = t_color;
        }
        Sprite t_sprite = Resources.Load("Characters/" + p_SpriteName, typeof(Sprite)) as Sprite;

        if (!CheckSameSprite(thisImage.sprite, t_sprite))
        {
            t_color = thisImage.color;
            t_color.a = 0;
            thisImage.color = t_color;

            thisImage.sprite = t_sprite;

            while (t_color.a < 1)
            {
                t_color.a += fadeSpeed;
                thisImage.color = t_color;
                yield return null;
            }
        }
        
    }
    bool CheckSameSprite(Sprite p_targetSprite, Sprite p_Sprite)
    {
        if (p_targetSprite == p_Sprite)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    //게임종료 함수
    public void GameExit()
    {
        Application.Quit();
    }



    //기존에 있던 세이브, 로드 기능을 세이브매니저에 옮겼음!
}
