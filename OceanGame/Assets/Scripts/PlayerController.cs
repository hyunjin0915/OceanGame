using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float walkSpeed;

    //�ʿ��� ������Ʈ��
    private Rigidbody2D myRigid;
    GameObject scanObj;
    public GameManager manager;
    Vector2 movement; //�÷��̾� �̵��� ���
    Vector3 dirVec; //�÷��̾ �ٶ󺸰� �ִ¹���

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

        if (Input.GetButtonDown("Jump") && scanObj != null) //�����̽��� ������ ��ȭ �ѱ��
            manager.Action(scanObj);
    }

    private void FixedUpdate()
    {
        Move();
        Debug.DrawRay(myRigid.position, dirVec , new Color(0, 1, 0));
        //�̹� �÷��̾�� collider ����. �÷��̾� ���� cast�� ���� ���̾� ����� ����
        RaycastHit2D rayHit = Physics2D.Raycast(myRigid.position, dirVec, 0.7f, LayerMask.GetMask("Object"));

        if (rayHit.collider != null)
        {
            scanObj = rayHit.collider.gameObject; //raycast�� ������Ʈ�� ������ �����Ͽ� Ȱ��
        }
        else
            scanObj = null;
    }
    void Move() //�÷��̾� �̵�
    {
        myRigid.MovePosition(myRigid.position + movement * walkSpeed * Time.fixedDeltaTime);
    }
}
