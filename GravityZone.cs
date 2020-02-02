using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SA;
public class GravityZone : MonoBehaviour
{
    
    float force = 10;
    [SerializeField]
    float radius = 10;
    Vector3 direction = Vector3.zero;
    [SerializeField]
    float lifetime = 10;
    void Start()
    {
       Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        Collider[] allHit = Physics.OverlapSphere(transform.position, radius);
        foreach(var h in allHit)
        {
            attract(h);
            
            
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }


    void attract(Collider c)
    {
        if (c.tag == StaticStrings.player || c.tag == StaticStrings.helper
           || c.tag == StaticStrings.AI||c.tag==StaticStrings.cpu||c.tag==StaticStrings.bullet)
        {
            Rigidbody rb = c.GetComponent<Rigidbody>();
            if (rb != null)
            {
                actractiveForce(c.transform.position, rb);
                
            }
            Inputhandler hand = c.GetComponent<Inputhandler>();
            if (hand == null) return;
            hand.frozing();
        }
    }
    public void actractiveForce(Vector3 enemypos,Rigidbody rb)
    {
        direction = transform.position- enemypos ;
        rb.velocity = direction * force;
    }
}
