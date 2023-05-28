using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster Data", menuName = "Scriptable Object/Monster Data", order = int.MaxValue)]
public class MonsterData : ScriptableObject
{
    public enum Grade
    {
        S,
        A,
        B,
        C,
        D,
        E,
        F,
    }

    //몬스터 이름
    public string Name { get;  set; }

    //몬스터 체력 : 초기 100으로 고정
    public float HP { get;  set; } = 100;

    //몬스터 방어력
    public float Def { get;  set; }

    //몬스터 스피드
    public float Speed { get;  set; } = 50;

    //몬스터 공격력
    public float Power { get;  set; } = 1;

    //플레이어 인식 거리
    public float checkPlayerInsideDistance = 6.0f;

    //몬스터의 움직임방식이 추적방식인가?
    public bool isChaseTime = true;

    //몬스터 움직임 간격에 관한 변수
    public float maxNormalMoveDelay = 2.0f;
    public float curNormalMoveDelay = 3.0f;
    public float stopNormalMoveDelay = 0.0f;

    //몬스터 등급
    public Grade grade;

    public GameObject hpBarPrefab; 

    public RectTransform hpBar;

    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rigidbody2D;
}
