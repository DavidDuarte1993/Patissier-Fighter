using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using td;


public class TD_healthBar : MonoBehaviour
{
    [SerializeField]
    Image healthslider = null;
    [SerializeField]
    Text healthtext = null;
    TD_HealthManager h = null;

    private void Start()
    {
        h = GetComponentInParent<TD_HealthManager>();
        
        healthslider.fillAmount = h.getmaxHealth();
    }

    private void Update()
    {
        healthslider.fillAmount=(h.getHealth ()/h.getmaxHealth());
        healthtext.text = h.getHealth() + "/" + h.getmaxHealth();
    }

}
