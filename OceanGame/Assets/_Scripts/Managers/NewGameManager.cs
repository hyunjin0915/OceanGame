using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewGameManager : Singleton<NewGameManager>
{
    public GameObject portraitImg;
    public TextMeshProUGUI talkText; //��ȭâ �ؽ�Ʈ
                                     //  [SerializeField]
    public GameObject talkPanel; //��ȭâ
    [SerializeField]
    private float textSpeed; //��ȭ�� ������ �ӵ�
    [SerializeField] float fadeSpeed;

    private GameObject scanObject; //�տ��ִ� ��ü �Ǻ�
    public bool isAction;
    public int talkIndex; //���° ���� �������� ����
    public bool isnowTalking; //���ϰ��ִµ� �����̽��� ������ ���� ���ڿ��� �Ѿ������ �ʰ�

    public GameObject MeueSet; //�޴�â
    public GameObject player; //�÷��̾�

    Dialogue[] dialogues;
    int lineCount = 0; //��ȭ ī��Ʈ
    int contextCount = 0; //��� ī��Ʈ

    private void Start()
    {
        Debug.Log(QuestManager.Instance.CheckQuest());
        talkText.text = string.Empty;

        //���� �����Ҷ� �ε��� ���� �ҷ��� ���� ���� �ʿ��ҵ� �ε���ư�� ����ȭ�鿡 ����Ÿ� �����ʿ�
        GameLoad();
    }

    public override void Awake()
    {
        base.Awake();
    }
    void Update()
    {
        //ESCŰ�� ������ �� �޴�â�� �������� ��
        if (Input.GetButtonDown("Cancel"))
        {
            if (MeueSet.activeSelf) //���� �޴��� Ŀ������ ��쿡
            {
                MeueSet.SetActive(false); //������.
                Time.timeScale = 1f; //���� �ӵ��� 1������� ��ȯ�Ѵ�.
            }
            else //�ƴ϶��
                MeueSet.SetActive(true); //MeueSet Ȱ��ȭ
            //���� �ӵ��� 0������� ��ȯ�Ѵ�.
            Time.timeScale = 0f;

        }


    }

    public void Action(GameObject scanObj)
    {
        scanObject = scanObj; //�Ѱܹ��� ��ĵ�� ������Ʈ��
        ObjData objData = scanObject.GetComponent<ObjData>(); //������ �����ͼ�
        NewTalk(scanObj.transform.GetComponent<InteractionEvent>().GetDialogue());

        //Talk(objData.id, objData.isNpc); //Talk�Լ� ȣ���ϰ�

        talkPanel.SetActive(isAction); //panel Ȱ��ȭ/��Ȱ��ȭ

    }
    void NewTalk(Dialogue[] p_dialogue)
    {
        dialogues = p_dialogue;
        
        string talkData = string.Empty;

        if(++contextCount< dialogues[lineCount].contexts.Length)
        {
            talkText.text = string.Empty;
            talkData = dialogues[lineCount].contexts[contextCount];
            StartCoroutine(TypeLine(talkData)); //��ȭâ�Է� �ڷ�ƾ ����
        }
        else
        {
            talkText.text = string.Empty;
            if(++lineCount<dialogues.Length)
            {
                contextCount = 0;
                talkData = dialogues[lineCount].contexts[contextCount];
                StartCoroutine(TypeLine(talkData)); //��ȭâ�Է� �ڷ�ƾ ����
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
    IEnumerator TypeLine(string talking) //�ѱ��ھ� ������ ȿ��
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

    //�������� �Լ�
    public void GameExit()
    {
        Application.Quit();
    }

    //���� ���� �Լ�
    public void GameSave()
    {
        //PlayerPrefs : ������ ������ �������� �����ϴ� Ŭ����
        PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
        PlayerPrefs.SetFloat("QuestId", QuestManager.Instance.questId);
        PlayerPrefs.SetFloat("QuestActionIndex", QuestManager.Instance.questActionIndex);
        PlayerPrefs.Save(); //������Ʈ���� �����ִ� �÷��̾� ��ġ, ����Ʈ�� �������ش�.

        MeueSet.SetActive(false); //���̺갡 �Ǿ����Ƿ� �޴�â ����
    }

    //���� �ҷ����� �Լ�
    public void GameLoad()
    {
        //���� ���� �������� �� �����Ͱ� �����Ƿ� ����ó�� ���� �ۼ� 
        if (!PlayerPrefs.HasKey("PlayerX"))
            return; //�ε带 ���� ����� ��

        //���ӵ����� ������ ���� �ҷ���
        float x = PlayerPrefs.GetFloat("PlayerX");
        float y = PlayerPrefs.GetFloat("PlayerY");
        int questId = PlayerPrefs.GetInt("QuestId");
        int questActionIndex = PlayerPrefs.GetInt("QuestActionIndex");

        //�ҷ��� �����͸� ���� ������Ʈ�� ����
        player.transform.position = new Vector3(x, y, 0);
        QuestManager.Instance.questId = questId;
        QuestManager.Instance.questActionIndex = questActionIndex;

        //�̿ܿ��� ����Ʈ�� ���õ� ������Ʈ �����̶�簡
        //�κ��丮 ������ �ʿ� �̿� ���� �ڷ� ã�ƺ�����
    }

}
