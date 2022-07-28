using System.Collections;
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
    //default 5

    public float momentum = 1;
    public float clampedMomentum;


    public float slashDamage;

    public bool cond1;
    public bool cond2;
    public bool cond3;
    public bool canAttack = true;

    public GameObject pulse;
    public Transform pulseOrigin;

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
        // ps = psParent.transform.GetChild(0).GetComponent<ParticleSystem>();

        GameManager = GameObject.FindGameObjectWithTag("gameManager");
        levelManager = GameObject.FindGameObjectWithTag("levelManager");
        upgrades = GameManager.GetComponent<PlayerInfo>().StingerUpgrades;
        Target target = gameObject.GetComponent<Target>();

        stoppingDistance = 0.5f;





        target.maxHP = 100;   //base HP 100, 30 for each level

      




    }



    void Update()
    {

        #region SET_BOUNDS

        Vector2 tl_a = new Vector2(transform.position.x - 5, transform.position.y + 1);
        Vector2 tr_a = new Vector2(transform.position.x + 100, transform.position.y + 1);
        Vector2 bl_a = new Vector2(transform.position.x - 5, transform.position.y - 10);
        Vector2 br_a = new Vector2(transform.position.x + 100, transform.position.y - 10);

        Vector2 tl_t = new Vector2(transform.position.x - 2, transform.position.y + 1);
        Vector2 tr_t = new Vector2(transform.position.x + 2, transform.position.y + 1);
        Vector2 bl_t = new Vector2(transform.position.x - 2, transform.position.y - 10);
        Vector2 br_t = new Vector2(transform.position.x + 2, transform.position.y - 10);


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

        #endregion


        allEnemies = Physics2D.OverlapAreaAll(tl_a, br_a, 1 << 7);
        targetEnemies = Physics2D.OverlapAreaAll(tl_t, br_t, 1 << 7);

        allAllies = Physics2D.OverlapAreaAll(new Vector2(transform.position.x - 1f, transform.position.y + 1),
            new Vector2(transform.position.x + 10, transform.position.y - 1), 1 << 8);



        if (targetEnemies.Length > 0)
        {
            if (Vector2.Distance(transform.position, GetClosestEntity(targetEnemies).transform.position) <= stoppingDistance * 2 && canAttack)
            {
                canAttack = false;
                StartCoroutine("Attack");
            }
        }



        if (allAllies.Length > 1)
        {
            for (int i = 0; i < allAllies.Length; i++)
            {
                if (allAllies[i].transform.parent.gameObject.name != "Player Crystal")
                {
                    if (allAllies[i].transform.parent.gameObject.GetComponent<AerialPriority>().aerialPriority == gameObject.GetComponent<AerialPriority>().aerialPriority - 1)
                    {

                        followThis = allAllies[i];
                    }
                }
            }


            if (followThis != null)
            {


                if (followThis.transform.parent.gameObject.GetComponent<AerialPriority>().aerialPriority == gameObject.GetComponent<AerialPriority>().aerialPriority - 1 &&
                          Vector2.Distance(transform.position, GetClosestEntity(allEnemies).transform.position) > stoppingDistance * 2 &&
                          Vector2.Distance(transform.position, followThis.transform.position) > stoppingDistance * 2)
                {
                    canStepUp = true;
                }
                else
                {
                    canStepUp = false;
                }
            }
            else
            {
                if (Vector2.Distance(transform.position, GetClosestEntity(allEnemies).transform.position) > stoppingDistance * 2)
                {
                    canStepUp = true;
                }
                else
                {
                    canStepUp = false;
                }
            }
        }
        else if (targetEnemies.Length == 0)
        {
            canStepUp = true;
        }
        else
        {
            if (Vector2.Distance(transform.position, GetClosestEntity(targetEnemies).transform.position) > stoppingDistance * 2)
            {
                canStepUp = true;
            }
            else
            {
                canStepUp = false;
            }


        }




         if (Input.GetKeyDown("z")) {

              Debug.Log(Vector2.Distance(transform.position, GetClosestEntity(allAllies).transform.position));

          }

          



        if (allEnemies.Length > 0)
        {
            rb.velocity = new Vector2(0, 0);
        }


        if (targetEnemies.Length == 0 && canStepUp)
        {

            walk = true;
            StopCoroutine("Attack");

        }





        if (!canStepUp)
        {
            walk = false;

        }
        else
        {
            walk = true;
        }


        if (walk)
        {
            rb.velocity = new Vector2(levelManager.GetComponent<GameFlow>().unitMovementSpeed, 0);
        }




    }



    public IEnumerator Attack()
    {
         Instantiate(pulse, pulseOrigin.position, transform.rotation);
        yield return new WaitForSeconds(1f);
    }




}
