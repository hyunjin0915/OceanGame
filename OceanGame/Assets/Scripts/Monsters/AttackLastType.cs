using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//후공형 몬스터
public class AttackLastType : Monster
{
    protected override void OnHit(float damage)
    {
        base.OnHit(damage);
        monsterData.isChaseTime = true;
    }
}
