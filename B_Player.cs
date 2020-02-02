using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
/*三角関係、Player, PancakeTower,KitchenHelper
 このスクリプトの目的はデ－タを集まることです。
     */
public class B_Player : MonoBehaviour, ITeam, IAttack<Transform>, Iflour
{
    [HideInInspector]
    public string playerID = "P1_";
    public string getID()
    {
        return playerID;
    }
    Sprite playerMarker;
    ParticleSystem spark;
    public event Action<stateType> onChangeOrder;
    public event Action manaIsFull;
    public event Action takeIngredient;
    [Header("Settings")]
    public Team team = Team.chocolate;
    public string PlayerName;
    public PancakeTower Tower;
    public KitchenHelper Helper = null;
    public List<B_Player> enemyList = new List<B_Player>();
    public STATUS status;
    BulletSpawner bulletSpawner = null;
    public BulletSpawner BULLETSPAWNER { get { return bulletSpawner; } set { bulletSpawner = value; } }
    bool isDeath = false;
    PlayerCanvas canvas;
    Animator anim = null;
    [SerializeField]
    GameObject canvasGameObject = null;
    [Header("Orders_Mesage")]
    [SerializeField]
    GameObject message = null, icon = null;
    [SerializeField]
    SpriteRenderer messageImage = null;
    Vector3 Cameradirection;
    //shootDirectiion
    Vector3 dir;
    
    //アクションのアイコンのため
    public GameObject getMesage()
    {
        return message;
    }
    public GameObject getIcon()
    {
        return icon;
    }
    //カンバスを渡すため
    public PlayerCanvas getCanvas()
    {
        return canvas;
    }
    ParticleSystem skillparticle;

    [SerializeField]
    Camera c = null;
    #region init
    private void Start()
    {
        playerID = "P1_";
        Tower.TowerTeam = team;
        Tower.Init(this);
        Helper = Tower.returnHelper();
        Helper.INIT(this, this.team);
        status = new STATUS();
        Gun newGun = new Gun(100, 20, 1.5f, 1, 1, 1);
        status.changeWeapon(newGun);
        status.getCurrentweapon.Attack = 1;
        Invoke("InIt", 2);
        skillparticle = GetComponentInChildren<ParticleSystem>();
        c = GetComponentInChildren<Camera>();
        spark = GetComponentInChildren<ParticleSystem>();
        playerMarker = icon.GetComponent<SpriteRenderer>().sprite;
       
    }

    //げ－ムマネジャーの処理が終わることを待って、敵のリストを作る
    void InIt()
    {

        //敵のリストを作る
        foreach (B_Player p in B_GameManager.instance.playerList)
        {
            if (p.gameObject != this.gameObject)
            {
                enemyList.Add(p);
            }

        }

    }
    #endregion
    //死イベント
    private void Update()
    {
       if (c == null) return;
       
        if (Input.GetButton(playerID+StaticStrings.L2_key))
        {
            Ray ray = new Ray(c.transform.position, c.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 newDir = hit.point;
                Cameradirection = newDir - transform.position;
            }
        }
        else
        {
            Cameradirection = transform.forward * 50;
        }
        Debug.DrawRay(c.transform.position, c.transform.forward*50, Color.blue);
    }

    //チムを渡す
    public Team getTeam()
    {
        return team;
    }

    #region orders

    public void helperAttack()
    {
        onChangeOrder(stateType.attack);
    }

    public void helperDefenceTower()
    {
        onChangeOrder(stateType.defence);
    }
    public void helperMakeCake()
    {
        onChangeOrder(stateType.cake);
    }



    #endregion
    #region events
    public void onDeath(bool v)
    {

        isDeath = v;
        if (isDeath)
        {
            anim.SetTrigger(StaticStrings.death);
        }

    }
    //healthmanagerにイベントを渡す
    public void onDead()
    {
        //activeEvent
        HealthManager health = GetComponentInParent<HealthManager>();
        if (health)
        {
            health.onDeath += this.onDeath;

        }
    }

    #endregion

    #region skill
    public void Add_SkillPoints(float value)
    {
       status.AddMP(value);

        if (canvas != null)
        {
            canvas.updatePlayerUI();
        }
        if (status.getMP() >= 100)
        {
            if (manaIsFull != null)
            {
                status.UseManapoint(100);
                manaIsFull();

            }
           
        }
    }

    #endregion

    #region passVALUE
    public void reciveAnimator(Animator a)
    {
        anim = a;
    }

    //
    public void initializeCanvas()
    {
        onDead();
        //active canvas
        canvas = GetComponentInChildren<PlayerCanvas>();
        if (canvas == null) return;
        canvas.Init(this);
        canvas.updatePlayerUI();
    }

    //
    public void changeMesageImage(Sprite sprite)
    {
        messageImage.sprite = sprite;
    }

    public void activeteCanvs()
    {
        canvasGameObject.SetActive(true);
    }

    public bool returnDeath()
    {
        return isDeath;
    }
    #endregion
  
    public Vector3 getCameraDir()
    {
        if(c!=null)
        return c.transform.forward * 50;
        else
        {
            return Vector3.zero;
        }
    }
    //マナーポイントを増やす
    public void attack(Transform t)
    {
        IHealth health = t.GetComponent<IHealth>();
        if (health == null)
        {
            Debug.LogError("Target dont'have healthManagement");
            return;
        }
        health.takeDamage(status.getCurrentweapon.Attack);
        status.AddMP(1);
        if (canvas != null)
            canvas.updatePlayerUI();
    }
    public void onTakingIngredient()
    {
        status.Ingredients++;
        if (takeIngredient != null)
        {
           takeIngredient();
        }
        else
        {    if(canvas!=null)
            canvas.activeIngredientImage(false);
        }
    }
    public void activeFlour()
    {
        if (canvas != null)
            canvas.activeFlour();
    }
    public void SetDirectionfForShot()
    {
       BULLETSPAWNER.GiveDirection(Cameradirection);
    }
    public void changeIcon(Sprite s)
    {
        if (s == null)
        {
            s = playerMarker;
        }
        icon.GetComponent<SpriteRenderer>().sprite = s;
    }
    public void changeWeapon(bool v)
    {
        if (v)
        {
            spark.Play();
        }
        else
        {
            spark.Stop();
        }
        bulletSpawner.changeWeapon();
    }
}