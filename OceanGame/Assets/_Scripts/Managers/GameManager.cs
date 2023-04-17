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
    public TextMeshProUGUI talkText; //��ȭâ �ؽ�Ʈ
  //  [SerializeField]
    public GameObject talkPanel; //��ȭâ
    [SerializeField]
    private float textSpeed; //��ȭ�� ������ �ӵ�

    private GameObject scanObject; //�տ��ִ� ��ü �Ǻ�
    public bool isAction;
    public int talkIndex; //���° ���� �������� ����
    public bool isnowTalking; //���ϰ��ִµ� �����̽��� ������ ���� ���ڿ��� �Ѿ������ �ʰ�
    
    public GameObject MeueSet; //�޴�â
    public GameObject player; //�÷��̾�

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
        Debug.Log("Action ���ɱ�");
        scanObject = scanObj; //�Ѱܹ��� ��ĵ�� ������Ʈ��
        ObjData objData = scanObject.GetComponent<ObjData>(); //������ �����ͼ�
        Talk(objData.id, objData.isNpc); //Talk�Լ� ȣ���ϰ�
       
        talkPanel.SetActive(isAction); //panel Ȱ��ȭ/��Ȱ��ȭ

    }

    void Talk(int id, bool isNpc)
    {
        int questTalkIndex = QuestManager.Instance.GetQuestTalkIndex(id);
        string talkData = TalkManager.Instance.GetTalk(id+ questTalkIndex, talkIndex); //�ش��ϴ� ��ȭ���� �����ͼ� 

        if (talkData == null) //��ȭ������
        {
            isAction = false; //â���ְ�
            talkIndex = 0; //�ε����ʱ�ȭ�� ����
            Debug.Log(QuestManager.Instance.CheckQuest(id)); //��ȭ�� ������ ����Ʈ�� ���� ��ȭ��
            return; //�Լ� ����
        }
        if (isNpc)
        {
            talkText.text = string.Empty; //�ؽ�Ʈ ����

            string realTalkData = talkData.Split(':')[0];
            StartCoroutine(TypeLine(realTalkData)); //��ȭâ�Է� �ڷ�ƾ ����

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
        talkIndex++; //���� ��������
    }

    IEnumerator TypeLine(string talking) //�ѱ��ھ� ������ ȿ��
    {
        foreach (char c in talking.ToCharArray())
        {
            isnowTalking = true;
            talkText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        isnowTalking = false;
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
