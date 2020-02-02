using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicRay : MonoBehaviour
{
    [SerializeField]
    GameObject particle = null;
    Vector3 destination;
    [SerializeField]
    float speed = 20;
   
    void Update()
    {
        if (destination == null) return;
        transform.position = Vector3.MoveTowards(transform.position, destination, speed*Time.deltaTime);
    }
   public void setDestination(Vector3 d)
    {
        destination = d;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == StaticStrings.player)
        {
            if (particle != null)
                Instantiate(particle, transform.position, transform.rotation);
            Destroy(gameObject);
        } 
    }
}
