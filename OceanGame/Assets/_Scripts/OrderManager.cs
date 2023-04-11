using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    private PlayerController thePlayer; //�̺�Ʈ ���߿� Ű �Է� ó�� ����
    private List<MovingObject> chatacters;
    //MovingObject[]�� �����ع����� �������� NPC�� ���� �ٸ��� ���������� �׷��� ����Ʈ���
    //List���� Add(), Remove(), Clear() ���� ����Ʈ�� ���� �������� ���� ����

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
    }

    //ĳ���� ä���ִ� �Լ�
    public void preLoadCharacter()
    {
        chatacters = ToList();
    }
    public List<MovingObject> ToList()
    {
        List<MovingObject> tempList = new List<MovingObject>();
        MovingObject[] temp = FindObjectsOfType<MovingObject>(); //MovingObhect�� �޸� ��� ��ü�� ã�� ��ȯ��

        for(int i = 0; i<temp.Length; i++)
        {
            tempList.Add(temp[i]); //for���� ���� MovingObhect�� �޸� ��� ��ü�� ����Ʈ�� ����
        }
        return tempList;
    }

    //ĳ���Ͱ� �Ⱥ��̰� �ϱ�
    public void SetTransparent(string _name)
    {
        for (int i = 0; i < chatacters.Count; i++)
        {
            if (_name == chatacters[i].characterName) //�̸��� ��ġ�� ��� �Ⱥ��̰� �ϱ�
            {
                chatacters[i].gameObject.SetActive(false);
            }
        }
    }

    //ĳ���Ͱ� ���̰� �ϱ�
    public void SetUnTransparent(string _name)
    {
        for (int i = 0; i < chatacters.Count; i++)
        {
            if (_name == chatacters[i].characterName) //�̸��� ��ġ�� ��� ���̰� �ϱ�
            {
                chatacters[i].gameObject.SetActive(true);
            }
        }
    }

    //ĳ������ ����, ���� �����̰� �ϱ�
    public void Move(string _name, string _dir)//ĳ���� �̸��� ������ �˾Ƽ� �����̰� �ϱ�
    {
        for(int i =0; i<chatacters.Count; i++)
        {
            if(_name == chatacters[i].characterName) //�̸��� ��ġ�� ��� Move �Լ� �߻�
            {
                chatacters[i].Move(_dir);
            }
        }
    }

    //ĳ������ ���⸸ �ٲٱ�
    public void Turn(string _name, string _dir)
    {
        for (int i = 0; i < chatacters.Count; i++)
        {
            chatacters[i].animator.SetFloat("DirX", 0f);
            chatacters[i].animator.SetFloat("DirY", 0f);
            if (_name == chatacters[i].characterName) //�̸��� ��ġ�� ��� Move �Լ� �߻�
            {
                switch (_dir)
                {
                    case "UP":
                        chatacters[i].animator.SetFloat("DirY", 1f);
                        break;
                    case "DOWN":
                        chatacters[i].animator.SetFloat("DirY", -1f);
                        break;
                    case "LEFT":
                        chatacters[i].animator.SetFloat("DirX", -1f);
                        break;
                    case "RIGHT":
                        chatacters[i].animator.SetFloat("DirX", 1f);
                        break;

                }
                
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
