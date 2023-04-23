using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPos : MonoBehaviour
{
    public GameObject playerPos;
    public string startPoint; //플레이어가 시작될 위치
    private void Awake()
    {
        if (startPoint == PlayerController.Instance.fromMapName)
        {
            playerPos = GameObject.FindWithTag("Player");
            playerPos.transform.position = gameObject.transform.position;

        }


    }
}
