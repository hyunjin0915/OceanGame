using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float walkSpeed;

    //필요한 컴포넌트들
    private Rigidbody2D myRigid;

    Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        myRigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        Move();
    }
    void Move()
    {
        myRigid.MovePosition(myRigid.position + movement * walkSpeed * Time.fixedDeltaTime);
    }
}
