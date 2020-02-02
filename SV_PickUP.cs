using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace td
{


    public class SV_PickUP : MonoBehaviour
    {
        public pickUpType Type = pickUpType.plin;
        bool isTaked = false;
        public Ammonation plin;
        public Ammonation bullet;
        public BOMB bomb;
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == StaticStrings.player)
            {
                if (isTaked) return;
                isTaked = true;
                switch (Type)
                {
                    case pickUpType.plin:
                        other.GetComponent<SkillKeaper>().addPlin(plin);
                        break;
                    case pickUpType.bullet:
                        other.GetComponentInChildren<TD_BulletSpawner>().changeBullet(bullet);
                        break;
                    case pickUpType.bomb:
                        other.GetComponent<TD_StateManager>().addBomb(bomb);
                        break;
                }

                Destroy(gameObject);
            }
        }
        public enum pickUpType
        {
            plin,
            bullet,
            bomb
        }
    }
    
}
[System.Serializable]
public class Ammonation
{
   public GameObject obj;
   public int Ammo;
   public WPbullet bullet = null;
}