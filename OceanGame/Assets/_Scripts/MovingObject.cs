using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{

    public string characterName; //캐릭터이름을 알아내는 변수

    public static MovingObject instance;

    public float speed;
    public int walkCount;
    public int currentWalkCount;

    public Vector3 vector;

    private BoxCollider2D boxCollider;
    private LayerMask layerMask;
    public Animator animator;

    private float runSpeed;
    private float applyRunSpeed;
    private bool applyRunFlag = false;
    public bool canMove = true;

    private bool notCoroutine = false;

    public Queue<string> queue;
    //npc를 이동 명령했을 때 속도가 빨라지는 오류와 대기상태 애니메이션으로 변하지 않는 문제 해결을 위한 큐
    //FIFO, 선입선출 자료구조

    void Start()
    {
        queue = new Queue<string>();
    }

    private void Awake()
    {
        /*if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }*/
    }


    /*IEnumerator MoveCoroutine()
    {
        while (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {

           *//* if (Input.GetKey(KeyCode.LeftShift))
            {
                applyRunSpeed = runSpeed;
                applyRunFlag = true;
            }
            else
            {
                applyRunSpeed = 0;
                applyRunFlag = false;
            }


            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

            if (vector.x != 0)
                vector.y = 0;


            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);
            animator.SetBool("Walking", true);*//*



            while (currentWalkCount < walkCount)
            {
                if (vector.x != 0)
                {
                    transform.Translate(vector.x * (speed + applyRunSpeed), 0, 0);
                }
                else if (vector.y != 0)
                {
                    transform.Translate(0, vector.y * (speed + applyRunSpeed), 0);
                }
                if (applyRunFlag)
                    currentWalkCount++;
                currentWalkCount++;
                yield return new WaitForSeconds(0.01f);
            }
            currentWalkCount = 0;

        }
        //animator.SetBool("Walking", false);
        canMove = true;

    }*/



    // Update is called once per frame
    void Update()
    {

        /*if (canMove)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                canMove = false;
                StartCoroutine(MoveCoroutine());
            }
        }*/

    }

    public void Move(string _dir, int _frequency = 5)
    {
        queue.Enqueue(_dir); //실행될때마다 방향이 쌓인다.
        if (!notCoroutine)
        {
            notCoroutine = true;
            StartCoroutine(MoveCoroutine(_dir, _frequency));
        }
        
    }

    IEnumerator MoveCoroutine(string _dir, int _frequency)
    {

        while(queue.Count != 0) //큐가 0이 아닐 경우에
        {
            string direction = queue.Dequeue();
            vector.Set(0, 0, vector.z);

            //한번 돌고나면 초기화
            vector.Set(0, 0, vector.z);

            switch (direction)
            {
                case "UP":
                    vector.y = 1f;
                    break;
                case "DOWN":
                    vector.y = -1f;
                    break;
                case "RIGHT":
                    vector.x = 1f;
                    break;
                case "LEFT":
                    vector.x = -1f;
                    break;
            }

            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);
            animator.SetBool("Walking", true);


            while (currentWalkCount < walkCount)
            {
                transform.Translate(vector.x * (speed), vector.y * (speed), 0);
                currentWalkCount++;
                yield return new WaitForSeconds(0.01f);
            }
            currentWalkCount = 0;

            if (_frequency != 5)
            {
                animator.SetBool("Walking", false);
            }
        }
        animator.SetBool("Walking", false);
        notCoroutine = false;
    }

    protected bool CheckCollsion()
    {
        RaycastHit2D hit;

        Vector2 start = transform.position; //A지점 캐릭터의 현재 위치값
        Vector2 end = start + new Vector2(vector.x * speed * walkCount, vector.y * speed * walkCount); //b지점, 캐릭터가 이동하고자하는 위치값

        boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, layerMask);
        boxCollider.enabled = true;

        //레이캐스트를 싸서 부딪치는 곳이 있으면 움직임을 멈춰라
        if (hit.transform != null)
        {
            return true;
        }
        return false;
    }
}