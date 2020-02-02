using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectGenerator : MonoBehaviour
{
    [SerializeField]
    Transform[] points = null;
    
    public void generateEffectAtPoint()
    {
        if (EffectDirector.instance != null)
        {
            foreach(var p in points)
            {
                EffectDirector.instance.spawnInPlace(p.position,0);
            }
            
        }
    }
}
