using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPos : MonoBehaviour
{
    public GameObject playerPos;
    public string startPoint; //플레이어가 시작될 위치
    private void Start()
    {
        if (startPoint == GameManager.Instance.playerLogic.fromMapName)
        {
            playerPos = GameManager.Instance.player;
            playerPos.transform.position = gameObject.transform.position;

        }
    }
}
