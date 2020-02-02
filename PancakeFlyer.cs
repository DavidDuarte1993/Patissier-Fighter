using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PancakeFlyer : ADV_Enemies
{
    [SerializeField]
    Transform bomb = null;
    bool haveBomb = true;
    Rigidbody rb;

    public override void init()
    {
        rb = bomb.GetComponent<Rigidbody>();
    }
    public override void Updating()
    {
        if (!canmove) return;
       
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
        float distance = Vector3.Distance(transform.position, targetPos);
        if (distance <= 1)
        {
            releseBomb();
        }
        transform.LookAt(targetPos, Vector3.up);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }
    public void releseBomb()
    {
        if (!haveBomb) return;
        haveBomb = false;
        bomb.SetParent(null);
        rb.isKinematic = false;
        bomb.GetComponent<Collider>().enabled = true;
        int rand = Random.Range(1, 3);
        float point;
        if (rand == 1) { point = 50; }
        else { point = -50; }
        targetPos = new Vector3(targetPos.x, transform.position.y, targetPos.z + point);

    }
    public override void follow(Transform t)
    {
        base.follow(t);
        targetPos = new Vector3(target.position.x, transform.position.y, target.position.z);
    }
    public override void interact(Vector3 hitpos)
    {
        
        if (haveBomb)
        {
            releseBomb();
            if (EffectDirector.instance != null)
            {
                EffectDirector.instance.playInPlace(hitpos, "PUFF");
            }
        }
        else
        {
            if (EffectDirector.instance != null)
            {
                EffectDirector.instance.generatePopUp(scorepoints);
            }
            Destroy(gameObject);
        }
        
       
    }
}
