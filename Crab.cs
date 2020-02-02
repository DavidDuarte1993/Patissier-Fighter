using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab : MonoBehaviour,IShotable
{
    [SerializeField]
    int hp = 3;
    bool death = false;
   
    public void interact(Vector3 hitPos)
    {
        if (death) return;
        hp--;
        if (hp <= 0)
        {
            death = true;
            GetComponent<Rigidbody>().isKinematic = false;
            EffectDirector.instance.EffectAndPopup(hitPos, "HELPERMELEE", 150);
        }
    }

}
