using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "phase", menuName = "BossPhase/Phase/RugbyEvent")]
public class BossRugbyEvent : BossPhase
{
    [SerializeField]
    float attackDelay = 3;
    float attackTime;
    
    public override void ENTERSTATE(ADV_Boss b,Animator a)
    {
        boss = b;
        anim = a;
        boss.wait = false;
        int animationIndex;
        Debug.Log("RugbyPhase");
        animationIndex = (int)BossAnimations.rugby;
        anim.SetInteger("PHASE", animationIndex);
        player = b.Get_player();
        attackTime = attackDelay;
        b.activePhaseParts();
    }
    public override void UPDATE()
    {
        attackTime -= Time.deltaTime;
        
        if (attackTime <= 0)
        {
            attackTime = attackDelay;
            boss.robotsAttack();
        }
    }
    public override void EXITSTATE()
    {
      
    }
}
