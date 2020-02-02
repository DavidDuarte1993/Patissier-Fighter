using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SV_Tower : MonoBehaviour,IHealth
{
    [SerializeField]
    float maxHealth = 100;
    float health = 100;
    [SerializeField]
    Image hpBar = null;
    Text hptext = null;
    [SerializeField]
    GameObject[] pancakePicies=null;
    private void Start()
    {
        health = maxHealth;
        hpBar = GameObject.Find("HPBAR").GetComponent<Image>();
        hptext = hpBar.GetComponentInChildren<Text>();
    }
    public bool getIsDestroyed()
    {
        return isDeatroyed;
    }
    bool isDeatroyed = false;
    private void Update()
    {
        if (hpBar == null) return;
        hpBar.fillAmount = health/100;
        hptext.text = health + "/" + maxHealth;
    }
    public void takeDamage(float damage)
    {
        if (isDeatroyed) return;
        health -= damage;
       
        if (health <= 0)
        {
            isDeatroyed = true;
            GameOver();
        }
        else
        {
            //パンケーキをピースを削除する
            if (health % 10 == 0)
            {
                pancakePicies[(int)health / 10].SetActive(false);
            }
        }
        
    }
    public void Healing(float hp)
    {
        health += hp;
        foreach(var p in pancakePicies)
        {
            if (!p.activeInHierarchy)
            {
                p.SetActive(true);
                break;
            }
        }
        
        if (health >= maxHealth)
        {
            health = maxHealth;
        }
      
    }

    public void GameOver()
    {
        SV_GameManager gm = FindObjectOfType<SV_GameManager>();
        gm.GoToEnd();
    }
}
