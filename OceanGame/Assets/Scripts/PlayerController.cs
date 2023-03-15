using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float walkSpeed;

    //필요한 컴포넌트들
    private Rigidbody2D myRigid;
    GameObject scanObj;
    public GameManager manager;
    Vector2 movement; //플레이어 이동시 사용
    Vector3 dirVec; //플레이어가 바라보고 있는방향

    // Start is called before the first frame update
    void Start()
    {
        myRigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = manager.isAction ? 0 :Input.GetAxisRaw("Horizontal");
        movement.y = manager.isAction ? 0 : Input.GetAxisRaw("Vertical");

        if (movement.x >0)
            dirVec = Vector3.right;
        if (movement.x < 0)
            dirVec = Vector3.left;
        if (movement.y > 0)
            dirVec = Vector3.up;
        if (movement.y < 0)
            dirVec = Vector3.down;

        if (Input.GetButtonDown("Jump") && scanObj != null) //스페이스바 눌러서 대화 넘기기
            manager.Action(scanObj);
    }

    private void FixedUpdate()
    {
        Move();
        Debug.DrawRay(myRigid.position, dirVec , new Color(0, 1, 0));
        //이미 플레이어는 collider 있음. 플레이어 제외 cast를 위해 레이어 나누어서 관리
        RaycastHit2D rayHit = Physics2D.Raycast(myRigid.position, dirVec, 0.7f, LayerMask.GetMask("Object"));

        if (rayHit.collider != null)
        {
            scanObj = rayHit.collider.gameObject; //raycast된 오브젝트를 변수로 저장하여 활용
        }
        else
            scanObj = null;
    }
    void Move() //플레이어 이동
    {
        myRigid.MovePosition(myRigid.position + movement * walkSpeed * Time.fixedDeltaTime);
    }
}
