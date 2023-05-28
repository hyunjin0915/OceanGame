using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public MonsterData monsterData;

    public GameObject canvas;

    //등급에 따라 몬스터를 초기 설정합니다...
    private void Awake()
    {
        monsterData.spriteRenderer = GetComponent<SpriteRenderer>();
        monsterData.rigidbody2D = GetComponent<Rigidbody2D>();

        switch (monsterData.grade)
        {
            case MonsterData.Grade.S:
                monsterData.Power = 3.5f;
                break;

            case MonsterData.Grade.A:
                monsterData.Power = 3.0f;
                break;

            case MonsterData.Grade.B:
                monsterData.Power = 2.5f;
                break;

            case MonsterData.Grade.C:
                monsterData.Power = 2.0f;
                break;

            case MonsterData.Grade.D:
                monsterData.Power = 1.5f;
                break;

            case MonsterData.Grade.E:
                monsterData.Power = 1.0f;
                break;

            case MonsterData.Grade.F:
                monsterData.Power = 0.5f;
                break;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        Vector3 hpBarPos =
            Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + 3.0f, 0));
        monsterData.hpBar.position = hpBarPos;
    }

    void Move()
    {
        if (monsterData.isChaseTime)
        {
            //플레이어의 위치를 받아온 후 플레이어 쪽으로 이동
            Vector3 playerPos = GameManager.Instance.player.transform.position;
            Vector3 moveDir = (playerPos - transform.position).normalized;

            monsterData.rigidbody2D.velocity = moveDir * monsterData.Speed * Time.fixedDeltaTime;
        }
        else
        {
            if (monsterData.maxNormalMoveDelay < monsterData.curNormalMoveDelay)
            {
                float randomValue_X = Random.Range(-1.0f, 1.0f);
                float randomValue_Y = Random.Range(-1.0f, 1.0f);

                monsterData.rigidbody2D.velocity = new Vector2(randomValue_X, randomValue_Y).normalized * monsterData.Speed * Time.deltaTime;

                monsterData.curNormalMoveDelay = 0.0f;
                monsterData.stopNormalMoveDelay = Random.Range(0.0f, monsterData.maxNormalMoveDelay);
            }
            else if (monsterData.curNormalMoveDelay > monsterData.stopNormalMoveDelay)
                monsterData.rigidbody2D.velocity = Vector2.zero;

            monsterData.curNormalMoveDelay += Time.fixedDeltaTime;
        }
    }

    //공격을 받았을 때 호출되는 함수
    protected virtual void OnHit(float damage)
    {
        FadeOut();

        monsterData.HP -= damage;
        if (monsterData.HP <= 0)
        {
            //죽기전에 사망모션을 보여주기 위해
            Invoke("Die", 1);
        }
    }

    void FadeOut()
    {
        monsterData.spriteRenderer.color = new Color(1, 1, 1, 0.2f);
        Invoke("FadeIn", 0.1f);
    }

    void FadeIn()
    {
        monsterData.spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    //죽을 때 각종 처리를 하는 함수
    void Die()
    {
        /*
         * (여기에 아이템을 드롭하는 함수 추가)
        */
        Destroy(gameObject);
    }

    //플레이어가 사정 거리안에 들어왔는지 확인하는 함수
    protected void CheckPlayerIsInside()
    {
        Vector2 playerPos = GameManager.Instance.player.transform.position;

        if (Vector2.Distance(playerPos, this.transform.position) <= monsterData.checkPlayerInsideDistance)
        {
            monsterData.isChaseTime = true;

            //임시 확인용
            monsterData.spriteRenderer.color = new Color(1, 0, 0);
        }
        else
        {
            monsterData.isChaseTime = false;
            monsterData.spriteRenderer.color = new Color(0, 1, 0);
        }
    }

}
