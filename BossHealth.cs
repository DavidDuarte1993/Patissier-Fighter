using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class BossHealth : MonoBehaviour
{
    
    public event Action onDeath;
    [SerializeField]
    float TotalHealth = 1000;
  //  float currentHealth;
    List<BossPart> allParts=new List<BossPart>();
    public float getTotalHp()
    {
        return TotalHealth;
    }
  /*  public float getCurrentHp()
    {
        return currentHealth;
    }*/
    public void AddPartToList(BossPart p)
    {

        allParts.Add(p);
    }
    public void removePartToList(BossPart p)
    {
        allParts.Remove(p);
        if (allParts.Count < 1)
        {
            if (onDeath != null)
            {
                onDeath();
            }
        }
    }
}
