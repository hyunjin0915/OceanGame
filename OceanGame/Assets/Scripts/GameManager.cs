using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
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
        string talkData = talkManager.GetTalk(id, talkIndex); //�ش��ϴ� ��ȭ���� �����ͼ� 

        if (talkData == null) //��ȭ������
        {
            isAction = false; //â���ְ�
            talkIndex = 0; //�ε����ʱ�ȭ�� ����
            return; //�Լ� ����
        }
        if (isNpc)
        {
            talkText.text = string.Empty; //�ؽ�Ʈ ����
            StartCoroutine(TypeLine(id)); //��ȭâ�Է� �ڷ�ƾ ����
        }
        else
        {
            talkText.text = string.Empty;
            StartCoroutine(TypeLine(id));
        }

        isAction = true;
        talkIndex++; //���� ��������
    }

    IEnumerator TypeLine(int id) //�ѱ��ھ� ������ ȿ��
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
