using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//선공형 몬스터
public class AttackFirstType : Monster
{
    private void Update()
    {
        CheckPlayerIsInside();
    }
}
