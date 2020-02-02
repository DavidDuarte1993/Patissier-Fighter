using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="phase",menuName ="BossPhase/Phase/Intro")]
public class BossPhase :ScriptableObject
{
    
    public float yOffset = 200;
    public float xOffset = 0;
    protected ADV_Boss boss;
    protected Animator anim;
    protected Transform player;
    
    protected float attackCounter = 3;
    public float attackingTime = 15;
    protected Transform transform;
    public float smoothing = 5f;
    Vector3 offset;
    
    public virtual void ENTERSTATE(ADV_Boss b, Animator a)
    {
        
        boss = b;
        transform = b.get_UpObject();
        anim = a;
        boss.wait = false;
        int animationIndex;
        animationIndex = (int)BossAnimations.Idle1;
        anim.SetInteger("PHASE",animationIndex);
        player = b.Get_player();
        offset = transform.position - player.position;
        attackCounter = 3;
        b.activePhaseParts();
    }
   public virtual void UPDATE()
    {
        
         if (player == null) return;
        Vector3 playerpos = new Vector3(player.position.x, yOffset,
            player.position.z);
         Vector3 targetpos = playerpos + offset;
         transform.position = Vector3.Lerp(transform.position, targetpos, smoothing * Time.deltaTime);
        attackCounter -= Time.deltaTime;
        if (attackCounter <= 0)
        {
            attackCounter = attackingTime;
            int rnd = Random.Range(1, 4);
            anim.SetTrigger("ATTACK"+rnd);

        }
    }
   public virtual void EXITSTATE()
    {
        GameObject fadeScreen;
        fadeScreen = GameObject.Find("FadeScreen");
        if (fadeScreen != null)
        {
            fadeScreen.GetComponent<Animation>().Play("fade");
        }
    }
}

public enum BossAnimations
{
    intro=-1,
    Idle1=0,
    ground=1,
    phase3=2,
    rugby=3,
    battleShip=4
}