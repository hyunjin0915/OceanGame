using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //�ν����� â�� ������ ���� �ۼ�
public class NPCMove //npc�� �ڵ����� �����̴� ��(��������)
{
    [Tooltip("NPCMove�� üũ�ϸ� NPC�� ������")] //Tooltip ����ϸ� �ν����� â���� ������ ���� �����
    public bool NPCmove;

    public string[] direction; //npc�� ������ ���� ����

    //frequency�� ����
    [Range(1,5)] [Tooltip("1 =õõ��, 2 = ���� õõ�� 3 = ����, 4 = ������, 5 = ���������� ")]
    public int frequency; //npc�� ������ �������� �󸶳� ���� �ӵ��� �����ϰ��ΰ�.
}

//playercontroller�� �θ� Ŭ������ ��ӹ���
public class NpcManager : MovingObject
{
    [SerializeField]
    public NPCMove npc;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(MoveCoroutine());
        queue = new Queue<string>();
    }

    public void SetMove()
    {
        StartCoroutine(MoveCoroutine());
    }

    public void SetNotMove()
    {

    }

    

   
    //������ �˰��� => �������� �����ϰ� �����̴� ��
    IEnumerator MoveCoroutine()
    {
        if(npc.direction.Length != 0)
        {
            for(int i = 0; i<npc.direction.Length; i++)
            {
                switch (npc.frequency)
                {
                    case 1:
                        //4�� ���
                        yield return new WaitForSeconds(4f);
                        break;
                    case 2:
                        yield return new WaitForSeconds(3f);
                        break;
                    case 3:
                        yield return new WaitForSeconds(2f);
                        break;
                    case 4:
                        yield return new WaitForSeconds(1f);
                        break;
                    case 5:
                        break;

                }

                //npcCanMove�� true�� �ɶ����� ���� ���
                yield return new WaitUntil(() => queue.Count<2);

                base.Move(npc.direction[i], npc.frequency);

                //�տ� ���� ������ ���߱�
                bool checkCollsionFlag = base.CheckCollsion();
                if (checkCollsionFlag)
                    break;

                //�������� �̵� ����
                if (i == npc.direction.Length - 1)
                {
                    i = -1;
                }
            }
        }
    }
}
