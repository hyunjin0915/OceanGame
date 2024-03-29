using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerKind
{
    PlayerTest,

}
public abstract class Player : MonoBehaviour
{
    public string fromMapName; //transferMapName 변수 값 저장용
    public string toMapName; //transferMapName 변수 값 저장용

    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;
    private float applyRunSpeed;

    //필요한 컴포넌트들
    private Rigidbody2D myRigid;
    GameObject scanObj;
    // public GameManager manager;
    Vector2 movement; //플레이어 이동시 사용
    Vector3 dirVec; //플레이어가 바라보고 있는방향

    public Animator anim;// 애니메이터 변수
    float h;
    float v; //애니메이터용 변수
    bool isHorizonMove;

    //플레이어 체력
    [field: SerializeField]
    public float HP { get; private set; } = 3.0f;

    protected void Awake()
    {
        anim = GetComponent<Animator>(); //anim 변수선언
        myRigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Monster")
        {
            float monsterDmg = collision.gameObject.GetComponent<Monster>().monsterData.Power;
            OnHit(monsterDmg);
        }
    }
    protected void Update()
    {
        movement.x = GameManager.Instance.isAction ? 0 : Input.GetAxisRaw("Horizontal");
        movement.y = GameManager.Instance.isAction ? 0 : Input.GetAxisRaw("Vertical");

        //Move Value
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        if (anim.GetBool("isChange"))
        {
            if (v == 1)
                dirVec = Vector3.up;
            else if (v == -1)
                dirVec = Vector3.down;
            else if (h == 1)
                dirVec = Vector3.right;
            else if (h == -1)
                dirVec = Vector3.left;
        }


        if (movement.x > 0)
        {
            dirVec = Vector3.right;
        }
        if (movement.x < 0)
        {
            dirVec = Vector3.left;
        }
        if (movement.y > 0)
        {
            dirVec = Vector3.up;
        }
        if (movement.y < 0)
        {
            dirVec = Vector3.down;
        }

        //Check Button Down & up
        bool hDown = Input.GetButtonDown("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool hUp = Input.GetButtonDown("Horizontal");
        bool vUp = Input.GetButtonDown("Vertical");


        //check Horizontal Move
        if (hDown || vUp)
        {
            isHorizonMove = true;
        }
        else if (hUp || vDown)
        {
            //isHorizonMove = h != 0;
            isHorizonMove = (hDown || hUp) || (vUp && h != 0);
        }

        //애니메이션
        if (anim.GetInteger("hAxisRaw") != h)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)h);
            //anim.SetTrigger("isChange 1");
        }
        else if (anim.GetInteger("vAxisRaw") != v)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)v);
            // anim.SetTrigger("isChange 1");
        }
        else
        {
            anim.SetBool("isChange", false);
        }



        if (Input.GetButtonDown("Jump") && scanObj != null && !GameManager.Instance.isnowTalking) //스페이스바 눌러서 대화 넘기기
        {
            GameManager.Instance.Action(scanObj);
        }
    }
    protected void FixedUpdate()
    {
        Move();
        Run();
        DrawRay();
    }
    void Move() //플레이어 이동
    {
        myRigid.MovePosition(myRigid.position + movement * (walkSpeed + applyRunSpeed) * Time.fixedDeltaTime);
    }

    void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            applyRunSpeed = runSpeed;
        else
            applyRunSpeed = 0;
    }

    //플레이어가 맞았을 때 실행되는 함수
    void OnHit(float dmg)
    {
        HP -= dmg;
        if (HP <= 0.0f) Die();
    }

    //플레이어가 죽었을 때 실행되는 함수
    void Die()
    {
        
    }

    void DrawRay()
    {
        Debug.DrawRay(myRigid.position, dirVec, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(myRigid.position, dirVec, 0.7f, LayerMask.GetMask("Object")); //이미 플레이어는 collider 있음. 플레이어 제외 cast를 위해 레이어 나누어서 관리

        if (rayHit.collider != null)
        {
            scanObj = rayHit.collider.gameObject; //raycast된 오브젝트를 변수로 저장하여 활용
        }
        else
            scanObj = null;
    }
    public abstract void ESkill();
    public abstract void FSkill();
    public abstract void CSkill();
    public abstract void VSkill();
}
