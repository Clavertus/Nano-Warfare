using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SDroneAI : MonoBehaviour
{
    public Animator anim;
    public bool walk;

    public Transform sting;

    public bool isShooting;
    public bool isWaiting = true;
    public bool canStepUp;
    public float stoppingDistance;


    public Rigidbody2D rb;

    public float range;

    public GameObject GameManager;
    public GameObject levelManager;
    public int[] upgrades;


    public float attackSpeed = 1;

    public ParticleSystem ps;
    public GameObject psParent;

    public Collider2D[] allEnemies;
    public Collider2D[] allAllies;
    public Collider2D[] targetEnemies;
    public Collider2D followThis;

    public Collider2D thisCollider;
    //default 5

    public float momentum = 1;
    public float clampedMomentum;


    public float slashDamage;

    public bool cond1;
    public bool cond2;
    public bool cond3;
    public bool canAttack = true;

    public Collider2D mem_1;
    public Collider2D mem_2;

    public bool attacking;

    public Collider2D closestAlly;
    public float distanceToClosestAlly;

    public GameObject pulse;
    public Transform pulseOrigin;

    public int filterCalls = 0;

    private Collider2D GetClosestEntity(Collider2D[] allEnemies)
    {
        Collider2D tMin = null;
        float minDist = Mathf.Infinity;
        Vector2 currentPos = transform.position;
        foreach (Collider2D t in allEnemies)
        {
            if (t != null)
            {
                float dist = Vector2.Distance(t.transform.position, currentPos);
                if (dist < minDist)
                {
                    tMin = t;
                    minDist = dist;
                }
            }
        }
        return tMin;
    }


    void Start()
    {
        walk = true;
        attacking = false;
        // ps = psParent.transform.GetChild(0).GetComponent<ParticleSystem>();

        GameManager = GameObject.FindGameObjectWithTag("gameManager");
        levelManager = GameObject.FindGameObjectWithTag("levelManager");
        upgrades = GameManager.GetComponent<PlayerInfo>().StingerUpgrades;
        Target target = gameObject.GetComponent<Target>();

        stoppingDistance = 1.33f;





        target.maxHP = 100;   //base HP 100, 30 for each level

        thisCollider = gameObject.transform.GetChild(0).GetComponent<PolygonCollider2D>();

      




    }


    void FilterOutOfEntityArray(ref Collider2D[] arr, int index)
    {

        for(int i = index; i < arr.Length - 1; i++)
        {
            arr[i] = arr[i + 1];
        }

        Array.Resize(ref arr, arr.Length - 1);


    }


    void Update()
    {

        #region SET_BOUNDS

        Vector2 tl_a = new Vector2(transform.position.x - 5, transform.position.y + 1);
        Vector2 tr_a = new Vector2(transform.position.x + 100, transform.position.y + 1);
        Vector2 bl_a = new Vector2(transform.position.x - 5, transform.position.y - 10);
        Vector2 br_a = new Vector2(transform.position.x + 100, transform.position.y - 10);

        Vector2 tl_t = new Vector2(transform.position.x - .5f, transform.position.y + 1);
        Vector2 tr_t = new Vector2(transform.position.x + 1, transform.position.y + 1);
        Vector2 bl_t = new Vector2(transform.position.x - .5f, transform.position.y - 10);
        Vector2 br_t = new Vector2(transform.position.x + 1, transform.position.y - 10);

        Vector2 tl_f = new Vector2(transform.position.x - 1f, transform.position.y + 1);
        Vector2 tr_f = new Vector2(transform.position.x + 10, transform.position.y + 1);
        Vector2 bl_f = new Vector2(transform.position.x - 1f, transform.position.y - 1);
        Vector2 br_f = new Vector2(transform.position.x + 10, transform.position.y - 1);


        #endregion

        #region RANGE_DRAW

        Debug.DrawLine(tl_a, bl_a, Color.red);
        Debug.DrawLine(bl_a, br_a, Color.red);
        Debug.DrawLine(br_a, tr_a, Color.red);
        Debug.DrawLine(tr_a, tl_a, Color.red);

        Debug.DrawLine(tl_t, bl_t, Color.green);
        Debug.DrawLine(bl_t, br_t, Color.green);
        Debug.DrawLine(br_t, tr_t, Color.green);
        Debug.DrawLine(tr_t, tl_t, Color.green);

        Debug.DrawLine(tl_f, bl_f, Color.blue);
        Debug.DrawLine(bl_f, br_f, Color.blue);
        Debug.DrawLine(br_f, tr_f, Color.blue);
        Debug.DrawLine(tr_f, tl_f, Color.blue);



        #endregion

        #region SET_ENTITY_ARRAYS

        allEnemies = Physics2D.OverlapAreaAll(tl_a, br_a, 1 << 7);
        targetEnemies = Physics2D.OverlapAreaAll(tl_t, br_t, 1 << 7);

        allAllies = Physics2D.OverlapAreaAll(new Vector2(transform.position.x - 1f, transform.position.y + 1),
            new Vector2(transform.position.x + 10, transform.position.y - 1), 1 << 8);

        //filter out PlayerCrystal from allAllies
        
        for (int i = 0; i < allAllies.Length; i++) 
        {
            if(allAllies[i] == thisCollider)
            {
                FilterOutOfEntityArray(ref allAllies, i);
            }             
        }

        for (int i = 0; i < allAllies.Length; i++)
        {
            if (allAllies[i].transform.parent.name == "Player Crystal")
            {
                FilterOutOfEntityArray(ref allAllies, i);
            }
        }




        #endregion





        rb.velocity = new Vector2(levelManager.GetComponent<GameFlow>().unitMovementSpeed, 0);

        closestAlly = GetClosestEntity(allAllies);

        if (GetClosestEntity(allAllies) != null)
        {
            distanceToClosestAlly = Vector2.Distance(transform.position, GetClosestEntity(allAllies).transform.position);
            Debug.Log("Distance to: " + GetClosestEntity(allAllies).gameObject.name + " is: " + distanceToClosestAlly);
        }

        

        if (checkForAlly())
        {
            rb.velocity = Vector2.zero;
        }



        if (targetEnemies.Length == 0)
        {
            StopCoroutine(Attack());
            attacking = false;
            return;
        }

        if (!attacking)
        { 
            StartCoroutine("Attack");
            attacking = true;
        }

    }


    bool checkForAlly()
    {

        if (GetClosestEntity(allAllies) != null
            && Vector2.Distance(transform.position, GetClosestEntity(allAllies).transform.position) < stoppingDistance
            && transform.position.x < GetClosestEntity(allAllies).transform.position.x)
        {
            return true;
        } else
        {
            return false;
        }
        
    }


    public IEnumerator Attack()
    {
         Instantiate(pulse, pulseOrigin.position, transform.rotation);        
         yield return new WaitForSeconds(1f);

         if(attacking)
         StartCoroutine("Attack");


    }




}
