using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FollowPlayer : MonoBehaviour
{
    //GameObject Player;
    CinemachineVirtualCamera vcam;
    // Start is called before the first frame update
    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        vcam.Follow = GameManager.Instance.player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
