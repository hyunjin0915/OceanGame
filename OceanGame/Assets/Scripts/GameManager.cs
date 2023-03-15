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
            talkText.text = talkData;
        }
        else
        {
            talkText.text = talkData;
        }

        isAction = true;
        talkIndex++; //���� ��������
    }
}
