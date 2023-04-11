using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //인스펙터 창에 나오기 위해 작성
public class NPCMove //npc가 자동으로 움직이는 것(마을에서)
{
    [Tooltip("NPCMove를 체크하면 NPC가 움직임")] //Tooltip 사용하면 인스펙터 창에서 변수에 대한 설명뜸
    public bool NPCmove;

    public string[] direction; //npc가 움직일 방향 설정

    //frequency의 범위
    [Range(1,5)] [Tooltip("1 =천천히, 2 = 조금 천천히 3 = 보통, 4 = 빠르게, 5 = 연속적으로 ")]
    public int frequency; //npc가 움직일 방향으로 얼마나 빠른 속도로 움직일것인가.
}

//playercontroller를 부모 클래스로 상속받음
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

    

   
    //움직임 알고리즘 => 마을에서 일정하게 움직이는 것
    IEnumerator MoveCoroutine()
    {
        if(npc.direction.Length != 0)
        {
            for(int i = 0; i<npc.direction.Length; i++)
            {
                switch (npc.frequency)
                {
                    case 1:
                        //4초 대기
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

                //npcCanMove가 true가 될때까지 무한 대기
                yield return new WaitUntil(() => queue.Count<2);

                base.Move(npc.direction[i], npc.frequency);

                //앞에 무언가 있으면 멈추기
                bool checkCollsionFlag = base.CheckCollsion();
                if (checkCollsionFlag)
                    break;

                //실질적인 이동 구간
                if (i == npc.direction.Length - 1)
                {
                    i = -1;
                }
            }
        }
    }
}
