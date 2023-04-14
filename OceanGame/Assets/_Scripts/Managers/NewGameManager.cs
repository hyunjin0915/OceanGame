using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewGameManager : Singleton<NewGameManager>
{
    public GameObject portraitImg;
    public TextMeshProUGUI talkText; //대화창 텍스트
                                     //  [SerializeField]
    public GameObject talkPanel; //대화창
    [SerializeField]
    private float textSpeed; //대화글 써지는 속도
    [SerializeField] float fadeSpeed;

    private GameObject scanObject; //앞에있는 물체 판별
    public bool isAction;
    public int talkIndex; //몇번째 문장 가져올지 결정
    public bool isnowTalking; //말하고있는데 스페이스바 눌러서 다음 문자열로 넘어가버리지 않게

    public GameObject MeueSet; //메뉴창
    public GameObject player; //플레이어

    Dialogue[] dialogues;
    int lineCount = 0; //대화 카운트
    int contextCount = 0; //대사 카운트

    private void Start()
    {
        Debug.Log(QuestManager.Instance.CheckQuest());
        talkText.text = string.Empty;

        //게임 시작할때 로딩한 것을 불러옴 여기 수정 필요할듯 로딩버튼을 시작화면에 만들거면 수정필요
        GameLoad();
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
            if (MeueSet.activeSelf) //만약 메뉴가 커져있을 경우에
            {
                MeueSet.SetActive(false); //꺼진다.
                Time.timeScale = 1f; //게임 속도를 1배속으로 전환한다.
            }
            else //아니라면
                MeueSet.SetActive(true); //MeueSet 활성화
            //게임 속도를 0배속으로 전환한다.
            Time.timeScale = 0f;

        }


    }

    public void Action(GameObject scanObj)
    {
        scanObject = scanObj; //넘겨받은 스캔된 오브젝트의
        ObjData objData = scanObject.GetComponent<ObjData>(); //정보를 가져와서
        NewTalk(scanObj.transform.GetComponent<InteractionEvent>().GetDialogue());

        //Talk(objData.id, objData.isNpc); //Talk함수 호출하고

        talkPanel.SetActive(isAction); //panel 활성화/비활성화

    }
    void NewTalk(Dialogue[] p_dialogue)
    {
        dialogues = p_dialogue;
        
        string talkData = string.Empty;

        if(++contextCount< dialogues[lineCount].contexts.Length)
        {
            talkText.text = string.Empty;
            talkData = dialogues[lineCount].contexts[contextCount];
            StartCoroutine(TypeLine(talkData)); //대화창입력 코루틴 실행
        }
        else
        {
            talkText.text = string.Empty;
            if(++lineCount<dialogues.Length)
            {
                contextCount = 0;
                talkData = dialogues[lineCount].contexts[contextCount];
                StartCoroutine(TypeLine(talkData)); //대화창입력 코루틴 실행
            }
            else
            {
                isAction = false;
                contextCount = 0;
                lineCount = 0;
                return;
            }
        }
        isAction = true;
    }

    void ChangeSprite()
    {
        if(dialogues[lineCount].spriteName[contextCount]!="")
        {
            Debug.Log(dialogues[lineCount].spriteName[contextCount]);
            StartCoroutine(SpriteChangeCoroutine(dialogues[lineCount].spriteName[contextCount]));
        }
    }
    IEnumerator TypeLine(string talking) //한글자씩 써지는 효과
    {
        ChangeSprite();
        string t_replace = talking.Replace("'", ",");
        foreach (char c in t_replace.ToCharArray())
        {
            isnowTalking = true;
            talkText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        isnowTalking = false;
    }
    public IEnumerator SpriteChangeCoroutine( string p_SpriteName)
    {
        Image thisImage = portraitImg.GetComponent<Image>();
        //SpriteRenderer t_spriteRenderer = portraitImg.GetComponent<SpriteRenderer>();
        Sprite t_sprite = Resources.Load("Characters/" + p_SpriteName, typeof(Sprite)) as Sprite;

        if (!CheckSameSprite(thisImage.sprite, t_sprite))
        {
            Color t_color = thisImage.color;
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

    //게임 저장 함수
    public void GameSave()
    {
        //PlayerPrefs : 간단한 데이터 저장기능을 지원하는 클래스
        PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
        PlayerPrefs.SetFloat("QuestId", QuestManager.Instance.questId);
        PlayerPrefs.SetFloat("QuestActionIndex", QuestManager.Instance.questActionIndex);
        PlayerPrefs.Save(); //레지스트리에 위에있는 플레이어 위치, 퀘스트를 저장해준다.

        MeueSet.SetActive(false); //세이브가 되었으므로 메뉴창 꺼짐
    }

    //게임 불러오기 함수
    public void GameLoad()
    {
        //최초 게임 실행했을 땐 데이터가 없으므로 예외처리 로직 작성 
        if (!PlayerPrefs.HasKey("PlayerX"))
            return; //로드를 하지 말라는 것

        //게임데이터 저장한 것을 불러옴
        float x = PlayerPrefs.GetFloat("PlayerX");
        float y = PlayerPrefs.GetFloat("PlayerY");
        int questId = PlayerPrefs.GetInt("QuestId");
        int questActionIndex = PlayerPrefs.GetInt("QuestActionIndex");

        //불러온 데이터를 게임 오브젝트에 적용
        player.transform.position = new Vector3(x, y, 0);
        QuestManager.Instance.questId = questId;
        QuestManager.Instance.questActionIndex = questActionIndex;

        //이외에도 퀘스트에 관련된 오브젝트 저장이라든가
        //인벤토리 저장이 필요 이에 대한 자료 찾아보겠음
    }

}
