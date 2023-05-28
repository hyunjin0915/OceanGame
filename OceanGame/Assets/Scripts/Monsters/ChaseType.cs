using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//추격형 몬스터
public class ChaseType : Monster
{
    private void Update()
    {
        CheckPlayerIsInside();
    }
}
