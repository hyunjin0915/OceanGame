using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    private PlayerController thePlayer; //이벤트 도중에 키 입력 처리 방지
    private List<MovingObject> chatacters;
    //MovingObject[]로 선언해버리면 지역마다 NPC의 수가 다른데 정해져버림 그래서 리스트사용
    //List에는 Add(), Remove(), Clear() 있음 리스트의 갯수 자유자재 조정 가능

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
    }

    //캐릭터 채워넣는 함수
    public void preLoadCharacter()
    {
        chatacters = ToList();
    }
    public List<MovingObject> ToList()
    {
        List<MovingObject> tempList = new List<MovingObject>();
        MovingObject[] temp = FindObjectsOfType<MovingObject>(); //MovingObhect가 달린 모든 객체를 찾아 반환함

        for(int i = 0; i<temp.Length; i++)
        {
            tempList.Add(temp[i]); //for문이 돌면 MovingObhect가 달린 모든 객체가 리스트로 들어옴
        }
        return tempList;
    }

    //캐릭터가 안보이게 하기
    public void SetTransparent(string _name)
    {
        for (int i = 0; i < chatacters.Count; i++)
        {
            if (_name == chatacters[i].characterName) //이름이 일치할 경우 안보이게 하기
            {
                chatacters[i].gameObject.SetActive(false);
            }
        }
    }

    //캐릭터가 보이게 하기
    public void SetUnTransparent(string _name)
    {
        for (int i = 0; i < chatacters.Count; i++)
        {
            if (_name == chatacters[i].characterName) //이름이 일치할 경우 보이게 하기
            {
                chatacters[i].gameObject.SetActive(true);
            }
        }
    }

    //캐릭터의 방향, 동작 움직이게 하기
    public void Move(string _name, string _dir)//캐릭터 이름과 방향을 알아서 움직이게 하기
    {
        for(int i =0; i<chatacters.Count; i++)
        {
            if(_name == chatacters[i].characterName) //이름이 일치할 경우 Move 함수 발생
            {
                chatacters[i].Move(_dir);
            }
        }
    }

    //캐릭터의 방향만 바꾸기
    public void Turn(string _name, string _dir)
    {
        for (int i = 0; i < chatacters.Count; i++)
        {
            chatacters[i].animator.SetFloat("DirX", 0f);
            chatacters[i].animator.SetFloat("DirY", 0f);
            if (_name == chatacters[i].characterName) //이름이 일치할 경우 Move 함수 발생
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
