using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class ADV_Player : MonoBehaviour, IShotable,IHealth
{
    ADV_UIManager uiManager;
    #region values

    [SerializeField]
    bool isRunning = false;
    [SerializeField]
    protected float jumpForce = 10;
    protected Animator anim;
    float gravityScale = 10f;
    CapsuleCollider coll = null;
    Rigidbody rb;
    ADV_PlayerHealth playerHealth;
    public ADV_PlayerHealth Get_Player_Health()
    {
        return playerHealth;
    }
    bool death = false;
    SkinnedMeshRenderer rend;
    float health = 5;
    #endregion
    #region grounCheck
    Vector3 colliderSize()
    {
        Vector3 size = new Vector3(coll.bounds.center.x,
            coll.bounds.min.y, coll.bounds.center.z);
        return size;
    }

    internal void Death()
    {
        death = true;
        anim.SetTrigger("D2");
    }

    public bool onGround()
    {
        return Physics.Raycast(transform.position, 
            Vector3.down, coll.bounds.extents.y + 0.1f);
    }
    #endregion
    SV_CameraFollow[] Allcam;
    #region Init
    private void Start()
    {
        Allcam = FindObjectsOfType<SV_CameraFollow>();
        foreach (var c in Allcam)
        {
            if(c!=null)
            c.settarget(this.transform);
        }
        anim = GetComponent<Animator>();
        
        coll = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        rend = GetComponentInChildren<SkinnedMeshRenderer>();
        if (PlayerPrefs.HasKey(StaticStrings.savedHealth))
        {
            health = PlayerPrefs.GetInt(StaticStrings.savedHealth);
        }
        else
        {
            Debug.Log("saving in not finded");
            health = 5;
        }
        //Create HealthClass
        playerHealth = new ADV_PlayerHealth(rend,this,health);
        uiManager = FindObjectOfType<ADV_UIManager>();
        //pass values to UIManager
        if (uiManager != null)
        {
            uiManager.OnChangingHealth(health);
            playerHealth.PassUIDelegate(uiManager.OnChangingHealth); 
        }
        
    }
    #endregion

    public void Update()
    {
        if (death) return;
        if (isRunning)
        {
            anim.SetFloat(StaticStrings.move, 1);
        }
        if(playerHealth!=null)
        playerHealth.UpdateHealth();
        Gravity();
    }
 
    public void Gravity()
    {
        if (!onGround())
        {
            rb.velocity +=Vector3.up*Physics.gravity.y* gravityScale * Time.deltaTime;
        }
    }
    public void interact(Vector3 hitpos)
    {
        if (!onGround()) return;
        anim.SetTrigger(StaticStrings.Jump);
        rb.velocity= Vector3.up * jumpForce;
        
    }
    #region HP
    public void Healing(float heal)
    {
        playerHealth.Healing(heal);
       
    }

    public void takeDamage(float damageToTake)
    {
        if (Soundmanager.instance != null)
        {
            Soundmanager.instance.PlaySeByName("Player_Hurt");
        }
        playerHealth.takeDamage(damageToTake);
       
    }
    #endregion
 
}
