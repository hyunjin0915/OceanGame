using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{

    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;
    private float applyRunSpeed;

    //�ʿ��� ������Ʈ��
    private Rigidbody2D myRigid;
    GameObject scanObj;
   // public GameManager manager;
    Vector2 movement; //�÷��̾� �̵��� ���
    Vector3 dirVec; //�÷��̾ �ٶ󺸰� �ִ¹���

    public Animator anim;// �ִϸ����� ����
    float h;
    float v; //�ִϸ����Ϳ� ����
    bool isHorizonMove;
    public override void Awake()
    {
        base.Awake();
    }
    // Start is called before the first frame update
    void Start()
    {
        //queue = new Queue<string>();
        anim = GetComponent<Animator>(); //anim ��������
        myRigid = GetComponent<Rigidbody2D>();
        //DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = GameManager.Instance.isAction ? 0 :Input.GetAxisRaw("Horizontal");
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
        else if(hUp || vDown)
        {
            //isHorizonMove = h != 0;
            isHorizonMove = (hDown || hUp) || (vUp && h != 0);
        }

        //�ִϸ��̼�
        if (anim.GetInteger("hAxisRaw") != h)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)h);
            //anim.SetTrigger("isChange 1");
        }
        else if(anim.GetInteger("vAxisRaw") != v)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)v);
           // anim.SetTrigger("isChange 1");
        }
        else
        {
            anim.SetBool("isChange", false);
        }
        


        if (Input.GetButtonDown("Jump") && scanObj != null&&!GameManager.Instance.isnowTalking) //�����̽��� ������ ��ȭ �ѱ��
        {
            GameManager.Instance.Action(scanObj);
        }

    }

    private void FixedUpdate()
    {
        Move();
        Run();
        DrawRay();
    }
    void Move() //�÷��̾� �̵�
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

    void DrawRay()
    {
        Debug.DrawRay(myRigid.position, dirVec, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(myRigid.position, dirVec, 0.7f, LayerMask.GetMask("Object")); //�̹� �÷��̾�� collider ����. �÷��̾� ���� cast�� ���� ���̾� ����� ����

        if (rayHit.collider != null)
        {
            scanObj = rayHit.collider.gameObject; //raycast�� ������Ʈ�� ������ �����Ͽ� Ȱ��
        }
        else
            scanObj = null;
    }

   
}
