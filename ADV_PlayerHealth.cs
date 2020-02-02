using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ADV_PlayerHealth 
{
    public event Action<float> onChangingHealth;
    float health = 5;
    public float getHealth()
    {
        return health;
    }
   
    bool invincible = false;
    float invincibletimer = 3;
    bool death = false;
    SkinnedMeshRenderer rend;
    ADV_Player player;
   

    public ADV_PlayerHealth(SkinnedMeshRenderer skin,ADV_Player p,float hp)
    {
        
        health = hp;
        rend = skin;
        player = p;
        
    }

    public void UpdateHealth()
    {
        if (invincible)
        {
            InvincibilityUpdate();
        }
    }
    public void Healing(float heal)
    {
        health += heal;
        if (onChangingHealth != null)
            onChangingHealth(health);

    }
    public void takeDamage(float damageToTake)
    {
        if (death || invincible) return;
        health -= damageToTake;
        if (onChangingHealth != null)
            onChangingHealth(health);
        if (health <= 0)
        {
            death = true;
            player.Death();
        }
        else
        {
            invincibletimer = 3;
            invincible = true;
        }
        
    }
    void InvincibilityUpdate()
    {
        invincibletimer -= Time.deltaTime;
        if (Mathf.FloorToInt(invincibletimer * 10) % 2 == 0)
        {
            rend.enabled = true;
        }
        else
        {
            rend.enabled = false;
        }
        if (invincibletimer <= 0)
        {
            invincible = false;
            rend.enabled = true;
        }
    }
    public void PassUIDelegate(Action<float> d)
    {
        onChangingHealth += d;
    }
}
