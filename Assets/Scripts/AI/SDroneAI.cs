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





        target.maxHP = 100;   //base HP 100, 30 for each level

      




    }



    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(pulse, pulseOrigin.position, transform.rotation);
        }

        allEnemies = Physics2D.OverlapAreaAll(new Vector2(transform.position.x, transform.position.y + 1),
            new Vector2(transform.position.x + 100, transform.position.y - 1), 1 << 7);

        allAllies = Physics2D.OverlapAreaAll(new Vector2(transform.position.x - 1f, transform.position.y + 1),
            new Vector2(transform.position.x + 10, transform.position.y - 1), 1 << 8);







        if (allEnemies.Length > 0)
        {
            if (Vector2.Distance(transform.position, GetClosestEntity(allEnemies).transform.position) <= stoppingDistance * 2 && canAttack)
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
                    if (allAllies[i].transform.parent.gameObject.GetComponent<Priority>().priority == gameObject.GetComponent<Priority>().priority - 1)
                    {

                        followThis = allAllies[i];
                    }
                }
            }


            if (followThis != null)
            {


                if (followThis.transform.parent.gameObject.GetComponent<Priority>().priority == gameObject.GetComponent<Priority>().priority - 1 &&
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
        else if (allEnemies.Length == 0)
        {
            canStepUp = true;
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




         if (Input.GetKeyDown("z")) {

              Debug.Log(Vector2.Distance(transform.position, GetClosestEntity(allAllies).transform.position));

          }

          



        if (allEnemies.Length > 0)
        {
            rb.velocity = new Vector2(0, 0);
        }


        if (allEnemies.Length == 0 && canStepUp)
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
        yield return false;
    }




}
