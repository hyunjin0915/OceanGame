using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : Player
{
    public PlayerKind playerKind;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        playerKind = PlayerKind.PlayerTest;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
    private void FixedUpdate()
    {
        base.FixedUpdate();
    }
    public override void ESkill()
    {
    }
    public override void FSkill()
    {
    }
    public override void CSkill()
    {
    }
    public override void VSkill()
    {
    }


}
