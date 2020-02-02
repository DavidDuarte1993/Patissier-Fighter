using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ADV_BossBar : MonoBehaviour
{
    
    Slider hpbar;
    [SerializeField]
    Image Icon;
    Image[] allImages;
    void Start()
    {
        hpbar = GetComponentInChildren<Slider>();
        allImages = GetComponentsInChildren<Image>();
    }
    public void takeDamage(float hp)
    {
        hpbar.value = hp;
    }
    public void activeBar(Sprite s, float maxHp,float hp)
    {
        Icon.sprite = s;
        Icon.preserveAspect = true;
        foreach(var i in allImages)
        {
            i.enabled = true;
        }
        hpbar.maxValue = maxHp;
        hpbar.value = hp;
    }
    public void deactivateBar()
    {
        foreach (var i in allImages)
        {
            i.enabled = false;
        }
    }
}
