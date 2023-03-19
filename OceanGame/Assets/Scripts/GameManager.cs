using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public QuestManager questManager;
    [SerializeField]
    public Image portraitImg;
    [SerializeField]
    public TextMeshProUGUI talkText; //��ȭâ �ؽ�Ʈ
    [SerializeField]
    private GameObject talkPanel; //��ȭâ
    [SerializeField]
    private float textSpeed; //��ȭ�� ������ �ӵ�

    private GameObject scanObject; //�տ��ִ� ��ü �Ǻ�
    public bool isAction;
    public int talkIndex; //���° ���� �������� ����
    public bool isnowTalking; //���ϰ��ִµ� �����̽��� ������ ���� ���ڿ��� �Ѿ������ �ʰ�

    private void Start()
    {
        Debug.Log(questManager.CheckQuest());
        talkText.text = string.Empty; 
    }
    public void Action(GameObject scanObj)
    { 
        scanObject = scanObj; //�Ѱܹ��� ��ĵ�� ������Ʈ��
        ObjData objData = scanObject.GetComponent<ObjData>(); //������ �����ͼ�
        Talk(objData.id, objData.isNpc); //Talk�Լ� ȣ���ϰ�
       
        talkPanel.SetActive(isAction); //panel Ȱ��ȭ/��Ȱ��ȭ

    }

    void Talk(int id, bool isNpc)
    {
        int questTalkIndex = questManager.GetQuestTalkIndex(id);
        string talkData = talkManager.GetTalk(id+ questTalkIndex, talkIndex); //�ش��ϴ� ��ȭ���� �����ͼ� 

        if (talkData == null) //��ȭ������
        {
            isAction = false; //â���ְ�
            talkIndex = 0; //�ε����ʱ�ȭ�� ����
            Debug.Log(questManager.CheckQuest(id)); //��ȭ�� ������ ����Ʈ�� ���� ��ȭ��
            return; //�Լ� ����
        }
        if (isNpc)
        {
            talkText.text = string.Empty; //�ؽ�Ʈ ����

            //int questTalkIndex = questManager.GetQuestTalkIndex(id);
            //string TalkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);
            string realTalkData = talkData.Split(':')[0];
            StartCoroutine(TypeLine(realTalkData)); //��ȭâ�Է� �ڷ�ƾ ����

            portraitImg.color = new Color(1, 1, 1, 1);
            portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
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
      //  int questTalkIndex = questManager.GetQuestTalkIndex(id);
      //  string TalkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);
      //  string realTalkData = TalkData.Split(':')[0];
        foreach (char c in talking.ToCharArray())
        {
            isnowTalking = true;
            talkText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        isnowTalking = false;
    }

}
