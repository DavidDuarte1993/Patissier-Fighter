using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADV_PickUps : MonoBehaviour
{
    public PickUpType tp=PickUpType.Heal;
    public string Effect = "";
    public float valor = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == StaticStrings.player)
        {
            IHealth h = other.GetComponent<IHealth>();
            switch (tp)
            {
                case PickUpType.Heal:
                    if(h!=null)
                    h.Healing(valor);
                    if (EffectDirector.instance != null && Effect != "")
                    {
                        EffectDirector.instance.playInPlace(other.transform.position, Effect);
                    }
                    break;
                case PickUpType.Poison:
                    if (h != null)
                        h.takeDamage(valor);
                    if (EffectDirector.instance != null && Effect != "")
                    {
                        EffectDirector.instance.playInPlace(transform.position, Effect);
                    }
                    break;
                case PickUpType.PointsUp:
                    if (EffectDirector.instance != null && Effect != "")
                    {
                        EffectDirector.instance.generatePopUp((int)valor);
                    }
                    break;
            }
          
            Destroy(gameObject);
        }
    }
   public enum PickUpType
    {
        Heal,
        Poison,
        PointsUp
    }
}
